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
	/// Clase alumnos_titulado generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Alumnos_Titulado
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

		#region Titulados
		public DataTable Alumnos_Titulados(string psNoControl)
        {

            StringBuilder lsConsulta;
            DataTable loDt;
            lsConsulta = new StringBuilder();
            lsConsulta.AppendFormat("if exists (select 1 from alumnos_titulados where no_de_control = '{0}') ", psNoControl);
            lsConsulta.AppendFormat("select '{0}' as no_de_control, a.nombre_alumno + ' ' + a.apellido_paterno + ' ' + a.apellido_materno as nombre, ", psNoControl);
            lsConsulta.Append("atl.periodo_titulacion, convert(varchar(40), atl.fecha_acto, 103) as fecha_acto, atl.id_tipo ");
            lsConsulta.AppendFormat("from {0} a, {1} atl where a.no_de_control = atl.no_de_control and a.no_de_control = '{2}' ", "alumnos", "alumnos_titulados", psNoControl);
            lsConsulta.Append("else ");
            lsConsulta.AppendFormat("select '{0}' as no_de_control, nombre_alumno + ' ' + apellido_paterno + ' ' + apellido_materno as nombre, ", psNoControl);
            lsConsulta.AppendFormat("'' as periodo_titulacion, '' as fecha_acto, '' as id_tipo ");
            lsConsulta.AppendFormat("from {0} where no_de_control = '{1}'", "alumnos", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
            return loDt;
        }

      
        public DataTable ConsultaTitulados() 
        {
            StringBuilder lsConsulta;
            DataTable loDt;
            lsConsulta = new StringBuilder();
            lsConsulta.Append("select '<button type=\"button\"  onclick =\"Regresa(''' + TRIM(alt.no_de_control) + ''');\" class=\"btn btn-success\"><i class=\"fa fa-eye\"></i></button>' as boton,"); 
            lsConsulta.Append("alt.no_de_control, a.apellido_paterno + ' ' + a.apellido_materno + ' ' + a.nombre_alumno as nombre, a.curp_alumno, c.nombre_reducido as carrera, ");
            lsConsulta.Append("  SUBSTRING(a.curp_alumno,9,2) + '/' + SUBSTRING(a.curp_alumno,7,2) + '/' + SUBSTRING(a.curp_alumno,5,2) as edad, ");
            lsConsulta.Append("(select identificacion_corta from periodos_escolares where a.periodo_ingreso_it = periodo) as periodo_ingreso, ");
            lsConsulta.Append("(select identificacion_corta from periodos_escolares where a.ultimo_periodo_inscrito = periodo) as periodo_egreso, a.sexo as genero, ");
            lsConsulta.Append("(select identificacion_corta from periodos_escolares where alt.periodo_titulacion = periodo) as semestre_titulacion, ");
            lsConsulta.Append("CONVERT(varchar(30), alt.fecha_acto, 103) as fecha_acto, (select tipo from tipos_titulaciones where alt.id_tipo = id_tipo) as tipo_titulacion ");
            lsConsulta.AppendFormat("from {0} a, {1} c, {2} alt where alt.no_de_control = a.no_de_control and a.carrera = c.carrera and a.reticula = c.reticula", "alumnos", "carreras", "alumnos_titulados");
            loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
            return loDt;
        }
        #endregion
    }
}
