using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSapei.App_Start;
using Sapei.Framework.Utilerias;
using Sapei.Framework.BaseDatos;
using Sapei;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Text;

namespace appSapei.Controllers
{
	public class FinancierosController : Controller
	{
		#region Manejo de Referencias


		#endregion
		#region Manejo de Servicios
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult Servicios()
		{
			try
			{
				Servicios loServicios = new Servicios(SesionSapei.Sistema);
				ViewData["Tabla"] = loServicios.CargarLista();
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public ActionResult RegistrarServicio([Bind(Include = "clave,concepto,monto,dias_vigencia,activo")] Servicio poServicio)
		{
			try
			{

				if (ModelState.IsValid)
				{
					Servicio loServicio = new Servicio(SesionSapei.Sistema);
					loServicio.Cargar(poServicio.clave);
					if (loServicio.EOF)
						loServicio.clave = poServicio.clave;
					loServicio.concepto = poServicio.concepto;
					loServicio.monto = poServicio.monto;
					loServicio.dias_vigencia = poServicio.dias_vigencia;
					loServicio.activo = poServicio.activo;
					loServicio.Guardar();
					return ManejoMensajesJson.RegresaMensajeAlertOK();
				}
			}
			catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 2627)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlert("Ya existe un servicio registrado con la clave:" + poServicio.clave, 'E', true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
			// Info  
			return ManejoMensajesJson.RegresaMensajeAlertErrorDatos();
		}

		#endregion
		#region Registro de PAgos
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult ControlServiciosAlumnos()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult PagoServiciosAlumnos(string psNoControl = null)
		{
			try
			{
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));

				if (string.IsNullOrEmpty(psNoControl))
				{
					loParametros.Add(new ParametrosSQL("@no_de_control", DBNull.Value));
					loParametros.Add(new ParametrosSQL("@tipo", "EVO"));

					using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
					{
						loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rf_alu_servicios", loParametros));
					}
					ViewData["Tabla"] = loDt;
					ViewData["Titulo"] = "Busqueda de historial de estudiantes";
					ViewData["Modulo"] = "HistorialPagos";
					ViewData["Vista"] = "Financieros/ControlServiciosAlumnos";
					ViewData["periodo_desc"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
					return PartialView("BuscaNoControl");
				}
				loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
				loParametros.Add(new ParametrosSQL("@tipo", ""));

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rf_alu_servicios", loParametros));
					ViewData["DatosEstudiante"] = loDt;
				}
				return PartialView("PagoServiciosAlumnos");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Bienvenida");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult RegistraCSVServicios()
		{
			try
			{
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rf_pagos_servicios", loParametros));
				}
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Registro de pagos de servicios";
				ViewData["Modulo"] = "RegistraCSVServicios";
				ViewData["Vista"] = "../../Financieros/ControlServiciosAlumnos";
				ViewData["periodo_desc"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
				return PartialView();

			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Bienvenida");
			}
		}

		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public ActionResult GuardaPagosServicios()
		{
			string lsTextoArchivo;
			string lsMensaje;
			System.IO.Stream loSt;
			HttpPostedFileBase loFile;
			RegistroPagos loDatos;
			try
			{
				loFile = Request.Files[0];
				loSt = loFile.InputStream;
				loSt.Position = 0;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loDatos = new RegistroPagos(SesionSapei.Sistema);

				using (System.IO.StreamReader reader = new System.IO.StreamReader(loSt, System.Text.Encoding.UTF8))
				{
					lsTextoArchivo = reader.ReadToEnd();
				}

				if (string.IsNullOrEmpty(lsTextoArchivo))
				{
					return Json("");
				}
				lsMensaje = loDatos.GuardaRegistros(lsTextoArchivo, loFile.FileName);
				if (!string.IsNullOrEmpty(lsMensaje))
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);

				loParametros.Add(new ParametrosSQL("@periodo", loDatos.periodo));
				loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_rf_procesa_pagos_servicios", loParametros);
					SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_rf_procesa_pagos_reinscripcion", loParametros);
		
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Proceso concluido correctamente", false);
				}

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult NuevoIngreso()
		{
			try
			{
				ViewData["vdTitulo"] = "Autorización de Nuevo Ingreso";
				ViewData["vdTipoAuto"] = "Inscripcion";
				return PartialView("AutorizaPago");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult AutorizaVerano()
		{
			try
			{
				ViewData["vdTitulo"] = "Autorización de Verano";
				ViewData["vdTipoAuto"] = "Verano";
				return PartialView("AutorizaPago");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public JsonResult GuardaPagoVerano_Inscripcion(string psNoControl, string psPeriodo, int piMonto, string psRecibo)
		{
			try
			{
				List<ParametrosSQL> loParametros;
				string lsMensaje;
				string lsTitulo;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
					loParametros.Add(new ParametrosSQL("@recibo_pago", psRecibo));
					loParametros.Add(new ParametrosSQL("@monto_pago", piMonto));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_autoriza_verano", loParametros);
					lsMensaje = "";
					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
					loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_avisos_reins_log", loParametros);
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);

				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult Condonacion(string psControl)
		{
			try
			{
				Alumnos_Historial_Pagos loPagos;
				ViewData["DatosPago"] = null;
				if (!string.IsNullOrEmpty(psControl))
				{
					loPagos = new Alumnos_Historial_Pagos(SesionSapei.Sistema);
					ViewData["DatosPago"] = loPagos.RegresaDatosPagoActual(psControl);
					ViewData["NoControl"] = psControl;
				}
				return PartialView("Condonacion");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public JsonResult GuardaPagoCondonacion(string psNoControl, string psMonto)
		{
			try
			{
				List<ParametrosSQL> loParametros;
				Alumno loAlumno;
				string lsMensaje;
				loAlumno = new Alumno(SesionSapei.Sistema);
				if (string.IsNullOrEmpty(psMonto))
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No hay monto que registrar, verifique", true);
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActual));
					loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
					loParametros.Add(new ParametrosSQL("@cadena_monto", psMonto));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rf_condonacion", loParametros);
				}
				// Se arma cadena para notificacion en gnosis
				lsMensaje = "Se le ha autorizado la condonación de pago";
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);


			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		#endregion
		#region Registro Vehicular
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult AutorizaRegistroVehicular()
		{
			try
			{
				//SisCombo loCombos;
				//loCombos = new SisCombo(SesionSapei.Sistema);
				//ViewData["cboTipoVehiculo"] = loCombos.CargarValoresPorCombo("cboTipoVehiculo");
				return PartialView("AutorizaRegistroVehicular");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public JsonResult CargaDatosRegistroVehicular(string psQR, string psControl, string psTipo)
		{
			try
			{
				string lsNoControl;
				string lsTipo;
				string[] lasCadena;
				if (!string.IsNullOrEmpty(psQR))
				{
					lasCadena = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.DecryptRJ256(psControl).Split('|');
					lsNoControl = lasCadena[1];
					lsTipo = lasCadena[2];
				}
				else
				{
					lsNoControl = psControl;
					lsTipo = psTipo;
				}
				Control_Vehicular_Registro loControl;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					loControl = new Control_Vehicular_Registro(SesionSapei.Sistema);
					return ManejoMensajesJson.RegresaJsonTabla(loControl.RegresaDatosEstudianteSolicitanteVehicular(lsNoControl, lsTipo), "El no. de control es incorrecto o no a sido autorizado el pago por Recursos Materiales");
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public JsonResult GuardaPagoVehicular(string psControl, string psTipoVehiculo, int piMonto)
		{
			try
			{
				Control_Vehicular_Pago loPago;
				Control_Vehicular_Registro loControl;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loPago = new Control_Vehicular_Pago(SesionSapei.Sistema);
					loControl = new Control_Vehicular_Registro(SesionSapei.Sistema);
					loPago.Cargar(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual, psControl, psTipoVehiculo);
					if (loPago.EOF)
					{
						loPago.periodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
						loPago.usuario = psControl;
						loPago.tipo_vehiculo = psTipoVehiculo;
					}
					loPago.monto = piMonto;
					loPago.fecha_registro = DateTime.Now;
					loPago.Guardar();
					loControl.Cargar(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual, psControl, psTipoVehiculo);
					if (Convert.ToInt32(loControl.estado_registro) <= 3)
					{
						loControl.estado_registro = "3";
						loControl.Guardar();
					}
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se le ha registrado su pago del registro vehicular con monto de: $" + piMonto.ToString(), true);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		#endregion
		#region Reinscripcion
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult ActivarPeriodoReinscripcion(string id)
		{
			try
			{
				DataTable loTabla;
				RF_activa_periodo loPeriodos;
				string lsPeriodo = id;
				if (string.IsNullOrEmpty(lsPeriodo))
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;

				loPeriodos = new RF_activa_periodo(SesionSapei.Sistema);

				loTabla = loPeriodos.RegresaTablaPeriodos();
				ViewData["txtPeriodo"] = lsPeriodo;
				ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo("/");
				ViewData["Tabla"] = loTabla;
				ViewData["Titulo"] = "Periodos Registrados";
				ViewData["Encabezados"] = new List<string> { "Periodo", "Inicio Impresión Ficha", "Fin Impresión Ficha", "Fecha Límite Pago", "Fecha límite de registro de pagos" };
				if (loTabla.Rows.Count == 0)
				{
					return PartialView();
				}
				if (loTabla.Rows[0].RegresaValor<string>("periodo").ToUpper() == lsPeriodo.RegresaDescripcionPeriodo("/").ToUpper())
				{
					ViewData["txtIniImprime"] = loTabla.Rows[0].RegresaValor<string>("ini_imprime_ficha");
					ViewData["txtFinImprime"] = loTabla.Rows[0].RegresaValor<string>("fin_imprime_ficha");
					ViewData["txtFinPago"] = loTabla.Rows[0].RegresaValor<string>("fin_pago");
					ViewData["txtFinPagoOrdinario"] = loTabla.Rows[0].RegresaValor<string>("fin_registro_pago_ordinario");
				}
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public JsonResult GuardaPeriodoReinscripcion(string psPeriodo, DateTime psIniImprime, DateTime psFinImprime, DateTime psFinPago, DateTime psFinRegistro)
		{
			try
			{
				RF_activa_periodo loControl;
				loControl = new RF_activa_periodo(SesionSapei.Sistema);
				string lsPeriodo = psPeriodo;
				loControl.Cargar(lsPeriodo);
				if (loControl.EOF)
				{
					loControl.periodo = lsPeriodo;
				}
				loControl.ini_imprime_ficha = psIniImprime;
				loControl.fin_imprime_ficha = psFinImprime;
				loControl.fin_pago = psFinPago;
				loControl.fin_registro_pago_ordinario = psFinRegistro;
				loControl.usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loControl.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		//[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		//public PartialViewResult AutorizaReinscripcion()
		//{
		//	try
		//	{
		//		RF_historial_pagos loDatos = new RF_historial_pagos(SesionSapei.Sistema);
		//		ViewData["Tabla"] = loDatos.RegresaRegistroPagosReinscripcion(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual);
		//		ViewData["Titulo"] = "Periodos Registrados";
		//		ViewData["Encabezados"] = new List<string> { "No. Control", "Importe", "Referencia", "Observaciones", "Fecha de registro", "Autorizado" };

		//		return PartialView();
		//	}
		//	catch (Exception ex)
		//	{
		//		SesionSapei.Sistema.GrabaLog(ex);
		//		return PartialView("../Home/Index");
		//	}
		//}
		[HttpPost]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public ActionResult GuardaPagoReinscripcion()
		{

			string lsTextoArchivo;
			string lsMensaje;
			System.IO.Stream loSt;
			HttpPostedFileBase loFile;
			Avisos_Reinscripcion_Pagos loDatos;
			//Se obtiene la fecha del ultimo dia cargado a base
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loFile = Request.Files[0];
					loSt = loFile.InputStream;
					loSt.Position = 0;
					System.Data.SqlClient.SqlDataReader loReader;
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loDatos = new Avisos_Reinscripcion_Pagos(SesionSapei.Sistema);
					using (System.IO.StreamReader reader = new System.IO.StreamReader(loSt, System.Text.Encoding.UTF8))
					{
						lsTextoArchivo = reader.ReadToEnd();
					}

					if (string.IsNullOrEmpty(lsTextoArchivo))
					{
						return Json("");
					}
					lsMensaje = loDatos.GuardaRegistros(lsTextoArchivo, loFile.FileName);
					if (!string.IsNullOrEmpty(lsMensaje))
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);

					loParametros.Add(new ParametrosSQL("@periodo", loDatos.periodo));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_rf_procesa_pagos_reinscripcion", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
					if (lsMensaje.Trim().Length == 1)
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult AutorizaReinscripcionAlumno(string psControl = null)
		{
			try
			{
				string lsPeriodo;
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				ViewData["periodo"] = lsPeriodo;
				ViewData["periodo_desc"] = lsPeriodo.RegresaDescripcionPeriodo();
				if (!string.IsNullOrEmpty(psControl))
				{
					Alumnos_Historial_Pagos loDatos = new Alumnos_Historial_Pagos(SesionSapei.Sistema);
					ViewData["DatosEstudiante"] = loDatos.RegresaDatosPagoActual(lsPeriodo, psControl);
				}
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public JsonResult AutorizaReinscripcionAlumnoJson(string psControl, bool pbTipo)
		{
			try
			{
				Alumnos_Historial_Pagos loDatos = new Alumnos_Historial_Pagos(SesionSapei.Sistema);
				loDatos.ActivaBloqueaPago(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual, psControl, pbTipo);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public JsonResult CargaDatosPagoServicio(string psNoControl, string psPeriodo, string psLineaCaptura)
		{
			try
			{
				Alumnos_Historial_Servicios loAlumno;
				loAlumno = new Alumnos_Historial_Servicios(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loAlumno.CargarDatosPagoServicios(psPeriodo, psNoControl, psLineaCaptura), "El no. de control es incorrecto");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public JsonResult GuardaPagoServicio(string psNoControl, string psPeriodo, string psLineaCaptura)
		{
			try
			{
				List<ParametrosSQL> loParametros;
				string lsMensaje;
				string lsTitulo;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
					loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
					loParametros.Add(new ParametrosSQL("@linea_captura", psLineaCaptura));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rf_autoriza_servicio", loParametros);
					lsMensaje = "";
					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();

					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se autoriza pago de servicio", true);

				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult Prorrogas(string psControl)
		{
			try
			{
				Alumnos_Historial_Pagos loPagos;
				ViewData["DatosPago"] = null;
				if (!string.IsNullOrEmpty(psControl))
				{
					loPagos = new Alumnos_Historial_Pagos(SesionSapei.Sistema);
					ViewData["DatosPago"] = loPagos.RegresaDatosPagoProrroga(psControl);
					ViewData["NoControl"] = psControl;
				}
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public JsonResult GuardaPagoProrrogas(string psNoControl)
		{
			try
			{
				List<ParametrosSQL> loParametros;
				string lsMensaje;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					System.Data.SqlClient.SqlDataReader loReader;
					loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
					loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_rf_prorroga", loParametros);
					lsMensaje = "";
					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();

					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se autoriza pago de servicio", true);

				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		#endregion
		#region Contratos
		/*
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult Tabulador()
		{
			try
			{
				DataTable loDatos;
				Personal loPersonal = new Personal(SesionSapei.Sistema);
				loDatos = loPersonal.CargarListaTabulador();
				ViewData["Titulo"] = "Tabulador";
				ViewData["Encabezados"] = new List<string> { "Clave", "(07)", "(ET)", "(39)", "(CA)", "(34)" };
				ViewData["Tabla"] = loDatos;
				return PartialView("Tabulador");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}


		public ActionResult CargaTabulador()
		{
			HttpPostedFileBase loFile;
			Stream loSt;
			string[] encabezado;
			string lsMensaje;
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			DataTable loDt = new DataTable();
			System.Data.SqlClient.SqlDataReader loReader;
			string obj;

			try
			{
				loFile = Request.Files[0];
				loSt = loFile.InputStream;
				using (ExcelPackage package = new ExcelPackage(loSt))
				{
					StringBuilder sb = new StringBuilder();

					ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
					int rowCount = worksheet.Dimension.Rows;
					int ColCount = worksheet.Dimension.Columns;
					bool bHeaderRow = true;

					encabezado = new String[ColCount+1];

					for (int col = 1; col <= ColCount; col++)
					{
						if (bHeaderRow)
						{
							encabezado[col] = worksheet.Cells[1, col].Value.ToString();
						}
						else 
						{
							encabezado[col] = worksheet.Cells[1, col].Value.ToString();

						}
					}

					sb.Append("[");

						for (int row = 2; row <= rowCount; row++)
					{
						sb.Append("{");
						for (int col = 1; col <= ColCount; col++)
						{
							if (col == ColCount)
							{
								sb.Append("\"" + encabezado[col].Trim()+"\": \"" + worksheet.Cells[row, col].Value.ToString() + "\"");
							}
							else
							{
								sb.Append("\"" + encabezado[col].Trim()+ "\": \"" + worksheet.Cells[row, col].Value.ToString() + "\", ");
							}
						}
						if (row != rowCount)
							sb.Append("}, ");
						else
						sb.Append("}");
						
					}
					sb.Append("]");
					obj = sb.ToString();

					using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
					{
						PersonalHorarioIndice loPersonalHorarioIndice;
						loPersonalHorarioIndice = new PersonalHorarioIndice(SesionSapei.Sistema);
						loParametros.Add(new ParametrosSQL("@objeto", obj));
						loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_tabulador", loParametros);
						lsMensaje = "";
						if (loReader.HasRows)
						{
							while (loReader.Read())
							{
								lsMensaje = loReader.GetString(0);
							}
						}
						loReader.Close();
						loReader.Dispose();

						if (lsMensaje.Trim() != "1")
						{
							return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
						}
					}


					return Json("XD|Tabulador actualizado|../../../Financieros/Tabulador");
				}

			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}*/

		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public PartialViewResult MontosPagos()
		{
			DataTable loDatos;
			Personal loPersonal = new Personal(SesionSapei.Sistema);
			loDatos = loPersonal.CargarListaMontosPagos();
			ViewData["Titulo"] = "Pagos";
			ViewData["Encabezados"] = new List<string> { "Descripción", "Tipo de Pago", "Monto por Hora", "Accion" };
			ViewData["Tabla"] = loDatos;
			return PartialView("MontosPagos");
		}

		//GuardaMontoPago

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
		public JsonResult GuardaMontoPago(string psId, string psTipoMonto, string psConcepto, double pdMonto = 0)
		{
			try
			{
				rf_tabulador loRFTabulador;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loRFTabulador = new rf_tabulador(SesionSapei.Sistema);
					if (string.IsNullOrEmpty(psId))
					{
						loRFTabulador.Cargar(Convert.ToInt32(null));
					}
					else
					{
						loRFTabulador.Cargar(Convert.ToInt32(psId));
					}
					loRFTabulador.tipo_monto = psTipoMonto;
					loRFTabulador.concepto = psConcepto;
					loRFTabulador.monto = pdMonto;// Convert.ToDouble(psMonto);
					loRFTabulador.Guardar();
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}

		public JsonResult EliminarMonto(int psID)
		{

			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					rf_tabulador lorf;
					lorf = new rf_tabulador(SesionSapei.Sistema);
					lorf.Cargar(psID);
					lorf.Eliminar();
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
				}
			}

			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		#endregion

	}
}
