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
	/// Clase aspirantes_periodo generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Aspirantes_Periodos
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
          #region Personzalidos
          public bool EsProcesoActivo()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT distinct 1 ");
               lsConsulta.AppendFormat(" FROM {0}", RutaTabla);
               lsConsulta.Append(" where GETDATE() between ini_registro and fin_registro");
               if (string.IsNullOrEmpty(Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsConsulta))))
                    return false;
               return true;
          }
          public DataTable RegresaTablaPeriodos()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT periodo, vuelta, ini_registro, fin_registro, fecha_examen, activo ");
               lsConsulta.AppendFormat(" FROM {0}", RutaTabla);
               lsConsulta.Append(" order by periodo desc, vuelta desc");
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }

          public string RegresaPeriodoActivo()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT  max(periodo) ");
               lsConsulta.AppendFormat(" FROM {0}", RutaTabla);
               return Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsConsulta));
          }
          public DateTime RegresaFechaHoraExamen()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT top 1 fecha_examen");
               lsConsulta.AppendFormat(" FROM {0}", RutaTabla);
               lsConsulta.Append(" where GETDATE() between ini_registro and fin_registro order by periodo asc, vuelta asc");
               return Convert.ToDateTime(_oSistema.Conexion.EjecutaEscalar(lsConsulta));
          }
          #endregion
     }
	}
