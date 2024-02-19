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
    /// Clase personal generada automáticamente desde el Generador de Código SII
    /// </summary>
    public partial class Personal
    {
        #region variables
        List<ParametrosSQL> _oSqlParametros;
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

        public DataTable RegresaHorario(string psPeriodo)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
            lsConsulta.Append("(SELECT D.periodo,");
            lsConsulta.Append(" stuff(( SELECT '|'+(select nombre_abreviado_materia from materias where materia = B.materia)+' / '+B.aula+',2018-01-0'+ CONVERT(varchar(2),dia_semana-1)+'T' +convert(varchar(8),B.hora_inicial) + ',' + '2018-01-0'+ CONVERT(varchar(2),dia_semana-1)+'T' +convert(varchar(8),B.hora_final)+',M'");
            lsConsulta.Append(" FROM horarios B");
            lsConsulta.Append(" WHERE D.materia = B.materia AND D.grupo = B.grupo AND D.periodo = B.periodo ");
            lsConsulta.Append(" FOR XML PATH('') ");
            lsConsulta.Append(" ), 1, 1, '') evento ");
            lsConsulta.Append(" FROM grupos D");
            lsConsulta.Append(" WHERE D.periodo = @periodo");
            lsConsulta.AppendFormat(" AND D.rfc = '{0}'", _oSistema.Sesion.Usuario.Usuario);
            lsConsulta.Append(" AND D.materia not in ('RESP07', 'RESP08', 'RPE-01', 'RPM-02', 'RPM-05', 'RPS-03', 'RPS-06', 'RPSC-03', 'RPSS03')");

            lsConsulta.Append(" ) UNION (");
            lsConsulta.Append("SELECT D.periodo, ");
            lsConsulta.Append(" stuff(( SELECT '|Tutorias '+TRIM(B.grupo)+','+'2018-01-0'+ CONVERT(varchar(2),(case dia when 'LUN' then 1 when 'MAR' then 2 when 'MIE' then 3 when 'JUE' then 4 when 'VIE' then 5 when 'SAB' then 6 end))+'T'+");
            lsConsulta.Append("B.hora_inicio + ':00,' + '2018-01-0'+  TRIM(CONVERT(varchar(1),(case dia when 'LUN' then 1 when 'MAR' then 2 when 'MIE' then 3 when 'JUE' then 4 when 'VIE' then 5 when 'SAB' then 6 end)))+'T' +B.hora_fin+':00,T'");
            lsConsulta.Append(" FROM tutorias_horarios B ");
            lsConsulta.Append(" WHERE D.grupo = B.grupo AND D.periodo = B.periodo");
            lsConsulta.Append(" FOR XML PATH('') ");
            lsConsulta.Append(" ), 1, 1, '') evento");
            lsConsulta.Append(" FROM ");
            lsConsulta.Append("tutorias_grupos D");
            lsConsulta.Append(" WHERE D.periodo = @periodo");
            lsConsulta.AppendFormat(" AND D.rfc = '{0}'", _oSistema.Sesion.Usuario.Usuario);

            lsConsulta.Append(" ) UNION ( ");
            lsConsulta.Append(" SELECT D.periodo   ,stuff((  SELECT '|'+(select descripcion from extra_actividades where id = B.id_actividad)+','+'2018-01-0'+ ");
            lsConsulta.Append(" CONVERT(varchar(2),(case dia when 'LUN' then 1 when 'MAR' then 2 when 'MIE' then 3 when 'JUE' then 4 when 'VIE' then 5 when 'SAB' then 6 end))+'T'+B.hora_inicio + ':00,' + '2018-01-0'+ ");
            lsConsulta.Append(" TRIM(CONVERT(varchar(1),(case dia when 'LUN' then 1 when 'MAR' then 2 when 'MIE' then 3 when 'JUE' then 4 when 'VIE' then 5 when 'SAB' then 6 end)))+'T' +B.hora_fin+':00,X' ");
            lsConsulta.Append(" FROM extra_actividades_horarios B ");
            lsConsulta.Append(" WHERE D.id = B.id_actividad AND D.periodo = B.periodo FOR XML PATH('')  ), 1, 1, '') evento ");
            lsConsulta.Append(" FROM  extra_actividades D WHERE D.periodo = @periodo ");
            lsConsulta.AppendFormat(" AND D.id_entrenador in (select id from extra_entrenador where usuario = '@'+'{0}')) ", _oSistema.Sesion.Usuario.Usuario);

            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        #region CapturaCalificaciones

        public DataTable RegresaGrupos(string lsPeriodo, string lsUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@usuario", lsUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_doc_grupos_calificaciones", loParametros));
            }
            return ldtTabla;
        }

    
        public DataTable RegresaAlumnos(string lsPeriodo, string lsMateria, string lsGrupo)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@materia", lsMateria));
                loParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_doc_regresa_lista_alumnos", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaObjeto(string lsPeriodo, string lsMateria, string lsGrupo, string lsUsuario)
        {
            StringBuilder lsTabla;
            lsTabla = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
            _oSqlParametros.Add(new ParametrosSQL("@materia", lsMateria));
            _oSqlParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
            lsTabla.Append("select 'captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '={ periodo:\"'+ trim(periodo) +'\" }; ");
            lsTabla.Append("captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '.no_de_control=\"'+ trim(no_de_control) +'\"; ");
            lsTabla.Append("captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '.materia=\"'+ trim(materia) +'\";");
            lsTabla.Append("captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '.grupo=\"'+ trim(grupo)+ '\"; ");
            lsTabla.Append("captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '.repeticion=\"'+ trim(repeticion)+ '\"; ");
            lsTabla.Append("captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '.status_seleccion=\"'+ trim(status_seleccion)+ '\"; ");
            lsTabla.Append("captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '.fecha_hora_seleccion=\"\"; ");
            lsTabla.AppendFormat("captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '.usuario=\"{0}\"; ", lsUsuario);
            lsTabla.Append("captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '.lugar=\"\"; ");
            lsTabla.Append("captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '.tipo_operacion=\"C\"; ");
            lsTabla.Append("captura.'+ trim(replace(materia, '-', '')) + '_' + trim(grupo) + '_' + trim(no_de_control) + '.motivo=\"\";' ");
            lsTabla.Append("from seleccion_materias where ");
            lsTabla.Append("periodo = @periodo AND materia = @materia AND grupo = @grupo ");

            DataTable loDT;
            loDT = _oSistema.Conexion.RegresaDataTable(lsTabla, _oSqlParametros);
            return loDT;
        }

        #endregion

        #region Calificaciones
        public DataTable RegresaDatosActa(string lsPeriodo, string lsMateria, string lsGrupo)
        {
            StringBuilder lsTabla;
            lsTabla = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
            _oSqlParametros.Add(new ParametrosSQL("@materia", lsMateria));
            _oSqlParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
            lsTabla.Append("select ");
            lsTabla.Append("(select identificacion_larga from periodos_escolares where periodo = G.periodo ) identificacion_periodo, ");
            lsTabla.Append("G.folio_acta, ");
            lsTabla.Append("(select nombre_completo_materia from materias where materia = G.materia) nombre_materia, ");
            lsTabla.Append("(select descripcion_area from organigrama where clave_area in (select area_academica from personal where rfc = G.rfc)) departamento, ");
            lsTabla.Append("G.materia, G.grupo, ");
            lsTabla.Append("isnull((select apellidos_empleado + ' ' + nombre_empleado from personal where rfc = G.rfc), '') nombre_empleado, ");
            lsTabla.Append("(select count(no_de_control) from seleccion_materias where periodo = G.periodo and materia = G.materia and grupo=G.grupo) no_alumnos, ");
            lsTabla.Append("isnull((select convert(varchar,hora_inicial,8)+' - '+ convert(varchar,hora_final,8) + '/' + aula from horarios where periodo = G.periodo AND materia = G.materia and grupo = G.grupo and dia_semana = 2),'') lunes, ");
            lsTabla.Append("isnull((select convert(varchar,hora_inicial,8)+' - '+ convert(varchar,hora_final,8) + '/' + aula from horarios where periodo = G.periodo AND materia = G.materia and grupo = G.grupo and dia_semana = 3),'') martes, ");
            lsTabla.Append("isnull((select convert(varchar,hora_inicial,8)+' - '+ convert(varchar,hora_final,8) + '/' + aula from horarios where periodo = G.periodo AND materia = G.materia and grupo = G.grupo and dia_semana = 4),'') miercoles, ");
            lsTabla.Append("isnull((select convert(varchar,hora_inicial,8)+' - '+ convert(varchar,hora_final,8) + '/' + aula from horarios where periodo = G.periodo AND materia = G.materia and grupo = G.grupo and dia_semana = 5),'') jueves, ");
            lsTabla.Append("isnull((select convert(varchar,hora_inicial,8)+' - '+ convert(varchar,hora_final,8) + '/' + aula from horarios where periodo = G.periodo AND materia = G.materia and grupo = G.grupo and dia_semana = 6),'') viernes ");
            lsTabla.Append("from grupos G where G.periodo = @periodo and G.grupo = @grupo and G.materia = @materia");
            DataTable loDT;
            loDT = _oSistema.Conexion.RegresaDataTable(lsTabla, _oSqlParametros);
            return loDT;
        }

        public DataTable RegresaAlumnosActa(string lsPeriodo, string lsMateria, string lsGrupo)
        {
            StringBuilder lsTabla;
            lsTabla = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
            _oSqlParametros.Add(new ParametrosSQL("@materia", lsMateria));
            _oSqlParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
            lsTabla.Append("select ROW_NUMBER() OVER(ORDER BY a.apellido_paterno ASC) AS Row, ");
            lsTabla.Append("sm.no_de_control, ");
            lsTabla.Append("a.apellido_paterno + ' ' + a.apellido_materno + ' ' + a.nombre_alumno as nombre, ");
            lsTabla.Append("(select carrera from carreras where carrera = a.carrera and reticula = a.reticula) as carrera, ");
            lsTabla.Append("sm.repeticion, sm.tipo_evaluacion, ");
            lsTabla.Append("'ord' = (case tipo_evaluacion when 'OO'  then (case  when calificacion < 70 then 'NA' else convert(varchar,calificacion) end) when 'RO' then (case when calificacion < 70 then 'NA' else convert(varchar,calificacion) end) else 'NA' end), ");
            lsTabla.Append("'rep' = (case tipo_evaluacion when 'OC' then (case  when calificacion < 70 then 'NA' else convert(varchar,calificacion) end) when 'RC' then (case  when calificacion < 70 then 'NA' else convert(varchar,calificacion) end) " +
                "when 'RP' then (case  when calificacion < 70 then 'NA' else convert(varchar,calificacion) end) when 'CE' then (case  when calificacion < 70 then 'NA' else convert(varchar,calificacion) end) else '' end),");
            lsTabla.Append("'esp' = (case tipo_evaluacion when 'CE' then (case  when calificacion < 70 then 'NA' else convert(varchar,calificacion) end) else '' end)  ");
            lsTabla.Append("from seleccion_materias sm, alumnos a ");
            lsTabla.Append("where a.no_de_control = sm.no_de_control and sm.periodo = @periodo and sm.materia = @materia and sm.grupo = @grupo ");
            lsTabla.Append("order by nombre asc");
            DataTable loDT;
            loDT = _oSistema.Conexion.RegresaDataTable(lsTabla, _oSqlParametros);
            return loDT;
        }

        public DataTable RegresaBotonReporteFinal(string psPeriodo, string psUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_verifica_grupos_calificados", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaDatosReporteFinal(string psPeriodo, string psUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_datos_reporte_final", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaMateriasReporteFinal(string psPeriodo, string psUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_calculos_reporte_final", loParametros));
            }
            return ldtTabla;
        }
        public DataTable RegresaTotalesReporteFinal(string psPeriodo, string psUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_total_reporte_final", loParametros));
            }
            return ldtTabla;
        }
        #endregion

        #region ListasExcel
        public DataTable RegresaDatosExcel(string lsPeriodo, string lsMateria, string lsGrupo)
        {
            StringBuilder lsTabla;
            lsTabla = new StringBuilder();
            lsTabla = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
            _oSqlParametros.Add(new ParametrosSQL("@materia", lsMateria));
            _oSqlParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
            lsTabla.Append("select ");
            lsTabla.Append("(select identificacion_larga from periodos_escolares where periodo = G.periodo ) identificacion_periodo, ");
            lsTabla.Append("(select nombre_completo_materia from materias where materia = G.materia) nombre_materia, ");
            lsTabla.Append("(select descripcion_area from organigrama where clave_area in (select area_academica from personal where rfc = G.rfc)) departamento, ");
            lsTabla.Append("G.grupo, ");
            lsTabla.Append("isnull((select nombre_empleado + ' ' + apellidos_empleado from personal where rfc = G.rfc), '') nombre_empleado, ");
            lsTabla.Append("isnull((select convert(varchar,hora_inicial,8)+' - '+ convert(varchar,hora_final,8) + '/' + aula from horarios where periodo = G.periodo AND materia = G.materia and grupo = G.grupo and dia_semana = 2),'') lunes, ");
            lsTabla.Append("isnull((select convert(varchar,hora_inicial,8)+' - '+ convert(varchar,hora_final,8) + '/' + aula from horarios where periodo = G.periodo AND materia = G.materia and grupo = G.grupo and dia_semana = 3),'') martes, ");
            lsTabla.Append("isnull((select convert(varchar,hora_inicial,8)+' - '+ convert(varchar,hora_final,8) + '/' + aula from horarios where periodo = G.periodo AND materia = G.materia and grupo = G.grupo and dia_semana = 4),'') miercoles, ");
            lsTabla.Append("isnull((select convert(varchar,hora_inicial,8)+' - '+ convert(varchar,hora_final,8) + '/' + aula from horarios where periodo = G.periodo AND materia = G.materia and grupo = G.grupo and dia_semana = 5),'') jueves, ");
            lsTabla.Append("isnull((select convert(varchar,hora_inicial,8)+' - '+ convert(varchar,hora_final,8) + '/' + aula from horarios where periodo = G.periodo AND materia = G.materia and grupo = G.grupo and dia_semana = 6),'') viernes ");
            lsTabla.Append("from grupos G where G.periodo = @periodo and G.grupo=@grupo and G.materia = @materia");
            DataTable loDT;
            loDT = _oSistema.Conexion.RegresaDataTable(lsTabla, _oSqlParametros);
            return loDT;
        }

        public DataTable RegresaAlumnosExcel(string lsPeriodo, string lsMateria, string lsGrupo)
        {
            StringBuilder lsTabla;
            lsTabla = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
            _oSqlParametros.Add(new ParametrosSQL("@materia", lsMateria));
            _oSqlParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
            lsTabla.Append("select ROW_NUMBER() OVER(ORDER BY a.apellido_paterno ASC) AS 'N. de Lista', ");
            lsTabla.Append("a.apellido_paterno + ' ' + a.apellido_materno + ' ' + a.nombre_alumno as 'Nombre de Alumno', ");
            lsTabla.Append("sm.no_de_control as 'No. de Control',");
			lsTabla.Append("(select usuario from alumnos_cuentas where no_de_control = sm.no_de_control and tipo_cuenta = 1) as 'Correo'");
			lsTabla.Append("from seleccion_materias sm, alumnos a ");
            lsTabla.Append("where a.no_de_control = sm.no_de_control and sm.periodo = @periodo and sm.materia = @materia and sm.grupo = @grupo ");
            lsTabla.Append("order by 'Nombre de Alumno' asc");
            DataTable loDT;
            loDT = _oSistema.Conexion.RegresaDataTable(lsTabla, _oSqlParametros);
            return loDT;
        }

        #endregion

        #region Facilitador

        public DataTable RegresaGruposFacilitador(string lsPeriodo, string lsUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@usuario", lsUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_fcl_grupos_calificaciones", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaDatosFacilitadorExcel(string lsPeriodo, string lsNivel, string lsGrupo)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@nivel", lsNivel));
                loParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_fcl_datos_facilitador_excel", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaAlumnosFacilitadorExcel(string lsPeriodo, string lsNivel, string lsGrupo)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@nivel", lsNivel));
                loParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_fcl_alumnos_facilitador_excel", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaAlumnosFacilitador(string lsPeriodo, string lsNivel, string lsGrupo)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@nivel", lsNivel));
                loParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_fcl_alumnos_facilitador", loParametros));
            }
            return ldtTabla;
        }


        public DataTable RegresaObjetoCapturaFacilitador(string lsPeriodo, string lsNivel, string lsGrupo)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@nivel", lsNivel));
                loParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_fcl_objeto_facilitador", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaDatosActaFacilitador(string lsPeriodo, string lsNivel, string lsGrupo, string lsFacilitador)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@nivel", lsNivel));
                loParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
                loParametros.Add(new ParametrosSQL("@facilitador", lsFacilitador));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_fcl_datos_acta", loParametros));
            }
            return ldtTabla;
        }
        public DataTable RegresaAlumnosActaFacilitador(string lsPeriodo, string lsNivel, string lsGrupo)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                loParametros.Add(new ParametrosSQL("@nivel", lsNivel));
                loParametros.Add(new ParametrosSQL("@grupo", lsGrupo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_fcl_alumnos_acta", loParametros));
            }
            return ldtTabla;
        }
        #endregion

        #region RegistroPersonal
        public DataTable RegresaNivelEstudios()
        {
            StringBuilder lsConsulta;
            DataTable loDt;
            lsConsulta = new StringBuilder();
            lsConsulta.Append("SELECT nivel_estudios,descripcion_nivel_estudios ");
            lsConsulta.Append("FROM nivel_de_estudios ");
            loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
            return loDt;
        }

        public DataTable RegresaPersonalDatos(string psRFC)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            DataTable loDT = new DataTable();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@rfc", psRFC));
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loDT.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_datos_personal", _oSqlParametros));
            }
            return loDT;
        }
        #endregion

        #region MarcadoParaEliminar


        public DataTable RegresaPersonalActivoPuestos()
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append(" select rfc, nombre_empleado + ' ' + apellido_paterno + ' ' + apellido_materno as nombre,  ");
            lsConsulta.Append(" case when tipo_personal = 'B' then 'Base' when tipo_personal = 'X' then 'Mixto' else 'Honorarios' end as tipo_personal, ");
            lsConsulta.Append("'<a class=\"btn btn-success\" href=\"/RecursosHumanos/PuestosPersonal?psRFC='+ rfc +'\" data-ajax-update=\"#BodyPrincipal\" data-ajax-mode=\"replace\" data-ajax-method=\"GET\" data-ajax=\"true\"><i class=\"fa fa-eye\"></i></a>' as boton");
            lsConsulta.Append(" from personal where status_empleado = '02' ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);
        }
        #endregion

        #region Recontratacion

        

        public DataTable RegresaHorarioSimple(int psID, string psPeriodo, string psRFC)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id", psID));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_horas_personal_grupos", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaHorarioPorHora(string psPeriodo, int psID)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@id", psID));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_horas_personal_facilitador", loParametros));
            }
            return ldtTabla;
        }
        
        public DataTable RegresaHorarioAdministrativo(int psID, string psPeriodo, string psRFC)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id", psID));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_horas_personal_administrativo", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaHorarioJefes(string lsPeriodo,string psUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
            loParametros.Add(new ParametrosSQL("@usuario", psUsuario));

            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_horarios_jefes", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaRFCJefe(string psUsusario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            loParametros.Add(new ParametrosSQL("@usuario", psUsusario));
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_jefe_rfc", loParametros));
            }
            return ldtTabla;
        }

        public DataTable VerificaExistenciaHorarioJefe(string psPeriodo, string psRFC)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
            loParametros.Add(new ParametrosSQL("@rfc", psRFC));
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_verifica_horario_jefe", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaPersonalDatosRegistro(string psRFC)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_datos_registro", loParametros));
            }
            return ldtTabla;
        }

        //
        public DataTable RegresaPersonalDatosModificacion(int psID)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id", psID));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_datos_modificacion", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaPersonalSolicitados()
        {
            DataTable ldtTabla = new DataTable();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_autorizar"));
            }
            return ldtTabla;
        }
        
        public DataTable RegresaClaveArea(string psUsuario)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@usuario", psUsuario));
            lsConsulta.Append(" select clave_area from jefes where usuario = @usuario ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable RegresaPersonalSolicitadoStatus(string psUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_status_seleccion", loParametros));
            }
            return ldtTabla;
        }
        
        public DataTable RegresaPersonalAutorizadoRecontratacion(string psUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_autorizado_recontratacion", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaDatosHorarioPersonalRecontratacion(int psID) 
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id", psID));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_datos_horario_laboral", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaPersonalHorarioAutorizado(string psUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_horario_autorizado", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaPersonalHorarioJefes(string psUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_horario_jefes", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaPersonalConVB()
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_con_vb"));
            }
            return ldtTabla;
        }

        public DataTable RegresaDatosPersonalContratoAsimilados(string psPeriodo, string psRFC)
        {

            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
            _oSqlParametros.Add(new ParametrosSQL("@rfc", psRFC));
            lsConsulta.Append(" select p.nombre_empleado + ' ' + p.apellido_paterno + ' ' + p.apellido_materno as nombre, ");
            lsConsulta.Append(" pp.nombre_carrera, pp.nacionalidad, p.rfc, ");
            lsConsulta.Append(" STUFF((select ', ' + DATENAME(DAY, fecha)  + ' de ' + DATENAME(MONTH, fecha) + ' de ' + DATENAME(YEAR, fecha) FROM  personal_fechas_pago where periodo = @periodo and tipo_personal = 'B' order by id_fecha asc  FOR XML PATH('')), 1, 1, '') fechas, ");
            lsConsulta.Append(" rtrim(pd.calle) calle, rtrim(pd.numero) numero, rtrim(cp.colonia) colonia, cp.cod_post, rtrim(cp.ciudad_localidad) ciudad_localidad, rtrim(ef.nombre_entidad) nombre_entidad, ");
            lsConsulta.Append(" case pp.genero when 'M' then 'FEMENINO' when 'H' then 'MASCULINO'end as genero, pp.telefono, ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and periodo = @periodo and dia_semana = 2) as seconds), '') + ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and periodo = @periodo and dia_semana = 3) as seconds), '')  + ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and periodo = @periodo and dia_semana = 4) as seconds), '') + ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and periodo = @periodo and dia_semana = 5) as seconds), '') + ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and periodo = @periodo and dia_semana = 6) as seconds), '') +  ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and periodo = @periodo and dia_semana = 7) as seconds), '') +  ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and periodo = @periodo and dia_semana = 8) as seconds), '')   ");
            lsConsulta.Append(" as horas_semanales,  DATENAME(DAY, ps.periodo_inicio)  + ' de ' + DATENAME(MONTH, ps.periodo_inicio) + ' de ' + DATENAME(YEAR, ps.periodo_inicio) periodo_inicio, DATENAME(DAY, ps.periodo_fin)  + ' de ' + DATENAME(MONTH, ps.periodo_fin) + ' de ' + DATENAME(YEAR, ps.periodo_fin) periodo_fin, ");
            lsConsulta.Append(" (select jefe_area from jefes where clave_area = ps.clave_area) jefe ");
            lsConsulta.Append(" from personal p, personal_domicilios pd, c_p cp, entidades_federativas ef, personal_personales pp, personal_solicitud ps ");
            lsConsulta.Append(" where p.rfc = pd.rfc and pd.colonia = cp.id and cp.ent_fed = ef.entidad_federativa and p.rfc = pp.rfc and p.rfc = ps.rfc and p.rfc = @rfc and ps.periodo = @periodo");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable RegresaDatosPersonalContratoHonorarios(int psID)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id", psID));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_datos_contrato_facilitador", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaDatosPersonalContratoGeneralFE(int psID)
        {
                DataTable ldtTabla = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                using (var loConexion = new ManejaConexion(_oSistema.Conexion))
                {
                loParametros.Add(new ParametrosSQL("@id", psID));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_datos_contrato_general", loParametros));
                }
                return ldtTabla;
            }

        //RegresaDatosContratoFacilitadorCLE
        public DataTable RegresaDatosPersonalContratoFacilitadoresCLE(int psID)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id", psID));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_datos_contrato_facilitador_cle", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaDatosHorarioPersonalFacilitadorFE(int psID)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id", psID));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_datos_horario_laboral_facilitador", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaHorarioFacilitador(int psID)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id", psID));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_horas_facilitador", loParametros));
            }
            return ldtTabla;
        }


        /* public DataTable RegresaDatosPersonalContratoGeneralFE(string psFechaCreacion, string psClaveArea, string psRFC, string psTipoContrato)
         {
             StringBuilder lsConsulta;
             lsConsulta = new StringBuilder();
             _oSqlParametros = new List<ParametrosSQL>();
             _oSqlParametros.Add(new ParametrosSQL("@fecha_creacion", psFechaCreacion));
             _oSqlParametros.Add(new ParametrosSQL("@clave_area", psClaveArea));
             _oSqlParametros.Add(new ParametrosSQL("@rfc", psRFC));
             _oSqlParametros.Add(new ParametrosSQL("@tipo_contrato", psTipoContrato));
             lsConsulta.Append(" SET Language 'Spanish'; with datos_contrato (nombre, calle, numero, colonia, cod_post, ciudad_localidad, ");
             lsConsulta.Append(" nombre_entidad, genero, telefono, rfc, curp,  horas_semanales, monto, periodo_inicio, periodo_fin, jefe, cadena_personal, qr_personal, cadena_jefe, qr_jefe, cadena_sub_admin, qr_sub_admin, cadena_rh, qr_rh, cadena_dir, qr_dir, id_valida_documento, qr_valida_documento, fecha_firma) as ( ");
             lsConsulta.Append(" select p.nombre_empleado + ' ' + p.apellido_paterno + ' ' + p.apellido_materno as nombre, ");
             lsConsulta.Append(" upper(rtrim(pd.calle)) calle, upper(rtrim(pd.numero)) numero, upper(rtrim(cp.colonia)) colonia, cp.cod_post, upper(rtrim(cp.ciudad_localidad)) ciudad_localidad, upper(rtrim(ef.nombre_entidad)) nombre_entidad, ");
             lsConsulta.Append(" case pp.genero when 'M' then 'FEMENINO' when 'H' then 'MASCULINO'end as genero, rtrim(pp.telefono) telefono, p.rfc, pp.curp, ");
             lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
             lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and fecha_creacion = @fecha_creacion and clave_area = @clave_area and tipo_horario = @tipo_contrato and dia_semana = 2) as seconds), '') + ");
             lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
             lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and fecha_creacion = @fecha_creacion and clave_area = @clave_area and tipo_horario = @tipo_contrato and dia_semana = 3) as seconds), '')  + ");
             lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
             lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and fecha_creacion = @fecha_creacion and clave_area = @clave_area and tipo_horario = @tipo_contrato and dia_semana = 4) as seconds), '') + ");
             lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
             lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and fecha_creacion = @fecha_creacion and clave_area = @clave_area and tipo_horario = @tipo_contrato and dia_semana = 5) as seconds), '') + ");
             lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
             lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and fecha_creacion = @fecha_creacion and clave_area = @clave_area and tipo_horario = @tipo_contrato and dia_semana = 6) as seconds), '') + ");
             lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
             lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and fecha_creacion = @fecha_creacion and clave_area = @clave_area and tipo_horario = @tipo_contrato and dia_semana = 7) as seconds), '') +  ");
             lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
             lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario where rfc = @rfc and fecha_creacion = @fecha_creacion and clave_area = @clave_area and tipo_horario = @tipo_contrato and dia_semana = 8) as seconds), '') ");
             lsConsulta.Append(" as horas_semanales, rft.monto, upper(DATENAME(DAY, ps.periodo_inicio))  + ' DE ' + upper(DATENAME(MONTH, ps.periodo_inicio)) + ' DE ' + DATENAME(YEAR, ps.periodo_inicio) periodo_inicio, upper(DATENAME(DAY, ps.periodo_fin))  + ' DE ' + upper(DATENAME(MONTH, ps.periodo_fin)) + ' DE ' + DATENAME(YEAR, ps.periodo_fin) periodo_fin, ");
             lsConsulta.Append(" (select jefe_area from jefes where clave_area = ps.clave_area) jefe, ");
             lsConsulta.Append(" (select cadena_personal from fiel_contratos where fecha_creacion = @fecha_creacion and rfc = @rfc and clave_area = @clave_area and tipo_contrato = @tipo_contrato) cadena_personal, CONVERT(varbinary(max),null) qr_personal, ");
             lsConsulta.Append(" (select cadena_jefe from fiel_contratos where fecha_creacion = @fecha_creacion and rfc = @rfc and clave_area = @clave_area and tipo_contrato = @tipo_contrato) cadena_jefe, CONVERT(varbinary(max),null) qr_jefe, ");
             lsConsulta.Append(" (select cadena_sub_admin from fiel_contratos where fecha_creacion = @fecha_creacion and rfc = @rfc and clave_area = @clave_area and tipo_contrato = @tipo_contrato) cadena_sub_admin, CONVERT(varbinary(max),null) qr_sub_admin, ");
             lsConsulta.Append(" (select cadena_rh from fiel_contratos where fecha_creacion = @fecha_creacion and rfc = @rfc and clave_area = @clave_area and tipo_contrato = @tipo_contrato) cadena_rh, CONVERT(varbinary(max),null) qr_rh, ");
             lsConsulta.Append(" (select cadena_dir from fiel_contratos where fecha_creacion = @fecha_creacion and rfc = @rfc and clave_area = @clave_area and tipo_contrato = @tipo_contrato) cadena_dir, CONVERT(varbinary(max),null) qr_dir, ");
             lsConsulta.Append(" (select id_valida_documento from fiel_contratos where fecha_creacion = @fecha_creacion and rfc = @rfc and clave_area = @clave_area and tipo_contrato = @tipo_contrato) id_valida_documento, CONVERT(varbinary(max),null) qr_valida_documento, ");
             lsConsulta.Append(" (select DATENAME(DAY, fecha_firma_personal)  + ' DE ' + upper(DATENAME(MONTH, fecha_firma_personal)) + ' DE ' + DATENAME(YEAR, fecha_firma_personal) fecha_firma_personal from fiel_contratos where fecha_creacion = @fecha_creacion and rfc = @rfc and clave_area = @clave_area and tipo_contrato = @tipo_contrato) fecha_firma ");
             lsConsulta.Append(" from personal p, personal_domicilios pd, c_p cp, entidades_federativas ef, personal_personales pp, personal_solicitud ps, rf_tabulador rft ");
             lsConsulta.Append(" where p.rfc = pd.rfc and pd.colonia = cp.id and cp.ent_fed = ef.entidad_federativa and p.rfc = pp.rfc and p.rfc = ps.rfc and ps.id_monto = rft.id and ps.rfc = @rfc and ps.clave_area = @clave_area and getdate() between ps.fecha_creacion and ps.periodo_fin ) ");
             lsConsulta.Append(" select nombre, calle, numero, colonia, cod_post, ciudad_localidad, ");
             lsConsulta.Append(" nombre_entidad, genero, telefono, rfc, curp, horas_semanales, convert( decimal(10,2), (convert(float, horas_semanales) * 2.0) * convert (float,monto)) as total_neto,");
             lsConsulta.Append(" [dbo].[ConvertirNumeroaLetra](convert(decimal(10, 2), (convert(float, horas_semanales) * 2.0) * convert(float, monto))) as monto_letra, ");
             lsConsulta.Append(" convert( decimal(10,2), round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) as total_bruto, ");
             lsConsulta.Append(" convert( decimal(10,2), (round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) * .16) as iva_trans, ");
             lsConsulta.Append(" convert( decimal(10,2), (round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) + ");
             lsConsulta.Append(" ((round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) * .16)) as sub_total, ");
             lsConsulta.Append(" convert( decimal(10,2), (round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) * .10) as isr_ret, ");
             lsConsulta.Append(" convert( decimal(10,2), ((round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) * .16) * (2.0/3.0)) as iva_ret, ");
             lsConsulta.Append(" periodo_inicio, periodo_fin, jefe, cadena_personal, qr_personal, cadena_jefe, qr_jefe, cadena_sub_admin, qr_sub_admin, cadena_rh, qr_rh, cadena_dir, qr_dir, id_valida_documento, qr_valida_documento, fecha_firma from datos_contrato ");
             return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
         }

         */
        public DataTable RegresaDatosPersonalParaFirmaContrato(string psRFC, string psTipoContrato)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@rfc", psRFC));
            _oSqlParametros.Add(new ParametrosSQL("@tipo_contrato", psTipoContrato));
            lsConsulta.Append(" SET Language 'Spanish'; with datos_contrato (nombre, calle, numero, colonia, cod_post, ciudad_localidad, ");
            lsConsulta.Append(" nombre_entidad, puesto, descripcion_area, fecha_creacion, clave_area, genero, telefono, rfc, curp,  horas_semanales, monto, periodo_inicio, periodo_fin) as ( ");
            lsConsulta.Append(" select p.nombre_empleado + ' ' + p.apellido_paterno + ' ' + p.apellido_materno as nombre, ");
            lsConsulta.Append(" upper(rtrim(pd.calle)) calle, upper(rtrim(pd.numero)) numero, upper(rtrim(cp.colonia)) colonia, cp.cod_post, upper(rtrim(cp.ciudad_localidad)) ciudad_localidad, upper(rtrim(ef.nombre_entidad)) nombre_entidad, ");
            lsConsulta.Append(" rtrim(pz.descripcion_puesto) puesto, rtrim(o.descripcion_area) descripcion_area, convert(varchar, ps.fecha_creacion, 23), ps.clave_area, case pp.genero when 'M' then 'FEMENINO' when 'H' then 'MASCULINO'end as genero, rtrim(pp.telefono) telefono, p.rfc, pp.curp, ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario  where rfc = @rfc and tipo_horario = @tipo_contrato and clave_area = (select clave_area from fiel_contratos where rfc = @rfc and tipo_horario = @tipo_contrato )and fecha_creacion = (select fecha_creacion from fiel_contratos where rfc = @rfc and tipo_contrato = @tipo_contrato) and dia_semana = 2) as seconds), '') + ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario  where rfc = @rfc and tipo_horario = @tipo_contrato and clave_area = (select clave_area from fiel_contratos where rfc = @rfc and tipo_horario = @tipo_contrato )and fecha_creacion = (select fecha_creacion from fiel_contratos where rfc = @rfc and tipo_contrato = @tipo_contrato) and dia_semana = 3) as seconds), '') + ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario  where rfc = @rfc and tipo_horario = @tipo_contrato and clave_area = (select clave_area from fiel_contratos where rfc = @rfc and tipo_horario = @tipo_contrato )and fecha_creacion = (select fecha_creacion from fiel_contratos where rfc = @rfc and tipo_contrato = @tipo_contrato) and dia_semana = 4) as seconds), '') + ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario  where rfc = @rfc and tipo_horario = @tipo_contrato and clave_area = (select clave_area from fiel_contratos where rfc = @rfc and tipo_horario = @tipo_contrato )and fecha_creacion = (select fecha_creacion from fiel_contratos where rfc = @rfc and tipo_contrato = @tipo_contrato) and dia_semana = 5) as seconds), '') + ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario  where rfc = @rfc and tipo_horario = @tipo_contrato and clave_area = (select clave_area from fiel_contratos where rfc = @rfc and tipo_horario = @tipo_contrato )and fecha_creacion = (select fecha_creacion from fiel_contratos where rfc = @rfc and tipo_contrato = @tipo_contrato) and dia_semana = 6) as seconds), '') + ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario  where rfc = @rfc and tipo_horario = @tipo_contrato and clave_area = (select clave_area from fiel_contratos where rfc = @rfc and tipo_horario = @tipo_contrato )and fecha_creacion = (select fecha_creacion from fiel_contratos where rfc = @rfc and tipo_contrato = @tipo_contrato) and dia_semana = 7) as seconds), '') + ");
            lsConsulta.Append(" isnull((SELECT Seconds / 3600 FROM ");
            lsConsulta.Append(" (SELECT SUM(DATEDIFF(SECOND, CONVERT(time, hora_inicio), CONVERT(time, hora_fin))) AS 'Seconds' FROM personal_horario  where rfc = @rfc and tipo_horario = @tipo_contrato and clave_area = (select clave_area from fiel_contratos where rfc = @rfc and tipo_horario = @tipo_contrato )and fecha_creacion = (select fecha_creacion from fiel_contratos where rfc = @rfc and tipo_contrato = @tipo_contrato) and dia_semana = 8) as seconds), '') ");
            lsConsulta.Append(" as horas_semanales, rft.monto, upper(DATENAME(DAY, ps.periodo_inicio))  + ' DE ' + upper(DATENAME(MONTH, ps.periodo_inicio)) + ' DE ' + DATENAME(YEAR, ps.periodo_inicio) periodo_inicio, upper(DATENAME(DAY, ps.periodo_fin))  + ' DE ' + upper(DATENAME(MONTH, ps.periodo_fin)) + ' DE ' + DATENAME(YEAR, ps.periodo_fin) periodo_fin ");
            lsConsulta.Append(" from personal p, personal_domicilios pd, c_p cp, entidades_federativas ef, personal_personales pp, personal_solicitud ps, rf_tabulador rft, puestos pz, organigrama o ");
            lsConsulta.Append(" where p.rfc = pd.rfc and pd.colonia = cp.id and cp.ent_fed = ef.entidad_federativa and p.rfc = pp.rfc and p.rfc = ps.rfc and pz.clave_puesto = ps.clave_puesto and ps.clave_area = o.clave_area and ps.id_monto = rft.id and ps.rfc = @rfc and ps.tipo_contrato = @tipo_contrato and getdate() between ps.fecha_creacion and ps.periodo_fin ) ");
            lsConsulta.Append(" select nombre, calle, numero, colonia, cod_post, ciudad_localidad, ");
            lsConsulta.Append(" nombre_entidad, puesto, descripcion_area, fecha_creacion, clave_area, genero, telefono, rfc, curp, horas_semanales, convert( decimal(10,2), (convert(float, horas_semanales) * 2.0) * convert (float,monto)) as total_neto,");
            lsConsulta.Append(" [dbo].[ConvertirNumeroaLetra](convert(decimal(10, 2), (convert(float, horas_semanales) * 2.0) * convert(float, monto))) as monto_letra, ");
            lsConsulta.Append(" convert( decimal(10,2), round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) as total_bruto, ");
            lsConsulta.Append(" convert( decimal(10,2), (round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) * .16) as iva_trans, ");
            lsConsulta.Append(" convert( decimal(10,2), (round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) + ");
            lsConsulta.Append(" ((round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) * .16)) as sub_total, ");
            lsConsulta.Append(" convert( decimal(10,2), (round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) * .10) as isr_ret, ");
            lsConsulta.Append(" convert( decimal(10,2), ((round( (convert(float, horas_semanales) * 2.0) * convert (float,monto)/(1.0+.16*(1.0-(2.0/3.0))- 0.1),3)) * .16) * (2.0/3.0)) as iva_ret, ");
            lsConsulta.Append(" periodo_inicio, periodo_fin from datos_contrato");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable RegresaContratosFirmados()
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            lsConsulta.Append(" select ps.rfc, nombre_empleado + ' ' + apellido_paterno + ' ' + apellido_materno as nombre_personal, o.descripcion_area, case ps.tipo_contrato when 'H' then 'Honorarios' else 'Asimilados' end as tipo_contrato, ");
            /*lsConsulta.Append(" ('<a class=\"btn btn-success\" href=\"/Reportes/ContratoDocenteAsimilados?psPeriodo='+ @periodo + '&psRFC='+ ps.rfc +'\" data-ajax-update=\"#BodyPrincipal\" data-ajax-mode=\"replace\" data-ajax-method=\"GET\" data-ajax=\"true\"> Revisar Horario </a>') end as boton ");*/
            lsConsulta.Append(" '<button type=\"submit\" class=\"btn btn-success\" onclick=\"vistaContrato(''' + convert(varchar, ps.fecha_creacion, 23) + ''',''' + ps.rfc + ''', ''' + ps.clave_area + ''', ''' + ps.tipo_contrato + ''');\"> Vista Previa </button>' as boton ");
            lsConsulta.Append(" from personal p, personal_solicitud ps, organigrama o where p.rfc = ps.rfc and o.clave_area = ps.clave_area and ps.status >= 11 and getdate() between ps.fecha_creacion and ps.periodo_fin ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable RegresaFechasPago(string psPeriodo, string psTipo)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
            _oSqlParametros.Add(new ParametrosSQL("@tipo", psTipo));
            lsConsulta.Append(" select periodo, id_fecha, tipo_personal, convert(varchar, fecha, 103) as fecha from personal_fechas_pago where periodo = @periodo and tipo_personal = @tipo order by id_fecha asc");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable RegresaFechasDetalles(string psPeriodo, string psTipo)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
            _oSqlParametros.Add(new ParametrosSQL("@tipo", psTipo));
            lsConsulta.Append(" select '<div class=\"col-md-2 col-sm-2 col-xs-12 form-group\"><label for=\"tags\">Fecha ' + ");
            lsConsulta.Append(" convert(varchar, ROW_NUMBER() OVER(ORDER BY fecha ASC)) + '</label><input type=\"text\" value=\"' + ");
            lsConsulta.Append(" convert(varchar, fecha, 107) + '\" class=\"form-control\" readonly /></div>' as fecha ");
            lsConsulta.Append(" from personal_fechas_pago where periodo = @periodo and tipo_personal = @tipo ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable RegresaIndice(string psPeriodo, string psTipo)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
            _oSqlParametros.Add(new ParametrosSQL("@tipo", psTipo));
            lsConsulta.Append(" select top(1) id_fecha from personal_fechas_pago where periodo = @periodo and tipo_personal = @tipo order by id_fecha desc");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable RegresaDatosPreContrato(int psID)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id", psID));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_datos_pre_contrato", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaListaDatosContrato()
        {
            DataTable ldtTabla = new DataTable();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_lista_pre_contrato"));
            }
            return ldtTabla;
        }

        public DataTable SelectMontosPago(string psTipoMonto)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@tipo_monto", psTipoMonto));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_montos_pago", loParametros));
            }
            return ldtTabla;
        }


        public DataTable RegresaDatosFirmaContratoJefe(string psUsuario)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@usuario", psUsuario));
            lsConsulta.Append(" select ps.rfc, nombre_empleado + ' ' + apellido_paterno + ' ' + apellido_materno as nombre_personal, case ps.tipo_contrato when 'H' then 'Honorarios' else 'Asimilados' end as tipo_contrato, ");
            lsConsulta.Append(" case WHEN ps.status > 7 then '<i class=\"green\">F I R M A D O</i>' else ");
            lsConsulta.Append(" ('<a class=\"btn btn-success\" href=\"/Generales/FirmaContrato?psRFC='+ ps.rfc +'&psFechaCreacion='+ convert(varchar, ps.fecha_creacion, 23)  +'&psClaveArea='+ ps.clave_area +'&psTipoContrato='+ ps.tipo_contrato +'\" data-ajax-update=\"#BodyPrincipal\" data-ajax-mode=\"replace\" data-ajax-method=\"GET\" data-ajax=\"true\"> Revisar Datos </a>') end as boton  ");
            lsConsulta.Append(" from personal p, personal_solicitud ps, organigrama o where p.rfc = ps.rfc and o.clave_area = ps.clave_area and ps.status >= 7 and getdate() between ps.fecha_creacion and ps.periodo_fin ");
            lsConsulta.Append(" and ps.clave_area in (select clave_area from jefes where usuario = @usuario) ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable RegresaDatosFirmaContratoSub(string psUsuario)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@usuario", psUsuario));
            lsConsulta.Append(" select ps.rfc, nombre_empleado + ' ' + apellido_paterno + ' ' + apellido_materno as nombre_personal, o.descripcion_area, case ps.tipo_contrato when 'H' then 'Honorarios' else 'Asimilados' end as tipo_contrato,");
            lsConsulta.Append(" case WHEN ps.status > 8 then '<i class=\"green\">F I R M A D O</i>' else ");
            lsConsulta.Append(" ('<a class=\"btn btn-success\" href=\"/SubDireccion/FirmaContratoSub?psRFC='+ ps.rfc +'&psFechaCreacion='+ convert(varchar, ps.fecha_creacion, 23)  +'&psClaveArea='+ ps.clave_area +'&psTipoContrato='+ ps.tipo_contrato +'\" data-ajax-update=\"#BodyPrincipal\" data-ajax-mode=\"replace\" data-ajax-method=\"GET\" data-ajax=\"true\"> Revisar Contrato </a>') end as boton  ");
            lsConsulta.Append(" from personal p, personal_solicitud ps, organigrama o where  p.rfc = ps.rfc and o.clave_area = ps.clave_area and ps.status >= 8 and getdate() between ps.fecha_creacion and ps.periodo_fin ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable RegresaDatosFirmaContratoRH(string psUsuario)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@usuario", psUsuario));
            lsConsulta.Append(" select ps.rfc, nombre_empleado + ' ' + apellido_paterno + ' ' + apellido_materno as nombre_personal, o.descripcion_area, case ps.tipo_contrato when 'H' then 'Honorarios' else 'Asimilados' end as tipo_contrato,");
            lsConsulta.Append(" case WHEN ps.status > 9 then '<i class=\"green\">F I R M A D O</i>' else ");
            lsConsulta.Append(" ('<a class=\"btn btn-success\" href=\"/RecursosHumanos/FirmaContratoRH?psRFC='+ ps.rfc +'&psFechaCreacion='+ convert(varchar, ps.fecha_creacion, 23)  +'&psClaveArea='+ ps.clave_area +'&psTipoContrato='+ ps.tipo_contrato +'\" data-ajax-update=\"#BodyPrincipal\" data-ajax-mode=\"replace\" data-ajax-method=\"GET\" data-ajax=\"true\"> Revisar Contrato </a>') end as boton  ");
            lsConsulta.Append(" from personal p, personal_solicitud ps, organigrama o where p.rfc = ps.rfc and o.clave_area = ps.clave_area and ps.status >= 9  and getdate() between ps.fecha_creacion and ps.periodo_fin ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable RegresaDatosFirmaContratoDireccion(string psUsuario)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@usuario", psUsuario));
            lsConsulta.Append(" select ps.rfc, nombre_empleado + ' ' + apellido_paterno + ' ' + apellido_materno as nombre_personal, o.descripcion_area, case ps.tipo_contrato when 'H' then 'Honorarios' else 'Asimilados' end as tipo_contrato,");
            lsConsulta.Append(" case WHEN ps.status > 10 then '<i class=\"green\">F I R M A D O</i>' else ");
            lsConsulta.Append(" ('<a class=\"btn btn-success\" href=\"/Direccion/FirmaContratoDir?psRFC='+ ps.rfc +'&psFechaCreacion='+ convert(varchar, ps.fecha_creacion, 23)  +'&psClaveArea='+ ps.clave_area +'&psTipoContrato='+ ps.tipo_contrato +'\" data-ajax-update=\"#BodyPrincipal\" data-ajax-mode=\"replace\" data-ajax-method=\"GET\" data-ajax=\"true\"> Revisar Contrato </a>') end as boton  ");
            lsConsulta.Append(" from personal p, personal_solicitud ps, organigrama o where p.rfc = ps.rfc and o.clave_area = ps.clave_area and ps.status >= 10 and getdate() between ps.fecha_creacion and ps.periodo_fin  ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable MuestraUsuarios(string psTipoUsuario, string psUsuario,string psNuevoUsuario,string psNombreUsuario, int psEstado)
        {

            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@tipo_usuario", psTipoUsuario));
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                loParametros.Add(new ParametrosSQL("@nuevo_usuario", psNuevoUsuario));
                loParametros.Add(new ParametrosSQL("@nombre_usuario", psNombreUsuario));
                loParametros.Add(new ParametrosSQL("@estado", psEstado));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_control_usuario", loParametros));
            }
            return ldtTabla;
        }

        public DataTable PermisosUsuarios(string psUsuario, string  psTipoUsuario, string psUsuarioSeleccionado)
        {

            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
               loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
               loParametros.Add(new ParametrosSQL("@tipo_usuario", psTipoUsuario));
               loParametros.Add(new ParametrosSQL("@usuario_seleccionado", psUsuarioSeleccionado));
               ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_control_usuario_permisos", loParametros));
            }
            return ldtTabla;
        }


        public DataTable OtorgarPermisosUsuario(string psUsuario, string psUsuarioSeleccionado, string psClave)
		{
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                loParametros.Add(new ParametrosSQL("@usuario_seleccionado", psUsuarioSeleccionado));
                loParametros.Add(new ParametrosSQL("@clave", psClave));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_asinga_permiso_usuario", loParametros));
            }
            return ldtTabla;
        }

        #endregion

        #region Retardos

        public DataTable RegresaPersonalContratadoAdministrativo()
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append(" select ps.id , ps.rfc + ' / ' + p.apellido_paterno + ' ' + p.apellido_materno + ' ' + p.nombre_empleado + ' / ' + pt.descripcion_puesto as nombre ");
            lsConsulta.Append(" from personal_solicitud ps, personal p, puestos pt where ps.rfc = p.rfc and ps.clave_puesto = pt.clave_puesto and ps.status <= 5 and ps.id_monto <> -1 and ps.clave_puesto not in (234, 235) and GETDATE() between ps.fecha_creacion and ps.periodo_fin order by ps.rfc ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);
        }

        public DataTable RegresaRetardosRegistrados()
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append(" select ps.rfc, p.apellido_paterno + ' ' + p.apellido_materno + ' ' + p.nombre_empleado as nombre, pt.descripcion_puesto, convert(varchar,pr.fecha_retardo, 107) fecha_retardo, pr.horas_retardo, ");
            lsConsulta.Append(" '<button type=\"submit\" class=\"btn btn-danger\" onclick=\"Eliminar(''' + convert(varchar, pr.id_retardo) + ''');\"> Eliminar </button>' as accion ");
            lsConsulta.Append(" from personal_retardos pr, personal p, personal_solicitud ps, puestos pt where p.rfc = ps.rfc and pt.clave_puesto = ps.clave_puesto and ps.id = pr.id and GETDATE() between ps.fecha_creacion and ps.periodo_fin ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);
        }
        
        public DataTable RegresaDatosPersonalContratado(int psID)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@id", psID));

            lsConsulta.Append(" select p.nombre_empleado + ' ' + p.apellido_paterno + ' ' + p.apellido_materno as nombre_empleado, ");
            lsConsulta.Append(" p.no_tarjeta, case ps.tipo_contrato when 'B' then 'ASIMILADOS' else 'HONORARIOS' end as tipo_contrato, o.descripcion_area, ");
            lsConsulta.Append(" pt.descripcion_puesto, convert(varchar,ps.periodo_inicio,107) periodo_inicio, convert(varchar,ps.periodo_fin,107) periodo_fin from personal p, personal_solicitud ps, organigrama o, puestos pt ");
            lsConsulta.Append(" where p.rfc = ps.rfc and ps.clave_area = o.clave_area and ps.clave_puesto = pt.clave_puesto ");
            lsConsulta.Append(" and ps.id = @id  ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }
        #endregion

        #region ListaPersonal
        public DataTable RegresaListaPersonal()
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append(" SELECT p.rfc, o.descripcion_area as area, p.no_tarjeta, p.nombre_empleado, p.apellido_paterno, p.apellido_materno, ");
            lsConsulta.Append(" case p.status_empleado ");
            lsConsulta.Append(" when '02' then 'ACTIVO' when '00' then 'BAJA POR RENUNCIA' when '01' then 'LICENCIA POR ASUNTOS PARTICULARES' ");
            lsConsulta.Append(" when '03' then 'BAJA POR JUBILACIÓN' when '06' then 'INACTIVO' when'04' then'INACTIVO POR CAMBIO DE CT' ");
            lsConsulta.Append(" when '05' then'INACTIVO POR FALLECIMIENTO' when '07' then 'LICENCIA POR COMISION SINDICAL' when '08' then 'BECA COMISION' end as status_empleado, ");
            lsConsulta.Append(" (select descripcion_area from organigrama where clave_area = p.area_academica) as area_academica, ");
            lsConsulta.Append(" case p.tipo_personal when 'B' then 'BASE' when 'X' then 'MIXTO' when 'H' then 'HONORARIOS' end as tipo_personal, ");
            lsConsulta.Append(" pp.curp, pp.estado_civil, case pp.nacionalidad when 'MX' then 'MEXICANA' else 'EXTRANJERA' end as nacionalidad, ");
            lsConsulta.Append(" nde.descripcion_nivel_estudios, pp.nombre_carrera, convert(varchar, pp.fecha_titulacion, 107) as fehca_titulacion, ");
            lsConsulta.Append(" pp.cedula_profesional, pp.correo_electronico, pp.telefono, pp.telefono_emergencia, pp.NSS, ");
            lsConsulta.Append(" case pp.genero when 'M' then 'mujer' when 'H' then 'hombre'end as genero, pp.fecha_nacimiento, ");
            lsConsulta.Append(" (select descripcion from sisCombos where valor = pp.estado_nacimiento and combo = 'cboEstados') as estado_nacimiento, ");
            lsConsulta.Append(" pd.calle, pd.numero, pd.id_cp ");
            lsConsulta.Append(" FROM personal p, personal_personales pp, personal_domicilios pd, organigrama o, nivel_de_estudios nde ");
            lsConsulta.Append(" where p.rfc = pp.rfc and p.rfc = pd.rfc and pp.rfc = pd.rfc and o.clave_area = p.clave_area and pp.nivel_estudios = nde.nivel_estudios ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);
        }

        public DataTable RegresaPersonalLista()
        {
            DataTable ldtTabla = new DataTable();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_lista"));
            }
            return ldtTabla;
        }
        #endregion

        #region DatosGeneralesTrabajador
        public DataTable RegresaDatosParaPersonal(string psRFC)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@rfc", psRFC));
            lsConsulta.Append(" SELECT p.rfc, p.no_tarjeta, pp.curp, p.nombre_empleado, p.apellido_paterno, p.apellido_materno, ");
            lsConsulta.Append(" o.descripcion_area as adscripcion, ");
            lsConsulta.Append(" (select descripcion_area from organigrama where clave_area = p.area_academica) as area_academica, ");
            lsConsulta.Append(" pp.NSS, case pp.genero when 'M' then 'mujer' when 'H' then 'hombre'end as genero, convert(varchar, pp.fecha_nacimiento, 107) as fecha_nacimiento, ");
            lsConsulta.Append(" case pp.estado_civil when 'S' then 'Soltero(a)' when 'C' then 'Casado(a)' when 'D' then 'Divorsiado(a)' when 'V'then 'Viudo(a)' when 'U' then 'Union Libre' end as estado_civil, ");
            lsConsulta.Append(" case pp.nacionalidad when 'MX' then 'MEXICANA' else 'EXTRANJERA' end as nacionalidad, ");
            lsConsulta.Append(" (select descripcion from sisCombos where valor = pp.estado_nacimiento and combo = 'cboEstados') as estado_nacimiento, ");
            lsConsulta.Append(" nde.descripcion_nivel_estudios as nivel_estudios, pp.nombre_carrera, convert(varchar, pp.fecha_titulacion, 107) as fecha_titulacion, ");
            lsConsulta.Append(" pp.cedula_profesional, pp.correo_electronico, pp.telefono, pp.telefono_emergencia, ");
            lsConsulta.Append(" pd.calle, pd.numero, pd.id_cp ");
            lsConsulta.Append(" FROM personal p, personal_personales pp, personal_domicilios pd, organigrama o, nivel_de_estudios nde ");
            lsConsulta.Append(" where p.rfc = pp.rfc and p.rfc = pd.rfc and pp.rfc = pd.rfc and o.clave_area = p.clave_area and pp.nivel_estudios = nde.nivel_estudios and p.rfc = @rfc ");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        public DataTable CargarListaMontosPagos()
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append(" SELECT concepto, case tipo_monto when 'H' then 'Honorarios' else 'Asimilados' end as tipo_horario, Round(monto, 2, 0) as monto, '<a class=\"btn btn-success\" onclick=\"actualizar(''' + convert(varchar,id) + ''','''+ tipo_monto +''','''+ concepto +''', ''' + convert(varchar,monto) + ''',''A'');\" >Modificar</a> &nbsp; <a class=\"btn btn-warning\" onclick=\"eliminar(''' + convert(varchar,id) + ''');\">Eliminar</a>' from rf_tabulador where id > 0");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);


        }

        public DataTable RegresaDatosFirmaContrato(string psRFC)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            _oSqlParametros = new List<ParametrosSQL>();
            _oSqlParametros.Add(new ParametrosSQL("@rfc", psRFC));
            lsConsulta.Append(" select ps.status ");
            lsConsulta.Append(" from personal p, personal_solicitud ps ");
            lsConsulta.Append(" where p.rfc = ps.rfc and p.rfc = @rfc and getdate() between ps.fecha_creacion and ps.periodo_fin and ps.status >= 6");
            return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
        }

        #endregion

        public DataTable RegresaDatosRetardos(int psID, string psFechaInicioRetardo, string psFechaFinRetardo)
        {
            DataTable ldtTabla = new DataTable();
            DateTime lsFechaInicioRetardo, lsFechaFinRetardo;
            lsFechaInicioRetardo = DateTime.Parse(psFechaInicioRetardo);
            lsFechaFinRetardo = DateTime.Parse(psFechaFinRetardo);
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@id", psID));
                loParametros.Add(new ParametrosSQL("@fecha_inicio", lsFechaInicioRetardo));
                loParametros.Add(new ParametrosSQL("@fecha_fin", lsFechaFinRetardo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_evaluar_retardo", loParametros));
            }
            return ldtTabla;
        }


        #region Actividades Apoyo
        public DataTable RegresaGruposPersonalActividadesApoyo(string psRFC, string psPeriodo)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_horario_bases", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaPesonalSeleccionActividadesApoyo(string psPeriodo, string psUsuario)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {

                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_actividades_apoyo", loParametros));
            }
            return ldtTabla;
        }

        public DataTable RegresaPesonalActividadesApoyo(string psRFC, string psPeriodo)
        {
            DataTable ldtTabla = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_horas_actividades_apoyo", loParametros));
            }
            return ldtTabla;
        }

        #endregion
    }
}
