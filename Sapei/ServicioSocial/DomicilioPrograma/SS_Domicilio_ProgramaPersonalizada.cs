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
	/// Clase rp_domicilio generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class SS_Domicilio_Programa
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
        public DataTable RegresaIdCodPostal(string psColonia)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" Select id ");
            lsQuery.AppendFormat(" from c_p where colonia = '{0}' ", psColonia);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable DomicilioSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append( "Select rfc_domicilio, domicilio, numero, ");
            lsQuery.AppendFormat(" from {0} ", RutaTabla);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

		}
	}
