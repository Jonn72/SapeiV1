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
	/// Clase alumnos_historial_servicio generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Alumnos_Historial_Servicios
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
			lsQuery.Append(",(select concepto from servicios where servicio = clave)");
			lsQuery.Append(",[fecha_solicitud],case pago_registrado when 1 then 'SI' else 'NO' end registrado");
			lsQuery.AppendFormat(" FROM {0} A", RutaTabla);
			lsQuery.AppendFormat(" WHERE  no_de_control = '{0}'", psControl);
			return _oSistema.Conexion.RegresaDataTable(lsQuery);
		}
		public DataTable RegresaDatosFichaServicio(string psControl, string psServicio)
		{
			DataTable loDt = new DataTable();
			string lsDatos;
			string lsCadenaQR;
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano));
				loParametros.Add(new ParametrosSQL("@servicio", psServicio));

				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rf_datos_ficha_servicio", loParametros));
			}
			if (loDt.Rows.Count == 0)
				return null;
			lsDatos = string.Format("Fecha de emisión:{0}&Periodo:{1}&No Control:{2}&Nombre:{3}&Documento:{4}",
			DateTime.Now.ToShortDateString(), loDt.Rows[0].Field<string>("periodo"), loDt.Rows[0].Field<string>("no_de_control"), loDt.Rows[0].Field<string>("nombre"), "Ficha de pago");
			lsCadenaQR = lsDatos.MD5HASH();
			loDt.Columns["qr"].ReadOnly = false;
			loDt.Columns["cb"].ReadOnly = false;
			loDt.Rows[0].SetField("qr", lsCadenaQR.RegresaQRValidacionDocumentos());
			loDt.Rows[0].SetField("cb", loDt.Rows[0].Field<string>("referencia").RegresaCodigoQR());
			_oSistema.GrabaValidacionDocumento(lsCadenaQR, lsDatos);
			return loDt;
		}

		public object CargarDatosPagoServicios(string psPeriodo, string psNoControl, string psLineaPago)
		{
			StringBuilder lsConsulta;
			DataTable loDt;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select ");
			lsConsulta.AppendFormat("(select apellido_paterno + ' ' + apellido_materno + ' ' + nombre_alumno from alumnos where no_de_control = '{0}') nombre,", psNoControl);
			lsConsulta.Append(" pago_registrado, concepto,monto");
			lsConsulta.AppendFormat(" FROM {0} A inner join servicios S on A.servicio = S.clave", RutaTabla);
			lsConsulta.AppendFormat(" WHERE linea_captura = '{0}' AND no_de_control = '{1}'", psLineaPago, psNoControl);

			loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);
			if (loDt.Rows.Count == 0)
				return null;
			return loDt;
		}
		#endregion
	}
	}
