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
    public class ControlUsuarioController : Controller
    {
        // GET: ControlUsuario
        public ActionResult Index()
        {
            return View();
        }

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
		public PartialViewResult CrearUsuario()
		{
			try
			{
				return PartialView("CrearUsuario");
			}
			catch (Exception)
			{
				return PartialView("Home", "Index");
			}

		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
		public JsonResult CrearUsuarios(string psUsuario, string psNombreUsuario, string psContraseña)
		{
			try
			{
				DataTable loDt = new DataTable();
		        string psContraseña1 = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.RegresaFirmaPersonalMD5(psContraseña);
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@usuario",psUsuario));
				loParametros.Add(new ParametrosSQL("@nombre_usuario", psNombreUsuario));
				loParametros.Add(new ParametrosSQL("@contraseña",psContraseña1));
				loParametros.Add(new ParametrosSQL("@tipo_usuario", SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString()));

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pai_control_usuario", loParametros));
					
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Datos guardados", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
		public PartialViewResult Usuarios( int psEstado)
		{
			DataTable loDt1 = new DataTable();
			
			try
			{
				string psTipoUsuario;
				string psUsuario;
				Personal loPersonal;
				loPersonal = new Personal(SesionSapei.Sistema);
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				psTipoUsuario = SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString();
				psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario.ToString();
				string psNuevoUsuario = "";
				string psNombreUsuario = "";


				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt1 = loPersonal.MuestraUsuarios(psTipoUsuario, psUsuario, psNuevoUsuario, psNombreUsuario, psEstado);
				}

				ViewData["Permisos"] = loDt1;

				return PartialView("Usuarios");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Home", "Index");
			}
		}

		public JsonResult EliminaUsuario(string psNuevoUsuario, string psNombreUsuario, int psEstado) {
			 
			try
			{
				DataTable loDt1 = new DataTable();

				string psTipoUsuario;
				string psUsuario;
				Personal loPersonal;
				loPersonal = new Personal(SesionSapei.Sistema);
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				psTipoUsuario = SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString();
				psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario.ToString();

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt1 = loPersonal.MuestraUsuarios(psTipoUsuario, psUsuario, psNuevoUsuario, psNombreUsuario, psEstado);
				}

				 return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Datos eliminados", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError(); 
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
		public PartialViewResult PermisosUsuario(string psUsuarioSeleccionado)
		{
			try
			{
				DataTable loDt1 = new DataTable();

				string psUsuario;
				string psTipoUsuario;
				Personal loPersonal;
				loPersonal = new Personal(SesionSapei.Sistema);
				psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario.ToString();
				psTipoUsuario = SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString();

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt1 = loPersonal.PermisosUsuarios(psUsuario, psTipoUsuario, psUsuarioSeleccionado);
				}

				ViewData["PermisosUsuario"] = loDt1;

				return PartialView("PermisosUsuario");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Home", "Index");
			}
		}

		public JsonResult OtotgarPermisosUsuario(string psUsuarioSeleccionado, string psClave) 
		{
			try
			{
				DataTable loDt = new DataTable();
				string psUsuario;
				Personal loPersonal;
				loPersonal = new Personal(SesionSapei.Sistema);
				psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario.ToString();

							
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt = loPersonal.OtorgarPermisosUsuario(psUsuario, psUsuarioSeleccionado, psClave);
				}

				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Datos guardados", true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}
	}
}