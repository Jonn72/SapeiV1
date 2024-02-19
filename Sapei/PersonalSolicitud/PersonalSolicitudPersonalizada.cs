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
using Sapei.Framework.Utilerias;
using Sapei.Framework.BaseDatos;

namespace Sapei
{
	/// <summary>
	/// Clase personal_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class PersonalSoicitud
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

		public void RegistraCadenaFIEL(int psID, string psUsuario, enmTiposDocumentos penmTipo, int psStatus)
		{
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@id", psID));
			loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
			loParametros.Add(new ParametrosSQL("@tipo_documento", penmTipo.ToString()));
			loParametros.Add(new ParametrosSQL("@status", psStatus));

			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pai_fiel_horario_personal", loParametros);
		}

		public void RegistraCadenaFIELContrato(string psFechaCreacion, string psRFC, string psClaveArea, string psTipoContrato, string psUsuario, enmTiposDocumentos penmTipo, int psStatus)
		{
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@fecha_creacion", psFechaCreacion));
			loParametros.Add(new ParametrosSQL("@rfc", psRFC));
			loParametros.Add(new ParametrosSQL("@clave_area", psClaveArea));
			loParametros.Add(new ParametrosSQL("@tipo_contrato", psTipoContrato));
			loParametros.Add(new ParametrosSQL("@usuario", psUsuario));
			loParametros.Add(new ParametrosSQL("@tipo_documento", penmTipo.ToString()));
			loParametros.Add(new ParametrosSQL("@status", psStatus));

			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pai_fiel_contrato", loParametros);
		}

		public void RegistraCadenaFIELActaCLE(string psPeriodo, string psNivel, string psGrupo, string psFacilitador, enmTiposDocumentos penmTipo)
		{
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
			loParametros.Add(new ParametrosSQL("@nivel", psNivel));
			loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
			loParametros.Add(new ParametrosSQL("@facilitador", psFacilitador));
			loParametros.Add(new ParametrosSQL("@tipo_documento", penmTipo.ToString()));
			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pai_fiel_acta_cle", loParametros);
		}

	}

}
