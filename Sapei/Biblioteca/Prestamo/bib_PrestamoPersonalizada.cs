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
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Configuracion;

namespace Sapei
{
	/// <summary>
	/// Clase bib_Prestamo generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Bib_Prestamos
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
        public DataTable RegresaPrestamos(string periodo)
        {
            StringBuilder lsQuery = new StringBuilder();

            //string editar = "<a class=\"btn btn-info btn-xs\" href=\"#frmMemorias\" ><i class=\"fa fa-pencil\"></i> Devolver </a> ";
            //string eliminar = "<a class=\"btn btn-danger btn-xs\" ><i class=\"fa fa-trash-o\"></i> Eliminar </a>";
            lsQuery.Append("Select  id_prestamo,titulo, f_prestamo, f_limite,f_entrega, usuario,id_mat_bib ");
            //lsQuery.Append(",'" + editar + "' editar");
            lsQuery.Append(" FROM bib_vprestamo");
            lsQuery.AppendFormat(" WHERE periodo='{0}'",periodo);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

        public DataTable VerificaPrestamo(string psUsuario) {


            StringBuilder lsQuery = new StringBuilder();

            string lsEditar = "<a class=\"btn btn-info btn-xs\" href=\"#frmMemorias\" ><i class=\"fa fa-pencil\"></i> Devolver </a> ";
            lsQuery.Append("Select  id_prestamo,titulo, f_prestamo, f_limite, usuario,id_mat_bib, ");
            lsQuery.Append("'" + lsEditar + "' editar");
            lsQuery.Append(" FROM bib_vprestamo ");
            lsQuery.AppendFormat(" WHERE  usuario='{0}' and f_entrega is null", psUsuario);
           
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        //madar all para generar adedudo de todos los usuarios que no han entregado un libro.
        public int VerificaAdeudo(string psUsuario) {
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            ParametrosSQL loSql = new ParametrosSQL("@usuario",psUsuario,SqlDbType.VarChar);
            loParametros.Add(loSql);
            _oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("dbo.pac_verifica_prestamo", loParametros);
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select count(Id_prestamo) ");
            lsQuery.Append(" FROM bib_vadeudos ");
            lsQuery.AppendFormat(" WHERE  usuario='{0}'", psUsuario);
            return (int)_oSistema.Conexion.EjecutaEscalar(lsQuery);
        }
        public int PrestamosPendientes(string usuario) {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select  count(usuario) as pretamos");
            lsQuery.Append(" FROM bib_prestamo ");
            lsQuery.AppendFormat(" WHERE usuario='{0}' and f_entrega is null", usuario);
            return Convert.ToInt32(_oSistema.Conexion.EjecutaEscalar(lsQuery));
        }
        public DataTable RegresaPrestamosUsuario(string psUsuario, enmRolUsuario enmTipoUsuario) {
            //solo se permite ROL ALU y DOC
            //VERIFICAR LA VISTA PARA LOS ROLES EN TIPO
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("SELECT [titulo],[f_prestamo],[f_entrega],[f_limite],[entregado]");

            if (enmTipoUsuario== enmRolUsuario.ALU)
                lsQuery.Append(",[adeudo],[pagado]");

            lsQuery.Append(" FROM bib_vusuario ");
            lsQuery.AppendFormat(" WHERE  usuario='{0}' and tipo='{1}'", psUsuario,enmTipoUsuario);
            lsQuery.Append(" ORDER BY  f_entrega,f_prestamo asc");

            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
     }
}
