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
	/// Clase extra_actividades_fecha generada automáticamente desde el Generador de Código SII
	/// </summary>
     public partial class Control_Vehicular
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
          public DataTable RegresaTablaPeriodos(string psPeriodo = null)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT periodo, ini_registro, fin_registro,ini_entrega, fin_entrega, max_autos, autos_registrados, max_motos, motos_registradas ");
               lsConsulta.AppendFormat(" FROM {0}", RutaTabla);
               lsConsulta.Append(" order by periodo desc");
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public string ValidaPeriodo()
          {
               DataTable loTabla;
               DateTime loFechaIni, loFechaFin;
               loTabla = RegresaTablaPeriodos(_oSistema.Sesion.Periodo.PeriodoActual);
               if (loTabla.Rows.Count == 0)
               {
                    return "Aún no hay fechas registradas para el periodo actual";
               }
               loFechaIni = loTabla.Rows[0].Field<DateTime>("ini_registro");
               loFechaFin = loTabla.Rows[0].Field<DateTime>("fin_registro");
               if (Sapei.Framework.Utilerias.ManejoFechas.ToBetween(loFechaIni, loFechaFin))
                    return null;
               return string.Format("La fecha para registro vehícular son {0} al {1}", loFechaIni, loFechaFin);
          }
          public string ValidaUsuario()
          {
               StringBuilder lsConsulta;
               bool lbEsDocente;
               string lsResultado;
               string lsMensaje;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT distinct 1 ");
               lbEsDocente = _oSistema.Sesion.Usuario.RolUsuario == Framework.Configuracion.enmRolUsuario.DOC;
               if (lbEsDocente)
               {
                    lsConsulta.AppendFormat(" FROM grupos WHERE periodo = '{0}' AND rfc = '{1}'", _oSistema.Sesion.Periodo.PeriodoActual, _oSistema.Sesion.Usuario.Usuario);
                    lsMensaje = "Estimado docente, no puede registrar vehículo debido a que no tiene materias asignadas en este periodo";
               }
               else
               {
                    lsConsulta.AppendFormat(" FROM seleccion_materias WHERE periodo = '{0}' AND no_de_control = '{1}'", _oSistema.Sesion.Periodo.PeriodoActual, _oSistema.Sesion.Usuario.Usuario);
                    lsMensaje = "Estimado guardian, no puede registrar vehículo debido a que no tiene materias seleccionadas en este periodo";
               }
               lsResultado = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsConsulta));
               if (!string.IsNullOrEmpty(lsResultado))
                    return null;
               return lsMensaje;
          }
          #endregion
		}
	}
