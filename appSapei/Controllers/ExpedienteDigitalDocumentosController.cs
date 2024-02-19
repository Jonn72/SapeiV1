using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using Sapei.Framework.Utilerias.ExpedienteDigital;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class ExpedienteDigitalDocumentosController : Controller
    {
		#region Alumnos
		public PartialViewResult RegresaDocumentoAlumno(int piClaveDoc, string psNoControl = null)
		{
			try
			{
				ManejoArchivos loArchivos;
				string lsControl = psNoControl;
				if (string.IsNullOrEmpty(psNoControl))
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loArchivos = new ManejoArchivos(SesionSapei.Sistema);
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loArchivos.RegresaDocumentoAlumno(lsControl, piClaveDoc));
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
		public JsonResult GuardaValidacionDocumentoAlumno(int piClaveDoc, string psControl, bool pbValida, string psObservaciones)
		{
			try
			{
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loParametros.Add(new ParametrosSQL("@clave_doc", piClaveDoc));
				loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				loParametros.Add(new ParametrosSQL("@estado", 0));
				loParametros.Add(new ParametrosSQL("@autorizado", pbValida));
				loParametros.Add(new ParametrosSQL("@observaciones", psObservaciones));

				using (var conexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					SesionSapei.Sistema.Conexion.EjecutaEscalarProcedimientoAlmacenado("pam_exp_dig_documentos_alumno", loParametros);
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Registro exitoso", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}
		#endregion
	}
}