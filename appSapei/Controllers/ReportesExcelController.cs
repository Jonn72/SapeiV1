using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class ReportesExcelController : Controller
    {
         private FileContentResult RegresaArchivoExcel(DataTable poTabla, string psNombre)
         {
              StringBuilder lsCadena = new StringBuilder();
              Encoding loISO = Encoding.GetEncoding("ISO-8859-1");
              Encoding loUTF8 = Encoding.UTF8;
              byte[] lysUTF8;
              foreach (DataColumn loCol in poTabla.Columns)
              {
                   //Add the Header row for CSV file.
                   lsCadena.AppendFormat(loCol.ColumnName + ',');
              }
              lsCadena = lsCadena.Remove(lsCadena.Length - 1, 1);
              lsCadena.Append("\r\n");
              foreach (DataRow loDr in poTabla.Rows)
              {
                   foreach (DataColumn loCol in poTabla.Columns)
                   {
                        //Add the Data rows.
                        lsCadena.AppendFormat("{0},", loDr[loCol.ColumnName].ToString());
                   }
                   lsCadena = lsCadena.Remove(lsCadena.Length - 1, 1);
                   lsCadena.Append("\r\n");
              }
              lysUTF8 = loUTF8.GetBytes(lsCadena.ToString());

              SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook();
              SpreadsheetGear.IWorksheet worksheet = workbook.Worksheets["Sheet1"];
              SpreadsheetGear.IRange cells = worksheet.Cells;
              worksheet.Name = "2005 Sales";

              // Load column titles and center.
              cells["B1"].Formula = "North";
              cells["C1"].Formula = "South";
              cells["D1"].Formula = "East";
              cells["E1"].Formula = "West";
              cells["B1:E1"].HorizontalAlignment = SpreadsheetGear.HAlign.Center;
              // Load row titles using multiple cell text reference and iteration.
              int quarter = 1;
              foreach (SpreadsheetGear.IRange cell in cells["A2:A5"])
                   cell.Formula = "Q" + quarter++;

              // Load random data and format as $ using a multiple cell range.
              SpreadsheetGear.IRange body = cells[1, 1, 4, 4];
              body.Formula = "=RAND() * 10000";
              body.NumberFormat = "$#,##0_);($#,##0)";

              byte[] sos = workbook.SaveToMemory(SpreadsheetGear.FileFormat.XLS97);

              return File(sos, "application/vnd.ms-excel", psNombre + ".xls");
              //return File(Encoding.Convert(loUTF8,loISO,lysUTF8) , "application/csv", psNombre + ".csv");

         }

    }
}
