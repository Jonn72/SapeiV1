using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Sapei.Framework.BaseDatos
{
     /// <summary>
     /// Clase para el manejo de la conexion a la base de datos
     /// </summary>
     [Serializable]
     public class AccesoDatos
     {
          #region Variables

          /// <summary>
          /// Determina si existe la tabla para el manejo de errores
          /// </summary>
          private bool _bExistesisBitacoraErrores;
          /// <summary>
          /// Para grabar el error
          /// </summary>
          private bool _bGrabandoError;


          #endregion

          #region Propiedades

          /// <summary>
          /// Prefijo para la las base datos
          /// </summary>
          public string BDBitacoraPrefijoObj
          {
               get
               {
                    return "sisBitacora_";
               }
          }

          /// <summary>
          /// Motor de base de datos
          /// </summary>
          public enmMotor Motor { get; private set; }

          /// <summary>
          /// NOmbre del servidor
          /// </summary>
          public string Servidor { get; private set; }

          /// <summary>
          /// base de datos principal del servidor
          /// </summary>
          public string Catalogo { get; private set; }

          /// <summary>
          /// Basde de datos para las bitacoras
          /// </summary>
          public string Bitacora { get; private set; }

          /// <summary>
          /// Cadena de conexion
          /// </summary>
          public string CadenaConexion { get; set; }

          /// <summary>
          /// Gets or sets a value indicating whether [existe conexion a la bd].
          /// </summary>
          /// <value>
          ///   <c>true</c> if [existe conexion bd]; otherwise, <c>false</c>.
          /// </value>
          public bool ExisteConexion { get; set; }

          /// <summary>
          /// Gets or sets the version sistema.
          /// </summary>
          /// <value>
          /// The version sistema.
          /// </value>
          public int VersionSistema { get; set; }

          /// <summary>
          /// Gets or sets the prefijo object.
          /// </summary>
          /// <value>
          /// The prefijo object.
          /// </value>
          public string PrefijoObj { get; set; }

          #endregion

          #region Constructures

          /// <summary>
          /// Initializes a new instance of the <see cref="AccesoDatos"/> class.
          /// Esta le pone una cadena seleccionando el catalogo inicial con seguridad integrada por default
          /// </summary>
          /// <param name="psServidor">The ps servidor.</param>
          public AccesoDatos(string psServidor)
          {
               string lsCadena;
               lsCadena = string.Format("Data Source={0};Initial Catalog=Master;Integrated Security=Yes;", psServidor);
               this.ConstructorInterno(lsCadena, "", enmMotor.Sql);
               VerificaConexion();
          }

          /// <summary>
          /// Initializes a new instance of the <see cref="AccesoDatos"/> class.
          /// </summary>
          /// <param name="psServidor">The ps servidor.</param>
          /// <param name="psBaseDatos">The ps base datos.</param>
          public AccesoDatos(string psServidor, string psBaseDatos)
          {
               string lsCadena;
               lsCadena = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=Yes;", psServidor, psBaseDatos);
               this.ConstructorInterno(lsCadena, "", enmMotor.Sql);
               VerificaConexion();
          }

          /// <summary>
          /// Initializes a new instance of the <see cref="AccesoDatos"/> class.
          /// </summary>
          /// <param name="psServidor">The ps servidor.</param>
          /// <param name="psUsuario">The ps usuario.</param>
          /// <param name="psContrasenia">The ps contrasenia.</param>
          public AccesoDatos(string psServidor, string psUsuario, string psContrasenia)
          {
               string lsCadena;
               lsCadena = string.Format("Data Source={0};Initial Catalog=Master;User ID={1};Password={2}; ", psServidor, psUsuario, psContrasenia);
               this.ConstructorInterno(lsCadena, "", enmMotor.Sql);
               VerificaConexion();
          }

          /// <summary>
          /// Initializes a new instance of the <see cref="AccesoDatos"/> class.
          /// </summary>
          /// <param name="psServidor">The ps servidor.</param>
          /// <param name="psBaseDatos">The ps base datos.</param>
          /// <param name="psUsuario">The ps usuario.</param>
          /// <param name="psContrasenia">The ps contrasenia.</param>
          public AccesoDatos(string psServidor, string psBaseDatos, string psUsuario, string psContrasenia)
          {
               string lsCadena;
               lsCadena = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}; ", psServidor, psBaseDatos, psUsuario, psContrasenia);
               this.ConstructorInterno(lsCadena, "", enmMotor.Sql);
               VerificaConexion();
          }

          /// <summary>
          /// Initializes a new instance of the <see cref="AccesoDatos"/> class.
          /// </summary>
          /// <param name="psCadConexion">The ps cad conexion.</param>
          /// <param name="psBDBitacora">The ps bd bitacora.</param>
          /// <param name="penMotor">The pen motor.</param>
          /// <param name="piVersionSistema">The pi version sistema.</param>
          public AccesoDatos(string psCadConexion, string psBDBitacora, Sapei.Framework.BaseDatos.enmMotor penMotor, int piVersionSistema)
          {
               VersionSistema = piVersionSistema;
               ConstructorInterno(psCadConexion, psBDBitacora, penMotor);
               VerificaConexion();
          }

          /// <summary>
          /// Crea una nueva instancia de AccesoDatos con la cadena de conexion,
          /// </summary>
          /// <param name="psCadConexion"></param>
          /// <param name="psBDBitacora"></param>
          /// <param name="penMotor"></param>
          public AccesoDatos(string psCadConexion, string psBDBitacora, Sapei.Framework.BaseDatos.enmMotor penMotor)
          {
               this.ConstructorInterno(psCadConexion, psBDBitacora, penMotor);
               VerificaConexion();
          }

          private void VerificaConexion()
          {
               if (!this.ExisteConexion)
               {
                    return;
               }
               try
               {
                    if (this.ExisteObjetoEnBD("sisBitacoraErrores", "", Bitacora))
                    {
                         this._bExistesisBitacoraErrores = true;
                    }
               }
               catch
               {
                    //TODO: si hay una excepcion al momento de isntanciar la clase, encontrar una mejor manera de manejar este error
               }
          }

          /// <summary>
          /// Constructors the interno.
          /// </summary>
          /// <param name="psCadConexion">The ps cad conexion.</param>
          /// <param name="psBDBitacora">The ps bd bitacora.</param>
          /// <param name="penMotor">The pen motor.</param>
          private void ConstructorInterno(string psCadConexion, string psBDBitacora, Sapei.Framework.BaseDatos.enmMotor penMotor)
          {
               this._bGrabandoError = false;
               this._bExistesisBitacoraErrores = false;
               this.VersionSistema = 0;
               this.Bitacora = psBDBitacora;
               this.CadenaConexion = psCadConexion;
               this.Motor = penMotor;
               this.CargaVariablesServidor(psCadConexion);
               this.ExisteConexionAlaBaseDatos();
          }

          #endregion

          #region Generics
          /// <summary>
          /// Fucnionq ue regresa una tabla generica
          /// </summary>
          /// <param name="psQuery">Consutla</param>
          /// <param name="psTabla">NOmbre de la tabla</param>
          /// <returns></returns>
          public TablaGenerica RegresaTablaGenerica(StringBuilder psQuery, string psTabla)
          {
               DataTable loDataTable = null;
               TablaGenerica loTabla = null;
               try
               {
                    loDataTable = this.RegresaDataTable(psQuery, psTabla);
                    loTabla = new TablaGenerica(psTabla);
                    loTabla.ConvertDataTable2List2(loDataTable, psQuery.ToString());
                    return loTabla;
               }
               finally
               {
                    if (!Object.Equals(loDataTable, null))
                    {
                         loDataTable.Clear();
                         loDataTable.Dispose();
                         loDataTable = null;
                    }
               }
          }
          /// <summary>
          /// Funcion que regresa una coleccion generica List de una clase usando internamente un datatable
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psQuery">Consulta.</param>
          /// <returns></returns>
          public List<T> RegresaListaGenericaDatos<T>(StringBuilder psQuery) where T : class, new()
          {
               DataTable loTabla;
               loTabla = this.RegresaDataTable(psQuery, "TablaGenerica");
               return loTabla.ToEnumerable<T>().ToList();
          }

          /// <summary>
          /// Regresa una coleccion HashSet en base a un campo especifico de la consulta.
          /// Esto permite tener una coleccion en base a un solo campo y sin que los campos se repitan
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psQuery">Consulta.</param>
          /// <param name="psCampo">Campo.</param>
          /// <returns></returns>
          public HashSet<T> RegresaColeccionGenericaEnBaseACampo<T>(StringBuilder psQuery, string psCampo)
          {
               return this.RegresaListaGenericaEnBaseACampo<T>(psQuery, psCampo).ToHash<T>();
          }


          /// <summary>
          /// Regresa una coleccion HashSet en base a un campo especifico de la consulta.
          /// Esto permite tener una coleccion en base a un solo campo y que los campos se repitan
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psQuery">Consulta.</param>
          /// <param name="psCampo">Campo.</param>
          /// <returns></returns>
          public List<T> RegresaListaGenericaEnBaseACampo<T>(StringBuilder psQuery, string psCampo)
          {
               DataTable loTabla;
               if (String.IsNullOrEmpty(psCampo))
               {
                    return null;
               }
               loTabla = this.RegresaDataTable(psQuery, "Tabla");
               if (!loTabla.Columns.Contains(psCampo))
               {
                    return null;
               }
               return loTabla.AsEnumerable().Select(s => s.Field<T>(psCampo)).ToList();
          }
                  

          /// <summary>
          /// Funcion que regresa una coleccion generica HashSet de una clase usando internamente un datatable        
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psQuery">Consulta.</param>
          /// <returns></returns>
          public HashSet<T> RegresaColeccionGenericaDatos<T>(StringBuilder psQuery) where T : class, new()
          {
               DataTable loTabla;
               loTabla = this.RegresaDataTable(psQuery, "TablaGenerica");
               return loTabla.ToEnumerable<T>().ToHash();
          }
         
          /// <summary>
          /// Regresas the coleccion generica datos.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="poParametros">The po parametros.</param>
          /// <returns></returns>
          public HashSet<T> RegresaColeccionGenericaDatos<T>(StringBuilder psQuery, IEnumerable<ParametrosSQL> poParametros) where T : class, new()
          {
               DataTable loTabla;
               loTabla = this.RegresaDataTable(psQuery, poParametros);
               return loTabla.ToEnumerable<T>().ToHash();
          }

          #endregion

          #region Dataset

          /// <summary>
          /// Funcion que regresa dataset
          /// </summary>
          /// <param name="psQuery">Consutla</param>
          /// <param name="psTabla">NOmbre de la tabla</param>
          /// <returns></returns>
          public System.Data.DataSet RegresaDataSet(StringBuilder psQuery, string psTabla)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         return ManejaConexion.FillDataset(psQuery, psTabla);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, psQuery);
                    throw;
               }
          }

          #endregion

          #region Dataview

          /// <summary>
          /// Funcion que regresa un dataview
          /// </summary>
          /// <param name="psQuery">Consutla</param>
          /// <param name="psTabla">NOmbre de la tabla</param>
          /// <returns></returns>
          public System.Data.DataView RegresaDataView(StringBuilder psQuery, string psTabla)
          {
               DataSet ldtsResultado = new DataSet();
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         ldtsResultado = ManejaConexion.FillDataset(psQuery, psTabla);
                    }
                    return ldtsResultado.Tables[psTabla].DefaultView;
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, psQuery);
                    throw;
               }
          }

          #endregion

          #region Datatable

          /// <summary>
          /// Fucnion que regresa un datatable
          /// </summary>
          /// <param name="psQuery">Consutla</param>
          /// <param name="psTabla">NOmbre de la tabla</param>
          /// <returns></returns>
          public System.Data.DataTable RegresaDataTable(StringBuilder psQuery, string psTabla)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {                    
                         return ManejaConexion.FillDataTable(psQuery, psTabla);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, psQuery);
                    throw;
               }
          }

          /// <summary>
          /// Regresas the data table.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <returns></returns>
          public System.Data.DataTable RegresaDataTable(StringBuilder psQuery)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         return ManejaConexion.FillDataTable(psQuery, "Tabla");
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, psQuery);
                    throw;
               }
          }

          /// <summary>
          /// Regresas the data table.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="poParametros">The po parametros.</param>
          /// <returns></returns>
          public System.Data.DataTable RegresaDataTable(StringBuilder psQuery, IEnumerable<ParametrosSQL> poParametros)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         return ManejaConexion.FillDataTable(psQuery, poParametros);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, psQuery);
                    throw;
               }
          }

          #endregion

          #region Ejecuta comando

          /// <summary>
          /// Funcion que ejecuta un comando en la base de datos
          /// </summary>
          /// <param name="psQuery">Consulta</param>
          /// <returns></returns>
          public int EjecutaComando(StringBuilder psQuery)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         return ManejaConexion.EjecutaComando(psQuery);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, psQuery);
                    throw;
               }
          }

          /// <summary>
          /// Funcionq ue ejecuta un comando usando una lista de parametros en la bas de datos
          /// </summary>
          /// <param name="psQuery">Consulta</param>
          /// <param name="poParametros">Lista de parametros</param>
          /// <param name="pbIlimitado">Permite hacer que la conexion no termine antes de ejecutar el comando </param>
          public void EjecutaComando(StringBuilder psQuery, IEnumerable<ParametrosSQL> poParametros, bool pbIlimitado = true)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         ManejaConexion.EjecutaComando(psQuery, poParametros, pbIlimitado);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, psQuery);
                    throw;
               }
          }

          /// <summary>
          /// Ejecutas the comando.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="pbIlimitado">if set to <c>true</c> [pb ilimitado].</param>
          public void EjecutaComando(StringBuilder psQuery, bool pbIlimitado = true)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         ManejaConexion.EjecutaComando(psQuery, null, pbIlimitado);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, psQuery);
                    throw;
               }
          }

          #endregion

          #region Ejecuta escalar

          /// <summary>
          /// Funcion que ejecuta una consutla en la base de datos y retorna un valor
          /// </summary>
          /// <param name="psQuery">Consulta</param>
          /// <returns></returns>
          public object EjecutaEscalar(StringBuilder psQuery)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         return ManejaConexion.EjecutaEscalar(psQuery);
                    }
               }
               catch (Exception ex)
               {
                    //if (ex.HResult != -2146232060 && !( ex.ToString().IndexOf("PRIMAR") > 0 ))
                    if (!(ex.ToString().IndexOf("PRIMAR") > 0))
                    {
                         //Se pone condion para evitar mandar un mensaje cuando se repita la llave duplicada
                         //esto es por la concurrencia en la bd al momento de insertar registros.
                         this.GrabaLog(ex, psQuery);
                         throw;
                    }
                    return null;
               }
          }

          /// <summary>
          /// Ejecutas the escalar.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="poParametros">The po parametros.</param>
          /// <returns></returns>
          public object EjecutaEscalar(StringBuilder psQuery, IEnumerable<ParametrosSQL> poParametros)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         return ManejaConexion.EjecutaEscalar(psQuery, poParametros);
                    }
               }
               catch (Exception ex)
               {
                    //if (ex.HResult != -2146232060 && !( ex.ToString().IndexOf("PRIMAR") > 0 ))
                    if (!(ex.ToString().IndexOf("PRIMAR") > 0))
                    {
                         //Se pone condion para evitar mandar un mensaje cuando se repita la llave duplicada
                         //esto es por la concurrencia en la bd al momento de insertar registros.
                         this.GrabaLog(ex, psQuery);
                         throw;
                    }
                    return null;
               }
          }

          /// <summary>
          /// Ejecutas the escalar.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="poParametros">The po parametros.</param>
          /// <returns></returns>
          public T EjecutaEscalar<T>(StringBuilder psQuery, IEnumerable<ParametrosSQL> poParametros)
          {
               object loResultado;
               Type loTipoDatoNativo = typeof(T);
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                              loResultado = ManejaConexion.EjecutaEscalar(psQuery, poParametros);                       
                    }
                    if (Object.Equals(loResultado, DBNull.Value) || loResultado == null || loResultado.Equals(""))
                    {
                         return default(T);
                    }
                    else
                    {
                         //return ((T)loResultado);
                         return ((T)Convert.ChangeType(loResultado, loTipoDatoNativo));
                    }
               }
               catch (Exception ex)
               {
                    if (ex.HResult != -2146232060 && !(ex.ToString().IndexOf("PRIMAR") > 0))
                    {
                         //Se pone condion para evitar mandar un mensaje cuando se repita la llave duplicada
                         //esto es por la concurrencia en la bd al momento de insertar registros.
                         this.GrabaLog(ex, psQuery);
                         throw;
                    }
                    return default(T);
               }
          }

          /// <summary>
          /// Ejecutas the escalar.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psQuery">The ps query.</param>
          /// <returns></returns>
          public T EjecutaEscalar<T>(StringBuilder psQuery)
          {
               object loResultado;
               Type loTipoDatoNativo = typeof(T);
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                              loResultado = ManejaConexion.EjecutaEscalar(psQuery);
                    }
                    if (Object.Equals(loResultado, DBNull.Value) || loResultado == null || loResultado.Equals(""))
                    {
                         return default(T);
                    }
                    else
                    {
                         //return ((T)loResultado);
                         return ((T)Convert.ChangeType(loResultado, loTipoDatoNativo));
                    }
               }
               catch (Exception ex)
               {
                    if (ex.HResult != -2146232060 && !(ex.ToString().IndexOf("PRIMAR") > 0))
                    {
                         //Se pone condion para evitar mandar un mensaje cuando se repita la llave duplicada
                         //esto es por la concurrencia en la bd al momento de insertar registros.
                         this.GrabaLog(ex, psQuery);
                         throw;
                    }
                    return default(T);
               }
          }

          #endregion

          #region Ejecuta sp

          /// <summary>
          /// Funcion que ejhecuta un store procedure
          /// </summary>
          /// <param name="psNombre">NOmbre del SP</param>
          /// <param name="poParametros">parametros para el SP</param>
          public void EjecutaComandoProcedimientoAlmacenado(string psNombre, IEnumerable<ParametrosSQL> poParametros)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         ManejaConexion.EjecutaComandoProcedimientoAlmacenado(psNombre, poParametros);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, new StringBuilder(psNombre));
                    throw;
               }
          }

          /// <summary>
          /// Ejecutas the comando procedimiento almacenado.
          /// </summary>
          /// <param name="psNombre">The ps nombre.</param>
          public void EjecutaComandoProcedimientoAlmacenado(string psNombre)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         ManejaConexion.EjecutaComandoProcedimientoAlmacenado(psNombre, null);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, new StringBuilder(psNombre));
                    throw;
               }
          }

          /// <summary>
          /// Funciopn que ejecuta un scalar SP
          /// </summary>
          /// <param name="psNombre">Nombre del SP</param>
          /// <param name="poParametros">Parametros del SP</param>
          /// <returns></returns>
          public object EjecutaEscalarProcedimientoAlmacenado(string psNombre, IEnumerable<ParametrosSQL> poParametros)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         return ManejaConexion.EjecutaEscalarProcedimientoAlmacenado(psNombre, poParametros);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, new StringBuilder(psNombre));
                    throw;
               }
          }

          /// <summary>
          /// Funciopn que ejecuta un scalar SP
          /// </summary>
          /// <param name="psNombre">Nombre del SP</param>
          /// <param name="poParametros">Parametros del SP</param>
          /// <returns></returns>
          public SqlDataReader EjecutaProcedimientoAlmacenadoDataReader(string psNombre, IEnumerable<ParametrosSQL> poParametros = null)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         return ManejaConexion.EjecutaProcedimientoAlmacenadoDataReader(psNombre, poParametros);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, new StringBuilder(psNombre));
                    throw;
               }
          }
          /// <summary>
          /// Ejecutas the escalar procedimiento almacenado.
          /// </summary>
          /// <param name="psNombre">The ps nombre.</param>
          /// <returns></returns>
          public object EjecutaEscalarProcedimientoAlmacenado(string psNombre)
          {
               return ManejaConexion.EjecutaEscalarProcedimientoAlmacenado(psNombre, null);
          }

          #endregion

          #region BulkCopuy

          /// <summary>
          /// Inserts the bulk copy.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poTablaDestino">The po tabla destino.</param>
          /// <param name="psTablaDestino">The ps tabla destino.</param>
          public void InsertBulkCopy<T>(IEnumerable<T> poTablaDestino, string psTablaDestino)
          {
               this.InsertBulkCopy(poTablaDestino.ToDataTable(), psTablaDestino);
          }

          /// <summary>
          /// Inserts the bulk copy.
          /// </summary>
          /// <param name="pdsTablaOrigen">The PDS tabla origen.</param>
          /// <param name="psTablaDestino">The ps tabla destino.</param>
          public void InsertBulkCopy(DataSet pdsTablaOrigen, string psTablaDestino)
          {
               this.InsertBulkCopy(pdsTablaOrigen.Tables[0], psTablaDestino);
          }

          /// <summary>
          /// Inserts the bulk copy.
          /// </summary>
          /// <param name="pdvTablaOrigen">The PDV tabla origen.</param>
          /// <param name="psTablaDestino">The ps tabla destino.</param>
          public void InsertBulkCopy(DataView pdvTablaOrigen, string psTablaDestino)
          {
               this.InsertBulkCopy(pdvTablaOrigen.ToTable(), psTablaDestino);
          }

          /// <summary>
          /// Inserts the bulk copy.
          /// </summary>
          /// <param name="pdtTablaOrigen">The PDT tabla origen.</param>
          /// <param name="psTablaDestino">The ps tabla destino.</param>
          public void InsertBulkCopy(DataTable pdtTablaOrigen, string psTablaDestino)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         ManejaConexion.InsertBulkCopy(pdtTablaOrigen, psTablaDestino);
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, new StringBuilder(string.Format("Error InsertBuilCopy{0}", psTablaDestino)));
                    throw;
               }
          }

          #endregion

          #region get/set parametros

          /// <summary>
          /// Funcion que permite guardar en sisParametros para todo (Pais = 0)
          /// </summary>
          /// <param name="psParametro">Nombre del Parametro</param>
          /// <param name="psValor">Valor del parametro</param>
          public void SetParametro(string psParametro, string psValor)
          {
               this.SetParametro(psParametro, psValor, 0, "", "");
          }

          /// <summary>
          /// Funcion que permite guardar en sisParametros seleccionando el Pais
          /// </summary>
          /// <param name="psParametro">Nombre del Parametro</param>
          /// <param name="psValor">Valor del Parametro</param>
          /// <param name="piPais">Numero del Pais</param>
          public void SetParametro(string psParametro, string psValor, int piPais)
          {
               this.SetParametro(psParametro, psValor, piPais, "", "");
          }

          /// <summary>
          /// Funcion que permite guardar en sisParametros seleccionando el Pais, asignado una descripcion y el nombre del modulo
          /// donde se utiliza el parametro. Usando la institucion con que se inicio la sesion.
          /// </summary>
          /// <param name="psParametro">Nombre del parametro</param>
          /// <param name="psValor">Valor del parametro</param>
          /// <param name="piPais">Numero del pais</param>
          /// <param name="psModulo">Nombre del modulo donde se utiliza el parametro</param>
          /// <param name="psDescripcion">Descripcion del uso del parametro</param>
          public void SetParametro(string psParametro, string psValor, int piPais, string psModulo, string psDescripcion)
          {
               this.SetParametro(psParametro, psValor, piPais, psModulo, psDescripcion, 0);
          }

          /// <summary>
          /// Funcion que permite guardar en sisParametros configurando cada uno de sus campos
          /// </summary>
          /// <param name="psParametro">Nombre del parametro</param>
          /// <param name="psValor">Valor del parametro</param>
          /// <param name="piPais">Numero del pais</param>
          /// <param name="psModulo">Nombre del modulo donde se utiliza el parametro</param>
          /// <param name="psDescripcion">Descripcion del uso del parametro</param>
          /// <param name="piinstitucion">Numero de institucion donde se utiliza el parametro</param>
          public void SetParametro(string psParametro, string psValor, int piPais, string psModulo, string psDescripcion, int piinstitucion)
          {
               StringBuilder lsQuery;
               int liNumeroRegistrosAfectados = 0;
               try
               {
                    lsQuery = new StringBuilder();
                    lsQuery.AppendFormat("Update [{0}sisParametros] set", PrefijoObj);
                    lsQuery.AppendFormat(" Valor='{0}'", psValor);
                    if (!string.IsNullOrEmpty(psModulo))
                    {
                         lsQuery.AppendFormat(",Modulo='{0}'", psModulo);
                    }
                    if (!string.IsNullOrEmpty(psDescripcion))
                    {
                         lsQuery.AppendFormat(",Descripcion=''", psDescripcion);
                    }
                    lsQuery.AppendFormat(" Where Pais={0}", piPais);
                    lsQuery.AppendFormat(" And institucion={0}", piinstitucion);
                    lsQuery.AppendFormat(" And Parametro='{0}'", psParametro);
                    liNumeroRegistrosAfectados = EjecutaComando(lsQuery);
                    if (liNumeroRegistrosAfectados == 0)
                    {
                         lsQuery.AppendFormat("Insert into [{0}sisParametros] (Pais,institucion, Parametro,Valor,Modulo,Descripcion) Values(", PrefijoObj);
                         lsQuery.Append(piPais);
                         lsQuery.AppendFormat(",{0}", piinstitucion);
                         lsQuery.AppendFormat(",'{0}'", psParametro);
                         lsQuery.AppendFormat(",'{0}'", psValor);
                         lsQuery.AppendFormat(",'{0}'", psModulo);
                         lsQuery.AppendFormat(",'{0}')", psDescripcion);
                         EjecutaComando(lsQuery);
                    }
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
          /// Regresa el parametro para todos los paises y la institucion actual
          /// </summary>
          /// <param name="psParametro">Nombre del parametro</param>
          /// <returns>Valor del parametro</returns>
          public string GetParametro(string psParametro)
          {
               return this.GetParametro(psParametro, 0);
          }


          /// <summary>
          /// Regresa el valor del parametro buscado
          /// </summary>
          /// <param name="psParametro">Nombre del parametro</param>
          /// <param name="piInstitucion">Numero de la institucion</param>
          /// <returns>Valor del parametro</returns>
          public string GetParametro(string psParametro, int piInstitucion)
          {
               StringBuilder lsQuery;
               Object lsValor = null;
               try
               {
                    lsQuery = new StringBuilder();
                    lsQuery.AppendFormat("Select top 1 Valor From [{0}sisParametros]", PrefijoObj);
                    lsQuery.Append(" Where");
                    lsQuery.AppendFormat(" idInstitucion in (0,{0})", piInstitucion);
                    lsQuery.AppendFormat(" And Parametro='{0}'", psParametro);
                    lsQuery.Append(" Order by idInstitucion desc");
                    lsValor = this.EjecutaEscalar(lsQuery);
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

          #region Metodos
          /// <summary>
          /// Regresas the tablas de la base datos.
          /// </summary>
          /// <returns></returns>
          public List<string> RegresaTablasDeLaBaseDatos()
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat(" Select name From sysobjects Where xtype = 'U' Order by name");
               return this.RegresaListaGenericaEnBaseACampo<string>(lsQuery, "name");
          }
          /// <summary>
          /// Existes the conexion ala base datos.
          /// </summary>
          public void ExisteConexionAlaBaseDatos()
          {
               try
               {
                    using (var loConexion = new ManejaConexion(this.CadenaConexion, this.Motor))
                    {
                         this.ExisteConexion = true;
                    }
               }
               catch
               {
                    this.ExisteConexion = false;
                    throw;
               }
          }

          /// <summary>
          /// Regresas the base datos.
          /// </summary>
          /// <returns></returns>
          public List<string> RegresaBaseDatos()
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat(" USE [Master] ");
               lsQuery.AppendFormat(" Select name From sysdatabases");
               lsQuery.AppendFormat(" USE [{0}] ", this.Catalogo);
               return this.RegresaListaGenericaEnBaseACampo<string>(lsQuery, "name");
          }

          /// <summary>
          /// Funcion que determina si existe una bd datos en el servidor
          /// </summary>
          /// <param name="psNombreBD">Nombre de la bd</param>
          /// <returns></returns>
          public bool ExisteBaseDeDatos(string psNombreBD)
          {
               StringBuilder lsQuery;
               object loValor;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat("SELECT name FROM sysdatabases where name = '{0}'", psNombreBD);
               loValor = this.EjecutaEscalar(lsQuery);
               if (psNombreBD.ToUpper().Trim() == Convert.ToString(loValor).Trim().ToUpper())
               {
                    return true;
               }
               else
               {
                    return false;
               }
          }
          /// <summary>
          /// Regresas the definicio columanasdela tabla.
          /// </summary>
          /// <param name="psPropietario">The ps propietario.</param>
          /// <param name="psTabla">The ps tabla.</param>
          /// <returns></returns>
          public HashSet<DefinicionColumna> RegresaDefinicioColumanasdelaTabla(string psPropietario, string psTabla)
          {
               return this.RegresaColeccionGenericaDatos<DefinicionColumna>(this.ArmaConsultaDefinicioColumanasdelaTablaSegunBD(psPropietario, psTabla));
          }
          public string RegresaNombreColumnas(string psTabla)
          {
               StringBuilder lsbSalida = new StringBuilder();
               HashSet<DefinicionColumna> loDefinicion;
               loDefinicion = this.RegresaDefinicioColumanasdelaTabla("dbo", psTabla);

               foreach (DefinicionColumna loColumna in loDefinicion)
               {
                    lsbSalida.AppendFormat("{0},", loColumna.Nombre);
               }
               return lsbSalida.Remove(lsbSalida.Length - 1, 1).ToString();
          }
          /// <summary>
          /// 
          /// </summary>
          /// <param name="psNombreBD"></param>
          /// <param name="psNombreEsquema"></param>
          /// <returns></returns>
          public bool ExisteEsquemaEnBaseDatos(string psNombreBD, string psNombreEsquema)
          {
               StringBuilder lsQuery;
               object loValor;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat("select name from {0}.sys.schemas where name = '{1}'", psNombreBD, psNombreEsquema);
               loValor = this.EjecutaEscalar(lsQuery);
               if (psNombreEsquema.ToUpper().Trim() == Convert.ToString(loValor).Trim().ToUpper())
               {
                    return true;
               }
               else
               {
                    return false;
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="psNombreBD"></param>
          /// <param name="psNombreEsquema"></param>
          public void CreaEsquemaEnBaseDatos(string psNombreBD, string psNombreEsquema)
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat("USE [{0}] ", psNombreBD);
               lsQuery.AppendFormat(" begin exec ('CREATE SCHEMA [{0}]') end", psNombreEsquema);
               this.EjecutaComando(lsQuery);
          }

          /// <summary>
          /// Creas the base de datos.
          /// </summary>
          /// <param name="psBaseDatos">The ps base datos.</param>
          public void CreaBaseDeDatos(string psBaseDatos)
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat(" CREATE DATABASE {0} ON PRIMARY", psBaseDatos);
               lsQuery.AppendFormat(" (NAME = '{0}', ", psBaseDatos);
               lsQuery.AppendFormat(" FILENAME = '{0}\\{1}.mdf', ", this.RutaFisicaBaseDatos(), psBaseDatos);
               lsQuery.Append(" SIZE = 5MB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB) ");
               lsQuery.AppendFormat(" LOG ON (NAME ='{0}_log', ", psBaseDatos);
               lsQuery.AppendFormat(" FILENAME = '{0}\\{1}_log.ldf', ", this.RutaFisicaLogBaseDatos(), psBaseDatos);
               lsQuery.Append(" SIZE = 1MB, MAXSIZE = 2048GB , FILEGROWTH = 10% )");
               this.EjecutaComando(lsQuery);
          }

          /// <summary>
          /// Rutas the fisica base datos.
          /// </summary>
          /// <returns></returns>
          public string RutaFisicaBaseDatos()
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat("SELECT mf.physical_name ");
               lsQuery.Append(" FROM sys.dm_io_virtual_file_stats(NULL, NULL) AS divfs JOIN sys.master_files AS mf ON mf.database_id = divfs.database_id AND mf.file_id = divfs.file_id");
               lsQuery.AppendFormat(" where  DB_NAME(mf.database_id) = '{0}' and type_desc = 'ROWS'", this.Catalogo);
               return Path.GetDirectoryName(this.EjecutaEscalar<string>(lsQuery));
          }

          /// <summary>
          /// Rutas the fisica log base datos.
          /// </summary>
          /// <returns></returns>
          public string RutaFisicaLogBaseDatos()
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat("SELECT mf.physical_name ");
               lsQuery.Append(" FROM sys.dm_io_virtual_file_stats(NULL, NULL) AS divfs JOIN sys.master_files AS mf ON mf.database_id = divfs.database_id AND mf.file_id = divfs.file_id");
               lsQuery.AppendFormat(" where  DB_NAME(mf.database_id) = '{0}' and type_desc = 'LOG'", this.Catalogo);
               return Path.GetDirectoryName(this.EjecutaEscalar<string>(lsQuery));
          }

          /// <summary>
          /// Verifica que exista un key constraint en alguna tabla
          /// </summary>
          /// <param name="psObjeto">The ps objeto.</param>
          /// <param name="psBaseDatos">The ps base datos.</param>
          /// <returns></returns>
          public bool ExisteRestrinccionPrimariaEnTabla(string psObjeto, string psBaseDatos = "")
          {
               StringBuilder lsQuery;
               string lsValor;
               lsQuery = new StringBuilder();
               try
               {
                    if (!String.IsNullOrEmpty(psBaseDatos))
                    {
                         //Esto sirve para poder conectarse a la BD de la bitacora, si es que esta, está en otra base
                         //si esta en la misma BD el valor del parametro psBaseDatos es vacio
                         lsQuery.AppendFormat("USE [{0}] ", psBaseDatos);
                    }
                    lsQuery.AppendFormat("select CONSTRAINT_NAME from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where table_name ='{0}'", psObjeto);
                    lsValor = Convert.ToString(this.EjecutaEscalar(lsQuery));
                    if (!String.IsNullOrEmpty(psBaseDatos))
                    {
                         lsQuery = new StringBuilder();
                         lsQuery.AppendFormat("USE [{0}] ", this.Catalogo);
                         this.EjecutaEscalar(lsQuery);
                    }
                    if (String.IsNullOrEmpty(lsValor))
                    {
                         return false;
                    }
                    else
                    {
                         return true;
                    }
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, lsQuery);
                    return false;
               }
          }
          /// <summary>
          /// 
          /// </summary>
          /// <param name="psObjeto"></param>
          /// <param name="psTipo"></param>
          /// <param name="psBaseDatos"></param>
          /// <returns></returns>
          public bool ExisteObjetoEnBD(string psObjeto, string psTipo = "", string psBaseDatos = "")
          {
               StringBuilder lsQuery;
               object loValor;
               bool lbTemporal;
               StringBuilder lsFiltro;
               lsQuery = new StringBuilder();
               lsFiltro = null;
               lbTemporal = false;
               if (!String.IsNullOrEmpty(psBaseDatos))
               {
                    if (!this.ExisteBaseDeDatos(psBaseDatos))
                         return false;
                    //Esto sirve para poder conectarse a la BD de la bitacora, si es que esta, está en otra base
                    //si esta en la misma BD el valor del parametro psBaseDatos es vacio
                    lsQuery.AppendFormat("USE [{0}] ", psBaseDatos);
               }
               //Si el valor de la base es null se usa el catalogo principal
               else
               {
                    lsQuery.AppendFormat("USE [{0}] ", this.Catalogo);
               }
               lbTemporal = psObjeto.Substring(0, Math.Min(1, psObjeto.Length)) == "#";
               if (!lbTemporal)
               {
                    lsQuery.AppendFormat("Select Name From sysobjects  Where Name='{0}'", psObjeto);
               }
               else
               {
                    //Si se encuentra en tempdb
                    //lsQuery = "Select Name From tempdb..SysObjects Where Name='" & psObjeto & "'"
                    //Para las tablas temporales se hace un query a la tabla para validar si truena el query o no, y dependiendo de eso
                    //se supone que la tabla existe o no
                    lsQuery.AppendFormat("Select Count(*) From {0}", psObjeto);
               }
               if (!String.IsNullOrEmpty(psTipo))
               {
                    lsFiltro = new StringBuilder();
                    lsFiltro.AppendFormat(" and type ='{0}' ", psTipo);
                    lsQuery.Append(lsFiltro);
               }
               try
               {
                    loValor = this.EjecutaEscalar(lsQuery);
                    if (!lbTemporal)
                    {
                         if (psObjeto.ToUpper().Trim() == Convert.ToString(loValor).Trim().ToUpper())
                         {
                              return true;
                         }
                         else
                         {
                              return false;
                         }
                    }
                    else
                    {
                         return true;
                    }
               }
               catch (Exception ex)
               {
                    if (!lbTemporal)
                    {
                         this.GrabaLog(ex, lsQuery);
                         throw;
                    }
                    else
                    {
                         return false;
                    }
               }
               finally
               {
               }
          }

          /// <summary>
          /// Fucion que graba en la tbala de errores del sistema
          /// </summary>
          /// <param name="poException">The po exception.</param>
          /// <param name="psConsulta">The ps consulta.</param>
          public void GrabaLog(Exception poException, StringBuilder psConsulta, string psModulo = "Sistema")
          {
               if (this._bGrabandoError)
               {
                    //con esta bandera controlamos que no se llame de forma ciclica esta función
                    return;
               }
               if (psConsulta == null)
               {
                    psConsulta = new StringBuilder();
               }
               this._bGrabandoError = true;
               //Que cuando no exista la tabla sisLog la cree                                        
               this.CreaTablaBitacoraErrores();
               this.InsertaError(poException, psConsulta, psModulo);
               //Siempre desactiva la bandera para evitar que se quede prendida
               this._bGrabandoError = false;
               psConsulta = psConsulta.Replace("'", "''");
          }

          /// <summary>
          /// Grabas the log.
          /// </summary>
          /// <param name="poException">The po exception.</param>
          public void GrabaLog(Exception poException)
          {
               GrabaLog(poException, new StringBuilder());
          }

          /// <summary>
          /// Grabas the log.
          /// </summary>
          /// <param name="poException">The po exception.</param>
          /// <param name="psConsulta">The ps consulta.</param>
          public void GrabaLog(Exception poException, List<StringBuilder> psConsulta)
          {
               foreach (StringBuilder lsQuery in psConsulta)
               {
                    GrabaLog(poException, lsQuery);
               }
          }

          /// <summary>
          /// Insertas the error.
          /// </summary>
          /// <param name="poException">The po exception.</param>
          /// <param name="psConsulta">The ps consulta.</param>
          private void InsertaError(Exception poException, StringBuilder psConsulta, string psModulo = "Sistema")
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               try
               {
                    if (!String.IsNullOrEmpty(this.Bitacora))
                    {
                         lsQuery.AppendFormat("Insert into [{0}].[dbo].[sis_bitacora_errores]", this.Bitacora);
                    }
                    else
                    {
                         lsQuery.AppendFormat("Insert into [{0}].[dbo].[sis_bitacora_errores]", this.Catalogo);
                    }
                    lsQuery.AppendFormat("(tipo,version_sistema,mensaje,modulo,fuente,consulta,base_datos_origen) ");
                    lsQuery.AppendFormat("values ('{0}'", poException.GetType());
                    lsQuery.AppendFormat(",'{0}'", this.VersionSistema);
                    lsQuery.AppendFormat(",'{0}'", poException.Message.Replace("'", "''"));
                    lsQuery.AppendFormat(",'{0}'", psModulo);
                    lsQuery.AppendFormat(",'{0}'", poException.ToString().Replace("'", "''"));
                    lsQuery.AppendFormat(",'{0}'", psConsulta);
                    lsQuery.AppendFormat(",'{0}')", this.Catalogo);
                    this.EjecutaComando(lsQuery);
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, lsQuery);
               }
          }

          /// <summary>
          /// Creas the tabla sisbitacoraErrores.
          /// </summary>
          private void CreaTablaBitacoraErrores()
          {
               StringBuilder lsQuery;
               if (this._bExistesisBitacoraErrores)
               {
                    return;
               }
               lsQuery = new StringBuilder();
               try
               {
                    if (!String.IsNullOrEmpty(this.Bitacora))
                    {
                         lsQuery.AppendFormat("CREATE TABLE [{0}].[dbo].[sis_bitacora_errores](", this.Bitacora);
                    }
                    else
                    {
                         lsQuery.AppendFormat("CREATE TABLE [{0}].[dbo].[sis_bitacora_errores](", this.Catalogo);
                    }
                    lsQuery.Append(" [id] int IDENTITY(1,1) NOT NULL,");
                    lsQuery.Append(" [tipo][varchar](100) not null,");
                    lsQuery.Append(" [version_sistema][tinyint] not null,");
                    lsQuery.Append(" [fecha] datetime NULL CONSTRAINT [DF_sis_bitacora_errores_Fecha]  DEFAULT (getdate()),");
                    lsQuery.Append(" [mensaje] [varchar](max) NULL,");
                    lsQuery.Append(" [modulo] [varchar](max) NULL,");
                    lsQuery.Append(" [fuente] [varchar](max) NULL,");
                    lsQuery.Append(" [consulta] [varchar](max) NULL,");
                    lsQuery.Append(" [base_datos_origen] [varchar] (50) not NULL,");
                    lsQuery.Append(" [reportado][bit] not null default(0),");
                    lsQuery.Append(" CONSTRAINT [PK_sis_bitacora_errores] PRIMARY KEY CLUSTERED ");
                    lsQuery.Append(" (ID Asc)");
                    lsQuery.Append(" )");
                    this.EjecutaComando(lsQuery);
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, lsQuery);
               }
          }

          /// <summary>
          /// Funcion que crea una vista
          /// </summary>
          /// <param name="pscstName"></param>
          /// <param name="psCstSQL"></param>
          /// <param name="pbValida"></param>
          /// <param name="psTipo"></param>
          /// <param name="psBaseDatos"></param>
          public void CreaConsulta(string pscstName, StringBuilder psCstSQL, bool pbValida = true, string psTipo = "", string psBaseDatos = "")
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               try
               {
                    if (pbValida)
                    {
                         if (!this.EliminaConsulta(pscstName, psTipo, psBaseDatos))
                         {
                              return;
                         }
                    }

                    lsQuery.AppendFormat("Create View [dbo].[{0}] as {1}", pscstName, psCstSQL);
                    this.EjecutaComando(lsQuery);
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, lsQuery);
                    throw;
               }
          }

          /// <summary>
          /// Funcion que elemina una vista de la bd
          /// </summary>
          /// <param name="pscstName"></param>
          /// <param name="psTipo"></param>
          /// <param name="psBaseDatos"></param>
          /// <returns></returns>
          public bool EliminaConsulta(string pscstName, string psTipo = "", string psBaseDatos = "")
          {
               StringBuilder lsQuery;
               string lsPropietario;
               lsQuery = new StringBuilder();
               try
               {
                    lsPropietario = this.ObtenerPropietarioTabla(pscstName);
                    if (String.IsNullOrEmpty(lsPropietario))
                    {
                         return true;
                    }

                    lsQuery.AppendFormat("DROP VIEW [{1}].[{0}]", pscstName, lsPropietario);
                    this.EjecutaComando(lsQuery);
                    return true;
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, lsQuery);
                    return false;
               }
          }

          /// <summary>
          /// Obteners the propietario tabla.
          /// </summary>
          /// <param name="psObjeto">The ps objeto.</param>
          /// <returns></returns>
          public string ObtenerPropietarioTabla(string psObjeto)
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               try
               {
                    lsQuery.Append("SELECT su.name FROM ");
                    lsQuery.Append(" sysobjects so JOIN sysusers su");
                    lsQuery.Append(" on so.uid = su.uid");
                    lsQuery.AppendFormat(" where so.name like '{0}'", psObjeto);
                    return Convert.ToString(this.EjecutaEscalar(lsQuery));
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, lsQuery);
                    throw;
               }
          }

          /// <summary>
          /// Funcion que dertemirna si existe un campo en la tabla
          /// </summary>
          /// <param name="psTabla">NOmbre de la tabla</param>
          /// <param name="psCampo">Nombre del campo</param>
          /// <param name="pbAzure"></param>
          /// <returns></returns>
          public bool ExisteCampoEnTabla(string psTabla, string psCampo, bool pbAzure = false)
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               if (pbAzure)
               {
                    //version windows azure
                    lsQuery.Append("SELECT    count (sys.columns.name) Columna ");
                    lsQuery.Append(" FROM sys.columns");
                    lsQuery.Append(" INNER JOIN sys.objects ");
                    lsQuery.Append(" ON  (sys.columns.object_id = sys.objects.object_id) ");
                    lsQuery.Append(" LEFT OUTER JOIN sys.index_columns ");
                    lsQuery.Append(" ON  (sys.columns.object_id = sys.index_columns.object_id  ");
                    lsQuery.Append(" AND sys.columns.column_id = sys.index_columns.column_id  ");
                    lsQuery.Append(" AND (sys.index_columns.index_id <= 1)) ");
                    lsQuery.Append(" INNER JOIN sys.types ");
                    lsQuery.Append(" ON  (sys.columns.system_type_id = sys.types.system_type_id  ");
                    lsQuery.Append(" AND sys.types.system_type_id = sys.types.user_type_id) ");
                    //Se hizo un upper para que la función no sea sensible a mayusculas y minusculas
                    lsQuery.AppendFormat(" WHERE      UPPER(sys.objects.name) = '{0}' ", psTabla.ToUpper());
                    lsQuery.AppendFormat(" AND UPPER(sys.columns.name) = '{0}'", psCampo.ToUpper());
               }
               else
               {
                    lsQuery.Append("SELECT     count (syscolumns.name) Columna");
                    lsQuery.Append(" FROM   syscolumns INNER JOIN");
                    lsQuery.Append(" sysobjects ON ");
                    lsQuery.Append(" (syscolumns.id = sysobjects.id) LEFT OUTER JOIN");
                    lsQuery.Append(" sysindexkeys ON ");
                    lsQuery.Append(" (syscolumns.id = sysindexkeys.id ");
                    lsQuery.Append(" AND syscolumns.colid = sysindexkeys.colid ");
                    lsQuery.Append(" AND (sysindexkeys.indid <= 1)) INNER JOIN");
                    lsQuery.Append(" systypes ON ");
                    lsQuery.Append(" (syscolumns.xtype = systypes.xtype ");
                    lsQuery.Append(" AND systypes.xtype = systypes.xusertype)");
                    lsQuery.Append(" WHERE     ");
                    //Se hizo un upper para que la función no sea sensible a mayusculas y minusculas
                    lsQuery.AppendFormat(" UPPER(sysobjects.name) = '{0}' ", psTabla.ToUpper());
                    lsQuery.AppendFormat(" AND UPPER(syscolumns.name) = '{0}'", psCampo.ToUpper());
               }
               try
               {
                    if (Convert.ToInt32(this.EjecutaEscalar(lsQuery)) > 0)
                    {
                         return true;
                    }
                    return false;
               }
               catch (Exception ex)
               {
                    this.GrabaLog(ex, lsQuery);
                    throw;
               }
          }

          /// <summary>
          /// Regresas the schema tabla.
          /// </summary>
          /// <param name="psTabla">The ps tabla.</param>
          /// <returns></returns>
          public string RegresaSchemaTabla(string psTabla)
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.Append(" Select Table_Schema from INFORMATION_SCHEMA.TABLES ");
               lsQuery.AppendFormat(" where Table_Name = '{0}'", psTabla);
               return this.EjecutaEscalar<string>(lsQuery);
          }

          /// <summary>
          /// Regresas the todaslas tablasdela bd.
          /// </summary>
          /// <returns></returns>
          public DataTable RegresaTodaslasTablasdelaBD()
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.Append(" Select Table_Schema,Table_Name from INFORMATION_SCHEMA.TABLES ");
               lsQuery.Append(" where table_type = 'BASE TABLE' and substring(TABLE_NAME,1,1) <> '_'");
               lsQuery.Append(" order by TABLE_SCHEMA,TABLE_NAME");
               return this.RegresaDataTable(lsQuery, "Tablas");
          }

          /// <summary>
          /// Armas the consulta definicio columanasdela tabla segun bd.
          /// </summary>
          /// <param name="psPropietario">The ps propietario.</param>
          /// <param name="psTabla">The ps tabla.</param>
          /// <returns></returns>
          private StringBuilder ArmaConsultaDefinicioColumanasdelaTablaSegunBD(string psPropietario, string psTabla)
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.Append(" SELECT C.Name Nombre ,ST.Name TipoDato ,STNativo.Name TipoNativo  ,C.max_Length Longitud  ,C.precision Precision  ,C.column_id Orden ");
               lsQuery.Append(" ,CASE IsNull(PKC.Columna,'NULA') when 'NULA' then 0 else 1 END EsPK  ,C.Is_Nullable PermiteNulo ");
               lsQuery.Append(" ,CASE ST.Name ");
               lsQuery.Append("    WHEN 'int' THEN 'Int32'");
               lsQuery.Append("    WHEN 'bit' THEN 'Boolean'");
               lsQuery.Append("    WHEN 'binary' THEN 'Byte'");
               lsQuery.Append("    WHEN 'varbinary' THEN 'Byte[]'");
               lsQuery.Append("    WHEN 'image' THEN 'Byte[]'");
               lsQuery.Append("    WHEN 'char' THEN 'string'");
               lsQuery.Append("    WHEN 'nchar' THEN 'string'");
               lsQuery.Append("    WHEN 'bigint' THEN 'Int64'");
               lsQuery.Append("    WHEN 'smallint' THEN 'Int16'");
               lsQuery.Append("    WHEN 'tinyint' THEN 'Byte'");
               lsQuery.Append("    WHEN 'ntext' THEN 'string'");
               lsQuery.Append("    WHEN 'varchar' THEN 'string'");
               lsQuery.Append("    WHEN 'nvarchar' THEN 'string'");
               lsQuery.Append("    WHEN 'text' THEN 'string'");
               lsQuery.Append("    WHEN 'datetime' THEN 'DateTime'");
               lsQuery.Append("    WHEN 'time' THEN 'DateTime'");
               lsQuery.Append("    WHEN 'datetime2' THEN 'DateTime'");
               lsQuery.Append("    WHEN 'datetimeoffset' THEN 'DateTimeOffset'");
               lsQuery.Append("    WHEN 'smalldatetime' THEN 'DateTime'");
               lsQuery.Append("    WHEN 'decimal' THEN 'Double'");
               lsQuery.Append("    WHEN 'numeric' THEN 'Double'");
               lsQuery.Append("    WHEN 'smallmoney' THEN 'Double'");
               lsQuery.Append("    WHEN 'money' THEN 'Double'");
               lsQuery.Append("    WHEN 'real' THEN 'Single'");
               lsQuery.Append("    WHEN 'float' THEN 'Double'");
               lsQuery.Append("    WHEN 'sql_variant' THEN 'object'");
               lsQuery.Append(" ELSE 'string'");
               lsQuery.Append(" END TipoCSharp");
               lsQuery.Append(" ,CASE ST.Name ");
               lsQuery.Append("    WHEN 'datetime' THEN 1");
               lsQuery.Append("    WHEN 'time' THEN 1");
               lsQuery.Append("    WHEN 'datetime2' THEN 1");
               lsQuery.Append("    WHEN 'datetimeoffset' THEN 1");
               lsQuery.Append("    WHEN 'smalldatetime' THEN 0");
               lsQuery.Append(" ELSE 0");
               lsQuery.Append(" END IncluyeHoras");
               lsQuery.Append(" , replace(replace(isnull(CO.definition,'NULL'),'(',''),')','') ValorPredeterminado ,SEP.value Descripcion");
               lsQuery.Append("  ,C.is_identity EsIdentity");
               lsQuery.Append(" FROM sys.columns C ");
               lsQuery.Append("    INNER JOIN sys.objects O ON (C.object_id = O.object_id) ");
               lsQuery.Append("    INNER JOIN sys.types ST ON (C.system_type_id = ST.system_type_id AND C.user_type_id = ST.user_type_id) ");
               lsQuery.Append("    INNER JOIN sys.types STNativo ON (C.system_type_id = STNativo.system_type_id  AND STNativo.system_type_id = STNativo.user_type_id) ");
               lsQuery.Append(" LEFT JOIN (		");
               lsQuery.Append(" select c.name as Columna ");
               lsQuery.Append(" from   sys.indexes i ");
               lsQuery.Append(" join   sys.objects o  ON i.object_id = o.object_id ");
               lsQuery.Append(" join   sys.objects pk ON i.name = pk.name AND pk.parent_object_id = i.object_id AND pk.type = 'PK' ");
               lsQuery.Append(" join   sys.index_columns ik on i.object_id = ik.object_id and i.index_id = ik.index_id ");
               lsQuery.Append(" join   sys.columns c ON ik.object_id = c.object_id AND ik.column_id = c.column_id INNER JOIN sys.schemas z ON o.schema_id = Z.schema_id ");
               lsQuery.AppendFormat(" where  o.name = '{0}' and z.name = '{1}') ", psTabla, psPropietario);
               lsQuery.Append(" PKC on PKC.Columna=C.Name ");
               lsQuery.Append(" Left join sys.default_constraints CO on (C.default_object_id = CO.object_id)");
               lsQuery.Append(" left join  sys.extended_properties SEP on(C.object_id = SEP.major_id and  C.column_id = SEP.minor_id and SEP.name = 'MS_Description')");
               lsQuery.Append(" INNER JOIN sys.schemas z ON o.schema_id = Z.schema_id ");
               lsQuery.AppendFormat(" WHERE O.name = '{0}' and z.name = '{1}' ", psTabla, psPropietario);
               lsQuery.Append(" Order by orden");
               return lsQuery;
          }
         

          /// <summary>
          /// Funciopn que recura el nombre del servidor de la cadena de conexion
          /// </summary>
          /// <param name="psCadConexion"></param>
          private void CargaVariablesServidor(string psCadConexion)
          {
               SqlConnectionStringBuilder loCadConexion = new SqlConnectionStringBuilder(psCadConexion);
               this.Servidor = loCadConexion.DataSource;
               this.Catalogo = loCadConexion.InitialCatalog;
          }

          #endregion
     }
}
