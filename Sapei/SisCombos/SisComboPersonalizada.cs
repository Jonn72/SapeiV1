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
	/// Clase sisCombo generada automáticamente desde el Generador de Código SII
	/// </summary>
     public partial class SisCombo
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
          #region Consultas
          public DataTable CargarValoresPorCombo(string psCombo, string psComboPadre = null, string psValorPadre = null)
          {
               StringBuilder lsConsulta;
               DataTable loDt;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select [valor],[descripcion]");
               if(!string.IsNullOrEmpty(psComboPadre))
               {
                    lsConsulta.Append(",valor_padre,");
                    lsConsulta.AppendFormat("(select descripcion from sisCombos where combo = '{0}' and valor = S.valor_padre) descripcion_padre", psComboPadre);
               }                     
               lsConsulta.AppendFormat(" From {0} S",RutaTabla);
               lsConsulta.AppendFormat(" where combo = '{0}'", psCombo);
               if (!string.IsNullOrEmpty(psComboPadre) && !string.IsNullOrEmpty(psValorPadre))
               {
                    lsConsulta.AppendFormat(" and '{0}' = (select valor_padre from sisCombos where combo = '{1}' and valor = S.valor_padre)", psValorPadre, psComboPadre);
               }
               else if (!string.IsNullOrEmpty(psValorPadre))
               {
                    lsConsulta.AppendFormat(" and valor_padre = '{0}'", psValorPadre);
               }
               loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
               if (loDt.Rows.Count == 0)
                    return null;
               return loDt;
          }
          public void GuardaActualizaSisCombos()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               //nuevo registro
               if (string.IsNullOrEmpty(valor))
               {
                    lsConsulta.AppendFormat("INSERT INTO {0} ([combo],[valor],[descripcion],[valor_padre])", RutaTabla);
                    lsConsulta.AppendFormat("VALUES ('{0}',(  SELECT max(isnull(convert(int, valor),0)+1) FROM {1} where combo = '{0}'),'{2}','{3}')", combo, RutaTabla, descripcion, valor_padre);
               }
               else
               {
                    //Se valida el valor, sino existe se crea nuevo registro
                    lsConsulta.AppendFormat("IF EXISTS(SELECT 1 FROM sisCombos WHERE combo = '{0}' AND valor = '{1}')", combo, valor);
                    lsConsulta.AppendFormat(" UPDATE sisCombos SET descripcion = '{0}' WHERE combo = '{1}' AND valor = '{2}'", descripcion, combo, valor);
                    lsConsulta.Append(" ELSE ");
                    lsConsulta.AppendFormat(" INSERT INTO sisCombos (combo, valor, descripcion, valor_padre) VALUES ('{0}','{1}','{2}','{3}')", combo, valor, descripcion, valor_padre);
               }
               _oSistema.Conexion.EjecutaComando(lsConsulta);
          }
          #endregion
     }
	}
