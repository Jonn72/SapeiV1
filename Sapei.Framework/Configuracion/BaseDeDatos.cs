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
     public class BaseDeDatos
     {
          /// <summary>
          /// Gets or sets the base.
          /// </summary>
          /// <value>
          /// The base.
          /// </value>
          public string Catalogo { get; set; }

          /// <summary>
          /// Gets or sets the bitacora.
          /// </summary>
          /// <value>
          /// The bitacora.
          /// </value>
          public string Bitacora { get; set; }

          /// <summary>
          /// Gets or sets the collation.
          /// </summary>
          /// <value>
          /// The collation.
          /// </value>
          public string Collation { get; set; }

          /// <summary>
          /// Gets or sets the usuario.
          /// </summary>
          /// <value>
          /// The usuario.
          /// </value>
          public string Usuario { get; set; }

          /// <summary>
          /// Gets or sets the contraseña.
          /// </summary>
          /// <value>
          /// The contraseña.
          /// </value>
          public string Contraseña { get; set; }

          /// <summary>
          /// Gets or sets the propietario.
          /// </summary>
          /// <value>
          /// The propietario.
          /// </value>
          public string Propietario { get; set; }

          /// <summary>
          /// Gets or sets the prefijo.
          /// </summary>
          /// <value>
          /// The prefijo.
          /// </value>
          public string Prefijo { get; set; }

          /// <summary>
          /// Gets or sets a value indicating whether [seguridad integrada].
          /// </summary>
          /// <value>
          ///   <c>true</c> if [seguridad integrada]; otherwise, <c>false</c>.
          /// </value>
          public bool SeguridadIntegrada { get; set; }

          /// <summary>
          /// Gets or sets a value indicating whether [conexion exitosa].
          /// </summary>
          /// <value>
          ///   <c>true</c> if [conexion exitosa]; otherwise, <c>false</c>.
          /// </value>
          public bool ConexionExitosa { get; set; }

          /// <summary>
          /// Gets or sets the nivel compatibilidad.
          /// </summary>
          /// <value>
          /// The nivel compatibilidad.
          /// </value>
          public int NivelCompatibilidad { get; set; }

          /// <summary>
          /// Gets or sets the cadena conexion.
          /// </summary>
          /// <value>
          /// The cadena conexion.
          /// </value>
          public string CadenaConexion { get; set; }

          /// <summary>
          /// Gets or sets a value indicating whether [conexion principal].
          /// </summary>
          /// <value>
          ///   <c>true</c> if [conexion principal]; otherwise, <c>false</c>.
          /// </value>
          public bool ConexionPrincipal { get; set; }

          /// <summary>
          /// Gets or sets the parametros.
          /// </summary>
          /// <value>
          /// The parametros.
          /// </value>
          public ConfiguracionParametros Parametros { get; set; }

          /// <summary>
          /// Gets or sets the pagina inicio.
          /// </summary>
          /// <value>
          /// The pagina inicio.
          /// </value>
          public string PaginaInicio { get; set; }
          /// <summary>
          /// Initializes a new instance of the <see cref="BaseDeDatos"/> class.
          /// </summary>
          public BaseDeDatos()
          {
               Parametros = new ConfiguracionParametros();
          }

          public string FormtoTabla(string psTabla)
          {
               StringBuilder lsTabla;
               lsTabla = new StringBuilder();
               lsTabla.AppendFormat("[{0}].[{1}].[{2}{3}]",Catalogo,Propietario,Prefijo,psTabla);
               return lsTabla.ToString();
          }
     }
}
