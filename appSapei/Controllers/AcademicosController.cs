using appSapei.App_Start;
using appSapei.Clases;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using Syncfusion.EJ2.Schedule;
using System.Web.Mvc;
using Sapei.Framework;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace appSapei.Controllers
{
	public class AcademicosController : Controller
	{
		#region Liberacion de Creditos
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
		public PartialViewResult Liberacion()
		{
			try
			{
				ViewData["DescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.ECO)]
		public PartialViewResult ConsultaLiberaciones()
		{
			try
			{
				Creditos_Complementarios loCreditos = new Creditos_Complementarios(SesionSapei.Sistema);
				string lsTipoCredito;
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.DAC)
				{
					lsTipoCredito = "ACA";
					ViewData["Tabla"] = loCreditos.RegresaTablaEstudiantesActividadesComplementarias(lsTipoCredito, SesionSapei.Sistema.Sesion.Usuario.PermisosCarreras.RegresaCadenaDeLista<string>());
				}
				else
				{
					lsTipoCredito = "ECO";
					ViewData["Tabla"] = loCreditos.RegresaTablaEstudiantesActividadesComplementarias(lsTipoCredito, null);
				}

				ViewData["Titulo"] = "Liberaciones Registradas";
				ViewData["Encabezados"] = new List<string> { "Periodo Liberó", "No. Control", "Nombre", "Fecha Liberación", "Promedio" };

				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		#endregion
		#region Residencias Profesionales
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult ControlEscolarRP()
		{
			try
			{
				ViewData["txtPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				ViewData["txtDescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Carga los números de control para la autorización de anteproyecto impreso
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult AutorizacionAnteproyectosRP()
		{
			try
			{
				RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loDt = loEstado.AutorizacionAnteproyecto(usuario);
				ViewData["Tabla"] = loDt;
				ViewData["periodo"] = lsPeriodo;
				ViewData["Titulo"] = "Autorización de Anteproyectos";
				ViewData["Encabezados"] = new List<string> { " ", "N° de control", "Nombre de Residente" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Función para autorizar anteproyecto, cambia de estado 3 --> 4
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public JsonResult AutorizaAnteproyectoJsonRP(string psNoControl)
		{
			try
			{
				RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
				loEstado.Cargar(psNoControl);
				if (loEstado.estado == 3)
				{
					loEstado.estado = 4;
					loEstado.Guardar();
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Carga los datos del anteproyecto para su autorización y comentarios en caso de corrección
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult SolicitudesAnteproyectosRP()
		{
			try
			{
				RP_Programa loDatos = new RP_Programa(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loDt = loDatos.AutorizacionAnteproyectoRP(usuario);
				ViewData["Tabla"] = loDt;
				ViewData["periodo"] = lsPeriodo;
				ViewData["Titulo"] = "Validación de Anteproyectos";
				ViewData["Encabezados"] = new List<string> { " ", "Id", "N° de proyecto", "Nombre de Proyecto", "Duración del proyecto" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Consulta los datos del anteproyecto
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public JsonResult RegresaDatosAnteproyectoRP(int piId)
		{
			try
			{
				RP_Datos_Programa loDatos = new RP_Datos_Programa(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loDatos.ConsultaRegistroRP(piId));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		//Valida los datos del anteproyecto, cambiando el estado de los integrantes
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public JsonResult ValidaAnteproyectoJsonRP(int piProyecto)
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				int estado = 8;
				loDt = loSolicitud.ValidaRP(piProyecto, lsPeriodo, estado);
				if (loDt.Columns.Count == 1)
				{
					string mensaje = loDt.RegresaValorFila<string>("mensaje");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(mensaje, false);
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Envia comentarios para la corrección del anteproyecto,
		//implica que ya no aparezca en SolicitudesAnteproyectosRP por el cambio de estado de los integrantes
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public JsonResult CorreccionAnteproyectoJsonRP(int piProyecto, string psComentarios)
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				DataTable loDt;
				loDt = loSolicitud.EviaComentario(piProyecto, psComentarios, lsPeriodo);
				if (loDt.Columns.Count == 1)
				{
					string mensaje = loDt.RegresaValorFila<string>("mensaje");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(mensaje, false);
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Carga los proyectos de acuerdo a la carrera para la selección de asesor interno
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult AsignacionAsesorInternoRP()
		{
			try
			{
				RP_Datos_Programa loDatos = new RP_Datos_Programa(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loDt = loDatos.RfcPersonalRP();
				ViewData["cboRfc"] = loDt;
				loDt = loDatos.AsignacionDocentesRP(usuario);
				ViewData["Tabla"] = loDt;
				ViewData["periodo"] = lsPeriodo;
				ViewData["Titulo"] = "Asignación de Asesor Interno";
				ViewData["Encabezados"] = new List<string> { "Asignación", "Id", "N° de proyecto", "Nombre de Proyecto" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Función para la asignación de asesor interno, recibe el rfc del docente y el ID del proyecto al cual va ser asignado
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public JsonResult AsignaInternoJsonRP(string psRfc, int piProyecto)
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
				RP_Datos_Programa loDatos = new RP_Datos_Programa(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				int estado = 9;
				loDatos.AsignaInternoRP(piProyecto, psRfc);
				loSolicitud.ValidaRP(piProyecto, lsPeriodo, estado);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Carga los proyectos de acuerdo a la carrera para la asignación de revisor del proyecto
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult AsignacionRevisorRP()
		{
			try
			{
				RP_Datos_Programa loDatos = new RP_Datos_Programa(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loDt = loDatos.RfcPersonalRP();
				ViewData["cboRfc"] = loDt;
				loDt = loDatos.AsignacionRevisorRP(usuario);
				ViewData["Tabla"] = loDt;
				ViewData["periodo"] = lsPeriodo;
				ViewData["Titulo"] = "Asignación de Revisor";
				ViewData["Encabezados"] = new List<string> { "Asignación", "Id", "N° de proyecto", "Nombre de Proyecto" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Función para la asignación de revisor del proyecto, recibe el rfc del docente y el ID del proyecto al cual va ser asignado
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public JsonResult AsignaRevisorJsonRP(string psRfc, int piProyecto)
		{
			try
			{
				RP_Datos_Programa loDatos = new RP_Datos_Programa(SesionSapei.Sistema);
				loDatos.AsignaRevisorRP(piProyecto, psRfc);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Consulta el nombre del revisor del proyecto
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public JsonResult ConsultaRevisorRP(int piId)
		{
			try
			{
				RP_Datos_Programa loDatos = new RP_Datos_Programa(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loDatos.Consultarevisor(piId));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		//Carga las asesorías de los residentes por carrera para su validación
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult AsesoriasRP()
		{
			try
			{
				RP_Asesoria loAsesoria = new RP_Asesoria(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loDt = loAsesoria.ResidenteAsesoriaRP(usuario);
				ViewData["Tabla"] = loDt;
				ViewData["periodo"] = lsPeriodo;
				ViewData["Titulo"] = "Validación de Asesorias";
				ViewData["Encabezados"] = new List<string> { " ", "N° de Control", "N° de Asesoría", "Residente" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Valida la asesoría del residente, recibe el número de control y número de asesoría
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public JsonResult ValidaAsesoriaJsonRP(string psNoControl, int psNoAsesoria)
		{
			try
			{
				RP_Asesoria loAsesoria = new RP_Asesoria(SesionSapei.Sistema);
				Alumno loAlumno = new Alumno(SesionSapei.Sistema);
				int piEstado = 2;
				loAlumno.Cargar(psNoControl);
				string lsNombreCarrera = Sapei.Framework.Utilerias.ManejoCarreras.RegresaNombreCompeto(loAlumno.carrera.ToEnum<enmCarreras>());
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.DAC && !SesionSapei.Sistema.TienePermisoCarrera(loAlumno.carrera))
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Usted no tiene permiso sobre estudiantes de la carrera " + lsNombreCarrera, false);
				}
				loAsesoria.ValidaAsesoriaRP(psNoControl, psNoAsesoria, piEstado);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Carga los residentes para la captura del seguimiento de acuerdo a la carrera
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult SeguimientosRP()
		{
			try
			{
				RP_Seguimiento loDatos = new RP_Seguimiento(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loDt = loDatos.Seguimientos(usuario);
				ViewData["Tabla"] = loDt;
				ViewData["periodo"] = lsPeriodo;
				ViewData["Titulo"] = "Validación de Seguimientos";
				ViewData["Encabezados"] = new List<string> { "'", "Residente", "N° de Control" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Captura la calificación del seguimiento, recibe la calificación del asesor interno y externo. número de control, fecha de la captura de seguimientop y número de seguimiento.
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public JsonResult AltaCalificacionJsonRP(int piCalExterno, int piCalInterno, string psNoControl, string psFecha, int piSeguimiento)
		{
			try
			{
				RP_Seguimiento loSeguimiento = new RP_Seguimiento(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loDt = loSeguimiento.AltaCalificacionRP(lsPeriodo, piCalExterno, piCalInterno, psNoControl, psFecha, piSeguimiento);
				if (loDt.Columns.Count == 1)
				{
					string mensaje = loDt.RegresaValorFila<string>("mensaje");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(mensaje, false);
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Consulta la calificación caprurada 
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public JsonResult RegresaDatosCalificacionRP(int psSeguimiento, int psNoControl)
		{
			try
			{
				RP_Seguimiento loDatos = new RP_Seguimiento(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loDatos.RegresaDatosRP(psSeguimiento, psNoControl));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		//Carga los número de control que ya se les fue validada la carta de termino
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult CartaTerminoRP()
		{
			try
			{
				RP_Seguimiento loSeguimiento = new RP_Seguimiento(SesionSapei.Sistema);
				string usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				DataTable loDt;
				loDt = loSeguimiento.ValidacionCartaTermino(usuario);
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Carta Termino";
				ViewData["Encabezados"] = new List<string> { "Validado", "Número de control", "Residente" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Carga el estado de la residencia en que se encuentren
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult ConsultaEstadoRP()
		{
			try
			{
				RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				DataTable loDt;
				loDt = loEstado.ConsultaEstadoRP(usuario, lsPeriodo);
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Estado de Residentes";
				ViewData["Encabezados"] = new List<string> { "N° de Control", "Nombre Residente", "Estado" };
				ViewData["periodo"] = lsPeriodo;
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		#endregion

		#region AsignacionGrupo

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult AsignacionDocenteGrupo()
		{
			try
			{
				string lsUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@usuario", lsUsuario));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_aca_carrera_depto", loParametros));
					ViewData["datos"] = loDt;
				}
				ViewData["cboPeriodos"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual.RegresaComboPeriodos(lsPeriodo, 1, false, true);
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}


		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult ListaMateriasAsignacion(string psCarreraReticula, string psPeriodo)
		{
			try
			{
				string[] subs = psCarreraReticula.Split('_');
				string lsCarrera = subs[0];
				string lsReticula = subs[1];
				string lsUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@carrera", lsCarrera));
					loParametros.Add(new ParametrosSQL("@reticula", lsReticula));
					loParametros.Add(new ParametrosSQL("@usuario", lsUsuario));
					ViewData["Titulo"] = "Lista de Materias";
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_aca_lista_materias", loParametros));
					ViewData["Tabla"] = loDt;
				}
				ViewData["Acciones"] = "DesactivaBotones(); ocultar_buscar(); orden(2, 'asc');";
				return PartialView("../Generales/TablaGeneral");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult ListaGruposAsignacion(string psPeriodo, string psCarrera, int psReticula, string psMateria)
		{
			try
			{
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@carrera", psCarrera));
					loParametros.Add(new ParametrosSQL("@reticula", psReticula));
					loParametros.Add(new ParametrosSQL("@materia", psMateria));
					ViewData["Titulo"] = "Lista de Grupos";
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_aca_horario_grupos_materia", loParametros));
					ViewData["Tabla"] = loDt;
				}
				ViewData["Acciones"] = "DesactivaBotones(); ocultar_buscar(); MostrarTodos(); QuitarMostrar();";
				return PartialView("../Generales/TablaGeneral");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

        public JsonResult EliminaAsignacionGrupoJSON(string psPeriodo, string psMateria, string psGrupo)
        {
            try
            {
                string lsMensaje;
                System.Data.SqlClient.SqlDataReader loReader;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable loDt = new DataTable();
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loParametros.Add(new ParametrosSQL("@materia", psMateria));
                    loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_aca_elimina_grupo_docente", loParametros);
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
                    if (lsMensaje.Trim() == "correcto")
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
                    }

                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }

        public class ButtonModel
		{
			public string content { get; set; }
			public string cssClass { get; set; }
			public bool isPrimary { get; set; }
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
		public PartialViewResult AsignaGrupo(string psCarrera, int psReticula, string psPeriodo, string psMateria, string psGrupo)
		{
			try
			{
                ViewBag.DialogButtons1 = new ButtonModel { content = "Aceptar", isPrimary = true };
                string lsUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                DataTable loDt = new DataTable();
                DataTable loDt2 = new DataTable();
                DataTable loDt3 = new DataTable();
                DataTable psDatosGrupo = new DataTable();
				string lsEspecialidad = "NA";
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
                    loParametros.Add(new ParametrosSQL("@usuario", lsUsuario));
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_aca_lista_docentes", loParametros));
                    loParametros.Clear();
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@materia", psMateria));
					loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
					loDt2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_datos_grupo_horario", loParametros));
                    loParametros.Add(new ParametrosSQL("@carrera", psCarrera));
                    loParametros.Add(new ParametrosSQL("@reticula", psReticula));
                    loParametros.Add(new ParametrosSQL("@especialidad", lsEspecialidad));
                    loDt3.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_datos_aula_grupo", loParametros));
                    AppointmentData loHorarioGrupoMateria;
					loHorarioGrupoMateria = new AppointmentData();
					ViewData["Docentes"] = loDt;
                    ViewBag.datasource = loHorarioGrupoMateria.RegresaHorarioGrupoMateria(loDt2);
                    ViewData["DatosSeleccion"] = loDt3;
                }
				return PartialView("AsignaGrupo");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

        public JsonResult RegresaHorarioDocente(string psRFC, string psPeriodo)
        {
            try
            {

                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable loDt = new DataTable();

                    loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_horario_completo", loParametros));
                    if (loDt.Rows.Count <= 0)
                    {

                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda("ERROR", false);

                    }
                    else
                    {
                        var retorno = loDt.Rows[0].RegresaValor<string>("JSON_F52E2B61-18A1-11d1-B105-00805F49916B");

                        return ManejoMensajesJson.RegresaJsonTabla(retorno);

                    }

                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }

		public JsonResult GuardaAsignacionGrupoJSON(string psPeriodo, string psRFC, string psMateria, string psGrupo)
		{
            try
            {
                string lsMensaje;
                System.Data.SqlClient.SqlDataReader loReader;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable loDt = new DataTable();
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                    loParametros.Add(new ParametrosSQL("@materia", psMateria));
                    loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_aca_asigna_grupo_docente", loParametros);
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
                    if (lsMensaje.Trim() == "correcto")
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
                    }

                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }

        #endregion

        #region PrestamoDocentes

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DAC)]
        public PartialViewResult PrestamoDocentes()
        {
            try
            {
                string lsUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                ViewData["Personal"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("personal", "rfc", "rfc+ ' - ' +apellido_paterno+ ' ' +apellido_materno+ ' ' +nombre_empleado", "status_empleado = '02' and area_academica = (select clave_area from jefes where usuario = '" + lsUsuario + "')", "apellido_paterno", false, 0, null, true);
                DataTable loDt = new DataTable();
                DataTable loDt2 = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_aca_deptos_academicos"));
                    loParametros.Add(new ParametrosSQL("@usuario", lsUsuario));
                    loDt2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_aca_docentes_prestados", loParametros));
                    ViewData["datos"] = loDt;
                    ViewData["Tabla"] = loDt2;
                    ViewData["Titulo"] = "Docentes Prestados";
                }
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        public JsonResult RegistraPrestamo(string psDocente, string psDepartamento)
        {
            try
            {
                string lsMensaje;
                System.Data.SqlClient.SqlDataReader loReader;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable loDt = new DataTable();
                    loParametros.Add(new ParametrosSQL("@rfc", psDocente));
                    loParametros.Add(new ParametrosSQL("@depto", psDepartamento));
                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_aca_registra_prestamo_docente", loParametros);
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
                    if (lsMensaje.Trim() == "correcto")
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
                    }

                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }

        public JsonResult EliminaPrestamo(string psDocente, string psDepartamento)
        {
            try
            {
                string lsMensaje;
                System.Data.SqlClient.SqlDataReader loReader;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable loDt = new DataTable();
                    loParametros.Add(new ParametrosSQL("@rfc", psDocente));
                    loParametros.Add(new ParametrosSQL("@depto", psDepartamento));
                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_aca_elimina_prestamo_docente", loParametros);
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
                    if (lsMensaje.Trim() == "correcto")
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
                    }

                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }

        #endregion
    }
}
