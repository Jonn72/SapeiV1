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
	/// Clase tutorias_grupo generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Tutorias_Grupos
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
          public DataTable RegresaTablaGrupos(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select grupo, (select apellido_paterno + ' ' + apellido_materno  + ' '+  nombre_empleado from personal where rfc = E.rfc) entrenador,");
               lsConsulta.Append("capacidad, inscritos");
               lsConsulta.Append(" ,stuff(( ");
               lsConsulta.Append(" SELECT ', ' + B.dia + ' ' + B.hora_inicio + ' - ' + B.hora_fin + ' AULA ' +B.aula");
               lsConsulta.Append(" FROM tutorias_horarios B ");
               lsConsulta.Append(" WHERE E.periodo = B.periodo and E.grupo = B.grupo ");
               lsConsulta.Append(" FOR XML PATH('') ");
               lsConsulta.Append(" ), 1, 1, '') horario");
               lsConsulta.AppendFormat(" From {0} E", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaTablaGruposInscritos(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT grupo,rfc, capacidad, inscritos");
               lsConsulta.Append(",'<div class=\"col-md-1\"><span class=\"input-group-btn\"><a href=\"/ReportesCSV/DescargaListaEstudiantesGrupoTutorias?psPeriodo='+periodo+'&amp;psGrupo='+trim(grupo)+'\" class=\"btn btn-info\" role=\"button\"><span class=\"fa fa-download\"></span></a></span></div>' descargar");
               lsConsulta.AppendFormat(" FROM {0} G", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaListaGruposCargados(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select  CASE registrados WHEN 0 THEN '<label class=\"fa fa-times-circle red\"></label>' ELSE '<label class=\"fa fa-check-circle-o blue\"></label>' END capturado, grupo, inscritos");
               lsConsulta.Append(",'<div class=\"col-md-1\"><span class=\"input-group-btn\"><a href=\"/ReportesCSV/DescargaListaEstudiantesGrupoTutorias?psPeriodo='+periodo+'&amp;psGrupo='+grupo+'\" class=\"btn btn-info\" role=\"button\"><span class=\"fa fa-file-excel-o\"></span></a></span></div>' descargar,");
               lsConsulta.Append("case registrados when 1 then '<div class=\"col-md-1\"><span class=\"input-group-btn\"><a href=\"/DocumentosOficiales/ResultadosPorTutoria?psPeriodo='+periodo+'&amp;psGrupo='+grupo+'\" class=\"btn btn-info\" role=\"button\"><span class=\"fa fa-file-pdf-o\"></span></a></span></div>' else ' ' end resultados");
               lsConsulta.AppendFormat(" FROM {0} ", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               if (_oSistema.Sesion.Usuario.RolUsuario == Framework.Configuracion.enmRolUsuario.DOC)
                    lsConsulta.AppendFormat(" AND rfc = {0}", _oSistema.Sesion.Usuario.Usuario);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }

		#endregion

		}
	}
