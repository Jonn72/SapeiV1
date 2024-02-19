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
	/// Clase servicio generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Servicio
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

		public DataTable ComboServicios()
		{
			StringBuilder lsQuery = new StringBuilder();
			lsQuery.Append("SELECT clave,concepto,monto,dias_vigencia");
			lsQuery.AppendFormat(" FROM {0} A", RutaTabla);
			lsQuery.Append(" WHERE activo = 1");
			return _oSistema.Conexion.RegresaDataTable(lsQuery);
		}
		#endregion

	}
	}
