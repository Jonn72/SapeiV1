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
	/// Clase historia_alumno_ingle generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Historia_alumno_ingles
	{
		#region variables
		public string grupo { get; set; }
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
			string lsError;
			int liPromedio;
			int liFIla;
			StringBuilder lsErrores;
			loTabla = new DataTable();
			loTabla.Columns.Add("periodo");
			loTabla.Columns.Add("no_de_control");
			loTabla.Columns.Add("nivel");
			loTabla.Columns.Add("grupo");
			loTabla.Columns.Add("promedio");
			loTabla.Columns.Add("asistencia");
			loTabla.Columns.Add("h1");
			loTabla.Columns.Add("h2");
			loTabla.Columns.Add("h3");
			loTabla.Columns.Add("h4");
			loTabla.Columns.Add("nombre_archivo");
			lsCadena = psCadena.Remove(0, psCadena.IndexOf("\n") + 1);
			lsErrores = new StringBuilder();
			liFIla = 0;
			this.periodo = _oSistema.Sesion.Periodo.PeriodoActualSinVerano;
			this.nivel = (lsCadena.Trim().Split(new char[2] { '\n', '\r' })[0]).Split(',')[2];
			this.grupo = (lsCadena.Trim().Split(new char[2] { '\n', '\r' })[0]).Split(',')[3];
			foreach (string lsRegistro in lsCadena.Trim().Split(new char[2] { '\n', '\r' }))
			{
				loFila = loTabla.NewRow();
				if (string.IsNullOrWhiteSpace(lsRegistro))
					continue;
				lasRegistro = lsRegistro.Trim().Split(',');
				if (lasRegistro.Length != 10)
					return "El archivo no contiene el número de columnas requeridas en la fila " + Convert.ToString(liFIla);
				if (lasRegistro[0].Trim() != this.periodo && liFIla == 0)
					return "El archivo no corresponde al periodo evaluado " + this.periodo.RegresaDescripcionPeriodo();
				else if (lasRegistro[0].Trim() != this.periodo && liFIla > 0)
				{
					lsError = "El archivo ha sido alterado, esto ha sido grabado en la bitacora del proceso que será entregada a la subdirección correspondiente.";
					GrabaErrorBitacora(lasRegistro, psNombreArchivo, "Periodo " + Convert.ToString(liFIla));
					return lsError;
				}
				if (this.nivel != lasRegistro[2])
				{
					lsError = "El archivo ha sido alterado, esto ha sido grabado en la bitacora del proceso que será entregada a la subdirección correspondiente.";
					GrabaErrorBitacora(lasRegistro, psNombreArchivo, "Nivel " + Convert.ToString(liFIla));
					return lsError;
				}
				if (this.grupo != lasRegistro[3])
				{
					lsError = "El archivo ha sido alterado, esto ha sido grabado en la bitacora del proceso que será entregada a la subdirección correspondiente.";
					GrabaErrorBitacora(lasRegistro, psNombreArchivo, "Grupo " + Convert.ToString(liFIla));
					return lsError;
				}
				//liPromedio = Convert.ToInt32(Math.Round((Convert.ToDouble(lasRegistro[6]) + Convert.ToDouble(lasRegistro[7]) + Convert.ToDouble(lasRegistro[8]) + Convert.ToDouble(lasRegistro[9])) / 4, 0));
				//if (liPromedio != Convert.ToInt32(lasRegistro[4]))
				//{
				//	lsError = "El archivo ha sido alterado, esto ha sido grabado en la bitacora del proceso que será entregada a la subdirección correspondiente.";
				//	GrabaErrorBitacora(lasRegistro, psNombreArchivo, "Promedio " + Convert.ToString(liFIla));
				//	return lsError;
				//}
				loFila["periodo"] = lasRegistro[0];
				loFila["no_de_control"] = lasRegistro[1];
				loFila["nivel"] = lasRegistro[2];
				loFila["grupo"] = lasRegistro[3];
				loFila["promedio"] = lasRegistro[4];
				loFila["asistencia"] = lasRegistro[5];
				loFila["h1"] = lasRegistro[6];
				loFila["h2"] = lasRegistro[7];
				loFila["h3"] = lasRegistro[8];
				loFila["h4"] = lasRegistro[9];
				loFila["nombre_archivo"] = psNombreArchivo;
				loTabla.Rows.Add(loFila);
				liFIla = liFIla + 1;
			}
			_oSistema.Conexion.EjecutaEscalar(new StringBuilder("TRUNCATE TABLE cle_historia_precarga"));
			_oSistema.Conexion.InsertBulkCopy(loTabla, "cle_historia_precarga");
			return null;
		}
		private void GrabaErrorBitacora(string[] lasRegistro, string psNombreArchivo, string psMensaje)
		{
			StringBuilder lsCadena = new StringBuilder();
			lsCadena.Append("INSERT INTO  historia_alumno_ingles_precarga_log ");
			lsCadena.Append(" ([periodo],[no_de_control],[nivel],[grupo],[promedio],[asistencia],[speaking],[writing],[listening],[reading],[nombre_archivo],[error],[usuario]) VALUES ");
			lsCadena.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}', '{11}', '{12}')", lasRegistro[0], lasRegistro[1], lasRegistro[2], lasRegistro[3], lasRegistro[4], lasRegistro[5], lasRegistro[6], lasRegistro[7], lasRegistro[8], lasRegistro[9], psNombreArchivo, psMensaje, _oSistema.Sesion.Usuario.Usuario);
			_oSistema.Conexion.EjecutaEscalar(lsCadena);
		}



		#endregion

	}
}
