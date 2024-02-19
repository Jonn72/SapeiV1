using appSapei.Models;
using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using appSapei.Clases;

namespace appSapei.Controllers
{
	public class EstudianteController : Controller
	{
		[SessionExpire]
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
		#region Datos
		[SessionExpire]
		public PartialViewResult ActualizarDatos()
		{
			return PartialView("ActualizarDatos");
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
		public JsonResult ActualizaDatos(string psNoControl, string psNombre, string psPaterno, string psMaterno, string psFechaNacimiento, int piEntidadNacimiento, string psSexo, string psCURP, string psEstadoCivil, string psNSS, string psTelefono, string psCelular, string psCorreo, string psCalle, string psNumero, int piIdCP)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					Alumno loAlumno;
					Alumno_generales loGenerales;
					string lsNoControl;
					loAlumno = new Alumno(SesionSapei.Sistema);


					loGenerales = new Alumno_generales(SesionSapei.Sistema);

					if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ESC)
					{
						lsNoControl = psNoControl;
						loAlumno.Cargar(lsNoControl);
						loAlumno.nombre_alumno = psNombre;
						loAlumno.apellido_paterno = psPaterno;
						loAlumno.apellido_materno = psMaterno;
						loAlumno.sexo = (psSexo == "H" ? "M" : "F");
						loAlumno.curp_alumno = psCURP;
						loAlumno.fecha_nacimiento = Convert.ToDateTime(psFechaNacimiento);
					}
					else
					{
						lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
						loAlumno.Cargar(lsNoControl);
					}
					loAlumno.estado_civil = psEstadoCivil;
					loAlumno.correo_electronico = psCorreo;
					loAlumno.Guardar();

					loGenerales.Cargar(lsNoControl);
					if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ESC)
					{
						loGenerales.lugar_nacimiento = piEntidadNacimiento;
					}

					loGenerales.telefono = psTelefono;
					loGenerales.celular = psCelular;
					loGenerales.nss = psNSS;
					loGenerales.Guardar();

					ActualizaDomicilio(lsNoControl, psCalle, psNumero, piIdCP);

					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		[SessionExpire]
		public PartialViewResult ModificarDomicilio()
		{
			return PartialView("ModificarDomicilio");
		}
		[HttpGet]
		[SessionExpire]
		public PartialViewResult EstudianteGenerales()
		{
			try
			{
				return PartialView("EstudianteGenerales");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire]
		public PartialViewResult CargaDatosGenerales(string psNoControl)
		{
			try
			{
				return CargaDatosGenerales("../Estudiante/FichaDatosGenerales", psNoControl);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire]
		public PartialViewResult CargaTablaGenerales(string psValor, string psTipo)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					DataTable loDt = new DataTable();
					loParametros.Add(new ParametrosSQL("@busqueda", psValor.ToUpper()));
					loParametros.Add(new ParametrosSQL("@opcion", psTipo));

					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_busqueda", loParametros));
					ViewData["Titulo"] = "Estudiantes Encontrados";
					ViewData["Tabla"] = loDt;
					ViewData["Encabezados"] = new List<string> { "No. de Control", "Nombre", "Carrera" };
					ViewData["ActivaSeleccion"] = true;
					return PartialView("EstudianteGenerales");
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		#endregion
		#region Extraescolares
		[SessionExpire]
		public PartialViewResult Extraescolares(string psControl = null)
		{
			try
			{
				string lsMensaje;
				string lsControl;
				Extra_actividades_inscrito loInscrito;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loDt = new DataTable();
				System.Data.SqlClient.SqlDataReader loReader;
				ViewData["Titulo"] = "";
				ViewData["Encabezados"] = new List<string> { "Periodo", "Tipo", "Actividad", "Concluida", "Horario" };
				if (Sapei.Framework.Utilerias.SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.EXT)
				{
					lsControl = psControl;
					if (string.IsNullOrEmpty(lsControl))
						return PartialView("Extraescolares");
				}
				else
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.RolUsuario));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pav_registra_act_extraescolar", loParametros);

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
						return PartialView("../Generales/AvisosGenerales");
					}
					loInscrito = new Extra_actividades_inscrito(SesionSapei.Sistema);
					ViewData["Tabla"] = loInscrito.RegresaTablaActividades(lsControl);
					return PartialView("Extraescolares");
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire]
		public PartialViewResult ExtraescolaresRegistradas()
		{
			try
			{
				Extra_actividades_inscrito loInscrito;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loInscrito = new Extra_actividades_inscrito(SesionSapei.Sistema);
					ViewData["Titulo"] = "";
					ViewData["Tabla"] = loInscrito.RegresaTablaActividades(SesionSapei.Sistema.Sesion.Usuario.Usuario);
					ViewData["Encabezados"] = new List<string> { "Periodo", "Tipo", "Actividad", "Concluida", "Horario" };
					return PartialView("ExtraescolaresRegistradas");
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		#endregion
		#region Datos 
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult DatosEscuela()
		{
			try
			{
				Alumnos_Escuela_Procedencia loDatos;
				ViewData["tipo_usuario"] = Sapei.Framework.Utilerias.SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString();

				if (Sapei.Framework.Utilerias.SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ESC)
					return PartialView("DatosEscuela");

				loDatos = new Alumnos_Escuela_Procedencia(SesionSapei.Sistema);
				loDatos.Cargar(SesionSapei.Sistema.Sesion.Usuario.Usuario);
				if (loDatos.EOF)
				{
					return PartialView("DatosEscuela");
				}
				ViewData["idEscuela"] = loDatos.id_escuela;
				ViewData["egreso"] = loDatos.anio_egreso;
				ViewData["promedio"] = loDatos.promedio;
				return PartialView("DatosEscuela");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult ModificarDatosEscuela(string psControl, string psIdEscuela, string psEgreso, string psPromedio)
		{
			try
			{
				Alumnos_Escuela_Procedencia loDatos;
				string lsNoControl;
				lsNoControl = psControl;
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
					lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					loDatos = new Alumnos_Escuela_Procedencia(SesionSapei.Sistema);
					loDatos.Cargar(lsNoControl);
					if (loDatos.EOF)
					{
						loDatos.no_de_control = lsNoControl;
					}
					loDatos.anio_egreso = Convert.ToInt16(psEgreso);
					loDatos.promedio = Convert.ToDouble(psPromedio);
					loDatos.id_escuela = Convert.ToInt32(psIdEscuela);
					loDatos.Guardar();
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		#endregion
		#region Ingles
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult DatosCursosIngles(string psNoControl)
		{
			try
			{
				string lsControl;
				string lsPeriodoActivo;
				string lsPeriodo;
				string lsPeriodoDescripcion;
				lsControl = psNoControl;
				Cle_Niveles loNivel;
				Periodos_Ingles loPeriodos;


				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					DataTable loDt = new DataTable();
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_cle_datos_cursos", loParametros));
					if (loDt.Columns.Count <= 1)
					{
						@ViewData["mensaje"] = loDt.Rows[0].Field<string>(0);
						return PartialView("../CentroLenguasExt/RegistrarEstudianteGrupo");
					}

					if (loDt.Columns.Count == 4)
					{
						@ViewData["DatosIngles"] = loDt;
						return PartialView("../CentroLenguasExt/IdiomaLiberado");
					}

					loNivel = new Cle_Niveles(SesionSapei.Sistema);
					@ViewData["niveles"] = loNivel.RegresaTablaNiveles();
					@ViewData["no_de_control"] = lsControl;
					@ViewData["historia_cursos"] = loDt;

					//Validacion de periodo de seleccion de cursos
					loPeriodos = new Periodos_Ingles(SesionSapei.Sistema);
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
					lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();
					lsPeriodoActivo = loPeriodos.ValidaPeriodoSeleccion(lsPeriodo, lsPeriodoDescripcion);
					loDt = new DataTable();
					if (string.IsNullOrEmpty(lsPeriodoActivo))
					{
						loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
						loParametros.Add(new ParametrosSQL("@tipo_usuario", SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString()));
						loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_cle_fechas_seleccion", loParametros));
						ViewData["cursos_validos"] = loDt;
					}
					else
						@ViewData["mensaje"] = lsPeriodoActivo;
					return PartialView("../CentroLenguasExt/RegistrarEstudianteGrupo");
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Home", "Index");
			}

		}

		#endregion
		#region JSON
		public JsonResult GuardaNuevo()
		{
			try
			{
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		/// <summary>
		/// Valida si existe el no de control buscado
		/// </summary>
		/// <param name="psNoControl"></param>
		/// <returns></returns>
		[SessionExpire]
		public JsonResult ExisteNoControl(string psNoControl)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					Alumno loAlumno;
					loAlumno = new Alumno(SesionSapei.Sistema);
					if (loAlumno.ValidaExisteNoControl(psNoControl))
					{
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
					}
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="psNoControl"></param>
		/// <param name="psIdCP"></param>
		/// <returns></returns>
		public JsonResult RegresaEstudianteDatos(string psNoControl)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					Alumno loAlumno;
					loAlumno = new Alumno(SesionSapei.Sistema);
					loAlumno.Cargar(psNoControl);
					if (loAlumno.EOF)
					{
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
					}
					return ManejoMensajesJson.RegresaJsonObjeto(loAlumno);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[SessionExpire]
		public JsonResult RegresaEstudianteDatosCompletos(string psNoControl)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					Alumno loAlumno;
					DataTable loDatos;
					loAlumno = new Alumno(SesionSapei.Sistema);
					loDatos = loAlumno.CargarVistaDatosCompletos(psNoControl);
					if (loDatos.Rows.Count == 0)
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
					return ManejoMensajesJson.RegresaJsonTabla(loDatos);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="psNoControl"></param>
		/// <param name="psIdCP"></param>
		/// <returns></returns>
		[SessionExpire]
		public JsonResult ModificarDomicilioEstudiante(string psNoControl, int piIdCP, string psCalle, string psNumero)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					if (ActualizaDomicilio(psNoControl, psCalle, psNumero, piIdCP))
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[SessionExpire]
		public JsonResult RegistraActividad(string psTipo, int piActividad, string psControl = null)
		{
			try
			{
				string lsMensaje;
				string lsControl;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loDt = new DataTable();
				System.Data.SqlClient.SqlDataReader loReader;
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.EXT)
					lsControl = psControl;
				else
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loParametros.Add(new ParametrosSQL("@tipo", psTipo));
					loParametros.Add(new ParametrosSQL("@id_actividad", piActividad));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_registra_act_extraescolar", loParametros);

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
		[SessionExpire]

		private PartialViewResult CargaDatosGenerales(string psVista, string psNoControl)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					DataTable loDatos;
					Alumno loEstudiante = new Alumno(SesionSapei.Sistema);
					loDatos = loEstudiante.CargarVistaDatosCompletos(psNoControl);
					if (loDatos.Rows.Count == 0)
						return PartialView("DatosPersonales");
					ViewData["txtNoControl"] = psNoControl;
					ViewData["txtNombre"] = loDatos.Rows[0].RegresaValor<string>("nombre");
					ViewData["txtPaterno"] = loDatos.Rows[0].RegresaValor<string>("paterno");
					ViewData["txtMaterno"] = loDatos.Rows[0].RegresaValor<string>("materno");
					ViewData["txtFechaNacimiento"] = loDatos.Rows[0].RegresaValor<DateTime>("fecha_nacimiento").ToString("yyyy/MM/dd");
					ViewData["txtLugarNacimiento"] = loDatos.Rows[0].RegresaValor<string>("lugar_nacimiento");
					ViewData["txtSexo"] = loDatos.Rows[0].RegresaValor<string>("sexo");
					ViewData["txtCURP"] = loDatos.Rows[0].RegresaValor<string>("curp");
					ViewData["cboEstadoCivil"] = loDatos.Rows[0].RegresaValor<string>("estado_civil");
					ViewData["txtCorreo"] = loDatos.Rows[0].RegresaValor<string>("correo");
					ViewData["txtTelefono"] = loDatos.Rows[0].RegresaValor<string>("telefono");

					ViewData["txtCalle"] = loDatos.Rows[0].RegresaValor<string>("calle");
					ViewData["txtNoDomicilio"] = loDatos.Rows[0].RegresaValor<string>("numero");
					ViewData["cboColonia"] = loDatos.Rows[0].RegresaValor<int>("id_cp");
					ViewData["txtColonia"] = loDatos.Rows[0].RegresaValor<string>("colonia");
					ViewData["txtCiudad"] = loDatos.Rows[0].RegresaValor<string>("ciudad");
					ViewData["txtEntidad"] = loDatos.Rows[0].RegresaValor<string>("entidad");
					ViewData["txtCodPostal"] = loDatos.Rows[0].RegresaValor<string>("cp");

					ViewData["txtPeriodoIngreso"] = loDatos.Rows[0].RegresaValor<string>("periodo_ingreso");
					ViewData["txtTipoIngreso"] = loDatos.Rows[0].RegresaValor<string>("tipo_ingreso");
					ViewData["txtCarrera"] = loDatos.Rows[0].RegresaValor<string>("carrera");
					ViewData["txtNombreCarrera"] = loDatos.Rows[0].RegresaValor<string>("nombre_carrera");

					ViewData["txtReticula"] = loDatos.Rows[0].RegresaValor<string>("reticula");
					ViewData["txtEstatus"] = loDatos.Rows[0].RegresaValor<string>("estatus");
					ViewData["txtPlanEstudios"] = loDatos.Rows[0].RegresaValor<string>("plan_de_estudios");
					ViewData["txtNivelEscolar"] = loDatos.Rows[0].RegresaValor<string>("nivel_escolar");
					ViewData["txtEspecialidad"] = loDatos.Rows[0].RegresaValor<string>("especialidad");
					ViewData["txtDescripcionEspecialidad"] = loDatos.Rows[0].RegresaValor<string>("descripcion_especialidad");
					return PartialView(psVista);
				}
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}

		#endregion
		#region Generales
		private bool ActualizaDomicilio(string psNoControl, string psCalle, string psNumero, int piIdCP)
		{
			Alumnos_domicilio loEstudiante;
			loEstudiante = new Alumnos_domicilio(SesionSapei.Sistema);
			loEstudiante.Cargar(psNoControl);
			if (loEstudiante.EOF)
			{
				loEstudiante.no_de_control = psNoControl;
			}
			loEstudiante.id_cp = piIdCP;
			loEstudiante.calle = psCalle;
			loEstudiante.numero = psNumero;
			loEstudiante.Guardar();
			return true;
		}
		#endregion
		#region Horarios
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult HorarioActividades()
		{
			try
			{
				string lsPeriodo;
				string lsPeriodoDescripcion;
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
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
		public JsonResult RegresaHorario()
		{
			try
			{
				Alumno loHorario;
				loHorario = new Alumno(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loHorario.RegresaHorario(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		#endregion
		#region ENcuestas
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult Encuestas()
		{
			try
			{
				En_Periodos loPeriodo = new En_Periodos(SesionSapei.Sistema);
				if (!loPeriodo.EsProcesoActivo())
				{
					ViewData["Mensaje"] = "No es tiempo de encuestas. Gracias";
					return PartialView("../Generales/AvisosGenerales");
				}
				ViewData["encuestas"] = Sapei.Encuestas.RegresaDatosEncuestas(SesionSapei.Sistema);
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult EncuestasJson(string psRespuestas)
		{
			try
			{
				Sapei.Encuestas.GuardaEncuesta(psRespuestas, SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		#endregion
		#region Evaluacion Docente
		[SessionExpire]
		public PartialViewResult EvaluacionDocente()
		{
			try
			{
				string lsControl;
				string lsPeriodo;
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					DataTable loDt = new DataTable();
					loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_evaluacion_docente", loParametros));
					ViewData["Evaluacion"] = loDt;
					return PartialView("EvaluacionDocente");
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Home", "Index");
			}

		}

		public JsonResult RegistraEncuestaJSON(string lsobjeto)
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
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_da_registra_evaluacion_docente", loParametros);
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
		#region Actividades Complementarias
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult ActividadesComplementarias()
		{
			try
			{
				Alumno loAlumno = new Alumno(SesionSapei.Sistema);
				ViewData["datos"] = loAlumno.RegresaTablaEstudiantesActividadesComplementarias();
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult Tutorias(string psControl = null)
		{
			try
			{
				string lsMensaje;
				string lsControl;
				Tutorias_Inscritos loInscrito;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loDt = new DataTable();
				System.Data.SqlClient.SqlDataReader loReader;
				ViewData["Titulo"] = "";
				ViewData["Encabezados"] = new List<string> { "Periodo", "Tipo", "Actividad", "Concluida", "Horario" };
				if (Sapei.Framework.Utilerias.SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.DDA)
				{
					lsControl = psControl;
					if (string.IsNullOrEmpty(lsControl))
						return PartialView();
				}
				else
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.RolUsuario));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_tutorias_cursos_viables", loParametros);

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
						return PartialView("../Generales/AvisosGenerales");
					}
					loInscrito = new Tutorias_Inscritos(SesionSapei.Sistema);
					ViewData["Tabla"] = loInscrito.RegresaTablaEstudiantesIncritos(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual, lsControl);
					return PartialView();
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		#endregion
		#region Servicio Social
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult ProcesoSS()
		{

			try
			{

				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				SS_Tipo_Programa loTipo = new SS_Tipo_Programa(SesionSapei.Sistema);
				SS_Actividades loActividades = new SS_Actividades(SesionSapei.Sistema);
				SS_Dependencia loDependencias = new SS_Dependencia(SesionSapei.Sistema);

				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				DataTable loPr;
				string estado;
				loPr = loSolicitud.RegresaValidacionSS(loNoControl, lsPeriodo);
				if (loPr.Columns.Count == 1)
				{
					ViewData["Mensaje"] = loPr.RegresaValorFila<string>("mensaje");
					return PartialView("../Generales/AvisosGenerales");
				}

				estado = loPr.RegresaValorFila<string>("estado");
				ViewData["estado"] = estado;

				if (Convert.ToInt32(estado) >= 2)
				{
					ViewData["modalidad"] = loPr.RegresaValorFila<string>("modalidad");
				}

				ViewData["txtTitulo"] = loPr.Rows[0].RegresaValor<string>("nombre");
				ViewData["txtUrl"] = loPr.Rows[0].RegresaValor<string>("url");
				ViewData["Reporte"] = loPr.RegresaValorFila<int>("no_reporte");


				ViewData["TipoPrograma"] = loTipo.CargaTipoProgramaSS();
				ViewData["Descripcion"] = loActividades.CargaTipoActividadesSS();
				ViewData["Dependencias"] = loDependencias.DependenciasSS();
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}

		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult ProcesoSS2()
		{
			SS_Tipo_Programa loTipo = new SS_Tipo_Programa(SesionSapei.Sistema);
			SS_Actividades loActividades = new SS_Actividades(SesionSapei.Sistema);
			SS_Dependencia loDependencias = new SS_Dependencia(SesionSapei.Sistema);
			
			string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
			DataTable loPr = new DataTable();
			List<ParametrosSQL> loParameros = new List<ParametrosSQL>();

			try
			{
				loParameros.Add(new ParametrosSQL("@no_de_control", lsNoControl));
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loPr.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_ss_proceso", loParameros));
				}				


				ViewData["DatosProceso"] = loPr;
				ViewData["TipoPrograma"] = loTipo.CargaTipoProgramaSS();
				ViewData["Descripcion"] = loActividades.CargaTipoActividadesSS();
				ViewData["Dependencias"] = loDependencias.DependenciasSS();
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}

		}


		//Estado de SS del estudiante
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult ModificaEstadoSS(int piEstado, string psValores = "")
		{
			List<ParametrosSQL> loParameros = new List<ParametrosSQL>();

			try
			{
				loParameros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				loParameros.Add(new ParametrosSQL("@estado", piEstado));

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_ss_proceso_estado", loParameros);
				}
				ViewBag.Respuesta = "Registro Correcto";
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);

			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult ModificaEstadoSS2(int piEstado, string psValores = "")
		{
			List<ParametrosSQL> loParameros = new List<ParametrosSQL>();

			try
			{
				loParameros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				loParameros.Add(new ParametrosSQL("@estado", piEstado));

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_ss_proceso_estado", loParameros);
				}
				ViewBag.Respuesta = "Registro Correcto";
				return new PartialViewResult
				{
					ViewName = "_Partial",
					ViewData = new ViewDataDictionary("ok")
				};
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return new PartialViewResult
				{
					ViewName = "Error",
					ViewData = this.ViewData
				};

			}
		}

		//Estado de SS del estudiante
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult CargaEstadoInicialJsonSS()
		{
			try
			{
				SS_Estado_Solicitud loEstado = new SS_Estado_Solicitud(SesionSapei.Sistema);
				SS_Activar_Periodo loPeriodo = new SS_Activar_Periodo(SesionSapei.Sistema);
				DataTable loDt;
				loDt = loPeriodo.RegresaPeriodoSS();
				string lsPeriodo = loDt.RegresaValorFila<string>("periodo");
				string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loEstado.Cargar(loNoControl);
				if (loEstado.EOF)
				{
					loEstado.periodo = lsPeriodo;
					loEstado.no_de_control = loNoControl;
					loEstado.estado = 1;
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

		//Controlador que guarda la soliciud del estudiante interno de servicio social, para  solicitar carta de presentacion (seleccionando unicamente el turno)*****
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult GuardarSolicitudInternoJsonSS(string psTurno)
		{
			try
			{
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				SS_Activar_Periodo loPeriodo = new SS_Activar_Periodo(SesionSapei.Sistema);
				SS_Estado_Solicitud loEstado = new SS_Estado_Solicitud(SesionSapei.Sistema);

				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				int liPrograma = 1;
				int liFolio = 0;
				
				loEstado.Cargar(lsNumero);
				if (loEstado.EOF)
				{
					loEstado.GuardaBitacora(lsPeriodo, lsNumero, "Se intenta realizar doble registro de solicitud");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
				if (loEstado.estado >= 2)
				{
					//loEstado.GuardaBitacora(lsPeriodo, lsNumero, "Se intenta realizar doble registro de solicitud, estado mayor a 2");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Ya cuenta con un registro en semestres pasados, pongase en contacto con Vinculación",false);
				}
				loEstado.estado = 2;
				loEstado.Guardar();

				liFolio = loSolicitud.ConsultaFolioSS(lsPeriodo);
				
				loSolicitud.Cargar(liFolio, lsPeriodo, liPrograma);
				if (loSolicitud.EOF)
				{
					loSolicitud.id_programa = liPrograma;
					loSolicitud.folio = liFolio;
					loSolicitud.periodo = lsPeriodo;
				}
				loSolicitud.fecha_inicio = DateTime.Now;
				loSolicitud.fecha_termino = DateTime.Now;
				loSolicitud.fecha_solicitud = DateTime.Now;
				loSolicitud.modalidad = "int";
				loSolicitud.turno = psTurno;
				loSolicitud.estado = "1";
				loSolicitud.no_de_control = lsNumero;
				loSolicitud.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);

			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

        //Controlador para guardar actividades modalidad interno***** 
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult GuardarSolicitudServicioSocialInternoSS(string psActividades, string psTipo_actividad )
        {
            try
            {
                SS_Actividades_Solicitud loServicioSocial = new SS_Actividades_Solicitud(SesionSapei.Sistema);
                SS_Estado_Solicitud loEstado = new SS_Estado_Solicitud(SesionSapei.Sistema);

                DataTable loDt;
                string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;				
				loEstado.Cargar(lsNumero);
				if (loEstado.EOF)
				{
					loEstado.GuardaBitacora(lsPeriodo, lsNumero, "Se intenta realizar registro indebido");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
				if (loEstado.estado >= 5)
				{
					//loEstado.GuardaBitacora(lsPeriodo, lsNumero, "Se intenta realizar doble registro de solicitud, estado mayor a 5");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
				loEstado.estado = 5;
				loEstado.Guardar();

				loDt = loServicioSocial.RegresaDatosSolicitud(lsNumero);
                int liPrograma = loDt.RegresaValorFila<int>("id_programa");
                int liFolio = loDt.RegresaValorFila<int>("folio");
                string lsTipoPrograma = loDt.RegresaValorFila<string>("id_tipo_programa");
                loServicioSocial.Cargar(liFolio, lsPeriodo, liPrograma, lsTipoPrograma, psTipo_actividad);
                if (loServicioSocial.EOF)
                {
                    loServicioSocial.folio = liFolio;
                    loServicioSocial.periodo = lsPeriodo;
                    loServicioSocial.id_programa = liPrograma;
                    loServicioSocial.id_tipo_programa = lsTipoPrograma;
                    loServicioSocial.id_actividades = psTipo_actividad;
                }
                loServicioSocial.tipo_actividades = psActividades;
                loServicioSocial.id_actividades = psTipo_actividad;
                loServicioSocial.Guardar();

                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {

                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        //Controlador para guardar actividades modalidad externo y actualiza programa***** 
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult GuardarSolicitudServicioSocialExternoSS(string psTipo_actividad, string psActividades, int piId)
        {
            try
            {
                SS_Actividades_Solicitud loServicioSocial = new SS_Actividades_Solicitud(SesionSapei.Sistema);
                SS_Estado_Solicitud loEstado = new SS_Estado_Solicitud(SesionSapei.Sistema);
                SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);

                DataTable loDt;
                string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                loEstado.Cargar(lsNumero);
                if (loEstado.EOF)
                {
                    loEstado.GuardaBitacora(lsPeriodo, lsNumero, "Se intenta realizar registro indebido");
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                }
                if (loEstado.estado >= 5)
                {
                    //loEstado.GuardaBitacora(lsPeriodo, lsNumero, "Se intenta realizar doble registro de solicitud, estado mayor a 5");
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Ya cuenta con un registro de solicitud",false);
                }
                loEstado.estado = 5;
                loEstado.Guardar();

                loDt = loServicioSocial.RegresaDatosSolicitud(lsNumero);
                int liPrograma = loDt.RegresaValorFila<int>("id_programa");
                int liFolio = loDt.RegresaValorFila<int>("folio");
                string lsTipoPrograma = loDt.RegresaValorFila<string>("id_tipo_programa");
                loServicioSocial.Cargar(liFolio, lsPeriodo, liPrograma, lsTipoPrograma, psTipo_actividad);
                if (loServicioSocial.EOF)
                {
                    loServicioSocial.folio = liFolio;
                    loServicioSocial.periodo = lsPeriodo;
                    loServicioSocial.id_programa = liPrograma;
                    loServicioSocial.id_tipo_programa = lsTipoPrograma;
                    loServicioSocial.id_actividades = psTipo_actividad;
                }
                loServicioSocial.tipo_actividades = psActividades;
                loServicioSocial.id_actividades = psTipo_actividad;
                loServicioSocial.Guardar();
                loSolicitud.ActualizarIdPrograma(piId, lsNumero);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {

                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        //controlador de carta compromiso para cambiarlo de estado
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult CartaCompromisoJsonSS()
        {
            try
            {
                SS_Solicitud loCartaCompromiso = new SS_Solicitud(SesionSapei.Sistema);
                SS_Activar_Periodo loPeriodo = new SS_Activar_Periodo(SesionSapei.Sistema);
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                SS_Estado_Solicitud loEstado = new SS_Estado_Solicitud(SesionSapei.Sistema);
                loEstado.Cargar(lsNumero);
                if (loEstado.EOF)
                {
					loEstado.GuardaBitacora(lsPeriodo, lsNumero, "Se intenta realizar registro indebido");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                }
				if (loEstado.estado >= 6)
				{
					//loEstado.GuardaBitacora(lsPeriodo, lsNumero, "Se intenta realizar doble registro de solicitud, estado mayor a 6");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
				loEstado.estado = 6;
                loEstado.Guardar();
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);

			}
			catch (Exception ex)
			{

				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult GuardarReportesJsonSS(int pspreg1, int pspreg2, int pspreg3, int pspreg4, int pspreg5, int pspreg6, int pspreg7)
		{
			try
			{
				SS_Reportes loReportes = new SS_Reportes(SesionSapei.Sistema);
				SS_Estado_Solicitud loEstado = new SS_Estado_Solicitud(SesionSapei.Sistema);
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				DataTable loDp;
				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;//numero de control   				
				string lsPeriodo = loEstado.RegresaPeriodoResgistroSS(lsNumero);
				int lsid = 0;
				int idPrograma;
				int liFolio;
				int lsNo_Reporte = loReportes.ConsultaNumeroReporte(lsNumero);				
				loDp = loSolicitud.RegresaDatosSolicitud(lsNumero);
				idPrograma = loDp.RegresaValorFila<int>("id_programa");
				liFolio = loDp.RegresaValorFila<int>("folio");
				loReportes.Cargar(lsid, lsNumero, liFolio, lsPeriodo, idPrograma);
				if (loReportes.EOF)
				{
					loReportes.id = lsid;
					loReportes.no_de_control = lsNumero;
					loReportes.folio = liFolio;
					loReportes.periodo = lsPeriodo;
					loReportes.id_programa = idPrograma;
				}
				loReportes.no_reporte = lsNo_Reporte;
				loReportes.p_1 = pspreg1;
				loReportes.p_2 = pspreg2;
				loReportes.p_3 = pspreg3;
				loReportes.p_4 = pspreg4;
				loReportes.p_5 = pspreg5;
				loReportes.p_6 = pspreg6;
				loReportes.p_7 = pspreg7;
				double evaluacion = (pspreg1 + pspreg2 + pspreg3 + pspreg4 + pspreg5 + pspreg6 + pspreg7);
				evaluacion = Math.Round((evaluacion / 7), 2);
				loReportes.evaluacion = (float)evaluacion;
				loReportes.no_de_control = lsNumero;
				loReportes.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

        //Controlador que guarda solicitud del estudiante externo  de servicio social, para la solicitar ccarta de presentacion *****
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult GuardarSolicitudExternoJsonSS(string psTurno)
        {
            try
            {
                SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
                SS_Activar_Periodo loPeriodo = new SS_Activar_Periodo(SesionSapei.Sistema);
				SS_Estado_Solicitud loEstado = new SS_Estado_Solicitud(SesionSapei.Sistema);

				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				int liFolio;
                int liPrograma = 1;
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;

				loEstado.Cargar(lsNumero);
				if (loEstado.EOF)
				{
					loEstado.GuardaBitacora(lsPeriodo, lsNumero, "Se intenta realizar registro indebido (2)");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
				if (loEstado.estado >= 2)
				{
					//loEstado.GuardaBitacora(lsPeriodo, lsNumero, "Se intenta realizar doble registro de solicitud, estado mayor a 2");
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
				loEstado.estado = 2;
				loEstado.Guardar();

				liFolio = loSolicitud.ConsultaFolioSS(lsPeriodo);
                loSolicitud.Cargar(liFolio, lsPeriodo, liPrograma);
                if (loSolicitud.EOF)
                {
                    loSolicitud.folio = liFolio;
                    loSolicitud.periodo = lsPeriodo;
                    loSolicitud.id_programa = liPrograma;
                }
                loSolicitud.fecha_inicio = DateTime.Now;
                loSolicitud.fecha_termino = DateTime.Now;
                loSolicitud.fecha_solicitud = DateTime.Now;
                loSolicitud.modalidad = "ext";
                loSolicitud.turno = psTurno;
                loSolicitud.estado = "1";
                loSolicitud.no_de_control = lsNumero;
                loSolicitud.Guardar();

                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult AltaProgramaExternoJsonSS(string psDependencia, string psPrograma, string psCorreo, string psResponsable, string psCargoResponsablePrograma, string psDepartamento, string psTipoprograma, string psObjetivo)
		{
			try
			{
				SS_Programa loPrograma = new SS_Programa(SesionSapei.Sistema);
				SS_Activar_Periodo loPeriodo = new SS_Activar_Periodo(SesionSapei.Sistema);
				DataTable loDt;
				loDt = loPeriodo.RegresaPeriodoSS();
				string lsPeriodo = loDt.RegresaValorFila<string>("periodo");
				int lsid = 0;
				loDt = loPrograma.ConsultaProgramaSS(psPrograma, psDepartamento, psDependencia);
				if (loDt == null)
				{
					loPrograma.Cargar(lsid, lsPeriodo);
					if (loPrograma.EOF)
					{
						loPrograma.id = lsid;
						loPrograma.periodo = lsPeriodo;
					}
					loPrograma.rfc = psDependencia;
					loPrograma.nombre = psPrograma;
					loPrograma.id_tipo_programa = psTipoprograma;
					loPrograma.correo_titular = psCorreo;
					loPrograma.responsable = psResponsable;
					loPrograma.cargo_responsable = psCargoResponsablePrograma;
					loPrograma.objetivo = psObjetivo;
					loPrograma.departamento = psDepartamento;
					loPrograma.Guardar();
				}
				else
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

		//regresa los datos de los programas a elejir *****
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult RegresaNombreProgramaSS(string psRfc)
		{
			try
			{
				SS_Programa loPrograma = new SS_Programa(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loPrograma.RegresaNombreProgramaSS(psRfc));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}

		//Con el rfc el estudiante puede obtener todos los datos correspondientes de la empresa
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult RegresaDatosDependenciaSS(string psRFC)
		{
			try
			{
				SS_Dependencia loDependencia = new SS_Dependencia(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loDependencia.RegresaDatos(psRFC));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}

		//Con el rfc el estudiante puede obtener todos los datos correspondientes de la empresa asi como habilitar los programas *****
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult RegresaDatosProgramaSS(string psPrograma)
		{
			try
			{
				SS_Programa loPrograma = new SS_Programa(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loPrograma.RegresaPrograma(psPrograma));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult GuardarReporteEvaluacionActividadesJsonSS(int pspreg1, int pspreg2, int pspreg3, int pspreg4, int pspreg5, int pspreg6, int pspreg7, int pspreg8)
		{
			try
			{
				SS_Reportes loReportes = new SS_Reportes(SesionSapei.Sistema);
				SS_Estado_Solicitud loEstado = new SS_Estado_Solicitud(SesionSapei.Sistema);
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				DataTable loDp;
				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;//numero de control   
				string lsPeriodo = loEstado.RegresaPeriodoResgistroSS(lsNumero) ;
				int lsNo_Reporte = loReportes.ConsultaNumeroReporte(lsNumero);
				int lsid = 0;
				int idPrograma;
				int liFolio;
				loDp = loSolicitud.RegresaDatosSolicitud(lsNumero);
				idPrograma = loDp.RegresaValorFila<int>("id_programa");
				liFolio = loDp.RegresaValorFila<int>("folio");
				loReportes.Cargar(lsid, lsNumero, liFolio, lsPeriodo, idPrograma);
				if (loReportes.EOF)
				{
					loReportes.id = lsid;
					loReportes.no_de_control = lsNumero;
					loReportes.folio = liFolio;
					loReportes.periodo = lsPeriodo;
					loReportes.id_programa = idPrograma;
				}

				loReportes.no_reporte = lsNo_Reporte;
				loReportes.p_1 = pspreg1;
				loReportes.p_2 = pspreg2;
				loReportes.p_3 = pspreg3;
				loReportes.p_4 = pspreg4;
				loReportes.p_5 = pspreg5;
				loReportes.p_6 = pspreg6;
				loReportes.p_7 = pspreg7;
				loReportes.p_8 = pspreg8;
				loReportes.no_de_control = lsNumero;
				loReportes.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

        #endregion
        #region Residencias Profesionales
		//Carga la vista para la apertura de expediente
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public PartialViewResult ProcesoRP()
		{
			try
			{
                RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                DateTime fechainicio;
                DateTime fechafin;
                loDt = loSolicitud.RegresaValidacionRP(loNoControl, lsPeriodo);
                if (loDt.Columns.Count == 1)
                {
                    ViewData["Mensaje"] = loDt.RegresaValorFila<string>("mensaje");
                    return PartialView("../Generales/AvisosGenerales");
                }
                ViewData["txtTitulo"] = loDt.Rows[0].RegresaValor<string>("titulo");
                ViewData["txtUrl"] = loDt.Rows[0].RegresaValor<string>("url");
                ViewData["estado"] = loDt.RegresaValorFila<int>("estado");
                ViewData["no_de_registros"] = loDt.RegresaValorFila<string>("no_de_registros");
                ViewData["id_programa"] = loDt.RegresaValorFila<int>("id_programa");
                ViewData["NombrePrograma"] = loDt.RegresaValorFila<string>("nombre");
                ViewData["valida_datos"] = loDt.RegresaValorFila<int>("valida_datos");
                ViewData["delimitaciones"] = loDt.RegresaValorFila<string>("delimitaciones");
                ViewData["objetivo_general"] = loDt.RegresaValorFila<string>("objetivo_general");
                ViewData["objetivo_especifico"] = loDt.RegresaValorFila<string>("objetivo_especifico");
                ViewData["actividades"] = loDt.RegresaValorFila<string>("actividades");
                ViewData["justificacion"] = loDt.RegresaValorFila<string>("justificacion");
                ViewData["duracion"] = loDt.RegresaValorFila<int>("duracion");
                ViewData["observaciones"] = loDt.RegresaValorFila<string>("observaciones");
                ViewData["ubicacion"] = loDt;
                fechainicio = loDt.RegresaValorFila<DateTime>("fecha_inicio");
                ViewData["fecha_inicio"] = fechainicio.ToString("dd/MM/yyyy");
                fechafin = loDt.RegresaValorFila<DateTime>("fecha_fin");
                ViewData["fecha_fin"] = fechafin.ToString("dd/MM/yyyy");
                ViewData["asesor_interno"] = loDt.RegresaValorFila<string>("asesor_interno");
                ViewData["revisor"] = loDt.RegresaValorFila<string>("revisor");
                return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult ControlEscolarRP()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Función para cambiar de estado 1 --> 2
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult SolicitudCartaRP()
        {
            try
            {
                RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
                string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                loEstado.Cargar(loNoControl);
                if (loEstado.estado == 1)
                {
                    loEstado.estado = 2;
                    loEstado.Guardar();
                }
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }

        }
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult RegresaEstadoRP()
		{
			try
			{
				RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
				string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				return ManejoMensajesJson.RegresaJsonTabla(loEstado.ConsultaEstadoStepRP(loNoControl));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		//Función para consultar los datos de la dependencia solicitada, recibe el RFC
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult RegresaDatosDependenciaRP(string psRFC)
		{
			try
			{
				RP_Dependencias loDependencia = new RP_Dependencias(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loDependencia.RegresaDatos(psRFC));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		//Función para consultar los programas de las dependencias, recibe el RFC de la dependencia
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult RegresaNombreProgramaRP(string psRFC)
		{
			try
			{
				RP_Programa loPrograma = new RP_Programa(SesionSapei.Sistema);
                Alumno loAlumno = new Alumno(SesionSapei.Sistema);
                string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                string lsCarrera;
                loAlumno.Cargar(lsNumero);
                lsCarrera = loAlumno.carrera;
				return ManejoMensajesJson.RegresaJsonTabla(loPrograma.RegresaNombrePrograma(psRFC, lsCarrera));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		//Función para consultar los datos del programa, recibe el ID del programa
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult RegresaDatosProgramaRP(string psIdPrograma)
		{
			try
			{
				RP_Programa loPrograma = new RP_Programa(SesionSapei.Sistema);
                string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                return ManejoMensajesJson.RegresaJsonTabla(loPrograma.RegresaDatosPrograma(psIdPrograma, lsNumero));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		//Función para guardar un programa para RP
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult AltaProyectoJsonRP(string psRfcDependencia, string psNombreProyecto, string psResponsable, string psCargoResponsable, string psCorreo, string psDepartamento, string psOpcion, int psId)
		{
			try
			{
				RP_Programa loPrograma = new RP_Programa(SesionSapei.Sistema);
                Alumno loAlumno = new Alumno(SesionSapei.Sistema);
                DataTable loDt;
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                string lsCarrera;
                int liNoPrograma;
                loAlumno.Cargar(lsNoControl);
                lsCarrera = loAlumno.carrera;
                loDt = loPrograma.ConsultaProgramaRP(psNombreProyecto, psRfcDependencia, lsCarrera);
                if (loDt != null)
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Este proyecto ya se encuentra registrado",false);

                }
                liNoPrograma = loPrograma.ConsultaNoProgramaRP(lsPeriodo, lsCarrera);
                loPrograma.Cargar(psId, lsPeriodo);
                if (loPrograma.EOF)
                    {
                        loPrograma.id = psId;
                        loPrograma.periodo_programa = lsPeriodo;
                        loPrograma.GuardaBitacora(lsNoControl, lsPeriodo, psNombreProyecto, psRfcDependencia);
                }
                    loPrograma.carrera = lsCarrera;
                    loPrograma.numero_proyecto = liNoPrograma;
                    loPrograma.nombre = psNombreProyecto;
					loPrograma.correo = psCorreo;
					loPrograma.departamento = psDepartamento;
					loPrograma.responsable = psResponsable;
					loPrograma.cargo = psCargoResponsable;
					loPrograma.opcion_programa = psOpcion;
					loPrograma.rfc_dependencia = psRfcDependencia;
					loPrograma.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Función para generar un registro del rp_solicitud
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult CartaSolicitudJsonRP(int psPrograma, string psModalidad)
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
                RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
                RP_Periodos loPeriodo = new RP_Periodos(SesionSapei.Sistema);
				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                int liFolio = loSolicitud.ConsultaFolioRP(lsPeriodo);
                loEstado.Cargar(lsNumero);
                if (loEstado.estado < 4)
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Debes terner los pasos anteriores autorizados", false);
                }
                loSolicitud.Cargar(psPrograma, liFolio, lsPeriodo, lsNumero);
				if (loSolicitud.EOF)
				{
					loSolicitud.id_programa = psPrograma;
					loSolicitud.folio = liFolio;
					loSolicitud.periodo_datos = lsPeriodo;
					loSolicitud.no_de_control = lsNumero;
				}
                loSolicitud.fecha_solicitud = DateTime.Now;
				loSolicitud.fecha_inicio = DateTime.Now;
				loSolicitud.fecha_fin = DateTime.Now;
				loSolicitud.modalidad = psModalidad;
				loSolicitud.estado_solicitud = 1;
				loSolicitud.evaluacion_final = 0;
				loSolicitud.Guardar();
                loEstado.periodo = lsPeriodo;
                loEstado.estado = 5;
                loEstado.Guardar();
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Función para generar registro en rp_estado_solicitud
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult CargaEstadoInicialJsonRP()
		{
			try
			{
                RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                loEstado.Cargar(lsNumero);
                if (loEstado.EOF)
                {
                    loEstado.no_de_control = lsNumero;
                }
                loEstado.periodo = lsPeriodo;
                loEstado.estado = 1;
                loEstado.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Función para almacenar los datos del anteproyecto
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        [HttpPost]
        public JsonResult AltaAnteproyectoJsonRP(int piPrograma, string psDelimitacion,string psObjetivoG, string psObjetivoE,
               string psActividades,string psJustificacion, string psFechaInicio, string psFechaFin, int piDuracion, string psObservaciones)
        {
            try
            {
                RP_Datos_Programa loDatos = new RP_Datos_Programa(SesionSapei.Sistema);
                RP_Periodos loPeriodo = new RP_Periodos(SesionSapei.Sistema);
                RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
                Stream loSt;
                HttpPostedFileBase loFile;
                string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                byte[] loData = null;
                int lifuncion = 1;
                string psPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                if (psDelimitacion.Trim() == "" || psObjetivoG.Trim() == "" || psObjetivoE.Trim() == "" || psActividades.Trim() == "" || psActividades.Trim() == "" || psFechaInicio.Trim() == "" || psFechaFin.Trim() == "")
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Debes completar todos los campos",false);
                }
				if (psObjetivoE.Length > 1000)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("El campo Objetivos especificos es de máximo 1000 caracteres", false);
				}
				if (psActividades.Length > 1000)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("El campo Actividades es de máximo 1000 caracteres", false);
				}
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loFile = Request.Files[0];
                    loSt = loFile.InputStream;
                    loSt.Position = 0;
                    using (var binaryReader = new BinaryReader(loSt))
                    {
                        loData = binaryReader.ReadBytes(loFile.ContentLength);
                    }
                    loDatos.Cargar(piPrograma, psPeriodo);
                    if (loDatos.EOF)
                    {
                        loDatos.id_programa = piPrograma;
                        loDatos.periodo_datos = psPeriodo;
                    }
                    loDatos.delimitaciones = psDelimitacion;
                    loDatos.objetivo_general = psObjetivoG;
                    loDatos.objetivo_especificos = psObjetivoE;
                    loDatos.actividades = psActividades;
                    loDatos.duracion = piDuracion;
                    loDatos.justificacion = psJustificacion;
                    loDatos.ubicacion = loData;
                    loDatos.rfc_asesor = " ";
                    loDatos.rfc_revisor = " ";
                    loDatos.observaciones = psObservaciones;
                    loDatos.Guardar();
                    loSolicitud.ActualizaFechas(lsNumero,psFechaInicio, psFechaFin);
                    loSolicitud.RegresaValidacionAnteproyectoRP(lsNumero, psPeriodo, lifuncion);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }
		//Función para validar los datos del anteproyecto 
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult AceptaAnteproyectoJsonRP()
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                int lifuncion = 2;
                loSolicitud.RegresaValidacionAnteproyectoRP(loNoControl, lsPeriodo, lifuncion);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Carga las asesorias del residente
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public PartialViewResult AsesoriasRP()
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
                RP_Asesoria loAsesoria = new RP_Asesoria(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loDt = loAsesoria.AsesoriasEstudianteRP(lsNoControl);
				ViewData["Tabla"] = loDt;
				ViewData["periodo"] = lsPeriodo;
				ViewData["Titulo"] = "Asesorias Generadas del periodo "+ lsPeriodo.RegresaDescripcionLargaPeriodo();
				ViewData["Encabezados"] = new List<string> { " ", "Número de Asesoria", "Fecha" };
				loDt = loSolicitud.ConsultaAsesoriaRP(lsNoControl);
                ViewData["numero_asesoria"] = loDt.RegresaValorFila<int>("numero_asesoria");
				ViewData["duracion"] = loDt.RegresaValorFila<int>("duracion");
                ViewData["id_programa"] = loDt.RegresaValorFila<int>("id_programa");
                ViewData["folio"] = loDt.RegresaValorFila<int>("folio");
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Función para generar asesoria generada por el residente
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult AltaAsesoriaJsonRP(string psTemas, string psSolucion, string psTipo, int psNumero, int piPrograma, int piFolio, int piId)
		{
			try
			{
				RP_Asesoria loAsesoria = new RP_Asesoria(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loAsesoria.Cargar(piId, piPrograma, piFolio, lsPeriodo, loNoControl);
				if (loAsesoria.EOF)
				{
					loAsesoria.id = piId;
					loAsesoria.id_programa = piPrograma;
					loAsesoria.folio = piFolio;
					loAsesoria.periodo_asesorias = lsPeriodo;
					loAsesoria.no_de_control = loNoControl;
				}
				loAsesoria.fecha = DateTime.Now;
				loAsesoria.numero_asesoria = psNumero;
				loAsesoria.tipo = psTipo;
				loAsesoria.descripcion = psTemas;
				loAsesoria.solucion = psSolucion;
				loAsesoria.estado = 1;
				loAsesoria.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Función para consultar los datos alamcenados de la asesoria, recibe el número de asesoria a consultar
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult RegresaDatosAsesoriaRP(int psNumeroAsesoria)
		{
			try
			{
				RP_Asesoria loAsesoria = new RP_Asesoria(SesionSapei.Sistema);
				string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				return ManejoMensajesJson.RegresaJsonTabla(loAsesoria.RegresaAsesoriaRP(psNumeroAsesoria, loNoControl));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		//Función que consulta el número máximo de asesoría
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public JsonResult RegresaMaxAsesoriaRP()
        {
            try
            {
                RP_Asesoria loAsesoria = new RP_Asesoria(SesionSapei.Sistema);
                string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                return ManejoMensajesJson.RegresaJsonTabla(loAsesoria.RegresaMaxAsesoriaRP(loNoControl));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }
		//Carga la vista para los seguimientos
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public PartialViewResult SeguimientosRP()
		{
			try
			{
				RP_Seguimiento loSeguimiento = new RP_Seguimiento(SesionSapei.Sistema);
                DataTable loDt;
                string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                loDt = loSeguimiento.RegresaValidacionSeguimientoRP(lsNoControl, lsPeriodo);
                ViewData["periodo"] = lsPeriodo.RegresaDescripcionLargaPeriodo();
                ViewData["estado_primer_seguimiento"] = loDt.RegresaValorFila<string>("estado_primer_seguimiento");
                ViewData["estado_segundo_seguimiento"] = loDt.RegresaValorFila<string>("estado_segundo_seguimiento");
                ViewData["estado_tercer_seguimiento"] = loDt.RegresaValorFila<string>("estado_tercer_seguimiento");
                ViewData["fecha_primerS"] = loDt.RegresaValorFila<DateTime>("fecha_primerS").ToString("dd/MM/yyyy");
                ViewData["fecha_segundoS"] = loDt.RegresaValorFila<DateTime>("fecha_segundoS").ToString("dd/MM/yyyy");
                ViewData["fecha_tercerS"] = loDt.RegresaValorFila<DateTime>("fecha_tercerS").ToString("dd/MM/yyyy");
                ViewData["calf_interno_primerS"] = loDt.RegresaValorFila<int>("calf_interno_primerS");
                ViewData["calf_interno_segundoS"] = loDt.RegresaValorFila<int>("calf_interno_segundoS");
                ViewData["calf_interno_tercerS"] = loDt.RegresaValorFila<int>("calf_interno_tercerS");
                ViewData["calf_externo_primerS"] = loDt.RegresaValorFila<int>("calf_externo_primerS");
                ViewData["calf_externo_segundoS"] = loDt.RegresaValorFila<int>("calf_externo_segundoS");
                ViewData["calf_externo_tercerS"] = loDt.RegresaValorFila<int>("calf_externo_tercerS");
                return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Carga la vista para la descarga del informe tecnico
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public PartialViewResult LiberacionInformeTecnicoRP()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Carga la vista para la descarga del informe semestral
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public PartialViewResult InformeSemestralRP()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		#endregion
		#region HistorialPagos
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult HistorialPagos()
		{
			try
			{
				string lsControl;
				DataTable loDatos;
				Alumnos_Historial_Pagos loPagos;
				loPagos = new Alumnos_Historial_Pagos(SesionSapei.Sistema);
				ViewData["txtDescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionPeriodo();
				ViewData["Titulo"] = "Pagos registrados";
				ViewData["Encabezados"] = new List<string> { "Periodo", "Concepto", "Monto", "Abonado", "Condonación"};
				lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				ViewData["Tabla"] = loPagos.RegresaHistorialPagos(lsControl);
				ViewData["DatosPago"] = loPagos.RegresaDatosPagoActual(lsControl);
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult PagosServicios()
		{
			try
			{
				string lsControl;
				Alumnos_Historial_Servicios loPagos;
				Servicio loServicios;
				loPagos = new Alumnos_Historial_Servicios(SesionSapei.Sistema);
				loServicios = new Servicio(SesionSapei.Sistema);
				ViewData["txtDescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
				ViewData["Titulo"] = "Pagos de servicios registrados";
				ViewData["Encabezados"] = new List<string> { "Periodo", "Concepto",  "Fecha", "Pago Registrado" };
				lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				ViewData["Tabla"] = loPagos.RegresaHistorialPagos(lsControl);
				ViewData["Servicios"] = loServicios.ComboServicios();
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
		public PartialViewResult ControlServicios()
		{
			try
			{
				string lsControl;
				Servicio loServicios;

				loServicios = new Servicio(SesionSapei.Sistema);
				lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				//lsControl = "17106837";
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable ldtTabla = new DataTable();
				DataTable ldtTablaDatosFactura = new DataTable();
				DataTable ldtHistorialConstancias = new DataTable();
				loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));


				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					ldtTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rf_servicios"));
					ldtTablaDatosFactura.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_alumnos_facturacion", loParametros));
					ldtHistorialConstancias.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_alumnos_servicios", loParametros));
				}
				ViewData["Titulo"] = "Historial de Servicios";
				ViewData["Tabla"] = ldtHistorialConstancias;
				ViewData["Facturas"] = ldtTablaDatosFactura;
				ViewData["Servicios"] = ldtTabla;
				ViewData["HistorialConstancias"] = ldtHistorialConstancias;
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
		public JsonResult CambiaDatosFacturaJson(string psRazonNuevo, string psDomicilioNuevo, string psNoExtNuevo, string psRfcNuevo, string psCorreoNuevo, int psCpNuevo)
		{
			try
			{
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

				loParametros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				//loParametros.Add(new ParametrosSQL("@no_de_control", "17106837"));				
				loParametros.Add(new ParametrosSQL("@razon", psRazonNuevo));
				loParametros.Add(new ParametrosSQL("@domicilio", psDomicilioNuevo));
				loParametros.Add(new ParametrosSQL("@noext", psNoExtNuevo));
				loParametros.Add(new ParametrosSQL("@rfc", psRfcNuevo));
				loParametros.Add(new ParametrosSQL("@correo", psCorreoNuevo));
				loParametros.Add(new ParametrosSQL("@id_cp", psCpNuevo));
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_alumnos_facturacion", loParametros);
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
		public JsonResult ControlServiciosJson(string psServicio, string psFactura, string psFirma)
		{
			try
			{
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loRespuesta = new DataTable();
				loParametros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				//loParametros.Add(new ParametrosSQL("@no_de_control", "17106837")); 
				loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
				loParametros.Add(new ParametrosSQL("@servicio", psServicio));
				loParametros.Add(new ParametrosSQL("@firma", psFirma.ToBoolean()));
				loParametros.Add(new ParametrosSQL("@factura", psFactura.ToBoolean()));

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{					
					loRespuesta.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pai_alumnos_servicios", loParametros));						
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(loRespuesta.Rows[0].Field<string>("mensaje"),true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
		public PartialViewResult ServicioGenerado()
		{
			try
			{
				string lsControl;
				Servicio loServicios;

				loServicios = new Servicio(SesionSapei.Sistema);

				lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable ldtTablaDocumentos = new DataTable();
				loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));


				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					ldtTablaDocumentos.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_Servicios"));
				}
				ViewData["DocumentoGenerado"] = ldtTablaDocumentos;

				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
		public JsonResult IniciaPagoBanca(string psServicio)
		{
			try
			{
				string lsControl;
				string lsMonto;
				string lsServicio;
				string lsId;
				string lsConcepto;
				lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				//lsControl = "17106837";

				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loTabla = new DataTable();
				loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
				loParametros.Add(new ParametrosSQL("@servicio_folio", psServicio));

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_alumno_servicio",loParametros));
				}
				if (loTabla.Rows.Count == 0)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se encuentra el servicio solicitado",false);
				}
				else { 
					EvoBanamex evo = new EvoBanamex();
					lsServicio = loTabla.Rows[0].Field<string>("servicio");
					lsConcepto = loTabla.Rows[0].Field<string>("concepto");
					lsMonto = Convert.ToString(loTabla.Rows[0].Field<decimal>("monto")).CambiaComaPunto();
					lsId = loTabla.Rows[0].Field<string>("id");
					evo.EnviaTransaccion(lsMonto, lsServicio, lsConcepto, lsId);

					if (string.IsNullOrEmpty(evo.SessionID))
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Error al iniciar sesión con la banca electrónica",false);
					
					loParametros.Add(new ParametrosSQL("@indicador", evo.SuccessIndicator));
					loParametros.Add(new ParametrosSQL("@validacion", false));

					using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
					{
						SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_alumno_servicio_pago_linea", loParametros);
					}
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(evo.SessionID, true);
				}				
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
        //[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
        //public JsonResult RegistraPagoBanca(string psServicio, string psIndicador)
        //{
        //	try
        //	{
        //		string lsControl;
        //		bool lbValidado;
        //		lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
        //		//lsControl = "17106837";

        //		List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
        //		DataTable loTabla = new DataTable();
        //		loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
        //		loParametros.Add(new ParametrosSQL("@servicio_folio", psServicio));
        //		loParametros.Add(new ParametrosSQL("@indicador", psIndicador.Split(',')[0]));
        //		loParametros.Add(new ParametrosSQL("@validacion", true));

        //		using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
        //		{
        //			loTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_alumno_servicio_pago_linea", loParametros));
        //		}
        //		//lbValidado = loTabla.Rows[0].Field<string>("validado").ToBoolean();
        //		//if (lbValidado)
        //		//{
        //		//	Sapei.Framework.Utilerias.Funciones.ManejoCorreos.NotificaPagoServicio(
        //		//								loTabla.Rows[0].Field<string>("nombre"),
        //		//								loTabla.Rows[0].Field<string>("correo"),
        //		//								loTabla.Rows[0].Field<string>("indicador"),
        //		//								loTabla.Rows[0].Field<string>("servicio"),
        //		//								loTabla.Rows[0].Field<string>("monto")
        //		//												);
        //		//}

        //		return ManejoMensajesJson.RegresaMensajeJsonBusqueda(loTabla.Rows[0].Field<string>("respuesta"), true);
        //	}
        //	catch (Exception ex)
        //	{
        //		Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
        //		return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
        //	}
        //}
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
        public ViewResult RegistraPagoBanca(string resultIndicator, string sessionVersion)
        {
            try
            {
                string lsControl;
                bool lbValidado;
                lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                //lsControl = "17106837";

                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                DataTable loTabla = new DataTable();
                loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
                loParametros.Add(new ParametrosSQL("@servicio_folio", ""));
                loParametros.Add(new ParametrosSQL("@indicador", resultIndicator));
                loParametros.Add(new ParametrosSQL("@validacion", true));

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_alumno_servicio_pago_linea", loParametros));
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return View("Index");
            }
        }
        #endregion
        #region Reinscripcion
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult DatosReinscripcion()
		{
			try
			{
				string lsPeriodo;
				DataTable loDt = new DataTable();
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				ViewData["periodo"] = lsPeriodo.RegresaDescripcionPeriodo();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
					loParametros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_reinscripcion_datos", loParametros));
					if (loDt.Rows.Count == 0)
					{
						ViewData["Mensaje"] = "No se encuentran datos, acude a Centro de Cómputo";
						return PartialView("../Generales/AvisosGenerales");
					}
					ViewData["pago"] = loDt.Rows[0].RegresaValor<string>("pago");
					ViewData["fecha"] = loDt.Rows[0].RegresaValor<string>("fecha");
					ViewData["creditos"] = loDt.Rows[0].RegresaValor<int>("creditos");
					ViewData["evaluacion"] = loDt.Rows[0].RegresaValor<string>("evaluacion");
					ViewData["adeudos"] = loDt.Rows[0].RegresaValor<string>("adeudos");
					ViewData["carga"] = loDt.Rows[0].RegresaValor<string>("carga");
				}
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult SeleccionMaterias()
		{
			try
			{
				string lsPeriodo;
				DataTable loDt = new DataTable();
				DataTable ldtTablaDatosFactura = new DataTable();
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				ViewData["PeriodoDesc"] = lsPeriodo.RegresaDescripcionPeriodo();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
					loParametros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loParametros.Add(new ParametrosSQL("@iniciar", 0));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_seleccion_materias_datos", loParametros));
					loParametros.Clear();
					loParametros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					ldtTablaDatosFactura.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_alumnos_facturacion", loParametros));
					if (loDt.Rows.Count == 0)
					{
						ViewData["Mensaje"] = "No se encuentran datos, acude a Centro de Cómputo";
						return PartialView("../Generales/AvisosGenerales");
					}	
				}
				ViewData["Facturas"] = ldtTablaDatosFactura;
				ViewData["DatosSeleccion"] = loDt;
			
				if(loDt.Columns.Count == 16)
					return PartialView("../Reticula/Seleccion");
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}

		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult SeleccionMateriasJson()
		{
			try
			{
				string lsPeriodo;
				DataTable loDt = new DataTable();
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				ViewData["PeriodoDesc"] = lsPeriodo.RegresaDescripcionPeriodo();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
					loParametros.Add(new ParametrosSQL("@no_de_control", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loParametros.Add(new ParametrosSQL("@iniciar", 1));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_seleccion_materias_datos", loParametros));
					if (loDt.Rows.Count == 0)
					{
						ViewData["Mensaje"] = "No se encuentran datos, acude a Centro de Cómputo";
						return PartialView("../Generales/AvisosGenerales");
					}
					ViewData["DatosSeleccion"] = loDt;
				}
				return PartialView("../Reticula/Seleccion");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}

		}
		#endregion
		#region Avance reticular
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult Kardex()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult InformacionEscolar()
		{
			try
			{
				Alumno loAlumno = new Alumno(SesionSapei.Sistema);
				ViewData["PeriodoDesc"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
				ViewData["Datos"] = loAlumno.RegresaInformacionEscolar(SesionSapei.Sistema.Sesion.Usuario.Usuario);
				ViewData["Aviso3"] = "Una vez firmada la Carga Académica, será enviada a la División de Estudios Profesionales para ser firmada";
				ViewData["Aviso4"] = "Cuando tú carga este firmada por la DEP podrás descargarla e imprimirla";

				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult FirmaCargaAcademica(string psFirma)
		{
			try
			{
				Alumno loAlumno = new Alumno(SesionSapei.Sistema);
				if (psFirma.Trim() != SesionSapei.Sistema.Sesion.Usuario.Contraseña)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se reconoce la contraseña",false);

				}
				loAlumno.RegistraCadenaFiEl(enmTiposDocumentos.CargaAcademica);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Documento firmado, debes esperar a que sea firmado la DEP",true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}
		#endregion
		#region cuentas
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult Cuentas()
		{
			try
			{
				Alumno loAlumnos = new Alumno(SesionSapei.Sistema);
				ViewData["DatosCuentas"] = loAlumnos.RegresaCuentas(SesionSapei.Sistema.Sesion.Usuario.Usuario);
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

        #endregion
        #region BoletasClaif

        [SessionExpire]
        public PartialViewResult BoletaCalificaciones()
        {
            try
            {
                string lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    DataTable loDt = new DataTable();
                    DataTable loDt2 = new DataTable();
                    loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_alumnos_periodos_cursados", loParametros));
                    ViewData["Titulo"] = "Personal Solicitado";
                    ViewData["Encabezados"] = new List<string> { "Periodo", "Periodo", "Acción" };
                    ViewData["Tabla"] = loDt;
                    return PartialView();
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Home", "Index");
            }

        }

        #endregion

        #region GruposDisponibles

        public PartialViewResult GruposDisponibles()
        {
            try
            {
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                DataTable loDt = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();                  
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {
                        loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                        loParametros.Add(new ParametrosSQL("@no_de_control", lsNoControl));
                        loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_reticula_grupos_disponibles", loParametros));
                        ViewData["DatosSeleccion"] = loDt;
                    }
                    return PartialView("GruposDisponibles");
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }

		public PartialViewResult DatosGruposDisponibles(string psPeriodo, string psMateria)
		{               
			try 
			{
                string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                DataTable loDt = new DataTable();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));               
					loParametros.Add(new ParametrosSQL("@materia", psMateria));                
					loParametros.Add(new ParametrosSQL("@no_de_control", lsNoControl));                
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_grupos_disponibles", loParametros));               
					ViewData["Titulo"] = "Personal Solicitado";               
					ViewData["Encabezados"] = new List<string> { "Materia", "Grupo", "Capacidad", "Docente", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };               
					ViewData["Tabla"] = loDt;
                }
                return PartialView();
            }              
			catch (Exception ex)               
			{                 
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");               
			}           
		}
  	}
        #endregion 
}

