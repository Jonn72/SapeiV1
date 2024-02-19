using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.Configuracion
{
     /// <summary>
     /// 
     /// </summary>
     [Serializable]
     public class ConfiguracionParametros
     {

          #region Generales
          
          /// <summary>
          /// Gets or sets a value indicating whether [forzar actualizaciones].
          /// </summary>
          /// <value>
          /// <c>true</c> if [forzar actualizaciones]; otherwise, <c>false</c>.
          /// </value>
          public bool ForzarActualizaciones { get; set; }

          #endregion

          #region Informacion cultura de la base de datos
          #endregion


     }
}
