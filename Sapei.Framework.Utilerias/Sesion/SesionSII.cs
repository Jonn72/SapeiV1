
using Sapei.Framework;
using Sapei.Framework.BaseDatos;

namespace Sapei.Framework.Utilerias
{
     /// <summary>
     /// Clase para el manejo de las variables de session para gobweb
     /// </summary>
     public class SesionSapei : Sesion
     {
          #region Constante del Sistema

          /// <summary>
          /// Constate para el password maestro del sistema
          /// </summary>
          public const string Password = "sii2016";
          /// <summary>
          /// onstante paa el usuario maestro del sistema
          /// </summary>
          public const string Usuario = "soporte";
          /// <summary>
          /// Carga de Archivo Gnosis
          /// </summary>
          public const string NombreVariableRutaUploadGnosis = "c:\\UploadsGnosis\\";
          /// <summary>
          /// Constante para la variable del sistema para docentes
          /// </summary>
          public const string NombreVariableTipoUsuario = "siTipoUsuario";
          /// <summary>
          /// Constante para la variable del sistema para docentes
          /// </summary>
          public const string NombreVariableSistemaSii = "soSistemaSII";
         
          /// <summary>
          /// Constante para accesos al sistema
          /// </summary>
          public const string NombreVariableAcceso = "ssAcceso";
          /// <summary>
          /// Constante para accesos al sistema
          /// </summary>
          public const string NombreVariableMensajeFinSesion = "ssMensaeFinSesion";
          /// <summary>
          /// Constante para el acceso al asconfiguraciones del sistema
          /// </summary>
          public const string NombreVariableAccesoConfiguracion = "ssAccesoConfiguracion";
          /// <summary>
          /// Constante para el acceso al sistema
          /// </summary>
          public const string NombreVariableAccesoCambioPassword = "ssAccesoCambiaPassword";
          /// <summary>
          /// Constante para el usuario logueado del sistema
          /// </summary>
          public const string NombreVariableUsuClave = "ssAccesoUsuClave";
          /// <summary>
          /// Constante para el manejo de la clase de drchivos
          /// </summary>
          public const string NombreVariableArchivo = "soArchivo";         
          /// <summary>
          /// Constante para determinar el numero de reporte
          /// </summary>
          public const string NombreVariableReporteGenerales = "ssReporte";
          /// <summary>
          /// Cadena temporal de uso generico
          /// </summary>
          public const string NombreVariableCadenaTemporal = "ssCadenaTemporal";
          /// <summary>
          /// contador temporal de uso generico
          /// </summary>
          public const string NombreVariableContadorTemporal = "siContadorTemporal";
          /// <summary>
          /// contador temporal de uso generico
          /// </summary>
          public const string NombreVariableFechaTemporal = "soFechaTemporal";
          #endregion

          #region Variable de Session para Reportes Generales

          /// <summary>
          /// Variable de session para almacenar el numero del reporte 
          /// </summary>
          public static string Reporte
          {
               get
               {
                    return Lee<string>(NombreVariableReporteGenerales);
               }
               set
               {
                    Escribe(NombreVariableReporteGenerales, value);
               }
          }        
          #endregion
          #region Conexion a la base de datos

          /// <summary>
          /// Esta Propiedad sirve para regresar la conexion
          /// Dejo la Conexion a nivel de sistema para poder  manejar adecuadamente la transaccionalidad
          /// Esta pendiente solo usar esta sola conexion en todo el proyecto esto nos permitira hacer la transaccionalidad en el sistema
          /// </summary>          
          //public DataAccess Conexion { get; set; }
          public AccesoDatos Conexion { get; set; }

          #endregion
          #region Variables de Sesion Manejo del Sistema
          /// <summary>
          /// Variable de sesion para el tipo de usuario
          /// </summary>
          public static Sapei.Framework.Configuracion.enmTipoUsuario TipoUsuario
          {
               get
               {
                    return Lee<Sapei.Framework.Configuracion.enmTipoUsuario>(NombreVariableTipoUsuario);
               }
               set
               {
                    Escribe(NombreVariableTipoUsuario, value);
               }
          }
          /// <summary>
          /// Variable de sesion para el manejor del sistema
          /// </summary>
          public static SistemaSapei Sistema
          {
               get
               {
                    return Lee<SistemaSapei>(NombreVariableSistemaSii);
               }
               set
               {
                    Escribe(NombreVariableSistemaSii, value);
               }
          }


          /// <summary>
          /// Variable de sesion para el manejo de acceso
          /// </summary>
          public static string Index
          {
               get
               {
                    return Lee<string>(NombreVariableAcceso);
               }
               set
               {
                    Escribe(NombreVariableAcceso, value);
               }
          }
          /// <summary>
          /// RutaUploadGnosis
          /// </summary>
          public static string RutaUploadGnosis
          {
               get
               {
                    return Lee<string>(NombreVariableRutaUploadGnosis);
               }
               set
               {
                    Escribe(NombreVariableRutaUploadGnosis, value);
               }
          }
          /// <summary>
          /// Variable de sesion para el manejo de mensajes de fin de sesion
          /// </summary>
          public static string MensajeFinSesion
          {
               get
               {
                    return Lee<string>(NombreVariableMensajeFinSesion);
               }
               set
               {
                    Escribe(NombreVariableMensajeFinSesion, value);
               }
          }
          /// <summary>
          /// Variable de sesion para el acceso a la configuracion
          /// </summary>
          public static bool AccesoConfiguracion
          {
               get
               {
                    return Lee<bool>(NombreVariableAccesoConfiguracion);
               }
               set
               {
                    Escribe(NombreVariableAccesoConfiguracion, value);
               }
          }

          /// <summary>
          /// Variable de sesion para el cambio de password
          /// </summary>
          public static bool AccesoCambioPassword
          {
               get
               {
                    return Lee<bool>(NombreVariableAccesoCambioPassword);
               }
               set
               {
                    Escribe(NombreVariableAccesoCambioPassword, value);
               }
          }

          /// <summary>
          /// Variable de sesion para la usuclave
          /// </summary>
          public static int UsuClave
          {
               get
               {
                    return Lee<int>(NombreVariableUsuClave);
               }
               set
               {
                    Escribe(NombreVariableUsuClave, value);
               }
          }

          #endregion
          #region Variables Auxilieares
          
          #endregion
     }
}
