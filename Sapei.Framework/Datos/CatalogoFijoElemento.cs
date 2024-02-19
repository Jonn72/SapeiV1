using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System.Data;
using System.Xml;

namespace Sapei.Framework.Datos
{
     /// <summary>
     /// Clase que contiene las funciones basicas para el manejo de tablas del sistema 
     /// Actualmete solo funcion para las tablas que no son de metadatos
     /// </summary>
     [Serializable]
     public class CatalogoFijoElemento : DynamicObject, IDynamicMetaObjectProvider 
     {
          #region variables

          /// <summary>
          /// Variable del sistema
          /// </summary>
          protected internal Sistema _oSistema;
          /// <summary>
          /// Determina si es un nuevo elemento
          /// </summary>
          protected bool _bNuevo;
          /// <summary>
          /// Determina si ya tiene una tabla de bitacora asociada
          /// </summary>
          private bool _bExisteBitacora;
          /// <summary>
          /// Detemirnas si existen cambios sobre los registros
          /// </summary>
          private bool _bExisteCambios;
          /// <summary>
          /// es el archivo xml que se inserta en la tabla de bitacoras
          /// </summary>
          private string _sXMLCampos;
          /// <summary>
          /// lista para los parametros
          /// </summary>
          private List<ParametrosSQL> _oParametrosSQL;
          /// <summary>
          /// Cadena que almacena los campos llaves
          /// </summary>
          protected internal StringBuilder _sCamposLLave;

          /////// <summary>
          /////// The errores
          /////// </summary>
          ////private bool Errores;
          /////// <summary>
          /////// The mensajes error
          /////// </summary>
          ////private StringBuilder MensajesError;

          #endregion

          #region Propiedades

          /// <summary>
          /// Obtiene y establece las propiedades de una tabla de la base de datos       
          /// </summary>
          /// <value>
          /// Las propiedades.
          /// </value>
          protected Dictionary<string, Propiedad> Propiedades { get; set; }

          /// <summary>
          /// Obtiene y establece una lista de campos llave de una tabla
          /// </summary>
          /// <value>
          /// The lista campos llave.
          /// </value>
          public Dictionary<string, object> CamposLlave { get; set; }

          /// <summary>
          /// Nombre de la tabla
          /// </summary>
          public string NombreTabla { get; set; }
          /// <summary>
          /// Ruta de la tabla
          /// </summary>
          public string RutaTabla { get; set; }
          /// <summary>
          /// Propietario de la talba (schema)
          /// </summary>
          public string Propietario { get; set; }

          /// <summary>
          /// Determina si exiten valores en la coleccion de registros
          /// </summary>
          public bool EOF { get; private set; }

          /// <summary>
          /// Obtiene o establece si se va agregar algun campo a la tabla de historial
          /// </summary>
          /// <value>
          ///   <c>true</c> if [agrega historial]; otherwise, <c>false</c>.
          /// </value>
          public bool AgregaHistorial { get; set; }

          /// <summary>
          /// Obtiene o establece el valor de una propiedad usando un indexador
          /// Esta idea se penso para poder reutilizar esta clase con los metadatos
          /// </summary>
          /// <param name="psNombre">Nombre de la propiedad</param>
          /// <returns></returns>
          public object this[string psNombre]
          {
               get
               {
                    return this.ObtenerValorPropiedad(psNombre);
               }

               set
               {
                    this.EstablecerValorPropiedad(psNombre, value);
               }
          }

          #endregion

          #region Contructor

          /// <summary>
          /// Constructor para los Generics, dado que requieren un constructor sin parametros publico
          /// </summary>
          public CatalogoFijoElemento()
          {
               this.ConstrutorInterno();
          }

          /// <summary>
          /// Constructor sobre cargado para la formula
          /// </summary>
          /// <param name="poSistema">Variable del sistema</param>
          public CatalogoFijoElemento(Sistema poSistema)
               : this()//invoca el otro constructor
          {
               this._oSistema = poSistema;
               this.ConstrutorInterno();
          }

          /// <summary>
          /// Constructo interno de la clase
          /// </summary>
          private void ConstrutorInterno()
          {
               this._bNuevo = false;
               this.EOF = true;
               this._bExisteBitacora = false;
               this.Propietario = "dbo";
               this.AgregaHistorial = false;
               this._oParametrosSQL = new List<ParametrosSQL>();
               this._sCamposLLave = new StringBuilder();
          }

          #endregion

          #region Metodos para el Manejo de las propiedades de los catalogos

          /// <summary>
          /// Gets the value.
          /// </summary>
          /// <typeparam name="TValue">The type of the value.</typeparam>
          /// <param name="psName">Name of the ps.</param>
          /// <returns></returns>
          protected TValue ObtenerValorPropiedad<TValue>(string psName)
          {
               PropiedadesColumna<TValue> loProperty = this.ObtenerPropiedad<TValue>(psName);
               if (loProperty != null)
               {
                    return loProperty.Valor;
               }
               return default(TValue);
          }

          /// <summary>
          /// Obteners the valor propiedad.
          /// </summary>
          /// <param name="psName">Name of the ps.</param>
          /// <returns></returns>
          /// <exception cref="System.InvalidOperationException"></exception>
          public object ObtenerValorPropiedad(string psName)
          {
               if (!this.ExistePropiedad(psName))
               {
                    throw new InvalidOperationException(String.Format("Can't get property {0} value, because it doesn't exist.", psName));
               }
               if (this.Propiedades[psName] is PropiedadesColumna<bool>)
               {
                    return this.ObtenerValorPropiedad<bool>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<byte>)
               {
                    return this.ObtenerValorPropiedad<byte>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int16>)
               {
                    return this.ObtenerValorPropiedad<Int16>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int32>)
               {
                    return this.ObtenerValorPropiedad<Int32>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int64>)
               {
                    return this.ObtenerValorPropiedad<Int64>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<DateTime>)
               {
                    return this.ObtenerValorPropiedad<DateTime>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<decimal>)
               {
                    return this.ObtenerValorPropiedad<decimal>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Single>)
               {
                    return this.ObtenerValorPropiedad<Single>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<double>)
               {
                    return this.ObtenerValorPropiedad<double>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<object>)
               {
                    return this.ObtenerValorPropiedad<object>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<byte[]>)
               {
                    return this.ObtenerValorPropiedad<byte[]>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<string>)
               {
                    return this.ObtenerValorPropiedad<string>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<byte?>)
               {
                    return this.ObtenerValorPropiedad<byte?>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<bool?>)
               {
                    return this.ObtenerValorPropiedad<bool?>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int16?>)
               {
                    return this.ObtenerValorPropiedad<Int16?>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int32?>)
               {
                    return this.ObtenerValorPropiedad<Int32?>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int64?>)
               {
                    return this.ObtenerValorPropiedad<Int64?>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<DateTime?>)
               {
                    return this.ObtenerValorPropiedad<DateTime?>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<decimal?>)
               {
                    return this.ObtenerValorPropiedad<decimal?>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Single?>)
               {
                    return this.ObtenerValorPropiedad<Single?>(psName);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<double?>)
               {
                    return this.ObtenerValorPropiedad<double?>(psName);
               }
               return null;
          }

          /// <summary>
          /// Sets the value.
          /// </summary>
          /// <typeparam name="TValue">The type of the value.</typeparam>
          /// <param name="psName">Name of the ps.</param>
          /// <param name="poValue">The po value.</param>
          /// <param name="pbValorAnterior">if set to <c>true</c> [pb valor anterior].</param>
          protected void EstablecerValorPropiedad<TValue>(string psName, TValue poValue, bool pbValorAnterior = false)
          {
               PropiedadesColumna<TValue> loProperty = this.ObtenerPropiedad<TValue>(psName);
               if (loProperty == null)
               {
                    return;
               }
               loProperty.Valor = poValue;
               if (pbValorAnterior)
               {
                    loProperty.ValorAnterior = poValue;
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="psName"></param>
          /// <param name="poValue"></param>
          public void EstablecerValorPropiedad(string psName, object poValue)
          {
               if (!this.ExistePropiedad(psName))
               {
                    throw new InvalidOperationException(String.Format("Can't get property {0} value, because it doesn't exist.", psName));
               }
               if (this.Propiedades[psName] is PropiedadesColumna<bool>)
               {
                    this.EstablecerValorPropiedad<bool>(psName, Convert.ToString(poValue).ToBoolean());
               }
               if (this.Propiedades[psName] is PropiedadesColumna<byte>)
               {
                    this.EstablecerValorPropiedad<byte>(psName, Convert.ToByte(poValue));
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int16>)
               {
                    this.EstablecerValorPropiedad<Int16>(psName, Convert.ToInt16(poValue));
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int32>)
               {
                    this.EstablecerValorPropiedad<Int32>(psName, Convert.ToInt32(poValue));
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int64>)
               {
                    this.EstablecerValorPropiedad<Int64>(psName, Convert.ToInt64(poValue));
               }
               if (this.Propiedades[psName] is PropiedadesColumna<DateTime>)
               {
                    this.EstablecerValorPropiedad<DateTime>(psName, Convert.ToDateTime(poValue));
               }
               if (this.Propiedades[psName] is PropiedadesColumna<decimal>)
               {
                    this.EstablecerValorPropiedad<decimal>(psName, Convert.ToDecimal(poValue));
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Single>)
               {
                    this.EstablecerValorPropiedad<Single>(psName, Convert.ToSingle(poValue));
               }
               if (this.Propiedades[psName] is PropiedadesColumna<double>)
               {
                    this.EstablecerValorPropiedad<double>(psName, Convert.ToDouble(poValue));
               }
               if (this.Propiedades[psName] is PropiedadesColumna<object>)
               {
                    this.EstablecerValorPropiedad<object>(psName, poValue);
               }
               if (this.Propiedades[psName] is PropiedadesColumna<byte[]>)
               {
                    if (!String.IsNullOrEmpty(Convert.ToString(poValue)))
                    {
                         this.EstablecerValorPropiedad<byte[]>(psName, (byte[])poValue);
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<byte[]>(psName, null);
                    }
               }
               if (this.Propiedades[psName] is PropiedadesColumna<string>)
               {
                    if (!Object.Equals(poValue, null))
                    {
                         this.EstablecerValorPropiedad<string>(psName, Convert.ToString(poValue));
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<string>(psName, null);
                    }
               }
               if (this.Propiedades[psName] is PropiedadesColumna<byte?>)
               {
                    if (!Object.Equals(poValue, null))
                    {
                         this.EstablecerValorPropiedad<byte?>(psName, Convert.ToByte(poValue));
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<byte?>(psName, null);
                    }
               }
               if (this.Propiedades[psName] is PropiedadesColumna<bool?>)
               {
                    if (!Object.Equals(poValue, null))
                    {
                         this.EstablecerValorPropiedad<bool?>(psName, Convert.ToString(poValue).ToBoolean());
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<bool?>(psName, null);
                    }
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int16?>)
               {
                    if (!Object.Equals(poValue, null))
                    {
                         this.EstablecerValorPropiedad<Int16?>(psName, Convert.ToInt16(poValue));
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<Int16?>(psName, null);
                    }
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int32?>)
               {
                    if (!Object.Equals(poValue, null))
                    {
                         this.EstablecerValorPropiedad<Int32?>(psName, Convert.ToInt32(poValue));
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<Int32?>(psName, null);
                    }
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Int64?>)
               {
                    if (!Object.Equals(poValue, null))
                    {
                         this.EstablecerValorPropiedad<Int64?>(psName, Convert.ToInt64(poValue));
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<Int64?>(psName, null);
                    }
               }
               if (this.Propiedades[psName] is PropiedadesColumna<DateTime?>)
               {
                    if (!Object.Equals(poValue, null))
                    {
                         this.EstablecerValorPropiedad<DateTime?>(psName, Convert.ToDateTime(poValue));
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<DateTime?>(psName, null);
                    }
               }
               if (this.Propiedades[psName] is PropiedadesColumna<decimal?>)
               {
                    if (!Object.Equals(poValue, null))
                    {
                         this.EstablecerValorPropiedad<decimal?>(psName, Convert.ToDecimal(poValue));
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<decimal?>(psName, null);
                    }
               }
               if (this.Propiedades[psName] is PropiedadesColumna<Single?>)
               {
                    if (!Object.Equals(poValue, null))
                    {
                         this.EstablecerValorPropiedad<Single?>(psName, Convert.ToSingle(poValue));
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<Single?>(psName, null);
                    }
               }
               if (this.Propiedades[psName] is PropiedadesColumna<double?>)
               {
                    if (!Object.Equals(poValue, null))
                    {
                         this.EstablecerValorPropiedad<double?>(psName, Convert.ToDouble(poValue));
                    }
                    else
                    {
                         this.EstablecerValorPropiedad<double?>(psName, null);
                    }
               }
          }

          /// <summary>
          /// Agregars the propiedad.
          /// </summary>
          /// <typeparam name="TValue">The type of the value.</typeparam>
          /// <param name="psName">Name of the ps.</param>
          /// <param name="poProperty">The po property.</param>
          /// <exception cref="InvalidOperationException"></exception>
          protected void AgregarPropiedad<TValue>(string psName, PropiedadesColumna<TValue> poProperty)
          {
               if (this.ExistePropiedad(psName))
               {
                    throw new InvalidOperationException(String.Format("Can't add property {0}, because it is already added.", psName));
               }
               this.Propiedades.Add(psName, poProperty);
          }

          /// <summary>
          /// Agregas the campo llave.
          /// </summary>
          /// <param name="psName">Name of the ps.</param>
          /// <param name="poValor">The po valor.</param>
          public void AgregaCampoLlave(string psName, object poValor)
          {
               this.CamposLlave[psName] = poValor;
          }

          /// <summary>
          /// Gets the property.
          /// </summary>
          /// <typeparam name="TValue">The type of the value.</typeparam>
          /// <param name="psName">Name of the ps.</param>
          /// <returns></returns>
          /// <exception cref="System.InvalidOperationException">
          /// </exception>
          private PropiedadesColumna<TValue> ObtenerPropiedad<TValue>(string psName)
          {
               if (!this.ExistePropiedad(psName))
               {
                    throw new InvalidOperationException(String.Format("Can't get property {0} value, because it doesn't exist.", psName));
               }
               PropiedadesColumna<TValue> loProperty = this.Propiedades[psName] as PropiedadesColumna<TValue>;
               if (loProperty == null)
               {
                    throw new InvalidOperationException(String.Format("Invalid type {0} specified for {1} property.", typeof(TValue).FullName, psName));
               }
               return loProperty;
          }

          /// <summary>
          /// Determines whether the specified ps name has property.
          /// </summary>
          /// <param name="psName">Name of the ps.</param>
          /// <returns></returns>
          protected bool ExistePropiedad(string psName)
          {
               return this.Propiedades == null ? false : this.Propiedades.ContainsKey(psName);
          }

          #endregion

          #region FUnciones para validaciones personalidadas

          /// <summary>
          /// Funcion para personal la validacion para un nuevo registro
          /// </summary>
          protected virtual void ValidacionNuevoPersonalizada()
          {
          }

          /// <summary>
          /// Funcion pra personalizar el grabar en una los registros
          /// </summary>
          protected virtual void ValidacionGrabarPersonalizada()
          {
          }

          /// <summary>
          /// Funcion para personalr para grabar los cambios en los registros
          /// </summary>
          protected virtual void ValidacionCambiosGrabarPersonalizada()
          {
          }

          /// <summary>
          /// Funcion que valida los campos antes de eleimnar n registro
          /// </summary>
          protected virtual void ValidacionEliminarPersonalizada()
          {
          }

          #endregion

          #region Metodos para el ABC


          /// <summary>
          /// Armas the consulta para delete parametros SQL.
          /// </summary>
          /// <returns></returns>
          protected StringBuilder ArmaConsultaParaDeleteParametrosSql()
          {
               StringBuilder lsbQuery;
               lsbQuery = new StringBuilder();
               lsbQuery.AppendFormat("delete from [{0}].[{1}].[{2}] where {3}", this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario, this.NombreTabla, this.ArmaWhereDeCamposLlaveParametrosSql());
               return lsbQuery;
          }

          /// <summary>
          /// Retorna los campos de la tabla
          /// </summary>
          /// <returns></returns>
          protected internal void ArmaCamposSeparadosPorComas()
          {
               StringBuilder lsbCampos;
               lsbCampos = new StringBuilder();
               if (this._sCamposLLave == null)
               {
                    this._sCamposLLave = new StringBuilder();
               }
               foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
               {
                    lsbCampos.AppendFormat("{0},", this.ArreglaNombreCampo(loColumna.Key));
               }
               this._sCamposLLave = lsbCampos.Remove(lsbCampos.Length - 1, 1);
          }

          /// <summary>
          /// Guardar2s the specified pb incluye horas.
          /// </summary>          
          public void Guardar()
          {
               this._sXMLCampos = "";
               if (this._bNuevo)
               {
                    //Si es un valor nuevo en la base de datos
                    this.ValidacionGrabarPersonalizada();
                    this.EjecutaConsultaYGrabaBitacora(enmMovimientos.Alta);
                    this._bNuevo = false;
                    return;
               }
               if (this.EOF)
                    return;
               this.ValidacionCambiosGrabarPersonalizada();
               this.EjecutaConsultaYGrabaBitacora(enmMovimientos.Cambio);
          }

          /// <summary>
          /// Carga un nuevo objeto
          /// </summary>
          public void Nuevo()
          {
               this.ValidacionNuevoPersonalizada();
               this.CargaFilaDeDatosDefault();
               this._bNuevo = true;
               this.EOF = true;
          }

          /// <summary>
          /// Elimina una objeto cargado
          /// </summary>
          public void Eliminar()
          {
               if (this.EOF || this._bNuevo)
               {
                    return;
               }
               this._sXMLCampos = "";
               this.ValidacionEliminarPersonalizada();
               //Lo tiene que hacer antes de eliminarlo fisicamente para guardar la foto del registro antes de quitarlo    
               this.GrabaBitacora(enmMovimientos.Eliminacion);
               this._oSistema.Conexion.EjecutaComando(this.ArmaConsultaParaDeleteParametrosSql(), this._oParametrosSQL);
               this.EOF = true;
          }

          /// <summary>
          ///  Carga los datos segun la llave primaria
          /// </summary>
          /// <param name="poLlavesPrimarias"></param>
          public void Cargar(params object[] poLlavesPrimarias)
          {
               this.PreparaCamposLlave(poLlavesPrimarias.ToList());
               this.CargarInternoConParametrosSql();
          }

          /// <summary>
          /// Cargar los datos segun una lista de valores para la llave primaria.
          /// </summary>
          /// <param name="poLlavesPrimarias">The po llaves primarias.</param>
          public void Cargar(List<object> poLlavesPrimarias)
          {
               this.PreparaCamposLlave(poLlavesPrimarias);
               this.CargarInternoConParametrosSql();
          }

          /// <summary>
          /// Si la coleccion que tiene las llaves ya contiene valores. Podemos usar este metodo para cargar los valores internos
          /// </summary>
          public void Cargar()
          {
               this.CargarInternoConParametrosSql();
          }

          /// <summary>
          /// Preparas the campos llave.
          /// </summary>
          /// <param name="poLlavesPrimarias">The po llaves primarias.</param>
          /// <exception cref="SolExcepcion">
          /// No hay argumentos de las llaves primarias
          /// or
          /// Las llaves primarias enviadas no coinciden con la definicion de la tabla
          /// </exception>
          private void PreparaCamposLlave(List<object> poLlavesPrimarias)
          {
               int liParametro;
               if (poLlavesPrimarias == null)
               {
                    throw new SolExcepcion("No hay argumentos de las llaves primarias");
               }
               if (this.CamposLlave.Count != poLlavesPrimarias.Count)
               {
                    throw new SolExcepcion("Las llaves primarias enviadas no coinciden con la definicion de la tabla");
               }
               liParametro = 0;
               foreach (string lsCampo in this.CamposLlave.Keys.ToList())
               {
                    this.CamposLlave[lsCampo] = poLlavesPrimarias[liParametro];
                    liParametro++;
               }
          }

          /// <summary>
          /// Arma la consulta para carga los datos conforme a la llave primaria
          /// </summary>          
          /// <returns></returns>
          private StringBuilder ArmaConsultaParaCargarDatos()
          {
               StringBuilder lsQuery;
               StringBuilder lsOrderby;
               lsQuery = new StringBuilder();
               lsOrderby = new StringBuilder();
               if (this._sCamposLLave.Length == 0)
               {
                    this.ArmaCamposSeparadosPorComas();
               }
               lsQuery.AppendFormat("Select {0} From [{1}].[{2}].[{3}{4}] where", this._sCamposLLave, this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario, this._oSistema.Servidor.Principal.BaseDatos.Prefijo, this.NombreTabla);
               foreach (KeyValuePair<string, Object> loCampo in this.CamposLlave)
               {
                    if (loCampo.Value.GetType() == Type.GetType("System.DateTime"))
                    {
                         lsQuery.AppendFormat(" {0}={1} And ", loCampo.Key, Sapei.Framework.Utilerias.ManejoFechas.FechaSQL(Convert.ToDateTime(loCampo.Value)));
                    }
                    else if (loCampo.Value.GetType() == Type.GetType("System.String"))
                    {
                         lsQuery.AppendFormat(" {0}='{1}' And ", loCampo.Key, Convert.ToString(loCampo.Value));
                    }
                    else if (loCampo.Value.GetType() == Type.GetType("System.Boolean"))
                    {
                         lsQuery.AppendFormat(" {0}={1} And ", loCampo.Key, Utilerias.ManejoObjetos.Iif(Convert.ToBoolean(loCampo.Value), 1, 0));
                    }
                    else
                    {
                         lsQuery.AppendFormat(" {0}={1} And ", loCampo.Key, loCampo.Value);
                    }
                    lsOrderby.AppendFormat("{0},", loCampo.Key);
               }
               lsQuery.Remove(lsQuery.Length - 4, 4); //le quita los ultimos cuatro caracteres del and
               lsOrderby.Remove(lsOrderby.Length - 1, 1); //le quita la ultima coma para el order by
               lsQuery.AppendFormat("Order by {0}", lsOrderby);
               return lsQuery;
          }

          /// <summary>
          /// Cargars the interno con parametros SQL.
          /// </summary>
          private void CargarInternoConParametrosSql()
          {
               StringBuilder lsQuery;
               //Carga los valores de la clase, segun un el PK
               DataTable ldtDatos;
               lsQuery = this.ArmaConsultaParaCargarDatosConParametrosSql();
               ldtDatos = this._oSistema.Conexion.RegresaDataTable(lsQuery, this._oParametrosSQL);
               this.EOF = true;
               this._bNuevo = true;
               if (ldtDatos.Rows.Count == 0)
               {
                    return;
               }
               //De la fila cargada del datable llena una estructura de tipo fila                         
               this.CargaFilaDeDatos(ldtDatos.Rows[0]);
          }

          /// <summary>
          /// Armas the consulta para cargar datos con parametros SQL.
          /// </summary>
          /// <returns></returns>
          private StringBuilder ArmaConsultaParaCargarDatosConParametrosSql()
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               if (this._sCamposLLave.Length == 0)
               {
                    this.ArmaCamposSeparadosPorComas();
               }
               lsQuery.AppendFormat("Select {0} From [{1}].[{2}].[{3}{4}] ", this._sCamposLLave, this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario, this._oSistema.Servidor.Principal.BaseDatos.Prefijo, this.NombreTabla);
               lsQuery.AppendFormat(" Where {0} ", this.ArmaWhereDeCamposLlaveParametrosSql());
               lsQuery.AppendFormat(" Order by {0}", this.ArmaOrdebyDeCamposLLave());
               return lsQuery;
          }

          /// <summary>
          /// Armas the where de campos llave.
          /// </summary>
          /// <returns></returns>
          private StringBuilder ArmaWhereDeCamposLlaveParametrosSql()
          {
               StringBuilder lsWhere;
               lsWhere = new StringBuilder();
               this._oParametrosSQL = new List<ParametrosSQL>(this.CamposLlave.Count);
               foreach (KeyValuePair<string, Object> loCampo in this.CamposLlave)
               {
                    lsWhere.AppendFormat(" {0}=@{0} And ", loCampo.Key);
                    if (loCampo.Value.GetType() == Type.GetType("System.DateTime"))
                         this._oParametrosSQL.Add(new ParametrosSQL(string.Format("@{0}", loCampo.Key), Convert.ToDateTime(loCampo.Value).FechaSqlParametros()));
                    else
                         this._oParametrosSQL.Add(new ParametrosSQL(string.Format("@{0}", loCampo.Key), loCampo.Value));
               }
               lsWhere.Remove(lsWhere.Length - 4, 4);
               return lsWhere;
          }

          /// <summary>
          /// Armas the ordeby de campos l lave.
          /// </summary>
          /// <returns></returns>
          private StringBuilder ArmaOrdebyDeCamposLLave()
          {
               StringBuilder lsOrderby;
               lsOrderby = new StringBuilder();
               foreach (KeyValuePair<string, Object> loCampo in this.CamposLlave)
               {
                    lsOrderby.AppendFormat("{0},", loCampo.Key);
               }
               return lsOrderby.Remove(lsOrderby.Length - 1, 1); //le quita la ultima coma para el order by
          }

          /// <summary>
          /// Limpia los controles cuando se crea un nuevo campo
          /// </summary>
          private void CargaFilaDeDatosDefault()
          {
               foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
               {
                    if (loColumna.Value.EsPrimaryKey)
                    {
                         this.CamposLlave[loColumna.Key] = null;
                    }
                    if (loColumna.Value is PropiedadesColumna<bool>)
                    {
                         this.EstablecerValorPropiedad<bool>(loColumna.Key, default(Boolean));
                    }
                    if (loColumna.Value is PropiedadesColumna<byte>)
                    {
                         this.EstablecerValorPropiedad<byte>(loColumna.Key, default(Byte));
                    }
                    if (loColumna.Value is PropiedadesColumna<Int16>)
                    {
                         this.EstablecerValorPropiedad<Int16>(loColumna.Key, default(Int16));
                    }
                    if (loColumna.Value is PropiedadesColumna<Int32>)
                    {
                         this.EstablecerValorPropiedad<Int32>(loColumna.Key, default(Int32));
                    }
                    if (loColumna.Value is PropiedadesColumna<Int64>)
                    {
                         this.EstablecerValorPropiedad<Int64>(loColumna.Key, default(Int64));
                    }
                    if (loColumna.Value is PropiedadesColumna<DateTime>)
                    {
                         this.EstablecerValorPropiedad<DateTime>(loColumna.Key, default(DateTime));
                    }
                    if (loColumna.Value is PropiedadesColumna<decimal>)
                    {
                         this.EstablecerValorPropiedad<decimal>(loColumna.Key, default(decimal));
                    }
                    if (loColumna.Value is PropiedadesColumna<Single>)
                    {
                         this.EstablecerValorPropiedad<Single>(loColumna.Key, default(Single));
                    }
                    if (loColumna.Value is PropiedadesColumna<double>)
                    {
                         this.EstablecerValorPropiedad<double>(loColumna.Key, default(Double));
                    }
                    if (loColumna.Value is PropiedadesColumna<object>)
                    {
                         this.EstablecerValorPropiedad<object>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<byte[]>)
                    {
                         this.EstablecerValorPropiedad<byte[]>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<string>)
                    {
                         this.EstablecerValorPropiedad<string>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<byte?>)
                    {
                         this.EstablecerValorPropiedad<byte?>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<bool?>)
                    {
                         this.EstablecerValorPropiedad<bool?>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<Int16?>)
                    {
                         this.EstablecerValorPropiedad<Int16?>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<Int32?>)
                    {
                         this.EstablecerValorPropiedad<Int32?>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<Int64?>)
                    {
                         this.EstablecerValorPropiedad<Int64?>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<DateTime?>)
                    {
                         this.EstablecerValorPropiedad<DateTime?>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<decimal?>)
                    {
                         this.EstablecerValorPropiedad<decimal?>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<Single?>)
                    {
                         this.EstablecerValorPropiedad<Single?>(loColumna.Key, null);
                    }
                    if (loColumna.Value is PropiedadesColumna<double?>)
                    {
                         this.EstablecerValorPropiedad<double?>(loColumna.Key, null);
                    }
               }
          }

          /// <summary>
          /// Funcion que transforma un datatable en una fila dinamica definida por el catalogo
          /// Siempre asigna el primer registro como el valor de la fila
          /// </summary>
          /// <param name="pdtDatos">datatable con la fila a convertir</param>
          /// <returns></returns>
          protected internal void CargaFilaDeDatos(DataRow pdtDatos)
          {
               this.EOF = false;
               this._bNuevo = false;
               //Cuando se hace una asignacion de un objeto a otro no hace una copia de valor hace una copia por refrencia
               foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
               {
                    if (loColumna.Value.EsPrimaryKey)
                    {
                         this.CamposLlave[loColumna.Key] = pdtDatos[loColumna.Key];
                    }
                    if (loColumna.Value is PropiedadesColumna<bool>)
                    {
                         this.EstablecerValorPropiedad<bool>(loColumna.Key, pdtDatos.RegresaValor<Boolean>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<byte>)
                    {
                         this.EstablecerValorPropiedad<byte>(loColumna.Key, pdtDatos.RegresaValor<byte>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<Int16>)
                    {
                         this.EstablecerValorPropiedad<Int16>(loColumna.Key, pdtDatos.RegresaValor<Int16>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<Int32>)
                    {
                         this.EstablecerValorPropiedad<Int32>(loColumna.Key, pdtDatos.RegresaValor<Int32>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<Int64>)
                    {
                         this.EstablecerValorPropiedad<Int64>(loColumna.Key, pdtDatos.RegresaValor<Int64>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<DateTime>)
                    {
                         this.EstablecerValorPropiedad<DateTime>(loColumna.Key, pdtDatos.RegresaValor<DateTime>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<decimal>)
                    {
                         this.EstablecerValorPropiedad<decimal>(loColumna.Key, pdtDatos.RegresaValor<decimal>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<Single>)
                    {
                         this.EstablecerValorPropiedad<Single>(loColumna.Key, pdtDatos.RegresaValor<Single>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<double>)
                    {
                         this.EstablecerValorPropiedad<double>(loColumna.Key, pdtDatos.RegresaValor<double>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<object>)
                    {
                         this.EstablecerValorPropiedad<object>(loColumna.Key, pdtDatos.RegresaValor<object>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<byte[]>)
                    {
                         this.EstablecerValorPropiedad<byte[]>(loColumna.Key, pdtDatos.RegresaValor<byte[]>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<string>)
                    {
                         this.EstablecerValorPropiedad<string>(loColumna.Key, pdtDatos.RegresaValor<string>(loColumna.Key), true);
                    }
                    if (loColumna.Value is PropiedadesColumna<byte?>)
                    {
                         if (Convert.IsDBNull(pdtDatos[loColumna.Key]) || pdtDatos[loColumna.Key] == null)
                         {
                              this.EstablecerValorPropiedad<byte?>(loColumna.Key, null, true);
                         }
                         else
                         {
                              this.EstablecerValorPropiedad<byte?>(loColumna.Key, pdtDatos.RegresaValor<byte>(loColumna.Key), true);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<bool?>)
                    {
                         if (Convert.IsDBNull(pdtDatos[loColumna.Key]) || pdtDatos[loColumna.Key] == null)
                         {
                              this.EstablecerValorPropiedad<bool?>(loColumna.Key, null, true);
                         }
                         else
                         {
                              this.EstablecerValorPropiedad<bool?>(loColumna.Key, pdtDatos.RegresaValor<bool>(loColumna.Key), true);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<Int16?>)
                    {
                         if (Convert.IsDBNull(pdtDatos[loColumna.Key]) || pdtDatos[loColumna.Key] == null)
                         {
                              this.EstablecerValorPropiedad<Int16?>(loColumna.Key, null, true);
                         }
                         else
                         {
                              this.EstablecerValorPropiedad<Int16?>(loColumna.Key, pdtDatos.RegresaValor<Int16>(loColumna.Key), true);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<Int32?>)
                    {
                         if (Convert.IsDBNull(pdtDatos[loColumna.Key]) || pdtDatos[loColumna.Key] == null)
                         {
                              this.EstablecerValorPropiedad<Int32?>(loColumna.Key, null, true);
                         }
                         else
                         {
                              this.EstablecerValorPropiedad<Int32?>(loColumna.Key, pdtDatos.RegresaValor<Int32>(loColumna.Key), true);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<Int64?>)
                    {
                         if (Convert.IsDBNull(pdtDatos[loColumna.Key]) || pdtDatos[loColumna.Key] == null)
                         {
                              this.EstablecerValorPropiedad<Int64?>(loColumna.Key, null, true);
                         }
                         else
                         {
                              this.EstablecerValorPropiedad<Int64?>(loColumna.Key, pdtDatos.RegresaValor<Int64>(loColumna.Key), true);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<DateTime?>)
                    {
                         if (Convert.IsDBNull(pdtDatos[loColumna.Key]) || pdtDatos[loColumna.Key] == null)
                         {
                              this.EstablecerValorPropiedad<DateTime?>(loColumna.Key, null, true);
                         }
                         else
                         {
                              this.EstablecerValorPropiedad<DateTime?>(loColumna.Key, pdtDatos.RegresaValor<DateTime>(loColumna.Key), true);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<decimal?>)
                    {
                         if (Convert.IsDBNull(pdtDatos[loColumna.Key]) || pdtDatos[loColumna.Key] == null)
                         {
                              this.EstablecerValorPropiedad<decimal?>(loColumna.Key, null, true);
                         }
                         else
                         {
                              this.EstablecerValorPropiedad<decimal?>(loColumna.Key, pdtDatos.RegresaValor<decimal>(loColumna.Key), true);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<Single?>)
                    {
                         if (Convert.IsDBNull(pdtDatos[loColumna.Key]) || pdtDatos[loColumna.Key] == null)
                         {
                              this.EstablecerValorPropiedad<Single?>(loColumna.Key, null, true);
                         }
                         else
                         {
                              this.EstablecerValorPropiedad<Single?>(loColumna.Key, pdtDatos.RegresaValor<Single>(loColumna.Key), true);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<double?>)
                    {
                         if (Convert.IsDBNull(pdtDatos[loColumna.Key]) || pdtDatos[loColumna.Key] == null)
                         {
                              this.EstablecerValorPropiedad<double?>(loColumna.Key, null, true);
                         }
                         else
                         {
                              this.EstablecerValorPropiedad<double?>(loColumna.Key, pdtDatos.RegresaValor<double>(loColumna.Key), true);
                         }
                    }
               }
          }

          #endregion

          #region Metodos para la bitacora

          /// <summary>
          ///Graba en las tablas de las bitacoras
          /// </summary>
          /// <param name="penMovimiento">The pen movimiento.</param>
          protected void GrabaBitacora(enmMovimientos penMovimiento)
          {
               if (!this.AgregaHistorial)
               {
                    //Si no quiere tener historiales sobre la base la tabla que retorne
                    return;
               }
               if (!this._bExisteBitacora)
               {
                    //Verificamos que exista la bitacora
                    if (!this._oSistema.Conexion.ExisteObjetoEnBD(string.Format("{0}{1}", this._oSistema.Conexion.BDBitacoraPrefijoObj, this.NombreTabla), "", this._oSistema.Servidor.Principal.BaseDatos.Bitacora))
                    {
                         //Se arma el query para la tabla de historiales                         
                         this._oSistema.Conexion.EjecutaComando(this.ArmaConsultaTablaHistorial(), null, true);
                    }
                    this._bExisteBitacora = true;
               }
               //lsQuery = this.ArmaConsultaParaBitacora(penMovimiento, this._sXMLCampos);
               //this._oSistema.Conexion.EjecutaEscalar(lsQuery, this._oParametrosSQL);
          }

          /// <summary>
          /// Ejecutas the consulta y graba bitacora.
          /// </summary>
          /// <param name="psQuery">The LPS query.</param>
          /// <param name="penMovimiento">The pen movimiento.</param>
          private void EjecutaConsultaYGrabaBitacora(StringBuilder psQuery, enmMovimientos penMovimiento)
          {
               //Insertamos el nuevo registros
               if (!this._bExisteCambios && !this._bNuevo)
               {
                    return;
               }
               if (this._oParametrosSQL.Count > 0)
               {
                    this._oSistema.Conexion.EjecutaComando(psQuery, this._oParametrosSQL, true);
               }
               else
               {
                    this._oSistema.Conexion.EjecutaEscalar(psQuery);
               }
               //Si se grabo correctamente que grabe en la bitacora
               this.GrabaBitacora(penMovimiento);
          }

          /// <summary>
          /// Ejecutas the consulta y graba bitacora.
          /// </summary>
          /// <param name="penMovimiento">The pen movimiento.</param>          
          private void EjecutaConsultaYGrabaBitacora(enmMovimientos penMovimiento)
          {
               StringBuilder lsQuery;
               //Insertamos el nuevo registros
               lsQuery = new StringBuilder();
               if (penMovimiento == enmMovimientos.Alta)
               {
                    if (!this._bNuevo)
                    {
                         return;
                    }
                    lsQuery = this.ValidaYArmaConsultaParaInsertParametrosSql();
               }
               if (penMovimiento == enmMovimientos.Cambio)
               {
                    lsQuery = this.ValidaYArmaConsultaParaUpdateParametrosSql();
                    if (!this._bExisteCambios)
                         return;
               }
               if (lsQuery.Length == 0)
                    return;
               this._oSistema.Conexion.EjecutaComando(lsQuery, this._oParametrosSQL, true);
               //Si se grabo correctamente que grabe en la bitacora
               this.GrabaBitacora(penMovimiento);
          }

          #endregion

          #region Metodos para Validaciones internas

          /// <summary>
          /// Agregas the valor.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psNombre">The ps nombre.</param>
          /// <param name="poPropiedad">The po propiedad.</param>
          /// <param name="psbValores">The PSB valores.</param>
          /// <param name="psbCamposLlave">The PSB campos llave.</param>
          /// <returns></returns>
          /// <exception cref="Sapei.Framework.SolExcepcion"></exception>
          private bool AgregaValor<T>(string psNombre, PropiedadesColumna<T> poPropiedad, ref StringBuilder psbValores, ref StringBuilder psbCamposLlave)
          {
               if (poPropiedad.EsPrimaryKey)
               {
                    psbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(psNombre), poPropiedad.ValorAnterior);
               }
               if (Object.Equals(poPropiedad.Valor, poPropiedad.ValorAnterior))
               {
                    return false;
               }
               this._bExisteCambios = true;
               if (poPropiedad.EsRequeridoBD)
               {
                    if (Object.Equals(poPropiedad.Valor, null))
                    {
                         throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", psNombre));
                    }
               }
               if (Object.Equals(poPropiedad.Valor, null))
               {
                    psbValores.AppendFormat("{0}=NULL,", this.ArreglaNombreCampo(psNombre));
               }
               else
               {
                    psbValores.AppendFormat("{0}={1},", this.ArreglaNombreCampo(psNombre), poPropiedad.Valor);
               }
               return true;
          }

          /// <summary>
          /// Agregas the valor.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psNombre">The ps nombre.</param>
          /// <param name="poPropiedad">The po propiedad.</param>
          /// <param name="psbValores">The PSB valores.</param>
          /// <param name="pbIncluyeHoras">if set to <c>true</c> [pb incluye horas].</param>
          /// <exception cref="Sapei.Framework.SolExcepcion"></exception>
          private void AgregaValor<T>(string psNombre, PropiedadesColumna<T> poPropiedad, ref StringBuilder psbValores, bool pbIncluyeHoras = false)
          {
               if (poPropiedad.EsPrimaryKey)
               {
                    this._oParametrosSQL.Add(new ParametrosSQL(string.Format("{0}key", psNombre), poPropiedad.Valor));
               }
               if (Object.Equals(poPropiedad.Valor, poPropiedad.ValorAnterior))
               {
                    return;
               }
               if (poPropiedad.EsRequeridoBD)
               {
                    if (Object.Equals(poPropiedad.Valor, null))
                    {
                         throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", psNombre));
                    }
               }
               if (poPropiedad is PropiedadesColumna<DateTime> || poPropiedad is PropiedadesColumna<DateTime?> || poPropiedad is PropiedadesColumna<DateTimeOffset> || poPropiedad is PropiedadesColumna<DateTimeOffset?>)
               {
                    if (!Object.Equals(poPropiedad, null) && !Object.Equals(poPropiedad.Valor, null))
                         this._oParametrosSQL.Add(new ParametrosSQL(string.Format("@{0}", psNombre), Convert.ToDateTime(poPropiedad.Valor).FechaSqlParametros(pbIncluyeHoras)));
                    else
                         this._oParametrosSQL.Add(new ParametrosSQL(string.Format("@{0}", psNombre), poPropiedad.Valor));
               }
               else
                    this._oParametrosSQL.Add(new ParametrosSQL(string.Format("@{0}", psNombre), poPropiedad.Valor));
               this._bExisteCambios = true;
               psbValores.AppendFormat("{0}=@{0},", this.ArreglaNombreCampo(psNombre));
          }

          /// <summary>
          /// Agregas the axml.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="psNombre">The ps nombre.</param>
          /// <param name="poPropiedad">The po propiedad.</param>
          /// <param name="poxmlWriter">The poxml writer.</param>
          private void AgregaAXML<T>(string psNombre, PropiedadesColumna<T> poPropiedad, XmlWriter poxmlWriter)
          {
               poxmlWriter.WriteStartElement("Campo");
               poxmlWriter.WriteAttributeString("CampoId", Convert.ToString(poPropiedad.CampoId));
               poxmlWriter.WriteAttributeString("Nombre", psNombre);
               poxmlWriter.WriteElementString("Valor", Convert.ToString(poPropiedad.Valor));
               poxmlWriter.WriteElementString("ValorAnterior", Convert.ToString(poPropiedad.ValorAnterior));
               poxmlWriter.WriteEndElement();
          }

          /// <summary>
          /// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en 
          /// otro metodo estatico
          /// Si los metodos no implementan sus propios campos se toman por defaulf la configuracion de la base de datos
          /// </summary>
          protected virtual void CargaPropiedadesdeColumna()
          {
               if (!Object.Equals(this.CamposLlave, null) && !Object.Equals(this.Propiedades, null))
               {
                    return;
               }
          }

          /// <summary>
          /// Validas the y arma consulta para update parametros SQL.
          /// </summary>          
          /// <returns></returns>
          private StringBuilder ValidaYArmaConsultaParaUpdateParametrosSql()
          {
               StringBuilder lsbCamposLlave;
               StringBuilder lsbValores;
               StringBuilder lsbQuery;
               PropiedadesColumna<byte> loPropiedadByte;
               PropiedadesColumna<Int16> loPropiedadInt16;
               PropiedadesColumna<Int32> loPropiedadInt32;
               PropiedadesColumna<Int64> loPropiedadInt64;
               PropiedadesColumna<double> loPropiedadDouble;
               PropiedadesColumna<Single> loPropiedadSingle;
               PropiedadesColumna<bool> loPropiedadBool;
               PropiedadesColumna<byte[]> loPropiedadBytes;
               PropiedadesColumna<string> loPropiedadString;
               PropiedadesColumna<DateTime> loPropiedadDateTime;
               PropiedadesColumna<DateTimeOffset> loPropiedadDateTimeOffset;
               PropiedadesColumna<decimal> loPropiedadDecimal;
               PropiedadesColumna<object> loPropiedadObject;
               PropiedadesColumna<byte?> loPropiedadByteN;
               PropiedadesColumna<Int16?> loPropiedadInt16N;
               PropiedadesColumna<Int32?> loPropiedadInt32N;
               PropiedadesColumna<Int64?> loPropiedadInt64N;
               PropiedadesColumna<double?> loPropiedadDoubleN;
               PropiedadesColumna<Single?> loPropiedadSingleN;
               PropiedadesColumna<bool?> loPropiedadBoolN;
               PropiedadesColumna<DateTime?> loPropiedadDateTimeN;
               PropiedadesColumna<DateTimeOffset?> loPropiedadDateTimeOffsetN;
               PropiedadesColumna<decimal?> loPropiedadDecimalN;
               System.Xml.XmlWriterSettings loxmlWriterSettings;
               lsbQuery = new StringBuilder();
               lsbCamposLlave = new StringBuilder();
               lsbValores = new StringBuilder();
               this._bExisteCambios = false;
               loxmlWriterSettings = new System.Xml.XmlWriterSettings();
               loxmlWriterSettings.Encoding = System.Text.Encoding.UTF8;
               //loxmlWriterSettings.Indent = true;
               //loxmlWriterSettings.IndentChars = "\t";
               Utf8StringWriter loStringXML = new Utf8StringWriter();
               //using Proporciona una sintaxis adecuada que garantiza el uso correcto de los objetos IDisposable.
               //La instrucción using garantiza que se llame a Dispose, aunque se produzca una excepción mientras se llama a los métodos del objeto. 
               //Puede conseguir el mismo resultado colocando el objeto dentro de un bloque try y llamando a continuación a Dispose en un bloque finally; 
               //de hecho, esta es la forma en que el compilador traduce la instrucción using
               //TODO: Para regresar toso lso mensaje de error en una sola ejecucion de la actualizacion de los campos
               ////Errores = false;
               //MensajesError = new StringBuilder();
               using (XmlWriter loxmlWriter = XmlWriter.Create(loStringXML, loxmlWriterSettings))
               {
                    loxmlWriter.WriteStartDocument();
                    loxmlWriter.WriteStartElement("Datos");
                    this._bExisteCambios = false;
                    this._oParametrosSQL = new List<ParametrosSQL>();
                    foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
                    {
                         if (loColumna.Value.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}=@{0}key AND ", this.ArreglaNombreCampo(loColumna.Key));
                              if (loColumna.Value.EsIdentity)
                              {
                                   new Switch(loColumna.Value)
                                                    .Case<PropiedadesColumna<Int16>>(action =>
                                                    {
                                                         loPropiedadInt16 = loColumna.Value as PropiedadesColumna<Int16>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadInt16, ref lsbValores);
                                                    })
                                                    .Case<PropiedadesColumna<Int32>>(action =>
                                                    {
                                                         loPropiedadInt32 = loColumna.Value as PropiedadesColumna<Int32>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadInt32, ref lsbValores);
                                                    });
                                   
                              }
                         }
                         if (loColumna.Value.EsIdentity)
                         {
                              //Si es un campo identidad que no lo considere para los updates
                              continue;
                         }

                         //obtener el tipo de datos    
                         new Switch(loColumna.Value)
                                                    .Case<PropiedadesColumna<bool>>(action =>
                                                    {
                                                         loPropiedadBool = loColumna.Value as PropiedadesColumna<bool>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadBool, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadBool, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<byte>>(action =>
                                                    {
                                                         loPropiedadByte = loColumna.Value as PropiedadesColumna<byte>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadByte, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadByte, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<Int16>>(action =>
                                                    {
                                                         loPropiedadInt16 = loColumna.Value as PropiedadesColumna<Int16>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadInt16, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadInt16, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<Int32>>(action =>
                                                    {
                                                         loPropiedadInt32 = loColumna.Value as PropiedadesColumna<Int32>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadInt32, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadInt32, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<Int64>>(action =>
                                                    {
                                                         loPropiedadInt64 = loColumna.Value as PropiedadesColumna<Int64>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadInt64, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadInt64, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<DateTime>>(action =>
                                                    {
                                                         loPropiedadDateTime = loColumna.Value as PropiedadesColumna<DateTime>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadDateTime, ref lsbValores, loPropiedadDateTime.IncluyeHoras);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadDateTime, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<DateTimeOffset>>(action =>
                                                    {
                                                         loPropiedadDateTimeOffset = loColumna.Value as PropiedadesColumna<DateTimeOffset>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadDateTimeOffset, ref lsbValores, loPropiedadDateTimeOffset.IncluyeHoras);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadDateTimeOffset, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<decimal>>(action =>
                                                    {
                                                         loPropiedadDecimal = loColumna.Value as PropiedadesColumna<decimal>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadDecimal, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadDecimal, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<Single>>(action =>
                                                    {
                                                         loPropiedadSingle = loColumna.Value as PropiedadesColumna<Single>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadSingle, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadSingle, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<double>>(action =>
                                                    {
                                                         loPropiedadDouble = loColumna.Value as PropiedadesColumna<double>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadDouble, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadDouble, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<object>>(action =>
                                                    {
                                                         loPropiedadObject = loColumna.Value as PropiedadesColumna<object>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadObject, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadObject, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<byte[]>>(action =>
                                                    {
                                                         loPropiedadBytes = loColumna.Value as PropiedadesColumna<byte[]>;
                                                         //Ponemos como regla que los byte[] no pueden ser llaves primarias
                                                         this.AgregaValor(loColumna.Key, loPropiedadBytes, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadBytes, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<string>>(action =>
                                                    {
                                                         loPropiedadString = loColumna.Value as PropiedadesColumna<string>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadString, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadString, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<byte?>>(action =>
                                                    {
                                                         loPropiedadByteN = loColumna.Value as PropiedadesColumna<byte?>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadByteN, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadByteN, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<bool?>>(action =>
                                                    {
                                                         loPropiedadBoolN = loColumna.Value as PropiedadesColumna<bool?>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadBoolN, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadBoolN, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<Int16?>>(action =>
                                                    {
                                                         loPropiedadInt16N = loColumna.Value as PropiedadesColumna<Int16?>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadInt16N, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadInt16N, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<Int32?>>(action =>
                                                    {
                                                         loPropiedadInt32N = loColumna.Value as PropiedadesColumna<Int32?>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadInt32N, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadInt32N, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<Int64?>>(action =>
                                                    {
                                                         loPropiedadInt64N = loColumna.Value as PropiedadesColumna<Int64?>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadInt64N, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadInt64N, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<DateTime?>>(action =>
                                                    {
                                                         loPropiedadDateTimeN = loColumna.Value as PropiedadesColumna<DateTime?>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadDateTimeN, ref lsbValores, loPropiedadDateTimeN.IncluyeHoras);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadDateTimeN, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<DateTimeOffset?>>(action =>
                                                    {
                                                         loPropiedadDateTimeOffsetN = loColumna.Value as PropiedadesColumna<DateTimeOffset?>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadDateTimeOffsetN, ref lsbValores, loPropiedadDateTimeOffsetN.IncluyeHoras);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadDateTimeOffsetN, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<decimal?>>(action =>
                                                    {
                                                         loPropiedadDecimalN = loColumna.Value as PropiedadesColumna<decimal?>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadDecimalN, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadDecimalN, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<Single?>>(action =>
                                                    {
                                                         loPropiedadSingleN = loColumna.Value as PropiedadesColumna<Single?>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadSingleN, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadSingleN, loxmlWriter);
                                                    })
                                                    .Case<PropiedadesColumna<double?>>(action =>
                                                    {
                                                         loPropiedadDoubleN = loColumna.Value as PropiedadesColumna<double?>;
                                                         this.AgregaValor(loColumna.Key, loPropiedadDoubleN, ref lsbValores);
                                                         this.AgregaAXML(loColumna.Key, loPropiedadDoubleN, loxmlWriter);
                                                    });
                    }
                    loxmlWriter.WriteEndElement();
                    loxmlWriter.WriteEndDocument();
               }
               if (!this._bExisteCambios)
               {
                    return lsbQuery;
               }
               this._sXMLCampos = loStringXML.ToString();
               lsbQuery.AppendFormat("Update [{0}].[{1}].[{2}] set {3}  where {4}",
                    this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario, this.NombreTabla,
                    lsbValores.Remove(lsbValores.Length - 1, 1),
                    lsbCamposLlave.Remove(lsbCamposLlave.Length - 5, 5));
               return lsbQuery;
          }

          /// <summary>
          /// Arreglas the nombre campo.
          /// </summary>
          /// <param name="psColumna">The ps columna.</param>
          /// <returns></returns>
          private string ArreglaNombreCampo(string psColumna)
          {
               if (psColumna.Contains("%"))
               {
                    return String.Format("[{0}]", psColumna);
               }

               return psColumna;
          }

          /// <summary>
          /// Validas the y arma consulta para insert parametros SQL.
          /// 
          /// </summary>          
          /// <returns></returns>
          /// <exception cref="Sapei.Framework.SolExcepcion">
          /// </exception>
          private StringBuilder ValidaYArmaConsultaParaInsertParametrosSql()
          {
               // Esta funcion se pone a prueba de desempeño, con el fin de evaluar el uso de switch dinamico
               StringBuilder lsbCampos;
               StringBuilder lsbValores;
               StringBuilder lsbQuery;
               PropiedadesColumna<byte> loPropiedadByte;

               PropiedadesColumna<Int16> loPropiedadInt16;
               PropiedadesColumna<Int32> loPropiedadInt32;
               PropiedadesColumna<Int64> loPropiedadInt64;
               PropiedadesColumna<double> loPropiedadDouble;
               PropiedadesColumna<Single> loPropiedadSingle;
               PropiedadesColumna<bool> loPropiedadBool;
               PropiedadesColumna<byte[]> loPropiedadBytes;
               PropiedadesColumna<string> loPropiedadString;
               PropiedadesColumna<DateTime> loPropiedadDateTime;
               PropiedadesColumna<DateTimeOffset> loPropiedadDateTimeOffset;
               PropiedadesColumna<decimal> loPropiedadDecimal;
               PropiedadesColumna<object> loPropiedadObject;
               PropiedadesColumna<byte?> loPropiedadByteN;
               PropiedadesColumna<Int16?> loPropiedadInt16N;
               PropiedadesColumna<Int32?> loPropiedadInt32N;
               PropiedadesColumna<Int64?> loPropiedadInt64N;
               PropiedadesColumna<double?> loPropiedadDoubleN;
               PropiedadesColumna<Single?> loPropiedadSingleN;
               PropiedadesColumna<bool?> loPropiedadBoolN;
               PropiedadesColumna<DateTime?> loPropiedadDateTimeN;
               PropiedadesColumna<DateTimeOffset?> loPropiedadDateTimeOffsetN;
               PropiedadesColumna<decimal?> loPropiedadDecimalN;
               string lsNombre;
               lsbQuery = new StringBuilder();
               lsbCampos = new StringBuilder();
               lsbValores = new StringBuilder();
               this._oParametrosSQL = new List<ParametrosSQL>();
               //liContadorParametros = 0;
               foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
               {
                    if (loColumna.Value.EsIdentity)
                    {
                         //Si es un campo identidad que no lo considere para los inserts
                         continue;
                    }
                    lsbCampos.AppendFormat("{0},", this.ArreglaNombreCampo(loColumna.Key));
                    lsbValores.AppendFormat("@{0},", this.ArreglaNombreCampo(loColumna.Key));
                    lsNombre = string.Format("@{0}", loColumna.Key);

                    new Switch(loColumna.Value)
                                               .Case<PropiedadesColumna<bool>>(action =>
                                               {
                                                    loPropiedadBool = loColumna.Value as PropiedadesColumna<bool>;
                                                    if (loPropiedadBool.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadBool.Valor;
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadBool.Valor));
                                               })
                                               .Case<PropiedadesColumna<byte>>(action =>
                                               {
                                                    loPropiedadByte = loColumna.Value as PropiedadesColumna<byte>;
                                                    if (loPropiedadByte.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadByte.Valor;
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadByte.Valor));
                                               })
                                               .Case<PropiedadesColumna<Int16>>(action =>
                                               {
                                                    loPropiedadInt16 = loColumna.Value as PropiedadesColumna<Int16>;
                                                    if (loPropiedadInt16.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadInt16.Valor;
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadInt16.Valor));
                                               })
                                               .Case<PropiedadesColumna<Int32>>(action =>
                                               {
                                                    loPropiedadInt32 = loColumna.Value as PropiedadesColumna<Int32>;
                                                    if (loPropiedadInt32.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadInt32.Valor;
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadInt32.Valor));
                                               })
                                               .Case<PropiedadesColumna<Int64>>(action =>
                                               {
                                                    loPropiedadInt64 = loColumna.Value as PropiedadesColumna<Int64>;
                                                    if (loPropiedadInt64.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadInt64.Valor;
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadInt64.Valor));
                                               })
                                               .Case<PropiedadesColumna<DateTime>>(action =>
                                               {
                                                    loPropiedadDateTime = loColumna.Value as PropiedadesColumna<DateTime>;
                                                    if (loPropiedadDateTime.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadDateTime.Valor;
                                                    if (loPropiedadDateTime.EsRequeridoBD)
                                                    {
                                                         if (loPropiedadDateTime.Valor == default(DateTime))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadDateTime.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadDateTime.Valor.FechaSqlParametros(loPropiedadDateTime.IncluyeHoras)));
                                               })
                                               .Case<PropiedadesColumna<DateTimeOffset>>(action =>
                                               {
                                                    loPropiedadDateTimeOffset = loColumna.Value as PropiedadesColumna<DateTimeOffset>;
                                                    if (loPropiedadDateTimeOffset.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadDateTimeOffset.Valor;
                                                    if (loPropiedadDateTimeOffset.EsRequeridoBD)
                                                    {
                                                         if (loPropiedadDateTimeOffset.Valor == default(DateTimeOffset))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadDateTimeOffset.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadDateTimeOffset.Valor.FechaSqlParametros(loPropiedadDateTimeOffset.IncluyeHoras)));
                                               })
                                               .Case<PropiedadesColumna<decimal>>(action =>
                                               {
                                                    loPropiedadDecimal = loColumna.Value as PropiedadesColumna<decimal>;
                                                    if (loPropiedadDecimal.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadDecimal.Valor;
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadDecimal.Valor));
                                               })
                                               .Case<PropiedadesColumna<Single>>(action =>
                                               {
                                                    loPropiedadSingle = loColumna.Value as PropiedadesColumna<Single>;
                                                    if (loPropiedadSingle.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadSingle.Valor;
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadSingle.Valor));
                                               })
                                               .Case<PropiedadesColumna<double>>(action =>
                                               {
                                                    loPropiedadDouble = loColumna.Value as PropiedadesColumna<double>;
                                                    if (loPropiedadDouble.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadDouble.Valor;
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadDouble.Valor));
                                               })
                                               .Case<PropiedadesColumna<object>>(action =>
                                               {
                                                    loPropiedadObject = loColumna.Value as PropiedadesColumna<object>;
                                                    if (loPropiedadObject.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadObject.Valor;
                                                    if (loPropiedadObject.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadObject.Valor, null))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadObject.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadObject.Valor));
                                               })
                                               .Case<PropiedadesColumna<byte[]>>(action =>
                                               {
                                                    loPropiedadBytes = loColumna.Value as PropiedadesColumna<byte[]>;
                                                    if (loPropiedadBytes.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadBytes.Valor;
                                                    if (loPropiedadBytes.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadBytes.Valor, null))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadBytes.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadBytes.Valor, SqlDbType.VarBinary));
                                               })
                                               .Case<PropiedadesColumna<string>>(action =>
                                               {
                                                    loPropiedadString = loColumna.Value as PropiedadesColumna<string>;
                                                    if (loPropiedadString.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadString.Valor;
                                                    if (loPropiedadString.EsRequeridoBD)
                                                    {
                                                         if (String.IsNullOrEmpty(loPropiedadString.Valor))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadString.Descripcion));
                                                         }
                                                         if (loPropiedadString.Valor.Length > loPropiedadString.Longitud && loPropiedadString.Longitud > -1)
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("La longitud para el campo {0} no es valido", loPropiedadString.Descripcion));
                                                         }
                                                    }
                                                    // Daniel
                                                    // 20/04/2016
                                                    // Se hace una validacion del valor cuando es null

                                                    if (!String.IsNullOrEmpty(loPropiedadString.Valor) && loPropiedadString.Valor.Length > loPropiedadString.Longitud && loPropiedadString.Longitud > -1)
                                                    {
                                                         throw new Sapei.Framework.SolExcepcion(string.Format("La longitud para el campo {0} no es valido", loPropiedadString.Descripcion));
                                                    }

                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadString.Valor));
                                               })
                                               .Case<PropiedadesColumna<byte?>>(action =>
                                               {
                                                    loPropiedadByteN = loColumna.Value as PropiedadesColumna<byte?>;
                                                    if (loPropiedadByteN.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadByteN.Valor;
                                                    if (loPropiedadByteN.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadByteN.Valor, null))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadByteN.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadByteN.Valor));
                                               })
                                               .Case<PropiedadesColumna<bool?>>(action =>
                                               {
                                                    loPropiedadBoolN = loColumna.Value as PropiedadesColumna<bool?>;
                                                    if (loPropiedadBoolN.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadBoolN.Valor;
                                                    if (loPropiedadBoolN.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadBoolN.Valor, null))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadBoolN.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadBoolN.Valor));
                                               })
                                               .Case<PropiedadesColumna<Int16?>>(action =>
                                               {
                                                    loPropiedadInt16N = loColumna.Value as PropiedadesColumna<Int16?>;
                                                    if (loPropiedadInt16N.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadInt16N.Valor;
                                                    if (loPropiedadInt16N.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadInt16N.Valor, null))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadInt16N.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadInt16N.Valor));
                                               })
                                               .Case<PropiedadesColumna<Int32?>>(action =>
                                               {
                                                    loPropiedadInt32N = loColumna.Value as PropiedadesColumna<Int32?>;
                                                    if (loPropiedadInt32N.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadInt32N.Valor;
                                                    if (loPropiedadInt32N.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadInt32N.Valor, null))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadInt32N.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadInt32N.Valor));
                                               })
                                               .Case<PropiedadesColumna<Int64?>>(action =>
                                               {
                                                    loPropiedadInt64N = loColumna.Value as PropiedadesColumna<Int64?>;
                                                    if (loPropiedadInt64N.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadInt64N.Valor;
                                                    if (loPropiedadInt64N.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadInt64N.Valor, null))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadInt64N.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadInt64N.Valor));
                                               })
                                               .Case<PropiedadesColumna<DateTime?>>(action =>
                                               {
                                                    loPropiedadDateTimeN = loColumna.Value as PropiedadesColumna<DateTime?>;
                                                    if (loPropiedadDateTimeN.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadDateTimeN.Valor;
                                                    if (loPropiedadDateTimeN.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadDateTimeN.Valor, null) || Object.Equals(loPropiedadDateTimeN.Valor, default(DateTime)))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadDateTimeN.Descripcion));
                                                         }
                                                    }
                                                    if (!Object.Equals(loPropiedadDateTimeN.Valor, null) && !Object.Equals(loPropiedadDateTimeN.Valor, default(DateTime)))
                                                         this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, Convert.ToDateTime(loPropiedadDateTimeN.Valor).FechaSqlParametros(loPropiedadDateTimeN.IncluyeHoras)));
                                                    else
                                                         this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadDateTimeN.Valor));
                                               })
                                               .Case<PropiedadesColumna<DateTimeOffset?>>(action =>
                                               {
                                                    loPropiedadDateTimeOffsetN = loColumna.Value as PropiedadesColumna<DateTimeOffset?>;
                                                    if (loPropiedadDateTimeOffsetN.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadDateTimeOffsetN.Valor;
                                                    if (loPropiedadDateTimeOffsetN.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadDateTimeOffsetN.Valor, null) || Object.Equals(loPropiedadDateTimeOffsetN.Valor, default(DateTime)))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadDateTimeOffsetN.Descripcion));
                                                         }
                                                    }
                                                    if (!Object.Equals(loPropiedadDateTimeOffsetN.Valor, null) && !Object.Equals(loPropiedadDateTimeOffsetN.Valor, default(DateTime)))
                                                         this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, Convert.ToDateTime(loPropiedadDateTimeOffsetN.Valor).FechaSqlParametros(loPropiedadDateTimeOffsetN.IncluyeHoras)));
                                                    else
                                                         this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadDateTimeOffsetN.Valor));
                                               })
                                               .Case<PropiedadesColumna<decimal?>>(action =>
                                               {
                                                    loPropiedadDecimalN = loColumna.Value as PropiedadesColumna<decimal?>;
                                                    if (loPropiedadDecimalN.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadDecimalN.Valor;
                                                    if (loPropiedadDecimalN.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadDecimalN.Valor, null))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadDecimalN.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadDecimalN.Valor));
                                               })
                                               .Case<PropiedadesColumna<Single?>>(action =>
                                               {
                                                    loPropiedadSingleN = loColumna.Value as PropiedadesColumna<Single?>;
                                                    if (loPropiedadSingleN.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadSingleN.Valor;
                                                    if (loPropiedadSingleN.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadSingleN.Valor, null))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadSingleN.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadSingleN.Valor));
                                               })
                                               .Case<PropiedadesColumna<double?>>(action =>
                                               {
                                                    loPropiedadDoubleN = loColumna.Value as PropiedadesColumna<double?>;
                                                    if (loPropiedadDoubleN.EsPrimaryKey)
                                                         this.CamposLlave[loColumna.Key] = loPropiedadDoubleN.Valor;
                                                    if (loPropiedadDoubleN.EsRequeridoBD)
                                                    {
                                                         if (Object.Equals(loPropiedadDoubleN.Valor, null))
                                                         {
                                                              throw new Sapei.Framework.SolExcepcion(string.Format("El valor del campo {0} es requerido no puede ir vacio", loPropiedadDoubleN.Descripcion));
                                                         }
                                                    }
                                                    this._oParametrosSQL.Add(new ParametrosSQL(lsNombre, loPropiedadDoubleN.Valor));
                                               });
               }
               this._bExisteCambios = true;
               lsbQuery.AppendFormat("Insert into [{0}].[{1}].[{2}] ({3}) values({4})", this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario, this.NombreTabla,
                    lsbCampos.Remove(lsbCampos.Length - 1, 1),
                    lsbValores.Remove(lsbValores.Length - 1, 1));
               return lsbQuery;
          }


          /// <summary>
          /// Funcion que verifica si existen cambios en la fila del catalogo
          /// </summary>
          protected int VerificaQueExistaCambioEnCampos()
          {
               //Daniel
               // 29-04-2016
               // Esta funcion esta a prueba, pues se hace uso de un switch dinamico
               int liCamposModificados;
               PropiedadesColumna<byte> loPropiedadByte;
               PropiedadesColumna<Int16> loPropiedadInt16;
               PropiedadesColumna<Int32> loPropiedadInt32;
               PropiedadesColumna<Int64> loPropiedadInt64;
               PropiedadesColumna<double> loPropiedadDouble;
               PropiedadesColumna<Single> loPropiedadSingle;
               PropiedadesColumna<bool> loPropiedadBool;
               PropiedadesColumna<byte[]> loPropiedadBytes;
               PropiedadesColumna<string> loPropiedadString;
               PropiedadesColumna<DateTime> loPropiedadDateTime;
               PropiedadesColumna<decimal> loPropiedadDecimal;
               PropiedadesColumna<object> loPropiedadObject;
               PropiedadesColumna<byte?> loPropiedadByteN;
               PropiedadesColumna<Int16?> loPropiedadInt16N;
               PropiedadesColumna<Int32?> loPropiedadInt32N;
               PropiedadesColumna<Int64?> loPropiedadInt64N;
               PropiedadesColumna<double?> loPropiedadDoubleN;
               PropiedadesColumna<Single?> loPropiedadSingleN;
               PropiedadesColumna<bool?> loPropiedadBoolN;
               PropiedadesColumna<DateTime?> loPropiedadDateTimeN;
               PropiedadesColumna<decimal?> loPropiedadDecimalN;
               liCamposModificados = 0;
               foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
               {
                    new Switch(loColumna.Value)
                                               .Case<PropiedadesColumna<bool>>(action =>
                                               {
                                                    loPropiedadBool = loColumna.Value as PropiedadesColumna<bool>;
                                                    if (!Object.Equals(loPropiedadBool.Valor, loPropiedadBool.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<byte>>(action =>
                                               {
                                                    loPropiedadByte = loColumna.Value as PropiedadesColumna<byte>;
                                                    if (!Object.Equals(loPropiedadByte.Valor, loPropiedadByte.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<Int16>>(action =>
                                               {
                                                    loPropiedadInt16 = loColumna.Value as PropiedadesColumna<Int16>;
                                                    if (!Object.Equals(loPropiedadInt16.Valor, loPropiedadInt16.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<Int32>>(action =>
                                               {
                                                    loPropiedadInt32 = loColumna.Value as PropiedadesColumna<Int32>;
                                                    if (!Object.Equals(loPropiedadInt32.Valor, loPropiedadInt32.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<Int64>>(action =>
                                               {
                                                    loPropiedadInt64 = loColumna.Value as PropiedadesColumna<Int64>;
                                                    if (!Object.Equals(loPropiedadInt64.Valor, loPropiedadInt64.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<DateTime>>(action =>
                                               {
                                                    loPropiedadDateTime = loColumna.Value as PropiedadesColumna<DateTime>;
                                                    if (!Object.Equals(loPropiedadDateTime.Valor, loPropiedadDateTime.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<decimal>>(action =>
                                               {
                                                    loPropiedadDecimal = loColumna.Value as PropiedadesColumna<decimal>;
                                                    if (!Object.Equals(loPropiedadDecimal.Valor, loPropiedadDecimal.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<Single>>(action =>
                                               {
                                                    loPropiedadSingle = loColumna.Value as PropiedadesColumna<Single>;
                                                    if (!Object.Equals(loPropiedadSingle.Valor, loPropiedadSingle.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<double>>(action =>
                                               {
                                                    loPropiedadDouble = loColumna.Value as PropiedadesColumna<double>;
                                                    if (!Object.Equals(loPropiedadDouble.Valor, loPropiedadDouble.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<object>>(action =>
                                               {
                                                    loPropiedadObject = loColumna.Value as PropiedadesColumna<object>;
                                                    if (!Object.Equals(loPropiedadObject.Valor, loPropiedadObject.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<byte[]>>(action =>
                                               {
                                                    loPropiedadBytes = loColumna.Value as PropiedadesColumna<byte[]>;
                                                    if (!Object.Equals(loPropiedadBytes.Valor, loPropiedadBytes.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<string>>(action =>
                                               {
                                                    loPropiedadString = loColumna.Value as PropiedadesColumna<string>;
                                                    if (!Object.Equals(loPropiedadString.Valor, loPropiedadString.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<byte?>>(action =>
                                               {
                                                    loPropiedadByteN = loColumna.Value as PropiedadesColumna<byte?>;
                                                    if (!Object.Equals(loPropiedadByteN.Valor, loPropiedadByteN.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<bool?>>(action =>
                                               {
                                                    loPropiedadBoolN = loColumna.Value as PropiedadesColumna<bool?>;
                                                    if (!Object.Equals(loPropiedadBoolN.Valor, loPropiedadBoolN.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<Int16?>>(action =>
                                               {
                                                    loPropiedadInt16N = loColumna.Value as PropiedadesColumna<Int16?>;
                                                    if (!Object.Equals(loPropiedadInt16N.Valor, loPropiedadInt16N.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<Int32?>>(action =>
                                               {
                                                    loPropiedadInt32N = loColumna.Value as PropiedadesColumna<Int32?>;
                                                    if (!Object.Equals(loPropiedadInt32N.Valor, loPropiedadInt32N.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<Int64?>>(action =>
                                               {
                                                    loPropiedadInt64N = loColumna.Value as PropiedadesColumna<Int64?>;
                                                    if (!Object.Equals(loPropiedadInt64N.Valor, loPropiedadInt64N.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<DateTime?>>(action =>
                                               {
                                                    loPropiedadDateTimeN = loColumna.Value as PropiedadesColumna<DateTime?>;
                                                    if (!Object.Equals(loPropiedadDateTimeN.Valor, loPropiedadDateTimeN.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })

                                               .Case<PropiedadesColumna<decimal?>>(action =>
                                               {
                                                    loPropiedadDecimalN = loColumna.Value as PropiedadesColumna<decimal?>;
                                                    if (!Object.Equals(loPropiedadDecimalN.Valor, loPropiedadDecimalN.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<Single?>>(action =>
                                               {
                                                    loPropiedadSingleN = loColumna.Value as PropiedadesColumna<Single?>;
                                                    if (!Object.Equals(loPropiedadSingleN.Valor, loPropiedadSingleN.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               })
                                               .Case<PropiedadesColumna<double?>>(action =>
                                               {
                                                    loPropiedadDoubleN = loColumna.Value as PropiedadesColumna<double?>;
                                                    if (!Object.Equals(loPropiedadDoubleN.Valor, loPropiedadDoubleN.ValorAnterior))
                                                    {
                                                         liCamposModificados++;
                                                    }
                                               });
               }
               return liCamposModificados;
          }

          /// <summary>
          /// Funcion que deterina si existe cambio en los campos
          /// </summary>
          /// <returns></returns>
          public bool ExistenCambios()
          {
               if (this.VerificaQueExistaCambioEnCampos() > 0)
               {
                    return true;
               }
               return false;
          }

          #endregion


          #region Metodos generales para los catálogos

          /// <summary>
          /// Funcion que regresa los campos llave seprados por comas
          /// </summary>
          /// <returns></returns>
          public string RegresaCamposLlaveSeparadosPorComas()
          {
               StringBuilder lsSelect;
               lsSelect = new StringBuilder();

               foreach (KeyValuePair<string, object> lsCampoLlave in this.CamposLlave)
               {
                    lsSelect.AppendFormat("{0},", this.ArreglaNombreCampo(lsCampoLlave.Key));
               }
               lsSelect.Remove(lsSelect.Length - 1, 1);

               return lsSelect.ToString();
          }

          /// <summary>
          /// Regresas the lista campos.
          /// </summary>
          /// <returns></returns>
          public List<string> RegresaListaCampos()
          {
               List<string> lstCampos;
               lstCampos = new List<string>();
               foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
               {
                    lstCampos.Add(loColumna.Key);
               }
               return lstCampos;
          }

          /// <summary>
          /// Armas the filtro campos l lave datos cargados.
          /// </summary>
          /// <returns></returns>
          private string ArmaFiltroCamposLLaveDatosCargados()
          {
               StringBuilder lsbCamposLlave;
               lsbCamposLlave = new StringBuilder();
               PropiedadesColumna<byte> loPropiedadByte;
               PropiedadesColumna<Int16> loPropiedadInt16;
               PropiedadesColumna<Int32> loPropiedadInt32;
               PropiedadesColumna<Int64> loPropiedadInt64;
               PropiedadesColumna<double> loPropiedadDouble;
               PropiedadesColumna<Single> loPropiedadSingle;
               PropiedadesColumna<bool> loPropiedadBool;
               PropiedadesColumna<byte[]> loPropiedadBytes;
               PropiedadesColumna<string> loPropiedadString;
               PropiedadesColumna<DateTime> loPropiedadDateTime;
               PropiedadesColumna<decimal> loPropiedadDecimal;
               PropiedadesColumna<object> loPropiedadObject;
               foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
               {
                    if (loColumna.Value is PropiedadesColumna<bool>)
                    {
                         loPropiedadBool = loColumna.Value as PropiedadesColumna<bool>;
                         if (loPropiedadBool.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(this.ArreglaNombreCampo(loColumna.Key)), loPropiedadBool.Valor);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<byte>)
                    {
                         loPropiedadByte = loColumna.Value as PropiedadesColumna<byte>;
                         if (loPropiedadByte.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadByte.Valor);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<Int16>)
                    {
                         loPropiedadInt16 = loColumna.Value as PropiedadesColumna<Int16>;
                         if (loPropiedadInt16.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadInt16.Valor);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<Int32>)
                    {
                         loPropiedadInt32 = loColumna.Value as PropiedadesColumna<Int32>;
                         if (loPropiedadInt32.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadInt32.Valor);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<Int64>)
                    {
                         loPropiedadInt64 = loColumna.Value as PropiedadesColumna<Int64>;
                         if (loPropiedadInt64.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadInt64.Valor);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<DateTime>)
                    {
                         loPropiedadDateTime = loColumna.Value as PropiedadesColumna<DateTime>;
                         if (loPropiedadDateTime.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadDateTime.Valor.FechaSQL());
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<decimal>)
                    {
                         loPropiedadDecimal = loColumna.Value as PropiedadesColumna<decimal>;
                         if (loPropiedadDecimal.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadDecimal.Valor);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<Single>)
                    {
                         loPropiedadSingle = loColumna.Value as PropiedadesColumna<Single>;
                         if (loPropiedadSingle.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadSingle.Valor);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<double>)
                    {
                         loPropiedadDouble = loColumna.Value as PropiedadesColumna<double>;
                         if (loPropiedadDouble.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadDouble.Valor);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<object>)
                    {
                         loPropiedadObject = loColumna.Value as PropiedadesColumna<object>;
                         if (loPropiedadObject.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}={1} AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadObject.Valor);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<byte[]>)
                    {
                         loPropiedadBytes = loColumna.Value as PropiedadesColumna<byte[]>;
                         if (loPropiedadBytes.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}='{1}' AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadBytes.Valor);
                         }
                    }
                    if (loColumna.Value is PropiedadesColumna<string>)
                    {
                         loPropiedadString = loColumna.Value as PropiedadesColumna<string>;
                         if (loPropiedadString.EsPrimaryKey)
                         {
                              lsbCamposLlave.AppendFormat("{0}='{1}' AND ", this.ArreglaNombreCampo(loColumna.Key), loPropiedadString.Valor);
                         }
                    }
               }
               return lsbCamposLlave.Remove(lsbCamposLlave.Length - 5, 5).ToString();
          }

          #endregion

          #region Consultas para el historial

          /// <summary>
          /// Funcion que arma la consulta para crear la tabla del historial
          /// </summary>
          /// <returns></returns>
          protected StringBuilder ArmaConsultaTablaHistorial()
          {
               StringBuilder lsbQuery;
               //string lsServidor;
               ////List<string> lstCamposLLave;
               lsbQuery = new StringBuilder();
               //lsbQuery.Append("Select  ");
               //lsbQuery.Append(" IDENTITY(INT, 1, 1) AS _Folio");
               //lsbQuery.AppendFormat(", convert(varchar(50),'{0}') _usuAbreviacion", this._oSistema.Sesion.Usuario.Abreviacion);
               //lsbQuery.AppendFormat(", {0} _usuClave", this._oSistema.Sesion.Usuario.Numero);
               //lsbQuery.Append(", getdate() _Fecha");
               //lsbQuery.AppendFormat(", {0} _Movimiento", (int)enmMovimientos.Alta);
               //lsbQuery.Append(", '' XMLCampo");
               //foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
               //{
               //     lsbQuery.AppendFormat(", [{0}].[{1}].[{2}].[{3}] ", this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario, this.NombreTabla, loColumna.Key);
               //}
               //if (String.IsNullOrEmpty(this._oSistema.Servidor.Principal.BaseDatos.Bitacora))
               //{
               //     lsbQuery.AppendFormat(" into [{2}].[{3}].[{0}{1}]", this._oSistema.Conexion.BDBitacoraPrefijoObj, this.NombreTabla, this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario);
               //}
               //else
               //{
               //     lsbQuery.AppendFormat(" into [{0}].[{3}].[{2}{1}]", this._oSistema.Servidor.Principal.BaseDatos.Bitacora, this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, this.Propietario);
               //}
               //lsbQuery.AppendFormat(" From [{1}].[{2}].[{0}];", this.NombreTabla, this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario);
               //if (String.IsNullOrEmpty(this._oSistema.Servidor.Principal.BaseDatos.Bitacora))
               //{
               //     lsServidor = this._oSistema.Servidor.Principal.BaseDatos.Catalogo;
               //}
               //else
               //{
               //     lsServidor = this._oSistema.Servidor.Principal.BaseDatos.Bitacora;
               //}
               //lsbQuery.AppendFormat(" Alter table [{2}].[{3}].[{1}{0}] Alter Column _Folio int not null;",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.AppendFormat(" Alter table [{2}].[{3}].[{1}{0}] Alter Column _usuAbreviacion varchar(50) not null;",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.AppendFormat(" Alter table [{2}].[{3}].[{1}{0}] Alter Column _usuClave int not null;",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.AppendFormat(" Alter table [{2}].[{3}].[{1}{0}] Alter Column _Fecha datetime not null;",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.AppendFormat(" Alter table [{2}].[{3}].[{1}{0}] Alter Column _Movimiento int not null;",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.AppendFormat(" Alter table [{2}].[{3}].[{1}{0}] Alter Column XMLCampo xml null;",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.AppendFormat(" Alter table [{2}].[{3}].[{1}{0}] Add  Constraint [DF_{1}{0}_Fecha]  DEFAULT (getdate()) FOR [_Fecha];",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.AppendFormat(" Alter table [{2}].[{3}].[{1}{0}] ADD CONSTRAINT [PK_{1}{0}] PRIMARY KEY CLUSTERED( ",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.Append(" [_Folio] ASC,");
               //lsbQuery.Append(" [_usuAbreviacion] ASC,");
               //lsbQuery.Append(" [_usuClave] ASC,");
               //lsbQuery.Append(" [_Fecha] ASC,");
               //lsbQuery.Append(" [_Movimiento] ASC");
               ////lstCamposLLave = RegresaCamposLLave();
               ////foreach (string loCampoPK in lstCamposLLave)
               ////{
               ////     lsbQuery.AppendFormat(", [{0}] ASC", loCampoPK);
               ////}
               //foreach (KeyValuePair<string, object> loCampoPK in this.CamposLlave)
               //{
               //     lsbQuery.AppendFormat(", [{0}] ASC", loCampoPK.Key);
               //}
               //lsbQuery.Append(" );");
               //// Create primary index.
               //lsbQuery.AppendFormat(" CREATE PRIMARY XML INDEX idx_XMLCampo_{1}{0} on [{2}].[{3}].[{1}{0}](XMLCampo)",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //// Create secondary indexes (PATH, VALUE, PROPERTY).
               //lsbQuery.AppendFormat(" CREATE XML INDEX idx_XMLCampo_{1}{0}_PATH ON [{2}].[{3}].[{1}{0}](XMLCampo)",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.AppendFormat(" USING XML INDEX idx_XMLCampo_{1}{0}",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj);
               //lsbQuery.Append(" FOR PATH;");
               //lsbQuery.AppendFormat(" CREATE XML INDEX idx_XMLCampo_{1}{0}_VALUE ON [{2}].[{3}].[{1}{0}](XMLCampo)",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.AppendFormat(" USING XML INDEX idx_XMLCampo_{1}{0}", this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj);
               //lsbQuery.Append(" FOR VALUE;");
               //lsbQuery.AppendFormat(" CREATE XML INDEX idx_XMLCampo_{1}{0}_PROPERTY ON [{2}].[{3}].[{1}{0}](XMLCampo)",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj, lsServidor, this.Propietario);
               //lsbQuery.AppendFormat(" USING XML INDEX idx_XMLCampo_{1}{0}",
               //     this.NombreTabla, this._oSistema.Conexion.BDBitacoraPrefijoObj);
               //lsbQuery.Append(" FOR PROPERTY;");

               //lsbQuery.AppendFormat(" Update [{0}].[{1}].[{2}{3}] set XMLCampo = NULL;",
               //     lsServidor, this.Propietario, this._oSistema.Conexion.BDBitacoraPrefijoObj, this.NombreTabla);
               return lsbQuery;
          }

          /// <summary>
          /// Funcion que inserta datos en la tabla de bitacoras
          /// </summary>
          /// <param name="penMovimiento">Tipo de movimiento Alta, Baja, MOdificacion</param>
          /// <param name="psXMLCampo">XML que se genero</param>
          /// <returns></returns>
          protected StringBuilder ArmaConsultaParaBitacora(enmMovimientos penMovimiento, string psXMLCampo)
          {
               StringBuilder lsbQuery;

               lsbQuery = new StringBuilder();
               //? ver la manera de quitar el insert into select por un insert into
               if (String.IsNullOrEmpty(this._oSistema.Servidor.Principal.BaseDatos.Bitacora))
               {
                    lsbQuery.AppendFormat("INSERT INTO [{0}].[{1}].[{2}{3}]",
                         this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario, this._oSistema.Conexion.BDBitacoraPrefijoObj, this.NombreTabla);
               }
               else
               {
                    lsbQuery.AppendFormat("INSERT INTO [{0}].[{1}].[{2}{3}]",
                         this._oSistema.Servidor.Principal.BaseDatos.Bitacora, this.Propietario, this._oSistema.Conexion.BDBitacoraPrefijoObj, this.NombreTabla);
               }
               //Colocamos los nombres de los campos para lo haga por referncia aunque los campos
               //no esten en cierto orden
               lsbQuery.Append("( _usuAbreviacion,_usuClave,_Fecha,_Movimiento, XMLCAMPO");
               foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
               {
                    lsbQuery.AppendFormat(", {0} ", loColumna.Key);
               }
               lsbQuery.Append(") ");
               lsbQuery.Append("Select ");
               //lsbQuery.AppendFormat(",{0} _usuClave", this._oSistema.Sesion.Usuario.Clave);
               lsbQuery.Append(",GETDATE() _Fecha");
               lsbQuery.AppendFormat(", {0} _Momiviento", (int)penMovimiento);
               if (!String.IsNullOrEmpty(psXMLCampo))
               {
                    lsbQuery.AppendFormat(", [{1}].[udf_LimpiaXML]('{0}') XMLCampo", psXMLCampo, this.Propietario);
               }
               else
               {
                    lsbQuery.Append(", NULL ");
               }
               foreach (KeyValuePair<string, Propiedad> loColumna in this.Propiedades)
               {
                    lsbQuery.AppendFormat(", [{0}].[{1}].[{2}].[{3}] ", this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario, this.NombreTabla, loColumna.Key);
               }
               lsbQuery.AppendFormat(" From [{0}].[{1}].[{2}]", this._oSistema.Servidor.Principal.BaseDatos.Catalogo, this.Propietario, this.NombreTabla);
               lsbQuery.AppendFormat(" Where {0}", this.ArmaWhereDeCamposLlaveParametrosSql());
               return lsbQuery;
          }

          #endregion
     }
}
