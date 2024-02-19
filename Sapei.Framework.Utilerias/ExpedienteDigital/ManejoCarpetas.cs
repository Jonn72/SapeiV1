using Sapei.Framework.BaseDatos;
using Sapei.Framework.Configuracion;
using Sapei.Framework.Utilerias.Funciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web;

namespace Sapei.Framework.Utilerias.ExpedienteDigital
{
	public class ManejoCarpetas
	{

		#region Variables
		Sistema _oSistema;
		#endregion
		#region Constructor
		public ManejoCarpetas(Sistema poSistema)
		{
			_oSistema = poSistema;
		}
		#endregion

		#region Generales
		public static bool ValidaYCrearCarpetas(string psRuta)
		{
			return Sapei.Framework.Utilerias.Funciones.FuncionesNAS.GeneraCarpeta(psRuta);
		}
		#endregion
		#region Alumnos
		public bool CrearCarpetasRaiz(string psPeriodo, enmSistema poEnmSistema)
		{
			string lsUsuario;
			string lsContraseña;
			string lsServidor;
			DataTable loDatosArchivo;
			try
			{

				loDatosArchivo = RegresaInformacionCarpetas(psPeriodo, enmExpDigRutas.RaizAlumnos);
				
				if (loDatosArchivo.Rows.Count == 0)
					return false;
				
				lsContraseña = FuncionesCifrado.DecryptRJ256(loDatosArchivo.Rows[0].Field<string>("contraseña").Trim());
				lsServidor = @loDatosArchivo.Rows[0].Field<string>("servidor");
				lsUsuario = loDatosArchivo.Rows[0].Field<string>("usuario").Trim();

				NetworkCredential loCredencial = new NetworkCredential(lsUsuario, lsContraseña);

				using (var connection = new ConnectToSharedFolder(lsServidor, loCredencial, poEnmSistema))
				{
					foreach (DataRow loDr in loDatosArchivo.Rows)
					{
						Sapei.Framework.Utilerias.Funciones.FuncionesNAS.GeneraCarpeta(Path.Combine(lsServidor,loDr.Field<string>("ruta")));
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				ManejoCorreos.EnviaNotificacionExpedianteDigital(ex.Message + " Modulo: FuncionesNas/GeneraCarpetaRaizAlumnos", Configuracion.enmSistema.SAPEI);
				return false;
			}
		}

		public void CrearCarpetasAlumnos(string psPeriodo, string psLista)
		{
			DataTable loDT;
			loDT = new DataTable();
			string lsRuta;
			string lsRaiz;
			using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
			{
				loDT.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_exp_dig_alumnos_rutas"));
			}

			if (loDT.Rows.Count == 0)
				return;

			lsRaiz = HttpContext.Current.Server.MapPath("~/Nas") + "\\" + psPeriodo.Substring(0, 4) + "\\" + psPeriodo.RegresaDescripcionPeriodo("", true);

			foreach (string lsNoControl in psLista.Split('|'))
			{
				foreach (DataRow loRw in loDT.Rows)
				{
					lsRuta = lsRaiz + string.Format(@"\\{0}{1}", lsNoControl, loRw.Field<string>("ruta"));
					Sapei.Framework.Utilerias.Funciones.FuncionesNAS.GeneraCarpeta(lsRuta);
				}
			}

		}

		private DataTable RegresaInformacionCarpetas(string psPeriodo, enmExpDigRutas poTipo)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@periodo",psPeriodo));
			loParametros.Add(new ParametrosSQL("@tipo_ruta", poTipo));
			using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
			{
				loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_exp_dig_alumnos_rutas",loParametros));
			}
			return loDt;
		}
		#endregion
	}
}
