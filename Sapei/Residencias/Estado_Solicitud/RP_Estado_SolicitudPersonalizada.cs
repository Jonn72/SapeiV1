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
	/// Clase rp_domicilio generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class RP_Estado_Solicitud
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
		public void GuardaBitacora(string psPeriodo, string psControl, string psObservaciones)
		{
			StringBuilder lsConsulta = new StringBuilder();
			lsConsulta.Append("INSERT INTO ss_bitacora (periodo,no_de_control,observaciones)");
			lsConsulta.Append(" values");
			lsConsulta.AppendFormat("('{0}','{1}','{2}')", psPeriodo, psControl, psObservaciones);
			_oSistema.Conexion.EjecutaComando(lsConsulta);
		}
        public DataTable AutorizacionAnteproyecto(string usuario)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" SELECT '<a  id=\"btn-valida\" '+ case when estado > 3 then 'class=\"btn btn-default\"' else 'class=\"btn btn-success\"' end +'><span class=\"fa fa-check\"></span></a>',");
            lsQuery.Append(" rp_estado_solicitud.no_de_control,");
            lsQuery.Append(" alumnos.nombre_alumno+' '+ alumnos.apellido_paterno+' '+ alumnos.apellido_materno");
            lsQuery.Append(" from rp_estado_solicitud inner join alumnos on rp_estado_solicitud.no_de_control = alumnos.no_de_control");
            lsQuery.Append(" where rp_estado_solicitud.estado >= 3");
            lsQuery.AppendFormat(" and alumnos.carrera in (select carrera from sis_permisos_carreras where usuario = '{0}')",usuario);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ValidacionCartaAceptacionRP()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select '<a  id=\"btn-valida\" '+ case when estado > 2 then 'class=\"btn btn-default\"' "); 
            lsQuery.Append(" else 'class=\"btn btn-success\"' end +'><span class=\"fa fa-check\"></span></a>',no_de_control," );
            lsQuery.Append(" (select nombre_alumno from alumnos where no_de_control = P.no_de_control)+' '+");
            lsQuery.Append(" (select apellido_paterno from alumnos where no_de_control = P.no_de_control)+'");
            lsQuery.Append(" '+(select apellido_materno from alumnos where no_de_control = P.no_de_control)");
            lsQuery.AppendFormat(" from {0} P where estado >=2", RutaTabla);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ConsultaEstadoRP(string usuario, string lsPeriodo)
        {
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            DataTable loDt = new DataTable();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@nombre_usuario", usuario));
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rp_estado_proceso", loParametros));
            }
            return loDt;
        }
        public DataTable ConsultaEstadoStepRP(string psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select estado ");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where no_de_control = {0} ", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable CargaCierre()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select (select identificacion_corta from periodos_escolares where periodo = P.periodo), no_de_control,");
            lsQuery.Append(" '<a class=\"btn btn-info\"><span class=\"fa fa-eye\"></span></a>',");
            lsQuery.Append(" case when estado > 11 then '<span class=\"fa fa-check\"></span>' else '<span class=\"fa fa-close\"></span>' end");
            lsQuery.Append(" from rp_estado_solicitud P where estado >= 12");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ValidaCartaTermino()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select case when A.estado > 11 then '<a class=\"btn btn-default\"><span class=\"fa fa-check\"></span></a>' else '<a class=\"btn btn-success\"><span class=\"fa fa-check\"></span></a>' end,");
            lsQuery.Append(" A.no_de_control, (select nombre+' '+ paterno+' '+materno from estudiantes_datos_completos where no_de_control = A.no_de_control)");
            lsQuery.Append(" from rp_estado_solicitud A");
            lsQuery.Append(" where A.estado >= 11");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
    }
	}
