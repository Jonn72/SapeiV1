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
	/// Clase ss_liberado generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class SS_liberados
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
		public DataTable RegresaTablaEstudiantesSS(string psControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("declare @html_no_ok varchar(max)");
			lsConsulta.Append("declare @html_ok varchar(max)");
			lsConsulta.Append(" SET @html_no_ok = '<img src=\"/Images/100x100/no-ok.png\" class=\"avatar\" alt=\"Avatar\" />'");
			lsConsulta.Append(" SET @html_ok = '<img src=\"/Images/100x100/ok.png\" class=\"avatar\" alt=\"Avatar\" />'");
			lsConsulta.Append("SELECT no_de_control, ");
			lsConsulta.Append("(select apellido_paterno+' '+apellido_materno+' '+nombre_alumno from alumnos where no_de_control = A.no_de_control) nombre,");
			lsConsulta.Append("desempeño,@html_ok dgtyv,");
			lsConsulta.Append("case validado when 1 THEN @html_ok else @html_no_ok end dse,");
			lsConsulta.Append("case	validado when 0 then '<a href=\"#\" onclick=\"LiberaSS('''+no_de_control+''');\" class=\"btn btn-primary btn-xs\"><i class=\"fa fa-check-square\"></i> Liberar </a>' else '' end   acciones");
			lsConsulta.AppendFormat(" from {0} A where no_de_control = '{1}'", RutaTabla, psControl);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		#endregion

	}
	}
