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
	/// Clase fecha_evaluacion generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class fecha_evaluacion
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

        public DataTable RegresaFechaPeriodoEvaluacion(string psPeriodo = null)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append("SELECT convert(varchar, fecha_inicio, 103) fecha_inicio, convert(varchar, fecha_fin, 103) fecha_fin");
            lsConsulta.AppendFormat(" FROM {0}", RutaTabla);
            if (!string.IsNullOrEmpty(psPeriodo))
                lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
            else
                lsConsulta.Append(" order by periodo desc");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);
        }

        public DataTable VerificaEvaluacion(string psPeriodo = null)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append("SELECT 1");
            lsConsulta.AppendFormat(" FROM {0}", "evaluacion_alumnos");
            if (!string.IsNullOrEmpty(psPeriodo))
                lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
            else
                lsConsulta.Append(" order by periodo desc");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);
        }

        public DataTable Evaluacion(string psPeriodo, string psNoControl)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append("SELECT a.no_de_control, a.apellido_paterno + ' ' + apellido_materno + ' ' + a.nombre_alumno nombre, ");
            lsConsulta.Append("(select nombre_carrera from carreras where no_de_control = a.no_de_control and reticula = a.reticula) carrera, a.semestre, ");
            lsConsulta.AppendFormat("case when exists (select 1 from evaluacion_alumnos where periodo = '{0}' and no_de_control = a.no_de_control) then 'Realizó Evaluacion' else 'No Realizó Evaluacion' end evaluacion ", psPeriodo);
            lsConsulta.AppendFormat("from alumnos a where a.no_de_control = '{0}'", psNoControl);
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);
        }

        public DataTable TablaEvaluacion(string psNoControl)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append("select distinct ea.periodo, (select identificacion_larga from periodos_escolares where periodo = ea.periodo) identificacion, convert(varchar,fecha_hora_evaluacion,113) fecha from evaluacion_alumnos ea ");
            lsConsulta.AppendFormat("where no_de_control = '{0}' order by periodo desc", psNoControl);
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);
        }

    }
	}
