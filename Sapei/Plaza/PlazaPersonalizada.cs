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
	/// Clase plaza generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Plaza
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

		public DataTable RegresaUnidades()
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" select distinct (unidad) from unidades ");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
		}

		public DataTable RegresaSubUnidad(string PsSub)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			_oSqlParametros = new List<ParametrosSQL>();
			_oSqlParametros.Add(new ParametrosSQL("@sub", PsSub));
			lsConsulta.Append(" select subunidad, '' + subunidad as sub from unidades where unidad = @sub ");
			DataTable loDT;
			loDT = _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
			return loDT;
		}

		public DataTable RegresaCategorias()
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" select distinct categoria,descripcion_categoria from categorias ");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
		}


		public DataTable RegresaPlazas()
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" select unidad, subunidad, categoria, horas, diagonal, partida, estatus_plaza, efectos_iniciales_plaza, efectos_finales_plaza from plazas order by categoria, unidad,subunidad,horas,diagonal,partida ");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
		}
		

	}
	}
