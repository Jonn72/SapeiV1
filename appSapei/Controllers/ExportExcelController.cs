using Sapei.Framework.Utilerias;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class ExportExcelController : Controller
    {

        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }

        public static byte[] ExportExcel(DataTable dataTable, DataTable dataTable2,  string heading = "", bool showSrNo = false, params string[] columnsToTake)
        {

            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));

                using (System.Drawing.Image image = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("../Images/logos/tecnm.jpg")))
                {
                    var excelImage = workSheet.Drawings.AddPicture("TecNM", image);

                    //add the image to row 20, column E
                    excelImage.SetPosition(1, 10, 9, 10);
                    excelImage.SetSize(75);
                }

                using (System.Drawing.Image image = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("../Images/logos/sep.jpg")))
                {
                    var excelImage = workSheet.Drawings.AddPicture("Educación", image);

                    //add the image to row 20, column E
                    excelImage.SetPosition(1, 15, 0, 15);
                    excelImage.SetSize(65);
                }

                workSheet.Cells["C1:I2"].Merge = true;
                workSheet.Cells["C1"].Value = "Instituto Tecnológico de Tláhuac";
                workSheet.Cells["C3:I4"].Merge = true;
                workSheet.Cells["C3"].Value = dataTable2.Rows[0].RegresaValor<string>("departamento");
                workSheet.Cells["C5:I5"].Merge = true;
                workSheet.Cells["C5"].Value = dataTable2.Rows[0].RegresaValor<string>("nombre_empleado");
                workSheet.Cells["C6:I6"].Merge = true;
                workSheet.Cells["C6"].Value = dataTable2.Rows[0].RegresaValor<string>("nombre_materia");
                workSheet.Cells["C7:I7"].Merge = true;
                workSheet.Cells["C7"].Value ="Grupo: " + dataTable2.Rows[0].RegresaValor<string>("grupo");
                workSheet.Cells["A8"].Value = "Dia";
                workSheet.Cells["A9"].Value = "Horario/aula";
                workSheet.Cells["B8"].Value = "Lunes";
                workSheet.Cells["B9"].Value = dataTable2.Rows[0].RegresaValor<string>("lunes");
                workSheet.Cells["C8"].Value = "Martes";
                workSheet.Cells["C9"].Value = dataTable2.Rows[0].RegresaValor<string>("martes");
                workSheet.Cells["D8"].Value = "Miercoles";
                workSheet.Cells["D9"].Value = dataTable2.Rows[0].RegresaValor<string>("miercoles");
                workSheet.Cells["E8"].Value = "Jueves";
                workSheet.Cells["E9"].Value = dataTable2.Rows[0].RegresaValor<string>("jueves");
                workSheet.Cells["F8"].Value = "Viernes";
                workSheet.Cells["F9"].Value = dataTable2.Rows[0].RegresaValor<string>("viernes");
                var allCells = workSheet.Cells["C1:C7"];
                allCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var titulo = workSheet.Cells["C1"];
                titulo.Style.Locked = true;
                var cellFont = titulo.Style.Font;
                cellFont.SetFromFont(new System.Drawing.Font("Montserrat ExtraBold", 16));
                cellFont.Bold = true;

                var datos = workSheet.Cells["C3:C7"];
                datos.Style.Locked = true;
                cellFont = datos.Style.Font;
                cellFont.SetFromFont(new System.Drawing.Font("Montserrat ExtraBold", 12));
                cellFont.Bold = true;

                var horario = workSheet.Cells["A8:F9"];
                horario.Style.Locked = true;

                int startRowFrom = 11;

                // add the content into the Excel file  
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));

                    r.Style.Locked = true;
                }

                using (ExcelRange r = workSheet.Cells["A8:F8"])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }

                workSheet.Cells["A9"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells["A9"].Style.Font.Bold = true;
                workSheet.Cells["A9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A9"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();


                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count + 6])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }
                result = package.GetAsByteArray();
            }

            return result;
        }


        public static byte[] ExportExcel2(DataTable dataTable, DataTable dataTable2, string heading = "", bool showSrNo = false, params string[] columnsToTake)
        {

            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));

                using (System.Drawing.Image image = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("../Images/logos/tecnm.jpg")))
                {
                    var excelImage = workSheet.Drawings.AddPicture("TecNM", image);

                    //add the image to row 20, column E
                    excelImage.SetPosition(1, 10, 9, 10);
                    excelImage.SetSize(75);
                }

                using (System.Drawing.Image image = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("../Images/logos/sep.jpg")))
                {
                    var excelImage = workSheet.Drawings.AddPicture("Educación", image);

                    //add the image to row 20, column E
                    excelImage.SetPosition(1, 15, 0, 15);
                    excelImage.SetSize(65);
                }

                workSheet.Cells["C1:I2"].Merge = true;
                workSheet.Cells["C1"].Value = "Instituto Tecnológico de Tláhuac";
                workSheet.Cells["C3:I4"].Merge = true;
                workSheet.Cells["C3"].Value = dataTable2.Rows[0].RegresaValor<string>("departamento");
                workSheet.Cells["C5:I5"].Merge = true;
                workSheet.Cells["C5"].Value = dataTable2.Rows[0].RegresaValor<string>("nombre_empleado");
                workSheet.Cells["C6:I6"].Merge = true;
                workSheet.Cells["C6"].Value = dataTable2.Rows[0].RegresaValor<string>("nombre_materia");
                workSheet.Cells["C7:I7"].Merge = true;
                workSheet.Cells["C7"].Value = "Grupo: " + dataTable2.Rows[0].RegresaValor<string>("grupo");
                workSheet.Cells["A8"].Value = "Dia";
                workSheet.Cells["A9"].Value = "Horario/aula";
                workSheet.Cells["B8"].Value = "Lunes";
                workSheet.Cells["B9"].Value = dataTable2.Rows[0].RegresaValor<string>("lunes");
                workSheet.Cells["C8"].Value = "Martes";
                workSheet.Cells["C9"].Value = dataTable2.Rows[0].RegresaValor<string>("martes");
                workSheet.Cells["D8"].Value = "Miercoles";
                workSheet.Cells["D9"].Value = dataTable2.Rows[0].RegresaValor<string>("miercoles");
                workSheet.Cells["E8"].Value = "Jueves";
                workSheet.Cells["E9"].Value = dataTable2.Rows[0].RegresaValor<string>("jueves");
                workSheet.Cells["F8"].Value = "Viernes";
                workSheet.Cells["F9"].Value = dataTable2.Rows[0].RegresaValor<string>("viernes");
                var allCells = workSheet.Cells["C1:C7"];
                allCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var titulo = workSheet.Cells["C1"];
                titulo.Style.Locked = true;
                var cellFont = titulo.Style.Font;
                cellFont.SetFromFont(new System.Drawing.Font("Montserrat ExtraBold", 16));
                cellFont.Bold = true;

                var datos = workSheet.Cells["C3:C7"];
                datos.Style.Locked = true;
                cellFont = datos.Style.Font;
                cellFont.SetFromFont(new System.Drawing.Font("Montserrat ExtraBold", 12));
                cellFont.Bold = true;

                var horario = workSheet.Cells["A8:F9"];
                horario.Style.Locked = true;

                int startRowFrom = 11;

                // add the content into the Excel file  
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));

                    r.Style.Locked = true;
                }

                using (ExcelRange r = workSheet.Cells["A8:F8"])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }

                workSheet.Cells["A9"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells["A9"].Style.Font.Bold = true;
                workSheet.Cells["A9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A9"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();


                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count + 6])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }
                result = package.GetAsByteArray();
            }

            return result;
        }

    }
}
