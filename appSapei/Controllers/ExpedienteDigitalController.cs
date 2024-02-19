using appSapei.App_Start;
using appSapei.Clases;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using Sapei.Framework.Utilerias.ExpedienteDigital;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class ExpedienteDigitalController : Controller
    {
		// GET: ExpedienteDigital
		#region Alumnos
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult ArchivarCargasAcademicas()
		{
			try
			{
				ProcesosMQ.ArchivaCargasAcademicas();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se inicio el proceso de archivo digital",true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		#endregion

		#region ServciosEscolares
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult ValidacionDocumentosEstudiante()
		{
			try
			{
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				string lsProcedimiento = "";
				lsProcedimiento = "pac_exp_dig_doc_alumnos_generales";

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					{
						loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader(lsProcedimiento, loParametros));
					}
				}

				ViewData["Tabla"] = loDt;

				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult MuestraDocumentosEstudiante(string psControl)
		{
			try
			{
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				string lsProcedimiento = "";
				lsProcedimiento = "pac_exp_dig_doc_alumnos";
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					{
						loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader(lsProcedimiento, loParametros));
					}
				}
				ViewData["Datos"] = loDt;

				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public ActionResult MuestraDocumentosEscolaresEstudiante(string psControl, enmTiposDocumentos penmDoc)
		{
			try
			{
				ReportesGenerales loReporte;
				string lsNoControl;
				byte[] lbyReporte;
				string lsNombreDoc = "";

				lsNombreDoc = penmDoc.ToString();
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ExpedienteDigital"), lsNombreDoc + ".rdlc");
				loReporte.RutaReportes = path;
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
				{
					lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				}
				else
				{
					lsNoControl = psControl;
				}

				lbyReporte = loReporte.RegresaDocumentosEscolares(lsNoControl, lsNombreDoc);

				ViewData["pdfbase64"] = System.Convert.ToBase64String(lbyReporte);
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public JsonResult ArchivaDocumentosEstudiante(string psControl, enmTiposDocumentos penmArchivo)
		{
			try
			{
				//ReportesGenerales loReporte;
				//Sapei.Framework.Utilerias.ExpedienteDigital.ManejoArchivos loArchivos = new Sapei.Framework.Utilerias.ExpedienteDigital.ManejoArchivos();
				//Byte[] lbysDocumento;
				//loReporte = new ReportesGenerales(SesionSapei.Sistema);
				//DataTable loDt = new DataTable();
				//List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				//string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Reinscripcion/"), "nombre.rdlc");
				//loReporte.RutaReportes = path;
				//using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				//{

				//	loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
				//	loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_reinscripcion_genera_horarios", loParametros));
				//}
				//lbysDocumento = loReporte.RegresaListaReinscripcion(loDt);
				//loArchivos.ArchivaDocumento(Sapei.Framework.Configuracion.enmSistema.SAPEI, lbysDocumento, penmArchivo);
				//ViewData["mensaje"] = "Documento generado correctamente";
				//return PartialView("../Generales/VisorPDF");
				return null;
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return null;
			}
		}
		#endregion
	}
}