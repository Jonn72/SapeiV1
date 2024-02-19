using System;
using System.Data;
using System.Linq;


namespace Sapei.Framework.BaseDatos
{
     /// <summary>
     /// Clase para los parametros de una consulta en SQL
     /// </summary>
     [Serializable]
     public class ParametrosSQL
     {
          /// <summary>
          /// NOmbre del parametro
          /// </summary>
          public string Parametro { get; set; }
          /// <summary>
          /// Valor del parametro
          /// </summary>
          public object Valor { get; set; }

          /// <summary>
          /// Valor del parametro
          /// </summary>
          public SqlDbType Tipo { get; set; }
          /// <summary>
          /// Crea una nueva instancia para los parametros
          /// </summary>
          /// <param name="psParametro">NOmbre del parametro</param>
          /// <param name="poValor">Valor del parametro</param>
          public ParametrosSQL(string psParametro, object poValor)
          {
               Parametro = psParametro;
               Valor = poValor;
               Tipo = SqlDbType.Variant;
          }
          public ParametrosSQL(string psParametro, object poValor, SqlDbType poTipo)
          {
               Parametro = psParametro;
               Valor = poValor;
               Tipo = poTipo;
          }
     }
}
