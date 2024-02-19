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
	/// Clase rf_activa_periodo generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class RF_activa_periodo
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

		public DataTable RegresaTablaPeriodos()
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("SELECT (select identificacion_corta from periodos_escolares where periodo = A.periodo) periodo, ini_imprime_ficha,  fin_imprime_ficha, fin_pago, fin_registro_pago_ordinario  ");
			lsConsulta.AppendFormat(" FROM {0} A", RutaTabla);
			lsConsulta.Append(" order by periodo desc");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		#endregion

	}
	}
