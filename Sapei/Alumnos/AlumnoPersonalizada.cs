using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Sapei.Framework;
using Sapei.Framework.Utilerias;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sapei.Framework.BaseDatos;

namespace Sapei
{
	/// <summary>
	/// Clase alumno generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Alumno
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
		#region Personalizados
		public List<string> RegresaPermisosFuncionRol(Sapei.Framework.Configuracion.enmRolUsuario enumRol)
		{
			StringBuilder lsQuery;
			List<string> loLista;
			DataTable loDatos;
			lsQuery = new StringBuilder();
			loLista = new List<string>();
			lsQuery.AppendFormat("Select clave From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "sis_menus_permisos_rol");
			lsQuery.Append(" where");
			lsQuery.AppendFormat(" tipo_usuario= ('{0}')", enumRol.ToString());
			loDatos = _oSistema.Conexion.RegresaDataTable(lsQuery);
			foreach (DataRow loRow in loDatos.Rows)
			{
				loLista.Add(loRow.RegresaValor<string>("clave").Trim());
			}
			return loLista;
		}
		public bool ValidaExisteNoControl(string psNoControl)
		{
			StringBuilder lsQuery;
			string lsResultado;
			lsQuery = new StringBuilder();
			lsQuery.AppendFormat("Select 1 From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, NombreTabla);
			lsQuery.Append(" where");
			lsQuery.AppendFormat(" no_de_control= ('{0}')", psNoControl);
			lsResultado = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
			if (string.IsNullOrEmpty(lsResultado))
				return false;
			return true;
		}
		public DataTable CargarVistaDatosCompletos(string psNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select [nombre],[paterno],[materno],[fecha_nacimiento],lugar_nacimiento, nss");
			lsConsulta.Append(",[sexo],[curp],[estado_civil],[correo],[telefono],[celular],[calle]");
			lsConsulta.Append(",[numero],[id_cp],[ciudad],[colonia],[cp],[entidad],[estatus]");
			lsConsulta.Append(",[periodo_ingreso],[tipo_ingreso],[carrera],[nombre_carrera],[plan_de_estudios],[nivel_escolar],[reticula],[especialidad],[descripcion_especialidad]");
			lsConsulta.Append(",[anio_egreso],[promedio_procedencia],[id_escuela],[escuela_procedencia],[periodo_ingreso_it]");
			lsConsulta.AppendFormat(" From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "estudiantes_datos_completos");
			lsConsulta.AppendFormat(" where no_de_control = '{0}'", psNoControl);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);

		}

		public object CargarDatosAvisosReinscripcion(string psPeriodo, string psNoControl)
		{
			StringBuilder lsConsulta;
			DataTable loDt;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select ");
			lsConsulta.AppendFormat("(select apellido_paterno + ' ' + apellido_materno + ' ' + nombre_alumno from alumnos where no_de_control = '{0}') nombre", psNoControl);
			lsConsulta.AppendFormat(",(select recibo_pago from avisos_reinscripcion where no_de_control = '{0}' and periodo = '{1}') recibo_pago", psNoControl, psPeriodo);
			lsConsulta.AppendFormat(",(select SUM(abonado) from alumnos_historial_pagos where periodo = '{1}' and no_de_control = '{0}') monto_pagado", psNoControl, psPeriodo);
			lsConsulta.AppendFormat(",(select SUM(monto) - (SUM(abonado)+SUM(condonacion)) from alumnos_historial_pagos where periodo = '{1}' and no_de_control = '{0}') monto_a_pagar", psNoControl, psPeriodo);
			loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}


		public object CargarDatosPagoDesglosado(string psPeriodo, string psNoControl)
		{
			StringBuilder lsConsulta;
			DataTable loDt;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select ");
			lsConsulta.Append(" monto_a_pagar, ");
			lsConsulta.Append(" case  prioridad_reinscripcion when 1 then 1 when 3 then 2 else 0 end as no_especiales,");
			lsConsulta.Append(" (select monto  from monto_servicios where clave = 106) costo_especial,");
			lsConsulta.Append(" isnull((select monto  from monto_servicios where clave > 330 and clave < 339 and semestre = A.semestre),0)  costo_ingles, ");
			lsConsulta.Append(" (select nombre_alumno+' '+apellido_paterno+' '+apellido_materno from alumnos where no_de_control = A.no_de_control) as nombre");
			lsConsulta.Append(" from avisos_reinscripcion A ");
			lsConsulta.Append(" where");
			lsConsulta.AppendFormat(" A.no_de_control = '{0}'  and periodo = '{1}'", psNoControl, psPeriodo);
			loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}
		public object CargarDatosReferencia(string psNoControl)
		{
			StringBuilder lsConsulta;
			DataTable loDt;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select ");
			lsConsulta.Append(" no_referencia_dv referencia, ");
			lsConsulta.Append(" (select nombre_alumno+' '+apellido_paterno+' '+apellido_materno from alumnos where no_de_control = A.no_de_control) as nombre");
			lsConsulta.Append(" from pago_referenciado A ");
			lsConsulta.Append(" where");
			lsConsulta.AppendFormat(" A.no_de_control = '{0}'", psNoControl);
			loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}

		public DataTable RegresaListaNuevosEstudiantes(string psPeriodo)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select no_de_control,  nombre_alumno + ' ' + apellido_paterno + ' ' + apellido_materno , carrera, nip ");
			lsConsulta.AppendFormat(" From {0}", RutaTabla);
			lsConsulta.Append(" where ");
			lsConsulta.AppendFormat(" periodo_ingreso_it = '{0}'", psPeriodo);
			lsConsulta.AppendFormat(" and no_de_control not in (select distinct no_de_control from seleccion_materias where periodo = '{0}')",psPeriodo);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable RegresaDatosCargaAcademica(string psPeriodo, string psNoControl, bool pbFiEl = false)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
			loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));

			using (var loCOnexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_alu_carga_academica", loParametros));
			}
			return loDt;
		}
		public DataTable RegresaDatosCargaAcademica(string psPeriodo,string psCarrea, string psSemestre)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
			loParametros.Add(new ParametrosSQL("@carrera", psCarrea));
			loParametros.Add(new ParametrosSQL("@semestre", psSemestre));

			using (var loCOnexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_fiel_cargas_academicas_reporte", loParametros));
			}

			return loDt;
		}

		public DataTable RegresaDatosDocumentosEscolares(string psNoControl, string psNombreDoc)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@no_de_control",psNoControl));
			loParametros.Add(new ParametrosSQL("@nombre_doc", psNombreDoc));
		
			using (var loCOnexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_exp_dig_alu_fiel_documentos_escolares_reporte", loParametros));
			}
			return loDt;
		}

		public DataTable RegresaDatosCertificado(string psNoControl)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
			loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_certificado_datos", loParametros));
			return loDt;
		}
		public DataTable RegresaDatosAspirantes2AlumnosInscritos(string psPeriodo)
		{
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_esc_inscritos", loParametros));
            }
            return ldtTabla;
   //         StringBuilder lsConsulta;
			//lsConsulta = new StringBuilder();
			//lsConsulta.Append(" select  (select distinct(nombre_carrera) from carreras where carrera = A.carrera) carrera, grupo,");
			//lsConsulta.Append(" sum(case vuelta  when 1 then 1 else 0 end) vuelta1, sum(case vuelta  when 2 then 1 else 0 end) vuelta2,");
			//lsConsulta.Append(" sum(case sexo  when 'H' then 1 else 0 end) hombres, sum(case sexo  when 'M' then 1 else 0 end) mujeres");
			//lsConsulta.AppendFormat(" ,(select count(no_de_control) from aspirantes_datos_completos where carrera = A.carrera and periodo = '{0}' and estatusAspirante = 3) aceptados", psPeriodo);
			//lsConsulta.Append(" FROM ");
			//lsConsulta.Append(" ( select no_de_control,sexo,  carrera, vuelta ");
			//lsConsulta.Append("  ,(select distinct(grupo) from seleccion_materias where periodo = A.periodo and no_de_control = A.no_de_control) grupo");
			//lsConsulta.AppendFormat(" from aspirantes_datos_completos A where periodo = '{0}' and ", psPeriodo);
			//lsConsulta.Append(" no_de_control in (select no_de_control from seleccion_materias where periodo = A.periodo)) A");
			//lsConsulta.Append(" group by grupo, carrera");

			//return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable RegresaTablaEstudiantesSinPago(string psPeriodo)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select no_de_control,[nombre],[paterno],[materno],[fecha_nacimiento], nss");
			lsConsulta.Append(",[sexo],[curp],[estado_civil],[correo],[telefono],[celular]");
			lsConsulta.Append(",[estatus]");
			lsConsulta.Append(",[periodo_ingreso],[nombre_carrera],[descripcion_especialidad]");
			lsConsulta.AppendFormat(" From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "estudiantes_datos_completos");
			lsConsulta.Append(" where no_de_control in ");
			lsConsulta.AppendFormat("(select no_de_control from avisos_reinscripcion where periodo = '{0}' and prioridad_reinscripcion in (0,1,2,3) and autoriza_escolar is null)", psPeriodo);

			return _oSistema.Conexion.RegresaDataTable(lsConsulta);

		}

		public DataTable RegresaHorario(string psPeriodo)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("(SELECT A.periodo  ");
			lsConsulta.Append(" ,stuff(( ");
			lsConsulta.Append(" SELECT '|'+(select nombre_abreviado_materia from materias where materia = B.materia)+' / '+B.aula+',2018-01-0'+ CONVERT(varchar(2),dia_semana-1)+'T' +convert(char(8),B.hora_inicial) + ',' + '2018-01-0'+ CONVERT(varchar(2),dia_semana-1)+'T' +convert(varchar(8),B.hora_final)+',M'");
			lsConsulta.Append(" FROM horarios B ");
			lsConsulta.Append(" WHERE A.materia = B.materia AND A.grupo = B.grupo AND A.periodo = B.periodo");
			lsConsulta.Append(" FOR XML PATH('') ");
			lsConsulta.Append(" ), 1, 1, '') evento");
			lsConsulta.Append(" FROM ");
			lsConsulta.AppendFormat(" seleccion_materias A");
			lsConsulta.AppendFormat(" WHERE A.periodo = '{0}'", psPeriodo);
			lsConsulta.AppendFormat(" AND A.no_de_control = '{0}'", _oSistema.Sesion.Usuario.Usuario);
			lsConsulta.Append(" ) UNION (");

			lsConsulta.Append("SELECT A.periodo  ");
			lsConsulta.Append(" ,stuff(( ");
			lsConsulta.Append(" SELECT '|Inglés '+TRIM(B.grupo)+','+'2018-01-0'+ CONVERT(varchar(2),dia-1)+'T'+B.hora_inicial + ':00,' + '2018-01-0'+ CONVERT(varchar(2),dia-1)+'T' +B.hora_final+':00,I'");
			lsConsulta.Append(" FROM cle_horarios B ");
			lsConsulta.Append(" WHERE A.nivel = B.nivel AND A.grupo = B.grupo AND A.periodo = B.periodo");
			lsConsulta.Append(" FOR XML PATH('') ");
			lsConsulta.Append(" ), 1, 1, '') evento");
			lsConsulta.Append(" FROM ");
			lsConsulta.AppendFormat(" cle_seleccion A");
			lsConsulta.AppendFormat(" WHERE A.periodo = '{0}'", psPeriodo);
			lsConsulta.AppendFormat(" AND A.no_de_control = '{0}')", _oSistema.Sesion.Usuario.Usuario);

			lsConsulta.Append(" union( ");
			lsConsulta.Append(" SELECT A.periodo   ,stuff((  SELECT '|'+(select descripcion from extra_actividades where id = B.id_actividad)+','+'2018-01-0'+ ");
			lsConsulta.Append(" CONVERT(varchar(2),(case dia when 'LUN' then 1 when 'MAR' then 2 when 'MIE' then 3 when 'JUE'");
			lsConsulta.Append(" then 4 when 'VIE' then 5 when 'SAB' then 6 end))+'T'+B.hora_inicio + ':00,' + '2018-01-0'+ ");
			lsConsulta.Append(" TRIM(CONVERT(varchar(1),(case dia when 'LUN' then 1 when 'MAR' then 2 when 'MIE' then 3 when 'JUE' then 4 when 'VIE' then 5 when 'SAB' then 6 end)))+'T' +B.hora_fin+':00,X' ");
			lsConsulta.Append(" FROM extra_actividades_horarios B ");
			lsConsulta.Append(" WHERE A.actividad = B.id_actividad AND A.periodo = B.periodo FOR XML PATH('')  ), 1, 1, '') evento ");
			lsConsulta.AppendFormat(" FROM  extra_actividades_inscritos A WHERE A.periodo = '{0}' ", psPeriodo);
			lsConsulta.AppendFormat(" AND A.no_de_control = '{0}')", _oSistema.Sesion.Usuario.Usuario);

			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable RegresaTablaEstudiantesActividadesComplementarias()
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("SELECT periodo, actividad, semestre, promedio,isnull(desempeño,'PENDIENTE'), docente, descripcion ");
			lsConsulta.Append(" from estudiantes_actividades_complementarias");
			lsConsulta.AppendFormat(" where no_de_control = '{0}' ", _oSistema.Sesion.Usuario.Usuario);
			lsConsulta.Append(" ORDER BY periodo desc");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		/*public DataTable DatosEvaluacion(string psPeriodo, string psNoControl)
		{
			StringBuilder lsConsulta;
			DataTable loDt;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" select TOP 1 ea.no_de_control no_de_control, a.apellido_paterno + ' ' + a.apellido_materno + ' ' + a.nombre_alumno as nombre, c.nombre_carrera, ");
			lsConsulta.Append(" case when CHARINDEX('/', ea.fecha_hora_evaluacion) > 0 then convert(varchar,convert(date, ea.fecha_hora_evaluacion, 103), 103) else convert(varchar, convert(date, REPLACE(ea.fecha_hora_evaluacion,'-','/')), 103) end as fecha,  ");
			lsConsulta.Append(" case when CHARINDEX('/', ea.fecha_hora_evaluacion) > 0 then convert(varchar, convert (datetime2(3), ea.fecha_hora_evaluacion, 103), 14)  else convert(varchar, convert(datetime2(3), REPLACE(ea.fecha_hora_evaluacion,'-','/')), 14) end as hora, ");
			lsConsulta.Append(" p.identificacion_larga, (select cadena from sis_validacion_documentos where datos = ");
			lsConsulta.Append(" 'Numero de Control:' + trim(ea.no_de_control) + ");
			lsConsulta.Append(" '&Nombre del Alumno(a):' + a.apellido_paterno + ' ' + a.apellido_materno + ' ' + a.nombre_alumno  + ");
			lsConsulta.Append(" '&Carrera:' + c.nombre_carrera + ");
			lsConsulta.Append(" '&Fecha:' + case when CHARINDEX('/', ea.fecha_hora_evaluacion) > 0 then convert(varchar,convert(date, ea.fecha_hora_evaluacion, 103), 103) else convert(varchar, convert(date, REPLACE(ea.fecha_hora_evaluacion,'-','/')), 103) end +  ");
			lsConsulta.Append(" '&Hora:' + case when CHARINDEX('/', ea.fecha_hora_evaluacion) > 0 then convert(varchar, convert (datetime2(3), ea.fecha_hora_evaluacion, 103), 14)  else convert(varchar, convert(datetime2(3), REPLACE(ea.fecha_hora_evaluacion,'-','/')), 14) end +  ");
			lsConsulta.Append(" '&Periodo de Evaluación:' + p.identificacion_larga) as id_valida_documento, ");
			lsConsulta.Append(" CONVERT(varbinary(max),null) qr_valida_documento ");
			lsConsulta.Append(" from alumnos a, carreras c, evaluacion_alumnos ea, periodos_escolares p ");
			lsConsulta.Append(" where a.no_de_control = ea.no_de_control and ea.periodo = p.periodo and a.carrera = c.carrera and a.reticula = c.reticula and ");
			lsConsulta.AppendFormat(" ea.periodo = {0} and a.no_de_control = {1}", psPeriodo, psNoControl);
			loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}*/

		public DataTable DatosEvaluacion(string psPeriodo, string psNoControl)
		{
			DataTable ldtTabla = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
				loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
				ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_da_valida_evaluacion_docente", loParametros));
			}
			return ldtTabla;
		}

		public DataTable EvaluacionesAnteriores(string psNoControl)
		{
			StringBuilder lsConsulta;
			DataTable loDt;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select distinct(periodo), convert(varchar(10),fecha_hora_evaluacion,103) as fecha,convert(varchar(10),fecha_hora_evaluacion,108) as hora ");
			lsConsulta.AppendFormat(" from evaluacion_alumnos where no_de_control={0} order by periodo desc", psNoControl);
			loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}
		public DataTable RegresaDatosConstanciaLiberacionAC(string psNoControl)
		{
			StringBuilder lsConsulta;
			DataTable loDt;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select no_de_control, desempeño, promedio, folio, ");
			lsConsulta.Append("UPPER((select apellido_paterno +' '+ apellido_materno +' '+ nombre_alumno from alumnos where no_de_control = C.no_de_control)) nombre ");
			lsConsulta.Append(",UPPER((select top 1 nombre_carrera from carreras where carrera = (select carrera from alumnos where no_de_control = C.no_de_control))) carrera ");
			lsConsulta.Append(" from creditos_complementarios_liberados C");
			lsConsulta.AppendFormat(" where no_de_control = '{0}'", psNoControl);

			loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}
		#endregion
		#region Informacion Escolar
		public DataTable RegresaInformacionEscolar(string psNoControl)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActual));
			loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
			using (var loCOnexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_informacion_escolar", loParametros));
			}
			return loDt;
		}
		public DataTable RegresaQR(string psControl)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_alumnos_qr", loParametros));
			}
			loDt.Columns["qr"].ReadOnly = false;
			loDt.Rows[0].SetField("qr", loDt.Rows[0].Field<string>("cadena").RegresaQRValidacionDocumentos());

			return loDt;
		}

		#endregion
		#region Servicio Social
		public DataTable RegresaCartaSS(string psNoControl, string psFolio)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select a.folio, a.no_de_control, a.modalidad,(select nombre from estudiantes_datos_completos where no_de_control = a.no_de_control)+' '+ ");
			lsConsulta.Append(" (select paterno from estudiantes_datos_completos where no_de_control = a.no_de_control )+' '+");
			lsConsulta.Append(" (select materno from estudiantes_datos_completos where no_de_control = a.no_de_control ) as nombre_estudiante,");
			lsConsulta.Append(" (select nombre_carrera from estudiantes_datos_completos where no_de_control = a.no_de_control ) as carrera, b.nombre,");
			lsConsulta.Append(" (select titular from ss_dependencias where rfc =b.rfc) as titular,");
			lsConsulta.Append(" (select puesto_cargo from ss_dependencias where rfc =b.rfc) as puesto_cargo");
			lsConsulta.Append(" from ss_solicitud a, ss_programa b");
			lsConsulta.AppendFormat(" where a.no_de_control = '{0}' ", psNoControl);
			lsConsulta.AppendFormat(" and a.folio = '{0}'", psFolio);
			lsConsulta.Append(" and a.id_programa = b.id ");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaCartaSExternoSS(string psNoControl, string psDependencia)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" select a.folio, a.no_de_control, a.modalidad, (select nombre from estudiantes_datos_completos where no_de_control = a.no_de_control)+' '+ ");
			lsConsulta.Append(" (select paterno from estudiantes_datos_completos where no_de_control = a.no_de_control )+' '+");
			lsConsulta.Append(" (select materno from estudiantes_datos_completos where no_de_control = a.no_de_control ) as nombre_estudiante,");
			lsConsulta.Append(" (select nombre_carrera from estudiantes_datos_completos where no_de_control = a.no_de_control ) as carrera,");
			lsConsulta.Append(" b.titular, b.puesto_cargo");
			lsConsulta.Append(" from ss_solicitud a, ss_dependencias b");
			lsConsulta.AppendFormat(" where a.no_de_control = '{0}'", psNoControl);
			lsConsulta.AppendFormat(" and b.rfc ='{0}'", psDependencia);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable EvaluacionActividadesPrestador(string lsNumero, string psReporte)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" select distinct a.nombre_alumno, a.apellido_paterno, a.apellido_materno" +
				",d.no_de_control,e.identificacion_larga,c.nombre, d.no_reporte,d.p_1,d.p_2,d.p_3,d.p_4,d.p_5,d.p_6,d.p_7,p_8 from alumnos a, ss_solicitud b, ss_programa c, ss_reportes d, periodos_escolares e ");
			lsConsulta.AppendFormat(" where a.no_de_control=b.no_de_control and b.id_programa=c.id and d.no_de_control=b.no_de_control");
			lsConsulta.AppendFormat(" and b.no_de_control = '{0}'", lsNumero);
			lsConsulta.AppendFormat(" and d.no_reporte = {0}", psReporte);
			lsConsulta.AppendFormat(" and b.periodo=e.periodo");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaPlanTrabajo(string loNoControl)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@no_de_control", loNoControl));
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_ss_plan_trabajo", loParametros));
			}

			return loDt;
		}
		#endregion
		#region Residencias Profesionales
		public DataTable RegresaCartaRP(string psNoControl, string psDependencia)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" SELECT A.titular, A.puesto_cargo, A.dependencia, B.no_de_control,");
			lsConsulta.Append(" (select nombre_alumno from alumnos where no_de_control = B.no_de_control)+' '+(select apellido_paterno from alumnos where no_de_control = B.no_de_control)+' '+(select apellido_materno from alumnos where no_de_control = B.no_de_control) as nombre_residente,");
			lsConsulta.Append(" (select nombre_carrera from estudiantes_datos_completos where no_de_control = B.no_de_control) as nombre_carrera");
			lsConsulta.Append(" from rp_dependencias A, rp_estado_solicitud B");
			lsConsulta.AppendFormat(" where A.rfc = '{0}'", psDependencia);
			lsConsulta.AppendFormat(" and B.no_de_control = '{0}'", psNoControl);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaSolicitud(string loNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("SELECT(select identificacion_larga from periodos_escolares where periodo = A.periodo_datos) as identificacion_larga, ");
			lsConsulta.Append(" B.id,B.nombre,B.responsable,B.cargo,B.opcion_programa, B.rfc_dependencia as rfc,");
			lsConsulta.Append(" (select dependencia from rp_dependencias where rfc = B.rfc_dependencia) as dependencia,");
			lsConsulta.Append(" (select giro from rp_dependencias where rfc = B.rfc_dependencia) as giro,");
			lsConsulta.Append(" (select titular from rp_dependencias where rfc = B.rfc_dependencia) as titular,");
			lsConsulta.Append(" (select puesto_cargo from rp_dependencias where rfc = B.rfc_dependencia) as puesto_cargo,");
			lsConsulta.Append(" (select telefono from rp_dependencias where rfc = B.rfc_dependencia) as telefono,");
			lsConsulta.Append(" (select mision from rp_dependencias where rfc = B.rfc_dependencia) as mision,");
			lsConsulta.Append(" (select domicilio from rp_domicilio_dependencias where rfc_domicilio = B.rfc_dependencia) as domicilio,");
			lsConsulta.Append(" (select numero from rp_domicilio_dependencias where rfc_domicilio = B.rfc_dependencia) as numero,");
			lsConsulta.Append(" (select ciudad_localidad from c_p where id in (select id_cp from rp_domicilio_dependencias where rfc_domicilio = B.rfc_dependencia)) as ciudad_depdendencia,");
			lsConsulta.Append(" (select cod_post from c_p where id in (select id_cp from rp_domicilio_dependencias where rfc_domicilio = B.rfc_dependencia)) as codigo_dependencia,");
			lsConsulta.Append(" (select nombre + ' ' + paterno + ' ' + materno from estudiantes_datos_completos where no_de_control = A.no_de_control) as nombre_residente,");
			lsConsulta.Append(" (select nombre_carrera from estudiantes_datos_completos where no_de_control = A.no_de_control) as nombre_carrera, A.no_de_control,");
			lsConsulta.Append(" (select calle from alumnos_domicilios where no_de_control = A.no_de_control) as calle,");
			lsConsulta.Append(" (select numero from alumnos_domicilios where no_de_control = A.no_de_control) as numero_alumno,");
			lsConsulta.Append(" (select ciudad_localidad from c_p where id in (select id_cp from alumnos_domicilios where no_de_control = A.no_de_control)) as ciudad_alumno,");
			lsConsulta.Append(" (select correo from estudiantes_datos_completos where no_de_control = A.no_de_control) as correo,");
			lsConsulta.Append(" (select telefono from estudiantes_datos_completos where no_de_control = A.no_de_control)as telefono_alumno,");
			lsConsulta.Append(" (select count(no_de_control) from rp_solicitud where estado_solicitud = 1 and id_programa  in (select id_programa from rp_solicitud where no_de_control = A.no_de_control)) as residentes,");
			lsConsulta.Append(" (select nombre_carrera from estudiantes_datos_completos where no_de_control = A.no_de_control) as nombre_carrera");
			lsConsulta.Append(" from rp_solicitud A inner join rp_programa B on A.id_programa = B.id");
			lsConsulta.AppendFormat(" where A.no_de_control = '{0}' and A.estado_solicitud = 1", loNoControl);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaEvaluacionSeguimiento(string lsNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" SELECT (select nombre+' '+paterno+' '+materno from estudiantes_datos_completos where no_de_control = A.no_de_control) as nombre_residente,");
			lsConsulta.Append(" A.no_de_control, (select nombre from rp_programa where id = A.id_programa) as nombre,");
			lsConsulta.Append(" (select responsable from rp_programa where id = A.id_programa) as responsable,");
			lsConsulta.Append(" (select identificacion_larga from periodos_escolares where periodo = A.periodo_datos) as identificacion_larga,");
			lsConsulta.Append(" (select nombre_empleado+' '+apellidos_empleado from personal where rfc in (select rfc_asesor from rp_datos_programa where id_programa = A.id_programa)) as asesor_interno");
			lsConsulta.Append(" from rp_solicitud A");
			lsConsulta.AppendFormat(" where A.no_de_control = '{0}'", lsNoControl);
			lsConsulta.Append(" and A.estado_solicitud = 1");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaAsesoria(int piNumeroAsesoria, string psNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" SELECT (select nombre from estudiantes_datos_completos where no_de_control = A.no_de_control)+' '+");
			lsConsulta.Append(" (select paterno from estudiantes_datos_completos where no_de_control = A.no_de_control)+' '+");
			lsConsulta.Append(" (select materno from estudiantes_datos_completos where no_de_control = A.no_de_control) as nombre_residente,");
			lsConsulta.Append(" (select carrera from estudiantes_datos_completos where no_de_control = A.no_de_control) as carrera,");
			lsConsulta.Append(" A.no_de_control, A.numero_asesoria, A.tipo,A.descripcion,A.solucion,");
			lsConsulta.Append(" (select nombre_empleado+' '+ apellidos_empleado from personal where rfc in (select rfc_asesor from rp_datos_programa where id_programa = B.id_programa)) as asesor_interno,");
			lsConsulta.Append(" (select identificacion_larga from periodos_escolares where periodo = A.periodo_asesorias) as periodo_asesorias");
			lsConsulta.Append(" from rp_asesorias A inner join rp_solicitud B on A.no_de_control = B.no_de_control");
			lsConsulta.AppendFormat(" where A.no_de_control = {0}", psNoControl);
			lsConsulta.AppendFormat(" and A.numero_asesoria = {0}", piNumeroAsesoria);
			lsConsulta.Append(" and B.estado_solicitud = 1");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaInformeSemestral(string lsNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" SELECT A.id_programa, (select nombre from rp_programa where id = A.id_programa) as nombre,");
			lsConsulta.Append(" (select dependencia from rp_dependencias where rfc in (select rfc_dependencia from rp_programa where id = A.id_programa)) as dependencia,");
			lsConsulta.Append(" (select carrera from carreras where reticula in (select reticula from alumnos where no_de_control = A.no_de_control) and carrera in (select carrera from alumnos where no_de_control = A.no_de_control)) as nombre_carrera");
			lsConsulta.Append(" from rp_solicitud A");
			lsConsulta.AppendFormat(" where A.estado_solicitud = 1 and A.no_de_control = '{0}'", lsNoControl);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}

		public DataTable RegresaAsesoriasInforme(string lsNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" SELECT numero_asesoria, descripcion");
			lsConsulta.Append(" from rp_asesorias");
			lsConsulta.AppendFormat(" where no_de_control = '{0}'", lsNoControl);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaLiberacionInforme(string psNoControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" SELECT A.id_programa, (select nombre from rp_programa where id = A.id_programa) as nombre,");
			lsConsulta.Append(" (select nombre_empleado+' '+apellidos_empleado from personal where rfc in (select rfc_revisor from rp_datos_programa where id_programa = A.id_programa)) as rfc_revisor,");
			lsConsulta.Append(" (select nombre_carrera from carreras where reticula in (select reticula from alumnos where no_de_control = A.no_de_control)) as nombre_carrera");
			lsConsulta.Append(" FROM rp_solicitud A");
			lsConsulta.AppendFormat(" where A.no_de_control = '{0}'", psNoControl);
			lsConsulta.Append(" and A.estado_solicitud = 1");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaCuentas(string psNoControl = null)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append(" SELECT usuario, contraseña, descripcion, tipo, url");
			lsConsulta.Append(" FROM alumnos_cuentas A inner join sis_alumnos_cuentas S");
			lsConsulta.Append(" on tipo_cuenta = clave");
			lsConsulta.Append(" where activo = 0");
			if (!string.IsNullOrEmpty(psNoControl))
			{
				lsConsulta.AppendFormat(" and no_de_control = '{0}'",psNoControl);
			}
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		#endregion
		#region FiEl
		public void RegistraCadenaFiEl(enmTiposDocumentos penmTipo)
		{


			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

			loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActual));
			loParametros.Add(new ParametrosSQL("@no_de_control", _oSistema.Sesion.Usuario.Usuario));
			loParametros.Add(new ParametrosSQL("@tipo_documento", penmTipo.ToString()));

			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pai_fiel_registra_alumno", loParametros);
		}

		public void RegistraCadenaFiElExpDig(string tipoDocumento)
		{			
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@no_de_control", _oSistema.Sesion.Usuario.Usuario));
			loParametros.Add(new ParametrosSQL("@nombre_doc", tipoDocumento.ToString()));
			
			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pai_exp_dig_alu_fiel_doc_inscripcion", loParametros);
		}
        #endregion
        #region Boleta

        public DataTable RegresaDatosEncabezadoBoleta(string psNoControl, string psPeriodo )
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_control", psNoControl));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_datosalumno", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaDatosCuerpoBoleta(string psPeriodo, string psNoControl)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_alumnos_boleta_calificaciones", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaDatosAcumuladoBoleta(string psNoControl, string psPeriodo)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {       
                loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
                loParametros.Add(new ParametrosSQL("@periodo_fin", psPeriodo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_datosacumuladohistorico", loParametros));
            }
            return ldtTabla;
        }

        #endregion 
    }
}
