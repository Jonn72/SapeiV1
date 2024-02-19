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
	/// Clase rp_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class RP_Solicitud
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

        public DataTable RegresaValidacionRP(string psControl, string psPeriodo)
        {
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pav_rp_inicio_proceso", loParametros));
            }
            return loDt;
        }
        public void RegresaValidacionAnteproyectoRP(string psControl, string psPeriodo, int lifuncion)
        {
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@funcion", lifuncion));
                _oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pav_rp_registro_anteproyecto", loParametros);
            }
        }
        public DataTable EviaComentario(int piProyecto, string psComentarios, string lsPeriodo)
        {
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            DataTable loDt = new DataTable();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id_programa", piProyecto));
                loParametros.Add(new ParametrosSQL("@observaciones", psComentarios));
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rp_envia_comentario", loParametros));
            }
            return loDt;
        }
        public DataTable ValidaRP(int piProyecto, string lsPeriodo, int estado)
        {
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            DataTable loDt = new DataTable();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id_programa", piProyecto));
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@estado", estado));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rp_valida", loParametros));
            }
            return loDt;
        }
        public DataTable ValidaTermino(string psControl)
        {
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            DataTable loDt = new DataTable();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rp_valida_termino", loParametros));
            }
            return loDt;
        }
        public DataTable ConsultaAsesoriaRP(string lsNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.AppendFormat(" select case when (select count(numero_asesoria) from rp_asesorias where no_de_control = '{0}' ) = 0 then 0 ", lsNoControl);
            lsQuery.AppendFormat(" else (select max(numero_asesoria) from rp_asesorias where no_de_control ='{0}') end as numero_asesoria", lsNoControl);
            lsQuery.Append(" ,(select duracion from rp_datos_programa where id_programa = A.id_programa) as duracion");
            lsQuery.Append(" ,id_programa, folio");
            lsQuery.AppendFormat(" from {0} A", RutaTabla);
            lsQuery.AppendFormat(" where no_de_control = '{0}' and estado_solicitud = 1", lsNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public void ActualizaFechas(string lsNumero, string psFechaInicio, string psFechaFin)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append(" UPDATE rp_solicitud set ");
            lsQuery.AppendFormat(" fecha_inicio = '{0}',", psFechaInicio);
            lsQuery.AppendFormat(" fecha_fin = '{0}'",psFechaFin);
            lsQuery.AppendFormat(" where no_de_control = '{0}'", lsNumero);
            _oSistema.Conexion.EjecutaComando(lsQuery);
        }
        public DataTable AutorizaResidenciaRP()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            List<string> loLista;
            loLista = new List<string>();
            lsQuery.Append(" select '<a  id=\"btn-valida\" '+ case when a.estado > 10 then 'class=\"btn btn-default\"' else 'class=\"btn btn-success\"' end +'><span class=\"fa fa-check\"></span></a>', a.no_de_control, b.nombre_alumno+' '+b.apellido_paterno+' '+b.apellido_materno as nombre_residente, (select distinct nombre_carrera from carreras where carrera = b.carrera), b.creditos_aprobados");
            lsQuery.Append("  from rp_estado_solicitud a inner join alumnos b on a.no_de_control = b.no_de_control ");
            lsQuery.Append(" where a.estado >= 10");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable CargaSolicitudesRP()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select '<a class=\"btn btn-warning\" ><span>Generar</span></a>', a.no_de_control,");
            lsQuery.Append(" (select nombre_alumno from alumnos where no_de_control =a.no_de_control) +' '+(select apellido_paterno from alumnos where no_de_control = a.no_de_control)+' '+(select apellido_materno from alumnos where no_de_control = a.no_de_control),");
            lsQuery.Append(" (select nombre_carrera from estudiantes_datos_completos where no_de_control = a.no_de_control)");
            lsQuery.Append(" from rp_estado_solicitud a where a.estado >= 2");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public int ConsultaFolioRP(string psPeriodo)
        {
            StringBuilder lsQuery = new StringBuilder();
            string lsValor;
            lsQuery.Append(" select isnull(max(folio),0) + 1 as folio ");
            lsQuery.AppendFormat(" from {0} ", RutaTabla);
            lsQuery.AppendFormat(" where periodo_datos = '{0}'", psPeriodo);
            lsValor = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
            return Convert.ToInt32(lsValor);
        }

        public int ConsultaFolioRegistroRP(string psnoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            string lsValor;
            lsQuery.Append("select count(estado_solicitud)");
            lsQuery.AppendFormat(" from {0} ", RutaTabla);
            lsQuery.AppendFormat(" where no_de_control = '{0}'", psnoControl);
            lsQuery.Append( " and estado_solicitud = 1");
            lsValor = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
            return Convert.ToInt32(lsValor);
        }
        public DataTable ActualizaFolioRP(string psFolio, int piEstado)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" UPDATE rp_solicitud ");
            lsQuery.AppendFormat(" SET estado_solicitud = {0}", piEstado);
            lsQuery.AppendFormat(" WHERE folio = '{0}'", psFolio);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            return loDt;
        }
        public DataTable ConsultaProgramaRP(string psnoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select id_programa, (select nombre from rp_programa where id=a.id_programa) as nombre_programa ");
            lsQuery.AppendFormat(" from {0} a ", RutaTabla);
            lsQuery.AppendFormat(" where no_de_control = {0}", psnoControl);
            lsQuery.Append(" and estado_solicitud = 1");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ConsultaEstadoResidentes(string lsid)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select (select estado as estado from rp_estado_solicitud where no_de_control = P.no_de_control) as estado ");
            lsQuery.AppendFormat(" from {0} P" , RutaTabla);
            lsQuery.AppendFormat(" where id_programa = {0}", lsid);
            lsQuery.Append(" order by estado asc");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ActualizaEstadoResidentes(string lsid)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select (select estado as estado from rp_estado_solicitud where no_de_control = P.no_de_control) as estado ");
            lsQuery.AppendFormat(" from {0} P", RutaTabla);
            lsQuery.AppendFormat(" where id_programa = {0}", lsid);
            lsQuery.Append(" order by estado desc");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ConsultaNo_de_Control_Residentes(string lsid)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select (select no_de_control from rp_estado_solicitud where no_de_control = P.no_de_control) as no_de_control ");
            lsQuery.AppendFormat(" from {0} P", RutaTabla);
            lsQuery.AppendFormat(" where id_programa = {0}", lsid);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ConsultaNombreResidentes(int liIdprograma)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" SELECT (select nombre_alumno from alumnos where no_de_control=A.no_de_control)+' '+(select apellido_paterno from alumnos where no_de_control=A.no_de_control)+' '+(select apellido_materno from alumnos where no_de_control=A.no_de_control) as nombre_residentes ,");
            lsQuery.Append(" (select nombre from rp_programa where id = A.id_programa) as nombre,");
            lsQuery.Append(" (select carrera from rp_programa where id = A.id_programa) as carrera, A.no_de_control");
            lsQuery.AppendFormat(" from {0} A", RutaTabla);
            lsQuery.AppendFormat(" where A.id_programa = {0}", liIdprograma);
            lsQuery.Append(" and A.estado_solicitud = 1");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ConsultaEvaluacionesRP(string psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select evaluacion_final ");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where no_de_control = {0} ", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable CargaFoliosRP(string lsPeriodo)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select  case when estado_solicitud = 1 then '<a class=\"btn btn-primary\" ><span>Cancelar</span></a>' else 'Inactivo' end ,");
            lsQuery.Append(" periodo_datos,folio, no_de_control");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where periodo_datos ='{0}'", lsPeriodo);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public void BajaFoliosRP(string usuario, int piFolio)
        {
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@usuario", usuario));
                loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano));
                loParametros.Add(new ParametrosSQL("@folio", piFolio));
                _oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rp_baja_folio", loParametros);
            }
        }
    }
	}
