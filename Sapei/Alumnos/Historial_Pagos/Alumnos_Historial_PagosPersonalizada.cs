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
	/// Clase alumnos_historial_pago generada automáticamente desde el Generador de Código Sapei
	/// </summary>
	public partial class Alumnos_Historial_Pagos
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
		public DataTable RegresaHistorialPagos(string psControl)
		{
			StringBuilder lsQuery = new StringBuilder();
			lsQuery.Append("SELECT (select identificacion_corta from periodos_escolares where periodo = A.periodo) periodo");
			lsQuery.Append(",(select concepto from servicios where clave = A.clave)");
			lsQuery.Append(",[monto],[abonado],[condonacion]");
			lsQuery.AppendFormat(" FROM {0} A", RutaTabla);
			lsQuery.AppendFormat(" WHERE  no_de_control = '{0}'",psControl);
			return _oSistema.Conexion.RegresaDataTable(lsQuery);
		}
		public DataTable RegresaDatosPagoActual(string psControl)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano));
				loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.RolUsuario.ToString()));

				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rf_genera_datos_pago", loParametros));
			}
			return loDt;
		}
		public DataTable RegresaDatosPagoProrroga(string psControl)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano));
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rf_datos_prorroga", loParametros));
			}
			return loDt;
		}
		public DataTable RegresaDatosFichaPagoActual(string psControl)
		{
			DataTable loDt = new DataTable();
			string lsDatos;
			string lsCadenaQR;
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano));
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rf_datos_ficha_pago", loParametros));
			}

			lsDatos = string.Format("Fecha de emisión:{0}&Periodo:{1}&No Control:{2}&Nombre:{3}&Documento:{4}",
			DateTime.Now.ToShortDateString(), loDt.Rows[0].Field<string>("periodo"), loDt.Rows[0].Field<string>("no_de_control"), loDt.Rows[0].Field<string>("nombre"), "Ficha de pago");
			lsCadenaQR = lsDatos.MD5HASH();
			loDt.Columns["qr"].ReadOnly = false;
			loDt.Columns["cb"].ReadOnly = false;
			loDt.Rows[0].SetField("qr", lsCadenaQR.RegresaQRValidacionDocumentos());

			foreach (DataRow loRow in loDt.Rows)
			{
				loRow.SetField("cb", loRow.Field<string>("referencia").RegresaCodigoQR());
			}

			_oSistema.GrabaValidacionDocumento(lsCadenaQR, lsDatos);
			return loDt;
		}
		public DataTable RegresaDatosPagoActual(string psPeriodo, string psControl)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));

				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rf_datos_pago_reinscripcion", loParametros));
			}
			return loDt;
		}
		public DataTable ActivaBloqueaPago(string psPeriodo, string psControl, bool pbAccion)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				if (pbAccion)
					loParametros.Add(new ParametrosSQL("@accion", 1));
				else
					loParametros.Add(new ParametrosSQL("@accion", 0));
				loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));

				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rf_datos_pago_reinscripcion", loParametros));
			}
			return loDt;
		}
		#endregion

	}
	}
