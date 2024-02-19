using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
	public class AspiranteController : Controller
	{
		[SessionExpire]
		public ActionResult Index()
		{
			try
			{
				return View();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return RedirectToAction("Index", "Home");
			}
		}

		[AllowAnonymous]
		[HttpGet]
		public ActionResult Registro()
		{
			try
			{
				ViewData["mensaje"] = "Futuro Guardian";

				if (SesionSapei.Sistema == null)
				{
					return RedirectToAction("Index", "Home");
				}
				if (!SesionSapei.Sistema.Sesion.Periodo.ActivaRegistroAspirantes)
				{
					return RedirectToAction("Index", "Home");
				}
				return View();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return RedirectToAction("Index", "Home");
			}
		}
		public JsonResult RegistraNuevo(string psNombre, string psPaterno, string psMaterno, string psCorreo, string psContrasena, string psCurp)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					System.Data.SqlClient.SqlDataReader loReader;
					Aspirantes_Periodos loPeriodo = new Aspirantes_Periodos(SesionSapei.Sistema);
					string lsMensaje = "";
					loParametros.Add(new ParametrosSQL("@curp", psCurp));
					loParametros.Add(new ParametrosSQL("@nombre", psNombre.QuitaAcento()));
					loParametros.Add(new ParametrosSQL("@paterno", psPaterno.QuitaAcento()));
					loParametros.Add(new ParametrosSQL("@materno", psMaterno.QuitaAcento()));
					loParametros.Add(new ParametrosSQL("@correo", psCorreo));
					loParametros.Add(new ParametrosSQL("@contrasena", psContrasena));

					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_registra_aspirante", loParametros);

					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();

					if (lsMensaje.ToUpper().Contains("CURP"))
					{
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Este CURP ya fue registrado antes", false);
					}
					if (lsMensaje.Trim().Length == 8)
					{
						Sapei.Framework.Utilerias.Funciones.ManejoCorreos.EnviaRegistroAspirante(psNombre, psPaterno, psMaterno, psCorreo, psContrasena, lsMensaje);
						lsMensaje = "Estimado aspirante, en su correo debe recibir un mensaje con sus datos de registro. Su folio es: " + lsMensaje + ". con él y su contraseña registrada ahora puede realizar su registro de datos, para ello debe iniciar sesión.";
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
					}

					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
			}


			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		//[SessionExpire]
		//public PartialViewResult DatosPersonales()
		//{
		//	try
		//	{
		//		return CargaDatos("DatosPersonales");
		//	}
		//	catch (Exception ex)
		//	{
		//		SesionSapei.Sistema.GrabaLog(ex);
		//		return PartialView("Index");
		//	}
		//}

		//[SessionExpire]
		//public JsonResult ModificarDatosPersonales(string psNombre, string psPaterno, string psMaterno, string psCurp, string psSexo, string psEntidad, string psEstadoCivil, string psCorreo, string psTelefono, string psCelular, string psFecha, string psCalle, string psNumero, string psId_cp, string psNSS)
		//{
		//	try
		//	{
		//		Aspirante_domicilio loDomicilio;
		//		Aspirante_datos loDatosAspirante;
		//		int liIdCP;
		//		string lsFolio;
		//		using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
		//		{
		//			lsFolio = SesionSapei.Sistema.Sesion.Usuario.Usuario;
		//			loDatosAspirante = new Aspirante_datos(SesionSapei.Sistema);
		//			loDatosAspirante.Cargar(lsFolio);
		//			loDatosAspirante.nombre = psNombre;
		//			loDatosAspirante.paterno = psPaterno;
		//			loDatosAspirante.materno = psMaterno;
		//			loDatosAspirante.curp = psCurp;
		//			loDatosAspirante.sexo = psSexo;
		//			loDatosAspirante.entidad_nacimiento = Convert.ToInt32(psEntidad);
		//			loDatosAspirante.estado_civil = psEstadoCivil;
		//			loDatosAspirante.fechaNacimiento = Convert.ToDateTime(psFecha);
		//			loDatosAspirante.correo = psCorreo;
		//			loDatosAspirante.telefonoCasa = psTelefono;
		//			loDatosAspirante.telefonoEmergencia = psTelefono;
		//			loDatosAspirante.celular = psCelular;
		//			loDatosAspirante.nss = psNSS;
		//			loDatosAspirante.Guardar();


		//			liIdCP = Convert.ToInt32(psId_cp);
		//			loDomicilio = new Aspirante_domicilio(SesionSapei.Sistema);
		//			loDomicilio.Cargar(lsFolio);
		//			loDomicilio.folio = lsFolio;
		//			loDomicilio.id_cp = Convert.ToInt32(psId_cp);
		//			loDomicilio.calle = psCalle;
		//			loDomicilio.numero = psNumero;
		//			loDomicilio.Guardar();
		//			return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
		//		return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
		//	}

		//}
		//[SessionExpire]
		///// <summary>
		///// 
		///// </summary>
		///// <returns></returns>
		//public PartialViewResult DatosEscuela()
		//{
		//	try
		//	{
		//		Aspirante_escuela_procedencia loDatosAspirante;
		//		loDatosAspirante = new Aspirante_escuela_procedencia(SesionSapei.Sistema);
		//		loDatosAspirante.Cargar(SesionSapei.Sistema.Sesion.Usuario.Usuario);
		//		if (loDatosAspirante.EOF)
		//		{
		//			return PartialView("DatosEscuela");
		//		}
		//		ViewData["idEscuela"] = loDatosAspirante.id_escuela;
		//		ViewData["egreso"] = loDatosAspirante.anio_egreso;
		//		ViewData["promedio"] = loDatosAspirante.promedio;
		//		return PartialView("DatosEscuela");
		//	}
		//	catch (Exception ex)
		//	{
		//		SesionSapei.Sistema.GrabaLog(ex);
		//		return PartialView("Index");
		//	}
		//}
		//[SessionExpire]
		//public JsonResult ModificarDatosEscuela(string psIdEscuela, string psEgreso, string psPromedio)
		//{
		//	try
		//	{
		//		Aspirante_escuela_procedencia loDatosAspirante;
		//		string lsFolio;
		//		using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
		//		{
		//			lsFolio = SesionSapei.Sistema.Sesion.Usuario.Usuario;
		//			loDatosAspirante = new Aspirante_escuela_procedencia(SesionSapei.Sistema);
		//			loDatosAspirante.Cargar(lsFolio);
		//			if (loDatosAspirante.EOF)
		//			{
		//				loDatosAspirante.Nuevo();
		//				loDatosAspirante.folio = lsFolio;
		//			}
		//			loDatosAspirante.anio_egreso = Convert.ToInt16(psEgreso);
		//			loDatosAspirante.promedio = Convert.ToDouble(psPromedio);
		//			loDatosAspirante.id_escuela = Convert.ToInt32(psIdEscuela);
		//			loDatosAspirante.Guardar();
		//			return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
		//		return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
		//	}

		//}
		//[SessionExpire]
		//public PartialViewResult DatosSolicitud()
		//{
		//	try
		//	{
		//		Aspirante_datos_solicitud loDatosAspirante;
		//		loDatosAspirante = new Aspirante_datos_solicitud(SesionSapei.Sistema);
		//		loDatosAspirante.Cargar(SesionSapei.Sistema.Sesion.Usuario.Usuario);
		//		if (loDatosAspirante.EOF)
		//		{
		//			return PartialView("DatosSolicitud");
		//		}
		//		ViewData["cboComoEnteraste"] = loDatosAspirante.enteraste;
		//		ViewData["cboCarrerasOfertadas"] = loDatosAspirante.carrera1;
		//		ViewData["cboCarrerasOfertadas2"] = loDatosAspirante.carrera2;
		//		return PartialView("DatosSolicitud");
		//	}
		//	catch (Exception ex)
		//	{
		//		SesionSapei.Sistema.GrabaLog(ex);
		//		return PartialView("Index");
		//	}
		//}
		//[SessionExpire]
		//public JsonResult ModificarDatosSolicitud(string psEnteraste, string psCarrera1, string psCarrera2)
		//{
		//	try
		//	{
		//		Aspirante_datos_solicitud loDatosAspirante;
		//		string lsFolio;
		//		using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
		//		{
		//			lsFolio = SesionSapei.Sistema.Sesion.Usuario.Usuario;
		//			loDatosAspirante = new Aspirante_datos_solicitud(SesionSapei.Sistema);
		//			loDatosAspirante.Cargar(lsFolio);
		//			if (loDatosAspirante.EOF)
		//			{
		//				loDatosAspirante.Nuevo();
		//				loDatosAspirante.folio = lsFolio;
		//			}
		//			loDatosAspirante.enteraste = psEnteraste;
		//			loDatosAspirante.carrera1 = psCarrera1;
		//			loDatosAspirante.carrera2 = psCarrera2;
		//			loDatosAspirante.Guardar();
		//			return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
		//		return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
		//	}

		//}
		//[SessionExpire]
		//public PartialViewResult DatosEstatus()
		//{
		//	try
		//	{
		//		return PartialView("DatosEstatus");
		//	}
		//	catch (Exception ex)
		//	{
		//		SesionSapei.Sistema.GrabaLog(ex);
		//		return PartialView("Index");
		//	}
		//}
		[SessionExpire]
		public PartialViewResult Ficha()
		{
			try
			{
				return CargaDatos("Ficha", true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire]
		public JsonResult RegresaAspiranteDatos(string psFolio)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					DataTable loDatos;
					Aspirante loDatosAspirante = new Aspirante(SesionSapei.Sistema);
					loDatos = loDatosAspirante.CargarVistaDatosCompletos(psFolio);
					if (loDatos.Rows.Count > 0)
						return ManejoMensajesJson.RegresaJsonTabla(loDatos);
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[SessionExpire]
		public JsonResult ModificarEstatusAspirante(string psFolio, string psIdEstatus)
		{
			try
			{				
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@periodo", SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano));
				loParametros.Add(new ParametrosSQL("@folio", psFolio));
				loParametros.Add(new ParametrosSQL("@estaus", psIdEstatus));
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_aspirantes_actualiza_estatus",loParametros));
				}
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(loDt.Rows[0].ItemArray[0].ToString(),true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		private PartialViewResult CargaDatos(string psVista, bool pbCambiaEstatus = false)
		{
			try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					DataTable loDatos;
					string lsTelefono;
					Aspirante loDatosAspirante = new Aspirante(SesionSapei.Sistema);
					loDatos = loDatosAspirante.CargarVistaDatosCompletos(SesionSapei.Sistema.Sesion.Usuario.Usuario);
					if (loDatos.Rows.Count == 0)
						return PartialView("DatosPersonales");
					ViewData["txtNombre"] = loDatos.Rows[0].RegresaValor<string>("nombre");
					ViewData["txtPaterno"] = loDatos.Rows[0].RegresaValor<string>("paterno");
					ViewData["txtMaterno"] = loDatos.Rows[0].RegresaValor<string>("materno");
					ViewData["txtFechaNacimiento"] = loDatos.Rows[0].RegresaValor<DateTime>("fechaNacimiento").ToString("yyyy/MM/dd");
					ViewData["cboEstados"] = loDatos.Rows[0].RegresaValor<int>("entidad_nacimiento");
					ViewData["rbtSexo"] = loDatos.Rows[0].RegresaValor<char>("sexo");
					ViewData["txtCURP"] = loDatos.Rows[0].RegresaValor<string>("curp");
					ViewData["cboEstadoCivil"] = loDatos.Rows[0].RegresaValor<string>("estado_civil");
					ViewData["txtNSS"] = loDatos.Rows[0].RegresaValor<string>("nss");
					ViewData["txtCorreo"] = loDatos.Rows[0].RegresaValor<string>("correo");
					lsTelefono = loDatos.Rows[0].RegresaValor<string>("telefonoCasa");
					ViewData["txtTelefono"] = (string.IsNullOrEmpty(lsTelefono)) ? "" : lsTelefono.Trim();
					lsTelefono = loDatos.Rows[0].RegresaValor<string>("celular");
					ViewData["txtCelular"] = (string.IsNullOrEmpty(lsTelefono)) ? "" : lsTelefono.Trim();
					ViewData["txtCalle"] = loDatos.Rows[0].RegresaValor<string>("calle");
					ViewData["txtNoDomicilio"] = loDatos.Rows[0].RegresaValor<string>("numero");
					ViewData["cboColonia"] = loDatos.Rows[0].RegresaValor<int>("id_cp");
					ViewData["txtColonia"] = loDatos.Rows[0].RegresaValor<string>("colonia");
					ViewData["txtCodPostal"] = loDatos.Rows[0].RegresaValor<string>("cp");
					ViewData["txtCarrera1"] = loDatos.Rows[0].RegresaValor<string>("carrera1");
					ViewData["txtCarrera2"] = loDatos.Rows[0].RegresaValor<string>("carrera2");
					ViewData["HidEstatus"] = loDatos.Rows[0].RegresaValor<string>("estatusAspirante");
					if (pbCambiaEstatus)
					{
						loDatosAspirante.Cargar(SesionSapei.Sistema.Sesion.Usuario.Usuario);
						loDatosAspirante.estatusAspirante = "1";
						loDatosAspirante.Guardar();
					}
					return PartialView(psVista);
				}
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ASP)]
		public PartialViewResult RegistroDatos()
		{
			try
			{
				Aspirante loAspirante = new Aspirante(SesionSapei.Sistema);
				ViewData["Datos"] = loAspirante.RegresaDatosCompletos(SesionSapei.Sistema.Sesion.Usuario.Usuario);
				ViewData["EscuelasProcedencia"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("escuelas_procedencia", "id", "nombre", null, "nombre",false,0,null,true);
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ASP)]
		public JsonResult DatosPersonalesJson(string psEstadoCivil, string psTelefono, string psTelefonoEmergencia, string psCelular, string psNSS)
		{
			try
			{
				if(string.IsNullOrEmpty(psTelefono) || string.IsNullOrEmpty(psTelefonoEmergencia) 
							|| string.IsNullOrEmpty(psCelular) || string.IsNullOrEmpty(psNSS))
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Capture todos los campos", false);
				}
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@folio", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				loParametros.Add(new ParametrosSQL("@valor1", psEstadoCivil));
				loParametros.Add(new ParametrosSQL("@valor2", psTelefono));
				loParametros.Add(new ParametrosSQL("@valor3", psTelefonoEmergencia));
				loParametros.Add(new ParametrosSQL("@valor4", psCelular));
				loParametros.Add(new ParametrosSQL("@valor5", psNSS));
				GuardaDatosAspirante(loParametros);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);				
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ASP)]
		public JsonResult DatosDomicilioJson(string psCalle, string psNumero, string psIdCP)
		{
			try
			{
				if (string.IsNullOrEmpty(psCalle) || string.IsNullOrEmpty(psNumero))
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Capture todos los campos", false);
				}
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@folio", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				loParametros.Add(new ParametrosSQL("@valor1", psCalle));
				loParametros.Add(new ParametrosSQL("@valor2", psNumero));
				loParametros.Add(new ParametrosSQL("@valor3", psIdCP));
				GuardaDatosAspirante(loParametros);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ASP)]
		public JsonResult DatosSeleccionCarreraJson(string psEnteraste, string psCarrera1, string psCarrera2)
		{
			try
			{
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@folio", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				loParametros.Add(new ParametrosSQL("@valor1", psEnteraste));
				loParametros.Add(new ParametrosSQL("@valor2", psCarrera1));
				loParametros.Add(new ParametrosSQL("@valor3", psCarrera2));
				GuardaDatosAspirante(loParametros);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ASP)]
		public JsonResult DatosEscuelaProcedenciaJson(string psIdEscuela, string psAño, string psPromedio)
		{
			try
			{
				if (string.IsNullOrEmpty(psAño) || string.IsNullOrEmpty(psPromedio))
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Capture todos los campos", false);
				}
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@folio", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				loParametros.Add(new ParametrosSQL("@valor1", psIdEscuela));
				loParametros.Add(new ParametrosSQL("@valor2", psAño));
				loParametros.Add(new ParametrosSQL("@valor3", psPromedio));
				GuardaDatosAspirante(loParametros);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
		private void GuardaDatosAspirante(List<ParametrosSQL> poParametros)
		{
			using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
			{
				SesionSapei.Sistema.Conexion.EjecutaEscalarProcedimientoAlmacenado("pam_aspirantes_registro", poParametros);
			}
		}

	}
}
