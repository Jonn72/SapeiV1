using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.Datos
{
     [Serializable]
     public class Propiedad
     {

          /// <summary>
          /// Gets or sets a value indicating whether [incluye horas].
          /// </summary>
          /// <value>
          ///   <c>true</c> if [incluye horas]; otherwise, <c>false</c>.
          /// </value>
          public bool IncluyeHoras { get; set; }

          /// <summary>
          /// Sirve para varchars, pero tambien para numeros. Longitud del campo
          /// </summary>
          public int Longitud { get; set; }

          /// <summary>
          /// Precision del valor
          /// </summary>
          public int Precision { get; set; }

          /// <summary>
          /// Es un campo requerido por la base de datos
          /// </summary>
          public bool EsRequeridoBD { get; set; }

          /// <summary>
          /// Es una llave primaria
          /// </summary>
          public bool EsPrimaryKey { get; set; }

          /// <summary>
          /// Id del campos
          /// </summary>
          public int CampoId { get; set; }
          /// <summary>
          /// Gets or sets the descripcion.
          /// </summary>
          /// <value>
          /// The descripcion.
          /// </value>
          public string Descripcion { get; set; }
          /// <summary>
          /// Gets or sets a value indicating whether [es identity].
          /// </summary>
          /// <value>
          ///   <c>true</c> if [es identity]; otherwise, <c>false</c>.
          /// </value>
          public bool EsIdentity { get; set; }
          /// <summary>
          /// Gets or sets the tipo.
          /// </summary>
          /// <value>
          /// The tipo.
          /// </value>
          public Type Tipo { get; set; }
     }
}
