using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Sapei.Framework;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sapei.Framework.BaseDatos;

namespace Sapei
{
     /// <summary>
     /// Clase aspirante generada automáticamente desde el Generador de Código SII
     /// </summary>
     [Serializable]
     public static class Encuestas
     {
          #region Funciones
          public static string RegresaHtmlEncuesta(int piId, Sistema poSistema)
          {
                    string lsMensaje;
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    DataTable loDt = new DataTable();
                    System.Data.SqlClient.SqlDataReader loReader;
                    using (var loConexion = new ManejaConexion(poSistema.Conexion))
                    {
                         loParametros.Add(new ParametrosSQL("@encuesta", piId));

                         loReader = poSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_encuesta_genera", loParametros);

                         lsMensaje = "";
                         if (loReader.HasRows)
                         {
                              while (loReader.Read())
                              {
                                   lsMensaje = loReader.GetString(0);
                              }
                         }
                         loReader.Close();
                         loReader.Dispose();
                         return lsMensaje;
                    }
          }
          public static void GuardaEncuesta(string psRespuestas, Sistema poSistema)
          {
               StringBuilder lsQuery = new StringBuilder();
               string[] lasRespuestas;
               string lsEncuesta;
               string lsPregunta;
               string lsRespuesta;
               lsQuery.Append("INSERT INTO alumnos_encuestas ");
               lsQuery.Append("(periodo, no_de_control, id_encuesta, id_pregunta, id_respuesta, fecha) ");
               lsQuery.Append(" values ");
               lasRespuestas = psRespuestas.Trim().Split('|');
               lsEncuesta = lasRespuestas[0].Trim().Split('&')[0].Trim();
               foreach (string lsRes in lasRespuestas)
               {
                    lsPregunta = lsRes.Trim().Split('&')[1].Trim();
                    lsRespuesta = lsRes.Trim().Split('&')[2].Trim();
                    lsQuery.AppendFormat(" ('{0}','{1}',{2},{3},'{4}',GETDATE()),",poSistema.Sesion.Periodo.PeriodoActual, poSistema.Sesion.Usuario.Usuario,lsEncuesta,lsPregunta,lsRespuesta);
               }          
               poSistema.Conexion.EjecutaComando(lsQuery.Remove(lsQuery.Length-1,1));
          }

          public static DataTable RegresaDatosEncuestas(Sistema poSistema)
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat("select id,nombre,1 from en_encuesta where id in (select id_encuesta from alumnos_encuestas where no_de_control = '{0}' and periodo = '{1}')", poSistema.Sesion.Usuario.Usuario, poSistema.Sesion.Periodo.PeriodoActual);
               lsQuery.Append(" UNION ");
               lsQuery.AppendFormat("select id,nombre,0 from en_encuesta where id not in (select id_encuesta from alumnos_encuestas where no_de_control = '{0}' and periodo = '{1}')", poSistema.Sesion.Usuario.Usuario, poSistema.Sesion.Periodo.PeriodoActual);
               return poSistema.Conexion.RegresaDataTable(lsQuery);
          }
          #endregion

     }
}
