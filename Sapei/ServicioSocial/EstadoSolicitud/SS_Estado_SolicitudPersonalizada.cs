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
	public partial class SS_Estado_Solicitud
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
		public DataTable ListaSolicitudSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select distinct'<a class=\"btn btn-success\" ><span class=\"fa fa-eye\"></span></a>', ");
            lsQuery.Append("(select folio from ss_solicitud where no_de_control = a.no_de_control), ");
            lsQuery.Append("a.no_de_control,(select apellido_paterno from alumnos where no_de_control = a.no_de_control) +' '+(select apellido_materno from alumnos where no_de_control = a.no_de_control) +' '+(select nombre_alumno from alumnos where no_de_control = a.no_de_control), ");
            lsQuery.Append("(select distinct nombre_carrera from carreras d where EXISTS(select carrera from alumnos e where e.no_de_control = a.no_de_control and e.carrera = d.carrera)) as nombre_carrera ");
            lsQuery.AppendFormat(" from {0} a", RutaTabla);
            lsQuery.Append(" where a.estado >= 5 ");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ListaCartaCompromisoSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select '<a class=\"btn btn-success\" ><span class=\"fa fa-eye\"></span></a>', ");
            lsQuery.Append(" (select folio from ss_solicitud where no_de_control = a.no_de_control), ");
            lsQuery.Append(" a.no_de_control,(select apellido_paterno from alumnos where no_de_control = a.no_de_control) +' '+(select apellido_materno from alumnos where no_de_control = a.no_de_control) +' '+(select nombre_alumno from alumnos where no_de_control = a.no_de_control), ");
            lsQuery.Append(" (select distinct nombre_carrera from carreras d where EXISTS(select carrera from alumnos e where e.no_de_control = a.no_de_control and e.carrera = d.carrera)) as nombre_carrera ");
            lsQuery.AppendFormat(" from {0} a", RutaTabla);
            lsQuery.Append(" where a.estado >= 6 ");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ListaCartaAsignacion()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select distinct '<a class=\"btn btn-success\" ><span class=\"fa fa-eye\"></span></a>', ");
            lsQuery.Append(" a.no_de_control,e.nombre+' '+e.paterno+' '+e.materno,e.nombre_carrera");
            lsQuery.Append(" from ss_estado_solicitud a, estudiantes_datos_completos e");
            lsQuery.Append(" where  a.estado >=6 and a.no_de_control = e.no_de_control");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ListaReporteBimestre1SS()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select case when a.estado >=8 then '<a class=\"btn btn-default\"><span class=\"fa fa-check\"></span></a>' else '<a class=\"btn btn-primary\" ><span class=\"fa fa-check\"></span></a>'end ,a.no_de_control, ");
            lsQuery.Append(" (select nombre_alumno from alumnos where no_de_control = a.no_de_control)+' '+ (select apellido_paterno from alumnos where no_de_control = a.no_de_control)+' '+ (select apellido_materno from alumnos where no_de_control = a.no_de_control),");
            lsQuery.Append(" (select distinct nombre_carrera from carreras d where EXISTS(select carrera from alumnos e where e.no_de_control = a.no_de_control and e.carrera = d.carrera)) as nombre_carrera ");
            lsQuery.AppendFormat(" from {0} a", RutaTabla);
            lsQuery.Append(" where a.estado >=7");
			lsQuery.AppendFormat(" and (select COUNT(id) from ss_reportes where no_de_control = a.no_de_control) >= 1");
			loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ListaReporteBimestre2SS()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select case when a.estado >=9 then '<a class=\"btn btn-default\"><span class=\"fa fa-check\"></span></a>' else '<a class=\"btn btn-primary\" ><span class=\"fa fa-check\"></span></a>'end ,a.no_de_control, ");
            lsQuery.Append(" (select nombre_alumno from alumnos where no_de_control = a.no_de_control)+' '+ (select apellido_paterno from alumnos where no_de_control = a.no_de_control)+' '+ (select apellido_materno from alumnos where no_de_control = a.no_de_control),");
            lsQuery.Append(" (select distinct nombre_carrera from carreras d where EXISTS(select carrera from alumnos e where e.no_de_control = a.no_de_control and e.carrera = d.carrera)) as nombre_carrera ");
            lsQuery.AppendFormat(" from {0} a", RutaTabla);
            lsQuery.Append(" where a.estado >=8 ");
			lsQuery.AppendFormat(" and (select COUNT(id) from ss_reportes where no_de_control = a.no_de_control)>=2");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ListaReporteBimestre3SS()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select case when a.estado >=10 then '<a class=\"btn btn-default\"><span class=\"fa fa-check\"></span></a>' else '<a class=\"btn btn-primary\" ><span class=\"fa fa-check\"></span></a>'end ,a.no_de_control, ");
            lsQuery.Append(" (select nombre_alumno from alumnos where no_de_control = a.no_de_control)+' '+ (select apellido_paterno from alumnos where no_de_control = a.no_de_control)+' '+ (select apellido_materno from alumnos where no_de_control = a.no_de_control),");
            lsQuery.Append(" (select distinct nombre_carrera from carreras d where EXISTS(select carrera from alumnos e where e.no_de_control = a.no_de_control and e.carrera = d.carrera)) as nombre_carrera ");
            lsQuery.AppendFormat(" from {0} a", RutaTabla);
            lsQuery.Append(" where a.estado >=9");
			lsQuery.AppendFormat(" and (select COUNT(id) from ss_reportes where no_de_control = a.no_de_control)>=3");
			loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable ListaReporteBimestre4SS()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select case when a.estado >=11 then '<a class=\"btn btn-default\"><span class=\"fa fa-check\"></span></a>' else '<a class=\"btn btn-primary\" ><span class=\"fa fa-check\"></span></a>'end ,a.no_de_control, ");
            lsQuery.Append(" (select nombre_alumno from alumnos where no_de_control = a.no_de_control)+' '+ (select apellido_paterno from alumnos where no_de_control = a.no_de_control)+' '+ (select apellido_materno from alumnos where no_de_control = a.no_de_control),");
            lsQuery.Append(" (select distinct nombre_carrera from carreras d where EXISTS(select carrera from alumnos e where e.no_de_control = a.no_de_control and e.carrera = d.carrera)) as nombre_carrera ");
            lsQuery.AppendFormat(" from {0} a", RutaTabla);
            lsQuery.Append(" where a.estado >=10");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ListaCartaTerminoSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select case when a.estado >=12 then '<a class=\"btn btn-default\"><span class=\"fa fa-check\"></span></a>' else '<a class=\"btn btn-primary\" ><span class=\"fa fa-check\"></span></a>'end ,a.no_de_control, ");
            lsQuery.Append(" (select nombre_alumno from alumnos where no_de_control = a.no_de_control)+' '+ (select apellido_paterno from alumnos where no_de_control = a.no_de_control)+' '+ (select apellido_materno from alumnos where no_de_control = a.no_de_control),");
            lsQuery.Append(" (select distinct nombre_carrera from carreras d where EXISTS(select carrera from alumnos e where e.no_de_control = a.no_de_control and e.carrera = d.carrera)) as nombre_carrera ");
            lsQuery.AppendFormat(" from {0} a", RutaTabla);
            lsQuery.Append(" where a.estado >=11");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ListaCartaTerminoFinalSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" Select distinct'<a class=\"btn btn-success\" ><span class=\"fa fa-eye\"></span></a>',");
            lsQuery.Append(" (select folio from ss_solicitud where no_de_control = e.no_de_control),");
            lsQuery.Append(" e.no_de_control,e.nombre+' '+e.paterno+' '+e.materno, e.nombre_carrera");
            lsQuery.Append(" from estudiantes_datos_completos e");
            lsQuery.Append(" where (select estado from ss_estado_solicitud where no_de_control = e.no_de_control) >= 12");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public string RegresaPeriodoResgistroSS(string psControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            string lsValor;
            lsQuery.Append(" select periodo");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where no_de_control = '{0}'", psControl);
            lsValor = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
            return Convert.ToString(lsValor);
        }

        public DataTable ConsultaEstadoSS(string psPeriodo, string psUsuario)
        {
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            DataTable loDt = new DataTable();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));

                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_ss_estado_proceso", loParametros));
            }
            return loDt;
        }
    }
	}
