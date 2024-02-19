using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;


namespace appSapei.Controllers
{
	public class HomeController : Controller
	{
		[AllowAnonymous]
		[HttpGet]
		public ActionResult Index()
		{
			try
			{
				InicioSesion.ValidaExisteVariableSistemaIniciada();
				return View();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex, "Home/Index");
				return View();
			}
		}
		[NoDirectAccess]
		public PartialViewResult Login()
		{
			try
			{
				return PartialView("Login");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex, "Home/Login");
				return null;
			}
		}
		[NoDirectAccess]
		public PartialViewResult Mantenimiento()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex, "Home/Mantenimiento");
				return null;
			}
		}
		[NoDirectAccess]
		public PartialViewResult FinSesion(string psValor)
		{
			try
			{
				switch (psValor)
				{
					case "1":
						@ViewData["Mensaje"] = "Sesión finalizada por actualización de sistema, debe iniciar sesión nuevamente. Gracias";
						break;
					case "2":
						if (!string.IsNullOrEmpty(SesionSapei.MensajeFinSesion))
							@ViewData["Mensaje"] = SesionSapei.MensajeFinSesion;
						else
							@ViewData["Mensaje"] = "Su sesión ha finalizado por inactividad, debe iniciar sesión nuevamente. Gracias";
						break;
					case "3":
						@ViewData["Mensaje"] = "Su sesión ha finalizado por hacer mal uso del sistema, debe iniciar sesión nuevamente. Gracias";
						break;
					default:
						@ViewData["Mensaje"] = "Usted ha intentado acceder a una página de forma incorrecta. Cerrando sesión...";
						break;
				}
				@ViewData["RedirecIndex"] = "../Home/Index";
				return PartialView("../Generales/AvisosGenerales");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex, "Home/FinSesion");
				return null;
			}
		}
		[AllowAnonymous]
		[HttpGet]
		public ActionResult Error404()
		{
			try
			{
				InicioSesion.ValidaExisteVariableSistemaIniciada();
				return View();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return View();
			}
		}

		[SessionExpire]
		public PartialViewResult Bienvenida()
		{
			return PartialView("Bienvenida");
		}

		public PartialViewResult ScriptsSecundarios()
		{
			return PartialView("_ScriptsSecundarios", "Shared");
		}

		public ActionResult CerrarSesion()
		{
			try
			{
				System.Web.Security.FormsAuthentication.SignOut();
				SesionSapei.Sistema.CerrarSesion();
				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return RedirectToAction("Index", "Home");
			}

		}


		[AllowAnonymous]
		[HttpGet]
		public ActionResult Recupera()
		{
			try
			{
				ViewData["mensaje"] = "Guardian";

				if (SesionSapei.Sistema == null)
				{
					return RedirectToAction("Index", "Home");
				}
				return View();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex, "Home/Recupera");
				return View();
			}
		}

		public JsonResult IniciaSolicitudContraseña(string psValor, string psTipo)
		{
			try
			{
				string lsMensaje;
				lsMensaje = InicioSesion.SolicitaContraseña(psValor, psTipo);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}

		public JsonResult ValidaSolicitudContraseña(string psCodigo)
		{
			try
			{
				if (string.IsNullOrEmpty(psCodigo))
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Debe ingresar un código valido", false);
				}
				if (!SesionSapei.Sistema.Sesion.Usuario.SolicitudContraseñaActiva)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Acceso Denegado", false);
				}
				if (psCodigo.Trim() != SesionSapei.Sistema.Sesion.Usuario.CodigoValidacion)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Código Incorrecto", false);
				}
				if (SesionSapei.Sistema.Sesion.Usuario.TipoUsuario == Sapei.Framework.Configuracion.enmTipoUsuario.DOCENTE)
				{
					Acceso loAcceso = new Acceso(SesionSapei.Sistema);
					loAcceso.Cargar(SesionSapei.Sistema.Sesion.Usuario.Usuario);
					loAcceso.contrasena = SesionSapei.Sistema.Sesion.Usuario.Contraseña.MD5HASH();
					loAcceso.Guardar();
				}
				Sapei.Framework.Utilerias.Funciones.ManejoCorreos.EnviaRecuperacionContraseña(SesionSapei.Sistema.Sesion.Usuario.Nombre, SesionSapei.Sistema.Sesion.Usuario.Correo, SesionSapei.Sistema.Sesion.Usuario.Contraseña);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se envio tu contraseña a correo ", true);

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex, "Home/ValidaSolicitudContraseña");
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
	}


}