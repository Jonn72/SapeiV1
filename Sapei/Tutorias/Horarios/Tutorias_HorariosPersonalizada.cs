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
	/// Clase tutorias_horario generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Tutorias_Horarios
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
          public void BorraHorarios(string psPeriodo, string psGrupo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("DELETE ");
               lsConsulta.AppendFormat("{0}", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = {0} and grupo = '{1}'", psPeriodo, psGrupo);
               _oSistema.Conexion.EjecutaComando(lsConsulta);
          }
          public void GuardaHorario(string psPeriodo, string psGrupo, string psHorarios)
          {
               StringBuilder lsConsulta;
               StringBuilder lsHorario;
               string[] pasHorarios;
               string[] pasDia;
               int liDia;
               lsHorario = new StringBuilder();
               lsConsulta = new StringBuilder();
               BorraHorarios(psPeriodo,psGrupo);
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
                    lsHorario.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}'),", psPeriodo, psGrupo, dia, hora_inicio, hora_fin, aula);

               }
               lsHorario = lsHorario.Remove(lsHorario.Length - 1, 1);
               lsConsulta.Append("INSERT INTO ");
               lsConsulta.AppendFormat("{0}", RutaTabla);
               lsConsulta.Append("([periodo],[grupo],[dia],[hora_inicio],[hora_fin],[aula])");
               lsConsulta.Append(" VALUES ");
               lsConsulta.AppendFormat("{0}", lsHorario.ToString());
               _oSistema.Conexion.EjecutaComando(lsConsulta);

          }
		#endregion

		}
	}
