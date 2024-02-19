using System;
using System.Linq;
using System.Web;

namespace Sapei.Framework.Utilerias
{
     /// <summary>
     /// Clase que contiene los metodos que heredan las paginas para manejar las variables del sistema
     /// </summary>
     public class Sesion
     {
          #region Metodos de Lectura y Escritura

          /// <summary>
          /// Funcion que lee la variable de session y retorna su valor
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psVariable"></param>
          /// <returns></returns>
          public static T Lee<T>(string psVariable)
          {
               object loValor = HttpContext.Current.Session[psVariable];
               if (loValor == null)
                    return default(T);
               else
                    return ((T)loValor);
          }

          /// <summary>
          /// Funcion que escribe el valor en la variable de session
          /// </summary>
          /// <param name="psVariable"></param>
          /// <param name="poValor"></param>
          public static void Escribe(string psVariable, object poValor)
          {
               HttpContext.Current.Session[psVariable] = poValor;
          }

          /// <summary>
          /// Funcion que elimna una variable de session
          /// </summary>
          /// <param name="psVariable"></param>
          public static void EliminarVariableSesion(string psVariable)
          {
               HttpContext.Current.Session.Remove(psVariable);
          }

          /// <summary>
          /// Funcion que elimina todas las variables de session
          /// </summary>
          public static void EliminarTodasVariablesSesion()
          {
               HttpContext.Current.Session.RemoveAll();
          }

          #endregion
     }
}
