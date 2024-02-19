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
	/// Clase cle_lista_seleccion generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Cle_Liberacion_Ingles
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

		public DataTable RegresaAlumnosLiberado(string psNoControl = "")
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@no_de_control",psNoControl));
			using (var conexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_cle_alumnos_liberados",loParametros));
			}
			return loDt;
		}

		public void RegistraLiberacion(string psNoControl, Int16 piTipoLiberacion, int piPromedio)
		{
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
			loParametros.Add(new ParametrosSQL("@tipo_liberacion", piTipoLiberacion));
			loParametros.Add(new ParametrosSQL("@promedio", piPromedio));
			loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));
			using (var conexion = new ManejaConexion(_oSistema.Conexion))
			{
				var respuesta = _oSistema.Conexion.EjecutaEscalarProcedimientoAlmacenado("pam_cle_alumnos_liberados", loParametros);
			}
		}

		public DataTable RegresaDatosConstancia(string psNoControl, Framework.Utilerias.enmCleDocumentos penmTipo, string psOficio = null, string psFecha=null)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
			loParametros.Add(new ParametrosSQL("@tipo_documento", penmTipo.ToString()));

			using (var conexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_cle_datos_constancias", loParametros));
			}
			return loDt;
		}

		public DataTable RegresaAlumnosCambioCalificacion(string psNoControl = "", string psPeriodo = "")
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
			loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));

			using (var conexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_cle_modifica_calificacion", loParametros));
			}
			return loDt;
		}

		public void RegistraCalificaciones(string psPeriodo, string psNoControl, string psObjeto = null)
		{
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
            loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
			loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));
			loParametros.Add(new ParametrosSQL("@objeto", psObjeto));
			using (var conexion = new ManejaConexion(_oSistema.Conexion))
			{
				var respuesta = _oSistema.Conexion.EjecutaEscalarProcedimientoAlmacenado("pam_cle_modifica_calificacion", loParametros);
			}
		}
	}
}
#endregion