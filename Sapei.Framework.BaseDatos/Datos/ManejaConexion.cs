using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sapei.Framework.BaseDatos
{
     /// <summary>
     /// Clase para el manejo de la conexion a la base de datos
     /// </summary>
     public class ManejaConexion : IDisposable
     {
          #region Objetos de conexion

          /// <summary>
          /// HIlo unico para la conexion
          /// </summary>
          [ThreadStatic]
          static ManejaConexion _oCurrentScope;
          /// <summary>
          /// Hilo unico para las trasacciones
          /// </summary>
          [ThreadStatic]
          static SqlTransaction _oSqlTransaccionActual;
          /// <summary>
          /// Hilo actual para las conexiones
          /// </summary>
          [ThreadStatic]
          static SqlConnection _oSqlConexionActual;

          /// <summary>
          /// La cadena de conecion no es thread static Cadena de coenxion
          /// </summary>
          [ThreadStatic]
          static string _sCadenaConexion;
          /// <summary>
          /// Para el motor de la base de datos a la que se conectara
          /// </summary>
          [ThreadStatic]
          static enmMotor _enmMotor;
          //// <summary>
          //// Determinar si la conexion esta abierta anteriormente
          //// </summary>
          //static bool _bConexionAbierta;          

          #endregion

          #region Variables

          /// <summary>
          /// 
          /// </summary>
          private bool _isDisposed;
          /// <summary>
          /// Esta variable es para determinar quien fue la primera instancia. Ya que esta tendra un false como valor.
          /// Y se encargara de hacer el Dispose de la conexion
          /// </summary>
          private bool _isNested;

          #endregion

          #region Propiedades

          /// <summary>
          /// 
          /// </summary>
          public static ManejaConexion CurrentScope
          {
               get
               {
                    return _oCurrentScope;
               }
          }

          /// <summary>
          /// Conexion SQL
          /// </summary>
          public static SqlTransaction SqlTransaccionActual
          {
               get
               {
                    return _oSqlTransaccionActual;
               }
          }

          /// <summary>
          /// Transaccion SQL
          /// </summary>
          public static SqlConnection SqlConexionActual
          {
               get
               {
                    return _oSqlConexionActual;
               }
          }
          #endregion

          #region Constructores

          /// <summary>
          /// Initializes a new instance of the <see cref="ManejaConexion"/> class.
          /// </summary>
          /// <param name="poConexion">The po conexion.</param>
          public ManejaConexion(AccesoDatos poConexion)
               : this(poConexion.CadenaConexion, poConexion.Motor)
          {
          }

          /// <summary>
          /// Initializes a new instance of the <see cref="ManejaConexion"/> class.
          /// </summary>
          public ManejaConexion()
               : this(_sCadenaConexion, _enmMotor)
          {
          }

          /// <summary>
          /// Initializes a new instance of the <see cref="ManejaConexion"/> class.
          /// </summary>
          /// <param name="psCadenaConexion">The ps cadena conexion.</param>
          /// <param name="penmMotor">The penm motor.</param>
          public ManejaConexion(string psCadenaConexion, enmMotor penmMotor)
          {
               _sCadenaConexion = psCadenaConexion;
               _enmMotor = penmMotor;
               if (!Object.Equals(_oCurrentScope, null) && !_oCurrentScope._isDisposed)
               {
                    //this.ConexionActual = _currentScope.ConexionActual;
                    _isNested = true;
                    //_bConexionAbierta = true;
               }
               else
               {
                    IniciaConexion();
                    Thread.BeginThreadAffinity();
                    _oCurrentScope = this;
               }
          }

          #endregion

          #region Manejo de conexiones y transacciones

          /// <summary>
          /// Inicia la conexion a la base de datos
          /// </summary>
          private static void IniciaConexion()
          {
               //Crear la nueba conecion de datos
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         _oSqlConexionActual = new SqlConnection(_sCadenaConexion);
                         if (!(_oSqlConexionActual.State == System.Data.ConnectionState.Open))
                              _oSqlConexionActual.Open();
                         break;
                    case enmMotor.Oracle:
                         break;
                    case enmMotor.Oledb:
                         break;
               }
          }

          /// <summary>
          /// Metodo para heredar a la conexion una transaccion
          /// </summary>
          public static void IniciaTransaccion()
          {
               if (ManejaConexion.CurrentScope == null)
                    return;
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual != null)
                              return;
                         var lvConnectionSql = ManejaConexion.SqlConexionActual;
                         //if (!_bConexionAbierta)                       
                         if (!(lvConnectionSql.State == System.Data.ConnectionState.Open))
                         {
                              lvConnectionSql.Open();
                         }
                         _oSqlTransaccionActual = lvConnectionSql.BeginTransaction();
                         break;
                    case enmMotor.Oracle:          
                         break;
                    case enmMotor.Oledb:
                         break;
               }
          }

          /// <summary>
          /// Funcion para confirmar la transaccion
          /// </summary>
          public static void ConfirmarTransaccion()
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual != null)
                         {
                              _oSqlTransaccionActual.Commit();
                              _oSqlTransaccionActual = null;
                         }
                         break;
                    case enmMotor.Oracle:
                         break;
                    case enmMotor.Oledb:
                         break;
               }
          }

          /// <summary>
          /// Fncion a para deshacer la transaccion
          /// </summary>
          public static void DeshacerTransaccion()
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual != null)
                         {
                              _oSqlTransaccionActual.Rollback();
                              _oSqlTransaccionActual = null;
                         }
                         break;
                    case enmMotor.Oracle:
                         break;
                    case enmMotor.Oledb:
                         break;
               }
          }

          /// <summary>
          /// Libera lso recursos de la conexion
          /// </summary>
          public void Dispose()
          {
               if (!_isNested && !_isDisposed)
               {
                    switch (_enmMotor)
                    {
                         case enmMotor.Sql:
                              if (_oSqlTransaccionActual != null)
                              {
                                   _oSqlTransaccionActual.Dispose();
                                   _oSqlTransaccionActual = null;
                              }
                              if (_oSqlConexionActual != null)
                              {
                                   _oSqlConexionActual.Dispose();
                                   _oSqlConexionActual = null;
                              }
                              break;
                         case enmMotor.Oracle:
                              break;
                         case enmMotor.Oledb:
                              break;
                    }
                    _oCurrentScope = null;
                    //_bConexionAbierta = false;
                    Thread.EndThreadAffinity();
                    _isDisposed = true;
               }
          }

          #endregion

          #region Dataset

          /// <summary>
          /// Fills the dataset.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="psTabla">The ps tabla.</param>
          /// <returns></returns>
          public static DataSet FillDataset(StringBuilder psQuery, string psTabla)
          {
               DataSet ldtsResultado = new DataSet();
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              SqlHelper.FillDataset(_oSqlConexionActual, CommandType.Text, psQuery.ToString(), ldtsResultado, new string[] { psTabla });
                         else
                              SqlHelper.FillDataset(_oSqlTransaccionActual, CommandType.Text, psQuery.ToString(), ldtsResultado, new string[] { psTabla });
                         break;
                    case enmMotor.Oracle:                         
                         break;
                    case enmMotor.Oledb:
                         break;
               }
               return ldtsResultado;
          }

          #endregion

          #region DataReader
          /// <summary>
          /// Fills the data reader.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <returns></returns>
          public static IDataReader FillDataReader(StringBuilder psQuery)
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              return SqlHelper.FillDataReader(_oSqlConexionActual, CommandType.Text, psQuery);
                         else
                              return SqlHelper.FillDataReader(_oSqlTransaccionActual, CommandType.Text, psQuery);
                    case enmMotor.Oracle:
                         return null;

                    case enmMotor.Oledb:
                         return null;
               }
               return null;
          }
          /// <summary>
          /// Fills the data reader.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="poParametros">The po parametros.</param>
          /// <returns></returns>
          public static IDataReader FillDataReader(StringBuilder psQuery, IEnumerable<ParametrosSQL> poParametros)
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              return SqlHelper.FillDataReader(_oSqlConexionActual, CommandType.Text, psQuery, poParametros);
                         else
                              return SqlHelper.FillDataReader(_oSqlTransaccionActual, CommandType.Text, psQuery, poParametros);
                    case enmMotor.Oracle:
                         return null;

                    case enmMotor.Oledb:
                         return null;
               }
               return null;
          }

          #endregion

          #region DataView

          #endregion

          #region DataTable

          /// <summary>
          /// Fills the data table.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="psTabla">The ps tabla.</param>
          /// <returns></returns>
          public static DataTable FillDataTable(StringBuilder psQuery, string psTabla)
          {
               DataTable ldtResultado = new DataTable();
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              SqlHelper.FillDataTable(_oSqlConexionActual, CommandType.Text, psQuery.ToString(), ldtResultado, new string[] { psTabla });
                         else
                              SqlHelper.FillDataTable(_oSqlTransaccionActual, CommandType.Text, psQuery.ToString(), ldtResultado, new string[] { psTabla });
                         break;
                    case enmMotor.Oracle:                                                 
                         break;
                    case enmMotor.Oledb:
                         break;
               }
               return ldtResultado;
          }
          /// <summary>
          /// Fills the data table.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <returns></returns>
          public static DataTable FillDataTable(StringBuilder psQuery)
          {
               return FillDataTable(psQuery, "Tabla");
          }
          /// <summary>
          /// Fills the data table.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="poParametros">The po parametros.</param>
          /// <returns></returns>
          public static DataTable FillDataTable(StringBuilder psQuery, IEnumerable<ParametrosSQL> poParametros)
          {
               DataTable ldtResultado = new DataTable();
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              SqlHelper.FillDataTable(_oSqlConexionActual, CommandType.Text, psQuery, ldtResultado, poParametros);
                         else
                              SqlHelper.FillDataTable(_oSqlTransaccionActual, CommandType.Text, psQuery, ldtResultado, poParametros);
                         break;
                    case enmMotor.Oracle:

                         break;
                    case enmMotor.Oledb:

                         break;
               }
               return ldtResultado;
          }

          #endregion

          #region Generics

          #endregion

          #region Ejecuta comando

          /// <summary>
          /// Executes the non query.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <returns></returns>
          public static int EjecutaComando(StringBuilder psQuery)
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              return SqlHelper.ExecuteNonQuery(_oSqlConexionActual, CommandType.Text, psQuery.ToString());
                         else
                              return SqlHelper.ExecuteNonQuery(_oSqlTransaccionActual, CommandType.Text, psQuery.ToString());
                    case enmMotor.Oracle:
                         return 0;
                    case enmMotor.Oledb:
                         return 0;
               }
               return 0;
          }

          /// <summary>
          /// Ejecutas the comando.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="poParametros">The po parametros.</param>
          /// <param name="pbIlimitado">if set to <c>true</c> [pb ilimitado].</param>
          public static int EjecutaComando(StringBuilder psQuery, IEnumerable<ParametrosSQL> poParametros, bool pbIlimitado = true)
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              return SqlHelper.ExecuteNonQuery(_oSqlConexionActual, CommandType.Text, psQuery, poParametros, pbIlimitado);
                         else
                              return SqlHelper.ExecuteNonQuery(_oSqlTransaccionActual, CommandType.Text, psQuery, poParametros, pbIlimitado);
                    case enmMotor.Oracle:
                         return 0;
                    case enmMotor.Oledb:
                         return 0;
               }
               return 0;
          }

          #endregion

          #region Ejectua escalar

          /// <summary>
          /// Ejecutas the escalar.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <param name="poParametros">The po parametros.</param>
          /// <returns></returns>
          public static object EjecutaEscalar(StringBuilder psQuery, IEnumerable<ParametrosSQL> poParametros)
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              return SqlHelper.ExecuteScalar(_oSqlConexionActual, CommandType.Text, psQuery, poParametros);
                         else
                              return SqlHelper.ExecuteScalar(_oSqlTransaccionActual, CommandType.Text, psQuery, poParametros);
                    case enmMotor.Oracle:
                         return null;
                    case enmMotor.Oledb:
                         return null;
               }
               return null;
          }

          /// <summary>
          /// Ejecutas the escalar.
          /// </summary>
          /// <param name="psQuery">The ps query.</param>
          /// <returns></returns>
          public static object EjecutaEscalar(StringBuilder psQuery)
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              return SqlHelper.ExecuteScalar(_oSqlConexionActual, CommandType.Text, psQuery.ToString());
                         else
                              return SqlHelper.ExecuteScalar(_oSqlTransaccionActual, CommandType.Text, psQuery.ToString());
                    case enmMotor.Oracle:
                         return null;
                    case enmMotor.Oledb:
                         return null;
               }
               return null;
          }

          #endregion

          #region  Store procedure

          /// <summary>
          /// Ejecutas the comando procedimiento almacenado.
          /// </summary>
          /// <param name="psNombre">The ps nombre.</param>
          /// <returns></returns>
          public static int EjecutaComandoProcedimientoAlmacenado(string psNombre)
          {
               return EjecutaComandoProcedimientoAlmacenado(psNombre, null);
          }

          /// <summary>
          /// Ejecutas the sp.
          /// </summary>
          /// <param name="psNombre">The ps nombre.</param>
          /// <param name="poParametros">The po parametros.</param>
          public static int EjecutaComandoProcedimientoAlmacenado(string psNombre, IEnumerable<ParametrosSQL> poParametros)
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              return SqlHelper.StoreProcedureExecuteNonQuery(_oSqlConexionActual, CommandType.StoredProcedure, psNombre, poParametros);
                         else
                              return SqlHelper.StoreProcedureExecuteNonQuery(_oSqlTransaccionActual, CommandType.StoredProcedure, psNombre, poParametros);
                    case enmMotor.Oracle:

                         break;
                    case enmMotor.Oledb:

                         break;
               }
               return 0;
          }

          /// <summary>
          /// Ejecutas the escalar procedimiento almacenado.
          /// </summary>
          /// <param name="psNombre">The ps nombre.</param>
          /// <returns></returns>
          public static object EjecutaEscalarProcedimientoAlmacenado(string psNombre)
          {
               return EjecutaEscalarProcedimientoAlmacenado(psNombre, null);
          }

          /// <summary>
          /// Ejecutas the escalar procedimiento almacenado.
          /// </summary>
          /// <param name="psNombre">The ps nombre.</param>
          /// <param name="poParametros">The po parametros.</param>
          /// <returns></returns>
          public static object EjecutaEscalarProcedimientoAlmacenado(string psNombre, IEnumerable<ParametrosSQL> poParametros)
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              return SqlHelper.StoreProcedureExecuteScalar(_oSqlConexionActual, CommandType.StoredProcedure, psNombre, poParametros);
                         else
                              return SqlHelper.StoreProcedureExecuteScalar(_oSqlTransaccionActual, CommandType.StoredProcedure, psNombre, poParametros);
                    case enmMotor.Oracle:

                         break;
                    case enmMotor.Oledb:

                         break;
               }
               return null;
          }
          /// <summary>
          /// Ejecutas the escalar procedimiento almacenado.
          /// </summary>
          /// <param name="psNombre">The ps nombre.</param>
          /// <param name="poParametros">The po parametros.</param>
          /// <returns></returns>
          public static SqlDataReader EjecutaProcedimientoAlmacenadoDataReader(string psNombre, IEnumerable<ParametrosSQL> poParametros)
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual == null)
                              return SqlHelper.StoreProcedureExecuteScalarDataReader(_oSqlConexionActual, CommandType.StoredProcedure, psNombre, poParametros);
                         break;
                    case enmMotor.Oracle:

                         break;
                    case enmMotor.Oledb:

                         break;
               }
               return null;
          }
          #endregion

          #region Bulkcopy

          /// <summary>
          /// Inserts the bulk copy.
          /// </summary>
          /// <param name="pdtTablaOrigen">The PDT tabla origen.</param>
          /// <param name="psTablaDestino">The ps tabla destino.</param>
          public static void InsertBulkCopy(DataTable pdtTablaOrigen, string psTablaDestino)
          {
               switch (_enmMotor)
               {
                    case enmMotor.Sql:
                         if (_oSqlTransaccionActual != null)
                         {
                              using (SqlBulkCopy loBulkCopy = new SqlBulkCopy(_oSqlConexionActual, SqlBulkCopyOptions.Default, _oSqlTransaccionActual))
                              {
                                   loBulkCopy.BatchSize = pdtTablaOrigen.Rows.Count;
                                   loBulkCopy.DestinationTableName = psTablaDestino;
                                   loBulkCopy.BulkCopyTimeout = 0;
                                   loBulkCopy.WriteToServer(pdtTablaOrigen);
                              }
                         }
                         else
                         {
                              using (SqlBulkCopy loBulkCopy = new SqlBulkCopy(_oSqlConexionActual))
                              {
                                   loBulkCopy.BatchSize = pdtTablaOrigen.Rows.Count;
                                   loBulkCopy.DestinationTableName = psTablaDestino;
                                   loBulkCopy.BulkCopyTimeout = 0;
                                   loBulkCopy.WriteToServer(pdtTablaOrigen);
                              }
                         }
                         break;
                    case enmMotor.Oracle:

                         break;
                    case enmMotor.Oledb:

                         break;
               }
          }

          #endregion
     }
}
