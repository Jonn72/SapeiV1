using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Sapei.Framework.BaseDatos;

namespace Sapei.Framework.Datos
{
     /// <summary>
     /// Clase que proporciana metodos para operar listado de la calse CatalogoFijoElemento
     /// </summary>
     /// <typeparam name="T">Cualquier clase que herede de la clase CatalogoFijoElemento</typeparam>
     [Serializable]
     public class CatalogoFijoElementos<T> where T : CatalogoFijoElemento, new()
     {
          #region variables

          /// <summary>
          /// Variables del sistema
          /// </summary>
          protected internal Sistema _oSistema;
          /// <summary>
          /// The _o catalogo
          /// </summary>
          protected internal T _oCatalogo;

          #endregion

          #region Propiedades

          #endregion

          #region Contructor

          /// <summary>
          /// Constructor con la variable del sistema
          /// </summary>
          /// <param name="poSistema"></param>
          public CatalogoFijoElementos(Sistema poSistema)
          {
               _oSistema = poSistema;
               _oCatalogo = new T();
               _oCatalogo._oSistema = poSistema;
               _oCatalogo.ArmaCamposSeparadosPorComas();
          }

          #endregion

          #region Metodos para CatalogoFijoElementos o lista de objetos

          /// <summary>
          /// Inserta masivamente la informacion de la tabla
          /// TODO: Hacer un bulkcopy sin las bitacoras para cuando se necesita almacenar un conjunto masivo de informacion en la base de datos          
          /// </summary>
          /// <param name="poObjeto">The po objeto.</param>
          public void InsertaMasivo(IEnumerable<T> poObjeto)
          {
               _oSistema.Conexion.InsertBulkCopy<T>(poObjeto, _oCatalogo.NombreTabla);
          }

          #region DataTable
          /// <summary>
          /// Regresas the data table.
          /// </summary>
          /// <param name="psSelectCampos">The ps select campos.</param>
          /// <param name="psFiltro">The ps filtro.</param>
          /// <returns></returns>
          public DataTable RegresaDataTable(StringBuilder psSelectCampos, StringBuilder psFiltro)
          {
               return RegresaDataTable(psSelectCampos.ToString(), "", false, psFiltro.ToString());
          }

          /// <summary>
          /// Regresas the data table.
          /// </summary>
          /// <param name="psSelectCampos">The ps select campos.</param>
          /// <param name="psFiltro">The ps filtro.</param>
          /// <param name="psOrden">The ps orden.</param>
          /// <returns></returns>
          public DataTable RegresaDataTable(StringBuilder psSelectCampos, StringBuilder psFiltro, string psOrden)
          {
               return RegresaDataTable(psSelectCampos.ToString(), "", false, psFiltro.ToString(), psOrden);
          }
          /// <summary>
          /// Funcion que regresa un data table del catalgo
          /// </summary>
          /// <param name="psSelectCampos">Campos </param>
          /// <param name="psFiltroEspecial">Filtro especial</param>          
          /// <param name="pbFiltrarporCamposLlave">if set to <c>true</c> [pb filtrarpor campos llave].</param>
          /// <param name="psFiltroPrincipal">The ps filtro principal.</param>
          /// <param name="psOrden">The ps orden.</param>
          /// <returns></returns>
          public DataTable RegresaDataTable(string psSelectCampos = null, string psFiltroEspecial = null, Boolean pbFiltrarporCamposLlave = true, string psFiltroPrincipal = null, string psOrden = null)
          {
               DataTable ldtDatos = null;
               StringBuilder lsQuery;
               try
               {
                    lsQuery = new StringBuilder();
                    if (string.IsNullOrEmpty(psSelectCampos))
                         psSelectCampos = _oCatalogo._sCamposLLave.ToString();
                    lsQuery.AppendFormat("Select {0} from [{1}].[{2}].[{3}{4}] where ", psSelectCampos, _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oCatalogo.Propietario, _oSistema.Servidor.Principal.BaseDatos.Prefijo, _oCatalogo.NombreTabla);
                    if (pbFiltrarporCamposLlave)
                         lsQuery.AppendFormat(" {0} = {1}", _oCatalogo.CamposLlave.First().Key, _oSistema.Sesion.Institucion.Numero);
                    else
                         lsQuery.Append(psFiltroPrincipal);
                    if (!string.IsNullOrEmpty(psFiltroEspecial))
                         lsQuery.AppendFormat(" and {0}", psFiltroEspecial);
                    if (!string.IsNullOrEmpty(psOrden))
                         lsQuery.AppendFormat(" Order by {0}", psOrden);
                    ldtDatos = _oSistema.Conexion.RegresaDataTable(lsQuery, _oCatalogo.NombreTabla);
                    return ldtDatos;
               }
               finally
               {
                    //FuncionesDatos.LiberaObjeto(ref ldtDatos);  //no se libera porque si no se destruye el objeto y luego no se puede hacer un bind
               }
          }

          public DataTable RegresaDataTable()
          {
               DataTable ldtDatos = null;
               StringBuilder lsQuery;
               string lsSelectCampos;
               try
               {
                    lsQuery = new StringBuilder();
                    lsSelectCampos = _oCatalogo._sCamposLLave.ToString();
                    lsQuery.AppendFormat("Select {0} from [{1}].[{2}].[{3}{4}] ", lsSelectCampos, _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oCatalogo.Propietario, _oSistema.Servidor.Principal.BaseDatos.Prefijo, _oCatalogo.NombreTabla);
                    ldtDatos = _oSistema.Conexion.RegresaDataTable(lsQuery, _oCatalogo.NombreTabla);
                    return ldtDatos;
               }
               finally
               {
                    //FuncionesDatos.LiberaObjeto(ref ldtDatos);  //no se libera porque si no se destruye el objeto y luego no se puede hacer un bind
               }
          }
          /// <summary>
          /// Regresa un data table de la tabla de historial
          /// </summary>
          /// <param name="psSelectCampos">The ps select campos.</param>
          /// <param name="psFiltroEspecial">The ps filtro especial.</param>
          /// <param name="pbFiltrarporCamposLlave">if set to <c>true</c> [pb filtrarpor campos llave].</param>
          /// <param name="psFiltroPrincipal">The ps filtro principal.</param>
          /// <returns></returns>
          public DataTable RegresaDataTabledelHistorial(string psSelectCampos = null, string psFiltroEspecial = null, Boolean pbFiltrarporCamposLlave = true, string psFiltroPrincipal = null)
          {
               DataTable ldtDatos = null;
               StringBuilder lsQuery;
               string lsServidor;
               try
               {
                    //! Asumimos que existe la tabla de historial
                    lsQuery = new StringBuilder();
                    if (string.IsNullOrEmpty(psSelectCampos))
                         psSelectCampos = _oCatalogo._sCamposLLave.ToString();
                    if (String.IsNullOrEmpty(_oSistema.Servidor.Principal.BaseDatos.Bitacora))
                         lsServidor = _oSistema.Servidor.Principal.BaseDatos.Catalogo;
                    else
                         lsServidor = _oSistema.Servidor.Principal.BaseDatos.Bitacora;
                    lsQuery.AppendFormat("Select {0} from [{1}].[{2}].[{3}{4}{5}] where ",
                         psSelectCampos,
                         lsServidor,
                         _oCatalogo.Propietario,
                         _oSistema.Servidor.Principal.BaseDatos.Prefijo,
                         _oSistema.Conexion.BDBitacoraPrefijoObj,
                         _oCatalogo.NombreTabla);
                    if (pbFiltrarporCamposLlave)
                    {
                         lsQuery.AppendFormat(" {0} = {1}",
                              _oCatalogo.CamposLlave.First().Key,
                              _oSistema.Sesion.Institucion.Numero);
                    }
                    else
                    {
                         lsQuery.Append(psFiltroPrincipal);
                    }
                    if (!string.IsNullOrEmpty(psFiltroEspecial))
                    {
                         lsQuery.AppendFormat(" and {0}", psFiltroEspecial);
                    }
                    ldtDatos = _oSistema.Conexion.RegresaDataTable(lsQuery, _oCatalogo.NombreTabla);
                    return ldtDatos;
               }
               finally
               {
                    //FuncionesDatos.LiberaObjeto(ref ldtDatos);  //no se libera porque si no se destruye el objeto y luego no se puede hacer un bind
               }
          }

          /// <summary>
          /// Funcion que regresa un data table del catalgo
          /// </summary>
          /// <param name="psSelectCampos">Campos </param>
          /// <param name="psFiltroCampos">Filtro especial</param>
          /// <returns></returns>
          public DataTable RegresaDataTableGenerico(string psSelectCampos, string psFiltroCampos)
          {
               DataTable ldtDatos = null;
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat("Select {0} from [{1}].[{2}].[{3}{4}]",
                    psSelectCampos,
                    _oSistema.Servidor.Principal.BaseDatos.Catalogo,
                    _oCatalogo.Propietario,
                    _oSistema.Servidor.Principal.BaseDatos.Prefijo, _oCatalogo.NombreTabla);
               lsQuery.AppendFormat(" where {0}", psFiltroCampos);
               ldtDatos = _oSistema.Conexion.RegresaDataTable(lsQuery, _oCatalogo.NombreTabla);
               return ldtDatos;
          }

          /// <summary>
          /// Regresas the data table.
          /// </summary>
          /// <param name="psFiltroEspecial">The ps filtro especial.</param>
          /// <param name="pbIncluyeFiltroInternoporInstitucion">if set to <c>true</c> [pb incluye filtro internopor Institucion].</param>
          /// <param name="psOrden">The ps orden.</param>
          /// <returns></returns>
          public DataTable RegresaDataTable(StringBuilder psFiltroEspecial, bool pbIncluyeFiltroInternoporInstitucion, string psOrden)
          {
               return RegresaDataTable(psFiltroEspecial.ToString(), pbIncluyeFiltroInternoporInstitucion, psOrden);
          }

          /// <summary>
          /// Regresas the data table datos.
          /// </summary>
          /// <param name="psFiltroEspecial">The ps filtro especial.</param>
          /// <param name="pbIncluyeFiltroInternoporInstitucion">if set to <c>true</c> [pb incluye filtro internopor Institucion].</param>
          /// <param name="psOrden">The ps orden.</param>
          /// <returns></returns>
          public DataTable RegresaDataTable(string psFiltroEspecial, bool pbIncluyeFiltroInternoporInstitucion, string psOrden)
          {
               StringBuilder lsQuery;
               string lsSelect;
               // T loCatalogo;
               lsQuery = new StringBuilder();
               lsSelect = _oCatalogo._sCamposLLave.ToString();
               lsQuery.AppendFormat("Select {0} from [{1}].[{2}].[{3}{4}]",
                    lsSelect,
                    _oSistema.Servidor.Principal.BaseDatos.Catalogo,
                    _oCatalogo.Propietario,
                    _oSistema.Servidor.Principal.BaseDatos.Prefijo, _oCatalogo.NombreTabla);
               if (pbIncluyeFiltroInternoporInstitucion)
               {
                    lsQuery.AppendFormat(" where {0} = {1} ",
                         _oCatalogo.CamposLlave.First().Key,
                         _oSistema.Sesion.Institucion.Numero);
                    if (!string.IsNullOrEmpty(psFiltroEspecial))
                    {
                         lsQuery.AppendFormat(" and {0}", psFiltroEspecial);
                    }
               }
               else
               {
                    if (!string.IsNullOrEmpty(psFiltroEspecial))
                    {
                         lsQuery.AppendFormat(" where {0}", psFiltroEspecial);
                    }
               }
               if (!String.IsNullOrEmpty(psOrden))
                    lsQuery.AppendFormat(" Order by {0}", psOrden);
               return _oSistema.Conexion.RegresaDataTable(lsQuery, _oCatalogo.NombreTabla);
          }
          #endregion

          #region Combos

          #endregion

          #region Colecciones
          /// <summary>
          /// Regresa la lista de los elementos segun  el catalogo 
          /// </summary>
          /// <param name="pdtDatos">El datatable debe contener los campos PK de la tabla para que pueda cargar los elementos</param>
          /// <returns></returns>
          public List<T> RegresaLista(DataTable pdtDatos)
          {
               //Limitamos el tamaño de la lista
               T loCatalogoFijoElemento;
               List<T> loListaCatalogoFijoElementos;
               loListaCatalogoFijoElementos = new List<T>(pdtDatos.Rows.Count);//Se estable el capacity para hacer mas eficiente la lista al crear los elementos               
               foreach (DataRow loFilaDatos in pdtDatos.Rows)
               {
                    loCatalogoFijoElemento = new T();
                    loCatalogoFijoElemento._oSistema = _oSistema;
                    loCatalogoFijoElemento.CargaFilaDeDatos(loFilaDatos);
                    //Esto se pude mejorar solo recorriendo el datatable
                    //ya no es necesario hacer mas consultas a la base de datos
                    //for (liContador = 0; liContador < loCatalogoFijoElemento.ListaCamposLlave.Count; liContador++)                    
                    //     lasArgumentoCarga[liContador] = loRow[loCatalogoFijoElemento.ListaCamposLlave[liContador]];                    
                    //loCatalogoFijoElemento.Cargar(lasArgumentoCarga);
                    //if (!loCatalogoFijoElemento.EOF)
                    loListaCatalogoFijoElementos.Add(loCatalogoFijoElemento);
               }
               return loListaCatalogoFijoElementos;
          }

          /// <summary>
          /// Regresas the coleccion.
          /// </summary>
          /// <param name="pdtDatos">The PDT datos.</param>
          /// <returns></returns>
          public HashSet<T> RegresaColeccion(DataTable pdtDatos)
          {
               //Limitamos el tamaño de la lista
               T loCatalogoFijoElemento;
               HashSet<T> loListaCatalogoFijoElementos;
               loListaCatalogoFijoElementos = new HashSet<T>();//Se estable el capacity para hacer mas eficiente la lista al crear los elementos               
               foreach (DataRow loFilaDatos in pdtDatos.Rows)
               {
                    loCatalogoFijoElemento = new T();
                    loCatalogoFijoElemento._oSistema = _oSistema;
                    loCatalogoFijoElemento.CargaFilaDeDatos(loFilaDatos);
                    //Esto se pude mejorar solo recorriendo el datatable
                    //ya no es necesario hacer mas consultas a la base de datos
                    //for (liContador = 0; liContador < loCatalogoFijoElemento.ListaCamposLlave.Count; liContador++)                    
                    //     lasArgumentoCarga[liContador] = loRow[loCatalogoFijoElemento.ListaCamposLlave[liContador]];                    
                    //loCatalogoFijoElemento.Cargar(lasArgumentoCarga);
                    //if (!loCatalogoFijoElemento.EOF)
                    loListaCatalogoFijoElementos.Add(loCatalogoFijoElemento);
               }
               return loListaCatalogoFijoElementos;
          }

          /// <summary>
          /// Funcion que regresa una lista de T (Catalogo)
          /// </summary>
          /// <param name="psFiltroEspecial">Filtro espaecial</param>
          /// <param name="pbIncluyeFiltroInternoporInstitucion">Habilita si filtra por Institucion o no</param>
          /// <param name="psOrden">The ps orden.</param>
          /// <returns></returns>
          public List<T> RegresaLista(StringBuilder psFiltroEspecial = null, bool pbIncluyeFiltroInternoporInstitucion = true, string psOrden = null)
          {
               return RegresaLista(psFiltroEspecial.ToString(), pbIncluyeFiltroInternoporInstitucion, psOrden);
          }

          /// <summary>
          /// Funcion que regresa una lista de T (Catalogo)
          /// </summary>
          /// <param name="psFiltroEspecial">Filtro espaecial</param>
          /// <param name="pbIncluyeFiltroInternoporInstitucion">Habilita si filtra por Institucion o no</param>
          /// <param name="psOrden">The ps orden.</param>
          /// <returns></returns>
          public List<T> RegresaLista(string psFiltroEspecial = null, bool pbIncluyeFiltroInternoporInstitucion = true, string psOrden = null)
          {
               return RegresaLista(RegresaDataTable(psFiltroEspecial, pbIncluyeFiltroInternoporInstitucion, psOrden));
          }

          /// <summary>
          /// Regresas the coleccion.
          /// </summary>
          /// <param name="psFiltroEspecial">The ps filtro especial.</param>
          /// <param name="pbIncluyeFiltroInternoporInstitucion">if set to <c>true</c> [pb incluye filtro internopor Institucion].</param>
          /// <param name="psOrden">The ps orden.</param>
          /// <returns></returns>
          public HashSet<T> RegresaColeccion(string psFiltroEspecial = null, bool pbIncluyeFiltroInternoporInstitucion = true, string psOrden = null)
          {
               return RegresaColeccion(RegresaDataTable(psFiltroEspecial, pbIncluyeFiltroInternoporInstitucion, psOrden));
          }

          /// <summary>
          /// Regresas the coleccion.
          /// </summary>
          /// <param name="psFiltroEspecial">The ps filtro especial.</param>
          /// <param name="pbIncluyeFiltroInternoporInstitucion">if set to <c>true</c> [pb incluye filtro internopor Institucion].</param>
          /// <param name="psOrden">The ps orden.</param>
          /// <returns></returns>
          public HashSet<T> RegresaColeccion(StringBuilder psFiltroEspecial = null, bool pbIncluyeFiltroInternoporInstitucion = true, string psOrden = null)
          {
               return RegresaColeccion(RegresaDataTable(psFiltroEspecial.ToString(), pbIncluyeFiltroInternoporInstitucion, psOrden));
          }
          /// <summary>
          /// Regresa ArrayList para llenar los combos segun el filtro
          /// </summary>
          /// <param name="psSelectCampos">Camspos </param>
          /// <param name="psFiltroEspecial">Filtro especial que se une al filtro principal</param>
          /// <param name="pbFiltrarporCamposLlave">Filtra por campo llaves</param>
          /// <param name="psFiltroPrincipal">Filtro princial</param>
          ///  <param name="psOrden">Order de los elementos del filtro.</param>
          /// <returns></returns>
          public ArrayList RegresaCombo(string psSelectCampos = null, string psFiltroEspecial = null, Boolean pbFiltrarporCamposLlave = true, string psFiltroPrincipal = null, string psOrden = null)
          {
               DataTable ldtDatos = null;
               ArrayList lasCombo = null;
               StringBuilder lsQuery;
               //  T loCatalogo;
               try
               {
                    lasCombo = new ArrayList();
                    lsQuery = new StringBuilder();
                    lsQuery = ArmaConsultaParaCombo(psSelectCampos, psFiltroEspecial, pbFiltrarporCamposLlave, psFiltroPrincipal, psOrden);
                    ldtDatos = _oSistema.Conexion.RegresaDataTable(lsQuery, _oCatalogo.NombreTabla);
                    foreach (DataRow loFila in ldtDatos.Rows)
                    {
                         if (loFila.ItemArray.Length < 2)
                         {
                              //Pregunto si por lo menos existen dos elementos en la tabla                              
                              continue;
                         }
                         lasCombo.Add(string.Format("{0},{1}", loFila[0].ToString(), loFila[1].ToString()));
                    }
                    return lasCombo;
               }
               finally
               {
                    // FuncionesDatos.LiberaObjeto(ref ldtDatos);
               }
          }

          /// <summary>
          /// Regresas the coleccion combo.
          /// </summary>
          /// <param name="psSelectCampos">The ps select campos.</param>
          /// <param name="psFiltroEspecial">The ps filtro especial.</param>
          /// <param name="pbFiltrarporCamposLlave">if set to <c>true</c> [pb filtrarpor campos llave].</param>
          /// <param name="psFiltroPrincipal">The ps filtro principal.</param>
          /// <param name="psOrden">The ps orden.</param>
          /// <returns></returns>
          public HashSet<string> RegresaColeccionCombo(string psSelectCampos, string psFiltroEspecial, bool pbFiltrarporCamposLlave = true, string psFiltroPrincipal = null, string psOrden = null)
          {
               DataTable ldtDatos = null;
               HashSet<string> loCombo;
               StringBuilder lsQuery;
               //T loCatalogo;               
               loCombo = new HashSet<string>();
               lsQuery = ArmaConsultaParaCombo(psSelectCampos, psFiltroEspecial, pbFiltrarporCamposLlave, psFiltroPrincipal, psOrden);
               ldtDatos = _oSistema.Conexion.RegresaDataTable(lsQuery, _oCatalogo.NombreTabla);
               foreach (DataRow loFila in ldtDatos.Rows)
               {
                    if (loFila.ItemArray.Length < 2)
                         //Pregunto si por lo menos existen dos elementos en la tabla                              
                         continue;
                    loCombo.Add(string.Format("{0},{1}", loFila[0].ToString(), loFila[1].ToString()));
               }
               return loCombo;
          }
          /// <summary>
          /// Funcion que regresa una tabla generica con los datos obtenidos de l catalogo fijo
          /// </summary>
          /// <param name="psSelectCampos"></param>
          /// <param name="psFiltroEspecial"></param>
          /// <returns></returns>
          public TablaGenerica RegresaTablaGenerica(string psSelectCampos = null, string psFiltroEspecial = null)
          {
               TablaGenerica ldtDatos = null;
               StringBuilder lsQuery;
               try
               {
                    lsQuery = new StringBuilder();
                    if (string.IsNullOrEmpty(psSelectCampos))
                    {
                         psSelectCampos = _oCatalogo._sCamposLLave.ToString();
                    }
                    lsQuery.AppendFormat("Select {0} from [{1}].[{2}].[{3}{4}] where {5} = {6}",
                         psSelectCampos,
                         _oSistema.Servidor.Principal.BaseDatos.Catalogo,
                         _oCatalogo.Propietario,
                         _oSistema.Servidor.Principal.BaseDatos.Prefijo, _oCatalogo.NombreTabla,
                         _oCatalogo.CamposLlave.First().Key,
                         _oSistema.Sesion.Institucion.Numero);

                    if (!string.IsNullOrEmpty(psFiltroEspecial))
                    {
                         lsQuery.AppendFormat(" and {0}", psFiltroEspecial);
                    }
                    ldtDatos = _oSistema.Conexion.RegresaTablaGenerica(lsQuery, _oCatalogo.NombreTabla);
                    return ldtDatos;
               }
               finally
               {
               }
          }
          /// <summary>
          /// Regresas the coleccion en base a campo.
          /// </summary>
          /// <param name="psCampos">The ps campos.</param>
          /// <param name="psFiltroEspecial">The ps filtro especial.</param>
          /// <returns></returns>
          public HashSet<string> RegresaColeccionEnBaseACampo(string psCampos, StringBuilder psFiltroEspecial)
          {
               return RegresaColeccionEnBaseACampo(psCampos, psFiltroEspecial.ToString());
          }

          /// <summary>
          /// Regresas the coleccion en base a campo.
          /// </summary>
          /// <param name="psCampos">The ps campos.</param>
          /// <param name="psFiltroEspecial">The ps filtro especial.</param>
          /// <returns></returns>
          public HashSet<string> RegresaColeccionEnBaseACampo(string psCampos, string psFiltroEspecial)
          {
               DataTable ldtDatos = null;
               HashSet<string> loCombo;
               StringBuilder lsQuery;
               loCombo = new HashSet<string>();
               lsQuery = new StringBuilder();
               if (string.IsNullOrEmpty(psCampos))
               {
                    psCampos = _oCatalogo.CamposLlave.First().Key;
               }
               lsQuery.AppendFormat("Select {0} from [{1}].[{2}].[{3}{4}] where {5} = {6}",
                    psCampos,
                    _oSistema.Servidor.Principal.BaseDatos.Catalogo,
                    _oCatalogo.Propietario,
                    _oSistema.Servidor.Principal.BaseDatos.Prefijo, _oCatalogo.NombreTabla,
                    _oCatalogo.CamposLlave.First().Key,
                    _oSistema.Sesion.Institucion.Numero);
               if (!string.IsNullOrEmpty(psFiltroEspecial))
               {
                    lsQuery.AppendFormat(" and {0}", psFiltroEspecial);
               }
               ldtDatos = _oSistema.Conexion.RegresaDataTable(lsQuery, _oCatalogo.NombreTabla);
               foreach (DataRow loFila in ldtDatos.Rows)
               {
                    loCombo.Add(Convert.ToString(loFila[psCampos]));
               }
               return loCombo;
          }
          #endregion

          /// <summary>
          /// Armas the consulta para combo.
          /// </summary>
          /// <param name="psSelectCampos">The ps select campos.</param>
          /// <param name="psFiltroEspecial">The ps filtro especial.</param>
          /// <param name="pbFiltrarporCamposLlave">if set to <c>true</c> [pb filtrarpor campos llave].</param>
          /// <param name="psFiltroPrincipal">The ps filtro principal.</param>
          /// <param name="psOrden">The ps orden.</param>
          /// <returns></returns>
          private StringBuilder ArmaConsultaParaCombo(string psSelectCampos, string psFiltroEspecial, bool pbFiltrarporCamposLlave, string psFiltroPrincipal, string psOrden)
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat("Select {0} from [{1}].[{2}].[{3}{4}]",
                    psSelectCampos,
                    _oSistema.Servidor.Principal.BaseDatos.Catalogo,
                    _oCatalogo.Propietario,
                    _oSistema.Servidor.Principal.BaseDatos.Prefijo, _oCatalogo.NombreTabla);
               if (pbFiltrarporCamposLlave)
                    lsQuery.AppendFormat(" where {0} = {1}", _oCatalogo.CamposLlave.First().Key, _oSistema.Sesion.Institucion.Numero);
               else if (!string.IsNullOrEmpty(psFiltroPrincipal))
                    lsQuery.AppendFormat(" where {0}", psFiltroPrincipal);
               if (!string.IsNullOrEmpty(psFiltroEspecial))
                    lsQuery.AppendFormat(" and {0}", psFiltroEspecial);
               if (!string.IsNullOrEmpty(psOrden))
                    lsQuery.AppendFormat(" order by {0}", psOrden);
               return lsQuery;
          }


          #endregion
     }
}
