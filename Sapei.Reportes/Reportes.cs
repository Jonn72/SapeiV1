using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace Sapei.Reportes
{
    /// <summary>
    /// Clase para la generacion de reportes usando reporting services local
    /// </summary>
    public class Reporte : IDisposable
    {
        #region Variables

        /// <summary>
        /// Variable para genear el reporte usando repotviewer
        /// </summary>
        ReportViewer _oReporte;
        /// <summary>
        /// Variable para almacenar los parametros de entrada para el reporte
        /// </summary>
        private List<ReportParameter> _oaListaParametros;
        /// <summary>
        /// Varibale para almacenar la lista de origenes de datos para el reporte
        /// </summary>
        private List<ReportDataSource> _oaOrigenDatos;
        /// <summary>
        /// Variable para almancenar el tipo de reporte
        /// </summary>
        private string _sTipoFormato;
        /// <summary>
        /// Variable para almancenar el nombre del reporte 
        /// </summary>
        private string _sNombreArchivoSalida;
        /// <summary>
        /// Lista para almacenar el nombre de los parametros y sus valores para no agregar parametros repetidos
        /// </summary>
        private Dictionary<string, string> _oListaParametros;

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad para obetener el nombre del archivo de salida
        /// </summary>
        public string NombreArchivoSalida { get; private set; }

        /// <summary>
        /// Ruta de entrada para el reporte
        /// </summary>
        public string RutaEntrada { get; set; }

        /// <summary>
        /// Ruta de salida para el reporte
        /// </summary>
        public string RutaSalida { get; set; }

        /// <summary>
        /// Tipo de formato para el reporte
        /// </summary>
        public enmTipoFormato TipoFormato { get; set; }
        /// <summary>
        /// Gets or sets the nombre repore.
        /// </summary>
        /// <value>
        /// The nombre repore.
        /// </value>
        public string NombreRepore
        {
            get { return _sNombreArchivoSalida; }
            set { _sNombreArchivoSalida = value; }
        }
        #endregion

        #region Constructores

        /// <summary>
        /// Crea una nueva instancia para  generar un reporte en RS
        /// </summary>
        /// <param name="psRutaEntrada">Ruta de entrada para del reporte .rdlc</param>
        /// <param name="psNombreReporte">Nombre del reporte</param>
        public Reporte(string psRutaEntrada, string psNombreReporte)
        {
            ConstructorInterno(psRutaEntrada, psNombreReporte, "");
        }

        /// <summary>
        /// Crea una nueva instancia para generar el reporte en RS
        /// </summary>
        /// <param name="psRutaEntrada">Ruta de entrar para el reporte .rdlc</param>
        /// <param name="psNombreReporte">Nombre del reporte</param>
        /// <param name="psRutaSalida">Ruta de salida para guardar el archivo seleccioando</param>
        public Reporte(string psRutaEntrada, string psNombreReporte, string psRutaSalida)
        {
            ConstructorInterno(psRutaEntrada, psNombreReporte, psRutaSalida);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reporte"/> class.
        /// </summary>
        /// <param name="psRutaEntrada">The ps nombre reporte.</param>
        public Reporte(string psRutaEntrada)
        {
            ConstructorInterno(psRutaEntrada, "", "");
        }

         ~Reporte()
        { 
        
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Reporte"/> class.
        /// </summary>
        public Reporte()
          {
               ConstructorInterno("", "", "");
          }

        #endregion
        public void Dispose()
        {
                if (_oReporte != null)
                {

                    _oReporte = null;
                }

            
        }
        #region Metodos

        /// <summary>
        /// Funcion para iniciar los valores para el reporte
        /// </summary>
        /// <param name="psRutaEntrada">Ruta de entrar para el reporte .rdlc</param>
        /// <param name="psReporteNombre">Nombre del reporte</param>
        /// <param name="psRutaSalida">Ruta de salida para guardar el archivo seleccioando</param>
        private void ConstructorInterno(string psRutaEntrada, string psReporteNombre, string psRutaSalida)
          {
               _oReporte = new ReportViewer();
               _oaOrigenDatos = new List<ReportDataSource>();
               _oaListaParametros = new List<ReportParameter>();
               _oListaParametros = new Dictionary<string, string>();
               _sNombreArchivoSalida = psReporteNombre.Replace(" ", "");
               RutaEntrada = psRutaEntrada;
               RutaSalida = psRutaSalida;
               if (!String.IsNullOrEmpty(RutaEntrada))
                    _oReporte.LocalReport.ReportPath = RutaEntrada;
          }

          /// <summary>
          /// Agrega un parametro del reporte
          /// </summary>
          /// <param name="psNompreParametro">Nombre del parametro</param>
          /// <param name="psValorParametro">Valor del parametro</param>
          public void AgregaParametro(string psNompreParametro, string psValorParametro)
          {
               ReportParameter loParametro;
               if (_oListaParametros.ContainsKey(psNompreParametro.ToLower()))
                    return;
               _oListaParametros.Add(psNompreParametro.ToLower(), psValorParametro);
               loParametro = new ReportParameter(psNompreParametro, psValorParametro);
               if (!_oaListaParametros.Contains(loParametro))
                    _oaListaParametros.Add(loParametro);
          }

          /// <summary>
          /// Agrega un origen de datos a la lista para el reporte
          /// </summary>
          /// <param name="poOrigen"></param>
          private void AgregaOrigenMapeo(ReportDataSource poOrigen)
          {
               if (!_oaOrigenDatos.Contains(poOrigen))
                    _oaOrigenDatos.Add(poOrigen);
          }

          /// <summary>
          /// Agrega un origen de datos usando un dataset
          /// </summary>
          /// <param name="psNombre">Nombre del Origen </param>
          /// <param name="poDatos">DataSet con la informacion</param>
          public void AgregaOrigenMapeo(string psNombre, System.Data.DataSet poDatos)
          {
               AgregaOrigenMapeo(new ReportDataSource(psNombre, poDatos.Tables[0]));
          }

          /// <summary>
          /// Agrega un origen de datos usando un DataTable
          /// </summary>
          /// <param name="psNombre">Nombre del origen</param>
          /// <param name="poDatos">DataTable con la informacion</param>
          public void AgregaOrigenMapeo(string psNombre, DataTable poDatos)
          {
               AgregaOrigenMapeo(new ReportDataSource(psNombre, poDatos));
          }

          /// <summary>
          /// Agrega un origen de datos usando un dataview
          /// </summary>
          /// <param name="psNombre">Nombre del origen</param>
          /// <param name="poDatos">DataView con la informacion</param>
          public void AgregaOrigenMapeo(string psNombre, DataView poDatos)
          {
               AgregaOrigenMapeo(new ReportDataSource(psNombre, poDatos.ToTable()));
          }

          /// <summary>
          /// Agrega un origen de datas usando un ArrayLIst
          /// </summary>
          /// <param name="psNombre">Nombre del origen</param>
          /// <param name="poDatos">ArryaList con la informacion</param>
          public void AgregaOrigenMapeo(string psNombre, ArrayList poDatos)
          {
               AgregaOrigenMapeo(new ReportDataSource(psNombre, poDatos));
          }

          /// <summary>
          /// Agregas the defincion reporte.
          /// </summary>
          /// <param name="poDefincion">The po defincion.</param>
          public void AgregaDefincionReporte(Stream poDefincion)
          {
               _oReporte.LocalReport.LoadReportDefinition(poDefincion);
          }

          /// <summary>
          /// Agrega un origen de datos usando un lista de objetos IEnumerable (ejemplo List)
          /// </summary>
          /// <param name="psNombre">Nombre del origen</param>
          /// <param name="poDatos">IEnumerable con la informacion</param>
          public void AgregaOrigenMapeo(string psNombre, IEnumerable poDatos)
          {
               AgregaOrigenMapeo(new ReportDataSource(psNombre, poDatos));
          }

          /// <summary>
          /// Funcion para genera el nombre de salida para el archivo generado
          /// </summary>
          private string GeneraNombreAleatoriodelArchivodeSalida()
          {
               StringBuilder lsNombreArchivo;
               int liNumAleatorio;
               int liLongitud;
               Random loGeneraNumeroRandom;
               if (String.IsNullOrEmpty(_sNombreArchivoSalida))
                    return "";
               lsNombreArchivo = new StringBuilder();
               loGeneraNumeroRandom = new Random();
               liNumAleatorio = loGeneraNumeroRandom.Next(1000, 9999);
               liLongitud = _sNombreArchivoSalida.LastIndexOf(".");
               if (liLongitud == -1)
                    liLongitud = _sNombreArchivoSalida.Length - 1;
               lsNombreArchivo.AppendFormat("{0}{1}{2}.", _sNombreArchivoSalida.Substring(0, liLongitud),
                    (DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()),
                    liNumAleatorio);
               //Selecionamos el tipo de extencion para el archivo
               switch (TipoFormato)
               {
                    case enmTipoFormato.PDF:
                         lsNombreArchivo.Append(enmTipoFormato.PDF.ToString());
                         break;
                    case enmTipoFormato.XLSX:
                         lsNombreArchivo.Append(enmTipoFormato.XLSX.ToString());
                         break;
                    case enmTipoFormato.XML:
                         lsNombreArchivo.Append(enmTipoFormato.XML.ToString());
                         break;
                    case enmTipoFormato.CSV:
                         lsNombreArchivo.Append(enmTipoFormato.CSV.ToString());
                         break;
                    case enmTipoFormato.DOCX:
                         lsNombreArchivo.Append(enmTipoFormato.DOCX.ToString());
                         break;
                    case enmTipoFormato.HTML:
                         lsNombreArchivo.Append(enmTipoFormato.HTML.ToString());
                         break;
                    default:
                         lsNombreArchivo.Append(enmTipoFormato.PDF.ToString());
                         break;
               }
               return lsNombreArchivo.ToString();
          }

          /// <summary>
          /// Funcion que agrega todos los origenes de datos al informe
          /// </summary>
          private void AgregaOrigenDatosAlReporte()
          {
               _oReporte.LocalReport.DataSources.Clear();
               foreach (ReportDataSource loOrigen in _oaOrigenDatos)
               {
                    //Agregamos al reporte cada origen de datos
                    _oReporte.LocalReport.DataSources.Add(loOrigen);
               }
          }

          /// <summary>
          /// FUncion que añade todos los parametros al reporte
          /// </summary>
          private void AgregaParametroAlReporte()
          {
               //Se pone de esta manera ya que reportviewer no valida que los parametros existan antes de crear el reporte.
               //Este paso previo asegura que el reporte se pueda generar sin mandar un error por que falte algun parametro.
               //La unica condicion es que los parametros se configuren como String en el reporte rdlc
               //Validar la informacion de los parametros, para que se agregen todos los parametros necesarios para el reporte                              
               foreach (ReportParameterInfo loParametro in _oReporte.LocalReport.GetParameters())
               {
                    //Recuperar toda los parametros del reporte
                    if (loParametro.Values.GetType() == Type.GetType("System.DateTime"))
                         AgregaParametro(loParametro.Name, DateTime.Now.ToShortDateString());
                    else
                         AgregaParametro(loParametro.Name, "Sin Valor");
               }
               _oReporte.LocalReport.SetParameters(_oaListaParametros);
          }

          /// <summary>
          /// Configura el reporte
          /// </summary>
          private void ConfiguraReporte()
          {
               //Aqui se agregan datos extra que se queiran configurar al reporte
               _oReporte.LocalReport.EnableExternalImages = true;
               _oReporte.LocalReport.EnableHyperlinks = true;
               _oReporte.LocalReport.ReportPath = RutaEntrada;
          }

          /// <summary>
          /// Configura el tipo de formato para generar el reporte
          /// </summary>
          public void ConfiguraTipoFormato()
          {
               switch (TipoFormato)
               {
                    case enmTipoFormato.PDF:
                         _sTipoFormato = "PDF";
                         break;
                    case enmTipoFormato.XLSX:
                         _sTipoFormato = "Excel";
                         break;
                    case enmTipoFormato.XML:
                         _sTipoFormato = "XML";
                         break;
                    case enmTipoFormato.CSV:
                         _sTipoFormato = "CSV";
                         break;
                    case enmTipoFormato.DOCX:
                         _sTipoFormato = "WORD";
                         break;
                    case enmTipoFormato.HTML:
                         _sTipoFormato = "HTML";
                         break;
                    default:
                         _sTipoFormato = "PDF";
                         break;
               }
          }

          /// <summary>
          /// Funcion que exporta o genera el archivo en un arreglo de datos binarios
          /// </summary>
          /// <returns></returns>
          public byte[] ExportaReporte()
          {
               //Configura los datos iniciales para el reporte
               ConfiguraReporte();
               //Agrega el origen de datos del reporte
               AgregaOrigenDatosAlReporte();
               //Agrega los parametros para el reporte
               AgregaParametroAlReporte();
               //Configura el tipo de formato para el reporte
               ConfiguraTipoFormato();
               //Genera el nombre de salida para el reporte
               NombreArchivoSalida = GeneraNombreAleatoriodelArchivodeSalida();
               //Genera el archivo de salida binario
               return _oReporte.LocalReport.Render(_sTipoFormato);
          }
          /// <summary>
          /// Exportas the reporte.
          /// </summary>
          /// <param name="penTipoFormato">The pen tipo formato.</param>
          /// <returns></returns>
          public byte[] ExportaReporte(enmTipoFormato penTipoFormato)
          {
               TipoFormato = penTipoFormato;
               return ExportaReporte();
          }

          #endregion
     }
}