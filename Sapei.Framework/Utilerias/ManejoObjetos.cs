using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sapei.Framework.Utilerias
{
     /// <summary>
     /// Clase para el manejo de objetos
     /// </summary>
     public static class ManejoObjetos
     {
          /// <summary>
          /// To the null.
          /// </summary>
          /// <param name="o">The o.</param>
          /// <returns></returns>
          public static object ToNull(this object o)
          {
               return null;
          }

          /// <summary>
          /// Checks an object to see if it is null or empty.
          /// <para>Empty is any collection, array or dictionary with an item count of 0 or a string that is empty.</para>
          /// </summary>
          public static bool NotEmpty(this object obj)
          {
               if (obj != null && obj is ICollection)
               {
                    return ((ICollection)obj).Count > 0;
               }
               if (obj != null && obj is IDictionary)
               {
                    return ((IDictionary)obj).Keys.Count > 0;
               }
               if (obj != null && obj is Array)
               {
                    return ((Array)obj).Length > 0;
               }
               return !(obj == null || string.IsNullOrEmpty(obj.ToString()));
          }


          /// <summary>
          /// Simula IIF de VB.net
          /// </summary>
          /// <param name="poCondicion"></param>
          /// <param name="poIzquierda"></param>
          /// <param name="poDerecha"></param>
          /// <returns></returns>
          public static object Iif(bool poCondicion, object poIzquierda, object poDerecha)
          {
               return poCondicion ? poIzquierda : poDerecha;
          }

          /// <summary>
          /// Simula un IIf de Vb.net con generics
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poCondicion"></param>
          /// <param name="poIzquierda"></param>
          /// <param name="poDerecha"></param>
          /// <returns></returns>
          public static T Iif<T>(bool poCondicion, T poIzquierda, T poDerecha)
          {
               return poCondicion ? poIzquierda : poDerecha;
          }

          /// <summary>
          /// The equivalent of Microsoft.VisualBasic.Command() in C# is Environment.CommandLine. 
          /// However, this property includes also the full path of the executable, that isn't returned by Microsoft.VisualBasic.Command.
          /// If you just want to obtain in C# the same value that you have with Microsoft.VisualBasic.Command method
          /// </summary>
          /// <param name="psNombreParametro"></param>
          /// <returns></returns>
          public static string RegresaParametroCommandLine(string psNombreParametro)
          {
               int liPosicionInicio = 0;
               int liPosicionFinal = 0;
               bool lbPosicionInicioEncontrada = false;
               try
               {
                    psNombreParametro = psNombreParametro.ToLower();
                    liPosicionInicio = Environment.GetCommandLineArgs().ToString().ToLower().IndexOf(psNombreParametro);
                    if (liPosicionInicio == -1)
                    {
                         return "";
                    }
                    for (liPosicionFinal = liPosicionInicio + 1; liPosicionFinal <= Environment.GetCommandLineArgs().Length + 1; liPosicionFinal++)
                    {
                         if (!lbPosicionInicioEncontrada)
                         {
                              if (Environment.GetCommandLineArgs().ToString().ToLower().Substring(liPosicionFinal, 1) == "=")
                              {
                                   liPosicionInicio = liPosicionFinal + 1;
                                   lbPosicionInicioEncontrada = true;
                              }
                              else if (Environment.GetCommandLineArgs().ToString().ToLower().Substring(liPosicionFinal, 1) == " ")
                              {
                                   return Environment.GetCommandLineArgs().ToString().Substring(liPosicionInicio, liPosicionFinal - liPosicionInicio);
                              }
                         }
                    }
                    if (!lbPosicionInicioEncontrada)
                    {
                         return "";
                    }
                    return Environment.GetCommandLineArgs().ToString().Substring(liPosicionInicio, liPosicionFinal - liPosicionInicio);
               }
               catch
               {
                    return "";
               }
          }

          /// <summary>
          /// FUncion que verifica si se tienen parametro de la linea de comandos
          /// </summary>
          /// <param name="psNombreParametro"></param>
          /// <returns></returns>
          public static bool ContieneParametroLineadeComandos(string psNombreParametro)
          {
               string[] lasLineaComandos;
               lasLineaComandos = Environment.GetCommandLineArgs();
               foreach (string lsComando in lasLineaComandos)
               {
                    if (lsComando.ToUpper().Contains(psNombreParametro.ToUpper()))
                    {
                         return true;
                    }
               }
               return false;
          }

          /// <summary>
          /// Funcion que regresa el parametro de la linea de comandos
          /// </summary>
          /// <param name="psNombreParametro"></param>
          /// <returns></returns>
          public static string RegresaParametroLineadeComandos(string psNombreParametro)
          {
               string[] lasLineaComandos;
               string lsResultado;
               lasLineaComandos = Environment.GetCommandLineArgs();
               lsResultado = "";
               foreach (string lsComando in lasLineaComandos)
               {
                    if (lsComando.ToUpper().Contains(psNombreParametro.ToUpper()))
                    {
                         lsResultado = lsComando.Substring(lsComando.IndexOf("=") + 1);
                    }
               }
               return lsResultado;
          }

          /// <summary>
          /// Regresas the valor fila.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poTabla">The po tabla.</param>
          /// <param name="psColumna">The ps columna.</param>
          /// <returns></returns>
          public static T RegresaValorFila<T>(this DataTable poTabla, string psColumna)
          {
               Object poValor;
               if (poTabla.Rows.Count == 0)
               {
                    return GetDefaultValue<T>();
               }
               if (!poTabla.Columns.Contains(psColumna))
               {
                    return GetDefaultValue<T>();
               }
               poValor = poTabla.Rows[0][psColumna];
               if (Convert.IsDBNull(poValor) || String.IsNullOrEmpty(Convert.ToString(poValor)))
               {
                    return GetDefaultValue<T>();
               }
               return (T)Convert.ChangeType(poValor, typeof(T));
          }

          /// <summary>
          /// Regresas the valor fila.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poTabla">The po tabla.</param>
          /// <param name="piFila">The pi fila.</param>
          /// <param name="psColumna">The ps columna.</param>
          /// <returns></returns>
          public static T RegresaValorFila<T>(this DataTable poTabla, int piFila, string psColumna)
          {
               Object poValor;
               if (poTabla.Rows.Count == 0)
               {
                    return GetDefaultValue<T>();
               }
               if (piFila >= poTabla.Rows.Count)
               {
                    return GetDefaultValue<T>();
               }
               if (!poTabla.Columns.Contains(psColumna))
               {
                    return GetDefaultValue<T>();
               }
               poValor = poTabla.Rows[piFila][psColumna];
               if (Convert.IsDBNull(poValor) || String.IsNullOrEmpty(Convert.ToString(poValor)))
               {
                    return GetDefaultValue<T>();
               }
               return (T)Convert.ChangeType(poValor, typeof(T));
          }

          /// <summary>
          /// Regresas the valor.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poFila">The po fila.</param>
          /// <param name="psColumna">The ps columna.</param>
          /// <returns></returns>
          public static T RegresaValor<T>(this DataRow poFila, string psColumna)
          {
               Object poValor;
               if (!poFila.Table.Columns.Contains(psColumna))
               {
                    return GetDefaultValue<T>();
               }
               poValor = poFila[psColumna];
               if (Convert.IsDBNull(poValor) || String.IsNullOrEmpty(Convert.ToString(poValor)))
               {
                    return GetDefaultValue<T>();
               }
               return (T)Convert.ChangeType(poValor, typeof(T));
          }

          /// <summary>
          /// Gets the default value.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <returns></returns>
          private static T GetDefaultValue<T>()
          {
               //if (typeof(T) == typeof(String))
               //     return (T)(object)String.Empty;
               if (typeof(T) == typeof(DateTime))
               {
                    return (T)(object)DateTime.Now;
               }
               return default(T);
          }

          public static string RegresaCadenaDeLista<T>(this List<T> loLista)
          {
               StringBuilder lsCadena = new StringBuilder();
               foreach (object loObj in loLista)
               {
                    lsCadena.AppendFormat("'{0}',", Convert.ToString(loObj));
               }
               return lsCadena.Remove(lsCadena.Length - 1, 1).ToString();
          }
        /// <summary>
        /// Libera de moemria la tabla generica
        /// </summary>
        /// <param name="poTabla"></param>
        public static void LiberaTablaGenerica(BaseDatos.TablaGenerica poTabla)
        {
            if (!Object.Equals(poTabla, null))
            {
                poTabla.Clear();
                poTabla.Dispose();
                poTabla = null;
            }
        }
    }
}
