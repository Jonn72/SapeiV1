using System;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using Sapei.Framework;
using appSapei.App_Start;

namespace appSapei.Controllers
{
	public class MantenimientoEquiposController : Controller
	{
		#region Solicitud
		
		#endregion
		[SessionExpire]
		public PartialViewResult OrdenMantenimiento()
		{
			try
			{
				mantenimiento_orden loCarga;
				loCarga = new mantenimiento_orden(SesionSapei.Sistema);

				ViewData["Tabla"] = loCarga.RegresaTablaSolicitudes();
				ViewData["Titulo"] = "Ordenes de Mantenimiento";

				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

		[SessionExpire]
		public PartialViewResult RegresaOrdenPreview(string psValor, string psPeriodo, string psNombreReporte, string psTipo_Mantenimiento, string psFolio, string psTipo_Servicio,  string psTrabajoRealizado, string psAsignado, string psVerificado, string psFechaLiberacion)
		{

			psNombreReporte = "OrdenMantenimientoPreview";

			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ProcesoEstrategicoAdmonRecursos/MantenimientoEquipos"), psNombreReporte + ".rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaOrdenMantenimiento(psValor, psPeriodo, psTipo_Mantenimiento, psFolio, psTipo_Servicio, psTrabajoRealizado, psAsignado, psVerificado, psFechaLiberacion));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}
		[SessionExpire]
		public PartialViewResult RegresaOrden(string psValor, string psPeriodo, string psNombreReporte, string psTipo_Mantenimiento, string psFolio, string psTipo_Servicio, string psTrabajoRealizado, string psAsignado, string psVerificado, string psFechaLiberacion)
		{

			psNombreReporte = "OrdenMantenimiento";

			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);		
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ProcesoEstrategicoAdmonRecursos/MantenimientoEquipos"), psNombreReporte + ".rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaOrdenMantenimiento(psValor, psPeriodo, psTipo_Mantenimiento, psFolio, psTipo_Servicio, psTrabajoRealizado, psAsignado, psVerificado, psFechaLiberacion));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}



		[SessionExpire]
		public JsonResult Contador()
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					DataTable loDt = new DataTable();
					List<ParametrosSQL> loParametrosDoc = new List<ParametrosSQL>();
					string lsMensaje = "";

					loParametrosDoc.Add(new ParametrosSQL("@periodo", ""));
					loParametrosDoc.Add(new ParametrosSQL("@tipo_solicitud", ""));
					loParametrosDoc.Add(new ParametrosSQL("@folio", ""));
					loParametrosDoc.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loParametrosDoc.Add(new ParametrosSQL("@descripcion", ""));
					loParametrosDoc.Add(new ParametrosSQL("@estatus", ""));
					loParametrosDoc.Add(new ParametrosSQL("@bandera", 7));

					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_mantenimiento_solicitud", loParametrosDoc));


					return ManejoMensajesJson.RegresaJsonTabla(loDt);

				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		[SessionExpire]
		public JsonResult FirmaOrdenMantenimientoFinalizado(string psFirma, string psFolio)
		{
			try
			{
				mantenimiento_orden loMs = new mantenimiento_orden(SesionSapei.Sistema);
				if (psFirma.Trim() != SesionSapei.Sistema.Sesion.Usuario.Contraseña)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se reconoce la contraseña", false);

				}
				loMs.RegistraCadenaFiElOrdenFinalizada(enmTiposDocumentos.SolicitudMantenimiento, psFolio);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Documentos firmados", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}
		public JsonResult FirmaSolicitudMantenimientoAceptado(string psFirma, string psFolio)
		{
			try
			{
				mantenimiento_orden loMs = new mantenimiento_orden(SesionSapei.Sistema);
				if (psFirma.Trim() != SesionSapei.Sistema.Sesion.Usuario.Contraseña)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se reconoce la contraseña", false);

				}
				loMs.RegistraCadenaFiElAceptada(enmTiposDocumentos.SolicitudMantenimiento, psFolio);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Documentos firmados", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}


		[SessionExpire]
		public PartialViewResult SolicitudMantenimiento()
		{
			try
			{
				mantenimiento_solicitud loCarga;
				loCarga = new mantenimiento_solicitud(SesionSapei.Sistema);


				ViewData["Tabla"] = loCarga.RegresaTablaSolicitudes();
				ViewData["Titulo"] = "Solicitudes de Mantenimiento";
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire]
		public PartialViewResult RegresaSolicitudPrevia(string psValor, string psPeriodo, string psTipo_solicitud,  string psDescripcion)
		{

			string lsNombreReporte = "SolicitudMantenimientoPreview";

			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ProcesoEstrategicoAdmonRecursos/MantenimientoEquipos"), lsNombreReporte + ".rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaSolicitudPrevia(psPeriodo, psTipo_solicitud,   psDescripcion));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		[SessionExpire]
		public PartialViewResult RegresaSolicitud(string psPeriodo, string psFolio)
		{

			string lsNombreReporte = "SolicitudMantenimiento";

			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ProcesoEstrategicoAdmonRecursos/MantenimientoEquipos"), lsNombreReporte + ".rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaSolicitudFirmada(psPeriodo, psFolio));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}
		[SessionExpire]
		public JsonResult AgregarMateriaJson(string tipo_solicitud, string descripcion, int estatus)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@periodo", ""));
					loParametros.Add(new ParametrosSQL("@tipo_solicitud", ""));
					loParametros.Add(new ParametrosSQL("@folio", ""));
					loParametros.Add(new ParametrosSQL("@area_solicitante",""));
					loParametros.Add(new ParametrosSQL("@fecha_solicitante", ""));
					loParametros.Add(new ParametrosSQL("@descripcion", ""));
					loParametros.Add(new ParametrosSQL("@estatus", ""));
					loParametros.Add(new ParametrosSQL("@bandera", "1"));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_materias_reticulas", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
				}

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[SessionExpire]
		public JsonResult FirmaSolicitudMantenimiento(string psFirma, string psTipo_solicitud, string psDescripcion)
		{
			try
			{
				mantenimiento_solicitud loMs = new mantenimiento_solicitud(SesionSapei.Sistema);
				if (psFirma.Trim() != SesionSapei.Sistema.Sesion.Usuario.Contraseña)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se reconoce la contraseña", false);

				}
				loMs.RegistraCadenaFiEl(enmTiposDocumentos.SolicitudMantenimiento, psTipo_solicitud, psDescripcion);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Documentos firmados", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}
		[SessionExpire]
		public JsonResult FirmaOrdenMantenimientoLiberada (string psFirma, string psFolio, string psAreaSolicitada, string psTipoMantenimiento, string psTipoServicio, string psAsignado, string psTrabajoRealizado)
		{
			try
			{
				mantenimiento_orden loMs = new mantenimiento_orden(SesionSapei.Sistema);
				if (psFirma.Trim() != SesionSapei.Sistema.Sesion.Usuario.Contraseña)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se reconoce la contraseña", false);

				}
				loMs.RegistraCadenaFiElOrdenLiberada(enmTiposDocumentos.SolicitudMantenimiento, psFolio, psAreaSolicitada, psTipoMantenimiento, psTipoServicio, psAsignado, psTrabajoRealizado);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Documentos firmados", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}
	}
}

