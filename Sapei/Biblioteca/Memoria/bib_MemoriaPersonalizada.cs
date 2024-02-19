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
	/// Clase bib_Memoria generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Bib_Memorias
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
        #region metodos
        public DataTable RegresaMemorias()
        {
            StringBuilder lsQuery = new StringBuilder();

            string editar = "<a class=\"btn btn-info btn-xs\" href=\"#frmMemorias\" ><i class=\"fa fa-pencil\"></i> Editar </a> ";
            //string eliminar = "<a class=\"btn btn-danger btn-xs\" ><i class=\"fa fa-trash-o\"></i> Eliminar </a>";
            lsQuery.Append("Select [id_mem_res],[fecha_publicacion],[lugar_p],[titulo],id_mat_bib,");
            lsQuery.Append("'"+editar+"' editar");
            lsQuery.AppendFormat(" FROM bib_vmemorias where baja=0");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

        public int IdentidificadorMemoria(int piIdMat)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select id_mem_res");
            lsQuery.AppendFormat(" FROM {0}", NombreTabla);
            lsQuery.AppendFormat(" WHERE id_mat_bib ={0}", piIdMat);
            DataTable tabla = _oSistema.Conexion.RegresaDataTable(lsQuery);
            int id_mat_bib = (from DataRow dr in tabla.Rows select (int)dr["id_mem_res"]).FirstOrDefault();
            return id_mat_bib;

        }
        public DataTable RegresaMemoria(int id_Mat_bib)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select TOP 1 id_mem_res, fecha_publicacion, lugar_p");
            lsQuery.AppendFormat(" FROM {0} ", NombreTabla);
            lsQuery.AppendFormat(" WHERE id_mat_bib={0}", id_Mat_bib);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

        #endregion
    }
}
