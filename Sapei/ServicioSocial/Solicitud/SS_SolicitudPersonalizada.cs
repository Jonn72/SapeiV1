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
	/// Clase ss_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class SS_Solicitud
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

		public DataTable RegresaValidacionSS(string psControl, string psPeriodo)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pav_ss_inicio_proceso", loParametros));
			}
			return loDt;
		}

		public DataTable CargaEstadoInicialSS(string periodo, string no_de_control, int estado)
		{
			StringBuilder lsQuery = new StringBuilder();
			lsQuery.Append(" insert into ss_estado_solicitud(periodo,no_de_control, estado) ");
			lsQuery.AppendFormat(" values ({0}, {1}, {2} )", periodo, no_de_control, estado);
			return _oSistema.Conexion.RegresaDataTable(lsQuery);
		}

		public DataTable ConsultaEstadosSS(string psNoControl)
		{
			StringBuilder lsQuery = new StringBuilder();
			DataTable loDt;
			lsQuery.Append(" select estado,(select modalidad from ss_solicitud where no_de_control = a.no_de_control) as modalidad " +
				" from ss_estado_solicitud a ");
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
		//REGRESA EL ID DEL PROGRAMA DE LA SOLICITUD CON EL NUMERO DE CONTROL INGRESADO
		public DataTable RegresaDatosSolicitud(string psNoControl)
		{
			StringBuilder lsQuery = new StringBuilder();
			DataTable loDt;
			lsQuery.Append(" Select a.periodo, a.folio,a.id_programa, a.fecha_inicio,a.fecha_termino,a.fecha_solicitud,a.modalidad,a.turno,a.estado, b.id_tipo_programa ");
			lsQuery.Append(" from ss_solicitud a, ss_programa b");
			lsQuery.AppendFormat(" where no_de_control = ('{0}') ", psNoControl);
			lsQuery.Append(" and a.id_programa = b.id");
			loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}
		public void ActualizarIdPrograma(int psIdPrograma, string psNoControl)
		{
			StringBuilder lsQuery = new StringBuilder();
			lsQuery.Append("UPDATE ss_solicitud ");
			lsQuery.AppendFormat("SET id_programa = {0}", psIdPrograma);
			lsQuery.AppendFormat("WHERE no_de_control = '{0}'", psNoControl);
			_oSistema.Conexion.EjecutaComando(lsQuery);
		}

		public DataTable ConsultaIdProgramaInterno(string psNoControl)
		{
			StringBuilder lsQuery = new StringBuilder();
			DataTable loDt;
			lsQuery.Append(" select id_programa from ss_solicitud ");
			lsQuery.AppendFormat(" where no_de_control = '{0}' ", psNoControl);
			loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}

		public DataTable ListaCursoInduccionSS()
		{
			StringBuilder lsQuery = new StringBuilder();
			DataTable loDt;
			lsQuery.Append(" select distinct a.no_de_control, b.apellido_paterno, b.apellido_materno, b.nombre_alumno, d.nombre_carrera from ss_estado_solicitud a, alumnos b,carreras d    ");
			lsQuery.AppendFormat(" where a.estado >= {0} ", 1);
			lsQuery.AppendFormat(" and a.no_de_control= b.no_de_control and b.carrera=d.carrera");
			lsQuery.AppendFormat(" AND a.periodo = '{0}'", _oSistema.Sesion.Periodo.PeriodoActualSinVerano);

			loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}

		public DataTable ListaTarjetaControl()
		{
			StringBuilder lsQuery = new StringBuilder();
			DataTable loDt;
			lsQuery.Append(" select distinct '<a class=\"btn btn-success\" ><span class=\"fa fa-eye\"></span></a>', a.no_de_control,e.nombre+' '+e.paterno+' '+e.materno,e.nombre_carrera ");
			lsQuery.Append(" from ss_estado_solicitud a, estudiantes_datos_completos e");
			lsQuery.Append(" where estado >= 11 and a.no_de_control =e.no_de_control");

			loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}

		public DataTable ListaCartaPresentacionExternoSS()
		{
			StringBuilder lsQuery = new StringBuilder();
			DataTable loDt;
			lsQuery.Append(" select (select case when estado =2 then '<a class=\"btn btn-warning\" ><span>Asignar</span></a>' else '<a class=\"btn btn-success\" ><span class=\"fa fa-eye\"></span></a>' end from ss_estado_solicitud where no_de_control = a.no_de_control),a.folio, a.no_de_control,(select nombre_alumno from alumnos where no_de_control =a.no_de_control) +' '+(select apellido_paterno from alumnos where no_de_control = a.no_de_control)+' '+(select apellido_materno from alumnos where no_de_control = a.no_de_control),(select distinct nombre_carrera from carreras where carrera =b.carrera) as carrera from ss_solicitud a, alumnos b where a.modalidad = 'ext' and a.no_de_control=b.no_de_control ");
			lsQuery.AppendFormat(" AND a.periodo = '{0}'", _oSistema.Sesion.Periodo.PeriodoActualSinVerano);
			lsQuery.Append(" AND  (select estado from ss_estado_solicitud where no_de_control = a.no_de_control)>=2 ");

			loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}

		public DataTable ListaCartaPresentacionInternoSS()
		{

			StringBuilder lsQuery = new StringBuilder();
			DataTable loDt;
			lsQuery.Append(" select (select case when estado =2 then '<a class=\"btn btn-warning\" ><span>Asignar</span></a>' else '<a class=\"btn btn-success\" ><span class=\"fa fa-eye\"></span></a>' end from ss_estado_solicitud where no_de_control=a.no_de_control),a.folio, a.no_de_control,(select nombre_alumno from alumnos where no_de_control =a.no_de_control) +' '+(select apellido_paterno from alumnos where no_de_control = a.no_de_control)+' '+(select apellido_materno from alumnos where no_de_control = a.no_de_control),(select distinct nombre_carrera from carreras where carrera =b.carrera) as carrera, turno from ss_solicitud a, alumnos b where a.modalidad = 'int' and a.no_de_control=b.no_de_control ");
			lsQuery.AppendFormat(" AND a.periodo = '{0}'", _oSistema.Sesion.Periodo.PeriodoActualSinVerano);
			lsQuery.Append(" AND  (select estado from ss_estado_solicitud where no_de_control = a.no_de_control)>=2 ");
			loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}

		public DataTable ListaCartaAceptacionSS()
		{
			StringBuilder lsQuery = new StringBuilder();
			DataTable loDt;
			lsQuery.Append(" select distinct case when c.estado >=4 then '<a class=\"btn btn-default\"><span class=\"fa fa-check\"></span></a>' else '<a class=\"btn btn-primary\" ><span class=\"fa fa-check\"></span></a>'end , a.no_de_control, b.nombre_alumno+' '+b.apellido_paterno+' '+b.apellido_materno as nombre_estudiante,(select distinct nombre_carrera from carreras where carrera=b.carrera) as carrera from ss_solicitud a, alumnos b, ss_estado_solicitud c where a.no_de_control=b.no_de_control and a.no_de_control = c.no_de_control and c.estado >=3");
			lsQuery.AppendFormat(" AND a.periodo = '{0}'", _oSistema.Sesion.Periodo.PeriodoActualSinVerano);
			loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}

		public int ConsultaFolioSS(string psPeriodo)
		{
			StringBuilder lsQuery = new StringBuilder();
			string lsValor;
			lsQuery.Append(" select isnull(max(folio),0) + 1 as folio");
			lsQuery.AppendFormat(" from {0}", RutaTabla);
			lsQuery.AppendFormat(" where periodo = '{0}'", psPeriodo);
			lsValor = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
			return Convert.ToInt32(lsValor);
		}

		public void RegistroCalificacionCualitativa(string psControl, double psCalificacion, int psReporte)
		{
			StringBuilder lsQuery = new StringBuilder();
			lsQuery.Append(" update ss_reportes ");
			lsQuery.AppendFormat(" set evaluacion_cualitativa = {0} ", psCalificacion);
			lsQuery.AppendFormat(" where no_de_control = '{0}'", psControl);
			lsQuery.AppendFormat(" and no_reporte = {0}", psReporte);
			_oSistema.Conexion.EjecutaComando(lsQuery);
		}

		public DataTable EvaluacionCualitativaBimestre1(string lsNumero)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("Select (select nombre_alumno from alumnos where no_de_control = a.no_de_control) as nombre_alumno, ");
			lsConsulta.Append(" (select apellido_paterno from alumnos where no_de_control = a.no_de_control) as apellido_paterno, ");
			lsConsulta.Append(" (select apellido_materno from alumnos where no_de_control = a.no_de_control) as apellido_materno, ");
			lsConsulta.Append(" a.no_de_control, (select nombre from ss_programa where id = a.id_programa) as nombre, ");
			lsConsulta.Append(" (select responsable from ss_programa where id = a.id_programa) as responsable, ");
			lsConsulta.Append(" (select cargo_responsable from ss_programa where id = a.id_programa) as cargo_responsable, ");
			lsConsulta.Append(" (select identificacion_larga from periodos_escolares where periodo = a.periodo) as identificacion_larga");
			lsConsulta.AppendFormat(" from {0} a", RutaTabla);
			lsConsulta.AppendFormat(" where no_de_control = '{0}' ", lsNumero);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable Bimestre1(string lsNumero)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("Select (select nombre_alumno from alumnos where no_de_control = a.no_de_control) as nombre_alumno, ");
			lsConsulta.Append("(select apellido_paterno from alumnos where no_de_control = a.no_de_control) as apellido_paterno, ");
			lsConsulta.Append("(select apellido_materno from alumnos where no_de_control = a.no_de_control) as apellido_materno, ");
			lsConsulta.Append("(select tipo_actividades from ss_actividades_solicitud where folio = a.folio  and periodo = a.periodo and id_programa = a.id_programa) as tipo_actividades, ");
			lsConsulta.Append("(select responsable from ss_programa where id =a.id_programa) as responsable, ");
			lsConsulta.Append("(select cargo_responsable from ss_programa where id= a.id_programa) as cargo_responsable, ");
			lsConsulta.Append(" a.no_de_control,(select dependencia from ss_dependencias b where EXISTS(select rfc from ss_programa c where b.rfc = c.rfc and c.id = a.id_programa)) as dependencia, ");
			lsConsulta.Append("(select nombre from ss_programa where id=a.id_programa) as nombre, ");
			lsConsulta.Append(" (select fecha_inicio from ss_activar_periodo where periodo = a.periodo) as fecha_inicio, ");
            lsConsulta.Append(" (select fecha_bimestre_1 from ss_activar_periodo where periodo = a.periodo) as fecha_bimestre_1, ");
            lsConsulta.Append(" (select fecha_bimestre_2 from ss_activar_periodo where periodo = a.periodo) as fecha_bimestre_2, ");
            lsConsulta.Append(" (select fecha_bimestre_3 from ss_activar_periodo where periodo = a.periodo) as fecha_bimestre_3, ");
            lsConsulta.Append(" (select distinct nombre_carrera from carreras d where EXISTS(select carrera from alumnos e where e.no_de_control = a.no_de_control and e.carrera = d.carrera)) as nombre_carrera ");
			lsConsulta.AppendFormat("from {0} a", RutaTabla);
			lsConsulta.AppendFormat(" where no_de_control = '{0}' ", lsNumero);
			//lsConsulta.AppendFormat(" AND periodo = '{0}'",_oSistema.Sesion.Periodo.PeriodoActualSinVerano);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable SolicitudServicio(string psNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select a.no_de_control, e.nombre_carrera,e.nombre as nombre_alumno, e.paterno as apellido_paterno, e.materno as apellido_materno, e.sexo,e.semestre , ");
			lsConsulta.Append(" (select identificacion_larga from periodos_escolares where periodo = a.periodo) as identificacion_larga,");
			lsConsulta.Append(" (select dependencia from ss_dependencias b where EXISTS(select rfc from ss_programa c where c.id = a.id_programa and c.rfc = b.rfc)) as dependencia,");
			lsConsulta.Append(" (select nombre from ss_programa where id = a.id_programa) as nombre,");
			lsConsulta.Append(" (select fecha_inicio from ss_activar_periodo where periodo = a.periodo) as fecha_inicial_periodo,");
			lsConsulta.Append(" (select fecha_fin from ss_activar_periodo where periodo = a.periodo) as fecha_fin,");
			lsConsulta.Append(" (select tipo_actividades from ss_actividades_solicitud where folio = a.folio and periodo = a.periodo) as tipo_actividades,");
			lsConsulta.Append(" (select titular from ss_dependencias b where EXISTS(select rfc from ss_programa c where c.id = a.id_programa and c.rfc = b.rfc)) as titular,");
			lsConsulta.Append(" (select puesto_cargo from ss_dependencias b where EXISTS(select rfc from ss_programa c where c.id = a.id_programa and c.rfc = b.rfc)) as puesto_cargo,");
			lsConsulta.Append(" (select id_tipo_programa from ss_programa where id = a.id_programa and periodo = a.periodo) as id_tipo_programa,");
			lsConsulta.Append(" a.modalidad, e.telefono, e.calle as domicilio_calle, e.ciudad, e.cp as codigo_postal");
			lsConsulta.Append(" from ss_solicitud a, estudiantes_datos_completos e ");
			lsConsulta.AppendFormat(" where a.no_de_control = '{0}' ", psNoControl);
			lsConsulta.Append(" and a.no_de_control = e.no_de_control ");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable CartaCompromiso(string psNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select a.no_de_control, e.nombre as nombre_alumno, e.paterno as apellido_paterno, e.materno as apellido_materno,e.nombre_carrera as nombre_carrera, ");
			lsConsulta.Append("  (select dependencia from ss_dependencias b where EXISTS(select rfc from ss_programa c where c.id = a.id_programa and c.rfc = b.rfc)) as dependencia,");
			lsConsulta.Append(" e.semestre, (select fecha_inicio from ss_activar_periodo where periodo = a.periodo) as fecha_inicial_periodo,");
			lsConsulta.Append(" (select fecha_fin from ss_activar_periodo where periodo = a.periodo) as fecha_fin,");
			lsConsulta.Append(" e.calle as domicilio_calle, e.colonia as domicilio_colonia, e.numero, e.cp  as codigo_postal, e.telefono, a.fecha_solicitud,");
			lsConsulta.Append(" (select responsable from ss_programa where id = a.id_programa) as responsable ");
			lsConsulta.Append(" from ss_solicitud a, estudiantes_datos_completos e");
			lsConsulta.AppendFormat(" where a.no_de_control = '{0}' ", psNoControl);
			lsConsulta.Append(" and a.no_de_control = e.no_de_control");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable CartaAsignacion(string psNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select a.no_de_control, e.nombre_carrera, e.nombre as nombre_alumno, e.paterno as apellido_paterno,e.sexo, ");
			lsConsulta.Append(" e.materno as apellido_materno, e.semestre,e.telefono, e.calle as domicilio_calle, e.colonia as domicilio_colonia, e.cp as codigo_postal,e.ciudad, e.creditos_aprobados, e.fecha_nacimiento,");
			lsConsulta.Append(" (select nombre from ss_programa where id= a.id_programa) as nombre,");
			lsConsulta.Append(" (select objetivo from ss_programa where id = a.id_programa) as objetivo,");
			lsConsulta.Append(" (select tipo_actividades from ss_actividades_solicitud where id_programa=a.id_programa) as tipo_actividades,");
			lsConsulta.Append(" (select id_actividades from ss_actividades_solicitud where id_programa = a.id_programa) as id_actividades,");
			lsConsulta.Append(" (select responsable from ss_programa where id= a.id_programa) as responsable");
			lsConsulta.Append(" from ss_solicitud a, estudiantes_datos_completos e ");
			lsConsulta.AppendFormat(" where a.no_de_control = '{0}' ", psNoControl);
			lsConsulta.Append(" and a.no_de_control = e.no_de_control");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable TarjetaControl(string psNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("Select a.no_de_control, e.nombre_carrera, e.nombre as nombre_alumno, e.paterno  as apellido_paterno, e.materno as apellido_materno,  ");
			lsConsulta.Append(" e.semestre,e.sexo, e.creditos_aprobados,e.calle as domicilio_calle, e.colonia as domicilio_colonia, e.ciudad, e.cp as codigo_postal,e.fecha_nacimiento,");
			lsConsulta.Append(" (select fecha_inicio from ss_activar_periodo where periodo = a.periodo) as fecha_inicial_periodo,");
			lsConsulta.Append(" (select fecha_fin from ss_activar_periodo where periodo = a.periodo) as fecha_fin,");
			lsConsulta.Append(" (select nombre from ss_programa where id= a.id_programa) as nombre,");
			lsConsulta.Append(" (select dependencia from ss_dependencias b where EXISTS(select rfc from ss_programa c where c.id = a.id_programa and c.rfc = b.rfc)) as dependencia");
			lsConsulta.Append(" from ss_solicitud a, estudiantes_datos_completos e");
			lsConsulta.AppendFormat(" where a.no_de_control = '{0}' ", psNoControl);
			lsConsulta.Append(" and a.no_de_control =e.no_de_control");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable CartaTerminoFinal(string psNoControl, string psFolio)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("Select a.folio, a.no_de_control, e.nombre_carrera, e.nombre as nombre_alumno,e.paterno as apellido_paterno, e.materno as apellido_materno, ");
			lsConsulta.Append(" (select nombre from ss_programa where id = a.id_programa) as nombre,");
			lsConsulta.Append(" (select dependencia from ss_dependencias b where EXISTS(select rfc from ss_programa c where c.id = a.id_programa and c.rfc = b.rfc)) as dependencia,");
			lsConsulta.Append(" (select fecha_inicio from ss_activar_periodo where periodo =a.periodo) as fecha_inicial_periodo,");
			lsConsulta.Append(" (select fecha_fin from ss_activar_periodo where periodo =a.periodo) as fecha_fin");
			lsConsulta.Append(" from ss_solicitud a, estudiantes_datos_completos e");
			lsConsulta.AppendFormat(" where a.no_de_control = '{0}'", psNoControl);
			lsConsulta.AppendFormat(" and a.folio = '{0}'", psFolio);
			lsConsulta.Append(" and a.no_de_control = e.no_de_control");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable ConsultaCalfinal(string psNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select (SUM(CONVERT (float, evaluacion)/'3')*'0.1') + (SUM(CONVERT (float, evaluacion_cualitativa)/'3')*'0.9') as promedio from ss_reportes");
			lsConsulta.AppendFormat(" where no_de_control = '{0}' and no_reporte in (1,2,3)", psNoControl);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public string EliminaProceso(string psControl, string psJustificacion, string psUsuario)
		{
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			string lsRespuesta = "";
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loParametros.Add(new ParametrosSQL("@justificacion", psJustificacion));
				loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
				lsRespuesta = Convert.ToString(_oSistema.Conexion.EjecutaEscalarProcedimientoAlmacenado("pap_ss_elimina_proceso", loParametros));
			}
			return lsRespuesta;
		}
		#endregion
	}
}
