using System;
using System.Linq;
using System.Web;
using Sapei.Framework.Configuracion;

namespace Sapei.Framework.Utilerias
{
     /// <summary>
     /// Clase que tiene los metodos para manipular la cache y las cookie 
     /// </summary>
     public class ManejaCookieCache
     {
          /// <summary>
          /// Variables para el nombre de cookie del correo del usuario
          /// </summary>
          private const string _iTipoUsuario = "siSIITipoUsuario";
          /// <summary>
          /// Variables para el nombre de cookie del correo del usuario
          /// </summary>
          private const string _sNombreCookieUsuario = "soSIIConfigUsuario";
          /// <summary>
          /// variable para el nombre que guarda el coreo del usuario
          /// </summary>
          private const string _sGuardaUsuario = "siSIIGuardaUsuario";
          /// <summary>
          /// variable pra elcorreo del usuario
          /// </summary>
          private const string _sNombreUsuario = "ssSIIUsuario";

          /// <summary>
          /// funcion estatica para guardar el  correo del usuario en una cookie
          /// </summary>
          /// <param name="pbRecordarCorreoUsuario">true/false para guardar el correo del usuario</param>
          /// <param name="psCorreoUsuario">cadena del correo del usuario</param>
          public static void GuardaCookieCorreoUsuario(bool pbRecordarCorreoUsuario, string psCorreoUsuario, enmTipoUsuario penmTipoUsuario)
          {
               HttpCookie loUsuario;
               bool lbExisteCookie;
               //recuperamos la cookie del navegador
               loUsuario = HttpContext.Current.Request.Cookies[_sNombreCookieUsuario];
               lbExisteCookie = false;
               if (Object.Equals(loUsuario, null))
                    //Sino existe instanciamos la cookie con el nombre respectivo
                    loUsuario = new HttpCookie(_sNombreCookieUsuario);
               else
                    lbExisteCookie = true;
               if (lbExisteCookie)
                    //Si existe actualizamos de la coleccion de valores que el valor de recordar el usuario
                    loUsuario.Values[_sGuardaUsuario] = ManejoObjetos.Iif<string>(pbRecordarCorreoUsuario, "1", "0");
               else
                    //Si existe agregamos a la coleccion de valores que el valor de recordar el usuario
                    loUsuario.Values.Add(_sGuardaUsuario, ManejoObjetos.Iif<string>(pbRecordarCorreoUsuario, "1", "0"));
               if (pbRecordarCorreoUsuario)
               {
                    if (lbExisteCookie)
                    {
                         //Actualizamos el valor dl correo del usuario
                         loUsuario.Values[_sNombreUsuario] = psCorreoUsuario;
                         loUsuario.Values[_iTipoUsuario] = penmTipoUsuario.ToString();
                    }
                    else
                    {
                         //agregamos el nuevo valor del correo del usuario
                         loUsuario.Values.Add(_sNombreUsuario, psCorreoUsuario);
                         loUsuario.Values.Add(_iTipoUsuario, penmTipoUsuario.ToString());
                    }
               }
               else
                    //sino desea guardar el usario, eliminamos el valor de la cookie
                    loUsuario.Values.Remove(_sNombreUsuario);
               //Le colocamos 10 dias pra que almacene la cookie
               loUsuario.Expires = DateTime.Now.AddDays(10);
               if (lbExisteCookie)
                    //Actualizamos la cookie existente en la peticion actual del navegador
                    HttpContext.Current.Response.Cookies.Set(loUsuario);
               else
                    //Agreamos una nueva cooike en la peticion actual del navegador
                    HttpContext.Current.Response.AppendCookie(loUsuario);
          }

          /// <summary>
          /// Funcion que regresa el correo actuala que se encuntra en la cookie del navegador
          /// </summary>
          /// <returns></returns>
          public static string RegresaNombreUsuario(enmTipoUsuario penmTipoUsuario)
          {
               HttpCookie loUsuario;
               //recuperamos la cookie actual en la peticion
               loUsuario = HttpContext.Current.Request.Cookies[_sNombreCookieUsuario];
               if (!Object.Equals(loUsuario, null))
               {
                    //Se valida si corresponde al tipo de usuario
                    if (loUsuario.Values[_iTipoUsuario] == penmTipoUsuario.ToString())
                         //si existe regresamos lo q ue contenga 
                         return loUsuario.Values[_sNombreUsuario];
               }
               //retornamos vacio si no existe la cookie en la peticion actual
               return "";
          }

          /// <summary>
          /// Funcion estatica q determina si se recuerda el usuario segun lo almacenado en la cookie
          /// </summary>
          /// <returns></returns>
          public static bool RecordarUsuario()
          {
               HttpCookie loUsuario;
               //Recuperamos la cookie actual en la peticion
               loUsuario = HttpContext.Current.Request.Cookies[ManejaCookieCache._sNombreCookieUsuario];
               if (!Object.Equals(loUsuario, null))
                    //regresamos lo que sencuentra  almacenado en la cookie
                    if (loUsuario.Values[ManejaCookieCache._sGuardaUsuario] == "1")
                         return true;
               //cualquier otro caso regresamos false
               return false;
          }

          /// <summary>
          /// Funcion que configura el usuario en cache para determinar si se loguea una vez
          /// </summary>
          /// <param name="psCorreoUsuario"></param>
          public static void ConfigurarCorreoUsuarioenCache(string psCorreoUsuario)
          {
               TimeSpan loSessTimeOut;
               string lsKey;
               lsKey = psCorreoUsuario + psCorreoUsuario;
               //creamos un tiempo basado en el timeout del wef.config del sistema
               loSessTimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
               //insertamos en cahce el usuario y el tiempo que durara en cache
               HttpContext.Current.Cache.Insert(lsKey, lsKey, null, DateTime.MaxValue, loSessTimeOut, System.Web.Caching.CacheItemPriority.NotRemovable, null);
          }

          /// <summary>
          /// Regresa el correo que se encuentra almacenado en cache
          /// </summary>
          /// <param name="psCorreoUsuario">Cadena del correo del usuario a almacenar</param>
          /// <returns></returns>
          public static string RegresaCorreoUsuarioenCache(string psCorreoUsuario)
          {
               string psKey = psCorreoUsuario + psCorreoUsuario;
               //retorna el valor que contenta en cache de la llave
               return Convert.ToString(HttpContext.Current.Cache[psKey]);
          }
     }
}
