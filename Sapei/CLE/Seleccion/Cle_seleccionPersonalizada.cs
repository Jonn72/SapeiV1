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
	/// Clase cle_seleccion generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Cle_Seleccion
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
          public DataTable RegresaListaGruposInscritos(string psPeriodo, string psGrupo, string psNivel)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select periodo, TRIM(nivel) nivel, TRIM(grupo) grupo, TRIM(C.no_de_control) control,");
               lsConsulta.Append("(apellido_paterno + ' ' + apellido_materno + ' ' + nombre_alumno) nombre,");
               lsConsulta.Append("carrera, sexo, semestre,");
				lsConsulta.Append("(select usuario from alumnos_cuentas where no_de_control = C.no_de_control and tipo_cuenta = 1) 'correo'");

			lsConsulta.AppendFormat(" FROM {0} C left join alumnos A on (C.no_de_control = A.no_de_control)", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}' AND grupo = '{1}' AND nivel = '{2}'", psPeriodo,psGrupo,psNivel);
			lsConsulta.Append("order by nombre");
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaListaGruposInscritos(string psNoControl)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append(" select (select identificacion_corta from periodos_escolares where periodo = C.periodo) periodo");
               lsConsulta.Append(" ,no_de_control, (select nombre_alumno + ' ' + apellido_paterno + ' '+ apellido_materno from alumnos where no_de_control = C.no_de_control) nombre");
               lsConsulta.Append(" ,nivel,grupo");
               lsConsulta.Append(" ,isnull((select hora_inicial+' - '+ hora_final +' / '+aula from cle_horarios where periodo = C.periodo and nivel = C.nivel and grupo = C.grupo and dia = 2),'') lunes");
               lsConsulta.Append(" ,isnull((select hora_inicial+' - '+ hora_final +' / '+aula from cle_horarios where periodo = C.periodo and nivel = C.nivel and grupo = C.grupo and dia = 3),'') martes");
               lsConsulta.Append(" ,isnull((select hora_inicial+' - '+ hora_final +' / '+aula from cle_horarios where periodo = C.periodo and nivel = C.nivel and grupo = C.grupo and dia = 4),'') miercoles");
               lsConsulta.Append(" ,isnull((select hora_inicial+' - '+ hora_final +' / '+aula from cle_horarios where periodo = C.periodo and nivel = C.nivel and grupo = C.grupo and dia = 5),'') jueves");
               lsConsulta.Append(" ,isnull((select hora_inicial+' - '+ hora_final +' / '+aula from cle_horarios where periodo = C.periodo and nivel = C.nivel and grupo = C.grupo and dia = 6),'') viernes");
               lsConsulta.Append(" ,isnull((select hora_inicial+' - '+ hora_final +' / '+aula from cle_horarios where periodo = C.periodo and nivel = C.nivel and grupo = C.grupo and dia = 7),'') sabado");
               lsConsulta.AppendFormat(" FROM {0} C",RutaTabla);
               lsConsulta.AppendFormat(" WHERE no_de_control = '{0}'",psNoControl);
			lsConsulta.AppendFormat(" and periodo = '{0}'", _oSistema.Sesion.Periodo.PeriodoActual);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
		#endregion

		}
	}
