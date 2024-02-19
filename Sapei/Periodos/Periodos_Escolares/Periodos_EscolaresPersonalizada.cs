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
	/// Clase periodos_escolare generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Periodos_Escolares
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

          public DataTable RegresaComboPeriodo(int piTop = 0, bool pbVeranos = false)
          {
               StringBuilder lsConsulta;
               DataTable loDt;
               lsConsulta = new StringBuilder();
               if (piTop == 0)
                    lsConsulta.Append(" SELECT periodo, identificacion_corta");
               else
                    lsConsulta.AppendFormat(" SELECT TOP ({0}) periodo, identificacion_corta",piTop);
               lsConsulta.AppendFormat(" from {0}", RutaTabla);
               if (!pbVeranos)
                    lsConsulta.Append(" where SUBSTRING(periodo,5,1) <> 2");
               lsConsulta.Append(" order by periodo desc ");
               loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
               if (loDt.Rows.Count == 0)
                    return null;
               return loDt;
          }
		#endregion

		}
	}
