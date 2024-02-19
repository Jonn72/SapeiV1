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

namespace Sapei
{
	/// <summary>
	/// Clase aviso_reinscripcion_pago generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class RegistroPagos
	{
		#region Contructor

		private Sistema _oSistema;
		/// <summary>
		/// Inicia una nueva instancia de la clase aviso_reinscripcion_pago.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public RegistroPagos(Sistema poSistema)
		{
			_oSistema = poSistema;
		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece periodo.Sin descripcion para periodo 
		/// </summary>
		/// <value>
		/// periodo 
		/// </value>
		[Required]
		[MaxLength(5)]
		[DefaultValue(null)]
		public string periodo { get; set; }
		/// <summary>
		/// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
		/// </summary>
		/// <value>
		/// no_de_control 
		/// </value>
		[Required]
		[MaxLength(10)]
		[DefaultValue(null)]
		public string no_de_control { get; set; }
		/// <summary>
		/// Obtiene o establece fecha_corte.Sin descripcion para fecha_corte 
		/// </summary>
		/// <value>
		/// fecha_corte 
		/// </value>
		[Required]
		[MaxLength(6)]
		[DefaultValue(null)]
		public string fecha_corte { get; set; }
		/// <summary>
		/// Obtiene o establece referencia_1.Sin descripcion para referencia_1 
		/// </summary>
		/// <value>
		/// referencia_1 
		/// </value>
		[Required]
		[MaxLength(20)]
		[DefaultValue(null)]
		public string referencia_1 { get; set; }
		/// <summary>
		/// Obtiene o establece sucursal.Sin descripcion para sucursal 
		/// </summary>
		/// <value>
		/// sucursal 
		/// </value>
		[Required]
		[MaxLength(3)]
		[DefaultValue(null)]
		public string sucursal { get; set; }
		/// <summary>
		/// Obtiene o establece importe.Sin descripcion para importe 
		/// </summary>
		/// <value>
		/// importe 
		/// </value>
		[Required]
		public Double importe { get; set; }
		/// <summary>
		/// Obtiene o establece autorizacion.Sin descripcion para autorizacion 
		/// </summary>
		/// <value>
		/// autorizacion 
		/// </value>
		[Required]
		[MaxLength(10)]
		[DefaultValue(null)]
		public string autorizacion { get; set; }
		/// <summary>
		/// Obtiene o establece forma_pago.Sin descripcion para forma_pago 
		/// </summary>
		/// <value>
		/// forma_pago 
		/// </value>
		[Required]
		[MaxLength(2)]
		[DefaultValue(null)]
		public string forma_pago { get; set; }

		/// <summary>
		/// Obtiene o establece fecha_registro.Sin descripcion para fecha_registro 
		/// </summary>
		/// <value>
		/// fecha_registro 
		/// </value>
		[Required]
		public DateTime fecha_registro { get; set; }
		/// <summary>
		/// Obtiene o establece usuario.Sin descripcion para usuario 
		/// </summary>
		/// <value>
		/// usuario 
		/// </value>
		[Required]
		[MaxLength(30)]
		[DefaultValue(null)]
		public string usuario { get; set; }
		#endregion
		#region Funciones
		public string GuardaRegistros(string psCadena, string psNombreArchivo)
		{
			DataTable loTabla;
			DataRow loFila;
			string[] lasRegistro;
			string lsCadena;
			string lsControl;
			string lsReferencia;
			string lsFolio;
			int liFIla;
			StringBuilder lsErrores;
			loTabla = new DataTable();
			loTabla.Columns.Add("periodo");
			loTabla.Columns.Add("no_de_control");
			loTabla.Columns.Add("fecha_corte");
			loTabla.Columns.Add("referencia_1");
			loTabla.Columns.Add("sucursal");
			loTabla.Columns.Add("importe");
			loTabla.Columns.Add("autorizacion");
			loTabla.Columns.Add("forma_pago");
			loTabla.Columns.Add("procesado");
			loTabla.Columns.Add("fecha_registro", System.Type.GetType("System.DateTime"));
			loTabla.Columns.Add("usuario");
			loTabla.Columns.Add("observaciones");
			loTabla.Columns.Add("folio");
			lsCadena = psCadena.Remove(0, psCadena.IndexOf("\n") + 1);
			lsErrores = new StringBuilder();
			liFIla = 0;
			this.fecha_corte = (lsCadena.Trim().Split(new char[2] { '\n', '\r' })[0]).Split(',')[1];
			if (!ValidaArchivo())
			{
				return "Este archivo ya fue procesado anteriormente";
			}
			this.periodo = _oSistema.Sesion.Periodo.PeriodoActualSinVerano.Trim();
			this.usuario = _oSistema.Sesion.Usuario.Usuario;
			this.fecha_registro = DateTime.Now;
			foreach (string lsRegistro in lsCadena.Trim().Split(new char[2] { '\n', '\r' }))
			{
				loFila = loTabla.NewRow();
				if (string.IsNullOrWhiteSpace(lsRegistro))
					continue;
				lasRegistro = lsRegistro.Trim().Split(',');
				if (lasRegistro.Length != 11)
					return "El archivo no contiene el número de columnas requeridas en la fila " + Convert.ToString(liFIla);

				lsReferencia = lasRegistro[10].Trim().LTrim('0');
				if (lsReferencia.Contains("F"))
				{
					lsFolio = lsReferencia.Split('F')[1];
					lsControl = lsReferencia.Split('F')[2];
				}
				else
				{
					lsFolio = "";
					lsControl = lsReferencia;
				}

				loFila["periodo"] = this.periodo;
				loFila["no_de_control"] = lsControl;
				loFila["fecha_corte"] = lasRegistro[1].Trim();
				loFila["referencia_1"] = lasRegistro[3].Trim();
				loFila["sucursal"] = lasRegistro[6].Trim();
				loFila["importe"] = lasRegistro[8].Trim().Split('.')[0];
				loFila["autorizacion"] = lasRegistro[9].Trim();
				loFila["forma_pago"] = "10";//lasRegistro[11].Trim();
				loFila["procesado"] = false;
				loFila["fecha_registro"] = this.fecha_registro;
				loFila["usuario"] = this.usuario;
				loFila["observaciones"] = "";
				loFila["folio"] = lsFolio;
				loTabla.Rows.Add(loFila);
				liFIla = liFIla + 1;
			}
			_oSistema.Conexion.InsertBulkCopy(loTabla, "rf_precarga_pagos");
			return null;
		}
		private bool ValidaArchivo()
		{
			StringBuilder lsQuery = new StringBuilder();
			string lsValor;
			lsQuery.Append("SELECT  1");
			lsQuery.Append(" from rf_precarga_pagos A");
			lsQuery.AppendFormat(" where periodo = '{0}' and fecha_corte = {1}", _oSistema.Sesion.Periodo.PeriodoActual, this.fecha_corte);
			lsQuery.Append(" order by no_de_control asc");
			lsValor = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
			if (string.IsNullOrEmpty(lsValor))
				return true;
			return false;
		}


		#endregion

	}
}
