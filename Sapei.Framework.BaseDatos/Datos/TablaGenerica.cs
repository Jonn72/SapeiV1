using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Sapei.Framework.BaseDatos
{
     /// <summary>
     /// Clase para  manejar dinamicamente tablas del sistema. El proposito de esta tabla generica es poder serializarla en stateserver de asp.net
     /// </summary>
     [Serializable]
     public class TablaGenerica : IDisposable
     {
          #region variables

          /// <summary>
          /// Lista de las columnas de la tabla
          /// </summary>
          protected HashSet<string> _oNombreColumnas;
          /// <summary>
          /// Registros de la tabla
          /// </summary>
          protected HashSet<Fila> _oRegistros;
          /// <summary>
          /// Nombre de la tabla
          /// </summary>
          protected string _sNombreTabla;
          /// <summary>
          /// 
          /// </summary>
          protected bool _bdisposed;
          /// <summary>
          /// Total de filas de la tabla
          /// </summary>
          protected int _iTotalFilas;
          /// <summary>
          /// Total de columnas de la tabla
          /// </summary>
          protected int _iTotalColumnas;
          /// <summary>
          /// Consulta que genero el registro
          /// </summary>
          protected string _sQueryFuente;

          #endregion

          #region Propiedades

          /// <summary>
          /// Lista de las columnas de la tabla
          /// </summary>
          public HashSet<Fila> Registros
          {
               get
               {
                    return _oRegistros;
               }
          }

          /// <summary>
          /// Lista con losbombre de la columna
          /// </summary>
          public HashSet<string> NombreColumnas
          {
               get
               {
                    return _oNombreColumnas;
               }
          }

          /// <summary>
          /// NOmbre de la tabla
          /// </summary>
          public string NombreTabla
          {
               get
               {
                    return _sNombreTabla;
               }
          }

          /// <summary>
          /// Total de filas
          /// </summary>
          public int TotalFilas
          {
               get
               {
                    return _iTotalFilas;
               }
          }

          /// <summary>
          /// Total de columnas
          /// </summary>
          public int TotalColumnas
          {
               get
               {
                    return _iTotalColumnas;
               }
          }

          /// <summary>
          /// Consulta origen
          /// </summary>
          public string Fuente
          {
               get
               {
                    return _sQueryFuente;
               }
          }

          /// <summary>
          /// Acceder a los registros de la tabla
          /// </summary>
          /// <param name="piFila">Numero de la fila</param>
          /// <param name="piColumna">NUmero de la colimna</param>
          /// <returns></returns>
          public object this[int piFila, int piColumna]
          {
               get
               {
                    return _oRegistros.ElementAt(piFila).ValorFila[_oNombreColumnas.ElementAt(piColumna)];
               }
               set
               {
                    _oRegistros.ElementAt(piFila).ValorFila[_oNombreColumnas.ElementAt(piColumna)] = value;
               }
          }

          /// <summary>
          /// Acceder a los registros de la tabla
          /// </summary>
          /// <param name="piFila">Numero de fila</param>
          /// <param name="psColumna">NOmbre columna</param>
          /// <returns></returns>
          public object this[int piFila, string psColumna]
          {
               get
               {
                    return _oRegistros.ElementAt(piFila).ValorFila[psColumna.ToLower()];
               }
               set
               {
                    _oRegistros.ElementAt(piFila).ValorFila[psColumna.ToLower()] = value;
               }
          }

          #endregion

          #region Constructores

          private void ConstructorInterno()
          {
               _oRegistros = new HashSet<Fila>();
               _oNombreColumnas = new HashSet<string>();
          }

          /// <summary>
          /// Crea una instancia de la TablaGenreica
          /// </summary>
          /// <param name="psNombreTabla">NOmbre de la tabla</param>
          public TablaGenerica(string psNombreTabla)
          {
               _sNombreTabla = psNombreTabla;
               ConstructorInterno();
          }
          /// <summary>
          /// Initializes a new instance of the <see cref="TablaGenerica"/> class.
          /// </summary>
          /// <param name="poTabla">The po tabla.</param>
          /// <param name="psNombreTabla">The ps nombre tabla.</param>
          /// <param name="psQuery">The ps query.</param>
          public TablaGenerica(DataTable poTabla, string psNombreTabla, string psQuery)
          {
               _sNombreTabla = psNombreTabla;
               ConstructorInterno();
               ConvertDataTable2List2(poTabla, psQuery);
          }

          /// <summary>
          /// Crea una nueva instancia de la TablaGenerica
          /// </summary>
          public TablaGenerica()
          {
               _sNombreTabla = "TablaGenerica";
               ConstructorInterno();
          }

          /// <summary>
          /// Crea una nueva instancia de la tabla generica con un data table
          /// </summary>
          /// <param name="poTabla">Tabla con valores</param>
          public TablaGenerica(DataTable poTabla)
          {
               _sNombreTabla = "TablaGenerica";
               ConstructorInterno();
               ConvertDataTable2List2(poTabla, "");
          }

          #endregion

          #region Funciones

          /// <summary>
          /// Regresas the valor.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="piColumna">The pi columna.</param>
          /// <param name="psColumna">The ps columna.</param>
          /// <returns></returns>
          public T RegresaValor<T>(int piColumna, string psColumna)
          {
               Object poValor = _oRegistros.ElementAt(piColumna).ValorFila[psColumna.ToLower()];
               if (Convert.IsDBNull(poValor) || poValor == null)
                    return GetDefaultValue<T>();
               return (T)Convert.ChangeType(poValor, typeof(T));
          }
          /// <summary>
          /// Regresas the valor.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psColumna">The ps columna.</param>
          /// <returns></returns>
          public T RegresaValor<T>(string psColumna)
          {
               Object poValor = _oRegistros.ElementAt(0).ValorFila[psColumna.ToLower()];
               if (Convert.IsDBNull(poValor) || String.IsNullOrEmpty(Convert.ToString(poValor).Trim()))
                    return GetDefaultValue<T>();
               return (T)Convert.ChangeType(poValor, typeof(T));
          }

          /// <summary>
          /// Gets the default value.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <returns></returns>
          private T GetDefaultValue<T>()
          {
               //if (typeof(T) == typeof(String))
               //     return (T)(object)String.Empty;
               if (typeof(T) == typeof(DateTime))
                    return (T)(object)DateTime.Now;
               return default(T);
          }

          /// <summary>
          /// Regresa el tipo de dato en la columna
          /// </summary>
          /// <param name="piColumna"></param>
          /// <returns></returns>
          public Type TipoDatoColumna(int piColumna)
          {
               return _oRegistros.ElementAt(0).ValorFila[_oNombreColumnas.ElementAt(piColumna)].GetType();
          }
          /// <summary>
          /// Tipoes the dato columna.
          /// </summary>
          /// <param name="psColumna">The ps columna.</param>
          /// <returns></returns>
          public Type TipoDatoColumna(string psColumna)
          {
               return _oRegistros.ElementAt(0).ValorFila[psColumna].GetType();
          }

          /// <summary>
          /// Funcion que convierte un DataTable en TablaGenerica
          /// </summary>
          /// <param name="pdtRecosrds"></param>
          /// <param name="psQuery"></param>
          protected internal void ConvertDataTable2List2(DataTable pdtRecosrds, string psQuery)
          {
               Fila loFilaTipada;
               string lsNombreColumna;
               _sQueryFuente = psQuery;
               _iTotalColumnas = pdtRecosrds.Columns.Count;
               _iTotalFilas = pdtRecosrds.Rows.Count;
               foreach (DataRow loFila in pdtRecosrds.Rows)
               {
                    loFilaTipada = new Fila();
                    foreach (DataColumn loColumna in loFila.Table.Columns)
                    {
                         lsNombreColumna = loColumna.ColumnName.ToLower();
                         _oNombreColumnas.Add(lsNombreColumna);
                         loFilaTipada.ValorFila[lsNombreColumna] = loFila[loColumna.ColumnName];
                    }
                    _oRegistros.Add(loFilaTipada);
               }
               _oNombreColumnas.TrimExcess();
               _oRegistros.TrimExcess();
          }

          /// <summary>
          /// Funcion que determina si contiene la columna
          /// </summary>
          /// <param name="psNombreColumna">Nombre de la columna</param>
          /// <returns></returns>
          public bool ContieneColumna(string psNombreColumna)
          {
               return _oNombreColumnas.Contains(psNombreColumna);
          }

          /// <summary>
          /// Sorts the specified Campos ordenar.
          /// Multiple Field Sorting by Field Names Using Linq
          /// Los campos se ordenan de la manera siguiente
          /// Ejemplos
          ///  
          /// 1. Campo0,Campo1,Campo2,... se tomar pode default un ordeamiento ASC
          /// 2. Campo0~asc,Campos1~desc,... se puede indecar el timpo de ordenamiento para el campo 
          /// 
          /// Si no se expecifica el ordenamiento se tomar por defaul ASC
          /// Si el campo escrito no existe no se toma en cuenta en el ordenamiento
          /// </summary>
          /// <param name="psCamposOrdenar">Cadena con los campos a ordenar.</param>          
          public void Sort(string psCamposOrdenar)
          {
               Tuple<string, string> loTupla;
               if (String.IsNullOrEmpty(psCamposOrdenar))
               {
                    return;
               }
               var loSortExpressions = new List<Tuple<string, string>>();
               foreach (string lsCampo in psCamposOrdenar.Split(','))
               {
                    loTupla = ArmaCampoAOrdenar(lsCampo);
                    if (!Object.Equals(loTupla, null))
                    {
                         loSortExpressions.Add(loTupla);
                    }
               }
               MultipleSort(loSortExpressions);
          }
          /// <summary>
          /// Agregas the columna.
          /// </summary>
          /// <param name="psNombre">The ps nombre.</param>
          public void AgregaColumna(string psNombre)
          {
               _oNombreColumnas.Add(psNombre);
          }
          /// <summary>
          /// Arma el campo a ordenar
          /// </summary>
          /// <param name="psCampo">Campos.</param>
          /// <returns></returns>
          private Tuple<string, string> ArmaCampoAOrdenar(string psCampo)
          {
               string lsCampoReal;
               string lsOrdenamiento;
               if (!psCampo.Contains('~'))
               {
                    lsCampoReal = psCampo;
                    lsOrdenamiento = "asc";
               }
               else
               {
                    lsCampoReal = psCampo.Split('~')[0];
                    lsOrdenamiento = psCampo.Split('~')[1];
               }
               if (!ContieneColumna(lsCampoReal))
               {
                    return null;
               }
               if (lsOrdenamiento.ToLower() != "asc" && lsOrdenamiento.ToLower() != "desc")
               {
                    lsOrdenamiento = "asc";
               }
               return new Tuple<string, string>(lsCampoReal, lsOrdenamiento.ToLower());
          }

          /// <summary>
          /// Multiples the sort.
          /// </summary>          
          /// <param name="poSortExpressions">Expresion de Ordenamiento.</param>          
          private void MultipleSort(List<Tuple<string, string>> poSortExpressions)
          {
               // No sorting needed
               if ((poSortExpressions == null) || (poSortExpressions.Count <= 0))
               {
                    //Si no tiene nada que ordenas lo regresamos
                    return;
               }
               //Obtenemos toda la informacion que se va a ordenar
               IEnumerable<Fila> loConsulta = from item in _oRegistros
                                              select item;
               //Permite almancenar la lista ordenana
               IOrderedEnumerable<Fila> loConsultaOrdenada = null;

               for (int liTupla = 0; liTupla < poSortExpressions.Count; liTupla++)
               {
                    //Hacemos un ciclo por indice, ya que es alterado por Linq                    
                    var loIndex = liTupla;
                    //Recuperamos el valor del campo por el cual vamos a ordenar.
                    //Recuperamos el objeto por el que se ordena por el nombre del campo
                    //Esto es una expresion lamda
                    //Encapsulamos el metodo para que tiene un parametro Fila y regresamos un nuevo objeto
                    //que usaremos como expreiosn para
                    Func<Fila, object> loExpression = loItem => loItem.ValorFila[poSortExpressions[loIndex].Item1];
                    //Generic con reflection
                    //Func<AccesoDatos, object> loExpresion = loItem => loItem.GetType().GetProperty(poSortExpressions[loIndex].Item1);
                    //Objeto.Empresa
                    //loExpression ()         

                    //Seleccionamos el tipo de ordenamiento
                    if (poSortExpressions[loIndex].Item2 == "asc")
                    {
                         loConsultaOrdenada = (loIndex == 0) ?
                              //Ordenamos la se cuenta por la expresion
                                              loConsulta.OrderBy(loExpression)
                              //Realiza la clasificaicon porterior al ordenamiento anterior
                                              : loConsultaOrdenada.ThenBy(loExpression);
                    }
                    else
                    {
                         loConsultaOrdenada = (loIndex == 0) ?
                              //Ordenamos la se cuenta por la expresion
                                              loConsulta.OrderByDescending(loExpression)
                              //Realiza la clasificaicon posterior al ordenamiento anterior
                                              : loConsultaOrdenada.ThenByDescending(loExpression);
                    }
               }
               //Una vez ordenado la lista por los campos la reasignamos al origen de datos
               _oRegistros = loConsultaOrdenada.ToHash();
          }



          #endregion

          #region Disposable

          /// <summary>
          /// 
          /// </summary>
          public void Clear()
          {
               _oRegistros.Clear();
               _oNombreColumnas.Clear();
          }

          /// <summary>
          /// Implementación de IDisposable. No se sobreescribe.
          /// </summary>
          public void Dispose()
          {
               this.Dispose(true);
               // GC.SupressFinalize quita de la cola de finalización al objeto.
               GC.SuppressFinalize(this);
          }

          /// <summary>
          /// Limpia los recursos manejados y no manejados.
          /// </summary>
          /// <param name="poDisposing">
          /// Si es true, el método es llamado directamente o indirectamente
          /// desde el código del usuario.
          /// Si es false, el método es llamado por el finalizador
          /// y sólo los recursos no manejados son finalizados.
          /// </param>
          protected virtual void Dispose(bool poDisposing)
          {
               // Preguntamos si Dispose ya fue llamado.
               if (!this._bdisposed)
               {
                    if (poDisposing)
                    {
                         //Llamamos al Dispose de todos los RECURSOS MANEJADOS.                         
                    }
                    // Acá finalizamos correctamente los RECURSOS NO MANEJADOS
                    if (!Object.Equals(_oNombreColumnas, null))
                    {
                         _oNombreColumnas.Clear();
                         _oNombreColumnas = null;
                    }
                    if (!Object.Equals(_oRegistros, null))
                    {
                         _oRegistros.Clear();
                         _oRegistros = null;
                    }
               }
               this._bdisposed = true;
          }

          /// <summary>
          /// Destructor de la instancia
          /// </summary>
          ~TablaGenerica()
          {
               this.Dispose(false);
          }

          #endregion
     }
}
