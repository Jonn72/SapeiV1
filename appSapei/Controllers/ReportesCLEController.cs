using appSapei.App_Start;
using Sapei;
using Sapei.Framework.Utilerias;
using System;
using System.IO;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class ReportesCLEController : Controller
    {
        #region Constancias
        public ActionResult GeneraConstancia(string psNoControl, enmCleDocumentos penmTipo)
        {
            try
            {
                ReportesGenerales loReporte;
                loReporte = new ReportesGenerales(SesionSapei.Sistema);
                string lsNombre = "";
                string path; 

                switch(penmTipo)
				{
                    case enmCleDocumentos.AVA:
                    case enmCleDocumentos.SIM:
                        lsNombre = "General";
                        break;
                    case enmCleDocumentos.HIS:
                        lsNombre = "Historia";
                        break;
                    case enmCleDocumentos.LIB:
                        lsNombre = "constancias";
                        break;
                }

                path = Path.Combine(Server.MapPath("~/Reportes/RDLC/CLE"), lsNombre + ".rdlc");
                loReporte.RutaReportes = path;
                ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.GeneraConstancias(psNoControl, penmTipo));
                ViewData["mensaje"] = "Documento generado correctamente";
                return PartialView("../Generales/VisorPDF");
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                ViewData["mensaje"] = "Error al descargar reporte";
                return View("../Generales/AvisosGenerales");
            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
        public PartialViewResult Documentos()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("../Generales/AvisosGenerales");
            }
        }

        #endregion

    }
}