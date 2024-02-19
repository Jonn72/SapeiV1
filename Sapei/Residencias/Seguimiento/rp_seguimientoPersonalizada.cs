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
	/// Clase rp_seguimiento generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class RP_Seguimiento
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

        public DataTable RegresaValidacionSeguimientoRP(string psControl, string psPeriodo)
        {
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pav_rp_inicio_seguimiento", loParametros));
            }
            return loDt;
        }
        public DataTable AltaCalificacionRP(string lsPeriodo, int piCalExterno, int piCalInterno, string psNoControl, string psFecha, int piSeguimiento)
        {
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@evaluacion_externo", piCalExterno));
                loParametros.Add(new ParametrosSQL("@evaluacion_interno", piCalInterno));
                loParametros.Add(new ParametrosSQL("@fecha_evaluacion", psFecha));
                loParametros.Add(new ParametrosSQL("@numero_seguimiento", piSeguimiento));
                loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rp_captura_seguimiento", loParametros));
            }
            return loDt;
        }
        public DataTable RegresaDatosRP(int psSeguimiento, int psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select no_de_control, evaluacion_interno, evaluacion_externo, fecha_evaluacion ");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where numero_seguimiento = {0} ", psSeguimiento);
            lsQuery.AppendFormat(" and no_de_control = {0}", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable Seguimientos(string psUsuario)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" SELECT '<a  class=\"btn btn-success\"><span class=\"fa fa-check\"></span></a>',");
            lsQuery.Append(" (select nombre+' '+ paterno+' '+ materno from estudiantes_datos_completos where no_de_control = A.no_de_control), A.no_de_control");
            lsQuery.Append(" from rp_estado_solicitud A inner join alumnos B on A.no_de_control = B.no_de_control");
            lsQuery.AppendFormat(" where A.estado >= 10 and B.carrera in (select carrera from sis_permisos_carreras where usuario = '{0}')", psUsuario);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable CargaEstadoInicialSeguimientosRP(string periodo, string no_control, int estado)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append(" INSERT INTO rp_estado_seguimiento (periodo, no_de_control, estado) ");
            lsQuery.AppendFormat(" values ( {0}, {1}, {2} )", periodo, no_control, estado);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public DataTable ActualizaEstadoSeguimientosRP(string psNoControl, int estado)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" UPDATE rp_estado_seguimiento ");
            lsQuery.AppendFormat(" SET estado = {0}", estado);
            lsQuery.AppendFormat(" WHERE no_de_control = {0}", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ConsultaRegistroSeguimientoRP(string psNoControl, int psSeguimiento)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select id ");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where numero_seguimiento = {0} ", psSeguimiento);
            lsQuery.AppendFormat(" and no_de_control = {0}", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ValidacionCartaTermino(string psUsuario)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select case when estado > 11 then '<span class=\"fa fa-check\"></span>' else '<span class=\"fa fa-close\"></span>' end,");
            lsQuery.Append(" (select nombre+' '+ paterno+' '+ materno from estudiantes_datos_completos where no_de_control = A.no_de_control), A.no_de_control");
            lsQuery.Append(" from rp_estado_solicitud A inner join alumnos B on A.no_de_control = B.no_de_control");
            lsQuery.AppendFormat(" where A.estado >= 11 and B.carrera in (select carrera from sis_permisos_carreras where usuario = '{0}')",psUsuario);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
		}
	}
