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
	/// Clase buzon_mensaje generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Buzon_Mensaje
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

        public void inserta_mensaje(int psTicket,string psMensaje,Enum psTipoUsuario)
        {
            StringBuilder lsInserta;
            lsInserta = new StringBuilder();
            lsInserta.Append("insert into buzon_mensaje values");
            int rol = 0;
            if(Convert.ToString(psTipoUsuario) == "ESTUDIANTE"){
                rol = 1;
            }
            lsInserta.AppendFormat("({0},GETDATE(),{1},'{2}',0)",psTicket,rol,psMensaje);
            _oSistema.Conexion.EjecutaComando(lsInserta);

        }

        public void respuesta_mensaje(int psTicket)
        {
            StringBuilder lsActualiza;
            lsActualiza = new StringBuilder();
            lsActualiza.Append("update buzon_mensaje set status_mensaje = 1 ");
            lsActualiza.AppendFormat("where ticket = {0}", psTicket);
            _oSistema.Conexion.EjecutaComando(lsActualiza);
        }

        public DataTable CargaListaMensajes()
        {
            StringBuilder lsCarga;
            lsCarga = new StringBuilder();
            lsCarga.AppendFormat("select ticket, rol, mensaje, status_mensaje, fecha from {0} e", "buzon_mensaje");
            DataTable loDT;

            loDT = _oSistema.Conexion.RegresaDataTable(lsCarga);

            return loDT;
        }

        #endregion
    }
}
