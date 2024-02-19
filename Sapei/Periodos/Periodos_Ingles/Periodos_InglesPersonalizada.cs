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
	/// Clase periodos_ingle generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Periodos_Ingles
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
		/// <summary>
		/// Regresa periodos registrados
		/// </summary>
		/// <returns></returns>
		public DataTable RegresaTablaPeriodos(string psPeriodo = null)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("SELECT periodo,                                                                            Convert(varchar(10),CONVERT(date,[ini_registro_grupos],106),103) ini_registro_grupos,                      Convert(varchar(10),CONVERT(date,[fin_registro_grupos],106),103) fin_registro_grupos,                       Convert(varchar(10),CONVERT(date,[ini_seleccion],106),103) ini_seleccion,                                   Convert(varchar(10),CONVERT(date,[fin_seleccion],106),103) fin_seleccion,                                   Convert(varchar(10),CONVERT(date,[ini_captura_calif],106),103) ini_captura_calif,                          Convert(varchar(10),CONVERT(date,[fin_captura_calif],106),103) fin_captura_calif ");
			lsConsulta.AppendFormat(" FROM {0}", RutaTabla);
			if (!string.IsNullOrEmpty(psPeriodo))
			{
				lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
			}
			lsConsulta.Append(" order by periodo desc");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public string ValidaPeriodo(string psPeriodo, string psDescripcion)
		{
			DataTable loTabla;
			string lsFechaIni, lsFechaFin;
			loTabla = RegresaTablaPeriodos(psPeriodo);
			if (loTabla.Rows.Count == 0)
			{
				return string.Format("Aún no hay fechas registradas para el periodo: {0}, consulte con la DEP", psDescripcion);
			}
			lsFechaIni = loTabla.Rows[0].Field<string>("ini_registro_grupos");
			lsFechaFin = loTabla.Rows[0].Field<string>("fin_registro_grupos") + " 11:59:59 pm";
			if (Sapei.Framework.Utilerias.ManejoFechas.ToBetween(Convert.ToDateTime(lsFechaIni), Convert.ToDateTime(lsFechaFin)))
				return null;
			return string.Format("La fecha para captura de grupos de inglés del periodo {0} son {1} al {2}", psDescripcion, lsFechaIni, lsFechaFin);
		}
		public string ValidaPeriodoPublicacion(string psPeriodo, string psDescripcion)
		{
			DataTable loTabla;
			loTabla = RegresaTablaPeriodos(psPeriodo);
			if (loTabla.Rows.Count == 0)
			{
				return string.Format("Aún no hay fechas registradas para el periodo: {0}", psDescripcion);
			}
			return null;
		}
		public string ValidaPeriodoSeleccion(string psPeriodo, string psDescripcion)
		{
			DataTable loTabla;
			string lsFechaFin;
			string lsFechaIni;
			loTabla = RegresaTablaPeriodos(psPeriodo);
			if (loTabla.Rows.Count == 0)
			{
				return string.Format("Aún no hay fechas registradas para el periodo: {0}, consulte en CLE", psDescripcion);
			}
			lsFechaIni = loTabla.Rows[0].Field<string>("ini_seleccion");
			lsFechaFin = loTabla.Rows[0].Field<string>("fin_seleccion") + " 11:59:59 pm";
			if (Sapei.Framework.Utilerias.ManejoFechas.ToBetween(Convert.ToDateTime(lsFechaIni), Convert.ToDateTime(lsFechaFin)))
				return null;
			if (Sapei.Framework.Utilerias.ManejoFechas.EsMayorQue(Convert.ToDateTime(lsFechaFin),DateTime.Now))
				return null;
			return string.Format("La fecha registrada para selección de cursos de inglés del periodo {0} son {1} al {2}", psDescripcion, lsFechaIni, lsFechaFin);
		}
		public bool ValidaPeriodoEscolar(string psPeriodo)
		{
			StringBuilder lsConsulta;
			string lsResultado;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("SELECT 1 ");
			lsConsulta.Append(" FROM periodos_escolares");
			lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
			lsResultado = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsConsulta));
			if (string.IsNullOrEmpty(lsResultado))
				return false;
			return true;
		}
		public string ValidaPeriodoImpresionGruposInscritos(string psPeriodo, string psDescripcion)
		{
			DataTable loTabla;
			string lsFechaFin;
			loTabla = RegresaTablaPeriodos(psPeriodo);
			if (loTabla.Rows.Count == 0)
			{
				return string.Format("Aún no hay fechas registradas para el periodo: {0}, consulte con la DEP", psDescripcion);
			}
			lsFechaFin = loTabla.Rows[0].Field<string>("fin_seleccion") + " 11:59:59 pm";
			if (Sapei.Framework.Utilerias.ManejoFechas.EsMayorQue(Convert.ToDateTime(lsFechaFin), DateTime.Now))
				return null;
			return string.Format("La fecha para imprimir lista de grupos de inglés del periodo {0} es despúes del {1}", psDescripcion, lsFechaFin);
		}
		public string ValidaPeriodoCapturaCalificacion(string psPeriodo, string psDescripcion)
		{
			DataTable loTabla;
			string lsFechaIni, lsFechaFin;
			loTabla = RegresaTablaPeriodos(psPeriodo);
			if (loTabla.Rows.Count == 0)
			{
				return string.Format("Aún no hay fechas registradas para el periodo: {0}", psDescripcion);
			}
			lsFechaIni = loTabla.Rows[0].Field<string>("ini_captura_calif");
			lsFechaFin = loTabla.Rows[0].Field<string>("fin_captura_calif") + " 11:59:59 pm";
			if (Sapei.Framework.Utilerias.ManejoFechas.ToBetween(Convert.ToDateTime(lsFechaIni), Convert.ToDateTime(lsFechaFin)))
				return null;
			return string.Format("La fecha para captura de calificaciones de inglés del periodo {0} son {1} al {2}", psDescripcion, lsFechaIni, lsFechaFin);
		}
		#endregion

	}
}
