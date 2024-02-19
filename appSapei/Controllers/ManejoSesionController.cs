using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System.Net.NetworkInformation;
using appSapei.Models;

namespace appSapei.Controllers
{
     public class ManejoSesionController : Controller
     {

          public JsonResult UserLogin(UsuarioInicioSesion psUsuario)
          {
               string lsMensaje;
               try
               {
                    InicioSesion.ValidaExisteVariableSistemaIniciada(true);
                    //Valida si es super usuario
                    if (InicioSesion.EsUsuarioSapei(psUsuario.Nombre, psUsuario.Contrasenia))
                    {
                         return new JsonResult { Data = psUsuario, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    lsMensaje = ValidaUsuario(psUsuario);

                    if (string.IsNullOrEmpty(lsMensaje))
                    {
                         psUsuario.Mensaje = "Datos Correctos, ingresando...";
                         psUsuario.Clase = "alert-success";
                         psUsuario.Success = true;
                         psUsuario.Index = "../" + SesionSapei.Index;
                         return new JsonResult { Data = psUsuario, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }

                    psUsuario.Mensaje = lsMensaje;
                    psUsuario.Clase = "alert-danger";
                    psUsuario.Success = false;
                    return new JsonResult { Data = psUsuario, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    psUsuario.Mensaje = "Error de conexión...";
                    psUsuario.Clase = "alert-danger";
                    psUsuario.Success = false;
                    return new JsonResult { Data = psUsuario, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="psUsuario"></param>
          /// <returns></returns>
          private string ValidaUsuario(UsuarioInicioSesion psUsuario)
          {
               SisUsuario loUsuario;
               try
               {
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {
                         loUsuario = new SisUsuario(SesionSapei.Sistema);
                         List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                         System.Data.SqlClient.SqlDataReader loReader;
                         string lsMensaje = "";
                         int liAcceso = 0;
                         string LsIp = System.Web.HttpContext.Current.Request.UserHostAddress;
                         string LsMac = RegresaDireccionMAC();
                         loParametros.Add(new ParametrosSQL("@usuario", psUsuario.Nombre));
                         loParametros.Add(new ParametrosSQL("@contraseña", psUsuario.Contrasenia));
                         loParametros.Add(new ParametrosSQL("@ip", LsIp));
                         loParametros.Add(new ParametrosSQL("@mac", LsMac));
                         loParametros.Add(new ParametrosSQL("@tipo_usuario", Convert.ToInt32(psUsuario.TipoUsuario)));

                         loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_control_acceso", loParametros);

                         if (loReader.HasRows)
                         {
                              while (loReader.Read())
                              {
                                   liAcceso = loReader.GetInt32(0);
                                   lsMensaje = loReader.GetString(1);
                              }
                         }
                         loReader.Close();
                         loReader.Dispose();
                         if (liAcceso >= 1)
                         {
                              if (liAcceso > 3)
                                   SesionSapei.Sistema.BloqueaUsuario = true;
                              return lsMensaje;
                         }

                         loUsuario.Cargar(psUsuario.Nombre, psUsuario.Contrasenia, psUsuario.TipoUsuario);
                         GuardaCookieUsuario(psUsuario);

                         //inicio sesion usuario
                         SesionSapei.Sistema.IniciaSesion(SesionSapei.Sistema.Sesion.Institucion.Numero, loUsuario);
                         if(Convert.ToString(SesionSapei.Sistema.Sesion.Usuario.RolUsuario) == "PER")
                             {
                        SesionSapei.Index =  "Personal/Index/";
                             }
                         else { 
                         SesionSapei.Index = psUsuario.TipoUsuario.ToString().Capitalizada() + "/Index/";
                         }
                         SesionSapei.Sistema.Sesion.Usuario.FuncionesPermitidas.Add("/Home/Bienvenida");
                         CargaRutaConexion(System.Web.HttpContext.Current.Request.IsLocal, System.Web.HttpContext.Current.Request.UserHostAddress);
                         System.Web.Security.FormsAuthentication.SetAuthCookie(loUsuario.Nombre, true);
                         return "";
                    }
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return "Error de conexión";
               }
          }

          private void CargaRutaConexion(bool pbLocal, string psIP)
          {

               if (pbLocal)
               {
                    SesionSapei.Sistema.RutaUploads = "http://localhost/";
                    return;
               }
               if (psIP.Contains("192.168."))
                    SesionSapei.Sistema.RutaUploads = "http://192.168.9.245/";
               else
                    SesionSapei.Sistema.RutaUploads = "https://Sapei.ittlahuac.edu.mx/";
          }
          #region Metodos auxiliares
          private string RegresaDireccionMAC()
          {
               NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
               String sMacAddress = string.Empty;
               String sipAddress = string.Empty;
               foreach (NetworkInterface adapter in nics)
               {
                    if (sMacAddress == String.Empty)// only return MAC Address from first card
                    {
                         IPInterfaceProperties properties = adapter.GetIPProperties();
                         sMacAddress = adapter.GetPhysicalAddress().ToString();
                         return sMacAddress;
                    }
               } return sMacAddress;
          }
          /// <summary>
          /// Recupera si existen valores en cookie de algun correo de usuario guardado en el explirador
          /// </summary>
          private UsuarioInicioSesion BuscaCookieUsuario()
          {
               UsuarioInicioSesion loUsuario;
               loUsuario = new UsuarioInicioSesion();
               //Regresa correo de usuario en cookie
               loUsuario.Nombre = ManejaCookieCache.RegresaNombreUsuario(SesionSapei.TipoUsuario);
               //Marca si la cookie se almacena o no
               loUsuario.Recordar = ManejaCookieCache.RecordarUsuario();
               return loUsuario;
          }
          /// <summary>
          /// 
          /// </summary>
          /// <param name="poUsuario"></param>
          private void GuardaCookieUsuario(UsuarioInicioSesion poUsuario)
          {
               ManejaCookieCache.GuardaCookieCorreoUsuario(poUsuario.Recordar, poUsuario.Nombre, SesionSapei.Sistema.Sesion.Usuario.TipoUsuario);
          }

          #endregion
     }
}