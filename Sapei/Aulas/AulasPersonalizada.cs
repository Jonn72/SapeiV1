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
	/// Clase aula generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Aulas
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

		public DataTable RegresaDatosAula(string psAula)
		{
			DataTable ldtTabla = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@aula", psAula));
				ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_datos_aula", loParametros));
			}
			return ldtTabla;
		}

		public DataTable RegresaDatosHorarioAula(string psPeriodo, string psMateria, string psGrupo, string psAula)
		{
			DataTable ldtTabla = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
				loParametros.Add(new ParametrosSQL("@materia", psMateria));
				loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
				loParametros.Add(new ParametrosSQL("@aula", psAula));
				ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_datos_aula_horario", loParametros));
			}
			return ldtTabla;
		}

		public DataTable RegresaDatosGrupoAula(string psPeriodo, string psMateria, string psGrupo, string psAula)
		{
			DataTable ldtTabla = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
				loParametros.Add(new ParametrosSQL("@materia", psMateria));
				loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
				loParametros.Add(new ParametrosSQL("@aula", psAula));
				ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_dep_datos_aula_horario_grupo", loParametros));
			}
			return ldtTabla;
		}

	}
	}
