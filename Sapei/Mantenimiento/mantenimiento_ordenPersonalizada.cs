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
	/// Clase mantenimiento_orden generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class mantenimiento_orden
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
		#region Personalizado
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
				loParametros.Add(new ParametrosSQL("@bandera", 4));
				ldtTabla.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_mantenimiento_solicitud", loParametros));
			}
			return ldtTabla;
		}

		public void RegistraCadenaFiElAceptada(enmTiposDocumentos penmTipo, string psFolio)
		{
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			string lsTipoServicio;
			if (_oSistema.Sesion.Usuario.RolUsuario == Framework.Configuracion.enmRolUsuario.SAD)
				lsTipoServicio = "CC";
			else
				lsTipoServicio = "XX";

			loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActual));
			loParametros.Add(new ParametrosSQL("@tipo_solicitud", lsTipoServicio));
			loParametros.Add(new ParametrosSQL("@folio", psFolio));
			loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));
			loParametros.Add(new ParametrosSQL("@tipo_documento", penmTipo.ToString()));
			loParametros.Add(new ParametrosSQL("@bandera", 2));

			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pai_fiel_solicitud_mantenimiento", loParametros);

		}

		public void RegistraCadenaFiElOrdenLiberada(enmTiposDocumentos penmTipo, string psFolio, string psAreaSolicitada, string psTipoMantenimiento, string psTipoServicio, string psAsignado, string psTrabajoRealizado)
		{
			string lsTipoServicio;
			if (_oSistema.Sesion.Usuario.RolUsuario == Framework.Configuracion.enmRolUsuario.SAD)
				lsTipoServicio = "CC";
			else
				lsTipoServicio = "XX";


			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

			loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActual));
			loParametros.Add(new ParametrosSQL("@tipo_solicitud", lsTipoServicio));
			loParametros.Add(new ParametrosSQL("@folio", psFolio));
			loParametros.Add(new ParametrosSQL("@bandera", 1));

			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pai_fiel_orden_matenimiento", loParametros);



			loParametros = new List<ParametrosSQL>();

			loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActual));
			loParametros.Add(new ParametrosSQL("@folio", psFolio));
			loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));
			loParametros.Add(new ParametrosSQL("@tipo_mantenimiento", psTipoMantenimiento));
			loParametros.Add(new ParametrosSQL("@tipo_servicio", psTipoServicio));
			loParametros.Add(new ParametrosSQL("@asignado", psAsignado));
			loParametros.Add(new ParametrosSQL("@fecha_realizado", DateTime.Now));
			loParametros.Add(new ParametrosSQL("@trabajo", psTrabajoRealizado));
			loParametros.Add(new ParametrosSQL("@estatus", 3));
			loParametros.Add(new ParametrosSQL("@bandera", 1));
			loParametros.Add(new ParametrosSQL("@tipo_solicitud", lsTipoServicio));

			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pam_mantenimiento_orden", loParametros);

		}

		public void RegistraCadenaFiElOrdenFinalizada(enmTiposDocumentos penmTipo, string psFolio)
		{
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

			loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActual));
			loParametros.Add(new ParametrosSQL("@tipo_solicitud", ""));
			loParametros.Add(new ParametrosSQL("@folio", psFolio));
			loParametros.Add(new ParametrosSQL("@bandera", 2));

			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pai_fiel_orden_matenimiento", loParametros);


			List<ParametrosSQL> loParametrosUp = new List<ParametrosSQL>();

			loParametrosUp.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActual));
			loParametrosUp.Add(new ParametrosSQL("@folio", psFolio));
			loParametrosUp.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));
			loParametrosUp.Add(new ParametrosSQL("@tipo_mantenimiento", ""));
			loParametrosUp.Add(new ParametrosSQL("@tipo_servicio", ""));
			loParametrosUp.Add(new ParametrosSQL("@asignado", ""));
			loParametrosUp.Add(new ParametrosSQL("@fecha_realizado", ""));
			loParametrosUp.Add(new ParametrosSQL("@trabajo", ""));
			loParametrosUp.Add(new ParametrosSQL("@estatus", "4"));
			loParametrosUp.Add(new ParametrosSQL("@bandera", 2));
			loParametrosUp.Add(new ParametrosSQL("@tipo_solicitud", ""));

			_oSistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pam_mantenimiento_orden", loParametrosUp);
		}
		#endregion

	}
}
