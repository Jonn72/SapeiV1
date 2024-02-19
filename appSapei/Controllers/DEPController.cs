using appSapei.App_Start;
using appSapei.Clases;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using Syncfusion.EJ2.Schedule;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
	public class DEPController : Controller
	{
		#region CargaAcademica
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult CargaAcademica()
		{
			try
			{
				DataTable loTabla;
				Dep_Fechas_Carga_Academica loCarga;
				loCarga = new Dep_Fechas_Carga_Academica(SesionSapei.Sistema);

				loTabla = loCarga.RegresaTablaPeriodos();
				ViewData["txtPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				ViewData["txtDescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
				ViewData["TablaPeriodos"] = loTabla;
				ViewData["Tabla"] = loCarga.RegresaTablaCargasAcademicasFiEl(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual);
				ViewData["Titulo"] = "Historial Cargas Académicas";
				ViewData["Encabezados"] = new List<string> { "No. Control", "Nombre", "Firma Estudiante", "Fecha", "Firma DEP", "Fecha" };
				ViewData["Aviso3"] = "Una vez firmada las Cargas Académicas, podran ser impresas por los estudiantes";
				ViewData["Aviso4"] = "Y aceptas que invitaras las cocas al Jefe de Centro de Cómputo";

				if (loTabla.Rows.Count == 0)
				{
					return PartialView();
				}
				if (loTabla.Rows[0].RegresaValor<string>("periodo").Trim().ToUpper() == SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta.Trim().ToUpper())
				{
					ViewData["txtIniCarga"] = loTabla.Rows[0].RegresaValor<string>("ini_carga_academica");
					ViewData["txtFinCarga"] = loTabla.Rows[0].RegresaValor<string>("fin_carga_academica");

				}
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult FirmaCargasAcademicas(string psFirma)
		{
			try
			{
				Dep_Fechas_Carga_Academica loDEP = new Dep_Fechas_Carga_Academica(SesionSapei.Sistema);
				if (psFirma.Trim() != SesionSapei.Sistema.Sesion.Usuario.Contraseña)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se reconoce la contraseña", false);

				}
				loDEP.RegistraCadenaFiEl(enmTiposDocumentos.CargaAcademica);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Documentos firmados", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult ActivaPeriodorCargaAcademicaJson(DateTime poInicio, DateTime poFin)
		{
			try
			{
				Dep_Fechas_Carga_Academica loPeriodos;
				string lsPeriodo;
				loPeriodos = new Dep_Fechas_Carga_Academica(SesionSapei.Sistema);
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				loPeriodos.Cargar(lsPeriodo);
				if (loPeriodos.EOF)
				{
					loPeriodos.periodo = lsPeriodo;
				}
				loPeriodos.ini_carga_academica = poInicio;
				loPeriodos.fin_carga_academica = poFin.UltimaHoraDia();
				loPeriodos.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

		#endregion
		#region Reinscripcion
		/// <summary>
		/// ActivarPeriodo
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult ReinscripcionFechasCarreras()
		{
			try
			{
				Dep_Fechas_Carreras_Seleccion loPeriodos;
				Carreras loCarreras;
				string lsPeriodo;
				loPeriodos = new Dep_Fechas_Carreras_Seleccion(SesionSapei.Sistema);
				loCarreras = new Carreras(SesionSapei.Sistema);
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				ViewData["txtPeriodo"] = lsPeriodo;
				ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
				ViewData["Tabla"] = loPeriodos.RegresaTablaPeriodos(lsPeriodo,true);
				ViewData["cboCarreras"] = loCarreras.RegresaComboCarreras();
				ViewData["Titulo"] = "Periodos Registrados";
				ViewData["Encabezados"] = new List<string> {"Editar/Listas/Publicar", "Periodo","Carrera", "Fecha Inicio","Intervalo", "Estudiantes por bloque" };

				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult ReinscripcionFechasCarrerasJson(string psCarrera, DateTime poInicio, int piIntervalo, int piBloque)
		{
			try
			{
				Dep_Fechas_Carreras_Seleccion loPeriodos;
				string lsPeriodo;
				loPeriodos = new Dep_Fechas_Carreras_Seleccion(SesionSapei.Sistema);
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loPeriodos.Cargar(lsPeriodo,psCarrera);
				if (loPeriodos.EOF)
				{
					loPeriodos.periodo = lsPeriodo;
					loPeriodos.carrera = psCarrera;
				}
				loPeriodos.fecha_inicio = poInicio;
				loPeriodos.intervalo = piIntervalo;
				loPeriodos.personas = piBloque;
				loPeriodos.publicada = false;
				loPeriodos.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult PublicaListasJson(string psCarrera)
		{
			try
			{
				Dep_Fechas_Carreras_Seleccion loPeriodos;
				string lsPeriodo;
				loPeriodos = new Dep_Fechas_Carreras_Seleccion(SesionSapei.Sistema);
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loPeriodos.Cargar(lsPeriodo, psCarrera);
				if (loPeriodos.EOF)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
				loPeriodos.publicada = !loPeriodos.publicada;
				loPeriodos.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult ListasSeleccionMateriasCarreras(string psCarrera, int piGenera)
		{
			try
			{
				string lsPeriodo;
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				ViewData["txtCarrera"] = psCarrera;
				ViewData["txtPeriodo"] = lsPeriodo;
				ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					DataTable loDatos = new DataTable();
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
					loParametros.Add(new ParametrosSQL("@carrera", psCarrera));
					loParametros.Add(new ParametrosSQL("@genera", piGenera));
					loDatos.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_reinscripcion_genera_horarios", loParametros));
					ViewData["Tabla"] = loDatos;
					ViewData["Titulo"] = "Periodos Registrados";
					ViewData["Encabezados"] = new List<string> { "No. Control", "Nombre", "Fecha Selección", "Prioridad", "Promedio", "Semestre", "Carga","Encuesto","Adeudo" };
					return PartialView();
				}
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult ResetHoraSeleccion()
		{
			return VistaCambiosHorarioSeleccionMaterias("ResetHoraSeleccion");
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult JsonResetHoraSeleccion(string psControl)
		{
			try
			{

				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
					loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					SesionSapei.Sistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pam_seleccion_materias_reset", loParametros);

				}

				Avisos_Reinscripcion loAviso;
				loAviso = new Avisos_Reinscripcion(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loAviso.Cargar(lsPeriodo, psControl);
				if (loAviso.fecha_hora_fin == null)
					return VistaCambiosHorarioSeleccionMaterias("ResetHoraSeleccion");
				loAviso.GrabaLog();
				loAviso.fecha_hora_fin = null;
				loAviso.Guardar();

				//Reset seleccion sapei


				return VistaCambiosHorarioSeleccionMaterias("ResetHoraSeleccion");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult ModificaHoraSeleccion()
		{
			return VistaCambiosHorarioSeleccionMaterias("ModificaHoraSeleccion",false);
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult JsonModificaHoraSeleccion(string psControl, DateTime poFecha)
		{
			try
			{
				Avisos_Reinscripcion loAviso;
				loAviso = new Avisos_Reinscripcion(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loAviso.Cargar(lsPeriodo, psControl);
				loAviso.GrabaLog();
				loAviso.fecha_hora_seleccion = poFecha;
				loAviso.Guardar();
				return VistaCambiosHorarioSeleccionMaterias("ModificaHoraSeleccion");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		private PartialViewResult VistaCambiosHorarioSeleccionMaterias(string psVista, bool pbEsSoloLectura = true)
		{
			try
			{
				string lsPeriodo;
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				if(pbEsSoloLectura)
					ViewData["readonly"] = "readonly";
				ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
				ViewData["Tabla"] = null;
				ViewData["Titulo"] = "Periodos Registrados";
				ViewData["Encabezados"] = new List<string> { "Nombre", "Creditos", "Fecha Selección", "Fecha Fin", "Autorizado", "Modificado el", "Modificado por" };
				return PartialView(psVista);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult DatosReinscripcion(string psControl)
		{
			try
			{
				Avisos_Reinscripcion loAviso;
				string lsPeriodo;
				DataTable loDt;
				loAviso = new Avisos_Reinscripcion(SesionSapei.Sistema);
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loAviso.Cargar(lsPeriodo, psControl);
				loDt = loAviso.RegresaBitacoraAvisos(lsPeriodo, psControl);
				if (loDt.Rows.Count == 0)
					ViewData["txtNombre"] = loAviso.no_de_control;
				else
					ViewData["txtNombre"] = loDt.Rows[0].RegresaValor<string>("nombre");
				ViewData["txtFecha"] = loAviso.fecha_hora_seleccion;
				ViewData["txtFechaFin"] = loAviso.fecha_hora_fin;
				ViewData["txtCreditos"] = loAviso.creditos_autorizados;
				if(loAviso.dep_autoriza_extemporaneo)
					ViewData["cbxExtemporaneo"] = "checked";				
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Periodos Registrados";
				ViewData["Encabezados"] = new List<string> { "Nombre", "Creditos", "Fecha Selección", "Fecha Fin", "Autorizado", "Modificado el", "Modificado por" };

				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult AutorizaReinscripcionExt()
		{
			try
			{
				return VistaCambiosHorarioSeleccionMaterias("AutorizaReinscripcionExt", false);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult AutorizaReinscripcionExtJson(string psControl, bool pbAutoriza)
		{
			try
			{
				Avisos_Reinscripcion loAviso;
				loAviso = new Avisos_Reinscripcion(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loAviso.Cargar(lsPeriodo, psControl);
				loAviso.dep_autoriza_extemporaneo = pbAutoriza;
				loAviso.Guardar();
				return VistaCambiosHorarioSeleccionMaterias("AutorizaReinscripcionExt");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

        public PartialViewResult AutorizacionesAcademicas()
        {
            try
            {
                string lsPeriodo;
                lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
                DataTable loDatos = new DataTable();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
					loParametros.Add(new ParametrosSQL("@no_de_control", null));
					loDatos.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_lista_autorizaciones", loParametros));
				}
                ViewData["Tabla"] = loDatos;
                ViewData["Titulo"] = "Autorizaciones Registradas";
                //ViewData["Encabezados"] = new List<string> { "N. Control", "Nombre", "Tipo de Autorizacion", "Motivo", "Autorizado por", "Fecha de Autoriacion", "Materia Afectada" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        public PartialViewResult DatosAlumnoAutorizacion(string psNoControl)
        {
            try
            {
                DataTable loDatos = new DataTable();
                DataTable loDatos2 = new DataTable();
                DataTable loDatos3 = new DataTable();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    loDatos.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_regresa_tipos_autorizacion", loParametros));

                    loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
                    loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
                    loDatos2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_datos_alumno_autorizacion", loParametros));
                    loDatos3.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_materias_sin_cursar_alumno", loParametros));
                }
				ViewData["TAutorizacion"] = loDatos;
                ViewData["Datos"] = loDatos2;
                ViewData["MateriasAlumno"] = loDatos3;
                return PartialView();
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("../Personal/Index");
            }

        }

        // GuardarAutorizacionAlumno

        public JsonResult GuardarAutorizacionAlumno(string psNoControl, string psMateria, string psMotivo, string psTipoAutorizacion)
        {
            try
            {
                string lsMensaje;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                System.Data.SqlClient.SqlDataReader loReader;
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
                    loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
                    loParametros.Add(new ParametrosSQL("@materia", psMateria));
                    loParametros.Add(new ParametrosSQL("@motivo", psMotivo));
                    loParametros.Add(new ParametrosSQL("@tipoauto", psTipoAutorizacion));
                    loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));

                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_registra_autorizacion", loParametros);
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

					ViewData["Mensaje"] = lsMensaje;
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);

                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        #endregion
        #region Seleccion de Materias
        [HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult SeleccionMaterias()
		{
			try
			{
				string lsPeriodo;
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				ViewData["txtPeriodo"] = lsPeriodo;
				ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		

		#endregion
		#region Autorizar Seleccion Grupos CLE
		/// <summary>
		/// ActivarPeriodo
		/// </summary>
		/// <returns></returns>
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult ActivarPeriodoCLE(string psPeriodo = null)
		{
			try
			{
				DataTable loTabla;
				Periodos_Ingles loPeriodos;
				string lsPeriodo;
				loPeriodos = new Periodos_Ingles(SesionSapei.Sistema);
				if (string.IsNullOrEmpty(psPeriodo))
				{
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;

				}
				else
				{
					lsPeriodo = psPeriodo;
				}

				ViewData["cboPeriodos"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual.RegresaComboPeriodos(lsPeriodo, 1, false, true);


				if (!loPeriodos.ValidaPeriodoEscolar(lsPeriodo))
				{
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				}
				loTabla = loPeriodos.RegresaTablaPeriodos();
				ViewData["txtPeriodo"] = lsPeriodo;
				ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
				ViewData["Tabla"] = loTabla;
				ViewData["Titulo"] = "Periodos Registrados";
				ViewData["Encabezados"] = new List<string> { "Periodo", "Inicio Registro", "Fin Registro", "Inicio Selección", "Fin Selección", "Inicio Captura Calif.", "Fin Captura Calif." };
				if (loTabla.Rows.Count == 0)
				{
					return PartialView("ActivarPeriodoCLE");
				}



				var loFilaV = loTabla.AsEnumerable().Where(r => r.Field<string>("periodo") == lsPeriodo);
				if (loFilaV.NotEmpty())
				{
					foreach (DataRow loFila in loFilaV)
					{
						ViewData["txtIniRegistro"] = loFila.RegresaValor<string>("ini_registro_grupos");
						ViewData["txtFinRegistro"] = loFila.RegresaValor<string>("fin_registro_grupos");
						ViewData["txtIniSeleccion"] = loFila.RegresaValor<string>("ini_seleccion");
						ViewData["txtFinSeleccion"] = loFila.RegresaValor<string>("fin_seleccion");
						ViewData["txtIniCaptura"] = loFila.RegresaValor<string>("ini_captura_calif");
						ViewData["txtFinCaptura"] = loFila.RegresaValor<string>("fin_captura_calif");
					}
				}
				return PartialView("ActivarPeriodoCLE");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult GuardaPeriodo(string psPeriodo, string psIniRegistro, string psFinRegistro, string psIniSeleccion, string psFinSeleccion, string psIniCaptura, string psFinCaptura)
		{
			try
			{
				Periodos_Ingles loControl;
				loControl = new Periodos_Ingles(SesionSapei.Sistema);
				loControl.Cargar(psPeriodo);
				if (loControl.EOF)
				{
					loControl.periodo = psPeriodo;
				}
				loControl.ini_registro_grupos = Convert.ToDateTime(psIniRegistro);
				loControl.fin_registro_grupos = Convert.ToDateTime(psFinRegistro);
				loControl.ini_seleccion = Convert.ToDateTime(psIniSeleccion);
				loControl.fin_seleccion = Convert.ToDateTime(psFinSeleccion);
				loControl.ini_captura_calif = Convert.ToDateTime(psIniCaptura);
				loControl.fin_captura_calif = Convert.ToDateTime(psFinCaptura);
				loControl.Guardar();
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
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
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
		//Carga la vista para la pertura de periodo de RP
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
        public PartialViewResult AperturaPeriodoRP(string psPeriodo = null)
		{
			try
			{
				RP_Periodos loPeriodos = new RP_Periodos(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo;
				if (string.IsNullOrEmpty(psPeriodo))
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				else
					lsPeriodo = psPeriodo;
				loDt = loPeriodos.PeriodosRP();
				ViewData["periodo"] = lsPeriodo.RegresaDescripcionPeriodo();
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Periodos Registrados";
				ViewData["Encabezados"] = new List<string> { "Periodo", "Descripción periodo", "Fecha Inicio", "Fecha Fin","1° Seguimiento 16", "2° Seguimiento 16", "3° Seguimiento 16", "1° Seguimiento 24", "2° Seguimiento 24", "3° Seguimiento 24", "Titulo","URL" };

				var loFilaV = loDt.AsEnumerable().Where(r => r.Field<string>("periodo") == lsPeriodo);

				if (loFilaV.NotEmpty())
				{
					foreach (DataRow loFila in loFilaV)
					{
						ViewData["txtFechaInicio"] = loFila.RegresaValor<string>("fecha_inicio");
						ViewData["txtFechaFin"] = loFila.RegresaValor<string>("fecha_fin");
						ViewData["primerS16"] = loFila.RegresaValor<string>("primerS16");
						ViewData["segundoS16"] = loFila.RegresaValor<string>("segundoS16");
						ViewData["tercerS16"] = loFila.RegresaValor<string>("tercerS16");
						ViewData["primerS24"] = loFila.RegresaValor<string>("primerS24");
						ViewData["segundoS24"] = loFila.RegresaValor<string>("segundoS24");
						ViewData["tercerS24"] = loFila.RegresaValor<string>("tercerS24");
						ViewData["txtTitulo"] = loFila.RegresaValor<string>("nombre");
						ViewData["txtUrl"] = loFila.RegresaValor<string>("url");
					}
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
		//Función para activar el periodo de RP, recibe el periodo y fechas
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
        public JsonResult ActivarPeriodoJsonRP(string psPeriodo, DateTime psInicio, DateTime psFin, DateTime psPrimerS16,
				DateTime psSegundoS16, DateTime psTercerS16, DateTime psPrimerS24, DateTime psSegundoS24, DateTime psTercerS24, 
				string psTitulo, string psUrl)
		{
			try
			{
				RP_Periodos loPeriodos = new RP_Periodos(SesionSapei.Sistema);
				loPeriodos.Cargar(psPeriodo);
				if (loPeriodos.EOF)
				{
					loPeriodos.periodo = psPeriodo;
				}
				loPeriodos.fecha_inicio = psInicio;
				loPeriodos.fecha_fin = psFin;
                loPeriodos.primerS16 = psPrimerS16;
                loPeriodos.segundoS16 = psSegundoS16;
                loPeriodos.tercerS16 = psTercerS16;
                loPeriodos.primerS24 = psPrimerS24;
                loPeriodos.segundoS24 = psSegundoS24;
                loPeriodos.tercerS24 = psTercerS24;
                loPeriodos.nombre = psTitulo;
                loPeriodos.url = psUrl;
				loPeriodos.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//Carga los residentes para evaluar su estatus escolar y autorizar su residencia
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
        public PartialViewResult AutorizacionResidenciasRP()
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loDt = loSolicitud.AutorizaResidenciaRP();
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Autorización de Residencias Profesionales " + lsPeriodo.RegresaDescripcionLargaPeriodo();
				ViewData["Encabezados"] = new List<string> { "  ", "Numero de Control", "Nombre de Residente", "Carrera", "Creditos Aprovados" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Función para validar la residencia, recibe el número de control del residente
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
        public JsonResult ValidaResidenciaJsonRP(string psNoControl)
		{
			try
			{
                RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
                loEstado.Cargar(psNoControl);
                if (loEstado.estado == 10)
                {
                    loEstado.estado = 11;
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
		//Carga los residentes que ya fueron liberados por DGTyV
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
        public PartialViewResult CierreResidenciasRP()
		{
			try
			{
				RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loDt = loEstado.CargaCierre();
				ViewData["periodo"] = lsPeriodo;
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Cierre de Residencias";
				ViewData["Encabezados"] = new List<string> { "Periodo", "Número de control", "Evaluación final", "Carta de termino" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Consulta el promedio final del residente obtenida de sus 3 seguimientos
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
        public JsonResult RegresaCalificacionFinalRP(string psNoControl)
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loSolicitud.ConsultaEvaluacionesRP(psNoControl));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		//Carga el estado en el que se encuentran los residentes
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
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
		//Carga el folio de los residentes para cancelación
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult BajaFoliosRP()
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loDt = loSolicitud.CargaFoliosRP(lsPeriodo);
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Baja de folio de Folio "+ lsPeriodo.RegresaDescripcionLargaPeriodo();
				ViewData["Encabezados"] = new List<string> { "Estado","Periodo","Folio", "N° de Control"};				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		//Función para la cancelación de folio, recibe el folio que sera dado de baja
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult BajaFoliosJsonRP(int piFolio)
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
				string usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loSolicitud.BajaFoliosRP(usuario, piFolio);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		#endregion
		#region Grupos
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult AdministracionGrupos()
		{
			try
			{
				string lsPeriodo;
				DataTable loDt = new DataTable();
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				ViewData["PeriodoDesc"] = lsPeriodo.RegresaDescripcionPeriodo();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{              
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_carreras_ret_esp"));
					ViewData["objeto"] = loDt;
				}

				return PartialView("AdministracionGrupos");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}

		}

		public PartialViewResult ReticulaGrupos(string psCarreraReticula, string psEspecialidad)
		{
			try
			{
				
				string lsPeriodo;
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				if (psCarreraReticula == "0" && psEspecialidad == "0")
                {
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
					ViewData["PeriodoDesc"] = lsPeriodo.RegresaDescripcionPeriodo();
					using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
					{
						loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
						loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_materias_sin_especialidad", loParametros));
						ViewData["Tabla"] = loDt;
						ViewData["Titulo"] = "Materias";
					}
					return PartialView("ListaMateriasSinEspecialidad");
				}
                else{
					string[] subs = psCarreraReticula.Split('_');
					string lsCarrera = subs[0];
					string lsReticula = subs[1];
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
					ViewData["PeriodoDesc"] = lsPeriodo.RegresaDescripcionPeriodo();
					using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
					{
						loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
						loParametros.Add(new ParametrosSQL("@carrera", lsCarrera));
						loParametros.Add(new ParametrosSQL("@reticula", lsReticula));
						loParametros.Add(new ParametrosSQL("@especialidad", psEspecialidad));
						loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_reticula_grupos", loParametros));
						ViewData["DatosSeleccion"] = loDt;
					}
					return PartialView("ReticulaGrupos");
				}
				
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

		public PartialViewResult Grupos(string psPeriodo, string psCarrera, string psReticula, string psMateria, string psEspecialidad)
		{
			try
			{

				DataTable ldtTabla = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@carrera", psCarrera));
					loParametros.Add(new ParametrosSQL("@reticula", psReticula));
					loParametros.Add(new ParametrosSQL("@materia", psMateria));
					loParametros.Add(new ParametrosSQL("@especialidad", psEspecialidad));
					ldtTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_horario_grupos_materia", loParametros));
					ViewData["Tabla"] = ldtTabla;
					ViewData["Titulo"] = "Grupos";
					ViewData["Periodo"] = psPeriodo;
					ViewData["Materia"] = psMateria;
					ViewData["Carrera"] = psCarrera;
					ViewData["Reticula"] = psReticula;
					ViewData["Especialidad"] = psEspecialidad;
					return PartialView("Grupos");
				}

			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

		public JsonResult GuardaGrupoJSON(string psPeriodo, string psMateria, string psCarrera, int psReticula, string psGrupo, int psCapacidad)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    
                    Grupos loGrupos;
					loGrupos = new Grupos(SesionSapei.Sistema);

					loGrupos.Cargar(psPeriodo, psMateria, psGrupo);
					if (loGrupos.EOF)
					{
						loGrupos.periodo = psPeriodo;
						loGrupos.materia = psMateria;
						loGrupos.grupo = psGrupo;
					}
                    else
                    {
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda("El Grupo ya existe", false);
					}
					loGrupos.capacidad_grupo = psCapacidad;
					if(psCarrera != "0" && psReticula != 0)
					{ 
						loGrupos.exclusivo_carrera = psCarrera;
						loGrupos.exclusivo_reticula = psReticula;
					}
					loGrupos.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
		}

		public JsonResult EliminaGrupoJSON(string psPeriodo, string psMateria, string psGrupo)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					Grupos loGrupos;
					loGrupos = new Grupos(SesionSapei.Sistema);					
					loGrupos.Cargar(psPeriodo, psMateria, psGrupo);
					if (loGrupos.EOF)
					{
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda("El Grupo no existe", false);
					}
					else
					{
						string lsMensaje;
						System.Data.SqlClient.SqlDataReader loReader;
						List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
						loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
						loParametros.Add(new ParametrosSQL("@materia", psMateria));
						loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
						loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_dep_elimina_grupo", loParametros);
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
							return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
						}
						loGrupos.Eliminar();
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
					}
				}
			}

			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}

		public PartialViewResult AgregaHorarioGrupo(string psPeriodo, string psMateria, string psGrupo, string psCarrera, string psReticula, string psEspecialidad)
		{
			try
			{
				DataTable loDt = new DataTable();
				DataTable loDt2 = new DataTable();
				DataTable ldtTabla = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@materia", psMateria));
					loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
					loParametros.Add(new ParametrosSQL("@carrera", psCarrera));
					loParametros.Add(new ParametrosSQL("@reticula", psReticula));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_aulas", loParametros));
					ldtTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_horario_grupo_materia_seleccionado", loParametros));
					loParametros.Add(new ParametrosSQL("@especialidad", psEspecialidad));
					loDt2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_datos_aula_grupo", loParametros));
					ViewData["Tabla"] = ldtTabla;
					ViewData["Titulo"] = "Grupo";
					ViewData["DatosSeleccion"] = loDt2;
					ViewData["Aulas"] = loDt;
					ViewData["Especialidad"] = psEspecialidad;
				}

				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

		public class ButtonModel
		{
			public string content { get; set; }
			public string cssClass { get; set; }
			public bool isPrimary { get; set; }
		}

		public PartialViewResult CargaHorarioAula(string psPeriodo, string psMateria, string psGrupo, string psAula, string psCarrera, string psReticula)
		{
			try
			{
				DataTable psDatosAula = new DataTable();
				DataTable psDatosGrupo = new DataTable();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					ViewBag.button1 = new ButtonModel { content = "Si", isPrimary = true };
					ViewBag.button2 = new ButtonModel { content = "No" };
					Aulas loAula;
					loAula = new Aulas(SesionSapei.Sistema);
					psDatosAula = loAula.RegresaDatosHorarioAula(psPeriodo, psMateria, psGrupo, psAula);
					psDatosGrupo = loAula.RegresaDatosGrupoAula(psPeriodo, psMateria, psGrupo, psAula);
					AppointmentData loHorarioAula;
					loHorarioAula = new AppointmentData();
					ViewBag.datasource = loHorarioAula.RegresaHorasClase(psDatosAula, psDatosGrupo, psPeriodo, psMateria, psGrupo, psAula, psCarrera, psReticula);
					ViewData["Periodo"] = psPeriodo;
					ViewData["Materia"] = psMateria;
					ViewData["Grupo"] = psGrupo;
					ViewData["Aula"] = psAula;
					ViewData["Carrera"] = psCarrera;
					ViewData["Reticula"] = psReticula;

				}
				return PartialView("CargaHorarioAula");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}

		public JsonResult GuardaHorarioGrupoJSON(string lsobjeto)
		{
			try
			{
				string lsMensaje;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loDt = new DataTable();
				System.Data.SqlClient.SqlDataReader loReader;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
					loParametros.Add(new ParametrosSQL("@objeto", lsobjeto));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_dep_registra_horario_grupo", loParametros);
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
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
					}
                    else 
					{ 				
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);		
					}
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult EliminaHorarioGrupoJSON(string psPeriodo, string psMateria, string psGrupo, string psAula, string psHoraInicio, string psHoraFin)
		{
			try
			{
				string lsMensaje;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				System.Data.SqlClient.SqlDataReader loReader;


				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@materia", psMateria));
					loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
					loParametros.Add(new ParametrosSQL("@aula", psAula));
					loParametros.Add(new ParametrosSQL("@hora_inicial", psHoraInicio));
					loParametros.Add(new ParametrosSQL("@hora_final", psHoraFin));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_dep_elimina_horario_grupo", loParametros);
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
					if (lsMensaje.Trim() == "DELETE")
					{
						ViewData["Mensaje"] = lsMensaje;
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
					}
					else
					{
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
					}
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("NE",false);
			}
		}


		public PartialViewResult GruposParalelos()
		{
			try
			{
				string lsPeriodo;
				DataTable loDt = new DataTable();
				DataTable loTabla = new DataTable();
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				ViewData["PeriodoDesc"] = lsPeriodo.RegresaDescripcionPeriodo();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_carreras_ret_esp"));
					loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
					loTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_tabla_paralelos", loParametros));
					ViewData["objeto"] = loDt;
					ViewData["periodo"] = lsPeriodo;
					ViewData["Tabla"] = loTabla;
					ViewData["Titulo"] = "Grupos Paralelos";

				}
				return PartialView("GruposParalelos");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

		public PartialViewResult VistaSeleccionCarreraParalelo(string psDiv)
		{
			try
			{
				DataTable loDt = new DataTable();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_carreras_seleccion"));
					ViewData["lista"] = loDt;
				}
				ViewData["Div"] = psDiv;
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}

		}

		public PartialViewResult ReticulaParalelo(string psPeriodo, string psCarreraReticula, string psEspecialidad, string psDiv)
		{
			try
			{
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string[] subs = psCarreraReticula.Split('_');
					string lsCarrera = subs[0];
					string lsReticula = subs[1];
					using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
					{
						loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
						loParametros.Add(new ParametrosSQL("@carrera", lsCarrera));
						loParametros.Add(new ParametrosSQL("@reticula", lsReticula));
						loParametros.Add(new ParametrosSQL("@especialidad", psEspecialidad));
						loParametros.Add(new ParametrosSQL("@divid", psDiv));
						loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_reticula_grupos", loParametros));
						ViewData["DatosSeleccion"] = loDt;
					}
				ViewData["DivAct"] = psDiv;
					return PartialView("ReticulaParalelo");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

		public PartialViewResult GrupoExistenteParalelo(string psPeriodo, string psCarrera, string psReticula, string psMateria, string psEspecialidad, string psDiv)
		{
			try
			{
				DataTable ldtTabla = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@carrera", psCarrera));
					loParametros.Add(new ParametrosSQL("@reticula", psReticula));
					loParametros.Add(new ParametrosSQL("@materia", psMateria));
					loParametros.Add(new ParametrosSQL("@especialidad", psEspecialidad));
					loParametros.Add(new ParametrosSQL("@divid", psDiv));
					ldtTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_horario_grupos_paralelo", loParametros));
					ViewData["Tabla"] = ldtTabla;
					ViewData["Titulo"] = "Grupos";
					ViewData["Periodo"] = psPeriodo;
					ViewData["Materia"] = psMateria;
					ViewData["Carrera"] = psCarrera;
					ViewData["Reticula"] = psReticula;
					ViewData["Especialidad"] = psEspecialidad;
					ViewData["DivAct"] = psDiv;
					return PartialView("GrupoExistenteParalelo");
				}

			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

		public PartialViewResult AsignaGrupoExistenteParalelo(string psPeriodo, string psMateria, string psGrupo, string psCarrera, string psReticula, string psEspecialidad, string psDiv)
		{
			try
			{
				DataTable loDt2 = new DataTable();
				DataTable psDatosAula = new DataTable();
				DataTable psDatosGrupo = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@materia", psMateria));
					loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
					loDt2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_datos_grupo_horario", loParametros));
					loParametros.Add(new ParametrosSQL("@carrera", psCarrera));
					loParametros.Add(new ParametrosSQL("@reticula", psReticula));
					loParametros.Add(new ParametrosSQL("@especialidad", psEspecialidad));
					psDatosGrupo.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_datos_grupo_existente_paralelo", loParametros));
					ViewData["DatosSeleccion"] = psDatosGrupo;
					AppointmentData loHorarioGrupoMateria;
					loHorarioGrupoMateria = new AppointmentData();
					ViewData["DivAct"] = psDiv;
					ViewBag.datasource = loHorarioGrupoMateria.RegresaHorarioGrupoMateria(loDt2);
				}
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

		public PartialViewResult CreaGrupoParalelo(string psPeriodo, string psCarrera, string psReticula, string psMateria, string psEspecialidad, string psDiv)
		{
			try
			{
				DataTable ldtTabla = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@materia", psMateria));
					loParametros.Add(new ParametrosSQL("@carrera", psCarrera));
					loParametros.Add(new ParametrosSQL("@reticula", psReticula));
					loParametros.Add(new ParametrosSQL("@especialidad", psEspecialidad)); 
					ldtTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_datos_paralelo", loParametros));
					ViewData["DatosSeleccion"] = ldtTabla;
					ViewData["DivAct"] = psDiv;
					return PartialView("CreaGrupoParalelo");
				}

			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult RegistraParaleloJSON(string psPeriodoExistente, string psCarreraExistente, int psReticulaExistente, string psMateriaExistente, string psGrupoExistente, string psRFCExistente, string psPeriodoParalelo, string psCarreraParalelo, int psReticulaParalelo, string psMateriaParalelo, string psGrupoParalelo, int psCapacidadParalelo)
		{
			try
			{
				string lsMensaje;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				System.Data.SqlClient.SqlDataReader loReader;


				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					loParametros.Add(new ParametrosSQL("@periodo_existente", psPeriodoExistente));
					loParametros.Add(new ParametrosSQL("@carrera_existente", psCarreraExistente));
					loParametros.Add(new ParametrosSQL("@reticula_existente", psReticulaExistente));
					loParametros.Add(new ParametrosSQL("@materia_existente", psMateriaExistente));
					loParametros.Add(new ParametrosSQL("@grupo_existente", psGrupoExistente));
					loParametros.Add(new ParametrosSQL("@rfc_existente", psRFCExistente));
					loParametros.Add(new ParametrosSQL("@periodo_paralelo", psPeriodoParalelo));
					loParametros.Add(new ParametrosSQL("@carrera_paralelo", psCarreraParalelo));
					loParametros.Add(new ParametrosSQL("@reticula_paralelo", psReticulaParalelo));
					loParametros.Add(new ParametrosSQL("@materia_paralelo", psMateriaParalelo));
					loParametros.Add(new ParametrosSQL("@grupo_paralelo", psGrupoParalelo));
					loParametros.Add(new ParametrosSQL("@capacidad_paralelo", psCapacidadParalelo));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_dep_crea_paralelo", loParametros);
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
						ViewData["Mensaje"] = lsMensaje;
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
					}
					else
					{
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje,false);
					}
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("NE", false);
			}

		}

		public PartialViewResult AdministracionAulas()
		{
			return PartialView();

		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult RegresaAula(string psAula)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					Aulas loDatos;
					loDatos = new Aulas(SesionSapei.Sistema);

					DataTable loDt;

					loDt = loDatos.RegresaDatosAula(psAula);

					if (loDt.Rows.Count <= 0)
					{

						return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();

					}
					else
					{
						return ManejoMensajesJson.RegresaJsonTabla(loDt);
					}
				}
			}

			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}


		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult GuardaAula(string psAula, string psUbicacion, int psCapacidad, string psObservaciones, string psPerCruce, string psEstatus)
		{

			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					Aulas loAulas;
					loAulas = new Aulas(SesionSapei.Sistema);
					loAulas.Cargar(psAula);
					if (loAulas.EOF)
						loAulas.aula = psAula;
					loAulas.ubicacion = psUbicacion;
					loAulas.capacidad_aula = psCapacidad;
					loAulas.observaciones = psObservaciones;
					loAulas.permite_cruce = psPerCruce;
					loAulas.estatus = psEstatus;
					loAulas.Guardar();
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}


		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public JsonResult EliminarAula(string psAula)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					Aulas loAulas = new Aulas(SesionSapei.Sistema);
					loAulas.Cargar(psAula);
					loAulas.Eliminar();
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
				}
			}

			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}


        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
        public PartialViewResult ListaGruposCarrera()
        {
            try
            {
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                DataTable loDt = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_aca_carrera_depto", loParametros));
                    DataRow loRw = loDt.NewRow();
                    loRw[0] = "0_0";
                    loRw[1] = "Grupos Sin Carrera";
                    loDt.Rows.InsertAt(loRw,0);
                    ViewData["datos"] = loDt;
                }
                ViewData["cboPeriodos"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual.RegresaComboPeriodos(lsPeriodo, 4, false, true);
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
        public PartialViewResult GruposCarreras(string psCarreraReticula, string psPeriodo)
        {
            try
            {
                string[] subs = psCarreraReticula.Split('_');
                string lsCarrera = subs[0];
                string lsReticula = subs[1];
                
                DataTable loDt = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loParametros.Add(new ParametrosSQL("@carrera", lsCarrera));
                    loParametros.Add(new ParametrosSQL("@reticula", lsReticula));
                    ViewData["Titulo"] = "Lista de Grupos";
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_horario_grupos_carrera", loParametros));
                    ViewData["Tabla"] = loDt;
                }
                ViewData["Acciones"] = "ocultar_buscar();";
                return PartialView("../Generales/TablaGeneral");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        #endregion

    }
}
