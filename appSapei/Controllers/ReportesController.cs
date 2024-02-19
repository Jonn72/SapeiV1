using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using Sapei.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
namespace appSapei.Controllers
{
	public class ReportesController : Controller
	{
		#region Aspirantes
		public ActionResult FichaAspirante(string id)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Aspirantes"), "Ficha.rdlc");
				loReporte.RutaReportes = path;
				loReporte.RutaReporteVacio = Path.Combine(Server.MapPath("~/Reportes/RDLC/Generales"), "Vacio.rdlc");

				return File(loReporte.RegresaFichaAspirante(), "application/octet-stream", "Ficha.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}
		#endregion
		#region Ingles
		public ActionResult ListaGruposIngles(string id)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/CLE"), "ListaGrupos.rdlc");
				loReporte.RutaReportes = path;
				return File(loReporte.RegresaListaGruposIngles(), "application/octet-stream", "ListaGrupos.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}
		public ActionResult ListaReinscripcionIngles(string psPeriodo)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/CLE"), "ListaReinscripcion.rdlc");
				loReporte.RutaReportes = path;
				return File(loReporte.RegresaListaReinscripcionIngles(psPeriodo), "application/octet-stream", "ListaReinscripcion.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}
		public PartialViewResult HorarioIngles(string psNoControl = null)
		{
			try
			{
				ReportesGenerales loReporte;
				string lsControl = psNoControl;
				if (string.IsNullOrEmpty(psNoControl))
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/CLE"), "Horarios.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.HorariosIngles(lsControl));
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
        #endregion
        #region Alumnos

        public PartialViewResult BoletaCalificacionesPDF(string psPeriodo, string psNoControl)
        {
            try
            {
                ReportesGenerales loReporte;
                loReporte = new ReportesGenerales(SesionSapei.Sistema);
                string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales/"), "BoletaCalificaciones.rdlc");
                loReporte.RutaReportes = path;
                ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaDatosBoletaCalificacion(psPeriodo, psNoControl));
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

        #endregion 
        #region Materiales
        public ActionResult FichaPagoVehicular(string psTipo)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Pagos"), "RegistroVehicular.rdlc");
				loReporte.RutaReportes = path;
				return File(loReporte.RegresaFichaPagoVehicular(psTipo), "application/octet-stream", "Ficha.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}
		#endregion
		#region EvaluacionDocente

		public PartialViewResult EvaluacionPDF(string psPeriodo)
		{

			if (psPeriodo == null || psPeriodo == "")
			{

				psPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;

			}

			string psNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;

			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Generales"), "EvaluacionDocente.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaEvaluacion(psPeriodo, psNoControl));
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

		public PartialViewResult ResultadosEvlDeptoPDF(string psPeriodo, string psDepto)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				DataTable ldtDatos = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DesarrolloAcademico/"), "REvlDepto.rdlc");
				loReporte.RutaReportes = path;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@clave_area", psDepto));
					ldtDatos.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_evl_depto", loParametros));
				}
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaEvlDepto(ldtDatos));
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

		#endregion
		#region CapturaCalificacion
		public PartialViewResult ActaPDF(string psPeriodo, string psMateria, string psGrupo)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales/"), "Actas.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaGrupoCalificacionActas(psPeriodo, psMateria, psGrupo));
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
		public PartialViewResult ReporteFinal(string psPeriodo, string psUsuario)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DesarrolloAcademico/"), "TecNM-AC-PO-003-03-.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaDatosReporteFinal(psPeriodo, psUsuario));
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
		#endregion
		#region Financieros
		public PartialViewResult FichaPagoReinscripcion(string psNoControl = null)
		{
			try
			{
				ReportesGenerales loReporte;
				string lsControl = psNoControl;
				if (string.IsNullOrEmpty(psNoControl))
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Pagos"), "FichaReinscripcion.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaFichaPagoReinscripcion(lsControl));
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
		public PartialViewResult FichaPagoServicios(string psServicio)
		{
			try
			{
				ReportesGenerales loReporte;
				string lsControl;
				lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Pagos"), "FichaServicios.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaFichaPagoServicio(lsControl, psServicio));
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
		#endregion
		#region Reinscripcion
		public PartialViewResult RegresaListasReinscripcion(string psCarrera)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Reinscripcion/"), "ListaReinscripcion.rdlc");
				loReporte.RutaReportes = path;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
					loParametros.Add(new ParametrosSQL("@carrera", psCarrera));
					loParametros.Add(new ParametrosSQL("@genera", 0));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_reinscripcion_genera_horarios", loParametros));
				}
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaListaReinscripcion(psCarrera, loDt));
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
		#endregion
		#region Recontratacion

		public PartialViewResult HorarioRecontratacionPDF(int psID)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales/"), "HorarioLaboral.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaDatosHorarioRecontratacion(psID));
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

		public PartialViewResult HorarioRecontratacionFacilitadorPDF(int psID)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales/"), "HorarioLaboralFacilitador.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaDatosHorarioFacilitador(psID));
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

		public PartialViewResult ContratoFacilitadoresCLE(int psID)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales/"), "ContratoFacilitadorCLE.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaDatosContratoFacilitadorCLE(psID));
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

		public PartialViewResult ContratoDocenteAsimilados(string psPeriodo, string psRFC)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales/"), "ContratoDocenteAsimilados.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaDatosContratoAsimilados(psPeriodo, psRFC));
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

		public PartialViewResult ContratoDocenteHonorarios(int psID)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales/"), "ContratoDocenteHonorarios.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaDatosContratoHonorarios(psID));
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
		public PartialViewResult ContratoGeneralFE(int psID)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales/"), "ContratoGeneral.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaDatosContratoGeneralFE(psID));
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
		#endregion
		#region Facilitadores
		public PartialViewResult ActaFacilitadoresPDF(string psPeriodo, string psNivel, string psGrupo, string psFacilitador)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/CLE/"), "ActasIngles.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaGrupoCalificacionActasFacilitador(psPeriodo, psNivel, psGrupo, psFacilitador));
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
		#endregion

	}
}
