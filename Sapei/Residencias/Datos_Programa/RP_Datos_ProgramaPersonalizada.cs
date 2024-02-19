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
	/// Clase rp_datos_programa generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class RP_Datos_Programa
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



        public DataTable ActulizaDuracionRP(int piProyecto, int piDuracion, string lsPeriodo)
        {
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id_programa", piProyecto));
                loParametros.Add(new ParametrosSQL("@duracion", piDuracion));
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pav_rp_actualiza_duracion", loParametros));
            }
            return loDt;
        }

        public DataTable ConsultaRegistroRP(int piId)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select B.id_programa, B.delimitaciones, B.objetivo_general, B.objetivo_especificos,");
            lsQuery.Append(" B.actividades, B.duracion, B.justificacion, B.ubicacion, B.rfc_asesor,");
            lsQuery.Append(" B.rfc_revisor, B.observaciones, A.nombre");
            lsQuery.Append(" from rp_programa A inner join rp_datos_programa B on A.id = B.id_programa");
            lsQuery.AppendFormat(" where A.id = {0}", piId);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable AsignacionDocentesRP(string psUsuario)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select distinct '<a  id=\"btn-AsignaInterno\" class=\"btn btn-success\"><span class=\"fa fa-user\"></span></a> '+ case when C.estado >=9 then '<a class=\"btn btn-primary\"><span class=\"fa fa-eye\"></span></a>' else '<a class=\"btn btn-default\"><span class=\"fa fa-eye-slash\"></span></a>' end +'',");
            lsQuery.Append(" B.id, B.numero_proyecto, B.nombre from rp_solicitud A inner join rp_programa B on A.id_programa = B.id, rp_estado_solicitud C");
            lsQuery.AppendFormat(" where A.estado_solicitud = 1 and B.carrera in (select carrera from sis_permisos_carreras where usuario = '{0}' )", psUsuario);
            lsQuery.Append(" and A.no_de_control = C.no_de_control and C.estado >= 8");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;

        }
        public DataTable AsignacionRevisorRP(string psUsuario)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select distinct (select case when rfc_revisor = '' then '<a class=\"btn btn-info\"><span class=\"fa fa-eye\"></span></a>' else '<a class=\"btn btn-primary\"><span class=\"fa fa-eye\"></span></a>' end from rp_datos_programa where id_programa = A.id_programa) +'<a  id=\"btn-AsignaRevisor\" class=\"btn btn-success\"><span class=\"fa fa-user\"></span></a>', ");
            lsQuery.Append(" B.id, B.numero_proyecto, B.nombre from rp_solicitud A inner join rp_programa B on A.id_programa = B.id, rp_estado_solicitud C");
            lsQuery.AppendFormat(" where A.estado_solicitud = 1 and B.carrera in (select carrera from sis_permisos_carreras where usuario = '{0}' )", psUsuario);
            lsQuery.Append(" and A.no_de_control = C.no_de_control and C.estado >= 11");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable RfcPersonalRP()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select rfc, nombre_empleado +' '+apellidos_empleado as docente from personal");
            lsQuery.Append(" order by docente asc");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public void AsignaInternoRP(int lsid, string psRFc)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("UPDATE rp_datos_programa ");
            lsQuery.AppendFormat(" SET rfc_asesor = '{0}' ", psRFc);
            lsQuery.AppendFormat("WHERE id_programa = {0}", lsid);
            _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public void AsignaRevisorRP(int piProyecto, string psRFc)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("UPDATE rp_datos_programa ");
            lsQuery.AppendFormat(" SET rfc_revisor = '{0}' ", psRFc);
            lsQuery.AppendFormat("WHERE id_programa = {0}", piProyecto);
            _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public DataTable Consultarevisor(int piId)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select B.nombre_empleado+' '+B.apellidos_empleado as rfc_revisor ");
            lsQuery.Append(" from rp_datos_programa A inner Join personal B on A.rfc_revisor = B.rfc");
            lsQuery.AppendFormat(" where A.id_programa = {0}", piId);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable RegresaAsignacionInterno(int piProyecto)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append(" SELECT id_programa as id, (select nombre from rp_programa where id = A.id_programa) as nombre, ");
            lsConsulta.Append(" (select nombre_empleado from personal where rfc = A.rfc_asesor)+' '+ (select apellidos_empleado from personal where rfc = A.rfc_asesor) as asesor, ");
            lsConsulta.Append(" (select dependencia from rp_dependencias where rfc in (select rfc_dependencia from rp_programa where id = A.id_programa)) as dependencia,");
            lsConsulta.Append(" (select identificacion_larga from periodos_escolares where periodo = A.periodo_datos) as identificacion_larga,");
            lsConsulta.Append(" (select DISTINCT nombre_carrera from carreras where carrera in (select carrera from rp_programa where id = A.id_programa)) as nombre_carrera");
            lsConsulta.Append(" from rp_datos_programa A");
            lsConsulta.AppendFormat(" where A.id_programa = {0}", piProyecto);
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);
        }
    }
	}