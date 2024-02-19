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
	/// Clase carrera generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Carreras
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

          public DataTable RegresaComboCarreras(bool pbOpcionTodas = false)
          {
               StringBuilder loQuery = new StringBuilder();
               loQuery.Append("SELECT distinct carrera, nombre_carrera ");
               loQuery.AppendFormat(" FROM {0}", RutaTabla);
			if (pbOpcionTodas)
			{
				loQuery.Append(" UNION ");
				loQuery.Append("SELECT '0' carrera, 'TODAS' nombre_carrera ");
			}
               return _oSistema.Conexion.RegresaDataTable(loQuery);
          }

		#endregion
		#region Reticula
		public DataTable RegresaInformacionCarrera(string psCarreraAbrv)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@carrera", psCarreraAbrv));
			loParametros.Add(new ParametrosSQL("@reticula", ""));
			loParametros.Add(new ParametrosSQL("@materia", ""));
			loParametros.Add(new ParametrosSQL("@creditos_materia", ""));
			loParametros.Add(new ParametrosSQL("@horas_teoricas", ""));
			loParametros.Add(new ParametrosSQL("@horas_practicas", ""));
			loParametros.Add(new ParametrosSQL("@orden_certificado", ""));
			loParametros.Add(new ParametrosSQL("@semestre_reticula", ""));
			loParametros.Add(new ParametrosSQL("@creditos_prerrequisito", ""));
			loParametros.Add(new ParametrosSQL("@especialidad", ""));
			loParametros.Add(new ParametrosSQL("@clave_oficial_materia", ""));
			loParametros.Add(new ParametrosSQL("@estatus_materia", ""));
			loParametros.Add(new ParametrosSQL("@programa_estudios", ""));
			loParametros.Add(new ParametrosSQL("@renglon", ""));
			loParametros.Add(new ParametrosSQL("@bandera", 1));
			using (var loCOnexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_modificar_reticula_carrera", loParametros));
			}
			return loDt;
		}
		#endregion

	}
}
