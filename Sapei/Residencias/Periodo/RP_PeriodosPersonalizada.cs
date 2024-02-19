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
	/// Clase rp_periodo generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class RP_Periodos
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
        public DataTable PeriodosRP()
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("select periodo, (select identificacion_corta from periodos_escolares where periodo = P.periodo), ");
            lsQuery.Append(" fecha_inicio, fecha_fin,primerS16,segundoS16,tercerS16,primerS24,segundoS24,tercerS24, nombre, url");
            lsQuery.AppendFormat(" from {0} P", RutaTabla);
            lsQuery.Append(" order by periodo desc");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
		#endregion

		}
	}
