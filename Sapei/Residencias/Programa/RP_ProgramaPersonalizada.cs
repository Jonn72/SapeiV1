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
	/// Clase rp_programa generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class RP_Programa
	{
        class Programa
        {
            public string nombre;
            public string correo;
            public string departamento;
            public string responsable;
            public string cargo;
        }
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
        public void GuardaBitacora(string psControl, string psPeriodo,string psNombreProyecto, string psRfcDependencia)
        {
            StringBuilder lsConsulta = new StringBuilder();
            lsConsulta.Append("INSERT INTO rp_bitacora_alta_programa (no_de_control,periodo,nombre_programa,rfc_dependencia)");
            lsConsulta.Append(" values");
            lsConsulta.AppendFormat("('{0}','{1}','{2}','{3}')",psControl, psPeriodo, psNombreProyecto, psRfcDependencia);
            _oSistema.Conexion.EjecutaComando(lsConsulta);
        }
        public DataTable RegresaNombrePrograma(string psRfcDependencia, string lsCarrera)
          {
            DataTable loDt;
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append(" SELECT id, nombre");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where rfc_dependencia = '{0}'", psRfcDependencia);
            lsQuery.AppendFormat(" and carrera = '{0}'", lsCarrera);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public int ConsultaNoProgramaRP(string psPeriodo, string lsCarrera)
        {
            StringBuilder lsQuery = new StringBuilder();
            string lsValor;
            lsQuery.Append(" select isnull(max(numero_proyecto),0) + 1 as numero_proyecto ");
            lsQuery.AppendFormat(" from {0} ", RutaTabla);
            lsQuery.AppendFormat(" where periodo_programa = '{0}'", psPeriodo);
            lsQuery.AppendFormat(" and carrera = '{0}'", lsCarrera);
            lsValor = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
            return Convert.ToInt32(lsValor);
        }
        public DataTable RegresaDatosPrograma(string psIdPrograma, string lsNumero)
        {
            DataTable loDt;
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("Select ");
            lsQuery.Append(" nombre, correo, departamento, responsable, cargo, opcion_programa, ");
            lsQuery.AppendFormat(" (select count(no_de_control) from rp_bitacora_alta_programa where no_de_control = '{0}') as registro", lsNumero);
            lsQuery.AppendFormat(" from {0} ", RutaTabla);
            lsQuery.AppendFormat(" where id = {0} ", psIdPrograma);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable ConsultaProgramaRP(string psNombre, string psRfcDependencia, string lscarrera)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select nombre ");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where nombre = '{0}'", psNombre);
            lsQuery.AppendFormat(" and carrera = '{0}'", lscarrera);
            lsQuery.AppendFormat(" and rfc_dependencia = '{0}'", psRfcDependencia);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ConsultaNombreProgramaRP(string psid)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("select nombre");
            lsQuery.AppendFormat(" from  {0}", RutaTabla);
            lsQuery.AppendFormat(" where id = {0}", psid);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable AutorizacionAnteproyectoRP(string psUsuario)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select distinct '<a  id=\"btn-verAnteproyecto\" class=\"btn btn-info\"><span class=\"fa fa-eye\"></span></a> <a  id=\"btn-validaAnteproyecto\" '+ case when  C.estado>= 8 then 'class=\"btn btn-default\"' else 'class=\"btn btn-success\"' end +'><span class=\"fa fa-check\"></span></a>',");
            lsQuery.Append(" B.id, B.numero_proyecto, B.nombre, (select duracion from rp_datos_programa where id_programa = B.id) from rp_solicitud A inner join rp_programa B on A.id_programa = B.id, rp_estado_solicitud C");
            lsQuery.AppendFormat(" where A.estado_solicitud = 1 and B.carrera in (select carrera from sis_permisos_carreras where usuario = '{0}' )", psUsuario);
            lsQuery.Append(" and A.no_de_control = C.no_de_control and C.estado >= 7");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        #endregion
    }
	}
