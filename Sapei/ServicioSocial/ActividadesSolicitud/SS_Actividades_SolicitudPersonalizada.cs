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
	/// Clase ss_actividades_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class SS_Actividades_Solicitud
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

        //REGRESA EL ID DEL PROGRAMA DE LA SOLICITUD CON EL NUMERO DE CONTROL INGRESADO
        public DataTable RegresaDatosSolicitud(string psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" Select a.folio,a.id_programa, a.fecha_inicio,a.fecha_termino,a.fecha_solicitud,a.modalidad,a.turno,a.estado, b.id_tipo_programa ");
            lsQuery.Append(" from ss_solicitud a, ss_programa b");
            lsQuery.AppendFormat(" where no_de_control = ('{0}') ", psNoControl);
            lsQuery.Append(" and a.id_programa = b.id");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable ConsultaIdTipoPrograma(string psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select (select id_tipo_programa from ss_programa where id = P.id_programa) as id_tipo_programa from ss_solicitud P ");
            lsQuery.AppendFormat(" where no_de_control = ('{0}') ", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable ConsultaFolioSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select max (folio) as folio from ss_solicitud");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable ConsultaEstadosSS(string psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select estado from ss_estado_solicitud ");
            lsQuery.AppendFormat(" where no_de_control = '{0}' ", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }


        public DataTable ActualizarEstadoSS(int psEstado, string psNoConotrol)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loEs;
            lsQuery.Append("UPDATE ss_estado_solicitud ");
            lsQuery.AppendFormat("SET estado = {0}", psEstado);
            lsQuery.AppendFormat("WHERE no_de_control= '{0}'", psNoConotrol);
            loEs = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loEs.Rows.Count == 0)
                return null;
            return loEs;
        }

        public DataTable RegresaActividades(string psNoConotrol, string lsPeriodo)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loEs;
            lsQuery.Append(" select value as tipo_actividades from STRING_SPLIT(convert (varchar (1000), (select tipo_actividades from ss_actividades_solicitud inner join ss_solicitud on ss_actividades_solicitud.folio = ss_solicitud.folio  ");
            lsQuery.AppendFormat(" where ss_solicitud.no_de_control = '{0}' and ss_actividades_solicitud.periodo = '{1}' )) , ',')", psNoConotrol, lsPeriodo);
            loEs = _oSistema.Conexion.RegresaDataTable(lsQuery);
            return loEs;
        }
        #endregion

    }
	}
