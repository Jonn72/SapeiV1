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
using Sapei.Framework.Utilerias;

namespace Sapei
{
	/// <summary>
	/// Clase extra_actividades_inscrito generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Extra_actividades_inscrito
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
		#region Consulta
		public DataTable RegresaTablaActividades(string psControl)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select periodo,");
			lsConsulta.Append("(select descripcion from sisCombos where combo = 'cboTipoActividades' and valor = tipo) tipo,");
			lsConsulta.Append("(select descripcion from extra_actividades where id = actividad) actividad,");
			lsConsulta.Append("case concluida when 0 then 'No' else 'Si' end concluida,");
			lsConsulta.AppendFormat("(select convert(varchar(max),stuff((SELECT  ', '+' ' + B.dia + ' ' + B.hora_inicio + ' - ' + B.hora_fin FROM extra_actividades_horarios B WHERE C.actividad = B.id_actividad FOR XML PATH('')), 1, 1, '') ))", _oSistema.Sesion.Usuario.Usuario);
			lsConsulta.AppendFormat(" From {0} C", RutaTabla);
			lsConsulta.AppendFormat(" WHERE no_de_control = '{0}'", psControl);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaTablaActividadesEstudiantes()
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select periodo, no_de_control,");
			lsConsulta.Append("(select descripcion from sisCombos where combo = 'cboTipoActividades' and valor = tipo) tipo,");
			lsConsulta.Append("(select descripcion from extra_actividades where id = actividad) actividad,");
			lsConsulta.Append("case concluida when 0 then 'No' else 'Si' end concluida,");
			lsConsulta.AppendFormat("(select convert(varchar(max),stuff((SELECT  ', '+' ' + B.dia + ' ' + B.hora_inicio + ' - ' + B.hora_fin FROM extra_actividades_horarios B WHERE C.actividad = B.id_actividad FOR XML PATH('')), 1, 1, '') )),", _oSistema.Sesion.Usuario.Usuario);
			lsConsulta.Append("actividad as id");
			lsConsulta.AppendFormat(" From {0} C", RutaTabla);
			lsConsulta.AppendFormat(" WHERE periodo = '{0}'", _oSistema.Sesion.Periodo.PeriodoActual);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaTablaEstudiantesIncritos(string psPeriodo)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("SELECT  [periodo],[no_de_control],[nombre],[sexo],[correo],[carrera],[semestre]");
			lsConsulta.Append(",[tipo],[actividad],[concluida],[fecha_registro],[fecha_termino] ");
			lsConsulta.AppendFormat(" FROM [bdtec].[dbo].[estudiantes_datos_extraescolares]");
			lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaTablaEstudiantesIncritos(string psPeriodo, int piIdActividad, bool pbAgregaResultado = false)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select apellido_paterno+' '+ apellido_materno+ ' '+ nombre_alumno nombre, no_de_control,");
			lsConsulta.Append(" semestre, ");
			if (pbAgregaResultado)
			{
				lsConsulta.AppendFormat("carrera especialidad,(select UPPER(descripcion) from extra_actividades where periodo = '{0}' and id = {1}) actividad", psPeriodo, piIdActividad);
				lsConsulta.AppendFormat(",(select case when promedio >= 1 then 'A' else 'NA' end  from extra_actividades_inscritos where periodo = '{0}' and actividad = {1} and no_de_control = A.no_de_control) resultado", psPeriodo, piIdActividad);
			}
			else
				lsConsulta.Append("(select nombre_carrera from carreras where carrera = A.carrera and reticula = A.reticula) especialidad");
			lsConsulta.Append(" from alumnos A WHERE no_de_control in ");
			lsConsulta.AppendFormat(" (SELECT no_de_control FROM extra_actividades_inscritos WHERE periodo = '{0}' AND actividad = {1})", psPeriodo, piIdActividad);
			lsConsulta.Append(" ORDER BY apellido_paterno");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaListaInscritosPorActividad(string psPeriodo, string psIdActividad)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("SELECT  actividad ,[no_de_control], (select  apellido_paterno +' '+ apellido_materno + ' '+ nombre_alumno from alumnos where no_de_control = C.no_de_control) as nombre , ISNULL(promedio,0) promedio");
			lsConsulta.Append(",(select usuario from alumnos_cuentas where no_de_control = C.no_de_control and tipo_cuenta = 1) 'correo'");
			lsConsulta.AppendFormat(" From {0} C", RutaTabla);
			lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
			lsConsulta.AppendFormat(" AND actividad = {0}", psIdActividad);
			lsConsulta.Append(" ORDER BY nombre");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		public DataTable RegresaActividadesPorEstudiante(string psPeriodo, string psNoControl, string psIdActividad = null, bool pbEs15 = false, bool pbIncluyeFolio = false, bool pbIncluyeNA = false)
		{
			StringBuilder lsConsulta;
			DataTable loDt;
			string lsDatos;
			string lsCadenaQR;
			byte[] lbysQR;
			lsConsulta = new StringBuilder();
			lsConsulta.AppendFormat("SELECT C.no_de_control, (select identificacion_corta from periodos_escolares where periodo = '{0}') periodo ,  (select  apellido_paterno +' '+ apellido_materno + ' '+ nombre_alumno from alumnos where no_de_control = C.no_de_control) as nombre , ", psPeriodo);
			lsConsulta.Append("(select  nombre_carrera from estudiantes_datos_completos where no_de_control = C.no_de_control) as carrera ,");
			lsConsulta.Append("(select semestre from alumnos where no_de_control = C.no_de_control) as semestre,");
			lsConsulta.Append(" tipo, actividad, (select UPPER(descripcion) from extra_actividades where id = actividad)descripcion ,");
			lsConsulta.Append("(select  upper(nombre + ' '+ paterno +' '+ materno)  from extra_entrenador where id = (select id_entrenador from extra_actividades where id = C.actividad)) entrenador,");
			lsConsulta.Append("promedio, case when (4 >= promedio and promedio >= 3.5) then 'EXCELENTE' when (3.5 > promedio and promedio >= 2.5) then 'NOTABLE' when (2.5 > promedio and promedio >= 1.5) then 'BUENO' when (1.5 > promedio and promedio >= 1) then 'SUFICIENTE'  end desempeño");
			lsConsulta.AppendFormat(",(select periodo_ingreso_it from alumnos where no_de_control = '{0}') ingreso ", psNoControl);
			if (pbIncluyeFolio)
			{
				lsConsulta.Append(",(select folio from extra_actividades_folios where no_de_control = C.no_de_control) as folio");
				lsConsulta.Append(",(select fecha_registro from extra_actividades_folios where no_de_control = C.no_de_control) as folio_fecha");
			}
			lsConsulta.AppendFormat(" From {0} C", RutaTabla);
			lsConsulta.AppendFormat(" WHERE periodo = '{0}' ", psPeriodo);
			if (!pbIncluyeNA)
				lsConsulta.Append("  AND promedio >= 1 and concluida = 1");
			if (!string.IsNullOrEmpty(psIdActividad))
			{
				lsConsulta.AppendFormat(" AND actividad = {0}", psIdActividad);
				if (pbEs15)
					lsConsulta.Append("AND (select periodo_ingreso_it from alumnos where no_de_control = C.no_de_control) >= '20152'");
				else
					lsConsulta.Append("AND (select periodo_ingreso_it from alumnos where no_de_control = C.no_de_control) < '20152'");
			}
			else
				lsConsulta.AppendFormat(" AND no_de_control = '{0}'", psNoControl);
			loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
			if (pbEs15)
			{
				loDt.Columns.Add("qr", typeof(byte[]));
				foreach (DataRow loRow in loDt.Rows)
				{
					lsDatos = string.Format("Fecha de emisión:{0}&Periodo:{1}&No Control:{2}&Nombre:{3}&Credito:{4}&Promedio:{5}&Folio:{6}",
					DateTime.Now.ToShortDateString(), loRow["periodo"], loRow["no_de_control"], loRow["nombre"], loRow["descripcion"], loRow["desempeño"], loRow["folio"]);
					lsCadenaQR = lsDatos.MD5HASH();
					loRow["qr"] = lsCadenaQR.RegresaQRValidacionDocumentos();
					_oSistema.GrabaValidacionDocumento(lsCadenaQR, lsDatos);
				}
				return loDt;
			}
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}
		public string CorregirCalificacion(string psPeriodo, string psControl, int piActividad, float pfCalif)
		{
			StringBuilder lsQuery = new StringBuilder();
			lsQuery.AppendFormat("IF NOT EXISTS (SELECT 1 FROM creditos_complementarios_liberados WHERE no_de_control = '{0}')", psControl);
			lsQuery.Append(" BEGIN ");
			lsQuery.AppendFormat("UPDATE {0} SET promedio = {1}", RutaTabla, pfCalif);
			if (pfCalif >= 1)
				lsQuery.Append(", concluida = 1");
			else
				lsQuery.Append(", concluida = 0");
			lsQuery.AppendFormat(" WHERE periodo = '{0}' and no_de_control = '{1}' and actividad = {2}", psPeriodo, psControl, piActividad);
			lsQuery.Append(" SELECT 'Actualización relizada'");
			lsQuery.Append(" END ");
			lsQuery.Append(" ELSE ");
			lsQuery.Append(" SELECT 'El estudiante ya fue liberado por Servicios Escolares y no es posible modificar la calificación' ");
			return Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
		}

		public string GeneraFolioLiberacion(string psPeriodo, string psNoControl)
		{
			StringBuilder lsQuery = new StringBuilder();
			lsQuery.AppendFormat("IF EXISTS (SELECT 1 FROM extra_actividades_folios WHERE no_de_control = '{0}')", psNoControl);
			lsQuery.AppendFormat(" SELECT folio FROM extra_actividades_folios WHERE no_de_control = '{0}'", psNoControl);
			lsQuery.Append(" ELSE ");
			lsQuery.Append(" BEGIN ");
			lsQuery.Append(" DECLARE @valor INT ");
			lsQuery.AppendFormat(" SELECT @valor = ISNULL(MAX(folio),0) + 1 FROM extra_actividades_folios WHERE periodo = '{0}'", psPeriodo);
			lsQuery.Append("INSERT INTO extra_actividades_folios (periodo, no_de_control, folio, fecha_registro)");
			lsQuery.AppendFormat(" values ('{0}','{1}',@valor,getdate())", psPeriodo, psNoControl);
			lsQuery.Append(" SELECT @valor");
			lsQuery.Append(" END ");
			return Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
		}
		public void GeneraFolioLiberacionGrupo(string psPeriodo, string psGrupo)
		{
			StringBuilder lsQuery = new StringBuilder();
			lsQuery.Append("insert into extra_actividades_folios(periodo,no_de_control,folio,fecha_registro)");
			lsQuery.Append("select periodo, no_de_control,");
			lsQuery.Append("(SELECT ISNULL(MAX(folio),0) FROM extra_actividades_folios WHERE periodo = E.periodo)");
			lsQuery.Append("+ROW_NUMBER() over (order by no_de_control),GETDATE() from extra_actividades_inscritos E");
			lsQuery.AppendFormat(" where actividad = {0} ", psGrupo);
			lsQuery.AppendFormat(" AND no_de_control not in (select no_de_control from extra_actividades_folios)", psPeriodo);
			_oSistema.Conexion.EjecutaEscalar(lsQuery);
		}
		#endregion
	}
}
