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
	/// Clase ss_programa generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class SS_Programa
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


        //REGRESA EL NOMBRE, RESPONSABLE, CARGO DEL RESPONSABLE Y CORREO DEL PROGRAMA 
        public DataTable RegresaPrograma(string psPrograma)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select [nombre],[responsable],[cargo_responsable],[correo_titular],[departamento],[id_tipo_programa],[objetivo] ");
            lsQuery.AppendFormat(" from ss_programa where id = '{0}' ", psPrograma);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        //REGRESA EL ID Y NOMBRE DEL PROGRAMA CORRESPONDIENTE AL RFC INGRESADO
        public DataTable RegresaNombreProgramaSS(string psRfc)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("Select id, nombre");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where rfc = '{0}'", psRfc);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        //REGRESA EL ID DEL RFC INGRESADO PARA EL PROGRAMA
        public DataTable RegresaIdPrograma(string psRfcDependencia)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" Select id ");
            lsQuery.AppendFormat(" from {0} ", RutaTabla);
            lsQuery.AppendFormat(" where rfc = ('{0}') ", psRfcDependencia);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
        public DataTable ConsultaProgramaSS(string psnombre, string psDepartamento, string psRFC)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select nombre");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where nombre = '{0}' ", psnombre);
            lsQuery.AppendFormat(" and departamento = '{0}'", psDepartamento);
            lsQuery.AppendFormat(" and rfc = '{0}'", psRFC);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable RegresaProgramaDepartamento(string psPrograma)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append(" select [nombre],[responsable],[cargo_responsable],[correo_titular],[departamento],(select descripcion from ss_tipo_programa where id =a.id_tipo_programa) as id_tipo_programa,[objetivo] ");
            lsQuery.AppendFormat(" from ss_programa a where id = '{0}' ", psPrograma);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }

        public DataTable RegresaNombreDepartamentoSS(string psDepartamento)
        {
            StringBuilder lsQuery = new StringBuilder();
            DataTable loDt;
            lsQuery.Append("Select [id],[nombre]");
            lsQuery.AppendFormat(" from {0}", RutaTabla);
            lsQuery.AppendFormat(" where departamento = '{0}'", psDepartamento);
            loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
            if (loDt.Rows.Count == 0)
                return null;
            return loDt;
        }
		
		#endregion

		}
	}
