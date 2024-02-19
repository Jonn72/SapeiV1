using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class SubDireccionController : Controller
    {
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
		[HttpGet]
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


		#region Recontratacion 

		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
		public PartialViewResult VistoBueno()
		{
			string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
			DataTable loDatos;
			Personal loPersonal = new Personal(SesionSapei.Sistema);
			loDatos = loPersonal.RegresaPersonalHorarioAutorizado(psUsuario);
			ViewData["Titulo"] = "Personal Solicitado";
			ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Tipo de Contrato", "Departamento que Solicita", "Acción" };
			ViewData["Tabla"] = loDatos;
			ViewData["Jefes"] = "0";
			return PartialView("VistoBueno");

		}

		
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
		public PartialViewResult HorarioJefes()
		{
			string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
			DataTable loDatos;
			Personal loPersonal = new Personal(SesionSapei.Sistema);
			loDatos = loPersonal.RegresaPersonalHorarioJefes(psUsuario);
			ViewData["Titulo"] = "Personal Solicitado";
			ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Departamento que Solicita", "Acción" };
			ViewData["Tabla"] = loDatos;
			ViewData["Jefes"] = "1";
			return PartialView("VistoBueno");

		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
		public PartialViewResult FechasPagosAsimilados()
		{
			try
			{

				DataTable loDatos, loDatos2;
				string psPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;

				
				Personal loPersonal = new Personal(SesionSapei.Sistema);
				loDatos = loPersonal.RegresaFechasPago(psPeriodo, "B");
				loDatos2 = loPersonal.RegresaIndice(psPeriodo, "B");

				int count = loDatos.Rows.Count;

				if (count > 0){
					ViewData["indice"] = loDatos2.Rows[0].RegresaValor<string>("id_fecha"); ;
					ViewData["fechas"] = loDatos;
					ViewData["periodo"] = psPeriodo;
					return PartialView();
				}

                else {
					ViewData["indice"] = 0;
					ViewData["periodo"] = psPeriodo;
					return PartialView();
				}
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
		public JsonResult RegistraFechasPagos(string lsobjeto)
		{
			try
			{
				string lsMensaje;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loDt = new DataTable();
				System.Data.SqlClient.SqlDataReader loReader;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@objeto", lsobjeto));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_fecha_pagos", loParametros);
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
						ViewData["Mensaje"] = lsMensaje;
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
					}
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

		public JsonResult EliminaFechaJSON(string psIdFecha, string psPeriodo, string psTipo)
		{
			try
			{
				string lsMensaje;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loDt = new DataTable();
				System.Data.SqlClient.SqlDataReader loReader;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@idfecha", psIdFecha));
					loParametros.Add(new ParametrosSQL("@tipopersonal", psTipo));
					loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
										
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_elimina_fecha_pago", loParametros);
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
					if (lsMensaje.Trim() == "delete")
					{
						ViewData["Mensaje"] = lsMensaje;
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
					}
					else if (lsMensaje.Trim() == "NE")
					{
						ViewData["Mensaje"] = lsMensaje;
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
					}
					else
					{
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
					}
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}


		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
		public PartialViewResult FechasPagosHonorarios()
		{
			try
			{

				DataTable loDatos, loDatos2;
				string psPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;


				Personal loPersonal = new Personal(SesionSapei.Sistema);
				loDatos = loPersonal.RegresaFechasPago(psPeriodo, "H");
				loDatos2 = loPersonal.RegresaIndice(psPeriodo, "H");

				int count = loDatos.Rows.Count;

				if (count > 0)
				{
					ViewData["indice"] = loDatos2.Rows[0].RegresaValor<string>("id_fecha"); ;
					ViewData["fechas"] = loDatos;
					ViewData["periodo"] = psPeriodo;
					return PartialView();
				}

				else
				{
					ViewData["indice"] = 0;
					ViewData["periodo"] = psPeriodo;
					return PartialView();
				}
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("../Home/Index");
			}
		}

		public PartialViewResult FirmaContratoSub(string psRFC = null, string psFechaCreacion = null, string psClaveArea = null, string psTipoContrato = null)
		{
		/*	try
			{
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
					DataTable loDatos, loDatos2;
					Personal loPersonal = new Personal(SesionSapei.Sistema);
					if (string.IsNullOrEmpty(psRFC))
					{
						ViewData["CSS2"] = "div-hide";
						loDatos = loPersonal.RegresaDatosFirmaContratoSub(psUsuario);
						ViewData["Titulo"] = "Contratos de Personal";
						ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Departamento", "Tipo de Contrato", "Acción" };
						ViewData["Tabla"] = loDatos;
					}
                    else
                    {
						ViewData["CSS1"] = "div-hide";
						loDatos2 = loPersonal.RegresaDatosPersonalContratoGeneral(psFechaCreacion, psRFC, psClaveArea, psTipoContrato);
						ViewData["txtRFC"] = psRFC;
						ViewData["txtCURP"] = loDatos2.Rows[0].RegresaValor<string>("curp");
						ViewData["txtNombre"] = loDatos2.Rows[0].RegresaValor<string>("nombre");
						ViewData["txtCalle"] = loDatos2.Rows[0].RegresaValor<string>("calle");
						ViewData["txtNumero"] = loDatos2.Rows[0].RegresaValor<string>("numero");
						ViewData["txtColonia"] = loDatos2.Rows[0].RegresaValor<string>("colonia");
						ViewData["txtCP"] = loDatos2.Rows[0].RegresaValor<string>("cod_post");
						ViewData["txtCD"] = loDatos2.Rows[0].RegresaValor<string>("ciudad_localidad");
						ViewData["txtEstado"] = loDatos2.Rows[0].RegresaValor<string>("nombre_entidad");
						ViewData["txtPeriodoInicio"] = loDatos2.Rows[0].RegresaValor<string>("periodo_inicio");
						ViewData["txtPeriodoFin"] = loDatos2.Rows[0].RegresaValor<string>("periodo_fin");
						ViewData["txtHSemanales"] = loDatos2.Rows[0].RegresaValor<string>("horas_semanales");
						ViewData["txtTNeto1"] = loDatos2.Rows[0].RegresaValor<string>("total_neto");
						ViewData["txtCLetra"] = loDatos2.Rows[0].RegresaValor<string>("monto_letra");
						ViewData["txtTBruto"] = loDatos2.Rows[0].RegresaValor<string>("total_bruto");
						ViewData["txtIVA"] = loDatos2.Rows[0].RegresaValor<string>("iva_trans");
						ViewData["txtSTotal"] = loDatos2.Rows[0].RegresaValor<string>("sub_total");
						ViewData["txtRIVA"] = loDatos2.Rows[0].RegresaValor<string>("iva_ret");
						ViewData["txtRISR"] = loDatos2.Rows[0].RegresaValor<string>("isr_ret");
						ViewData["txtTNeto2"] = loDatos2.Rows[0].RegresaValor<string>("total_neto");
						ViewData["txtFechaCreacion"] = psFechaCreacion;
						ViewData["txtClaveArea"] = psClaveArea;
						ViewData["txtTipoContrato"] = psTipoContrato;
					}*/
					return PartialView("FirmaContratoSub");
				/*}
			}
			catch (Exception ex)

			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return PartialView("Index");
			}*/
		}

		#endregion
	}
}