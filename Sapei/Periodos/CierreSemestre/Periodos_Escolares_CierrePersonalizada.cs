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
	/// Clase periodos_escolares_cierre generada automáticamente desde el Generador de Código Sapei
	/// </summary>
	public partial class Periodos_Escolares_Cierre
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
		public void IniciaProceso()
		{
			string lsServidor;
			System.Threading.Tasks.Task.Run(() =>
			{
				using (var loConexion = new ManejaConexion(_oSistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActual));
					loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));

					_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_procesa_cierre_semestre", loParametros);

				}
			});
		}
		#endregion

		}
	}
