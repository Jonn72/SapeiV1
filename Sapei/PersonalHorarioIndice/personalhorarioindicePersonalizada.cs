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
	/// Clase personal_horario_indice generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class PersonalHorarioIndice
	{
		#region variables
		List<ParametrosSQL> _oSqlParametros;
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

		#region Recontratacion

		public DataTable VerificaPeriodoActual()
		{
			StringBuilder lsConsulta;
			DataTable loDt;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" select top(1) periodo from personal_horario_indice order by periodo desc ");
			loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
			return loDt;
		}

		public DataTable ResetPersonalHorariosIndice()
		{
			StringBuilder lsConsulta;
			DataTable loDt;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" DBCC CHECKIDENT ('personal_horario_indice', RESEED, 0) ");
			loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
			return loDt;
		}
        #endregion
    }
}
