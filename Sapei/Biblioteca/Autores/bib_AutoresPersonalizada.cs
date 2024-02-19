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
	/// Clase bib_Autore generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Bib_Autores
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
        public DataTable RegresarAutores(bool boton=true)
        {
            StringBuilder lsQuery = new StringBuilder();
            if (boton)
            {
                string lsEditar = "<a class=\"btn btn-info btn-xs\" href=\"#frmAutores\" ><i class=\"fa fa-pencil\"></i> Editar </a> ";
                string lsEliminar = "<a class=\"btn btn-danger btn-xs\" ><i class=\"fa fa-trash-o\"></i> Eliminar </a>";
                lsQuery.Append("Select  [id_autor],[nombre_autor],[apellido_p],[apellido_m],");
                lsQuery.AppendFormat("'{0}{1}' Editar",lsEditar,lsEliminar);
            }
            else {
                lsQuery.Append("Select  [id_autor] clave,[apellido_p]+' '+[apellido_m]+' '+[nombre_autor] autor ");
            }
            lsQuery.AppendFormat(" FROM {0}", RutaTabla);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public string RegresarMaxId()
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select  ISNULL(MAX(id_autor)+1,1)");
            lsQuery.AppendFormat(" FROM {0}", RutaTabla);
            return Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));           
        }
        #endregion
    }
}
