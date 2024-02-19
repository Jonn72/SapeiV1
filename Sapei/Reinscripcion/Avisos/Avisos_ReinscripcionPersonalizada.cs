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
	/// Clase avisos_reinscripcion generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Avisos_Reinscripcion
	{
		#region variables
		private List<Framework.BaseDatos.ParametrosSQL> _SqlParametros;
		#endregion
		#region Propiedades
		#endregion
		#region Contructor
		#endregion
		#region Metodos/Funciones
		public void GrabaLog()
		{
			StringBuilder lsQuery = new StringBuilder();
			string lsFechaSeleccion, lsFechafin, lsFechaRecibo;
			if (this.fecha_hora_seleccion == null)
				lsFechaSeleccion = "";
			else
				lsFechaSeleccion = this.fecha_hora_seleccion.Value.ToShortDateString();

			if (this.fecha_hora_fin == null)
				lsFechafin = "";
			else
				lsFechafin = this.fecha_hora_fin.Value.ToShortDateString();
			if (string.IsNullOrEmpty(autoriza_escolar))
				autoriza_escolar = "";

			lsQuery.Append("INSERT INTO avisos_reinscripcion_log");
			lsQuery.Append(" (periodo, no_de_control");
			lsQuery.Append(",[creditos_autorizados],[fecha_hora_seleccion],[fecha_hora_fin],[autoriza_escolar]");
			lsQuery.Append(",[fecha_hora_modificacion],[quien_modifica]");
			lsQuery.Append(",[recibo_pago])");
			lsQuery.Append(" VALUES ");
			lsQuery.AppendFormat("('{0}','{1}',{2}", this.periodo,this.no_de_control,this.creditos_autorizados);
			lsQuery.AppendFormat(",'{0}','{1}','{2}'",lsFechaSeleccion, lsFechafin, this.autoriza_escolar);
			lsQuery.AppendFormat(",'{0}','{1}','{2}')",DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), _oSistema.Sesion.Usuario.Usuario,this.recibo_pago);
			_oSistema.Conexion.EjecutaComando(lsQuery);
		}
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

		public DataTable RegresaBitacoraAvisos(string psPeriodo, string psControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			_SqlParametros = new List<Framework.BaseDatos.ParametrosSQL>();
			_SqlParametros.Add(new Framework.BaseDatos.ParametrosSQL("@periodo", psPeriodo));
			_SqlParametros.Add(new Framework.BaseDatos.ParametrosSQL("@control", psControl));
			lsConsulta.Append("SELECT");
			lsConsulta.Append("(SELECT apellido_paterno + ' ' + apellido_materno +' '+ nombre_alumno from alumnos where no_de_control = A.no_de_control) nombre");
			lsConsulta.Append(" ,[creditos_autorizados],[fecha_hora_seleccion],[fecha_hora_fin],[autoriza_escolar]");
			lsConsulta.Append(",[fecha_hora_modificacion],[quien_modifica]");
			lsConsulta.Append(" FROM avisos_reinscripcion_log A");
			lsConsulta.Append(" WHERE periodo = @periodo and no_de_control = @control");
			lsConsulta.Append(" order by fecha_hora_modificacion desc");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta, _SqlParametros);
		}

		#endregion

	}
}
