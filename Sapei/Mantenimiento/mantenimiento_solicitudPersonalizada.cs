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
using Sapei.Framework.Utilerias;

namespace Sapei
{
	/// <summary>
	/// Clase mantenimiento_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class mantenimiento_solicitud
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
		#region Perzonalizados
		public DataTable RegresaTablaSolicitudes()
		{
			DataTable ldtTabla = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@periodo", ""));
				loParametros.Add(new ParametrosSQL("@tipo_solicitud", ""));
				loParametros.Add(new ParametrosSQL("@folio", ""));
				loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));
				loParametros.Add(new ParametrosSQL("@descripcion", ""));
				loParametros.Add(new ParametrosSQL("@estatus", ""));
				loParametros.Add(new ParametrosSQL("@bandera", 1));
				ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_mantenimiento_solicitud", loParametros));
			}
			return ldtTabla;
		}

		public void RegistraCadenaFiEl(enmTiposDocumentos penmTipo, string psTipo_solicitud, string psDescripcion)
		{

			List<ParametrosSQL> loParametrosDoc = new List<ParametrosSQL>();

			loParametrosDoc.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActual));
			loParametrosDoc.Add(new ParametrosSQL("@tipo_solicitud", psTipo_solicitud));
			loParametrosDoc.Add(new ParametrosSQL("@folio", ""));
			loParametrosDoc.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));
			loParametrosDoc.Add(new ParametrosSQL("@descripcion", psDescripcion));
			loParametrosDoc.Add(new ParametrosSQL("@estatus", 1));
			loParametrosDoc.Add(new ParametrosSQL("@bandera", 2));


			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pam_mantenimiento_solicitud", loParametrosDoc);
		}

		#endregion

	}
}
