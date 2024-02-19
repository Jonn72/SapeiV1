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
	/// Clase ss_dependencia generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class SS_Dependencia
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

        public DataTable RegresaRfc(string psRfc)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select rfc ");
            lsQuery.AppendFormat(" from ss_dependencias where rfc = '{0}' ", psRfc);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        //regresa los datos del autocompleta autocompletadep
        public DataTable RegresaDatos(string psRfc)
        {
            StringBuilder lsQuery = new StringBuilder();            
            lsQuery.Append(" select a.rfc, a.dependencia, a.titular, a.puesto_cargo, a.telefono, b.domicilio, b.numero, c.cod_post, c.colonia, c.ciudad_localidad ");
            lsQuery.AppendFormat(" from ss_dependencias a, ss_domicilio_programa b, c_p c ");            
            lsQuery.AppendFormat(" where a.rfc= '{0}' ", psRfc);
            lsQuery.AppendFormat(" and a.rfc = b.rfc_domicilio and c.id = b.id_cp ");
            DataTable loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable RfcDependenciaSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("select rfc");
            lsQuery.AppendFormat(" from {0} ", "ss_dependencias");
            lsQuery.Append(" order by rfc desc");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

        public DataTable DependenciasCompletoSS()
        {            
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append(" select '<a class=\"btn btn-success\" ><span class=\"fa fa-edit\"></span></a>' + case when  EXISTS(select rfc from ss_programa where rfc = a.rfc) then ' ' else '<a class=\"btn btn-danger\" ><span class=\"fa fa-trash\"></span></a>' end, ");
            lsQuery.Append(" a.rfc, a.dependencia, a.titular,a.puesto_cargo,a.telefono, b.domicilio, b.numero ");
            lsQuery.Append(", (select colonia from c_p where id = id_cp) cod_post");
            lsQuery.Append(", (select cod_post from c_p where id = id_cp) cod_post");
            lsQuery.AppendFormat(" from ss_dependencias a inner join ss_domicilio_programa b ");
            lsQuery.AppendFormat(" on a.rfc = b.rfc_domicilio ");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }

        public DataTable DependenciasSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("select rfc, dependencia, titular, puesto_cargo, telefono  ");
            lsQuery.AppendFormat(" from {0} P", RutaTabla);
            lsQuery.Append(" order by rfc desc");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }	

		}
	}
