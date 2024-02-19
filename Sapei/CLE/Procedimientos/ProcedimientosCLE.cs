using System;
using System.Collections.Generic;
using System.Data;
using Sapei.Framework;
using Sapei.Framework.Datos;
using Sapei.Framework.BaseDatos;

namespace Sapei
{
	/// <summary>
	/// Clase historia_alumno_ingle generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class ProcedimientosCLE
	{
		#region Variables
		protected internal Sistema _oSistema;
		#endregion

		#region Contructor

		/// <summary>
		/// Inicia una nueva instancia de la clase historia_alumno_ingle.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public ProcedimientosCLE(Sistema poSistema)
		{
			_oSistema = poSistema;
		}
		#endregion
		#region Funciones
		public DataTable RegresaRegistroPagoCLE(string psPeriodo, string psControl)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
			loParametros.Add(new ParametrosSQL("@no_de_control", psControl));

			using (var loCOnexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_cle_datos_avance_pago", loParametros));
			}
			return loDt;
		}
		public DataTable EliminaRegistraPagoCLE(string psPeriodo, string psControl, bool pbAccion)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
			loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
			if(pbAccion)
				loParametros.Add(new ParametrosSQL("@accion", 1));
			else
				loParametros.Add(new ParametrosSQL("@accion", 0));
			loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));

			using (var loCOnexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_cle_pago", loParametros));
			}
			return loDt;
		}
		#endregion

	}
	}
