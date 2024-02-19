using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

using System.Web.Mvc;
using appSapei.Clases;
using Sapei.Framework.Utilerias.ExpedienteDigital;
using System.Text;

namespace appSapei.Controllers
{
	public class ServiciosEscolaresController : Controller
	{
		#region Aspirantes 2 Estudiantes
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult Aspirante2Estudiante()
		{
			try
			{
				ManejoCarpetas loCarpetas = new ManejoCarpetas(SesionSapei.Sistema);

				if (!loCarpetas.CrearCarpetasRaiz(SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano,Sapei.Framework.Configuracion.enmSistema.SAPEI))
					ViewData["Mensaje"] = "No ha sido posible crear las rutas para el expediente digital, pongase en contacto con Centro de Cómputo";
				else
					ViewData["Mensaje"] = "Se han creado las rutas para el expediente digital";

				return PartialView("Aspirante2Estudiante");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult CargaTablaAspirantesEstudiantes(string psPeriodo)
		{
			try
			{
				DataTable loDt;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					ServiciosEscolares loConsulta;
					loConsulta = new ServiciosEscolares(SesionSapei.Sistema);
					loDt = loConsulta.CargarAspirantes2Estudiantes(psPeriodo);
					ViewData["Titulo"] = "Relación Aspirantes-Estudiantes Encontrados";
					ViewData["Tabla"] = loDt;
					ViewData["Encabezados"] = new List<string> { "Folio", "Vuelta", "Nombre", "Carrera", "No. Control","NIP" };
					return PartialView("../Generales/TablaGeneral");
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public JsonResult ProcesaAspirantes2Estudiantes(string psPeriodo)
		{
			string lsMensaje = "";

			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_aspirantes2estudiantes", loParametros);
					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
				}

				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		#endregion
		#region Asignacion Materias por Bloque
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult MateriasPorBloque(string psPeriodo = null, bool pbSoloTabla = false)
		{
			try
			{
				Aspirante loDato;
				Grupos loGrupos;
				Alumno loAlumnos;
				string lsPeriodo;
				string lsDescripcion;
				DataTable loTabla;
				loDato = new Aspirante(SesionSapei.Sistema);
				loGrupos = new Grupos(SesionSapei.Sistema);
				loAlumnos = new Alumno(SesionSapei.Sistema);
				if (psPeriodo == null)
					lsPeriodo = loDato.RegresaPeriodoActivo();
				else
					lsPeriodo = psPeriodo;
				ViewData["periodo"] = lsPeriodo;
				lsDescripcion = lsPeriodo.RegresaDescripcionPeriodo();
				if (!pbSoloTabla)
				{
					loTabla = loAlumnos.RegresaListaNuevosEstudiantes(lsPeriodo);
					if (loTabla.Rows.Count == 0)
					{
						ViewData["Mensaje"] = "No hay nuevos estudiantes para registrar en el periodo " + lsDescripcion;
						return PartialView("../Generales/AvisosGenerales");
					}
					ViewData["descPeriodo"] = lsDescripcion;
					ViewData["txtEstudiantes"] = loTabla;
				}
				loTabla = loGrupos.RegresaGruposPrimero(lsPeriodo);
				ViewData["grupos"] = loTabla;
				ViewData["Tabla"] = loTabla;
				ViewData["Titulo"] = "Grupos registrados";
				ViewData["Encabezados"] = new List<string> { "Grupo", "Inscritos", "Carrera" };
				if (pbSoloTabla)
					return PartialView("../Generales/TablaGeneral");
				return PartialView("MateriasPorBloque");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public JsonResult MateriasPorBloqueJson(string psPeriodo, string psControl, string psGrupo)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
					loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_carga_por_bloque", loParametros);
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
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult ReporteAspirantes2AlumnosInscritos(string psPeriodo)
		{
			try
			{
				ReportesGenerales loReporte;
				string lsPeriodo;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				if (string.IsNullOrEmpty(psPeriodo))
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				else
					lsPeriodo = psPeriodo;
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Aspirantes"), "Aspirantes2EstudiantesInscritos.rdlc");
				loReporte.RutaReportes = path;
				ViewData["periodo"] = lsPeriodo;
				ViewData["desc_periodo"] = lsPeriodo.RegresaDescripcionPeriodo();
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaReporteAspirantes2AlumnosInscritos(lsPeriodo));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("ReporteAspirantes2AlumnosInscritos");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al generar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}

		#endregion
		#region Estudiantes
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult CambiaNoControl()
		{
			try
			{
				return PartialView("CambiaNoControl");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public JsonResult CambiaNoControlJson(string psNoControl, string psNoControlNuevo)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
					loParametros.Add(new ParametrosSQL("@no_de_control_nuevo", psNoControlNuevo));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_cambia_no_control", loParametros);
					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
					if (string.IsNullOrEmpty(lsMensaje))
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult RegresaQR(string psControl = null)
		{
			try
			{
				Alumno loAlumno;
				ViewData["DatosQR"] = null;
				if (!string.IsNullOrEmpty(psControl))
				{
					loAlumno = new Alumno(SesionSapei.Sistema);
					ViewData["DatosQR"] = loAlumno.RegresaQR(psControl);
					ViewData["NoControl"] = psControl;
				}
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		#endregion
		#region Titulados
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult AlumnosTitulados()
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					DataTable loPeriodos_EscolaresT;
					DataTable loTipos_TitulacionT;
					Periodos_Escolares loPeriodos_Escolares;
					Tipos_Titulaciones loTipos_Titulacion;
					Alumnos_Titulado loAlumnos_Titulados;
					loPeriodos_Escolares = new Periodos_Escolares(SesionSapei.Sistema);
					loTipos_Titulacion = new Tipos_Titulaciones(SesionSapei.Sistema);
					loAlumnos_Titulados = new Alumnos_Titulado(SesionSapei.Sistema);
					loPeriodos_EscolaresT = loPeriodos_Escolares.RegresaComboPeriodo();
					loTipos_TitulacionT = loTipos_Titulacion.RegresaSelectTiposTitulacion();
					ViewData["cboPeriodoEscolar"] = loPeriodos_EscolaresT;
					ViewData["cboTiposTitulacion"] = loTipos_TitulacionT;
					ViewData["Titulo"] = "Lista de Alumnos Titulados";
					ViewData["Encabezados"] = new List<string> { ".", "Numero de control", "Nombre del estudiante", "CURP", "Carrera", "Edad", "Periodo de ingreso", "Periodo de Egreso", "Genero", "Semestre de titulacion", "Fecha de protocolo", "Tipo de Titulación" };
					ViewData["Tabla"] = loAlumnos_Titulados.ConsultaTitulados();

					return PartialView("AlumnosTitulados");
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}


		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public JsonResult CargaTitulados(string psNoControl)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					Alumnos_Titulado loTipos_TitulacionesT;
					DataTable loDatos;
					loTipos_TitulacionesT = new Alumnos_Titulado(SesionSapei.Sistema);
					loDatos = loTipos_TitulacionesT.Alumnos_Titulados(psNoControl);
					return ManejoMensajesJson.RegresaJsonTabla(loDatos);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}

		public JsonResult ManejoTitulados(string psNoControl, string psPeriodo, string psFechaActo, int psTipoTitulacion, string psNoCedula, string psFechaCedula)
		{

			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					Alumnos_Titulado loAlumnos_TituladosT;
					string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
					loAlumnos_TituladosT = new Alumnos_Titulado(SesionSapei.Sistema);
					loAlumnos_TituladosT.Cargar(psNoControl);
					if (loAlumnos_TituladosT.EOF)
						loAlumnos_TituladosT.no_de_control = psNoControl;
					loAlumnos_TituladosT.periodo_titulacion = psPeriodo;
					loAlumnos_TituladosT.fecha_acto = Convert.ToDateTime(psFechaActo);
					loAlumnos_TituladosT.id_tipo = psTipoTitulacion;
					loAlumnos_TituladosT.usuario = psUsuario;
					loAlumnos_TituladosT.fecha_modificacion = DateTime.Today;
					loAlumnos_TituladosT.Guardar();
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
		#region NSS
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult LoteNSS()
		{
			Alumno loAlumnos;
			loAlumnos = new Alumno(SesionSapei.Sistema);
			ViewData["Titulo"] = "Lista de Alumnos Titulados";
			ViewData["Encabezados"] = new List<string> { "Numero de control", "Nombre del estudiante", "Carrera", "Semestre", "NSS", "Lote" };
			//ViewData["Tabla"] = loAlumnos.RegresaloteNSS();
			return PartialView("LoteNSS");
		}
		#endregion
		#region Creditos Complementarios
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
		public PartialViewResult LiberacionCreditosComplementarios(string psControl)
		{
			try
			{
				if (string.IsNullOrEmpty(psControl))
				{
					return PartialView();
				}
				Creditos_Complementarios loCreditos = new Creditos_Complementarios(SesionSapei.Sistema);
				ViewData["DatosEstudiante"] = loCreditos.RegresaDatosActividadesComplementarias(psControl,true);
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public JsonResult LiberacionCreditosComplementariosJson(string psControl)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
					loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_creditos_complementarios_liberacion", loParametros);
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

		#endregion
		#region Documentos Oficiales
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult Certificados()
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
        #endregion
        #region Cierre de Semestre
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
        public PartialViewResult ModificaPeriodo()
        {
            try
            {
                DataTable loDt = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_periodo_lista", loParametros));
                }

				ViewData["Periodos"] = loDt;


                return PartialView();

            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult MantoPeriodosEscolares(string psPeriodo = null)
		{
			try
			{
				if (psPeriodo == null)
				{
					DateTime loYear = DateTime.Now;
					string loAño = loYear.Year.ToString();

					ViewData["año"] = loAño;

					return PartialView();

				}
				else {
                    DataTable loDt = new DataTable();
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {
                        loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_periodo_datos", loParametros));
                    }

                    ViewData["año"] = loDt.Rows[0].Field<string>("periodo").SubString(0,4);

                    ViewData["Datos"] = loDt;

                    return PartialView();
                }
            }
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}


        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
        public JsonResult RegistraPeriodoJSON(string psPeriodo, string psStatus, string psFechaInicio, string psFechaTermino, string psCierreHorarios, string psCierreSeleccion, string psNumDiasClase, string psInicioVacacional, string psTerminoVacacional, string psInicioVacacionalSS, string psTerminoVacacionalSS, string psInicioEncEst, string psFinEncEst, string psInicioSeleAlumnos, string psFinSeleAlumnos, string psInicioCalDocentes, string psFinCalDocentes)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    System.Data.SqlClient.SqlDataReader loReader;
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    string lsMensaje = "";
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loParametros.Add(new ParametrosSQL("@status", psStatus));
                    loParametros.Add(new ParametrosSQL("@fecha_inicio", psFechaInicio));
                    loParametros.Add(new ParametrosSQL("@fecha_termino", psFechaTermino));
                    loParametros.Add(new ParametrosSQL("@cierre_horarios", psCierreHorarios));
                    loParametros.Add(new ParametrosSQL("@cierre_seleccion", psCierreSeleccion));
                    loParametros.Add(new ParametrosSQL("@num_dias_clase", psNumDiasClase));
                    loParametros.Add(new ParametrosSQL("@inicio_vacacional", psInicioVacacional));
                    loParametros.Add(new ParametrosSQL("@termino_vacacional", psTerminoVacacional));
                    loParametros.Add(new ParametrosSQL("@inicio_vacacional_ss", psInicioVacacionalSS));
                    loParametros.Add(new ParametrosSQL("@termino_vacacional_ss", psTerminoVacacionalSS));
                    loParametros.Add(new ParametrosSQL("@inicio_enc_estudiantil", psInicioEncEst));
                    loParametros.Add(new ParametrosSQL("@fin_enc_estudiantil", psFinEncEst));
                    loParametros.Add(new ParametrosSQL("@inicio_sele_alumnos", psInicioSeleAlumnos));
                    loParametros.Add(new ParametrosSQL("@fin_sele_alumnos", psFinSeleAlumnos));
                    loParametros.Add(new ParametrosSQL("@inicio_cal_docentes", psInicioCalDocentes));
                    loParametros.Add(new ParametrosSQL("@fin_cal_docentes", psFinCalDocentes));

                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_periodos_escolares", loParametros);
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

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult CierreSemestre()
		{
			try
			{
				Periodos_Escolares_Cierre loPeriodo;
				Periodos_Escolares loPerEsc;
				loPerEsc = new Periodos_Escolares(SesionSapei.Sistema);
				loPeriodo = new Periodos_Escolares_Cierre(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				loPerEsc.Cargar(lsPeriodo);
				loPeriodo.Cargar(lsPeriodo);
				if (loPeriodo.EOF)
				{
					loPeriodo.periodo = lsPeriodo;
				}
				loPeriodo.usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loPeriodo.fecha = DateTime.Now;
				loPeriodo.mensaje = "";
				loPeriodo.Guardar();
				CargaDatosCierre(loPeriodo);

				ViewData["periodo"] = lsPeriodo;
				ViewData["descPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public void IniciaCierreSemestre()
		{
			try
			{
				Periodos_Escolares_Cierre loPeriodo;
				loPeriodo = new Periodos_Escolares_Cierre(SesionSapei.Sistema);
				loPeriodo.IniciaProceso();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult CargaDatosCierreSemestre()
		{
			try
			{
				Periodos_Escolares_Cierre loPeriodo;
				loPeriodo = new Periodos_Escolares_Cierre(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				loPeriodo.Cargar(lsPeriodo);
				CargaDatosCierre(loPeriodo);
				return PartialView("CierreSemestreDatos");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		private void CargaDatosCierre(Periodos_Escolares_Cierre poPeriodo)
		{
			int liProcesados;
			liProcesados = poPeriodo.paso1_procesados;
			if (liProcesados == 0)
				liProcesados = 1;
			ViewData["iniciado"] = poPeriodo.paso1_historia_alumnos;
			ViewData["en_proceso"] = poPeriodo.paso1_historia_alumnos & !poPeriodo.paso4_cierre;
			ViewData["cancelado"] = string.IsNullOrEmpty(poPeriodo.mensaje);
			ViewData["mensaje"] = poPeriodo.mensaje;
			ViewData["paso1_estado"] = RegresaEstadoProceso(poPeriodo.paso1_procesados, liProcesados);
			ViewData["paso2_estado"] = RegresaEstadoProceso(poPeriodo.paso2_procesados, liProcesados);
			ViewData["paso3_estado"] = RegresaEstadoProceso(poPeriodo.paso3_procesados, liProcesados);
			if (poPeriodo.paso4_cierre)
				ViewData["paso4_estado"] = "TERMINADO";
			else
				ViewData["paso4_estado"] = "PENDIENTE";
			ViewData["paso1_avance"] = ((float)poPeriodo.paso1_procesados / (float)(1 + poPeriodo.paso1_procesados)) * 100;

			ViewData["paso2_avance"] = ((float)poPeriodo.paso2_procesados / (float)liProcesados) * 100;
			ViewData["paso3_avance"] = ((float)poPeriodo.paso3_procesados / (float)liProcesados) * 100;
			if (poPeriodo.paso3_procesados == liProcesados)
				ViewData["paso4_avance"] = 100;
			else
				ViewData["paso4_avance"] = 0;
		}
		private string RegresaEstadoProceso(int piAvance, int piTotal)
		{
			if (piAvance == 0)
				return "PENDIENTE";
			else if (piAvance == piTotal)
				return "TERMINADO";
			return "PROCESANDO";
		}
		#endregion
		#region Servicio Social
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult LiberacionServicioSocial(string psControl)
		{
			try
			{
				SS_liberados loCreditos = new SS_liberados(SesionSapei.Sistema);
				ViewData["Titulo"] = "Creditos Registrados";
				ViewData["Encabezados"] = new List<string> { "No. Control", "Nombre", "Desempeño", "Liberado en DGTV", "Liberado en DSE", "Acciones" };
				if (string.IsNullOrEmpty(psControl))
				{
					ViewData["Tabla"] = null;
					return PartialView();
				}
				ViewData["Tabla"] = loCreditos.RegresaTablaEstudiantesSS(psControl);

				return PartialView("../Generales/TablaGeneral");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public JsonResult LiberacionServicioSocialJson(string psControl)
		{
			try
			{
				SS_liberados loCreditos = new SS_liberados(SesionSapei.Sistema);
				Alumno loAlumno = new Alumno(SesionSapei.Sistema);
				loCreditos.Cargar(psControl);
				if (loCreditos.EOF)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No ha sido liberado por el DGTyV", true);
				}
				loCreditos.validado = true;
				loCreditos.usuario_se = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loCreditos.Guardar();
				loAlumno.Cargar(psControl);
				loAlumno.creditos_aprobados = loAlumno.creditos_aprobados + 10;
				loAlumno.creditos_cursados = loAlumno.creditos_cursados + 10;
				loAlumno.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Liberación exitosa", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}

		#endregion
		#region Mantenimiento
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult Reticulas()
		{
			try
			{
				ViewData["cboNivEscolar"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("nivel_escolar", "nivel_escolar", "descripcion_nivel", "", "", false, 0, null, true);
				ViewData["cboTipoMateria"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("tipo_materia", "tipo_materia", "nombre_tipo", "", "", false, 0, null, true);
				ViewData["cboClaveArea"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("organigrama", "clave_area", "descripcion_area", "descripcion_area LIKE 'DEPARTAMENTO%' AND area_depende = 110000 AND nivel  = 4", "", false, 0, null, true);
				ViewData["cboClaveMateria"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("materias", "materia", "nombre_completo_materia+'-'+materia", "", "materia", false, 0, null, true);
				ViewData["cboCarreras"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("carreras", "siglas", "nombre_carrera+' - '+siglas","","carrera",false,0,null,true);
				ViewData["cboEspecialidades"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("especialidades", "especialidad", "nombre_especialidad + ' - ' + especialidad", "", "", false, 0, null, true);
				ViewData["cboCarrerasEspecialidad"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("carreras", "reticula", "nombre_carrera+' - '+siglas", "", "carrera", false, 0, null, true);

				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult vistaReticula()
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

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public JsonResult AgregarMateriaJson(string clave_materia, string clave_area, string nivel_escolar, int tipo_materia, string nombre_materia, string nombre_abreviado_materia)
		{
			try
			{

				Materias loMateria = new Materias(SesionSapei.Sistema);
				loMateria.Cargar(clave_materia);
				if (!loMateria.EOF)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Ya existe esta materia", false);
				}
				loMateria.materia = clave_materia;
				loMateria.clave_area = clave_area;
				loMateria.nombre_completo_materia = nombre_materia;
				loMateria.nombre_abreviado_materia = nombre_abreviado_materia;
				loMateria.tipo_materia = tipo_materia;
				loMateria.nivel_escolar = nivel_escolar;

				loMateria.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se ha guardado la materia exitosamente", true);

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public JsonResult BuscarMateriaJson(string materia)
		{
			try
			{
				Materias loMateria = new Materias(SesionSapei.Sistema);
				loMateria.Cargar(materia);
				if (loMateria.EOF)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
				return ManejoMensajesJson.RegresaJsonObjeto(loMateria);

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		public JsonResult ModificarMateriaJson(string clave_materia, string clave_area, string nivel_escolar, int tipo_materia, string nombre_materia, string nombre_abreviado_materia)
		{

				try
				{
					using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
					{
						System.Data.SqlClient.SqlDataReader loReader;
						List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
						string lsMensaje = "";
						loParametros.Add(new ParametrosSQL("@materia", clave_materia));
						loParametros.Add(new ParametrosSQL("@clave_area", clave_area));
						loParametros.Add(new ParametrosSQL("@nvl_esc", nivel_escolar));
						loParametros.Add(new ParametrosSQL("@tipo_materia", tipo_materia));
						loParametros.Add(new ParametrosSQL("@nombre_materia", nombre_materia));
						loParametros.Add(new ParametrosSQL("@nombre_abrv_materia", nombre_abreviado_materia));
						loParametros.Add(new ParametrosSQL("@bandera", '1'));
		
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
		public JsonResult EliminarMateriaJson(string clave_materia)
		{

			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@materia", clave_materia));
					loParametros.Add(new ParametrosSQL("@clave_area", ""));
					loParametros.Add(new ParametrosSQL("@nvl_esc", ""));
					loParametros.Add(new ParametrosSQL("@tipo_materia", ""));
					loParametros.Add(new ParametrosSQL("@nombre_materia", ""));
					loParametros.Add(new ParametrosSQL("@nombre_abrv_materia", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '2')); 

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

		public JsonResult AgregarCarreraJson(string carrera, int reticula, string nivel_escolar, string clave_oficial, string nombre_carrera, string nombre_reducido, string siglas, int carga_maxima, int carga_minima, string fecha_inicio, string fecha_termino, string clave_cosnet, int creditos_totales, string id_area_carr, string id_sub_area_carr, string id_nivel_carr, string consecutivo_carrera, string nivel,  string clave, string modalidad)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", carrera));
					loParametros.Add(new ParametrosSQL("@reticula", ""));
					loParametros.Add(new ParametrosSQL("@nivel_escolar", ""));
					loParametros.Add(new ParametrosSQL("@clave_oficial", ""));
					loParametros.Add(new ParametrosSQL("@nombre_carrera", ""));
					loParametros.Add(new ParametrosSQL("@nombre_reducido", ""));
					loParametros.Add(new ParametrosSQL("@siglas", ""));
					loParametros.Add(new ParametrosSQL("@carga_maxima", ""));
					loParametros.Add(new ParametrosSQL("@carga_minima", ""));
					loParametros.Add(new ParametrosSQL("@fecha_inicio", ""));
					loParametros.Add(new ParametrosSQL("@fecha_termino", ""));
					loParametros.Add(new ParametrosSQL("@creditos_totales", ""));
					loParametros.Add(new ParametrosSQL("@clave", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '1'));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_carreras_reticulas", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							reticula = loReader.GetInt32(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
				}


				Carreras loCarreras = new Carreras(SesionSapei.Sistema);
				loCarreras.Cargar(carrera,reticula);
				if (!loCarreras.EOF)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Ya existe esta carrera", false);
				}
				loCarreras.carrera = carrera;
				loCarreras.reticula = reticula;
				loCarreras.nivel_escolar = nivel_escolar;
				loCarreras.clave_oficial = clave_oficial;
				loCarreras.nombre_carrera = nombre_carrera;
				loCarreras.nombre_reducido = nombre_reducido;
				loCarreras.siglas = siglas;
				loCarreras.carga_maxima = carga_maxima;
				loCarreras.carga_minima = carga_minima;
				loCarreras.fecha_inicio = fecha_inicio;
				loCarreras.fecha_termino = fecha_termino;
				loCarreras.clave_cosnet = clave_cosnet;
				loCarreras.creditos_totales = creditos_totales;
				loCarreras.id_area_carr = id_area_carr;
				loCarreras.id_sub_area_carr = id_sub_area_carr;
				loCarreras.id_nivel_carr = id_nivel_carr;
				loCarreras.consecutivo_carrera = consecutivo_carrera;
				loCarreras.nivel = nivel;
				loCarreras.clave = clave;
				loCarreras.modalidad = modalidad;
				loCarreras.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se ha guardado la carrera exitosamente", true);

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		public JsonResult AgregarCarreraAbrvJson(string carrera)
		{

			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", carrera));
					loParametros.Add(new ParametrosSQL("@reticula", ""));
					loParametros.Add(new ParametrosSQL("@nivel_escolar", ""));
					loParametros.Add(new ParametrosSQL("@clave_oficial", ""));
					loParametros.Add(new ParametrosSQL("@nombre_carrera", ""));
					loParametros.Add(new ParametrosSQL("@nombre_reducido", ""));
					loParametros.Add(new ParametrosSQL("@siglas", ""));
					loParametros.Add(new ParametrosSQL("@carga_maxima", ""));
					loParametros.Add(new ParametrosSQL("@carga_minima", ""));
					loParametros.Add(new ParametrosSQL("@fecha_inicio", ""));
					loParametros.Add(new ParametrosSQL("@fecha_termino", ""));
					loParametros.Add(new ParametrosSQL("@creditos_totales", ""));
					loParametros.Add(new ParametrosSQL("@clave", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '2'));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_carreras_reticulas", loParametros);

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
		public JsonResult BuscarCarreraJson(string carrera, string siglas, int reticula)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", siglas));
					loParametros.Add(new ParametrosSQL("@reticula", ""));
					loParametros.Add(new ParametrosSQL("@nivel_escolar", ""));
					loParametros.Add(new ParametrosSQL("@clave_oficial", ""));
					loParametros.Add(new ParametrosSQL("@nombre_carrera", ""));
					loParametros.Add(new ParametrosSQL("@nombre_reducido", ""));
					loParametros.Add(new ParametrosSQL("@siglas", ""));
					loParametros.Add(new ParametrosSQL("@carga_maxima", ""));
					loParametros.Add(new ParametrosSQL("@carga_minima", ""));
					loParametros.Add(new ParametrosSQL("@fecha_inicio", ""));
					loParametros.Add(new ParametrosSQL("@fecha_termino", ""));
					loParametros.Add(new ParametrosSQL("@creditos_totales", ""));
					loParametros.Add(new ParametrosSQL("@clave", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '3'));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_carreras_reticulas", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							reticula = loReader.GetInt32(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
				}
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", siglas));
					loParametros.Add(new ParametrosSQL("@reticula", ""));
					loParametros.Add(new ParametrosSQL("@nivel_escolar", ""));
					loParametros.Add(new ParametrosSQL("@clave_oficial", ""));
					loParametros.Add(new ParametrosSQL("@nombre_carrera", ""));
					loParametros.Add(new ParametrosSQL("@nombre_reducido", ""));
					loParametros.Add(new ParametrosSQL("@siglas", ""));
					loParametros.Add(new ParametrosSQL("@carga_maxima", ""));
					loParametros.Add(new ParametrosSQL("@carga_minima", ""));
					loParametros.Add(new ParametrosSQL("@fecha_inicio", ""));
					loParametros.Add(new ParametrosSQL("@fecha_termino", ""));
					loParametros.Add(new ParametrosSQL("@creditos_totales", ""));
					loParametros.Add(new ParametrosSQL("@clave", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '4'));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_carreras_reticulas", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							carrera = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
				}
				Carreras loCarreras= new Carreras(SesionSapei.Sistema);
				loCarreras.Cargar(carrera, reticula);
				if (loCarreras.EOF)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
				return ManejoMensajesJson.RegresaJsonObjeto(loCarreras);

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		public JsonResult ModificarCarreraJson(string carrera, int reticula, string nivel_escolar, string clave_oficial, string nombre_carrera, string nombre_reducido, string siglas, int carga_maxima, int carga_minima, string fecha_inicio, string fecha_termino, int creditos_totales, string clave)
        {

			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", siglas));
					loParametros.Add(new ParametrosSQL("@reticula", ""));
					loParametros.Add(new ParametrosSQL("@nivel_escolar", ""));
					loParametros.Add(new ParametrosSQL("@clave_oficial", ""));
					loParametros.Add(new ParametrosSQL("@nombre_carrera", ""));
					loParametros.Add(new ParametrosSQL("@nombre_reducido", ""));
					loParametros.Add(new ParametrosSQL("@siglas", ""));
					loParametros.Add(new ParametrosSQL("@carga_maxima", ""));
					loParametros.Add(new ParametrosSQL("@carga_minima", ""));
					loParametros.Add(new ParametrosSQL("@fecha_inicio", ""));
					loParametros.Add(new ParametrosSQL("@fecha_termino", ""));
					loParametros.Add(new ParametrosSQL("@creditos_totales", ""));
					loParametros.Add(new ParametrosSQL("@clave", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '3'));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_carreras_reticulas", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							reticula = loReader.GetInt32(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
				}

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", carrera));
					loParametros.Add(new ParametrosSQL("@reticula", reticula));
					loParametros.Add(new ParametrosSQL("@nivel_escolar", nivel_escolar));
					loParametros.Add(new ParametrosSQL("@clave_oficial", clave_oficial));
					loParametros.Add(new ParametrosSQL("@nombre_carrera", nombre_carrera));
					loParametros.Add(new ParametrosSQL("@nombre_reducido", nombre_reducido));
					loParametros.Add(new ParametrosSQL("@siglas", siglas));
					loParametros.Add(new ParametrosSQL("@carga_maxima", carga_maxima));
					loParametros.Add(new ParametrosSQL("@carga_minima", carga_minima));
					loParametros.Add(new ParametrosSQL("@fecha_inicio", fecha_inicio));
					loParametros.Add(new ParametrosSQL("@fecha_termino", fecha_termino));
					loParametros.Add(new ParametrosSQL("@creditos_totales", creditos_totales));
					loParametros.Add(new ParametrosSQL("@clave", clave));
					loParametros.Add(new ParametrosSQL("@bandera", '5'));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_carreras_reticulas", loParametros);

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
		public JsonResult EliminarCarreraJson(string carrera, int reticula, string siglas)
        {
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", siglas));
					loParametros.Add(new ParametrosSQL("@reticula", ""));
					loParametros.Add(new ParametrosSQL("@nivel_escolar", ""));
					loParametros.Add(new ParametrosSQL("@clave_oficial", ""));
					loParametros.Add(new ParametrosSQL("@nombre_carrera", ""));
					loParametros.Add(new ParametrosSQL("@nombre_reducido", ""));
					loParametros.Add(new ParametrosSQL("@siglas", ""));
					loParametros.Add(new ParametrosSQL("@carga_maxima", ""));
					loParametros.Add(new ParametrosSQL("@carga_minima", ""));
					loParametros.Add(new ParametrosSQL("@fecha_inicio", ""));
					loParametros.Add(new ParametrosSQL("@fecha_termino", ""));
					loParametros.Add(new ParametrosSQL("@creditos_totales", ""));
					loParametros.Add(new ParametrosSQL("@clave", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '3'));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_carreras_reticulas", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							reticula = loReader.GetInt32(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
				}

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", ""));
					loParametros.Add(new ParametrosSQL("@reticula", reticula));
					loParametros.Add(new ParametrosSQL("@nivel_escolar", ""));
					loParametros.Add(new ParametrosSQL("@clave_oficial", ""));
					loParametros.Add(new ParametrosSQL("@nombre_carrera", ""));
					loParametros.Add(new ParametrosSQL("@nombre_reducido", ""));
					loParametros.Add(new ParametrosSQL("@siglas", ""));
					loParametros.Add(new ParametrosSQL("@carga_maxima", ""));
					loParametros.Add(new ParametrosSQL("@carga_minima", ""));
					loParametros.Add(new ParametrosSQL("@fecha_inicio", ""));
					loParametros.Add(new ParametrosSQL("@fecha_termino", ""));
					loParametros.Add(new ParametrosSQL("@creditos_totales", ""));
					loParametros.Add(new ParametrosSQL("@clave", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '6'));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_carreras_reticulas", loParametros);

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

		public JsonResult AgregarEspecialidadJson(string especialidad, string carrera, int reticula, string nombre_especialidad, string periodo_inicio, string periodo_termino, int creditos_optativos, int creditos_especialidad, string clave_especialidad)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@especialidad", ""));
					loParametros.Add(new ParametrosSQL("@carrera", carrera));
					loParametros.Add(new ParametrosSQL("@nombre_especialidad", ""));
					loParametros.Add(new ParametrosSQL("@periodo_inicio", ""));
					loParametros.Add(new ParametrosSQL("@periodo_termino", ""));
					loParametros.Add(new ParametrosSQL("@creditos_optativos", ""));
					loParametros.Add(new ParametrosSQL("@creditos_especialidad", ""));
					loParametros.Add(new ParametrosSQL("@clave_especialidad", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '2'));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_especialidad_reticulas", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							reticula = loReader.GetInt32(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
				}

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@especialidad", ""));
					loParametros.Add(new ParametrosSQL("@carrera", carrera));
					loParametros.Add(new ParametrosSQL("@nombre_especialidad", ""));
					loParametros.Add(new ParametrosSQL("@periodo_inicio", ""));
					loParametros.Add(new ParametrosSQL("@periodo_termino", ""));
					loParametros.Add(new ParametrosSQL("@creditos_optativos", ""));
					loParametros.Add(new ParametrosSQL("@creditos_especialidad", ""));
					loParametros.Add(new ParametrosSQL("@clave_especialidad", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '1'));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_especialidad_reticulas", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							carrera = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
				}
				Especialidades loEspecialidades = new Especialidades(SesionSapei.Sistema);
				loEspecialidades.Cargar(especialidad);
				if (!loEspecialidades.EOF)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Ya existe esta especialidad", false);
				}
				loEspecialidades.especialidad = especialidad;
				loEspecialidades.carrera = carrera;
				loEspecialidades.reticula = reticula;
				loEspecialidades.nombre_especialidad = nombre_especialidad;
				loEspecialidades.periodo_inicio = periodo_inicio;
				loEspecialidades.periodo_termino = periodo_termino;
				loEspecialidades.creditos_optativos = creditos_optativos;
				loEspecialidades.creditos_especialidad = creditos_especialidad;
				loEspecialidades.clave_especialidad = clave_especialidad;

				loEspecialidades.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se ha guardado la especialidad exitosamente", true);

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		public JsonResult BuscarEspecialidadJson(string especialidad)
        {
            try
            {
				Especialidades loEspecialidades = new Especialidades(SesionSapei.Sistema);
				loEspecialidades.Cargar(especialidad);
                if (loEspecialidades.EOF)
                {
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                }
				return ManejoMensajesJson.RegresaJsonObjeto(loEspecialidades);
            }
            catch (Exception ex)
            {
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }
		public JsonResult ModificarEspecialidadJson(string especialidad, string carrera, string nombre_especialidad, string periodo_inicio, string periodo_termino, int creditos_optativos, int creditos_especialidad, string clave_especialidad)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@especialidad", especialidad));
					loParametros.Add(new ParametrosSQL("@carrera",carrera ));
					loParametros.Add(new ParametrosSQL("@nombre_especialidad", nombre_especialidad));
					loParametros.Add(new ParametrosSQL("@periodo_inicio", periodo_inicio));
					loParametros.Add(new ParametrosSQL("@periodo_termino", periodo_termino));
					loParametros.Add(new ParametrosSQL("@creditos_optativos", creditos_optativos));
					loParametros.Add(new ParametrosSQL("@creditos_especialidad", creditos_especialidad));
					loParametros.Add(new ParametrosSQL("@clave_especialidad", clave_especialidad));
					loParametros.Add(new ParametrosSQL("@bandera", '3'));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_especialidad_reticulas", loParametros);

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
		public JsonResult ElimnarEspecialidadJson(string especialidad)
        {
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@especialidad", especialidad));
					loParametros.Add(new ParametrosSQL("@carrera", ""));
					loParametros.Add(new ParametrosSQL("@nombre_especialidad", ""));
					loParametros.Add(new ParametrosSQL("@periodo_inicio", ""));
					loParametros.Add(new ParametrosSQL("@periodo_termino", ""));
					loParametros.Add(new ParametrosSQL("@creditos_optativos", ""));
					loParametros.Add(new ParametrosSQL("@creditos_especialidad", ""));
					loParametros.Add(new ParametrosSQL("@clave_especialidad", ""));
					loParametros.Add(new ParametrosSQL("@bandera", '4'));


					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_especialidad_reticulas", loParametros);

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


		public JsonResult ModificarOrdenReticula(string clave_materia, string clave_area, string nivel_escolar, int tipo_materia, string nombre_materia, string nombre_abreviado_materia)
		{

			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@materia", clave_materia));
					loParametros.Add(new ParametrosSQL("@clave_area", clave_area));
					loParametros.Add(new ParametrosSQL("@nvl_esc", nivel_escolar));
					loParametros.Add(new ParametrosSQL("@tipo_materia", tipo_materia));
					loParametros.Add(new ParametrosSQL("@nombre_materia", nombre_materia));
					loParametros.Add(new ParametrosSQL("@nombre_abrv_materia", nombre_abreviado_materia));
					loParametros.Add(new ParametrosSQL("@bandera", '3'));

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

		public JsonResult ModificarOrdenReticulaVacia(string clave_materia, string clave_area, string nivel_escolar, int tipo_materia, string nombre_materia, string nombre_abreviado_materia)
		{

			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@materia", clave_materia));
					loParametros.Add(new ParametrosSQL("@clave_area", clave_area));
					loParametros.Add(new ParametrosSQL("@nvl_esc", nivel_escolar));
					loParametros.Add(new ParametrosSQL("@tipo_materia", tipo_materia));
					loParametros.Add(new ParametrosSQL("@nombre_materia", nombre_materia));
					loParametros.Add(new ParametrosSQL("@nombre_abrv_materia", nombre_abreviado_materia));
					loParametros.Add(new ParametrosSQL("@bandera", '4'));

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


		#endregion
		#region Servicio
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult ControlServiciosAlumnos()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult PagoServiciosAlumnos(string psNoControl = null)
		{
			try
			{
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));

				if (string.IsNullOrEmpty(psNoControl))
				{
					loParametros.Add(new ParametrosSQL("@no_de_control", DBNull.Value));

					using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
					{
						loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_esc_alu_servicios", loParametros));
					}
					ViewData["Tabla"] = loDt;
					ViewData["Titulo"] = "Busqueda de servicios de estudiantes";
					ViewData["Modulo"] = "HistorialPagos";
					ViewData["Vista"] = "ServiciosEscolares/PagoServiciosAlumnos";
					ViewData["periodo_desc"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
					return PartialView("BuscaNoControl");
				}
				loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_esc_alu_servicios", loParametros));
					ViewData["DatosEstudiante"] = loDt;
				}
				return PartialView("PagoServiciosAlumnos");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Bienvenida");
			}
		}
		#endregion
	}

}
