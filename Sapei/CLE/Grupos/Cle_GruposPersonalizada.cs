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
	/// Clase cle_grupo generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Cle_Grupos
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
               lsConsulta.Append("SELECT periodo, nivel, grupo,  capacidad, inscritos, ");
               lsConsulta.Append("stuff((SELECT ', Aula ' + H.aula + ' ' + case H.dia when 2 then 'LUN' when 3 then 'MAR' when 4 then 'MIE' when 5 then 'JUE' when 6 then 'VIE' else 'SAB' end +' ' + H.hora_inicial + ' - ' + H.hora_final FROM cle_horarios H   WHERE H.periodo = G.periodo AND H.nivel = G.nivel AND H.grupo = G.grupo  FOR XML PATH('')), 1, 1, '') horario");
               lsConsulta.AppendFormat(" FROM {0} G", RutaTabla);
               if (!string.IsNullOrEmpty(psPeriodo))
                    lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }

          public DataTable RegresaTablaGruposImprimir(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT nivel, grupo, capacidad ");
               lsConsulta.Append(",stuff((SELECT ', Aula: ' + H.aula + CASE H.dia WHEN '2' THEN 'LUN' WHEN '3' THEN 'MAR' WHEN '4' THEN 'MIE' WHEN '5' THEN 'JUE' WHEN '6' THEN 'VIE' WHEN '7' THEN 'SAB' END + ' ' + H.hora_inicial + ' - ' + H.hora_final FROM cle_horarios H   WHERE H.periodo = G.periodo AND H.nivel = G.nivel AND H.grupo = G.grupo  FOR XML PATH('')), 1, 1, '') horario");
               lsConsulta.AppendFormat(" FROM {0} G", RutaTabla);
               if (!string.IsNullOrEmpty(psPeriodo))
                    lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaTablaGruposInscritos(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT nivel, grupo, capacidad, inscritos");
               lsConsulta.Append(",'<div class=\"col-md-1\"><span class=\"input-group-btn\"><a href=\"/ReportesCSV/DescargaGruposInscritosCLE?psPeriodo='+periodo+'&amp;psGrupo='+TRIM(grupo)+'&amp;psNivel='+TRIM(nivel)+'\" class=\"btn btn-info\" role=\"button\"><span class=\"fa fa-download\"></span></a></span></div>' descargar");
               lsConsulta.AppendFormat(" FROM {0} G", RutaTabla);
              lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaListaGruposCargados(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select  CASE registrados WHEN 0 THEN '<label class=\"fa fa-times-circle red\"></label>' ELSE '<label class=\"fa fa-check-circle-o blue\"></label>' END registrado, nivel, grupo, capacidad, inscritos, registrados, altas, bajas ");
               lsConsulta.AppendFormat(" FROM {0} ", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
		#endregion

		}
	}
