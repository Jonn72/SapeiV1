using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Linq;
using System.Reflection;
using System.Data.SqlClient;

namespace Sapei.Framework.BaseDatos
{
     /// <summary>
     /// 
     /// </summary>
     public static class Extensiones
     {
          /// <summary>
          /// Basic data types 
          /// </summary>
          private static Type[] _oDataTypes = new[]
          {
               typeof(byte),
               typeof(sbyte),
               typeof(short),
               typeof(ushort),
               typeof(int),
               typeof(uint),
               typeof(long),
               typeof(ulong),
               typeof(float),
               typeof(double),
               typeof(decimal),
               typeof(bool),
               typeof(char),
               typeof(Guid),
               typeof(DateTime),
               typeof(DateTimeOffset),
               typeof(byte[]),
               typeof(string)
          };

          /// <summary>
          /// To the data table.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poData">The data.</param>
          /// <returns></returns>
          public static DataTable ToDataTable<T>(this IEnumerable<T> poData)
          {
               //Excluir las propiedades publicas que viene de la clase padre
               IEnumerable<PropertyDescriptor> loProperties = from loProperty in TypeDescriptor.GetProperties(typeof(T)).Cast<PropertyDescriptor>()
                                                              where IsBasicType(loProperty.PropertyType) &&
                                                                    loProperty.Name != "NombreTabla" &&
                                                                    loProperty.Name != "EOF" &&
                                                                    loProperty.Name != "AgregaHistorial" &&
                                                                    loProperty.Name != "Propietario"
                                                              select loProperty;
               return GetDataTable(poData, loProperties);
          }

          /// <summary>
          /// To the data table.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poData">The data.</param>
          /// <param name="poExpression">The expression.</param>
          /// <returns></returns>
          public static DataTable ToDataTable<T>(this IEnumerable<T> poData, Func<PropertyDescriptor, bool> poExpression)
          {
               IEnumerable<PropertyDescriptor> loProperties = TypeDescriptor.GetProperties(typeof(T))
                                                                          .Cast<PropertyDescriptor>()
                                                                          .Where(poExpression);
               return GetDataTable(poData, loProperties);
          }

          /// <summary>
          /// To the listof.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poDataTable">The dt.</param>
          /// <returns></returns>
          public static IEnumerable<T> ToEnumerable<T>(this DataTable poDataTable) where T : class, new()
          {
               T loInstanceOfT;
               const BindingFlags loFlags = BindingFlags.Public | BindingFlags.Instance;
               IEnumerable<string> loColumnNames;
               IEnumerable<PropertyInfo> loObjectProperties;
               loColumnNames = poDataTable.Columns
                                          .Cast<DataColumn>()
                                          .Select(c => c.ColumnName.ToLower());
               loObjectProperties = typeof(T).GetProperties(loFlags);

               IEnumerable<T> loTargetList = poDataTable.AsEnumerable().Select(loDataRow =>
               {
                    //var instanceOfT = Activator.CreateInstance<T>();
                    loInstanceOfT = new T();
                    foreach (PropertyInfo loProperty in loObjectProperties.Where(loProperties => loColumnNames.Contains(loProperties.Name.ToLower()) && loDataRow[loProperties.Name] != DBNull.Value))
                    {
                         loProperty.SetValue(loInstanceOfT, ChangeType(loDataRow[loProperty.Name], loProperty.PropertyType), null);
                    }
                    return loInstanceOfT;
               });

               return loTargetList;
          }

          /// <summary>
          /// To the hash set.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poEnumerable">The po enumerable.</param>
          /// <returns></returns>
          public static HashSet<T> ToHash<T>(this IEnumerable<T> poEnumerable)
          {
               HashSet<T> loHashSet = new HashSet<T>();
               foreach (T loItem in poEnumerable)
               {
                    loHashSet.Add(loItem);
               }
               return loHashSet;
          }

          /// <summary>
          /// Changes the type.
          /// </summary>
          /// <param name="poData">The po data.</param>
          /// <param name="poType">Type of the po.</param>
          /// <returns></returns>
          public static object ChangeType(object poData, Type poType)
          {
               if (Object.Equals(poData, System.DBNull.Value))
                    return null;
               return Convert.ChangeType(poData, poType);
          }

          /// <summary>
          /// To the XML.
          /// </summary>
          /// <param name="poData">The dt.</param>
          /// <param name="psRootName">Name of the root.</param>
          /// <returns></returns>
          public static XDocument ToXml(this DataTable poData, string psRootName)
          {
               XDocument loXDocument = new XDocument
               {
                    Declaration = new XDeclaration("1.0", "utf-8", "")
               };
               loXDocument.Add(new XElement(psRootName));
               foreach (DataRow loRow in poData.Rows)
               {
                    XElement loElement = new XElement(poData.TableName);
                    foreach (DataColumn loColumn in poData.Columns)
                    {
                         loElement.Add(new XElement(loColumn.ColumnName, loRow[loColumn].ToString().Trim(' ')));
                    }
                    if (loXDocument.Root != null)
                         loXDocument.Root.Add(loElement);
               }
               return loXDocument;
          }

          /// <summary>
          /// Funcion que obtiene la cadena de conexion del archivo .config
          /// </summary>
          /// <param name="psNombreConexion">NOmbre de la cadena de conexion</param>
          /// <returns></returns>
          public static string ObteneCadenadeConexion(string psNombreConexion)
          {
               //SqlConnectionStringBuilder loCadConexion;
               //string lsBDServidor;
               //string lsBDNombre;
               //string lsBDUsuario;
               //string lsBDPassword;
               //string lsCadenaConexion;
               //lsCadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings[psNombreConexion].ConnectionString;
               //if (lsCadenaConexion != null)
               //{
               //     loCadConexion = new SqlConnectionStringBuilder(lsCadenaConexion);
               //     lsBDServidor = loCadConexion.DataSource;
               //     lsBDNombre = loCadConexion.InitialCatalog;
               //     lsBDUsuario = loCadConexion.UserID;
               //     lsBDPassword = loCadConexion.Password;
               //     lsCadenaConexion = "Persist Security Info=True";
               //     lsCadenaConexion += string.Format(";Initial Catalog={0}", lsBDNombre);
               //     lsCadenaConexion += string.Format(";Data Source={0}", lsBDServidor);
               //     if (lsBDUsuario != null)
               //     {
               //          lsCadenaConexion += string.Format(";User id={0}", lsBDUsuario);
               //          lsCadenaConexion += string.Format(";Password={0}", lsBDPassword);
               //     }
               //     else
               //     {
               //          lsCadenaConexion += " ;Integrated Security=True ";
               //          lsCadenaConexion += " ;MultipleActiveResultSets=True ";
               //     }
               //}
               //else
               //{
               //     lsCadenaConexion = "";
               //}
               //return lsCadenaConexion;
               return "";
          }

          #region Private methods

          /// <summary>
          /// Determines whether [is basic type] [the specified type].
          /// </summary>
          /// <param name="poType">The type.</param>
          /// <returns></returns>
          private static bool IsBasicType(Type poType)
          {
               poType = Nullable.GetUnderlyingType(poType) ?? poType;
               return poType.IsEnum || _oDataTypes.Contains(poType);
          }

          /// <summary>
          /// Gets the data table.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poData">The data.</param>
          /// <param name="poMappedProperties">The mapped properties.</param>
          /// <returns></returns>
          private static DataTable GetDataTable<T>(this IEnumerable<T> poData, IEnumerable<PropertyDescriptor> poMappedProperties)
          {
               DataTable loTable = new DataTable();
               // columns
               foreach (PropertyDescriptor loProperty in poMappedProperties)
               {
                    loTable.Columns.Add(loProperty.Name, Nullable.GetUnderlyingType(loProperty.PropertyType) ?? loProperty.PropertyType);
               }
               // row values
               foreach (T loItem in poData)
               {
                    DataRow loRow = loTable.NewRow();
                    foreach (PropertyDescriptor loProperty in poMappedProperties)
                    {
                         object value = loProperty.GetValue(loItem) ?? DBNull.Value;
                         loRow[loProperty.Name] = value;
                    }
                    loTable.Rows.Add(loRow);
               }
               return loTable;
          }

          #endregion
     }
}
