using System;
using System.Web.Mvc;
using Sapei.Framework.Utilerias;
using System.Web.Routing;

namespace appSapei.App_Start
{
     public class SessionExpireAttribute : ActionFilterAttribute
     {
          private Sapei.Framework.Configuracion.enmRolUsuario[] _oRolUsuarioValido;
          public SessionExpireAttribute(params Sapei.Framework.Configuracion.enmRolUsuario[] poRoles)
          {
               _oRolUsuarioValido = poRoles;
          }
          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               if (SesionSapei.Sistema == null)
               {
                    // check if a new session id was generated
                    filterContext.Result = new RedirectToRouteResult(new
                                   RouteValueDictionary(new { controller = "Home", action = "FinSesion", psValor = "1" }));
                    return;
               }
               if (string.IsNullOrEmpty(SesionSapei.Sistema.Sesion.Usuario.Usuario))
               {
                    // check if a new session id was generated
                    filterContext.Result = new RedirectToRouteResult(new
                                   RouteValueDictionary(new { controller = "Home", action = "FinSesion", psValor = "2" }));
                    return;
               }
               
               if (filterContext.HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
               {
                    
                    if (filterContext.HttpContext.Request.Url.AbsolutePath != "/" + SesionSapei.Index)
                    {
                        if (filterContext.HttpContext.Request.Url.AbsolutePath == "/Estudiante/RegistraPagoBanca/" 
                            && SesionSapei.Sistema.Sesion.Usuario.TipoUsuario == Sapei.Framework.Configuracion.enmTipoUsuario.ESTUDIANTE)
                        {
                            return;
                        }

                    SesionSapei.MensajeFinSesion = "Usted ha intentado acceder a una página de forma incorrecta";
                         filterContext.Result = new RedirectResult("~/Home/CerrarSesion");
                         return;
                    }
               }
               if (filterContext.HttpContext.Request.Url.AbsolutePath != "/" + SesionSapei.Index)
               {
                    if (_oRolUsuarioValido.Length > 0)
                    {
                         if (Array.IndexOf(_oRolUsuarioValido, SesionSapei.Sistema.Sesion.Usuario.RolUsuario) < 0)
                         {
                              SesionSapei.MensajeFinSesion = "Usted ha intentado acceder a una página de forma incorrecta";
                              filterContext.Result = new RedirectResult("~/Home/FinSesion/4");
                              return;
                         }
                    }
               }
               base.OnActionExecuting(filterContext);
          }
          private bool ValidaPermisoFuncion(string psUrl)
          {
               return SesionSapei.Sistema.Sesion.Usuario.FuncionesPermitidas.Contains(psUrl);
          }
     }

}