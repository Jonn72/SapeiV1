using SpreadsheetGear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.ExcelSSG
{
     /// <summary>
     /// 
     /// </summary>
     public class ImportadoCuentas
     {
          /// <summary>
          /// Gets the total paginas.
          /// </summary>
          /// <value>
          /// The total paginas.
          /// </value>
          public int TotalPaginas
          {
               get
               {
                    return _oLibro.Names.Count;
               }
          }
          /// <summary>
          /// Gets the fila.
          /// </summary>
          /// <value>
          /// The fila.
          /// </value>
          public int Fila
          {
               get
               {
                    return _oRango.Row;
               }
          }
          /// <summary>
          /// Gets the columna.
          /// </summary>
          /// <value>
          /// The columna.
          /// </value>
          public int Columna
          {
               get
               {
                    return _oRango.Column;
               }
          }
          /// <summary>
          /// Gets the total filas.
          /// </summary>
          /// <value>
          /// The total filas.
          /// </value>
          public int TotalFilas
          {
               get
               {
                    return _oRango.RowCount;
               }
          }
          /// <summary>
          /// Gets the total columnas.
          /// </summary>
          /// <value>
          /// The total columnas.
          /// </value>
          public int TotalColumnas
          {
               get
               {
                    return _oRango.ColumnCount;
               }
          }
          /// <summary>
          /// Celdas the specified pi fila.
          /// </summary>
          /// <param name="piFila">The pi fila.</param>
          /// <param name="piColumna">The pi columna.</param>
          /// <returns></returns>
          public string Celda(int piFila, int piColumna)
          {
               return _oCeldas[piFila, piColumna].Text;
          }
          /// <summary>
          /// The _o workbook set/
          /// </summary>
          private IWorkbookSet _oWorkbookSet;
          /// <summary>
          /// The _o libro
          /// </summary>
          private IWorkbook _oLibro;
          /// <summary>
          /// The _o rango
          /// </summary>
          private IRange _oRango;
          /// <summary>
          /// The _o hoja libro/
          /// </summary>
          private IWorksheet _oHojaLibro;
          /// <summary>
          /// The _o celdas
          /// </summary>
          private IRange _oCeldas;
          /// <summary>
          /// The _o valores
          /// </summary>
          private SpreadsheetGear.Advanced.Cells.IValues _oValores;
          /// <summary>
          /// Initializes a new instance of the <see cref="ImportadoCuentas"/> class.
          /// </summary>
          public ImportadoCuentas()
          {
               _oWorkbookSet = Factory.GetWorkbookSet();
          }
          /// <summary>
          /// Initializes a new instance of the <see cref="ImportadoCuentas"/> class.
          /// </summary>
          /// <param name="pabyArchivo">The paby archivo.</param>
          public ImportadoCuentas(byte[] pabyArchivo)
          {
               _oWorkbookSet = Factory.GetWorkbookSet();
               _oLibro = _oWorkbookSet.Workbooks.OpenFromMemory(pabyArchivo);
          }
          /// <summary>
          /// Abres the libro en memoria.
          /// </summary>
          /// <param name="pabyArchivo">The paby archivo.</param>
          public void AbreLibroEnMemoria(byte[] pabyArchivo)
          {
               _oLibro = _oWorkbookSet.Workbooks.OpenFromMemory(pabyArchivo);
          }
          /// <summary>
          /// Refrecas the rango.
          /// </summary>
          public void RefrecaRango()
          {
               _oRango = _oLibro.Names[1].RefersToRange;
          }
          /// <summary>
          /// Inicias the libro.
          /// </summary>
          public void IniciaLibro()
          {
               _oHojaLibro = _oRango.Worksheet;
               _oCeldas = _oHojaLibro.Cells;
               _oValores = (SpreadsheetGear.Advanced.Cells.IValues)_oHojaLibro;
          }
     }
}