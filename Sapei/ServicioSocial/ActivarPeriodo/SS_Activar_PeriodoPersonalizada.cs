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
	/// Clase ss_activar_periodo generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class SS_Activar_Periodo
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
        public DataTable PeriodosSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("select periodo,(select identificacion_corta from periodos_escolares where periodo = P.periodo),fecha_inicio, fecha_fin, fecha_cierre_registro, nombre, url, fecha_bimestre_1,fecha_bimestre_2,fecha_bimestre_3 ");
            lsQuery.AppendFormat("from {0} P", RutaTabla);
            lsQuery.Append(" order by periodo desc ");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public DataTable ConsultaCurso(string psPeriodo)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append(" select nombre, url");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where periodo = '{0}'", psPeriodo);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public DataTable RegresaPeriodoSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append(" select max(periodo) as periodo");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
		#endregion

		}
	}
