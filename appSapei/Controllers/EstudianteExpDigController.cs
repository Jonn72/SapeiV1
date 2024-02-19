using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using Sapei.Framework.Utilerias.ExpedienteDigital;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
	public class EstudianteExpDigController : Controller
	{

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult DocumentosInscripcion(string psControl = null)
		{
			try
			{
				DataTable loDt = new DataTable();
				string lsControl;
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				else
					lsControl = psControl;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_exp_dig_doc_alumnos", loParametros));
					ViewData["Datos"] = loDt;


				}
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex, "EstudianteExpDig/DocumentosInscripcion");
				return PartialView("Home", "Index");
			}

		}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public ActionResult GuardaDocumento()
		{

			HttpPostedFileBase loFile;
			DataTable loRespuesta = new DataTable();
			ManejoArchivos loArchivo;
			string lsClave_doc;
			string lsTipoDoc;
			enmTipoArchivo leExtension;
			//Se obtiene la fecha del ultimo dia cargado a base
			try
			{
				loFile = Request.Files[0];
				if (Request.Form.Keys.Count >= 1)
				{
					lsClave_doc = Request.Form.Get("clave_doc");
				}
				else
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No fue posible guardar este documento, Error #FGCD", false);

				if (loFile.ContentLength <= 0)
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No fue posible guardar este documento, Error #CLCD", false);

				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loParametros.Add(new ParametrosSQL("@clave_doc", lsClave_doc));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loParametros.Add(new ParametrosSQL("@estado", 1));
					loRespuesta.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_exp_dig_documentos_alumno", loParametros));
				}

				if (loRespuesta.Columns.Count <= 0)
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No fue posible guardar este documento, Error #RCCD", false);

				lsTipoDoc =  loRespuesta.Rows[0].RegresaValor<string>("nombre_doc").Trim();
				leExtension = (enmTipoArchivo)Enum.Parse(typeof(enmTipoArchivo), loRespuesta.Rows[0].RegresaValor<string>("extension"));

				loArchivo = new ManejoArchivos();
				loArchivo.ArchivaDocumento(Sapei.Framework.Configuracion.enmSistema.SAPEI,
											loFile,
											lsTipoDoc,
											leExtension,
											loRespuesta.Rows[0].RegresaValor<string>("ruta"),
											new System.Net.NetworkCredential(
												loRespuesta.Rows[0].RegresaValor<string>("usuario").Trim(),
												Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.DecryptRJ256(loRespuesta.Rows[0].RegresaValor<string>("contraseña").Trim())
											),
											loRespuesta.Rows[0].RegresaValor<string>("servidor")
										);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult FirmaDocumento(string psFirma, enmTiposDocumentos penmDoc)
		{
			try
			{
				string nomDoc = "";
				Alumno loAlumno = new Alumno(SesionSapei.Sistema);
				if (psFirma.Trim() != SesionSapei.Sistema.Sesion.Usuario.Contraseña)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se reconoce la contraseña", false);
				}

				nomDoc = penmDoc.ToString();
				loAlumno.RegistraCadenaFiElExpDig(nomDoc);

				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Documento firmado, ya puedes descargar tu documento", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult RegresaDocumentosEscolares(string psControl, enmTiposDocumentos penmDoc)
		{
			try
			{
				ReportesGenerales loReporte;
				string lsNoControl;
				string lsNombreDoc = "";
				byte[] lbyReporte;
				lsNombreDoc = penmDoc.ToString();
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ExpedienteDigital"), lsNombreDoc + ".rdlc");
				loReporte.RutaReportes = path;
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
				{
					lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				}
				else {
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

		public PartialViewResult SolicitudInscripcion()
		{
			return PartialView();
		}

		public PartialViewResult CartaCompromiso()
		{
			return PartialView();
		}

		public JsonResult  FormatoAutorizacionExp(string psNoControl, string psParentesco, string psNomPersona)
		 {
			try
			{
				DataTable loDt = new DataTable();
				string lsControl;
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				else
					lsControl = psNoControl;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				loParametros.Add(new ParametrosSQL("@parentesco", psParentesco));
				loParametros.Add(new ParametrosSQL("@nombre", psNomPersona));
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pai_exp_dig_alu_autorizacion_de_consulta", loParametros));
					ViewData["Datos"] = loDt;
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Datos guardos", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}

		public JsonResult ActualizacionDocumentos(string psNoControl, int psClaveDoc)
		{
			try
			{
				DataTable loDt = new DataTable();
				string lsControl;
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				else
					lsControl = psNoControl;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loParametros.Add(new ParametrosSQL("@clave_doc", psClaveDoc));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_exp_dig_actualizacion_doc_alu", loParametros));
					ViewData["Datos"] = loDt;
				}

				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Cambios realizados correctamente", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
				throw;
			}

		}

		public PartialViewResult FormatoAvisoPrivacidad()
		{
			return PartialView("FormatoAvisoPrivacidad");
		}

		public PartialViewResult NoPertenenciaTecnologico()
		{
			return PartialView("NoPertenenciaTecnologico");
		} 

		public PartialViewResult CartaResponsiva()
		{
			return PartialView("CartaResponsiva");
		}
	}
}