using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias.Funciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sapei.Framework.Utilerias.ExpedienteDigital
{
	public class ManejoArchivos
	{
		#region Variables
		Sistema _oSistema;
		#endregion
		#region Constructor
		public ManejoArchivos(Sistema poSistema)
		{
			_oSistema = poSistema;
		}

		public ManejoArchivos()
		{
		}
		#endregion
		#region General
		public DataTable RegresaDatosArchivo(enmTiposDocumentos penmYipoDoc)
		{
			StringBuilder lsQuery;
			lsQuery = new StringBuilder();
			lsQuery.Append(" SELECT nombre, extension, ");
			lsQuery.Append("  TRIM(ruta) + '\\' + TRIM(carpeta) ruta, incluye_fecha,");
			lsQuery.Append(" servidor , TRIM(usuario) usuario , TRIM(contraseña) contraseña");
			lsQuery.Append(" FROM exp_dig_documentos F inner join exp_dig_carpetas C");
			lsQuery.Append(" on F.clave_carpeta = C.clave ");
			lsQuery.Append(" inner join exp_dig_nas N on N.id = C.credencial_red");
			lsQuery.AppendFormat(" WHERE UPPER(nombre) = '{0}'", penmYipoDoc.ToString().ToUpper());

			return _oSistema.Conexion.RegresaDataTable(lsQuery);

		}
		public void CrearArchivo(Sapei.Framework.Configuracion.enmSistema penmTipoSistema, string psruta, byte[] pbyArchivo, enmTiposDocumentos penmTipoDoc, enmTipoArchivo penmTipoArchivo, bool pbIncluyeFecha = false)
		{
			string lsFecha = "";
			string lsRuta = "";
			string lsNombreArchivo;
			lsRuta = psruta;
			if (pbIncluyeFecha)
				lsFecha = DateTime.Now.ToString("_dd-MM-yyyy");
			lsNombreArchivo = string.Format("{0}{1}.{2}", penmTipoDoc.ToString(), lsFecha, penmTipoArchivo.ToString());
			ManejoCarpetas.ValidaYCrearCarpetas(lsRuta);
			lsRuta = Path.Combine(lsRuta, lsNombreArchivo);

			using (FileStream fs = new FileStream(lsRuta, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
			{
				fs.Write(pbyArchivo, 0, pbyArchivo.Length);
				fs.Flush();
				fs.Close();
			}
		}

		public bool ArchivaDocumento(Sapei.Framework.Configuracion.enmSistema penmSistema, HttpPostedFileBase poFile, string psTipoDoc, enmTipoArchivo penmFromato, string psRuta, NetworkCredential poCredencial, string psServidor)
		{
			try
			{
				string lsRuta;
				lsRuta = Path.Combine(psServidor, psRuta);
				using (var connection = new ConnectToSharedFolder(psServidor, poCredencial, penmSistema))
				{
					FuncionesNAS.GeneraCarpeta(lsRuta);
					poFile.SaveAs(Path.Combine(lsRuta, psTipoDoc + "." + penmFromato));
				}
				return true;
			}
			catch (Exception ex)
			{
				ManejoCorreos.EnviaNotificacionExpedianteDigital(ex.Message + " Modulo: ManejoArchivos/ArchivaDocumento", Configuracion.enmSistema.SAPEI);
				return false;
			}
		}

		public bool ArchivaDocumento(string psControl, Byte[] poFile, enmTiposDocumentos penmTipoDoc)
		{
			try
			{
				string lsRuta;
				string lsNombreArchivo;
				string lsExtension;
				string lsServidor;
				string lsUsuario;
				string lsContraseña;
				DataTable loDatos;
				loDatos = RegresaDatosRutaConexion("", 2, psControl, Convert.ToInt32(penmTipoDoc));

				lsRuta = loDatos.Rows[0].RegresaValor<string>("ruta_doc");
				lsNombreArchivo = loDatos.Rows[0].RegresaValor<string>("nombre_doc");
				lsExtension = loDatos.Rows[0].RegresaValor<string>("extension");
				lsServidor = loDatos.Rows[0].RegresaValor<string>("servidor");
				lsUsuario = loDatos.Rows[0].RegresaValor<string>("usuario");
				lsContraseña = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.DecryptRJ256(loDatos.Rows[0].RegresaValor<string>("contraseña"));

				using (var connection = new ConnectToSharedFolder(lsServidor, new NetworkCredential(lsUsuario, lsContraseña), Configuracion.enmSistema.SAPEI))
				{
					FuncionesNAS.GeneraCarpeta(lsRuta);
					File.WriteAllBytes(Path.Combine(lsRuta, lsNombreArchivo + "." + lsExtension),poFile);
				}
				return true;
			}
			catch (Exception ex)
			{
				ManejoCorreos.EnviaNotificacionExpedianteDigital(ex.Message + " Modulo: ManejoArchivos/ArchivaDocumento", Configuracion.enmSistema.SAPEI);
				return false;
			}
		}
		public byte[] RegresaDocumento(Sapei.Framework.Configuracion.enmSistema penmSistema, string psRuta, NetworkCredential poCredencial, string psServidor)
		{
			try
			{
				string lsRuta;
				lsRuta = Path.Combine(psServidor, psRuta);
				byte[] lbsDocumento;
				using (var connection = new ConnectToSharedFolder(psServidor, poCredencial, penmSistema))
				{
					using (FileStream lofs = File.Open(lsRuta, FileMode.Open))
					{
						lbsDocumento = new byte[lofs.Length];
						lofs.Read(lbsDocumento, 0, lbsDocumento.Length);
					}
				}
				return lbsDocumento;
			}
			catch (Exception ex)
			{
				ManejoCorreos.EnviaNotificacionExpedianteDigital(ex.Message + " Modulo: ManejoArchivos/RegresaDocumento", Configuracion.enmSistema.SAPEI);
				return null;
			}
		}
		#endregion
		#region Alumnos
		public byte[] RegresaDocumentoAlumno(string psControl, int piClaveDoc)
		{
			string lsRuta;
			string lsNombreArchivo;
			string lsExtension;
			string lsServidor;
			string lsUsuario;
			string lsContraseña;
			DataTable loDatos = RegresaDatosRutaConexion("",2,psControl,piClaveDoc);
			if (loDatos.Rows.Count == 0)
				return null;
			lsRuta = loDatos.Rows[0].RegresaValor<string>("ruta_doc");
			lsNombreArchivo = loDatos.Rows[0].RegresaValor<string>("nombre_doc");
			lsExtension = loDatos.Rows[0].RegresaValor<string>("extension");
			lsServidor = loDatos.Rows[0].RegresaValor<string>("servidor");
			lsUsuario =  loDatos.Rows[0].RegresaValor<string>("usuario");
			lsContraseña  = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.DecryptRJ256(loDatos.Rows[0].RegresaValor<string>("contraseña"));
			lsRuta = Path.Combine(lsRuta,lsNombreArchivo+"."+lsExtension);
			return RegresaDocumento(Sapei.Framework.Configuracion.enmSistema.SAPEI,lsRuta,new NetworkCredential(lsUsuario,lsContraseña),lsServidor);
		}

		private DataTable RegresaDatosRutaConexion(string psPeriodo, byte pbyTipoRuta, string psControl, int piClaveDoc)
		{
			DataTable loDatos = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
			loParametros.Add(new ParametrosSQL("@tipo_ruta", 2));
			loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
			loParametros.Add(new ParametrosSQL("@clave_doc", piClaveDoc));

			using (var conexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDatos.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_exp_dig_alumnos_rutas", loParametros));
			}

			return loDatos;
		}
		#endregion
	}
}
