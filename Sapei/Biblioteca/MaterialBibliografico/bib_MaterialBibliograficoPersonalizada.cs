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
    /// Clase bib_MaterialBibliografico generada automáticamente desde el Generador de Código SII
    /// </summary>
    /// enum TipoMaterial { Memorias, Libros, Cds, Revistas, Tesis };
    public enum TipoMaterial { Memorias, Libros, Cds, Revistas, Tesis };
    public partial class Bib_MaterialesBibliograficos
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
        public DataTable RegresarMaterial(bool psBoton=true)
        {
            StringBuilder lsQuery = new StringBuilder();
            if (psBoton)
            {
                string lsEditar = "<a class=\"btn btn-info btn-xs\" href=\"#frmMaterial\" ><i class=\"fa fa-pencil\"></i> Editar </a> ";
                string lsEliminar = "<a class=\"btn btn-danger btn-xs\" ><i class=\"fa fa-trash-o\"></i> Eliminar </a>";

                lsQuery.Append("SELECT  id_mat_bib, titulo, " +
                    "existencia, autor, editorial," +
                    " carrera,material = case tipo_material" +
                    "\n WHEN 0 THEN 'Memorias'" +
                    "\n WHEN 1 THEN 'Libro'" +
                    "\n WHEN 2 THEN 'CD'" +
                    "\n WHEN 3 THEN 'Revista'" +
                    "\n WHEN 4 THEN 'Tesis'" +
                    "\n END,  ");
                lsQuery.AppendFormat("'{0}{1}' editar", lsEditar,lsEliminar);
            }
            else {
                lsQuery.Append("SELECT  id_mat_bib clave, titulo");
            }

            lsQuery.Append(" FROM bib_vmaterialbib where baja=0");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

        public int RegresaId() {
            StringBuilder lsQuery = new StringBuilder();
            
               lsQuery.Append("SELECT  max(id_mat_bib) id");
               lsQuery.AppendFormat(" FROM {0}",NombreTabla);
               DataTable tabla= _oSistema.Conexion.RegresaDataTable(lsQuery);
               int id = (from DataRow dr in tabla.Rows
                      select (int)dr["id"]).FirstOrDefault();
            return id;
        }
      
        #endregion


    }
}
