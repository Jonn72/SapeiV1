using System.Configuration;


namespace Sapei.Framework.BaseDatos
{
     /// <summary>
     /// Clase par ala configuracion de la base de datos en el .config
     /// </summary>
     public class ConfiguracionDatos : ConfigurationSection
     {
          #region constants
          /// <summary>
          /// NOmbre de la seecion para la base de datos del .config
          /// </summary>
          private const string TITLE_PROPERTY_NAME = "BaseDatosDefault";
          //private const string NUMBER_OF_ITEMS_PROPERTY_NAME = "NumberOfItemsPerPage";
          #endregion

          #region properties
          /// <summary>
          /// Ontiene el nombre de la base de datos pordefaul en la secion de la cadena de conexion
          /// </summary>
          [ConfigurationProperty(TITLE_PROPERTY_NAME, DefaultValue = "ConexionBaseDatos", IsRequired = true)]
          public string BaseDatosDefault
          {

               //Las configuraciones son de solo lectura
               get
               {
                    return (string)this[TITLE_PROPERTY_NAME];
               }
          }
          #endregion
     }
     /// <summary>
     /// Clase para manejar las confguraciones
     /// </summary>
     public static class ManejadorConfiguraciones
     {
          #region constants
          /// <summary>
          /// Nombre el nodo de configuraciones
          /// </summary>
          private static string BLOG_SETTINGS_NODE_NAME = "ConfiguracionDatos";

          #endregion

          #region members
          /// <summary>
          /// Regresa el valor de la configuracion
          /// </summary>
          private static ConfiguracionDatos _settings = ConfigurationManager.GetSection(BLOG_SETTINGS_NODE_NAME) as ConfiguracionDatos;

          #endregion

          #region Properties
          /// <summary>
          /// Popiedade de solo lectura para obtener la conexion a la bd
          /// </summary>
          public static ConfiguracionDatos Settings
          {
               get { return _settings; }
          }

          #endregion
     }
}
