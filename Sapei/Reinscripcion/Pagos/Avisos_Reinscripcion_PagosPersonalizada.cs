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
	public partial class Avisos_Reinscripcion_Pagos
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
		public string GuardaRegistros(string psCadena, string psNombreArchivo)
		{
			DataTable loTabla;
			DataRow loFila;
			string[] lasRegistro;
			string lsCadena;
			string lsControl;
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
				if (lasRegistro.Length != 12)
					return "El archivo no contiene el número de columnas requeridas en la fila " + Convert.ToString(liFIla);

				lsControl = lasRegistro[10].Trim().LTrim('0');

				loFila["periodo"] = this.periodo;
				loFila["no_de_control"] = lsControl;
				loFila["fecha_corte"] = lasRegistro[1].Trim();
				loFila["referencia_1"] = lasRegistro[3].Trim();
				loFila["sucursal"] = lasRegistro[6].Trim();
				loFila["importe"] = lasRegistro[8].Trim();
				loFila["autorizacion"] = lasRegistro[9].Trim();
				loFila["forma_pago"] = "10";//lasRegistro[11].Trim();
				loFila["procesado"] = false;
				loFila["fecha_registro"] = this.fecha_registro;
				loFila["usuario"] = this.usuario;
				loFila["observaciones"] = "";
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
