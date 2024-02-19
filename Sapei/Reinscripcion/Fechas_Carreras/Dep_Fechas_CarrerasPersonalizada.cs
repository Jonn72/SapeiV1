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
	/// Clase dep_fechas_carrera generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Dep_Fechas_Carreras_Seleccion
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

		/// <summary>
		/// Regresa periodos registrados
		/// </summary>
		/// <returns></returns>
		public DataTable RegresaTablaPeriodos(string psPeriodo,bool pbBotonEditar = false)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" SELECT ");
			if (pbBotonEditar)
			{
				lsConsulta.AppendFormat("case when periodo = '{0}' ", psPeriodo);
				lsConsulta.Append(" then '<a class=\"btn btn-success\" ><span class=\"fa fa-edit\"></span></a> ");
				lsConsulta.Append("<a class=\"btn btn-info\"><span class=\"fa fa-file-pdf-o\"></span></a>'");
				lsConsulta.Append("+ case generada when 1 then ");
				lsConsulta.Append(" case publicada when 0 then '<a class=\"btn btn-danger\" data-toggle=\"tooltip\" data-placement=\"top\" title =\"Publicar Listas\" ><span class=\"fa fa-eye-slash\"></span></a>' else '<a class=\"btn btn-warning\" data-toggle=\"tooltip\" data-placement=\"top\" title =\"No Publicar Listas\"><span class=\"fa fa-eye\"></span></a>' end ");
				lsConsulta.Append(" else '' end  else '' end ,");
			}
				lsConsulta.Append("(select identificacion_corta from periodos_escolares where periodo = P.periodo) periodo,");
			lsConsulta.Append("[carrera],[fecha_inicio]");
			lsConsulta.Append(",[intervalo],[personas]");
			lsConsulta.AppendFormat(" FROM {0} P", RutaTabla);
			lsConsulta.Append(" order by periodo desc");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		#endregion

	}
	}
