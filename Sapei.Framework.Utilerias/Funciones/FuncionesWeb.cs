using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using Sapei.Framework.BaseDatos;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Sapei.Framework.Utilerias;


namespace Sapei.Framework.Utilerias.Funciones
{
	/// <summary>
	/// Clases para el manejo de funciones web
	/// </summary>
	public static class FuncionesWeb
	{
		class Domicilio
		{
			public int id;
			public string CodigoPostal;
			public string Colonia;
			public string Ciudad;
			public string Entidad;
		}
		class SelectHTML
		{
			public string Valor;
			public string Descripcion;
		}
		class EchartHTML
		{
			public string value;
			public string name;
		}
		/// <summary>
		/// Esta función permite descargar un archivo
		/// </summary>
		/// <param name="psNombreArchivo">Nombre del archivo</param>
		/// <param name="poResponse">Response actual de la pagina</param>
		/// <remarks></remarks>
		public static void DescargaArchivo(string psNombreArchivo, ref System.Web.HttpResponse poResponse)
		{
			System.IO.FileInfo loDescarga;
			string lsMensaje;
			loDescarga = null;

			try
			{
				if (File.Exists(psNombreArchivo))
				{
					loDescarga = new System.IO.FileInfo(psNombreArchivo);
					poResponse.Clear();
					poResponse.AddHeader("Content-Disposition", "attachment; filename=" + loDescarga.Name);
					poResponse.AddHeader("Content-Length", loDescarga.Length.ToString());
					poResponse.ContentType = "application/octet-stream";
					poResponse.WriteFile(psNombreArchivo);
				}
				else
				{
					lsMensaje = ""; //Co_Traduccion.Traduce(clsSICTraduccion.enTipoTexto.MENSAJE, "msjNoExisteElArchivo");
					if (lsMensaje == "")
						lsMensaje = "No existe el archivo {1}";

					lsMensaje.Replace("{1}", psNombreArchivo);
					throw new ApplicationException(lsMensaje);
				}
			}
			finally
			{
				if (loDescarga != null)
					loDescarga = null;
			}
		}

		/// <summary>
		/// Metodo que permite descarga un archivo binario
		/// </summary>
		/// <param name="psNombreArchivo">NOmbre del archivo</param>
		/// <param name="poResponse">Respose actual</param>
		/// <param name="pabyArchivo">Array byte del archivo</param>
		public static void DescargaArchivo(string psNombreArchivo, System.Web.HttpResponse poResponse, byte[] pabyArchivo)
		{
			if (Object.Equals(pabyArchivo, null))
				return;
			poResponse.Clear();
			poResponse.ClearHeaders();
			poResponse.AddHeader("Content-Disposition", "attachment; filename=" + psNombreArchivo);
			poResponse.AddHeader("Content-Length", pabyArchivo.Length.ToString());
			poResponse.ContentType = "application/octet-stream";
			poResponse.BinaryWrite(pabyArchivo);
			poResponse.Flush();
		}

		/// <summary>
		/// Manejas the excepcion. Sirve para capturar cualquier problema en la version web y no enviar ninguna notificaicon
		/// </summary>
		/// <param name="poExcepcion">The po excepcion.</param>
		public static void ManejaExcepcion(Exception poExcepcion, string psModulo = "Pendiente Modulos WEB...")
		{
			if (Object.Equals(SesionSapei.Sistema, null))
				return;
			if (!(poExcepcion is SolExcepcion))
			{
				((Sistema)SesionSapei.Sistema).GrabaLog(poExcepcion, psModulo);
			}
		}

		/// <summary>
		/// Funcoin que regresa el nombre la pagina actual
		/// </summary>
		/// <returns></returns>
		public static string RegresaNombrePaginaActual()
		{
			if (HttpContext.Current.Request.ApplicationPath == "/")
				return HttpContext.Current.Request.Url.PathAndQuery.Substring(1, HttpContext.Current.Request.Url.PathAndQuery.Length - 1);
			return HttpContext.Current.Request.Url.PathAndQuery.Replace(HttpContext.Current.Request.ApplicationPath, "");
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="psCodigoPostal"></param>
		/// <returns></returns>
		public static object RegresaDomicilioCodigoPostal(string psCodigoPostal)
		{
			StringBuilder lsQuery;
			List<Domicilio> loLista = new List<Domicilio>();
			DataTable loDt;
			lsQuery = new StringBuilder();
			lsQuery.Append("Select ");
			lsQuery.Append(" id, colonia, ciudad_localidad ciudad,");
			lsQuery.Append(" (select nombre_entidad from entidades_federativas where entidad_federativa = C.ent_fed) entidad");
			lsQuery.AppendFormat(" From [{0}].[{1}].[{2}] C", SesionSapei.Sistema.Servidor.Principal.BaseDatos.Catalogo, SesionSapei.Sistema.Servidor.Principal.BaseDatos.Propietario, "c_p");
			lsQuery.Append(" where");
			lsQuery.AppendFormat(" cod_post= {0}", psCodigoPostal);
			loDt = SesionSapei.Sistema.Conexion.RegresaDataTable(lsQuery);
			if (loDt.Rows.Count == 0)
				return null;
			foreach (DataRow loDr in loDt.Rows)
			{
				loLista.Add(new Domicilio()
				{
					id = loDr.RegresaValor<int>("id"),
					CodigoPostal = psCodigoPostal,
					Colonia = loDr.RegresaValor<string>("colonia"),
					Ciudad = loDr.RegresaValor<string>("ciudad"),
					Entidad = loDr.RegresaValor<string>("entidad")
				});
			}
			return loLista;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="psTabla"></param>
		/// <param name="psCampoValor"></param>
		/// <param name="psCampoDescripcion"></param>
		/// <param name="psFiltro"></param>
		/// <param name="psOrdena"></param>
		/// <returns></returns>
		public static object RegresaElementosSelect(string psTabla, string psCampoValor, string psCampoDescripcion, string psFiltro = null, string psOrdena = null, bool pbEsTablaDeConsulta = false, short piTop = 0, string psGroup = null, bool pbRegresaTabla = false)
		{
			StringBuilder lsQuery;
			List<SelectHTML> loLista = new List<SelectHTML>();
			DataTable loDt;
			lsQuery = new StringBuilder();
			if (piTop > 0)
				lsQuery.AppendFormat("Select top({0})", piTop);
			else
				lsQuery.Append("Select ");
			lsQuery.AppendFormat(" {0} valor, {1} descripcion", psCampoValor, psCampoDescripcion);
			if (pbEsTablaDeConsulta)
				lsQuery.AppendFormat(" From ({0}) as Tabla", psTabla);
			else
				lsQuery.AppendFormat(" From [{0}].[{1}].[{2}] C", SesionSapei.Sistema.Servidor.Principal.BaseDatos.Catalogo, SesionSapei.Sistema.Servidor.Principal.BaseDatos.Propietario, psTabla);
			if (!string.IsNullOrEmpty(psFiltro))
			{
				lsQuery.Append(" where");
				lsQuery.AppendFormat(" {0}", psFiltro);
			}
			if (!string.IsNullOrEmpty(psGroup))
			{
				lsQuery.Append(" group by ");
				lsQuery.AppendFormat(" {0}", psGroup);
			}
			if (!string.IsNullOrEmpty(psOrdena))
			{
				lsQuery.Append(" order by ");
				lsQuery.AppendFormat(" {0}", psOrdena);
			}
			loDt = SesionSapei.Sistema.Conexion.RegresaDataTable(lsQuery);
			if (loDt.Rows.Count == 0)
				return null;
			if (pbRegresaTabla)
				return loDt;
			foreach (DataRow loDr in loDt.Rows)
			{
				loLista.Add(new SelectHTML()
				{
					Valor = loDr.RegresaValor<string>("valor"),
					Descripcion = loDr.RegresaValor<string>("descripcion")
				});
			}
			return loLista;
		}
		/// <summary>
		/// Funcion para cargar elementos select a partir de la tabla siscombos
		/// </summary>
		/// <param name="psSelectID"></param>
		/// <param name="psFiltro"></param>
		/// <returns></returns>
		public static object RegresaElementosSelect(string psSelectID, string psFiltro = null)
		{
			StringBuilder lsFiltro;
			lsFiltro = new StringBuilder();
			lsFiltro.AppendFormat(" combo ='{0}' ", psSelectID);
			if (!string.IsNullOrEmpty(psFiltro))
			{
				lsFiltro.Append(" and ");
				lsFiltro.AppendFormat(" {0} ", psFiltro);

			}
			return RegresaElementosSelect("sisCombos", "valor", "descripcion", lsFiltro.ToString());
		}
		public static object RegresaElementosSelect(string psNombreTabla, string psCampoValor, string psCampoDescripcion, string psFiltro, bool pbRegresaTabla)
		{
			return RegresaElementosSelect(psNombreTabla, psCampoValor, psCampoDescripcion, psFiltro, null, false, 0, null, pbRegresaTabla);
		}
		public static object RegresaElementosSelect(string psNombreTabla, string psCampoValor, string psCampoDescripcion, bool pbRegresaTabla)
		{
			return RegresaElementosSelect(psNombreTabla, psCampoValor, psCampoDescripcion, null, null, false, 0, null, pbRegresaTabla);
		}
		public static object RegresaEstadistica(string psNombreTabla, string psCampo)
		{
			return RegresaEstadistica(psNombreTabla, psCampo, null, null, 0);
		}
		public static object RegresaEstadistica(string psNombreTabla, string psCampo, int piTop = 0)
		{
			return RegresaEstadistica(psNombreTabla, psCampo, null, null, 0, piTop);
		}
		public static object RegresaEstadistica(string psNombreTabla, string psCampo, int piNombreTabla = 0, int piTop = 0)
		{
			return RegresaEstadistica(psNombreTabla, psCampo, null, null, piNombreTabla, piTop);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="psNombreTabla"></param>
		/// <param name="psCampo"></param>
		/// <param name="psFiltro"></param>
		/// <returns></returns>
		public static object RegresaEstadistica(string psNombreTabla, string psCampo, string psCampoGroup = null, string psFiltro = null, int piTablaConsulta = 0, int piTop = 0, string psOrderBy = null, string psCampoValor = null)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();

			if (string.IsNullOrEmpty(psCampoGroup))
				psCampoGroup = psCampo;
			if (piTop == 0)
				lsConsulta.AppendFormat("SELECT  {0} as descripcion, ", psCampo);
			else
				lsConsulta.AppendFormat("SELECT TOP ({1})  {0} as descripcion, ", psCampo, piTop);

			if (string.IsNullOrEmpty(psCampoValor))
				lsConsulta.AppendFormat(" count({0}) valor", psCampoGroup);
			else
				lsConsulta.AppendFormat(" {0} valor", psCampoValor);

			switch (piTablaConsulta)
			{
				case 0: //Tabla de base principal
					lsConsulta.AppendFormat(" From [{0}].[{1}].[{2}]", SesionSapei.Sistema.Servidor.Principal.BaseDatos.Catalogo, SesionSapei.Sistema.Servidor.Principal.BaseDatos.Propietario, psNombreTabla);
					break;
				case 1://Tabla de consulta
					lsConsulta.AppendFormat(" From ({0}) Tabla", psNombreTabla);
					break;
				case 2:
					lsConsulta.AppendFormat(" From {0} Tabla", psNombreTabla);
					break;

			}
			if (!string.IsNullOrEmpty(psFiltro))
				lsConsulta.AppendFormat(" where {0}", psFiltro);
			lsConsulta.AppendFormat(" GROUP BY  {0}", psCampoGroup);
			if (!string.IsNullOrEmpty(psOrderBy))
				lsConsulta.AppendFormat(" ORDER BY  {0}", psOrderBy);
			return ConvierteObjetoHtmlJson(SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta));
		}

		private static object ConvierteObjetoHtmlJson(DataTable poDt)
		{
			string lsValor;
			List<EchartHTML> loLista = new List<EchartHTML>();
			if (poDt.Rows.Count == 0)
				return null;
			foreach (DataRow loDr in poDt.Rows)
			{
				lsValor = loDr.RegresaValor<string>("valor");
				loLista.Add(new EchartHTML()
				{
					value = lsValor,
					name = (loDr.RegresaValor<string>("descripcion")).RTrim() + "( " + lsValor + " )"
				});
			}
			return loLista;
		}

	}
}