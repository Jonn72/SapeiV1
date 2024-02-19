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
	/// Clase bib_Libro generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Bib_Libros
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
        public DataTable RegresaLibros()
        {
            string lsEditar = "<a class=\"btn btn-info btn-xs\" href=\"#frmLibros\" ><i class=\"fa fa-pencil\"></i> Editar </a> ";
            //string eliminar = "<a class=\"btn btn-danger btn-xs\" ><i class=\"fa fa-trash-o\"></i> Eliminar </a>";
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select id_libro, isbn, no_paginas, capitulos, titulo,id_mat_bib,edicion,");
            lsQuery.AppendFormat("'{0}' editar", lsEditar);
            lsQuery.Append(" FROM bib_vlibros where baja=0");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

       public int IdentidificadorLibro(int piIdMat)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select id_libro ");
            lsQuery.AppendFormat("FROM {0}",RutaTabla);
            lsQuery.AppendFormat(" WHERE id_mat_bib ={0}", piIdMat);
            DataTable tabla = _oSistema.Conexion.RegresaDataTable(lsQuery);
            int id_mat_bib = (from DataRow dr in tabla.Rows select (int)dr["id_libro"]).FirstOrDefault();
            return id_mat_bib;

        }
        public DataTable RegresaLibro(int id_Mat_bib)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select TOP 1 id_libro, isbn, no_paginas, capitulos,edicion,clasificacion");
            lsQuery.AppendFormat(" FROM {0} ",RutaTabla);
            lsQuery.AppendFormat(" WHERE id_mat_bib={0}",id_Mat_bib);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

        #endregion
    }
	}
