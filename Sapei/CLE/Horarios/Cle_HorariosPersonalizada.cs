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
	/// Clase cle_horario generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Cle_Horarios
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
          public void GuardaHorario(string psHorarios)
          {
               StringBuilder lsConsulta;
               StringBuilder lsHorario;
               string[] pasHorarios;
               string[] pasDia;
               int liDia;
               lsHorario = new StringBuilder();
               lsConsulta = new StringBuilder();
               BorraHorarios();
               psHorarios = psHorarios.Substring(1,psHorarios.Length-1);
               pasHorarios = psHorarios.Split('|');
               foreach (string psDia in pasHorarios)
               {

                    pasDia = psDia.Split(',');
                    this.hora_inicial = pasDia[0].Substring(11,5);
                    this.hora_final = pasDia[1].Substring(11, 5);
                    this.dia = Convert.ToString(Convert.ToInt32(pasDia[0].Substring(8,2))+1);
                    liDia = Convert.ToInt32(pasDia[0].Substring(8, 2)) + 1;
                    lsHorario.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),", periodo, nivel, grupo, liDia, hora_inicial, hora_final, aula);
                    
               }
               lsHorario = lsHorario.Remove(lsHorario.Length - 1, 1);
               lsConsulta.Append("INSERT INTO ");
               lsConsulta.AppendFormat("{0}", RutaTabla);
               lsConsulta.Append("([periodo],[nivel],[grupo],[dia],[hora_inicial],[hora_final],[aula])");
               lsConsulta.Append(" VALUES ");
               lsConsulta.AppendFormat("{0}",lsHorario.ToString());
               _oSistema.Conexion.EjecutaComando(lsConsulta);

          }
          public void BorraHorarios()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("DELETE ");
               lsConsulta.AppendFormat("{0}", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}' AND nivel = '{1}' AND grupo = '{2}'", this.periodo, this.nivel, this.grupo);
               _oSistema.Conexion.EjecutaComando(lsConsulta);
          }

          public DataTable RegresaHorarios(string psPeriodo)
          {
               StringBuilder lsConsulta;
            DataTable dt = new DataTable();
            List<ParametrosSQL> param = new List<ParametrosSQL>();
            param.Add(new ParametrosSQL("@periodo",psPeriodo));
            using (var conexion = new ManejaConexion(_oSistema.Conexion))
            {
                dt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_aulas_horarios", param ));
            }
            return dt; ;
          }    
        #endregion

    }
}
