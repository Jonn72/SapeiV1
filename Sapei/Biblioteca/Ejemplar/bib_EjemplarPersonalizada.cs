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
	/// Clase bib_Ejemplar generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Bib_Ejemplares
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
        #region Ejemplar
        public DataTable RegresaEjemplar()
        {

            string eliminar = "<a class=\"btn btn-danger btn-xs\" ><i class=\"fa fa-trash-o\"></i> Eliminar </a>";
            StringBuilder lsQuery = new StringBuilder();

            lsQuery.Append("SELECT bib_ejemplar.id_ejemplar,bib_ejemplar.reserva"+
                ",bib_materialbibliografico.titulo, ");
            lsQuery.Append(" CASE bib_materialbibliografico.tipo_material");
            lsQuery.Append(" WHEN 0 THEN 'Memorias'");
            lsQuery.Append(" WHEN 1 THEN 'Libros'");
            lsQuery.Append(" WHEN 2 THEN 'CD'" );
            lsQuery.Append(" WHEN 3 THEN 'Revistas'");
            lsQuery.Append(" WHEN 4 THEN 'Tesis'");
            lsQuery.Append(" END Tipo, ");
            lsQuery.Append(" '" + eliminar + "' Eliminar ");
            lsQuery.Append(" FROM bib_ejemplar inner join bib_materialbibliografico on");
            lsQuery.Append(" bib_ejemplar.id_mat_bib = bib_materialbibliografico.id_mat_bib where bib_ejemplar.baja=0");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);

        }
        public DataTable RegresaEjemplar(string psEjemplar)
        {

            StringBuilder lsQuery = new StringBuilder();

            lsQuery.Append("SELECT bib_ejemplar.id_ejemplar" +
                ",bib_materialbibliografico.titulo,bib_ejemplar.reserva,");
            lsQuery.Append(" CASE bib_materialbibliografico.tipo_material");
            lsQuery.Append(" WHEN 0 THEN 'Memoria'");
            lsQuery.Append(" WHEN 1 THEN 'Libro'");
            lsQuery.Append(" WHEN 2 THEN 'CD'");
            lsQuery.Append(" WHEN 3 THEN 'Revista'");
            lsQuery.Append(" WHEN 4 THEN 'Tesis'");
            lsQuery.Append(" END Tipo ");
            lsQuery.Append(" FROM bib_ejemplar inner join bib_materialbibliografico on");
            lsQuery.Append(" bib_ejemplar.id_mat_bib = bib_materialbibliografico.id_mat_bib");
            lsQuery.AppendFormat(" where bib_ejemplar.id_ejemplar='{0}'  and bib_materialbibliografico.baja=0", psEjemplar);

            return _oSistema.Conexion.RegresaDataTable(lsQuery);

        }
        public DataTable RegresaEjemplares()
        {

            StringBuilder lsQuery = new StringBuilder();

            lsQuery.Append("SELECT id_ejemplar as clave,titulo ");
            lsQuery.Append(" FROM bib_vejemplar ");
            lsQuery.Append(" WHERE baja=0");

            return _oSistema.Conexion.RegresaDataTable(lsQuery);

        }
        public bool EjemplarDisponible(string psEjemplar) {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select count(id_prestamo) from bib_prestamo");
            lsQuery.AppendFormat(" where id_ejemplar='{0}' and f_entrega is NULL",psEjemplar);
            int loPrestamo=(int)_oSistema.Conexion.EjecutaEscalar(lsQuery);
            return !(loPrestamo>0);//el ejemplar no se encuentra disponible
        }

        #endregion
    }
}
