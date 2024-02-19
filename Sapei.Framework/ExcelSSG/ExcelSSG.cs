using System;
using System.Collections.Generic;
using System.Linq;
using Sapei.Framework.BaseDatos;
using System.Data;

namespace Sapei.Framework.ExcelSSG
{
     /// <summary>
     /// 
     /// </summary>
     public class ExcelSSG : IDisposable 
     {
          #region Variables
          
          /// <summary>
          /// 
          /// </summary>
          protected int _iFilaEnc;
          /// <summary>
          /// 
          /// </summary>
          private Sapei.Framework.Sistema Sistema;
          /// <summary>
          /// 
          /// </summary>
          private SpreadsheetGear.IWorksheet WorkSheet;
          // Indica si ya se llamo al método Dispose. (default = false)
          /// <summary>
          /// 
          /// </summary>
          private bool disposed;
          
          #endregion 
          
          #region Propiedades
          
          /// <summary>
          /// 
          /// </summary>
          public string TituloReporte { get; set; }
          
          /// <summary>
          /// 
          /// </summary>
          public SpreadsheetGear.IWorkbook WorkBook { get; private set; }
          
          /// <summary>
          /// 
          /// </summary>
          public bool ArchivoExcelSinMascara { set; private get; }
          
          /// <summary>
          /// 
          /// </summary>
          public TablaGenerica Registros { set; private get; }
          
          /// <summary>
          /// 
          /// </summary>
          public string NombreReporteTraduccion { get; set; }
          
          /// <summary>
          /// 
          /// </summary>
          public string NombreReporte { set; private get; }
          
          /// <summary>
          /// 
          /// </summary>
          public string RutaTmp { set; private get; }
          
          /// <summary>
          /// 
          /// </summary>
          public bool UltimaFilaTot { set; private get; }
          
          /// <summary>
          /// 
          /// </summary>
          public string RFCEmpresa { get; set; }
          
          /// <summary>
          /// 
          /// </summary>
          public string DireccionEmpresa { get; set; }
          
          /// <summary>
          /// 
          /// </summary>
          public DateTime FechaAplicacionPeriodo { get; set; }
          
          /// <summary>
          /// Gets or sets the nombre reporte salida.
          /// </summary>
          /// <value>
          /// The nombre reporte salida.
          /// </value>
          public string NombreReporteSalida { get; set; }
          
          /// <summary>
          /// Gets or sets the binario reporte salida.
          /// </summary>
          /// <value>
          /// The binario reporte salida.
          /// </value>
          public byte[] BinarioReporteSalida { get; set; }
          
          /// <summary>
          /// Gets or sets the <see cref="System.Object"/> with the specified pi fila.
          /// </summary>
          /// <value>
          /// The <see cref="System.Object"/>.
          /// </value>
          /// <param name="piFila">The pi fila.</param>
          /// <param name="piColumna">The pi columna.</param>
          /// <returns></returns>
          public object this[int piFila, int piColumna]
          {
               get
               {
                    return this.WorkSheet.Cells[piFila - 1, piColumna - 1].Value;
               }
               set
               {
                    this.WorkSheet.Cells[piFila - 1, piColumna - 1].Value = value;
               }
          }
          
          #endregion
          
          #region Constructores
          
          /// <summary>
          /// 
          /// </summary>  
          public ExcelSSG() 
          {
               this.ConstructorInterno();
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="psRutaArchivo"></param>
          public ExcelSSG(string psRutaArchivo) 
          {
               this.WorkBook = SpreadsheetGear.Factory.GetWorkbook(psRutaArchivo);
               // Get the first worksheet.
               this.WorkSheet = this.WorkBook.Worksheets[0];
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="poSistema"></param>
          /// <param name="piFilaEnc"></param>
          public ExcelSSG(Sapei.Framework.Sistema poSistema, int piFilaEnc)//, ref clsSICTraduccion poTraduccion )
          {
               this.Sistema = poSistema;
               
               this._iFilaEnc = piFilaEnc + 1;
               this.UltimaFilaTot = false;
               this.ConstructorInterno();
          }
          
          private void ConstructorInterno()
          {
               this.WorkBook = SpreadsheetGear.Factory.GetWorkbook();
               // Get the first worksheet.
               this.WorkSheet = this.WorkBook.Worksheets[0];
          }
          
          #endregion
          
          #region Funciones
          
          
          /// <summary>
          /// 24/Abril/2014
          /// Gabriel
          /// </summary>
          /// <param name="piNumero">Se detecto que cuando se le pasaba como argumento un numero mayor a 256 se desbordaba por el tipo de dato
          /// que era de tipo Byte. Asi que se cambio el tipo del argumento por un Integer.</param>
          /// <returns></returns>
          /// <remarks></remarks>
          private string ConvierteNumeroAColumnaDeExcel(int piNumero) 
          {
               string lsDigito1;
               string lsDigito2;     
               lsDigito1 = "";
               lsDigito2 = "";
               //Valores Ascii A=65 Z=90
               if (piNumero % 26 == 0)
               {
                    if (Convert.ToInt32(piNumero / 26) - 1 > 0)
                         lsDigito1 = this.ConvierteNumeroAColumnaDeExcel((int)(piNumero / 26) - 1);
                    else
                         lsDigito1 = "";
                    lsDigito2 = "Z";
               }
               else
               {
                    if (Convert.ToInt32(piNumero / 26) > 0)                    
                         lsDigito1 = Convert.ToString((char)((int)(piNumero / 26) + 64));                    
                    lsDigito2 = Convert.ToString((char)((piNumero - ((int)(piNumero / 26) * 26)) + 64));
               }
               return string.Format("{0}{1}", lsDigito1, lsDigito2);
          }
          
          /// <summary>
          /// 
          /// </summary>
          public void AutoFit()
          {
               this.WorkSheet.Cells.EntireColumn.AutoFit();
          }
          
          /// <summary>
          /// 
          /// </summary>
          public virtual void EscribeReporteSpreadSinParametros()
          {
               int liCol;
               string lsColNombre;
               int liColumna;
               int lifila;
               int liTemp;
               
               lsColNombre = "";
               this._iFilaEnc = 6;
               liTemp = 6;
               lifila = 0;
               this.WorkSheet.Cells[1, 1].Value = "REPORTE PREVIO DE ARTICULOS A FACTURAR";
               this.WorkSheet.Cells[1, 1].Font.Bold = true;
               this.WorkSheet.Cells[2, 5].Value = System.String.Format(DateTime.Now.Date.ToString(), "dd/MM/yyyy");
               for (liColumna = 1; liColumna <= this.Registros.TotalColumnas; liColumna++)               
                    this.WorkSheet.Cells[1, liColumna - 1].Interior.ColorIndex = 48;               
               for (liColumna = 1; liColumna <= this.Registros.TotalColumnas; liColumna++)               
                    this.WorkSheet.Cells[2, liColumna - 1].Interior.ColorIndex = 14;               
               this.WorkSheet.Cells[1, 1].Font.ColorIndex = 2;
               this.WorkSheet.Range["E"].NumberFormat = "$#,D0.00";
               //SpreadExcel.Columns("E"].NumberFormat = "$#,##0.00"
               for (liCol = 0; liCol < this.Registros.TotalColumnas; liCol++)
               {
                    //para utilizar las mismas columnas del grid (liberando espacio en la tabla xtstextos)                    
                    if (lsColNombre != "")                    
                         this.WorkSheet.Cells[this._iFilaEnc - 2, liCol].Value = lsColNombre;                    
                    else
                         this.WorkSheet.Cells[this._iFilaEnc - 2, liCol].Value = this.Registros.NombreColumnas.ElementAt(liCol);                    
                    //Le pone negritas
                    this.WorkSheet.Cells[this._iFilaEnc - 2, liCol].Font.Bold = true;
                    this.WorkSheet.Cells[this._iFilaEnc - 2, liCol].Interior.ColorIndex = 48;
                    this.WorkSheet.Cells[this._iFilaEnc - 2, liCol].Font.ColorIndex = 1;
               }
               
               this.WorkSheet.Range[this._iFilaEnc - 1, 0, this._iFilaEnc + this.Registros.TotalFilas - 2, this.Registros.TotalColumnas - 1].Value = this.RegresaConvierteTablaaArreglo();               
               liColumna = 6;
               while (lifila != this.Registros.TotalFilas)
               {
                    this.WorkSheet.Cells[liColumna - 1, this._iFilaEnc - 1].Formula = string.Format("=E{0}*D{1}", liTemp, liTemp);
                    lifila = lifila + 1;
                    liColumna = liColumna + 1;
                    liTemp = liTemp + 1;
               }
               this.WorkSheet.Cells.Select();
               this.EscribeTotales();
               this.WorkSheet.Cells[this._iFilaEnc + this.Registros.TotalFilas - 1, 5].Formula = string.Format("=SUM((F6:F{0}))* (.15) + SUM((F6:F{1}))", Convert.ToString((this.Registros.TotalFilas + this._iFilaEnc) - 1), Convert.ToString((this.Registros.TotalFilas + this._iFilaEnc) - 1));
               this.WorkBook.ActiveSheet.Name = this.TituloReporte.Substring(1, 30);
               //Pone esta celda Activa
               //WorkSheet.Cells.EntireColumn.AutoFit()
               //25/Junio/2013
               //Karina,
               //SpreadSheetGear da mejores resultados usando Cells["rango"].Columns.Autofit que
               //Cells.EntireColumn.AutoFit() como se usa en owc
               this.WorkSheet.Cells[string.Format("A:{0}", this.ConvierteNumeroAColumnaDeExcel(Registros.TotalColumnas))].Columns.AutoFit();
               this.WorkSheet.Range["A1"].Select();
          }
          
          /// <summary>
          /// 
          /// </summary>
          private void EscribeTotales() 
          {
               int liFilaTotales;
               int liCol;
               int liFila;
               double ldoTotales;
               bool lbSumTotales;
               string lsEncEtiquetaT; 
               lsEncEtiquetaT = "";
               lbSumTotales = true;
               ldoTotales = 0;
               liFilaTotales = this.Registros.TotalFilas + this._iFilaEnc;
               for (liCol = 2; liCol < this.Registros.TotalColumnas; liCol++)
               {
                    //DataTable.Columns[liCol].DataType Al usar un datatable de esta manera.
                    if (!(this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.DateTime")))
                         if (this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.string") || this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.decimal"))
                         {
                              for (liFila = 0; liFila < this.Registros.TotalFilas; liFila++)                              
                                   if (this.Registros[liFila, liCol].ToString() != "")                                   
                                        if (Sapei.Framework.Utilerias.NumeroLetras.EsNumerico(this.Registros[liFila, liCol].ToString()))
                                        {
                                             ldoTotales += Convert.ToDouble(this.Registros[liFila, liCol]);
                                             lbSumTotales = true;
                                        }
                                        else
                                        {
                                             lbSumTotales = false;
                                             break;
                                        }
                              if (lbSumTotales)                         
                                   this.WorkSheet.Cells[liFilaTotales - 1, liCol].Value = ldoTotales;                         
                         }
                    ldoTotales = 0;
               }
               //Mete el color de fondo azul al pie del detalle
               //06/06/07, traduciendo
               //if( Traduccion != null ){
               //     lsEncEtiquetaT = Traduccion.Traduce(clsSICTraduccion.enTipoTexto.REPORTE, this.NombreReporteTraduccion, "xlsTotTotalGeneral");
               //}
               if (lsEncEtiquetaT != "")
                    this.WorkSheet.Cells[liFilaTotales - 1, 1].Value = string.Format("{0} ({1})", lsEncEtiquetaT, this.Registros.TotalFilas);
               else
                    this.WorkSheet.Cells[liFilaTotales - 1, 1].Value = string.Format("TOTAL GENERAL ({0})", this.Registros.TotalFilas);                           
               this.WorkSheet.Range[liFilaTotales - 1, 0, liFilaTotales - 1, this.Registros.TotalColumnas - 1].Select();
               this.WorkSheet.Cells[liFilaTotales - 1, 1].Font.Bold = true;
               this.WorkSheet.Range[liFilaTotales - 1, 0, liFilaTotales - 1, this.Registros.TotalColumnas - 1].Interior.ColorIndex = 48;
               this.WorkSheet.Range[liFilaTotales - 1, 0, liFilaTotales - 1, this.Registros.TotalColumnas - 1].Font.ColorIndex = 1;
               this.WorkSheet.Range[0, 0, 0, 0].Select(); 
               //WorkBook.Selection.Interior.ColorIndex = 49
               //SpreadExcel.Selection.Font.ColorIndex = 2
               //SpreadExcel.Range[SpreadExcel.Cells[1, 1), SpreadExcel.Cells[1, 1)].Select()
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          private double EscribeTotal() 
          {
               int liFilaTotales;
               int liCol;
               int liFila;
               double ldoTotales;
               bool lbSumTotales;
               //string lsEncEtiquetaT; 
               //lsEncEtiquetaT = "";
               
               lbSumTotales = true;
               liFilaTotales = this.Registros.TotalFilas + this._iFilaEnc;
               ldoTotales = 0;
               for (liCol = 2; liCol < this.Registros.TotalColumnas; liCol++)
               {
                    if (!(this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.DateTime")))
                         if (this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.string") || this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.decimal"))
                         {
                              for (liFila = 0; liFila < this.Registros.TotalFilas; liFila++)                              
                                   if (this.Registros[liFila, liCol].ToString() != "")
                                        if (Sapei.Framework.Utilerias.NumeroLetras.EsNumerico(this.Registros[liFila, liCol].ToString()))
                                        {
                                             ldoTotales += Convert.ToDouble(this.Registros[liFila, liCol]);
                                             lbSumTotales = true;
                                        }
                                        else
                                        {
                                             lbSumTotales = false;
                                             break;
                                        }
                              if (lbSumTotales)                              
                                   return ldoTotales;
                         }
                    ldoTotales = 0;
               }
               return ldoTotales;
          }
          

          
          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public string ExportaReporte() 
          {
               string lsNombreReporte;
               string lsNombreReporteFisico;
               int liNumAleatorioReporte;
               Random loGeneraNumeroRandom; 
               try
               {
                    loGeneraNumeroRandom = new Random(); 
                    liNumAleatorioReporte = loGeneraNumeroRandom.Next(1000, 9999);
                    lsNombreReporte = string.Format("{0}_{1}{2}.xls", this.TituloReporte.Replace(" ", ""), System.String.Format(DateTime.Now.ToString(), "yyyyMMddhhmmss"), liNumAleatorioReporte);
                    lsNombreReporteFisico = string.Format("{0}{1}", this.RutaTmp, lsNombreReporte);
                    this.WorkSheet.SaveAs(lsNombreReporteFisico, SpreadsheetGear.FileFormat.Excel8);
                    //WorkSheet.Export(lsNombreReporteFisico, SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportAsAppropriate)
                    return lsNombreReporte;
               }
               finally
               {
                    loGeneraNumeroRandom = null;
               }
          }
          
          /// <summary>
          /// Exportas the reporte memoria.
          /// </summary>
          public void ExportaReporteMemoria()
          { 
               int liNumAleatorioReporte;
               Random loGeneraNumeroRandom;               
               loGeneraNumeroRandom = new Random();
               liNumAleatorioReporte = loGeneraNumeroRandom.Next(1000, 9999);
               this.NombreReporteSalida = string.Format("{0}_{1}{2}.xls", this.TituloReporte.Replace(" ", ""), DateTime.Now.ToString("yyyyMMddhhmmss"), liNumAleatorioReporte);                    
               this.BinarioReporteSalida = this.WorkSheet.SaveToMemory(SpreadsheetGear.FileFormat.Excel8);                                
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="psRuta"></param>
          public void GuardaExcelComo(string psRuta) 
          {
               this.WorkSheet.SaveAs(psRuta, SpreadsheetGear.FileFormat.Excel8);
          }
          

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          private object RegresaConvierteTablaaArreglo() 
          {
               object[,] laoDatos;
               int liFila;
               int liCol;
               string lsValor;      
               laoDatos = new object[this.Registros.TotalFilas - 1, this.Registros.TotalColumnas - 1];      
               for (liFila = 0; liFila < this.Registros.TotalFilas; liFila++)
               {
                    for (liCol = 0; liCol < this.Registros.TotalColumnas; liCol++)
                    {
                         //Esto se lo puse, para exportar fechas a excel tipo Ole Date
                         //Para ello es importante que si se diseña un reporte de excel
                         //con una fecha, no se use la función FECHA()
                         //ya que en un servidor configurado en MM/dd/yyyy causaría problemas
                         if (this.Registros[liFila, liCol].ToString() != "")
                         {
                              if (this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.string"))
                              {
                                   //Verifico si una cadena contiene una fecha preguntando por las posiciones de las diagonales
                                   if (this.Registros[liFila, liCol].ToString().IndexOf("/") == 2 & this.Registros[liFila, liCol].ToString().LastIndexOf("/") == 5 & this.Registros[liFila, liCol].ToString().Length == 10)
                                   {
                                        lsValor = this.Registros[liFila, liCol].ToString();
                                        //laoDatos(liFila, liCol) = DateSerial(Mid(lsValor, 7, 4), Mid(lsValor, 4, 2), Mid(lsValor, 1, 2)].ToOADate
                                        laoDatos[liFila, liCol] = System.String.Format(new DateTime(Int32.Parse(lsValor.Substring(7, 4)),Int32.Parse(lsValor.Substring(4, 2)), Int32.Parse(lsValor.Substring(1, 2))).ToString(), "yyyy-MM-dd");
                                   }
                                   else if (Sapei.Framework.Utilerias.NumeroLetras.EsNumerico(this.Registros[liFila, liCol].ToString().Replace(" ", "")))                                   
                                        laoDatos[liFila, liCol] = this.Registros[liFila, liCol].ToString().Trim().Replace(" ", "");
                                   else 
                                        //Se reviso con Ivan, Jorge, Pao y Victor
                                        //y se acordo que no se debe limpiar el texto en esta clase
                                        //si necesitan limpiarlo, haganlo sobre el  antes de pasarselo
                                        laoDatos[liFila, liCol] = this.Registros[liFila, liCol].ToString();                                                                           
                              }
                              else if (this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.decimal")) if (this.ArchivoExcelSinMascara == true)                                   
                                   laoDatos[liFila, liCol] = System.Math.Round(Convert.ToDouble(this.Registros[liFila, liCol]), 2);
                              else 
                                   laoDatos[liFila, liCol] = string.Format("'{0}", this.Registros[liFila, liCol].ToString());                                                                 
                              else if (this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.DateTime") & this.ArchivoExcelSinMascara == true)                                                                 
                                   laoDatos[liFila, liCol] = System.String.Format(this.Registros[liFila, liCol].ToString(), "yyyy-MM-dd");                              
                              else 
                                   laoDatos[liFila, liCol] = this.Registros[liFila, liCol].ToString();                              
                         }
                         else if (this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.DateTime"))                         
                              if (!(DBNull.Value == this.Registros[liFila, liCol]))                                   
                                   laoDatos[liFila, liCol] = Convert.ToDateTime(this.Registros[liFila, liCol]).ToOADate();                                        
                              
                              else if (this.Registros.TipoDatoColumna(liCol) == Type.GetType("System.decimal")) if (this.ArchivoExcelSinMascara == true)                                   
                                   laoDatos[liFila, liCol] = this.Registros[liFila, liCol].ToString();                                   
                              else 
                                   laoDatos[liFila, liCol] = string.Format("'{0}", this.Registros[liFila, liCol].ToString());                                                            
                              else 
                                   laoDatos[liFila, liCol] = this.Registros[liFila, liCol].ToString();                                                             
                    }
               }
               return laoDatos;
          }
          

          /// <summary>
          /// 
          /// </summary>
          /// <param name="psCliente"></param>
          /// <param name="psSucursal"></param>
          public void EscribeReporteSpreadSinEncabezado(string psCliente, string psSucursal) 
          {
               int liindex;
               int liConsecutivo;               
               int lsFila;
               double ldoTotal;                
               this._iFilaEnc = 2;
               lsFila = 1;
               liConsecutivo = 0;  
               for (liindex = 0; liindex < this.Registros.TotalFilas; liindex++)
               {
                    lsFila = lsFila + 1;
                    liConsecutivo = liConsecutivo + 1;
                    this.WorkSheet.Cells[lsFila - 1, 0].Value = psCliente;
                    this.WorkSheet.Cells[lsFila - 1, 1].Value = this.Registros[liindex, "RPTCOL1"].ToString();
                    this.WorkSheet.Cells[lsFila - 1, 2].Value = this.Registros[liindex, "RPTCOL2"];
                    this.WorkSheet.Cells[lsFila - 1, 3].Value = this.Registros[liindex, "RPTCOL3"];      
                    this.WorkSheet.Cells[lsFila - 1, 4].Value = string.Format("||'{0}", this.Registros[liindex, "RPTCOL4"]);
                    this.WorkSheet.Cells[lsFila - 1, 5].Value = liConsecutivo;               
               }
               ldoTotal = this.EscribeTotal();
               this.WorkSheet.Cells[this.Registros.TotalFilas + 1, 2].Value = ldoTotal;                  
               this.WorkSheet.Cells[0, 0].Value = "A";
               this.WorkSheet.Cells[0, 1].Value = "1";
               this.WorkSheet.Cells[0, 2].Value = "2";
               this.WorkSheet.Cells[0, 3].Value = psCliente.ToString().Substring(0, 5);
               this.WorkSheet.Cells[0, 4].Value = psCliente.ToString().Substring(7, 1);
               this.WorkSheet.Cells[0, 5].Value = psSucursal;
               this.WorkSheet.Cells[0, 6].Value = ldoTotal;
               this.WorkSheet.Cells[0, 7].Value = liConsecutivo;
               this.WorkSheet.Cells[0, 8].Value = System.String.Format(DateTime.Now.ToString(), "yyyyMMdd");
               this.WorkSheet.Cells.Select();                                             
               this.WorkSheet.Cells[string.Format("A:{0}", this.ConvierteNumeroAColumnaDeExcel(Registros.TotalFilas))].Columns.AutoFit();                 
               this.WorkSheet.Name = this.TituloReporte.Substring(1, 30);
               //Pone esta celda Activa
               this.WorkSheet.Range["A1"].Select();
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="psTipoNomina"></param>
          /// <param name="psDescripcionPeriodo"></param>
          public void EscribeTitulosNomina(string psTipoNomina, string psDescripcionPeriodo) 
          {
               string lsProceso;
               string lsPeriodo;         
               lsProceso = "";
               lsPeriodo = "";
               //Agrega el Encabezado de nomina especial para reportes de nomina
               this.WorkSheet.Cells[1, 0].Value = this.WorkSheet.Cells[2, 1].Value;
               //18/06/07, Helaman, mismo cambio de clsExcel, para corrgir error de ...referencia a objeto....
               //if( Traduccion != null )
               //lsProceso = Traduccion.Traduce(clsSICTraduccion.enTipoTexto.REPORTE, this.NombreReporteTraduccion, "xlsEncProcesoNomina");
               if (lsProceso != "")               
                    this.WorkSheet.Cells[8, 0].Value = string.Format("{0}{1}", lsProceso, psTipoNomina);
               else
                    this.WorkSheet.Cells[8, 0].Value = string.Format("Tipo de Nómina: {0}", psTipoNomina);        
               this.WorkSheet.Cells[8, 0].Font.Bold = true;
               //if( Traduccion != null )
               //    lsPeriodo = Traduccion.Traduce(clsSICTraduccion.enTipoTexto.REPORTE, this.NombreReporteTraduccion, "xlsEncPeriodoNomina");           
               //08/12/2009 Si no hay descripcion que no muestre nada
               if (lsPeriodo != "" & psDescripcionPeriodo != "")
                    
                    this.WorkSheet.Cells[9, 0].Value = string.Format("{0}{1}", lsPeriodo, psDescripcionPeriodo);
               else if (lsPeriodo == "" & psDescripcionPeriodo != "")
                    //cesar 24/08/2011, Agrega esta condicion para que cuando mande imprimir en excel por meses para
                    //no imprima en la hoja de excel nada en periodo
                    this.WorkSheet.Cells[9, 0].Value = string.Format("Periodo: {0}", psDescripcionPeriodo);
               
               this.WorkSheet.Cells[9, 0].Font.Bold = true;
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="pasNombresColumna"></param>
          public void NombrarColumnas(String[] pasNombresColumna) 
          {
               int liColumna;
               for (liColumna = 0; liColumna <= pasNombresColumna.Length; liColumna++)
               //Pone el nombre de la columna
                    this.WorkSheet.Cells[11, liColumna].Value = pasNombresColumna[liColumna];                    
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="pasMascarasColumna"></param>
          public void DarFormatoColumnas(String[] pasMascarasColumna) 
          {
               int liColumna;
               string lsMascara; 
               for (liColumna = 0; liColumna < pasMascarasColumna.Length; liColumna++)
               {
                    //Primero revisa que la mascara no venga vacia
                    if (!(DBNull.Value.ToString() == pasMascarasColumna[liColumna]))                    
                         if (!(Object.ReferenceEquals(pasMascarasColumna[liColumna], null)))                         
                              if (pasMascarasColumna[liColumna] != "")
                              {
                                   if (pasMascarasColumna[liColumna].IndexOf("9") > 0 || pasMascarasColumna[liColumna].IndexOf("0") > 0)                                   
                                        lsMascara = pasMascarasColumna[liColumna].Replace("9", "#");                                   
                                   else if (pasMascarasColumna[liColumna].ToUpper().IndexOf("X") > 0)                                   
                                        //lsMascara = Replace(UCase(Col(liColumna, 2)), "X", "@")
                                        lsMascara = "@";
                                   else 
                                        lsMascara = pasMascarasColumna[liColumna];                        
                                   //loSpreadSheet.Range[loSpreadSheet.Cells[1, liColumna + 1), loSpreadSheet.Cells[1, liColumna + 1)].Select()
                                   this.WorkSheet.Range[0, liColumna, 0, liColumna].Select();
                                   this.WorkSheet.Range[0, liColumna, 0, liColumna].EntireColumn.NumberFormat = lsMascara;
                              }
               }
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="liNumeroRegistros"></param>
          /// <param name="liNumeroColumnas"></param>
          public void DaFormatoATotal(int liNumeroRegistros, int liNumeroColumnas) 
          {
               this.WorkSheet.Range[liNumeroRegistros + 11, 0, liNumeroRegistros + 11, liNumeroColumnas - 1].Select();
               this.WorkSheet.Range[liNumeroRegistros + 11, 0, liNumeroRegistros + 11, liNumeroColumnas - 1].Font.Bold = true;
               this.WorkSheet.Range[liNumeroRegistros + 11, 0, liNumeroRegistros + 11, liNumeroColumnas - 1].Interior.ColorIndex = 48;
               this.WorkSheet.Range[liNumeroRegistros + 11, 0, liNumeroRegistros + 11, liNumeroColumnas - 1].Font.ColorIndex = 1;
               this.WorkSheet.Range[0, 0, 0, 0].Select();
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFila"></param>
          /// <param name="piColumna"></param>
          /// <param name="poValor"></param>
          public void AsignaValorCelda(int piFila, int piColumna, object poValor) 
          {
               this.WorkSheet.Cells[piFila - 1, piColumna - 1].Value = poValor;
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFila"></param>
          /// <param name="piColumna"></param>
          /// <param name="poValor"></param>
          public void AsignaAnchoColumna(int piFila, int piColumna, object poValor) 
          {
               this.WorkSheet.Cells[piFila - 1, piColumna - 1].ColumnWidth = Convert.ToDouble(poValor);
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFila"></param>
          public void InsertarFilaDebajo(int piFila) 
          {
               this.WorkSheet.Cells[string.Format("{0}:{1}", Convert.ToString(piFila - 1), Convert.ToString(piFila - 1))].Insert(SpreadsheetGear.InsertShiftDirection.Down);
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFila"></param>
          /// <param name="piColumna"></param>
          /// <param name="poValor"></param>
          public void ConcatenaValorCelda(int piFila, int piColumna, String poValor) 
          {
               this.WorkSheet.Cells[piFila - 1, piColumna - 1].Value = string.Format("{0}{1}", Convert.ToString(this.WorkSheet.Cells[piFila - 1, piColumna - 1].Value), Convert.ToString(poValor));
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFila"></param>
          /// <param name="piColumna"></param>
          /// <param name="poValor"></param>
          public void ConcatenaValorCelda(int piFila, int piColumna, Double poValor) 
          {
               this.WorkSheet.Cells[piFila - 1, piColumna - 1].Value = Convert.ToDouble(this.WorkSheet.Cells[piFila - 1, piColumna - 1].Value) + Convert.ToDouble(poValor);
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFila"></param>
          /// <param name="piColumna"></param>
          /// <param name="Pbold"></param>
          public void CeldaBold(int piFila, int piColumna, bool Pbold) 
          {
               this.WorkSheet.Cells[piFila - 1, piColumna - 1].Font.Bold = Pbold;
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFila"></param>
          /// <param name="piColumna"></param>
          /// <param name="Pstring"></param>
          public void CeldaFormatoNumero(int piFila, int piColumna, string Pstring) 
          {
               this.WorkSheet.Cells[piFila - 1, piColumna - 1].NumberFormat = Pstring;
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFila"></param>
          /// <param name="piColumna"></param>
          public void CeldaAlineacionCentro(int piFila, int piColumna) 
          {
               this.WorkSheet.Cells[piFila - 1, piColumna - 1].HorizontalAlignment = SpreadsheetGear.HAlign.Center;
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="psNombreArchivo"></param>
          public void CopiaArchivo(string psNombreArchivo) 
          {
               SpreadsheetGear.IWorkbook loFuenteBook;
               SpreadsheetGear.IRange loFuenteRango;
               SpreadsheetGear.IRange loRangoDestino;
               loFuenteBook = SpreadsheetGear.Factory.GetWorkbook(psNombreArchivo, System.Globalization.CultureInfo.CurrentCulture);
               loFuenteRango = loFuenteBook.Worksheets[0].Range;
               loRangoDestino = this.WorkSheet.Cells[loFuenteRango.Address];
               loFuenteRango.Copy(loRangoDestino, SpreadsheetGear.PasteType.All, SpreadsheetGear.PasteOperation.None, true, false);
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="psValorOrignal"></param>
          /// <param name="psNuevoValor"></param>
          /// <returns></returns>
          public int Remplazar(string psValorOrignal, string psNuevoValor) 
          {
               return this.WorkSheet.Cells.Replace(psValorOrignal, psNuevoValor, SpreadsheetGear.LookAt.Whole, SpreadsheetGear.SearchOrder.ByRows, false);
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFilaInicial"></param>
          /// <param name="piColumnaInicial"></param>
          /// <param name="piFilaFinal"></param>
          /// <param name="piColumnaFinal"></param>
          /// <param name="piColorCelda"></param>
          /// <param name="piColorFuente"></param>
          /// <param name="Pbold"></param>
          /// <param name="Pb_Merge"></param>
          /// <param name="Pb_UnMerge"></param>
          /// <param name="psFormatoNumerico"></param>
          public void EstableceFormatoRango(int piFilaInicial, int piColumnaInicial, int piFilaFinal, int piColumnaFinal, int piColorCelda, int piColorFuente, bool Pbold, bool Pb_Merge, bool Pb_UnMerge, string psFormatoNumerico) 
          {
               this.WorkSheet.Range[piFilaInicial - 1, piColumnaInicial - 1, piFilaFinal - 1, piColumnaFinal - 1].Select();
               if (piColorCelda != -1)               
                    this.WorkSheet.Range[piFilaInicial - 1, piColumnaInicial - 1, piFilaFinal - 1, piColumnaFinal - 1].Interior.ColorIndex = piColorCelda - 1;               
               if (piColorFuente != -1)               
                    this.WorkSheet.Range[piFilaInicial - 1, piColumnaInicial - 1, piFilaFinal - 1, piColumnaFinal - 1].Font.ColorIndex = piColorFuente - 1;        
               this.WorkSheet.Range[piFilaInicial - 1, piColumnaInicial - 1, piFilaFinal - 1, piColumnaFinal - 1].Font.Bold = Pbold;
               if (Pb_Merge)               
                    this.WorkSheet.Range[piFilaInicial - 1, piColumnaInicial - 1, piFilaFinal - 1, piColumnaFinal - 1].Merge();        
               if (Pb_UnMerge)               
                    this.WorkSheet.Range[piFilaInicial - 1, piColumnaInicial - 1, piFilaFinal - 1, piColumnaFinal - 1].UnMerge();        
               if (psFormatoNumerico != "")               
                    this.WorkSheet.Range[piFilaInicial - 1, piColumnaInicial - 1, piFilaFinal - 1, piColumnaFinal - 1].NumberFormat = psFormatoNumerico;        
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFila"></param>
          /// <param name="piColumna"></param>
          /// <returns></returns>
          public object RegresaValorCelda(int piFila, int piColumna) 
          {
               return this.WorkSheet.Cells[piFila - 1, piColumna - 1].Value;
          }
          

          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="poCol"></param>
          /// <param name="piColumnas"></param>
          /// <param name="poRPT"></param>
          /// <returns></returns>
          private string Agrupaciones(Object[,] poCol, int piColumnas, TablaGenerica poRPT) 
          {
               int liElemtosDeLaMatrizDeSuma;
               int[] Lai_ColumnasConSuma;
               int liContador;
               int liFilaEncabezado;
               bool lbAgrupaReg;
               IDictionary<int, int> loColumnasAgupacion ;
               string lsMensaje; 
               liElemtosDeLaMatrizDeSuma = -1;
               loColumnasAgupacion = new Dictionary<int, int>();
               lbAgrupaReg = false;
               //Revisa cada columna para saber si hay agrupaciones
               for (liContador = 0; liContador <= piColumnas; liContador++)
               {
                    if (poCol[liContador, 5].ToString() != "")
                    {
                         lbAgrupaReg = true;
                         break;
                    }
               }
               if (! lbAgrupaReg)
               {
                    //Si no hay agrupaciones, procede a exportar el reporte
                    //SpreadSheetGear da mejores resultados usando Cells["rango"].Columns.Autofit que
                    //Cells.EntireColumn.AutoFit() como se usa en owc
                    this.WorkSheet.Cells[string.Format("A:{0}", this.ConvierteNumeroAColumnaDeExcel(piColumnas))].Columns.AutoFit();
                    this.WorkSheet.Cells["A1"].Select();
                    return this.ExportaReporte();
               }
               
               //Revisa si realmente trae datos el reporte para despues revisar las agrupaciones
               if (poRPT.TotalFilas == 0)
               {
                    //Si no hay agrupaciones, procede a exportar el reporte                    
                    //SpreadSheetGear da mejores resultados usando Cells["rango"].Columns.Autofit que
                    //Cells.EntireColumn.AutoFit() como se usa en owc
                    this.WorkSheet.Cells[string.Format("A:{0}", this.ConvierteNumeroAColumnaDeExcel(piColumnas))].Columns.AutoFit();
                    this.WorkSheet.Cells["A1"].Select();
                    return this.ExportaReporte();
               }
               //Obteniendo columnas de tipo número
               Lai_ColumnasConSuma = new int[1]; 
               for (liContador = 0; liContador <= piColumnas; liContador++)
               {
                    if (poCol[liContador, 2].ToString() != "0" & poCol[liContador, 2].ToString() != "")
                    {
                         if (poCol[liContador, 2].ToString().Contains("9") || poCol[liContador, 2].ToString().Contains("0"))
                         {
                              //Utiliza la mascara de la columna para saber si se trata de un número
                              //de ser asi, hara un total sobre la columna
                              liElemtosDeLaMatrizDeSuma = liElemtosDeLaMatrizDeSuma + 1;
                              Array.Resize(ref Lai_ColumnasConSuma, liElemtosDeLaMatrizDeSuma);                              
                              Lai_ColumnasConSuma[liElemtosDeLaMatrizDeSuma] = liContador;
                         }
                    }
                    if (poCol[liContador, 5].ToString() != "0" && poCol[liContador, 5].ToString() != "")
                         //Se quiere agrupar por esta columna
                         loColumnasAgupacion.Add(Convert.ToInt32(poCol[liContador, 5]), liContador);            
               }
               liFilaEncabezado = 12;
               if (Lai_ColumnasConSuma[0] != 0)
                    this.SubtotalParaAgrupacionesRecursivo(liFilaEncabezado + 1, 1, liFilaEncabezado + poRPT.TotalFilas, piColumnas + 1, Lai_ColumnasConSuma, loColumnasAgupacion, 1);
               else
               {
                    //Verificamos si existe algun campo con un formato numerico para hacer la agrupacion
                    //lsMensaje = Traduccion.Traduce(clsSICTraduccion.enTipoTexto.MENSAJE, "msjNoCamposNumericos");
                    //if( lsMensaje == "" )               
                    lsMensaje = "No hay campos con formato numerico en el reporte";            
                    throw new ApplicationException(lsMensaje);
               }
               //SpreadSheetGear da mejores resultados usando Cells["rango"].Columns.Autofit que
               //Cells.EntireColumn.AutoFit() como se usa en owc
               this.WorkSheet.Cells[string.Format("A:{0}", this.ConvierteNumeroAColumnaDeExcel(piColumnas))].Columns.AutoFit();
               this.WorkSheet.Cells["A1"].Select();
               return this.ExportaReporte();
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFilaInicial"></param>
          /// <param name="piColumnaInicial"></param>
          /// <param name="piFilaFinal"></param>
          /// <param name="piColumnaFinal"></param>
          /// <param name="Pai_ColumnasSuma"></param>
          /// <param name="poColumnasAgrupacion"></param>
          /// <param name="piLlaveColumnaAgrupacion"></param>
          /// <returns></returns>
          public int SubtotalParaAgrupacionesRecursivo(int piFilaInicial, int piColumnaInicial, int piFilaFinal, int piColumnaFinal, int[] Pai_ColumnasSuma, IDictionary<int, int> poColumnasAgrupacion, int piLlaveColumnaAgrupacion) 
          {
               string lsValorActual;
               int liX;
               int liIniciaSubtotal;
               int liOffset;
               int liFilaFinalOriginal;
               //Esta función obtiene el total de varias filas de datos relacionados, insertando autómaticamente subtotales
               //y totales para las celdas seleccionadas. A diferencia de la función SUBTOTAL de excel soporta la
               //agrupación de varias columnas
               //Descripción de parámetros:
               //piFilaInicial, piColumnaInicia, piFilaFinal, piColumnaFinal: Definen el rango sobre el que se quiere aplicar la función
               //Pai_ColumnaSumas: Arreglo que indica las columnas para las cuales se haran los subtotales
               //poColumnasAgrupacion: Indica sobre que columnas agrupar y el orden ejemplo: {(1),(5)}, {(2),(3)} indica que primero se agrupara por la columna 5 y después por la columna 3
               //piLlaveColumnaAgrupacion: Valor usado para recursividadl, indica a partir de que columna agrupar: Tomando el ejemplo anterior si  piLlaveColumnaAgrupacion=2 significaría
               //que ya se agrupo por la columna 5 y que ahora se desea agrupar por la columna 3
               liFilaFinalOriginal = piFilaFinal;
               lsValorActual = this.WorkSheet.Cells[piFilaInicial - 1, poColumnasAgrupacion[piLlaveColumnaAgrupacion] - 1].Value.ToString();
               liIniciaSubtotal = piFilaInicial;
               liX = liIniciaSubtotal + 1; 
               while ((liX <= piFilaFinal + 1))
               {
                    if (!(this.WorkSheet.Cells[liX - 1, poColumnasAgrupacion[piLlaveColumnaAgrupacion] - 1].Value == null))
                    {
                         if (this.WorkSheet.Cells[liX - 1, poColumnasAgrupacion[piLlaveColumnaAgrupacion] - 1].Value.ToString() != "" & this.WorkSheet.Cells[liX - 1, poColumnasAgrupacion[piLlaveColumnaAgrupacion] - 1].Value.ToString() != lsValorActual)
                         {
                              //Cambio el valor
                              this.CreaGrupoSubtotal(liIniciaSubtotal, piColumnaInicial, liX - 1, piColumnaFinal, Pai_ColumnasSuma, poColumnasAgrupacion[piLlaveColumnaAgrupacion], lsValorActual);
                              liOffset = 1;
                              if (poColumnasAgrupacion.ContainsKey(piLlaveColumnaAgrupacion + 1))                              
                                   //Agrupamos los siguientes niveles
                                   liOffset = liOffset + this.SubtotalParaAgrupacionesRecursivo(liIniciaSubtotal, piColumnaInicial, liX - 1, piColumnaFinal, Pai_ColumnasSuma, poColumnasAgrupacion, piLlaveColumnaAgrupacion + 1);                              
                              piFilaFinal = piFilaFinal + liOffset;
                              lsValorActual = this.WorkSheet.Cells[liX - 1 + liOffset, poColumnasAgrupacion[piLlaveColumnaAgrupacion] - 1].Value.ToString();
                              liIniciaSubtotal = liX + liOffset;
                              liX = liIniciaSubtotal;
                         }
                    }
                    liX = liX + 1;
               }
               //Subtotal del ultimo grupo
               //Agrupamos los siguientes niveles
               this.CreaGrupoSubtotal(liIniciaSubtotal, piColumnaInicial, piFilaFinal, piColumnaFinal, Pai_ColumnasSuma, poColumnasAgrupacion[piLlaveColumnaAgrupacion], lsValorActual);
               liOffset = 1;
               if (poColumnasAgrupacion.ContainsKey(piLlaveColumnaAgrupacion + 1))
               {
                    //Agrupamos los siguientes niveles
                    liOffset = liOffset + this.SubtotalParaAgrupacionesRecursivo(liIniciaSubtotal, piColumnaInicial, liX - 2, piColumnaFinal, Pai_ColumnasSuma, poColumnasAgrupacion, piLlaveColumnaAgrupacion + 1);
               }
               piFilaFinal = piFilaFinal + liOffset;
               return piFilaFinal - liFilaFinalOriginal;
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFilaInicial"></param>
          /// <param name="piColumnaInicial"></param>
          /// <param name="piFilaFinal"></param>
          /// <param name="piColumnaFinal"></param>
          /// <param name="Pai_ColumnasSuma"></param>
          /// <param name="piColumaAgrupacion"></param>
          /// <param name="psDescripcion"></param>
          private void CreaGrupoSubtotal(int piFilaInicial, int piColumnaInicial, int piFilaFinal, int piColumnaFinal, int[] Pai_ColumnasSuma, int piColumaAgrupacion, string psDescripcion) 
          {
               string lsColumna;
               int liX;
               //Esta función obtiene los subtotales de varias filas de datos relacionados y las agrupa.
               //Descripción de parámetros:
               //piFilaInicial, piColumnaInicia, piFilaFinal, piColumnaFinal: Definen el rango sobre el que se quiere aplicar la función
               //Pai_ColumnaSumas: Arreglo que indica las columnas para las cuales se haran los subtotales
               //piColumnaAgrupacion: Indica la columna sobre la que pondra la leyenda de subtotal
               //psDescripcion: Indica la descripción del subtotal
               this.WorkSheet.Cells[string.Format("{0}{1}:{2}{3}", piFilaFinal, 1, piFilaFinal, 1)].Insert(SpreadsheetGear.InsertShiftDirection.Down);
               this.WorkSheet.Cells[piFilaFinal, piColumaAgrupacion - 1].Value = string.Format("SUBTOTAL {0}", psDescripcion);
               this.WorkSheet.Cells[piFilaFinal, piColumaAgrupacion - 1].Font.Bold = true; 
               for (liX = 0; liX < Pai_ColumnasSuma.Length; liX++)
               {
                    lsColumna = this.ConvierteNumeroAColumnaDeExcel(Pai_ColumnasSuma[liX]);
                    this.WorkSheet.Cells[piFilaFinal, Pai_ColumnasSuma[liX] - 1].Formula = string.Format("=SUBTOTAL(9,{0}{1}:{2}{3})", lsColumna, piFilaInicial, lsColumna, piFilaFinal);
                    this.WorkSheet.Cells[piFilaFinal, Pai_ColumnasSuma[liX] - 1].NumberFormat = "#,D0_);(#,D0)";
                    this.WorkSheet.Cells[piFilaFinal, Pai_ColumnasSuma[liX] - 1].Font.Bold = true;
               }
               this.WorkSheet.Range[string.Format("{0}{1}:{2}{3}", this.ConvierteNumeroAColumnaDeExcel(piColumnaInicial), piFilaInicial, this.ConvierteNumeroAColumnaDeExcel(piColumnaFinal), piFilaFinal)].EntireRow.Group();
          }
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="piFila"></param>
          /// <param name="piColumna"></param>
          /// <param name="poValor"></param>
          public void AsignaFormulaCelda(int piFila, int piColumna, string poValor) 
          {
               this.WorkSheet.Cells[piFila - 1, piColumna - 1].Formula = poValor;
          }
          
          
          
          #region Disposable
          
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
               if (!this.disposed) 
               {
                    if (poDisposing) 
                    {
                         //Llamamos al Dispose de todos los RECURSOS MANEJADOS.                         
                    }
                    // Acá finalizamos correctamente los RECURSOS NO MANEJADOS
                    this.Sistema = null;
                    if (this.WorkSheet != null)
                         //02/Mayo/2011
                         //Estoy haciendo pruebas con este metodo
                         //tiene implicaciones por las cuales no dejo la linea operando
                         //Marshal.FinalReleaseComObject(SpreadExcel)
                         this.WorkSheet = null;
                    
                    if (this.WorkBook != null)
                    {
                         //02/Mayo/2011
                         //Estoy haciendo pruebas con este metodo
                         //tiene implicaciones por las cuales no dejo la linea operando
                         //Marshal.FinalReleaseComObject(SpreadExcel)
                         this.WorkBook = null;
                    }
               }
               this.disposed = true;
          }
          
          /// <summary>
          /// Destructor de la instancia
          /// </summary>
          ~ExcelSSG() 
          {
               this.Dispose(false);
          }
          
          #endregion

          #endregion
     }
}