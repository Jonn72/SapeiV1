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
	/// Clase bib_CD generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Bib_CDs
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

        #region CD
        public DataTable RegresarCDs()
        {
            string editar = "<a class=\"btn btn-info btn-xs\" href=\"#frmLibros\" ><i class=\"fa fa-pencil\"></i> Editar </a> ";
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select  [id_cds],[titulo],[descripcion],[duracion],id_mat_bib,");
            lsQuery.Append("'" + editar + "' editar");
            lsQuery.AppendFormat(" FROM bib_vcds where baja=0");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public DataTable RegresarCD(int idMaterial)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select  id_cds,descripcion,duracion");
            lsQuery.AppendFormat(" FROM {0}",NombreTabla);
            lsQuery.AppendFormat(" WHERE id_mat_bib={0}", idMaterial);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public int IdentidificadorCds(int piIdMat)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select id_cds ");
            lsQuery.AppendFormat("FROM {0}", NombreTabla);
            lsQuery.AppendFormat(" WHERE id_mat_bib ={0}", piIdMat);
            DataTable tabla = _oSistema.Conexion.RegresaDataTable(lsQuery);
            int id_mat_bib = (from DataRow dr in tabla.Rows select (int)dr["id_cds"]).FirstOrDefault();
            return id_mat_bib;

        }
        public DataTable RegresaCDs(int id_Mat_bib)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select TOP 1 id_cds, duracion, descripcion");
            lsQuery.AppendFormat(" FROM {0} ", NombreTabla);
            lsQuery.AppendFormat(" WHERE id_mat_bib={0}", id_Mat_bib);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        #endregion
    }
}
