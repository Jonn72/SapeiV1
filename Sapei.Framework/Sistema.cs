using System;
using System.Collections.Generic;
using System.Text;
using Sapei.Framework.Configuracion;
using Sapei.Framework.BaseDatos;

namespace Sapei.Framework
{
     [Serializable]
     public class Sistema
     {
          #region Variables

          #endregion

          #region Configuraciones del sistema

          
          /// <summary>
          /// Nombre del Sistema
          /// </summary>
          public enmSistema VersionSistema { get; set; }
          /// <summary>
          /// Todas las configuraciones que aplican a nivel de base de datos o servidor central
          /// </summary>
          public ConfiguracionServidor Servidor { get; set; }
          /// <summary>
          /// Todas los datos que se definen a partir de la sesion en donde se logueo el usuario
          /// </summary>
          public ConfiguracionSesion Sesion { get; set; }

          #endregion

          #region Conexion a la base de datos

          /// <summary>
          /// Esta Propiedad sirve para regresar la conexion
          /// se deja la Conexion a nivel de sistema para poder  manejar adecuadamente la transaccionalidad
          /// </summary>          
          public AccesoDatos Conexion { get; set; }

          #endregion

          #region Propiedades del sistema

          /// <summary>
          /// Modo Debug
          /// </summary>
          public bool ModoDebug { get; set; }

          /// <summary>
          /// Modo Log
          /// </summary>
          public bool ModoLog { get; set; }

          #endregion

          #region Constructores

          /// <summary>
          /// Instancia una nueva variable del sistema
          /// </summary>
          public Sistema()
          {
          }

          /// <summary>
          /// Constructor sobre cargado para configurar conforme de un arhivo .ini
          /// </summary>
          /// <param name="psArchivoINI">Nombre del archivo .ini</param>
          /// <param name="penPlataforma">Plataforma del sistema</param>
          /// <param name="penSistema">Version del Sistema</param>
          public Sistema(Sapei.Framework.Configuracion.enmSistema penSistema)
          {
               this.VersionSistema = penSistema;
               this.Constructor(penSistema);
          }

          /// <summary>
          /// Configuracions the comunes.
          /// </summary>
          private void ConfiguracionComunes()
          {
               this.AbreConexion();
               if (!Conexion.ExisteConexion)
                    return;
               using (var loConexion = new ManejaConexion(Conexion))
               {
                    this.ConfiguraParametrosSistema();                  
                    CargaOpcionesdeBD();
               }
          }
          /// <summary>
          /// Cargas the opcionesde bd.
          /// </summary>
          private void CargaOpcionesdeBD()
          {
               StringBuilder lsQuery;
               if (Servidor.Principal.Motor == enmMotor.Sql)
               {
                    lsQuery = new StringBuilder("SELECT SERVERPROPERTY( 'Collation' )");
                    Servidor.Principal.BaseDatos.Collation = string.Format(" COLLATE {0}", Conexion.EjecutaEscalar(lsQuery));
                    lsQuery = new StringBuilder();
                    lsQuery = new StringBuilder();
                    lsQuery.AppendFormat("Select cmptlevel from master..sysdatabases where Name='{0}'", Servidor.Principal.BaseDatos.Catalogo);
                    Servidor.Principal.BaseDatos.NivelCompatibilidad = short.Parse(Convert.ToString(Conexion.EjecutaEscalar(lsQuery).ToString()));
                    //Saca el nivel de compatibilidad
                    Servidor.Principal.BaseDatos.Propietario = "dbo";
               }
          }
          /// <summary>
          /// Periodo Actual
          /// </summary>
          private void CargaPeriodoActual()
          {
               System.Data.SqlClient.SqlDataReader loReader;
               List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
               loReader = Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_periodo_actual", loParametros);

               if (loReader.HasRows)
               {
                    while (loReader.Read())
                    {
                         Sesion.Periodo.PeriodoActual = loReader.GetString(0);
                         Sesion.Periodo.IdentificacionCorta = loReader.GetString(2);
                         Sesion.Periodo.Identificacionlarga = loReader.GetString(1);
                    }
               }
               loReader.Close();
               loReader.Dispose();
          }
          /// <summary>
          /// Funcion que carlos parametros del sistema
          /// </summary>
          private void ConfiguraParametrosSistema()
          {           
               ModoDebug = false;
               ModoLog = false;
               Servidor.Principal.BaseDatos.Parametros.ForzarActualizaciones = false;
               Sesion.Institucion.Numero = Convert.ToInt16(this.GetParametro("idInstitucion"));
               CargaPeriodoActual();
          }
          /// <summary>
          /// Funcion que sirve para abrir la conexion. Este metodo quedo publico porque puede invocarse la clase con el new .
          /// Solito que no carga parametros y no construye la conexion asi puede ejecutarse despues de instanciar la clase
          /// </summary>       
          protected void AbreConexion()
          {
               if (!Object.Equals(this.Conexion, null))
                    return;
               this.Conexion = new AccesoDatos(this.Servidor.Principal.BaseDatos.CadenaConexion, this.Servidor.Principal.BaseDatos.Bitacora, this.Servidor.Principal.Motor, Convert.ToInt32(this.VersionSistema));
          }
          #endregion

          #region Funciones

          
          /// <summary>
          /// Constructors the specified ps archivo ini.
          /// </summary>
          /// <param name="psArchivoINI">El archivo ini.</param>
          /// <param name="penPlataforma">La plataforma.</param>
          /// <param name="penSistema">El sistema.</param>
          /// <param name="psArchivoINIRuta">La ruta del archivo ini.</param>
          private void Constructor(Sapei.Framework.Configuracion.enmSistema penSistema)
          {
               this.Sesion = new ConfiguracionSesion();
               this.Servidor = new ConfiguracionServidor(penSistema);
               this.VersionSistema = penSistema;
               ConfiguracionComunes();       
          }
         

          #region parametros

          /// <summary>
          /// Funcion que permite guardar en xtsParametros para todo (Pais = 0)
          /// </summary>
          /// <param name="psParametro">Nombre del Parametro</param>
          /// <param name="psValor">Valor del parametro</param>
          public void SetParametro(string psParametro, string psValor)
          {
               this.SetParametro(psParametro, psValor, 0, "", "");
          }

          /// <summary>
          /// Funcion que permite guardar en xtsParametros seleccionando el Pais
          /// </summary>
          /// <param name="psParametro">Nombre del Parametro</param>
          /// <param name="psValor">Valor del Parametro</param>
          /// <param name="piPais">Numero del Pais</param>
          public void SetParametro(string psParametro, string psValor, int piPais)
          {
               this.SetParametro(psParametro, psValor, piPais, "", "");
          }

          /// <summary>
          /// Funcion que permite guardar en xtsParametros seleccionando el Pais, asignado una descripcion y el nombre del modulo
          /// donde se utiliza el parametro. Usando la empresa con que se inicio la sesion.
          /// </summary>
          /// <param name="psParametro">Nombre del parametro</param>
          /// <param name="psValor">Valor del parametro</param>
          /// <param name="piPais">Numero del pais</param>
          /// <param name="psModulo">Nombre del modulo donde se utiliza el parametro</param>
          /// <param name="psDescripcion">Descripcion del uso del parametro</param>
          public void SetParametro(string psParametro, string psValor, int piPais, string psModulo, string psDescripcion)
          {
               //this.SetParametro(psParametro, psValor, piPais, psModulo, psDescripcion, this.Sesion.Empresa.Numero);
          }

          /// <summary>
          /// Funcion que permite guardar en xtsParametros configurando cada uno de sus campos
          /// </summary>
          /// <param name="psParametro">Nombre del parametro</param>
          /// <param name="psValor">Valor del parametro</param>
          /// <param name="piPais">Numero del pais</param>
          /// <param name="psModulo">Nombre del modulo donde se utiliza el parametro</param>
          /// <param name="psDescripcion">Descripcion del uso del parametro</param>
          /// <param name="piEmpresa">Numero de empresa donde se utiliza el parametro</param>
          public void SetParametro(string psParametro, string psValor, int piPais, string psModulo, string psDescripcion, int piEmpresa)
          {
               //StringBuilder lsQuery;
               try
               {
                  
               }
               catch (Exception ex)
               {
                    throw new Exception(ex.Message);
               }
               finally
               {
               }
          }  

          /// <summary>
          /// Regresa el pais empresa
          /// </summary>
          /// <param name="psParametro">Nombre del parametro</param>
          public string GetParametro(string psParametro)
          {
               StringBuilder lsQuery;
               Object lsValor = null;
               try
               {

                    lsQuery = new StringBuilder();
                    lsQuery.AppendFormat("Select top 1 Valor From [{0}].[{1}].[{2}sisParametros]", this.Servidor.Principal.BaseDatos.Catalogo, this.Servidor.Principal.BaseDatos.Propietario,this.Servidor.Principal.BaseDatos.Prefijo);
                    lsQuery.Append(" Where");
                    lsQuery.AppendFormat(" Parametro='{0}'", psParametro);
                    lsValor = this.Conexion.EjecutaEscalar(lsQuery);
                    if (lsValor == null)
                    {
                         lsValor = "";
                    }
                    return lsValor.ToString();
               }
               catch (Exception ex)
               {
                    throw new Exception(ex.Message);
               }
          }

          #endregion

          /// <summary>
          /// Funcion que graba en algun error en el LOG
          /// </summary>
          /// <param name="poException">Valor del LOG</param>
          public void GrabaLog(Exception poException, string psModulo = "")
          {
               this.Conexion.GrabaLog(poException, null, psModulo);
          }

          /// <summary>
          /// Grabas the log.
          /// </summary>
          /// <param name="poException">The po exception.</param>
          /// <param name="lsQuery">The ls query.</param>
          public void GrabaLog(Exception poException, StringBuilder lsQuery)
          {
               this.Conexion.GrabaLog(poException, lsQuery);
          }

        /// <summary>
        /// Grabas the log.
        /// </summary>
        /// <param name="poException">The po exception.</param>
        /// <param name="lsQuery">The ls query.</param>
        public void GrabaLog(Exception poException, List<StringBuilder> lsQuery)
          {
               this.Conexion.GrabaLog(poException, lsQuery);
          }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="psCadena"></param>
		/// <param name="psDatos"></param>
		public void GrabaValidacionDocumento(string psCadena, string psDatos)
		{
			StringBuilder lsQuery;
			try
			{

				lsQuery = new StringBuilder();
				lsQuery.AppendFormat("IF NOT EXISTS(SELECT 1 FROM sis_validacion_documentos WHERE cadena = '{0}')",psCadena);
				lsQuery.AppendFormat(" INSERT INTO [{0}].[{1}].[sis_validacion_documentos]", this.Servidor.Principal.BaseDatos.Catalogo, this.Servidor.Principal.BaseDatos.Propietario);
				lsQuery.Append(" (cadena,datos,fecha) values");
				lsQuery.AppendFormat(" ('{0}','{1}',getdate())", psCadena,psDatos);
				this.Conexion.EjecutaComando(lsQuery);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="psCadena"></param>
		public string RecuperaValidacionDocumento(string psCadena)
		{
			try
			{
                using (var loConexion = new ManejaConexion(this.Conexion))
                {
                    System.Data.SqlClient.SqlDataReader loReader;
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    string lsMensaje = "";
                    loParametros.Add(new ParametrosSQL("@cadena", psCadena));

                    loReader = this.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_qr_valida_documento", loParametros);
                    if (loReader.HasRows)
                    {
                        while (loReader.Read())
                        {
                            lsMensaje = loReader.GetString(0);
                        }
                    }
                    loReader.Close();
                    loReader.Dispose();
                    return lsMensaje;
                }
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		#endregion
	}
}
