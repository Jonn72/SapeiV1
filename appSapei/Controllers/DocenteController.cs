using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace appSapei.Controllers
{
	public class DocenteController : Controller
	{
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DOC)]
		[HttpGet]
		public ActionResult Index()
		{
			try
			{
				return View();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return RedirectToAction("Index", "Home");
			}
		}

        #region Horario
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DOC)]
		public PartialViewResult HorarioActividades()
		{
			try
			{
				string lsPeriodo;
				string lsPeriodoDescripcion;
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();
                
				ViewData["txtPeriodo"] = lsPeriodo;
				ViewData["txtDescPeriodo"] = lsPeriodoDescripcion;

				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DOC)]
		public JsonResult RegresaHorario()
		{
			try
			{
				Personal loHorario;
				loHorario = new Personal(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loHorario.RegresaHorario(SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
        #endregion

        #region CapturaCalificaciones
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DOC)]
		public PartialViewResult CapturaCalificaciones(string psMateria = null, string psGrupo = null, string psPeriodo = null, string psNMateria = null)
		{
			try
			{
				string lsPeriodo;
				string lsUsuario;
				lsUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				Personal loPersonal;
				loPersonal = new Personal(SesionSapei.Sistema);
				if (string.IsNullOrEmpty(psMateria) && string.IsNullOrEmpty(psGrupo))
				{
					if (string.IsNullOrEmpty(psPeriodo))
					{
						lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
						ViewData["botonperiodo"] = "<div class=\"x_title\"><h3>Revisar Actas del Semestre Anterior</h3><div class=\"clearfix\"></div></div>Al dar click en este boton podra revisar las actas del semestre anterior asi como el reporte final.<center><a class=\"btn btn-success\" href=\"/Docente/CapturaCalificaciones?psPeriodo=1\" data-ajax-update=\"#BodyPrincipal\" data-ajax-mode=\"replace\" data-ajax-method=\"GET\" data-ajax=\"true\">Periodo Anterior</a></center>";
						
					}
						
                    else
                    {
						lsPeriodo = Sapei.Framework.Utilerias.ManejoPeriodos.PeriodoAnterior(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual, true);
						
						ViewData["botonperiodo"] = "<div class=\"x_title\"><h3>Regresar al Periodo Actual</h3><div class=\"clearfix\"></div></div>Al dar click en este boton regresará al periodo actual.<center><a class=\"btn btn-success\" href=\"/Docente/CapturaCalificaciones\" data-ajax-update=\"#BodyPrincipal\" data-ajax-mode=\"replace\" data-ajax-method=\"GET\" data-ajax=\"true\">Periodo Actual</a></center>";

					}

					ViewData["periodo"] = lsPeriodo;
					ViewData["docente"] = lsUsuario;
					ViewData["periodo_desc"] = lsPeriodo.RegresaDescripcionPeriodo();
					ViewData["Tabla"] = loPersonal.RegresaGrupos(lsPeriodo, lsUsuario);
                    
					ViewData["Titulo"] = "Lista de Grupos";
					ViewData["Encabezados"] = new List<string> { "Nombre de la Materia", "Grupo", " " };

					ViewData["boton"] = loPersonal.RegresaBotonReporteFinal(lsPeriodo, lsUsuario);
					return PartialView("CapturaCalificaciones");
				}
				else
				{
					if (string.IsNullOrEmpty(psPeriodo))
					{
                        lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    }
					else 
					{
						lsPeriodo = psPeriodo;
                    }
                    ViewData["Tabla"] = loPersonal.RegresaAlumnos(lsPeriodo, psMateria, psGrupo);
					ViewData["Titulo"] = "Lista de Alumnos de la Materia " + psNMateria + " del Grupo " + psGrupo;
					ViewData["Encabezados"] = new List<string> { "Nombre del Alumno", "No. de Control", "Calificación", "Tipo de Evaluación", "No Presentó", "Verificación", "Repite" };
					ViewData["Objeto"] = loPersonal.RegresaObjeto(lsPeriodo, psMateria, psGrupo, lsUsuario);
					return PartialView("Calificaciones");
				}
				
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DOC)]
		public JsonResult RegistraCalificacionJSON(string lsobjeto)
		{
			try
			{
				string lsMensaje;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loDt = new DataTable();
				System.Data.SqlClient.SqlDataReader loReader;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@objeto", lsobjeto));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_captura_calificacion", loParametros);
					lsMensaje = "";
					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
					if (lsMensaje.Trim() != "1")
					{
						ViewData["Mensaje"] = lsMensaje;
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
					}
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
        #endregion

        #region ListasExcel
        public FileContentResult EEX(string psPeriodo = null, string psMateria = null, string psGrupo = null)
		{
			try
			{
				Personal loPersonal;
				loPersonal = new Personal(SesionSapei.Sistema);
				DataTable loDt = loPersonal.RegresaAlumnosExcel(psPeriodo, psMateria, psGrupo);
				DataTable loDt2 = loPersonal.RegresaDatosExcel(psPeriodo, psMateria, psGrupo);
				string[] columns = { "Numero", "No. de control", "Nombre" };
				byte[] filecontent = ExportExcelController.ExportExcel(loDt, loDt2, psMateria, true, columns);
				return File(filecontent, ExportExcelController.ExcelContentType, psMateria + "-" + psGrupo + ".xlsx");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return null;
			}
		}
        #endregion
    }
}
