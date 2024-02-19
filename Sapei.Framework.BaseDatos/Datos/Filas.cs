using System;
using System.Linq;


namespace Sapei.Framework.BaseDatos
{
     /// <summary>
     /// Clase para almancear las columnas dinamicas de la tabla
     /// </summary>
     public class Fila
     {
          /// <summary>
          /// property is a class that will create dynamic properties at runtime
          /// </summary>
          private Columna _oProperty = new Columna();

          /// <summary>
          /// Propiedade para acceder a las propiedades dinamicas
          /// </summary>
          public Columna ValorFila
          {
               get
               {
                    return _oProperty;
               }
               set
               {
                    _oProperty = value;
               }
          }

          /// <summary>
          /// Propiedad para acceder al valor del campo usando el nombre como parametro
          /// </summary>
          /// <param name="psValor"></param>
          /// <returns></returns>
          public object this[string psValor]
          {
               get
               {
                    return _oProperty[psValor.ToLower()];
               }
               set
               {
                    _oProperty[psValor.ToLower()] = value;
               }
          }
     }
}
