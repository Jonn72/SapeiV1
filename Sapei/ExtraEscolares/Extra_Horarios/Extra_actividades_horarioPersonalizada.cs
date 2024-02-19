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

namespace Sapei
{
     /// <summary>
     /// Clase extra_actividades_horario generada automáticamente desde el Generador de Código SII
     /// </summary>
     public partial class Extra_actividades_horario
     {
          #region variables
          #endregion
          #region Propiedades
          #endregion
          #region Contructor
          #endregion
          #region Metodos/Funciones
          /// <summary>
          /// Funcion para personalizar la validacion para un nuevo registro
          /// </summary>
          protected override void ValidacionNuevoPersonalizada()
          {
          }
          /// <summary>
          /// Funcion para personalizar el grabar en una los registros
          /// </summary>
          protected override void ValidacionGrabarPersonalizada()
          {
          }
          /// <summary>
          /// Funcion para personalizar la validacion de cambios en los registros
          /// </summary>
          protected override void ValidacionCambiosGrabarPersonalizada()
          {
          }
          /// <summary>
          /// Funcion para personalizar al eliminar los registros
          /// </summary>
          protected override void ValidacionEliminarPersonalizada()
          {
          }
          #endregion
          #region Consulta
          private StringBuilder BorraHorarios(int piID)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("DELETE ");
               lsConsulta.AppendFormat("{0}", RutaTabla);
               lsConsulta.AppendFormat(" WHERE id_actividad = {0}", piID);
               return lsConsulta;
          }
          //public void GuardaHorario(int piID, string psHorarios)
          //{
          //     StringBuilder lsConsulta;
          //     StringBuilder lsHorario;
          //     string[] pasHorarios;
          //     string[] pasDia;
          //     string psInserta;
          //     lsConsulta = new StringBuilder();
          //     lsHorario = new StringBuilder();
          //     lsConsulta = BorraHorarios(piID);
          //     psInserta = "";
          //     if (string.IsNullOrEmpty(psHorarios))
          //     {
          //          _oSistema.Conexion.EjecutaComando(lsConsulta);
          //          return;
          //     }
          //     pasHorarios = psHorarios.Split('|');
          //     foreach (string psDia in pasHorarios)
          //     {
          //          pasDia = psDia.Split('-');
          //          switch (pasDia[0].ToUpper())
          //          {
          //               case "LUN":
          //                    lsHorario.AppendFormat("({0},'LUN','{1}','{2}'),", piID, pasDia[1], pasDia[2]);
          //                    break;
          //               case "MAR":
          //                    lsHorario.AppendFormat("({0},'MAR','{1}','{2}'),", piID, pasDia[1], pasDia[2]);
          //                    break;
          //               case "MIE":
          //                    lsHorario.AppendFormat("({0},'MIE','{1}','{2}'),", piID, pasDia[1], pasDia[2]);
          //                    break;
          //               case "JUE":
          //                    lsHorario.AppendFormat("({0},'JUE','{1}','{2}'),", piID, pasDia[1], pasDia[2]);
          //                    break;
          //               case "VIE":
          //                    lsHorario.AppendFormat("({0},'VIE','{1}','{2}'),", piID, pasDia[1], pasDia[2]);
          //                    break;
          //               case "SAB":
          //                    lsHorario.AppendFormat("({0},'SAB','{1}','{2}'),", piID, pasDia[1], pasDia[2]);
          //                    break;
          //          }
          //     }
          //     lsHorario = lsHorario.Remove(lsHorario.Length - 1, 1);
          //     lsConsulta.Append("INSERT INTO ");
          //     lsConsulta.AppendFormat("{0}", RutaTabla);
          //     lsConsulta.Append("([id_actividad],[dia],[hora_inicio],[hora_fin])");
          //     lsConsulta.Append(" VALUES ");
          //     lsConsulta.AppendFormat("{0}",lsHorario.ToString());
          //     _oSistema.Conexion.EjecutaComando(lsConsulta);
          //}
          public void GuardaHorario1(int piID, string psHorarios)
          {
               StringBuilder lsConsulta;
               StringBuilder lsHorario;
               string[] pasHorarios;
               string[] pasDia;
               int liDia;
               lsHorario = new StringBuilder();
               lsConsulta = new StringBuilder();
                _oSistema.Conexion.EjecutaComando(BorraHorarios(piID));
               psHorarios = psHorarios.Substring(1, psHorarios.Length - 1);
               pasHorarios = psHorarios.Split('|');
               this.periodo = _oSistema.Sesion.Periodo.PeriodoActual;
               foreach (string psDia in pasHorarios)
               {

                    pasDia = psDia.Split(',');
                    this.hora_inicio = pasDia[0].Substring(11, 5);
                    this.hora_fin = pasDia[1].Substring(11, 5);
                    liDia = Convert.ToInt32(pasDia[0].Substring(8, 2));
                    switch (liDia)
                    {
                         case 1:
                              this.dia = "LUN";
                              break;
                         case 2:
                              this.dia = "MAR";
                              break;
                         case 3:
                              this.dia = "MIE";
                              break;
                         case 4:
                              this.dia = "JUE";
                              break;
                         case 5:
                              this.dia = "VIE";
                              break;
                         case 6:
                              this.dia = "SAB";
                              break;
                    }
                    lsHorario.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}'),", piID, dia, hora_inicio, hora_fin, aula, periodo);

               }
               lsHorario = lsHorario.Remove(lsHorario.Length - 1, 1);
               lsConsulta.Append("INSERT INTO ");
               lsConsulta.AppendFormat("{0}", RutaTabla);
               lsConsulta.Append("([id_actividad],[dia],[hora_inicio],[hora_fin],[aula],[periodo])");
               lsConsulta.Append(" VALUES ");
               lsConsulta.AppendFormat("{0}", lsHorario.ToString());
               _oSistema.Conexion.EjecutaComando(lsConsulta);

          }
          private void RegresaHorarios(int piID)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT A.id_actividad ");
               lsConsulta.Append(" ,stuff(( ");
               lsConsulta.Append(" SELECT ', ' + B.dia + ' ' + B.hora_inicio + ' - ' + B.hora_fin ");
               lsConsulta.Append(" FROM extra_actividades_horarios B ");
               lsConsulta.Append(" WHERE A.id_actividad = B.id_actividad ");
               lsConsulta.Append(" FOR XML PATH('') ");
               lsConsulta.Append(" ), 1, 1, '') Nombres");
               lsConsulta.Append(" FROM");
               lsConsulta.AppendFormat("{0} A", RutaTabla);
               lsConsulta.Append(" GROUP BY A.id_actividad ");
               lsConsulta.Append(" ORDER BY A.id_actividad ");             
          }
          #endregion
     }
}
