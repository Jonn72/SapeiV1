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
	/// Clase organigrama generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class organigrama
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

        public DataTable RegresaOrganigrama()
        {
            StringBuilder lsConsulta;
            DataTable loDt;
            lsConsulta = new StringBuilder();
            lsConsulta.Append("select clave_area, descripcion_area, tipo_area ");
            lsConsulta.Append("from organigrama ");
            loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
            return loDt;
        }

        public DataTable RegresaJefes()
        {
            StringBuilder lsConsulta;
            DataTable loDt;
            lsConsulta = new StringBuilder();
            lsConsulta.Append("select clave_area, descripcion_area, rfc, jefe_area ");
            lsConsulta.Append("from jefes ");
            loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
            return loDt;
        }



    }
}
