using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.Configuracion
{
     [Serializable]
     public class ConfiguracionInstitucion
     {
          /// <summary>
          /// Número de la institucion actual    
          /// </summary>
          public short Numero { get; set; }
          /// <summary>
          /// nombre de la institucion actual    
          /// </summary>
          public string Nombre { get; set; }
          /// <summary>
          /// Razón Social de la institucion actual
          /// </summary>
          public string RazonSocial { get; set; }

          /// <summary>
          /// Gets or sets the direccion.
          /// </summary>
          /// <value>
          /// The direccion.
          /// </value>
          public string Direccion { get; set; }

          /// <summary>
          /// Gets or sets the RFC institucion.
          /// </summary>
          /// <value>
          /// The RFC institucion.
          /// </value>
          public string RFC { get; set; }
          /// <summary>
          /// 
          /// </summary>
          public string NombreDirector { get; set; }
     }
}