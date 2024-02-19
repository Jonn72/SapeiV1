using System;
using System.Linq;

namespace Sapei.Framework.Datos
{
     /// <summary>
     /// Clase para carga las propiedades de la columna de la tabla
     /// </summary>
     [Serializable]
     public class PropiedadesColumna<T> : Propiedad
     {
          /// <summary>
          /// Valor actual del registro
          /// </summary>
          public T Valor { get; set; }

          /// <summary>
          /// Valor anterior del registro. Este es el mismo valor que la propiedad de Valor si es que no cambio de valor
          /// </summary>
          internal protected T ValorAnterior { get; set; }
     }
}
