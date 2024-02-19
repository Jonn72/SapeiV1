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
	/// Clase extra_actividades_fecha generada automáticamente desde el Generador de Código SII
	/// </summary>
     public partial class Tutorias_Periodos
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
          #region Consulta
          public DataTable RegresaTablaPeriodos(string psPeriodo = null)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT periodo, inicio_grupos, fin_grupos, inicio_seleccion, fin_seleccion, inicio_captura, fin_captura ");
               lsConsulta.AppendFormat(" FROM {0}", RutaTabla);
               if (!string.IsNullOrEmpty(psPeriodo))
                    lsConsulta.AppendFormat(" WHERE periodo = '{0}'",psPeriodo);
               else
                    lsConsulta.Append(" order by periodo desc");
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public string ValidaPeriodoImpresionGruposInscritos(string psPeriodo, string psDescripcion)
          {
               DataTable loTabla;
               DateTime loFechaFin;
               loTabla = RegresaTablaPeriodos(psPeriodo);
               if (loTabla.Rows.Count == 0)
               {
                    return string.Format("Aún no hay fechas registradas para el periodo: {0}.", psDescripcion);
               }
               loFechaFin = loTabla.Rows[0].Field<DateTime>("fin_seleccion");
               if (Sapei.Framework.Utilerias.ManejoFechas.EsMayorQue(loFechaFin, DateTime.Now))
                    return null;
               return string.Format("La fecha para imprimir lista de grupos del periodo {0} es despúes del {1}", psDescripcion, loFechaFin.Date.ToString());
          }
          public string ValidaPeriodoCapturaCalificacion(string psPeriodo, string psDescripcion)
          {
               DataTable loTabla;
               string lsFechaIni, lsFechaFin;
               loTabla = RegresaTablaPeriodos(psPeriodo);
               if (loTabla.Rows.Count == 0)
               {
                    return string.Format("Aún no hay fechas registradas para el periodo: {0}, consulte con la DEP", psDescripcion);
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
