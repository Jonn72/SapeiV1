using Sapei;
using Sapei.Framework.Utilerias;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class ReportesCSVController : Controller
    {
         private FileContentResult RegresaArchivoCSV(DataTable poTabla, string psNombre)
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

              return File(Encoding.Convert(loUTF8,loISO,lysUTF8) , "application/csv", psNombre + ".csv");

         }
         #region CLE
         public FileContentResult DescargaGruposInscritosCLE(string psPeriodo, string psGrupo, string psNivel)
         {          
              Cle_Seleccion loReporte;
              loReporte = new Cle_Seleccion(SesionSapei.Sistema);
              DataTable loDt = loReporte.RegresaListaGruposInscritos(psPeriodo, psGrupo, psNivel);
              return RegresaArchivoCSV(loDt, psPeriodo + "-" + psGrupo + "-" + psNivel);
         }
          #endregion
         #region Extraescolares
         public FileContentResult DescargaListaEstudiantesPorActividad(string psPeriodo, string psId)
         {
              Extra_actividades_inscrito loReporte;
              loReporte = new Extra_actividades_inscrito(SesionSapei.Sistema);
              DataTable loDt = loReporte.RegresaListaInscritosPorActividad(psPeriodo, psId);
              return RegresaArchivoCSV(loDt, psPeriodo + "-" + psId);
         }
          #endregion
         #region Tutorias
         public ActionResult DescargaListaEstudiantesGrupoTutorias(string psPeriodo, string psGrupo)
         {
              Tutorias_Inscritos loReporte;
              loReporte = new Tutorias_Inscritos(SesionSapei.Sistema);
              DataTable loDt = loReporte.RegresaListaInscritosPorGrupo(psPeriodo, psGrupo);
              return RegresaArchivoCSV(loDt, psPeriodo + "-" + psGrupo);
         }
         #endregion
    }
}
