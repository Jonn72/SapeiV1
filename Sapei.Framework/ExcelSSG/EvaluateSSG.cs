namespace Sapei.Framework.ExcelSSG
{
     /// <summary>
     /// 
     /// </summary>
     public class EvaluateSSG
     {
          #region Variables
          
          /// <summary>
          /// 
          /// </summary>
          private SpreadsheetGear.IWorkbook _oWorkBook;
          /// <summary>
          /// 
          /// </summary>
          private SpreadsheetGear.IWorksheet _oWorkSheet;
          
          #endregion
          
          #region Constructores
          
          /// <summary>
          /// 
          /// </summary>
          public EvaluateSSG()
          {
               // Create a new empty workbook and get the first sheet.
               // NOTE: Use GetWorkbook(System.Globalization.CultureInfo.CurrentCulture)
               //       to use the current culture instead of the default US English culture.
               _oWorkBook = SpreadsheetGear.Factory.GetWorkbook();
               // Get the first worksheet.
               _oWorkSheet = _oWorkBook.Worksheets[0];
          }
          
          #endregion
          
          #region Funciones
          
          /// <summary>
          /// 
          /// </summary>
          /// <param name="psFormula"></param>
          /// <returns></returns>
          public object Evaluate(string psFormula) 
          {
               // Evaluate the input formula.
               object result = _oWorkSheet.EvaluateValue(psFormula); 
               // Display the result to the user.               
               if (result == null)                                   
               {
                    return 0;
               }
               else if (result is SpreadsheetGear.ValueError)               
               {
                    return 0;
               }
               else 
               {
                    return result;
               }
          }
          
          #endregion
     }
}