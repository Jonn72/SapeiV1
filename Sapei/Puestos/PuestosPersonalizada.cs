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
	/// Clase puesto generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Puestos
	{
		#region variables
		private List<ParametrosSQL> _oSqlParametros;
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


		public DataTable RegresaPuesto(string psRFC)
		{
			_oSqlParametros = new List<ParametrosSQL>();
			_oSqlParametros.Add(new ParametrosSQL("@rfc", psRFC));
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" select clave_puesto, descripcion_puesto from puestos  ");
			lsConsulta.Append(" where clave_puesto not in (select clave_puesto from personal_puestos where rfc = @rfc and fecha_termino_puesto is null)  ");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
		}

		public DataTable RegresaListaPuestos()
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" select id, concepto ");
			lsConsulta.Append(" from rf_tabulador order by concepto asc ");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

	}
	}
