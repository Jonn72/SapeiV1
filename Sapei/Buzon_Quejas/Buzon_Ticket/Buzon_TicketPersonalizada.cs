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
	/// Clase buzon_ticket generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Buzon_Ticket
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
        #region Consultas

        public string inserta_ticket(string psAsunto, string psNControl)
        {
            StringBuilder lsInserta;

            lsInserta = new StringBuilder();
            lsInserta.Append("insert into buzon_ticket ");
            lsInserta.Append("OUTPUT Inserted.ticket ");
            lsInserta.Append("values");
            lsInserta.AppendFormat("(GETDATE(), 1, '{0}', '{1}')",psAsunto, psNControl);
            string hola = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsInserta));


            return hola;

        }

        public DataTable carga_lista_ticket()
        {
            StringBuilder lsConsulta;

            lsConsulta = new StringBuilder();
            lsConsulta.Append("select ticket, (select nombre_alumno from alumnos where no_de_control = e.quien_apertura) nalumno, ");
            lsConsulta.Append("(select nombre_alumno + ' ' + apellido_paterno + ' ' + apellido_materno from alumnos where no_de_control = e.quien_apertura) nalumno_comp, ");
            lsConsulta.Append(" quien_apertura, convert(varchar(40), fecha_apertura, 22) fecha_apertura, titulo, case when status_ticket = 0 then 'False' when status_ticket = 1 then 'True' end status_ticket ");
            lsConsulta.AppendFormat("from {0} e order by ticket desc", "buzon_ticket");
            DataTable loDT;

            loDT = _oSistema.Conexion.RegresaDataTable(lsConsulta);

            return loDT;
        }

        public DataTable carga_lista_ticket_estudiante(string psQuienApertura)
        {
            StringBuilder lsConsulta;

            lsConsulta = new StringBuilder();
            lsConsulta.Append("select ticket ");
            lsConsulta.AppendFormat("from {0} ", "buzon_ticket");
            lsConsulta.AppendFormat("where quien_apertura = '{0}'", psQuienApertura);
            DataTable loDT;

            loDT = _oSistema.Conexion.RegresaDataTable(lsConsulta);

            return loDT;
        }

        public DataTable tablaticket(string psQuienApertura) {
            StringBuilder lsTabla;
            lsTabla = new StringBuilder();
            lsTabla.Append("select fecha_apertura, titulo, case status_ticket when 1 then 'Abierto' else 'Cerrado' end as status_ticket, ");
            lsTabla.Append("convert(varchar(40),(select top 1 fecha from buzon_mensaje where ticket = e.ticket order by fecha desc),0) actualizacion, ");
            lsTabla.Append("'<button type=\"button\"  onclick =\"cambia_contenido('''+convert(varchar(1000),ticket)+ ''', '''+");
            lsTabla.Append("(select nombre_alumno + ' ' + apellido_paterno + ' ' + apellido_materno from alumnos where no_de_control = e.quien_apertura)");
            lsTabla.Append("+''', '''+quien_apertura+''', '''+convert(varchar(40), fecha_apertura, 0)+ ''', '''+titulo+''', '''+convert(varchar(1),status_ticket)+''');\" class=\"btn btn-success\"><i class=\"fa fa-eye\"></i></button>' boton ");
            lsTabla.AppendFormat("from {0} e ", "buzon_ticket");
            lsTabla.AppendFormat("where e.quien_apertura = '{0}'", psQuienApertura);
            DataTable loDT;
            loDT = _oSistema.Conexion.RegresaDataTable(lsTabla);
            return loDT;
        }

        public void cierreticket(int psTicket) {

            StringBuilder lsCierra;
            lsCierra = new StringBuilder();
            lsCierra.Append("update buzon_ticket set status_ticket = 0 ");
            lsCierra.AppendFormat("where ticket = {0}", psTicket);
            _oSistema.Conexion.EjecutaComando(lsCierra);

        }
        #endregion
	}
}
