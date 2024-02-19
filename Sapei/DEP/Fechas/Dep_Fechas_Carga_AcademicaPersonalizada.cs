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
using Sapei.Framework.Utilerias;

namespace Sapei
{
	/// <summary>
	/// Clase dep_fecha generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Dep_Fechas_Carga_Academica
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
		#region Personalizados
		public DataTable RegresaTablaPeriodos()
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" SELECT ");

			lsConsulta.Append("(select identificacion_corta from periodos_escolares where periodo = P.periodo) periodo,");
			lsConsulta.Append("[ini_carga_academica],[fin_carga_academica]");
			lsConsulta.AppendFormat(" FROM {0} P", RutaTabla);
			lsConsulta.Append(" order by periodo desc");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		#endregion

		#region FiEl
		public DataTable RegresaTablaCargasAcademicasFiEl(string psPeriodo)
		{
			DataTable ldtTabla = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			DataTable loDt = new DataTable();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
				loParametros.Add(new ParametrosSQL("@tipo_usuario", _oSistema.Sesion.Usuario.RolUsuario.ToString()));
				ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_fiel_cargas_academicas", loParametros));
			}
			return ldtTabla;
		}

		public void RegistraCadenaFiEl(enmTiposDocumentos penmTipo)
		{
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

			loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActual));
			loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));
			loParametros.Add(new ParametrosSQL("@tipo_documento", penmTipo.ToString()));

			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pai_fiel_registra_dep", loParametros);

		}
		#endregion
	}
}
