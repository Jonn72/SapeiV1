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
	/// Clase bib_Editorial generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Bib_Editoriales
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
        public DataTable RegresarEditoriales(bool botones=true) {
            
            StringBuilder lsQuery = new StringBuilder();

            if (botones)
            {
                lsQuery.Append("Select [id_editorial],[nombre_editorial],[edicion],");
                string editar = "<a class=\"btn btn-info btn-xs\" ><i class=\"fa fa-pencil\"></i> Editar </a> ";
                string eliminar = "<a class=\"btn btn-danger btn-xs\" ><i class=\"fa fa-trash-o\"></i> Eliminar </a>";
                lsQuery.Append("'" + editar + " " + eliminar + "' Editar");
            }
            else {
                lsQuery.Append("Select [id_editorial] clave,[nombre_editorial] editorial");
            }
            lsQuery.AppendFormat(" FROM {0}", RutaTabla);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        #endregion
    }
}
