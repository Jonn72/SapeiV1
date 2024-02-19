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
	/// Clase ss_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class SS_Reportes
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

        public DataTable ConsultaFolioSS(string psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select folio ");
            lsQuery.AppendFormat(" from ss_solicitud");
             lsQuery.AppendFormat(" where no_de_control = ('{0}') ", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable RegresaIdPrograma(string psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" Select id_programa from ss_solicitud");
            lsQuery.AppendFormat(" where no_de_control = ('{0}') ", psNoControl);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public int ConsultaNumeroReporte(string psNoControl)
        {
            StringBuilder lsQuery = new StringBuilder();
            int liValor;
            lsQuery.Append(" select isnull(max (no_reporte),0) + 1 as no_reporte ");
            lsQuery.AppendFormat(" from {0} ", RutaTabla );
            lsQuery.AppendFormat(" where no_de_control = '{0}'", psNoControl);
            liValor = Convert.ToInt32(_oSistema.Conexion.EjecutaEscalar(lsQuery));
            return Convert.ToInt32(liValor);
        }

        public DataTable ListaReporteFinalSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select case when estado >=10 then '<a class=\"btn btn-default\"><span class=\"fa fa-check\"></span></a>'  else '<a class=\"btn btn-primary\" ><span class=\"fa fa-check\"></span></a>'end ,no_de_control, (select nombre_alumno from alumnos where no_de_control = P.no_de_control) +' '+ (select apellido_paterno from alumnos where no_de_control = P.no_de_control) +' '+ (select apellido_materno from alumnos where no_de_control = P.no_de_control) from ss_estado_solicitud P where estado >= 10 ");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable AutoEvaluacionCualitativaPrestador(string lsNumero, string psReporte)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append(" Select a.no_de_control, a.no_reporte, a.p_1, a.p_2, a.p_3, a.p_4, a.p_5, a.p_6, a.p_7, ");
            lsConsulta.AppendFormat(" (select identificacion_larga from periodos_escolares where periodo = a.periodo) as identificacion_larga, ");
            lsConsulta.AppendFormat(" (select nombre_alumno from alumnos where no_de_control = a.no_de_control) as nombre_alumno, ");
            lsConsulta.AppendFormat(" (select apellido_paterno from alumnos where no_de_control = a.no_de_control) as apellido_paterno, ");
            lsConsulta.AppendFormat(" (select apellido_materno from alumnos where no_de_control = a.no_de_control) as apellido_materno, ");
            lsConsulta.AppendFormat(" (select nombre from ss_programa where id = a.id_programa) as nombre ");
            lsConsulta.AppendFormat(" from {0} a ", RutaTabla);
            lsConsulta.AppendFormat(" where a.no_de_control = '{0}'", lsNumero);
            lsConsulta.AppendFormat(" and a.no_reporte = {0}", psReporte);
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);
        }
        #endregion


    }
	}
