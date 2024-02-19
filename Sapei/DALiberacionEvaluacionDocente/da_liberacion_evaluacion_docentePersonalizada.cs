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
	/// Clase rf_tabulador generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class da_liberacion_evaluacion_docente
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

		public DataTable RegresaListaAlumnosLiberados(string psPeriodo)
		{
			DataTable ldtTabla = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
				ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_lista_liberacion", loParametros));
			}
			return ldtTabla;
		}

		public DataTable RegresaDatosEvaluacionAlumno(string psPeriodo, string psNoControl)
		{
			DataTable ldtTabla = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
				loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
				ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_evalua_liberacion", loParametros));
			}
			return ldtTabla;
		}
	}
	
}
