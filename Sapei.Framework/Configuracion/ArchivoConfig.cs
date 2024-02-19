using System;
using System.Text;
using System.Xml;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Excepciones;

namespace Sapei.Framework.Configuracion
{
     class ArchivoConfig
     {
          #region Variables
          
          /// <summary>
          /// Variable para carga el documento .config
          /// </summary>
          XmlDocument _oXmlDocument;
          /// <summary>
          /// Variable para agregar un nuevo elemento al .config
          /// </summary>
          private XmlNode _oXMLNode;
          /// <summary>
          /// Variable que almacena el nombre del servidor
          /// </summary>
          private String _sServidor;
          /// <summary>
          /// Variable que almacena el nombre de la base de datos
          /// </summary>
          private String _sBaseDatos;
          /// <summary>
          /// Variable que almacena el nombre del usuario
          /// </summary>
          private String _sUsuario;
          /// <summary>
          /// Variable que almacena la contraseña del usuario
          /// </summary>
          private String _sContrasenia;
          /// <summary>
          /// Variable que almacena el nombre del arhcivo .config que se cargara
          /// </summary>
          private String _sNombreConfig;
          /// <summary>
          /// variable que almacena la ruta del archivo .config
          /// </summary>
          private String _sRuta;
          /// <summary>
          /// Varible que almacena el tipo de autenticacion de la base de datos
          /// </summary>
          private enmAutenticacionSQL _enModoAutenticacion;
          
          #endregion
          
          #region Contructores
          
          /// <summary>
          /// Contructor sobre cargador para determinar el tipo de documento .config que se cargara
          /// </summary>
          /// <param name="penPlataforma">Tipo de plataforma que se cargara</param>
          public ArchivoConfig(Sapei.Framework.Configuracion.enmSistema penSistema)
          {
            System.Reflection.Assembly Asm;

            switch (penSistema)
            {
                case enmSistema.PROCESO:
                    Asm = System.Reflection.Assembly.GetEntryAssembly();
                    _sNombreConfig = string.Format("{0}.exe.config", (Asm.GetName()).Name);
                    _sRuta = string.Format("{0}\\", System.Windows.Forms.Application.StartupPath);
                    break;
                case enmSistema.SAPEI:
                    _sNombreConfig = System.Web.HttpContext.Current.Server.MapPath("~/web.config");
                    _sRuta = "";
                    break;
            }

               
               CargarDocumentoConfig(_sRuta);
          }
          
          #endregion
          
          #region Funciones
          
          /// <summary>
          /// Fncion que carga el documento .config
          /// </summary>
          private void CargarDocumentoConfig(string psRuta = "")
          {
               _oXmlDocument = new XmlDocument();
               _oXmlDocument.Load(string.Format("{0}{1}", psRuta, _sNombreConfig));
          }
          
          /// <summary>
          /// Funcion que permite modificar el valor de la cadena de conexion
          /// Si el usuario y/o contraseña son omitidos se utiliza un modo de configuracion por windows.
          /// </summary>
          ///  <param name="psNombreCadenaConexion">Nombre del servidor</param>
          /// <param name="psServidor">Nombre del servidor</param>
          /// <param name="psBaseDatos">Nombre de la base de datos</param>
          /// <param name="pbConexionPrincipal">Determina si es la cadena principal (true por omision) o la cadena para el manejor de las excepciones (false)</param>
          /// <param name="psUsuario">Nombre del usuario (opcional)</param>
          /// <param name="psContrasenia">Contraseña del usuario (opcional)</param>
          public void ModificarCadenaConexion(String psNombreCadenaConexion, String psServidor, String psBaseDatos, bool pbConexionPrincipal = true, String psUsuario = "", String psContrasenia = "")
          {
               _sServidor = psServidor;
               _sBaseDatos = psBaseDatos;
               _sUsuario = psUsuario;
               _sContrasenia = psContrasenia;
               if (VerificamodoAutenticacionSQL(psUsuario, psContrasenia))
               {
                    _enModoAutenticacion = enmAutenticacionSQL.Windows;
               }
               else
               {
                    _enModoAutenticacion = enmAutenticacionSQL.Mixto;
               }
               if (pbConexionPrincipal)
               {
                    AgregarNodoCadenaConexion(psNombreCadenaConexion);
               }
               else
               {
                    AgregarNodoCadenaConexion(ConstantesExcepciones.NombreConexionBD);
               }
          }
          
          /// <summary>
          /// Funcion que verifica el modo de autenticacion de la base de datos.
          /// En esta funcion verifica que forzosamente existan ambos valores para poder hacer la utenticacion por modo mixto
          /// NO advierte si falta alguna. Si falta alguna selecciona el modo windows par la conexion.
          /// </summary>
          /// <param name="psUsuario">Nombre del usuario</param>
          /// <param name="psContrasenia">Contraseña del usuario</param>
          /// <returns>Retorna true si existen ambos valores, sino retorna false</returns>
          private bool VerificamodoAutenticacionSQL(String psUsuario, String psContrasenia)
          {
               if (psContrasenia != "" && psUsuario != "")
               {
                    return true;
               }
               else
               {
                    return false;
               }
          }
          
          /// <summary>
          /// Funcion que verifica que existe el nodo para la cadena de conexion dentro del .config
          /// </summary>
          private void VerificaNodoCadenaConexion()
          {
               _oXMLNode = _oXmlDocument.SelectSingleNode("//connectionStrings");
               if (Object.ReferenceEquals(_oXMLNode, null))
               {
                    throw new System.InvalidOperationException("seccion connectionStrings no encontrada");
               }
          }
          
          /// <summary>
          /// Funcion que verifica que exista el nodo para los parametros en el .config
          /// </summary>
          private void VerificaNodoParametros()
          {
               _oXMLNode = _oXmlDocument.SelectSingleNode("//appSettings");
               if (Object.ReferenceEquals(_oXMLNode, null))
               {
                    throw new System.InvalidOperationException("seccion appSettings no encontrada");
               }
          }
          
          /// <summary>
          /// Funcion que verica si se tiene el nombre de la base de datos por default
          /// </summary>
          private void VerificaBaseDatosDefault()
          {
               _oXMLNode = _oXmlDocument.SelectSingleNode("//dataConfiguration");
               if (Object.ReferenceEquals(_oXMLNode, null))
               {
                    throw new System.InvalidOperationException("seccion appSettings no encontrada");
               }
          }
          
          /// <summary>
          /// Funcion que agrega el nodo para la cadena de conexion
          /// </summary>
          /// <param name="psNombreConexion">Nombre de la cadena de conexion</param>
          private void AgregarNodoCadenaConexion(String psNombreConexion)
          {
               XmlElement loXmlAddElement;
               VerificaNodoCadenaConexion();
               loXmlAddElement = (XmlElement)_oXMLNode.SelectSingleNode(string.Format("//add[@name='{0}']", psNombreConexion));
               if (!Object.ReferenceEquals(loXmlAddElement, null))
               {
                    //loXmlAddElement.RemoveAll();
                    AgregaAtributosCadenaConexion(ref loXmlAddElement, psNombreConexion);
               }
               else
               {
                    loXmlAddElement = _oXmlDocument.CreateElement("add");
                    AgregaAtributosCadenaConexion(ref loXmlAddElement, psNombreConexion);
                    _oXMLNode.AppendChild(loXmlAddElement);
               }
               EscribeDocumentoConfig();
          }
          
          /// <summary>
          /// Funcion que agrega los atributos a la cadena de conexion dependiendo del modo de autenticacion
          /// </summary>
          /// <param name="poXmlAddElement">Variable tipo XMLElement</param>
          /// <param name="psNombreConexion">Nombre de la conexion</param>
          private void AgregaAtributosCadenaConexion(ref XmlElement poXmlAddElement, String psNombreConexion)
          {
               StringBuilder lsCadenaConexion;
               poXmlAddElement.SetAttribute("name", psNombreConexion);
               lsCadenaConexion = new StringBuilder();
               if (_enModoAutenticacion == enmAutenticacionSQL.Mixto)
               {
                    lsCadenaConexion.AppendFormat("Data Source= {0};Initial Catalog={1};User ID={2};Password={3}", _sServidor, _sBaseDatos, _sUsuario, _sContrasenia);
               }
               else
               {
                    lsCadenaConexion.AppendFormat("Data Source= {0};Initial Catalog={1};Integrated Security=True", _sServidor, _sBaseDatos);
               }
               poXmlAddElement.SetAttribute("connectionString", lsCadenaConexion.ToString());
               poXmlAddElement.SetAttribute("providerName", "System.Data.SqlClient");
          }
          
          /// <summary>
          /// Funcion que permite actualizar o grabar un parametro en la seccion appSettings del .config
          /// </summary>
          /// <param name="psParametro">Nombre del parametro</param>
          /// <param name="psValor">Valor del parametro</param>
          public void GrabarParametro(String psParametro, String psValor)
          {
               XmlElement loXmlAddElement;
               VerificaNodoParametros();
               loXmlAddElement = (XmlElement)_oXMLNode.SelectSingleNode(string.Format("//add[@key='{0}']", psParametro));
               if (!Object.ReferenceEquals(loXmlAddElement, null))
               {
                    loXmlAddElement.SetAttribute("value", psValor);
               }
               else
               {
                    loXmlAddElement = _oXmlDocument.CreateElement("add");
                    loXmlAddElement.SetAttribute("key", psParametro);
                    loXmlAddElement.SetAttribute("value", psValor);
                    _oXMLNode.AppendChild(loXmlAddElement);
               }
               EscribeDocumentoConfig();
          }
          
          /// <summary>
          /// Funcion que escribe los valores en el archivo .config
          /// </summary>
          private void EscribeDocumentoConfig()
          {
               XmlTextWriter lowriter = null;
               try
               {
                    lowriter = new XmlTextWriter(_sRuta, UTF8Encoding.UTF8);
                    lowriter.Formatting = Formatting.Indented;
                    _oXmlDocument.WriteTo(lowriter);
               }
               finally
               {
                    if (!Object.ReferenceEquals(lowriter, null))
                    {
                         lowriter.Flush();
                         lowriter.Close();
                    }
               }
          }
          
          /// <summary>
          /// Funcion que retorna el valor de algun elmento dentro del .config
          /// </summary>
          /// <param name="psKeyName">Nombre del parametro</param>
          /// <returns>Valor del parametro</returns>
          public String ObtenerValorParametro(String psKeyName)
          {
               String lsValor = String.Empty;
               XmlNode loXmlAddElement;
               VerificaNodoParametros();
               loXmlAddElement = _oXMLNode.SelectSingleNode(string.Format("//add [@key=\"{0}\"]", psKeyName));
               if (!Object.ReferenceEquals(loXmlAddElement, null))
               {
                    lsValor = loXmlAddElement.Attributes["value"].InnerText;
               }
               return lsValor;
          }
          
          /// <summary>
          /// Funcion que obtiene la cadena de conexion
          /// </summary>
          /// <param name="psNombreConexion"></param>
          /// <returns></returns>
          public String ObtenerCadenaConexion(String psNombreConexion)
          {
               String lsValor = String.Empty;
               XmlNode loXmlAddElement;
               VerificaNodoCadenaConexion();
               loXmlAddElement = _oXMLNode.SelectSingleNode(string.Format("//add[@name=\"{0}\"]", psNombreConexion));
               if (!Object.ReferenceEquals(loXmlAddElement, null))
               {
                    lsValor = loXmlAddElement.Attributes["connectionString"].InnerText;
               }
               return lsValor;
          }
          
          /// <summary>
          /// Funcion para obtener la base de datos por omision
          /// </summary>
          /// <returns></returns>
          public String ObtenerBaseDatosDefault()
          {
               String lsValor = String.Empty;
               VerificaBaseDatosDefault();
               lsValor = _oXMLNode.Attributes["BaseDatosDefault"].InnerText;
               return lsValor;
          }
     
          #endregion
     }
}