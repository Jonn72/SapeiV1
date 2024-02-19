using Sapei.Framework;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using Sapei.Framework.Utilerias.ExpedienteDigital;
using Sapei.Framework.Utilerias.Funciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Sapei.Procesos
{
	public partial class Procesos
	{

		#region Carga Academica
		public void ProcesaCargasAcademicas(int piFolio, string psPeriodo)
		{
			DataTable loDt;
			DataTable loDatosArchivo;
			string lsRuta;
			byte[] lbyReporte;
			loDt = new DataTable();
			bool lbIncluyeFecha;
			loDatosArchivo = new DataTable();
			string lsContraseña;
			string lsServidor;
			string lsUsuario;
			int liCargasGeneradas;
			ManejoArchivos loArchivos = new ManejoArchivos(_oSistemaSapei);
			ReportesGenerales loReporte;
			loReporte = new ReportesGenerales(_oSistemaSapei);
			this.LogMessage(string.Format("Inicia proceso de expediente digital, carga acádemica", _enProceso.ToString()), EventLogEntryType.Information);

			try
			{

				lsRuta = AppDomain.CurrentDomain.RelativeSearchPath;
				if (string.IsNullOrEmpty(lsRuta))
					lsRuta = AppDomain.CurrentDomain.BaseDirectory;
				lsRuta = Path.Combine(lsRuta, Path.Combine("Reportes/RDLC/DocumentosOficiales", "CargaAcademicaFiEL.rdlc"));
				loReporte.RutaReportes = lsRuta;

				loDatosArchivo = loArchivos.RegresaDatosArchivo(enmTiposDocumentos.CargaAcademica);
				lsContraseña = FuncionesCifrado.DecryptRJ256(loDatosArchivo.Rows[0].Field<string>("contraseña").Trim());
				lsServidor = @loDatosArchivo.Rows[0].Field<string>("servidor");
				lsUsuario = loDatosArchivo.Rows[0].Field<string>("usuario");
				if (loDatosArchivo.Rows.Count == 0)
					return;

				loDt = RegresaTablaCargaAcademica(piFolio);
				if (loDt.Rows.Count == 0)
					return;

				lbIncluyeFecha = loDatosArchivo.Rows[0].Field<bool>("incluye_fecha");
				NetworkCredential loCredencial = new NetworkCredential(lsUsuario, lsContraseña);
				liCargasGeneradas = 0;

				using (var connection = new ConnectToSharedFolder(lsServidor, loCredencial))
				{
					foreach (DataRow loFila in loDt.Rows)
					{
						lbyReporte = loReporte.RegresaCargaAcademica(psPeriodo, loFila.Field<string>("no_de_control"), true);
						lsRuta = $"{lsServidor}\\{ loFila.Field<string>("ruta")}\\{loDatosArchivo.Rows[0].Field<string>("ruta")}";
						loArchivos.CrearArchivo(Sapei.Framework.Configuracion.enmSistema.PROCESO, lsRuta, lbyReporte, enmTiposDocumentos.CargaAcademica, enmTipoArchivo.pdf, lbIncluyeFecha);
						lbyReporte = null;
						GuardaRegistro(psPeriodo ,loFila.Field<string>("no_de_control"));
						liCargasGeneradas+=1;
					}
				}
				this._oProcesos._oProceso.Terminar();
				this.LogMessage(string.Format("Finaliza proceso de expediente digital, " + liCargasGeneradas +" cargas academicas generadas", _enProceso.ToString()), EventLogEntryType.Information);

			}
			catch (Exception ex)
			{
				ManejoCorreos.EnviaNotificacionExpedianteDigital(ex.Message + " Modulo: ExpedienteDigital/ProcesaCargasAcademicas", Sapei.Framework.Configuracion.enmSistema.PROCESO, _oSistemaSapei);
			}
			finally
			{
				if (loDt != null)
					loDt.Dispose();
				if (loReporte != null)
					loReporte.Dispose();
			}
		}

		private DataTable RegresaTablaCargaAcademica(int piFolio)
		{

			DataTable loDatos = new DataTable();
			using (var loConexion = new ManejaConexion(_oSistemaSapei.Conexion))
			{
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@folio", piFolio));
				loDatos.Load(_oSistemaSapei.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_procesoMQ_carga_academica_datos", loParametros));
			}
			return loDatos;
		}

		private void GuardaRegistro(string psPeriodo,string psControl)
		{
			StringBuilder lsCadena = new StringBuilder();
			lsCadena.Append("insert into exp_dig_carga_academica");
			lsCadena.Append(" (periodo, no_de_control, fecha_registro)");
			lsCadena.AppendFormat("values ('{0}','{1}',GETDATE())", psPeriodo, psControl);
			_oSistemaSapei.Conexion.EjecutaEscalar(lsCadena);
		}
		#endregion

	}
}
