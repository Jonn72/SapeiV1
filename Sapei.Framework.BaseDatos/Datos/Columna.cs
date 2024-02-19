using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.BaseDatos
{
     /// <summary>
     /// Clase para alcenar las columnas de una tabla de manejra dinamica
     /// </summary>
     public class Columna
     {
          /// <summary>
          ///Dictionary that hold all the dynamic property values 
          /// </summary>
          private Dictionary<string, object> ValorColumna = new Dictionary<string, object>();

          /// <summary>
          /// the property call to get any dynamic property in our Dictionary, or "" if none found. rvt [DataMember]          
          /// </summary>
          /// <param name="psName"></param>
          /// <returns></returns>
          public object this[string psName]
          {
               get
               {
                    if (ValorColumna.ContainsKey(psName.ToLower()))
                    {
                         return ValorColumna[psName.ToLower()];
                    }
                    return "";
               }
               set
               {
                    ValorColumna[psName.ToLower()] = value;
               }
          }
     }
}
