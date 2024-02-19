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

namespace Sapei
{
	/// <summary>
	/// Clase bib_Adeudo generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Bib_Adeudos
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

        public DataTable RegresaAdeudos(string usuario) {
            StringBuilder lsQuery = new StringBuilder();
            //EL USUARIO DEBE estar VERIFICADO
            VerificaAdeudos(usuario);//genera adeudos si el alumno no ha regresado un libro
            //string pago = "<a class=\"btn btn-info btn-xs\" href=\"#frmAutores\" ><i class=\"fa fa-pencil\"></i> Liquidar </a> ";
            lsQuery.Append("Select  id_adeudo,id_prestamo,usuario, f_entrega, f_limite, monto, liquidado, d_retardo");
            //lsQuery.Append("'" + pago + "' Editar");
            lsQuery.AppendFormat(" FROM bib_vadeudos", RutaTabla);
            lsQuery.AppendFormat(" WHERE  usuario={0} AND liquidado=0", usuario);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

        //financieros
        public DataTable RegresaAdeudosPeriodo(string periodo)
        {
            VerificaAdeudos("ALL");//genera adeudos de alumnos que no han regresado un libro

            //delimitar por fecha
            StringBuilder lsQuery = new StringBuilder();
            string loAcciones = "<a class=\"btn btn-info btn-xs\" href=\"#frmAutores\" ><i class=\"fa fa-pencil\"></i>Acciones</a> ";
            lsQuery.Append("Select  id_adeudo,id_prestamo,usuario,f_entrega, f_limite, monto, liquidado, d_retardo,");
            lsQuery.AppendFormat("'{0}' editar",loAcciones);
            lsQuery.AppendFormat(" FROM bib_vadeudos where liquidado=0 ", RutaTabla);
            lsQuery.AppendFormat(" AND periodo='{0}'",periodo);
            
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public DataTable RegresaAdeudosUsuario(String usuario,DateTime f_inicio,DateTime f_fin)
        {
            VerificaAdeudos(usuario);//genera adeudos si el alumno no ha regresado un libro
            //delimitar por fecha
            StringBuilder lsQuery = new StringBuilder();
            string loAcciones = "<a class=\"btn btn-info btn-xs\" href=\"#frmAutores\" ><i class=\"fa fa-pencil\"></i>Acciones</a> ";
            lsQuery.Append("Select  id_adeudo,id_prestamo,usuario,f_entrega, f_limite, monto, liquidado, d_retardo,");
            lsQuery.AppendFormat("'{0}' editar", loAcciones); 
            lsQuery.AppendFormat(" FROM bib_vadeudos where liquidado=0 and usuario={0} ",usuario,f_inicio,f_fin);
            if(f_inicio!=DateTime.MinValue)
                lsQuery.AppendFormat("  AND f_limite between {1} and {2}",f_inicio, f_fin);

            //if(estado.Length==1)
            //    lsQuery.AppendFormat(" and estado={0}",estado);

            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

        //mandar 'ALL' como usuario para verificar los prestamos 
        // de todos los usuarios que no han entregado un libro.
        public void VerificaAdeudos(string usuario)
        {
            List<ParametrosSQL> parametros = new List<ParametrosSQL>();
            ParametrosSQL sql = new ParametrosSQL("@usuario", usuario, SqlDbType.VarChar);
            parametros.Add(sql);
            _oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("dbo.pac_verifica_prestamo", parametros);

        }

    }

}
