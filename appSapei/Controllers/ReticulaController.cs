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

namespace appSapei.Controllers
{
    public class ReticulaController : Controller
    {
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
		public PartialViewResult Index()
        {
			return PartialView();
		}
        #region Seleccion
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult Consulta(string psControl = null)
		{
			try
			{
				string lsMensaje;
				string lsControl;

				if (string.IsNullOrEmpty(psControl))
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				else
					lsControl = psControl;

				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				System.Data.SqlClient.SqlDataReader loReader;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_regresa_reticula", loParametros);

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
				}
				ViewData["reticula"] = lsMensaje;
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult Seleccion(string psControl = null)
		{
			return RegresaReticula(psControl);
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult SeleccionGrupoJson(string psControl, string psGrupo)
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
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loParametros.Add(new ParametrosSQL("@md5", psGrupo));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));

					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_seleccion_registra_grupo", loParametros));
					ViewData["DatosSeleccion"] = loDt;

				}
				return PartialView("Seleccion");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult SeleccionFinalizarJson(string psControl)
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
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString()));

					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_seleccion_finalizar", loParametros));
					ViewData["DatosSeleccion"] = loDt;

				}
				return PartialView("Seleccion");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		private PartialViewResult RegresaReticula(string psControl = null)
		{
			try
			{
				string lsControl;
				DataTable loDt = new DataTable();
				if (string.IsNullOrEmpty(psControl))
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				else
					lsControl = psControl;

				ViewData["DescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.Identificacionlarga;

				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
					if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
					{
						loParametros.Add(new ParametrosSQL("@iniciar", 0));
						loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_seleccion_materias_datos", loParametros));
					}
					else
						loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pav_seleccion_materias", loParametros));

				}
				if (loDt.Columns.Count == 1)
				{
					ViewData["Mensaje"] = loDt.Rows[0].ItemArray[0].ToString();
					return PartialView("../Generales/AvisosGenerales");
				}
				ViewData["DatosSeleccion"] = loDt;
				return PartialView("Seleccion");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
		public PartialViewResult SeleccionEliminaGrupoJson(string psControl, string psGrupo)
		{
			try
			{
				string lsControl;
				DataTable loDt = new DataTable();
				if (string.IsNullOrEmpty(psControl))
					lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				else
					lsControl = psControl;

				ViewData["DescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.Identificacionlarga;

				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
					loParametros.Add(new ParametrosSQL("@no_de_control", lsControl));
					loParametros.Add(new ParametrosSQL("@md5", psGrupo));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_seleccion_elimina_grupo", loParametros));

				}
				ViewData["DatosSeleccion"] = loDt;
				return PartialView("Seleccion");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

        #endregion
        #region Mantenimiento
        public PartialViewResult NuevaReticula(string psCarreraAbrv = null)
		{
			try
			{
				Carreras loCarreras = new Carreras(SesionSapei.Sistema);
				ViewData["cboMaterias"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("materias", "materia", "nombre_abreviado_materia+' - '+materia ", "", "", false, 0, null, true);
				ViewData["cboEspecialidad"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("especialidades", "especialidad", "nombre_especialidad+' - '+especialidad ", "", "", false, 0, null, true);
				ViewData["DatosCarrera"] = loCarreras.RegresaInformacionCarrera(psCarreraAbrv);

				string lsMensaje;

				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				System.Data.SqlClient.SqlDataReader loReader;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@carrera_abrv", psCarreraAbrv));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_regresa_reticula_mantenimiento", loParametros);

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
				}
				ViewData["reticulaMantenimiento"] = lsMensaje;
				return PartialView("vistaReticula");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		public JsonResult AgregarMateriaReticula(string carrera, string reticula, string materia, string creditos_materia, string horas_teoricas, string horas_practicas, string orden_certificado, string semestre_reticula, string creditos_prerrequisito, string especialidad, string clave_oficial_materia, string estatus_materia_carrera, string programa_estudios, string renglon)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", carrera));
					loParametros.Add(new ParametrosSQL("@reticula", reticula));
					loParametros.Add(new ParametrosSQL("@materia", materia));
					loParametros.Add(new ParametrosSQL("@creditos_materia", creditos_materia));
					loParametros.Add(new ParametrosSQL("@horas_teoricas", horas_teoricas));
					loParametros.Add(new ParametrosSQL("@horas_practicas", horas_practicas));
					loParametros.Add(new ParametrosSQL("@orden_certificado", orden_certificado));
					loParametros.Add(new ParametrosSQL("@semestre_reticula", semestre_reticula));
					loParametros.Add(new ParametrosSQL("@creditos_prerrequisito", creditos_prerrequisito));
					loParametros.Add(new ParametrosSQL("@especialidad", especialidad));
					loParametros.Add(new ParametrosSQL("@clave_oficial_materia", clave_oficial_materia));
					loParametros.Add(new ParametrosSQL("@estatus_materia", estatus_materia_carrera));
					loParametros.Add(new ParametrosSQL("@programa_estudios", programa_estudios));
					loParametros.Add(new ParametrosSQL("@renglon", renglon));
					loParametros.Add(new ParametrosSQL("@bandera", 2));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_modificar_reticula_carrera", loParametros);

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
		public JsonResult BuscarMateriaReticula(string carrera, string reticula, string materia, string semestre_reticula, string renglon)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					DataTable loDt = new DataTable();
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", carrera));
					loParametros.Add(new ParametrosSQL("@reticula", reticula));
					loParametros.Add(new ParametrosSQL("@materia", materia));
					loParametros.Add(new ParametrosSQL("@creditos_materia", ""));
					loParametros.Add(new ParametrosSQL("@horas_teoricas", ""));
					loParametros.Add(new ParametrosSQL("@horas_practicas", ""));
					loParametros.Add(new ParametrosSQL("@orden_certificado", ""));
					loParametros.Add(new ParametrosSQL("@semestre_reticula", semestre_reticula));
					loParametros.Add(new ParametrosSQL("@creditos_prerrequisito", ""));
					loParametros.Add(new ParametrosSQL("@especialidad", ""));
					loParametros.Add(new ParametrosSQL("@clave_oficial_materia", ""));
					loParametros.Add(new ParametrosSQL("@estatus_materia", ""));
					loParametros.Add(new ParametrosSQL("@programa_estudios", ""));
					loParametros.Add(new ParametrosSQL("@renglon", renglon));
					loParametros.Add(new ParametrosSQL("@bandera", 3));
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_modificar_reticula_carrera", loParametros));


					return ManejoMensajesJson.RegresaJsonTabla(loDt);

				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}


		public JsonResult ModificarMateriaReticula(string carrera, string reticula, string materia, string creditos_materia, string horas_teoricas, string horas_practicas, string orden_certificado, string semestre_reticula, string creditos_prerrequisito, string especialidad, string clave_oficial_materia, string estatus_materia_carrera, string programa_estudios, string renglon)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", carrera));
					loParametros.Add(new ParametrosSQL("@reticula", reticula));
					loParametros.Add(new ParametrosSQL("@materia", materia));
					loParametros.Add(new ParametrosSQL("@creditos_materia", creditos_materia));
					loParametros.Add(new ParametrosSQL("@horas_teoricas", horas_teoricas));
					loParametros.Add(new ParametrosSQL("@horas_practicas", horas_practicas));
					loParametros.Add(new ParametrosSQL("@orden_certificado", orden_certificado));
					loParametros.Add(new ParametrosSQL("@semestre_reticula", semestre_reticula));
					loParametros.Add(new ParametrosSQL("@creditos_prerrequisito", creditos_prerrequisito));
					loParametros.Add(new ParametrosSQL("@especialidad", especialidad));
					loParametros.Add(new ParametrosSQL("@clave_oficial_materia", clave_oficial_materia));
					loParametros.Add(new ParametrosSQL("@estatus_materia", estatus_materia_carrera));
					loParametros.Add(new ParametrosSQL("@programa_estudios", programa_estudios));
					loParametros.Add(new ParametrosSQL("@renglon", renglon));
					loParametros.Add(new ParametrosSQL("@bandera", 5));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_modificar_reticula_carrera", loParametros);

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


		public JsonResult EliminarMateriaReticula(string carrera, string reticula, string materia, string semestre_reticula, string renglon)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@carrera", carrera));
					loParametros.Add(new ParametrosSQL("@reticula", reticula));
					loParametros.Add(new ParametrosSQL("@materia", materia));
					loParametros.Add(new ParametrosSQL("@creditos_materia", ""));
					loParametros.Add(new ParametrosSQL("@horas_teoricas", ""));
					loParametros.Add(new ParametrosSQL("@horas_practicas", ""));
					loParametros.Add(new ParametrosSQL("@orden_certificado", ""));
					loParametros.Add(new ParametrosSQL("@semestre_reticula", semestre_reticula));
					loParametros.Add(new ParametrosSQL("@creditos_prerrequisito", ""));
					loParametros.Add(new ParametrosSQL("@especialidad", ""));
					loParametros.Add(new ParametrosSQL("@clave_oficial_materia", ""));
					loParametros.Add(new ParametrosSQL("@estatus_materia", ""));
					loParametros.Add(new ParametrosSQL("@programa_estudios", ""));
					loParametros.Add(new ParametrosSQL("@renglon", renglon));
					loParametros.Add(new ParametrosSQL("@bandera", 4));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_modificar_reticula_carrera", loParametros);

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
	}
}