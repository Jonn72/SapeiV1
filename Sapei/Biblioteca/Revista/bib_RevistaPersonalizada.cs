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
	/// Clase bib_Revista generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Bib_Revistas
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

        #region Metodos
        public DataTable RegresaRevistas() {
   
            string lsEditar = "<a class=\"btn btn-info btn-xs\" href=\"#frmRevistas class=\"fa fa-pencil\"></i> Editar </a> ";
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("SELECT id_revista,titulo, secciones, fecha_p,id_mat_bib,edicion,");
            lsQuery.AppendFormat("'{0}' editar", lsEditar);
            lsQuery.Append(" FROM bib_vrevistas where baja=0");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public int IdentidificadorRevista(int piIdMat)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select id_revista");
            lsQuery.AppendFormat(" FROM {0}", RutaTabla);
            lsQuery.AppendFormat(" WHERE id_mat_bib= {0} ", piIdMat);
            DataTable tabla = _oSistema.Conexion.RegresaDataTable(lsQuery);
            int id_mat_bib = (from DataRow dr in tabla.Rows select (int)dr["id_revista"]).FirstOrDefault();
            return id_mat_bib;
        }
        public DataTable RegresaRevista(int idMaterial)
        {

            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("SELECT [id_revista],[secciones],[fecha_p],edicion");
            lsQuery.AppendFormat(" FROM {0}",NombreTabla);
            lsQuery.AppendFormat(" WHERE id_mat_bib={0}",idMaterial);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        #endregion
    }

}

