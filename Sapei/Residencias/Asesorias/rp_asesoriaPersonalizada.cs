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
	/// Clase rp_asesoria generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class RP_Asesoria
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

        public DataTable AsesoriasEstudianteRP(string lsNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select '<a class=\"btn btn-primary\"><span class=\"fa fa-print\"></span></a>");
            lsQuery.Append(" '+ case when A.estado = 2 then '<a class=\"btn btn-default\"><span class=\"fa fa-check\"></span></a>' ");
            lsQuery.Append(" else '<a class=\"btn btn-success\"><span class=\"fa fa-pencil\"></span></a>' end+'',");
            lsQuery.Append("  A.numero_asesoria, A.fecha ");
            lsQuery.AppendFormat("from {0} A ", RutaTabla);
            lsQuery.Append(" inner join rp_solicitud B on A.folio = B.folio");
            lsQuery.AppendFormat(" where A.no_de_control = '{0}' ", lsNoControl);
            lsQuery.Append(" and B.estado_solicitud = 1");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable RegresaAsesoriaRP(int psNumeroAsesoria, string psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select id,numero_asesoria, tipo, descripcion, solucion ");
            lsQuery.AppendFormat("  from {0} ", RutaTabla);
            lsQuery.AppendFormat(" where no_de_control = '{0}' ", psNoControl);
            lsQuery.AppendFormat(" and numero_asesoria = {0}",psNumeroAsesoria);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable RegresaMaxAsesoriaRP(string psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select IsNull(max(numero_asesoria),0) as  numero_asesoria");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where no_de_control = '{0}' ", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ResidenteAsesoriaRP(string psUsuario)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" SELECT case when A.estado = 2 then '<a class=\"btn btn-default\"><span class=\"fa fa-check\"></span></a>' else '<a class=\"btn btn-success\"><span class=\"fa fa-check\"></span></a>' end , ");
            lsQuery.Append("  A.no_de_control, A.numero_asesoria,");
            lsQuery.Append(" (select nombre+' '+paterno+' '+ materno from estudiantes_datos_completos where no_de_control = A.no_de_control)");
            lsQuery.Append("  from rp_asesorias A inner join rp_programa B on A.id_programa = B.id");
            lsQuery.AppendFormat("  where B.carrera in (select carrera from sis_permisos_carreras where usuario = '{0}' and carrera = B.carrera)", psUsuario);
			lsQuery.Append(" Order By A.numero_asesoria desc");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public void ValidaAsesoriaRP(string psNocontrol, int psNoAsesoria, int piEstado)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append(" UPDATE rp_asesorias ");
            lsQuery.AppendFormat(" SET estado = {0}", piEstado);
            lsQuery.AppendFormat(" WHERE no_de_control = '{0}'", psNocontrol);
            lsQuery.AppendFormat(" and numero_asesoria = {0}", psNoAsesoria);
            _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
		}
	}
