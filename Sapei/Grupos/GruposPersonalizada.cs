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
	/// Clase grupo generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Grupos
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

          public DataTable RegresaGruposPrimero(string psPeriodo)
          {
               StringBuilder lsCadena;
               lsCadena = new StringBuilder();
               lsCadena.Append("select grupo,");
               lsCadena.AppendFormat("isnull((select count(distinct no_de_control) from seleccion_materias where periodo = '{0}' and grupo = G.grupo group by grupo),0) inscritos ", psPeriodo);
               lsCadena.Append(",exclusivo_carrera");
               lsCadena.AppendFormat(" from {0} G", RutaTabla);
               lsCadena.AppendFormat(" where periodo = '{0}' and substring(grupo,1,1) = '1' and exclusivo_carrera is not null", psPeriodo);
               lsCadena.Append(" group by grupo, exclusivo_carrera");
               lsCadena.Append(" order by exclusivo_carrera");
               return _oSistema.Conexion.RegresaDataTable(lsCadena);
          }
		#endregion
        #region actas

          public DataTable RegresaDatosGrupo(string lsPeriodo, string lsMateria, string lsGrupo, string lsUsuario)
          {
              StringBuilder lsTabla;
              lsTabla = new StringBuilder();
              lsTabla.Append("select G.materia, (select identificacion_larga from periodos_escolares where periodo = G.periodo ) per, ");
              lsTabla.Append("(select nombre_completo_materia from materias where materia = G.materia) mat,  G.grupo, ");
              lsTabla.Append("(select apellidos_empleado + ' ' + nombre_empleado from personal where rfc = G.rfc) nom, ");
              lsTabla.Append("(select count(no_de_control) from seleccion_materias where periodo = G.periodo and materia = G.materia and grupo=G.grupo) no_alumnos, ");
              lsTabla.Append("isnull(convert(varchar,n.[2],8) + '-' + convert(varchar,n2.[2],8),'') as lunes, ");
              lsTabla.Append("isnull(convert(varchar,n.[3],8) + '-' + convert(varchar,n2.[3],8),'') as martes, ");
              lsTabla.Append("isnull(convert(varchar,n.[4],8) + '-' + convert(varchar,n2.[4],8),'') as miercoles, ");
              lsTabla.Append("isnull(convert(varchar,n.[5],8) + '-' + convert(varchar,n2.[5],8),'') as jueves, ");
              lsTabla.Append("isnull(convert(varchar,n.[6],8) + '-' + convert(varchar,n2.[6],8),'') as viernes, ");
              lsTabla.Append("isnull(convert(varchar,n.[7],8) + '-' + convert(varchar,n2.[7],8),'') as sabado, ");
              lsTabla.Append("isnull(convert(varchar,n.[1],8) + '-' + convert(varchar,n2.[1],8),'') as domingo");
              lsTabla.Append("from ");
              lsTabla.Append("(select * from ");
              lsTabla.AppendFormat("(select dia_semana, hora_inicial from horarios where  periodo = '{0}' and materia = '{1}' and grupo = '{2}' and rfc = '{3}') cte", lsPeriodo, lsMateria, lsGrupo, lsUsuario);
              lsTabla.Append("Pivot (max(hora_inicial)  for dia_semana in ([1],[2],[3],[4],[5],[6],[7])) As TP) as n, ");
              lsTabla.Append("(select * from ");
              lsTabla.AppendFormat("(select dia_semana, hora_final from horarios where  periodo = '{0}' and materia = '{1}' and grupo = '{2}'  and rfc = '{3}') cte2", lsPeriodo, lsMateria, lsGrupo, lsUsuario);
              lsTabla.Append("Pivot (max(hora_final)  for dia_semana in ([1],[2],[3],[4],[5],[6],[7])) As TP2) as n2, ");
              lsTabla.Append("grupos G ");
              lsTabla.AppendFormat("where G.periodo = '{0}' and G.materia = '{1}' and G.grupo = '{2}' and G.rfc = '{3}'", lsPeriodo, lsMateria, lsGrupo, lsUsuario);
              DataTable loDT;
              loDT = _oSistema.Conexion.RegresaDataTable(lsTabla);
              return loDT;
          }

        #endregion
    }
	}
