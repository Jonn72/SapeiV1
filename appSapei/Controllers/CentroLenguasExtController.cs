using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
	public class CentroLenguasExtController : Controller
	{

		#region Captura Calificaciones
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult CapturaCalificacion()
		{
			try
			{
				DataTable loTabla;
				Cle_Grupos loLista;
				Periodos_Ingles loPeriodos;

				string lsPeriodoActivo;
				string lsPeriodo;
				string lsPeriodoDescripcion;
				loLista = new Cle_Grupos(SesionSapei.Sistema);
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				loPeriodos = new Periodos_Ingles(SesionSapei.Sistema);
				lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();

				lsPeriodoActivo = loPeriodos.ValidaPeriodoCapturaCalificacion(lsPeriodo, lsPeriodoDescripcion);
				if (!string.IsNullOrEmpty(lsPeriodoActivo))
				{
					ViewData["Mensaje"] = lsPeriodoActivo;
					return PartialView("../Generales/AvisosGenerales");
				}

				loTabla = loLista.RegresaListaGruposCargados(lsPeriodo);
				ViewData["Tabla"] = loTabla;
				ViewData["Titulo"] = "Lista";
				ViewData["Encabezados"] = new List<string> { "Registrado", "Nivel", "Grupo", "Capacidad", "Inscritos", "Registrados", "Altas", "Bajas" };

				ViewData["periodo"] = lsPeriodo;
				ViewData["descPeriodo"] = lsPeriodoDescripcion;
				ViewData["encabezado"] = "Listas de reinscripción a cursos de inglés";
				ViewData["nombre_reporte"] = "ListaReinscripcionIngles";
				return PartialView("CapturaCalificacion");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public ActionResult CargaCalificacionPorGrupo()
		{
			string lsTextoArchivo;
			string lsMensaje;
			Stream loSt;
			HttpPostedFileBase loFile;
			Historia_alumno_ingles loDatos;
			//Se obtiene la fecha del ultimo dia cargado a base
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loFile = Request.Files[0];
					loSt = loFile.InputStream;
					loSt.Position = 0;
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loDatos = new Historia_alumno_ingles(SesionSapei.Sistema);
					using (StreamReader reader = new StreamReader(loSt, System.Text.Encoding.UTF8))
					{
						lsTextoArchivo = reader.ReadToEnd();
					}

					if (string.IsNullOrEmpty(lsTextoArchivo))
					{
						return Json("");
					}
					lsMensaje = loDatos.GuardaRegistros(lsTextoArchivo, loFile.FileName);
					if (!string.IsNullOrEmpty(lsMensaje))
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);

					loParametros.Add(new ParametrosSQL("@periodo", loDatos.periodo));
					loParametros.Add(new ParametrosSQL("@nivel", loDatos.nivel));
					loParametros.Add(new ParametrosSQL("@grupo", loDatos.grupo));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_cle_carga_historia_alumnos", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
					if (lsMensaje.Trim().Length == 1)
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
				}
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se generó un error, pongase en contacto con Centro de Cómputo", false);
			}
		}
		#endregion
		#region Seleccion de grupos
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult RegistrarEstudianteGrupo()
		{
			try
			{
				return PartialView("RegistrarEstudianteGrupo");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult RegistrarNuevoEstudiante(string psPeriodo, bool pbSoloTabla = false)
		{
			try
			{
				Cle_Niveles loNiveles;
				Cle_Grupos loGrupos;
				DataTable loTabla;
				string lsPeriodo;
				loNiveles = new Cle_Niveles(SesionSapei.Sistema);
				loGrupos = new Cle_Grupos(SesionSapei.Sistema);
				if (string.IsNullOrEmpty(psPeriodo))
				{
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				}
				else
				{
					lsPeriodo = psPeriodo;
				}

				loTabla = loGrupos.RegresaTablaGrupos(lsPeriodo);
				ViewData["cboGruposIngles"] = loTabla;
				ViewData["Tabla"] = loTabla;
				ViewData["Titulo"] = "Grupos Registrados";
				ViewData["Encabezados"] = new List<string> { "Periodo", "Nivel", "Grupo", "Capacidad", "Inscritos", "Horario" };

				if (pbSoloTabla)
				{
					return PartialView("../Generales/TablaGeneral");
				}
				ViewData["periodo"] = lsPeriodo;
				ViewData["periodo_desc"] = lsPeriodo.RegresaDescripcionPeriodo();
				ViewData["cboNivelesIngles"] = loNiveles.RegresaTablaNiveles();
				return PartialView("RegistrarNuevoEstudiante");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult RegistrarNuevoEstudianteJson(string psPeriodo, string psControl, string psNivel, string psGrupo, bool pbActualiza)
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
					loParametros.Add(new ParametrosSQL("@nivel", psNivel));
					loParametros.Add(new ParametrosSQL("@grupo", psGrupo));

					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loParametros.Add(new ParametrosSQL("@actualiza", pbActualiza));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_cle_coloca_estudiante", loParametros);
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
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult GeneraListas(string psPeriodo)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_cle_genera_horario", loParametros);
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
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public JsonResult GuardaGrupoEstudiante(string psControl, string psGrupo)
		{
			try
			{
				string lsControl;
				string lsRedirecciona;
				lsControl = psControl;
				if (string.IsNullOrEmpty(lsControl) && SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
				{
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				}
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loParametros.Add(new ParametrosSQL("@grupo", psGrupo.Base64Decode()));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_CLE_guarda_grupo_estudiante", loParametros);
					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
					if (lsMensaje.ToUpper().Contains("ERROR"))
					{
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje.Replace("ERROR", ""), false);
					}
					if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
						lsRedirecciona = "Estudiante/DatosCursosIngles";
					else
						lsRedirecciona = "CentroLenguasExt/RegistrarEstudianteGrupo";
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsRedirecciona, true);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult ImprimirListaReinscripcion(string id)
		{
			try
			{
				DataTable loTabla;
				Cle_Lista_Seleccion loLista;

				string lsPeriodo;
				string lsPeriodoDescripcion;
				loLista = new Cle_Lista_Seleccion(SesionSapei.Sistema);
				if (string.IsNullOrEmpty(id))
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				else
					lsPeriodo = id;
				lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();

				loTabla = loLista.RegresaLista(lsPeriodo);
				ViewData["Tabla"] = loTabla;
				ViewData["Titulo"] = "Lista";
				ViewData["Encabezados"] = new List<string> { "No. Control", "Nivel", "Inicio Selección", "Fin Selección" };

				ViewData["periodo"] = lsPeriodo;
				ViewData["descPeriodo"] = lsPeriodoDescripcion;
				ViewData["encabezado"] = "Listas de reinscripción a cursos de inglés";
				ViewData["nombre_reporte"] = "ListaReinscripcionIngles";
				return PartialView("ImprimirListaReinscripcion");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Personal");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult ActivaEstudiante(string psPeriodo = null)
		{
			try
			{
				Cle_Niveles loNiveles;
				string lsPeriodo;
				if (string.IsNullOrEmpty(psPeriodo))
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				else
					lsPeriodo = psPeriodo;
				loNiveles = new Cle_Niveles(SesionSapei.Sistema);
				ViewData["periodo"] = lsPeriodo;
				ViewData["periodo_desc"] = lsPeriodo.RegresaDescripcionPeriodo();
				ViewData["cboNivelesIngles"] = loNiveles.RegresaTablaNiveles();
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult RegresaDatosEstudiante(string psControl)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					int liRes = 0;
					loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_cle_datos_estudiante", loParametros);
					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							liRes = loReader.GetInt32(0);
							lsMensaje = loReader.GetString(1);
						}
					}
					loReader.Close();
					loReader.Dispose();

					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(liRes.ToString() + "|" + lsMensaje, true);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult ActivaEstudianteJson(int piOpcion, string psControl, string psNivel, string psFecha)
		{
			try
			{
				Cle_Lista_Seleccion loSeleccion = new Cle_Lista_Seleccion(SesionSapei.Sistema);
				loSeleccion.ActivaEstudiante(piOpcion, psControl, psNivel, psFecha);
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
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult RegistrarGrupos(bool pbSoloTabla = false)
		{
			try
			{
				DataTable loTabla;
				Cle_Grupos loGrupos;
				Cle_Niveles loNiveles;
				Periodos_Ingles loPeriodos;

				string lsPeriodoActivo;
				string lsPeriodo;
				string lsPeriodoDescripcion;
				loNiveles = new Cle_Niveles(SesionSapei.Sistema);
				loGrupos = new Cle_Grupos(SesionSapei.Sistema);
				loPeriodos = new Periodos_Ingles(SesionSapei.Sistema);
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();
				lsPeriodoActivo = loPeriodos.ValidaPeriodo(lsPeriodo, lsPeriodoDescripcion);
				if (!string.IsNullOrEmpty(lsPeriodoActivo))
				{
					ViewData["Mensaje"] = lsPeriodoActivo;
					return PartialView("../Generales/AvisosGenerales");
				}

				loTabla = loGrupos.RegresaTablaGrupos(lsPeriodo);
				ViewData["Tabla"] = loTabla;
				ViewData["Titulo"] = "Grupos Registrados";
				ViewData["Encabezados"] = new List<string> { "Periodo", "Nivel", "Grupo", "Capacidad", "Inscritos", "Horario" };

				if (pbSoloTabla)
				{
					return PartialView("../Generales/TablaGeneral");
				}


				ViewData["txtPeriodo"] = lsPeriodo;
				ViewData["txtDescPeriodo"] = lsPeriodoDescripcion;
				ViewData["cboNivelesIngles"] = loNiveles.RegresaTablaNiveles();
				return PartialView("RegistrarGrupos");


			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult GuardaGrupo(string psPeriodo, string psNivel, string psGrupo, short piCapacidad, string psAula, string psHorario)
		{
			try
			{
				Cle_Grupos loGrupo;
				Cle_Horarios loHorario;
				loGrupo = new Cle_Grupos(SesionSapei.Sistema);
				loGrupo.Cargar(psPeriodo, psNivel, psGrupo);
				if (loGrupo.EOF)
				{
					loGrupo.periodo = psPeriodo;
					loGrupo.nivel = psNivel;
					loGrupo.grupo = psGrupo;
				}
				loGrupo.capacidad = piCapacidad;
				loGrupo.Guardar();

				loHorario = new Cle_Horarios(SesionSapei.Sistema);
				loHorario.aula = psAula;
				loHorario.periodo = psPeriodo;
				loHorario.nivel = psNivel;
				loHorario.grupo = psGrupo;
				loHorario.GuardaHorario(psHorario);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult EliminaGrupo(string psPeriodo, string psNivel, string psGrupo)
		{
			try
			{
				Cle_Grupos loGrupo;
				Cle_Horarios loHorario;
				loGrupo = new Cle_Grupos(SesionSapei.Sistema);
				loHorario = new Cle_Horarios(SesionSapei.Sistema);
				loHorario.periodo = psPeriodo;
				loHorario.nivel = psNivel;
				loHorario.grupo = psGrupo;
				loHorario.BorraHorarios();
				loGrupo.Cargar(psPeriodo, psNivel, psGrupo);
				loGrupo.Eliminar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult ImprimirListaGrupos()
		{
			try
			{
				DataTable loTabla;
				Cle_Grupos loGrupos;
				Cle_Niveles loNiveles;
				Periodos_Ingles loPeriodos;

				string lsPeriodoActivo;
				string lsPeriodo;
				string lsPeriodoDescripcion;
				loNiveles = new Cle_Niveles(SesionSapei.Sistema);
				loGrupos = new Cle_Grupos(SesionSapei.Sistema);
				loPeriodos = new Periodos_Ingles(SesionSapei.Sistema);
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;

				lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();
				lsPeriodoActivo = loPeriodos.ValidaPeriodoPublicacion(lsPeriodo, lsPeriodoDescripcion);
				if (!string.IsNullOrEmpty(lsPeriodoActivo))
				{
					ViewData["Mensaje"] = lsPeriodoActivo;
					return PartialView("../Generales/AvisosGenerales");
				}

				loTabla = loGrupos.RegresaTablaGruposImprimir(lsPeriodo);
				ViewData["Tabla"] = loTabla;
				ViewData["Titulo"] = "Grupos Registrados";
				ViewData["Encabezados"] = new List<string> { "Nivel", "Grupo", "Capacidad", "Horario" };



				ViewData["txtPeriodo"] = lsPeriodo;
				ViewData["txtDescPeriodo"] = lsPeriodoDescripcion;
				ViewData["cboNivelesIngles"] = loNiveles.RegresaTablaNiveles();
				return PartialView("ImprimirListaGrupos");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult ImprimirListaInscritos(string id)
		{
			try
			{
				DataTable loTabla;
				Cle_Grupos loLista;
				Periodos_Ingles loPeriodos;
				string lsPeriodo;
				string lsPeriodoDescripcion;
				loLista = new Cle_Grupos(SesionSapei.Sistema);
				loPeriodos = new Periodos_Ingles(SesionSapei.Sistema);
				if (string.IsNullOrEmpty(id))
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				else
					lsPeriodo = id;
				lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();

				loTabla = loLista.RegresaTablaGruposInscritos(lsPeriodo);
				ViewData["Tabla"] = loTabla;
				ViewData["Titulo"] = "Grupos Registrados";
				ViewData["Encabezados"] = new List<string> { "Nivel", "Grupo", "Capacidad", "Inscritos", "Descargar" };
				ViewData["periodo"] = lsPeriodo;
				ViewData["descPeriodo"] = lsPeriodoDescripcion;
				return PartialView("ImprimirListaInscritos");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		#endregion
		#region Niveles Inglés
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult RegistrarNivel()
		{
			try
			{
				DataTable loTabla;
				Cle_Niveles loNiveles;
				loNiveles = new Cle_Niveles(SesionSapei.Sistema);
				loTabla = loNiveles.RegresaTablaNiveles();
				ViewData["Tabla"] = loTabla;
				ViewData["Titulo"] = "Niveles Registrados";
				ViewData["Encabezados"] = new List<string> { "Nivel", "Seriación" };
				return PartialView("RegistrarNivel");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult GuardaNivel(string psNivel, short piSerie)
		{
			try
			{
				Cle_Niveles loNivel;
				loNivel = new Cle_Niveles(SesionSapei.Sistema);
				loNivel.Cargar(psNivel);
				if (loNivel.EOF)
				{
					loNivel.nivel = psNivel;
				}
				loNivel.serie = piSerie;
				loNivel.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		#endregion
		#region Pagos
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult ActivaDesactivaPago(string psControl = null)
		{
			try
			{
				string lsPeriodo;
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				ViewData["periodo"] = lsPeriodo;
				ViewData["periodo_desc"] = lsPeriodo.RegresaDescripcionPeriodo();
				if (!string.IsNullOrEmpty(psControl))
				{
					ProcedimientosCLE loCLE = new ProcedimientosCLE(SesionSapei.Sistema);
					ViewData["DatosEstudiante"] = loCLE.RegresaRegistroPagoCLE(lsPeriodo, psControl);
				}
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult EliminaActivaPagoJson(string psControl, bool pbTipo)
		{
			try
			{
				ProcedimientosCLE loCLE = new ProcedimientosCLE(SesionSapei.Sistema);
				loCLE.EliminaRegistraPagoCLE(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual, psControl, pbTipo);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		#endregion
		#region Liberacion
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult LiberacionIngles()
		{
			try
			{
				Cle_Liberacion_Ingles loLiberacion = new Cle_Liberacion_Ingles(SesionSapei.Sistema); 
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					ViewData["Tabla"] = loLiberacion.RegresaAlumnosLiberado();
					ViewData["Titulo"] = "Liberación de Inglés";
					ViewData["Modulo"] = "LiberacionIngles";
					ViewData["Vista"] = "CentroLenguasExt/LiberacionIngles";
					return PartialView("BuscaNoControl");
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult LiberacionIngles(string psNoControl)
		{
			try
			{
				Cle_Liberacion_Ingles loLiberacion = new Cle_Liberacion_Ingles(SesionSapei.Sistema);
				ViewData["cboTipo_liberacion"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cle_tipos_liberacion", "id_liberacion", "tipo_liberacion",true);
				ViewData["Datos"] = loLiberacion.RegresaAlumnosLiberado(psNoControl);
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("../Personal/Index");
			}

		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult LiberacionInglesJson(string psNoControl, Int16 piTipoLiberacion, int piPromedio)
		{

			try
			{
				Cle_Liberacion_Ingles loLiberacion = new Cle_Liberacion_Ingles(SesionSapei.Sistema);
				loLiberacion.RegistraLiberacion(psNoControl.Trim(), piTipoLiberacion, piPromedio);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		#endregion
		#region Facilitadores

		public PartialViewResult AsignaFacilitador()
		{
			try
			{
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				DataTable ldtTabla = new DataTable();
				DataTable ldtTabla2 = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					ldtTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_cle_lista_facilitador", loParametros));
					loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
					ldtTabla2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_cle_lista_asignacion", loParametros));
					ViewData["Facilitadores"] = ldtTabla;
					ViewData["Tabla"] = ldtTabla2;
					ViewData["Titulo"] = "Grupos";
					return PartialView("AsignaFacilitador");
				}
				
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

		public JsonResult ActualizaFacilitador(string psRFC, string psPeriodo, string psNivel, string psGrupo)
		{
			try
			{
				Cle_Grupos loGrupo;
				loGrupo = new Cle_Grupos(SesionSapei.Sistema);
				loGrupo.Cargar(psPeriodo, psNivel, psGrupo);
				if (!loGrupo.EOF)
				{
					if (psRFC == "")
						psRFC = null;
					loGrupo.facilitador = psRFC;
					loGrupo.Guardar();
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
				}
                else
                {
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

		public PartialViewResult ActasSinCapturaCLE()
		{
			try
			{
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				string lsPeriodoCompleto = SesionSapei.Sistema.Sesion.Periodo.Identificacionlarga;
				DataTable ldtTabla = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
					ldtTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_cle_lista_actas_sin_calificacion", loParametros));
					ViewData["Tabla"] = ldtTabla;
					ViewData["Titulo"] = "Consulta de Actas Sin Calificación";
					ViewData["txtDescPeriodo"] = lsPeriodoCompleto;
					return PartialView("ActasSinCapturaCLE");
				}

			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

		#endregion
		#region ModificaCalificaciones

		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult ModificaCalificacion()
		{
			try
			{
				Cle_Liberacion_Ingles loLiberacion = new Cle_Liberacion_Ingles(SesionSapei.Sistema);
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					ViewData["Tabla"] = loLiberacion.RegresaAlumnosCambioCalificacion();
					ViewData["Titulo"] = "Modificación de Calificaciones de Ingles";
					ViewData["Modulo"] = "ModificaCalificacion";
					ViewData["Vista"] = "CentroLenguasExt/ModificaCalificacion";
					return PartialView("BuscaNoControl");
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public PartialViewResult ModificaCalificacion(string psNoControl)
		{
			try
			{
				Cle_Liberacion_Ingles loLiberacion = new Cle_Liberacion_Ingles(SesionSapei.Sistema);
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				ViewData["Datos"] = loLiberacion.RegresaAlumnosCambioCalificacion(psNoControl, lsPeriodo);
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("../Personal/Index");
			}

		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.CLE)]
		public JsonResult ModificaCalificacionJson(string psNoControl, string psObjeto)
		{

			try
			{
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                Cle_Liberacion_Ingles loLiberacion = new Cle_Liberacion_Ingles(SesionSapei.Sistema);
				loLiberacion.RegistraCalificaciones(lsPeriodo, psNoControl.Trim(), psObjeto);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
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
