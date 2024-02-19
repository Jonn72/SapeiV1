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
	/// Clase sis_consulta generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Sis_Consultas
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
          public DataTable RegresaConsultasCombo()
          {
               StringBuilder lsConsulta;
               DataTable loDt;
               lsConsulta = new StringBuilder();
               lsConsulta.AppendFormat("select id, titulo from {0}",RutaTabla);
			   lsConsulta.AppendFormat(" where permisos like '%{0}%'",_oSistema.Sesion.Usuario.RolUsuario.ToString());
               loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
               if (loDt.Rows.Count == 0)
                    return null;
               return loDt;
          }
		#endregion
          #region Planeacion
          public DataTable RegresaConsultaDocentesMateria()
          {
               StringBuilder lsConsulta;
               DataTable loDt;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select (select nombre_completo_materia from materias where materia = G.materia) materia,rfc, (select apellido_paterno +' '+ apellido_materno +' '+ nombre_empleado from personal where rfc = G.rfc) docente, (select apellido_paterno +' '+ apellido_materno +' '+ nombre_empleado from personal where rfc = G.rfc) docente,(select UPPER(estudios) from personal where rfc = G.rfc) carrera,case tipo_personal when 'H' then 'HONORARIOS' when 'B' then 'PLAZA' end asignacion ");
               lsConsulta.AppendFormat(" from grupos G where periodo = '{0}' and rfc is not null",_oSistema.Sesion.Periodo.PeriodoActual);
               loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
               if (loDt.Rows.Count == 0)
                    return null;
               return loDt;
          }
          #endregion
     }
	}
