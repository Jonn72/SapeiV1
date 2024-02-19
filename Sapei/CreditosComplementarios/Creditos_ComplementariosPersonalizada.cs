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
	/// Clase creditos_complementario generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Creditos_Complementarios
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

		public string GeneraFolioLiberacion(string psPeriodo, string psNoControl, string psTipo)
		{
			StringBuilder lsQuery = new StringBuilder();
			lsQuery.AppendFormat("IF EXISTS (SELECT 1 FROM creditos_complementarios_folios WHERE no_de_control = '{0}' AND tipo = '{1}')", psNoControl, psTipo);
			lsQuery.AppendFormat(" SELECT folio FROM creditos_complementarios_folios WHERE no_de_control = '{0}' AND tipo = '{1}'", psNoControl, psTipo);
			lsQuery.Append(" ELSE ");
			lsQuery.Append(" BEGIN ");
			lsQuery.Append(" DECLARE @valor INT ");
			lsQuery.AppendFormat(" SELECT @valor = ISNULL(MAX(convert(int,folio))+1,0) + 1 FROM creditos_complementarios_folios WHERE periodo = '{0}' AND tipo = '{1}'", psPeriodo, psTipo);
			lsQuery.Append("INSERT INTO creditos_complementarios_folios (periodo, no_de_control, folio, fecha_registro, tipo)");
			lsQuery.AppendFormat(" values ('{0}','{1}',@valor,getdate(),'{2}')", psPeriodo, psNoControl, psTipo);
			lsQuery.Append(" SELECT @valor");
			lsQuery.Append(" END ");
			return Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
		}

		public DataTable RegresaDatosActividadesComplementarias(string psControl, bool pbSoloAprobadas)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));
				loParametros.Add(new ParametrosSQL("@solo_aprobados", pbSoloAprobadas));


				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_creditos_complementarios_alumno", loParametros));
			}
			return loDt;
		}
		public DataTable RegresaTablaEstudiantesActividadesComplementarias(string psTipo, string psCarreras = null)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("SELECT periodo, no_de_control, ");
			lsConsulta.Append("(select apellido_paterno +' '+ apellido_materno +' '+nombre_alumno from alumnos where no_de_control = C.no_de_control) nombre,");
			lsConsulta.Append("fecha_registro, promedio");
			lsConsulta.AppendFormat(" from {0} C", RutaTabla);
			lsConsulta.AppendFormat(" where tipo = '{0}' ", psTipo);
			if (!string.IsNullOrEmpty(psCarreras))
				lsConsulta.AppendFormat(" and no_de_control in (select no_de_control from alumnos where carrera in ({0})) ", psCarreras);
			lsConsulta.Append(" ORDER BY periodo desc");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);
		}
		#endregion

	}
}
