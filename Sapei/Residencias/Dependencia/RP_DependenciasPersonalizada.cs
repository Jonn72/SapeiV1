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
using Sapei.Framework.BaseDatos;

namespace Sapei
{
	/// <summary>
	/// Clase rp_dependencia generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class RP_Dependencias
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

        public DataTable DependenciasRP()
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append("select rfc, dependencia, titular, puesto_cargo, telefono  ");
            lsQuery.AppendFormat(" from {0} P", RutaTabla);
            lsQuery.Append(" order by rfc desc");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public DataTable DependenciasCompletoRP()
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append(" select '<a class=\"btn btn-success\" ><span class=\"fa fa-edit\"></span></a><a class=\"btn btn-info\" ><span class=\"fa fa-cubes\"></span></a>' + case when  EXISTS(select rfc_dependencia from rp_programa where rfc_dependencia = P.rfc) then ' ' else '<a class=\"btn btn-danger\" ><span class=\"fa fa-trash\"></span></a>' end, ");
			lsQuery.Append(" rfc, dependencia, titular, puesto_cargo, telefono ");                     
            lsQuery.AppendFormat(" from {0} P", RutaTabla);
            lsQuery.Append(" order by rfc desc");
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public DataTable RegresaDatos(string psRfc)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select A.rfc, A.dependencia, A.giro, A.mision, A.titular, A.puesto_cargo, A.telefono, B.domicilio, B.numero, ");
            lsQuery.Append(" (Select cod_post from c_p where id = B.id_cp) as cod_post,");
            lsQuery.Append(" (Select id from c_p where id = B.id_cp) as id");
            lsQuery.Append(" from rp_dependencias A, rp_domicilio_dependencias B ");
            lsQuery.AppendFormat(" where A.rfc = '{0}' ", psRfc);
            lsQuery.Append(" and A.rfc = B.rfc_domicilio");
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable RegresaDatosDependenciasRP(string psRfc, int tipo_consulta)
        {
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            loParametros.Add(new ParametrosSQL("@tipo_consulta", tipo_consulta));
            loParametros.Add(new ParametrosSQL("@rfc", psRfc));

            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rp_dependencias", loParametros));
            }
            return loDt;
        }

    }
	}
