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
     
     public class PersonalController : Controller
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

        public PartialViewResult FirmaContratoPersonal()
        {
          /*   try
             {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {

                    DataTable loDatos;
                    Personal loPersonal;
                    int status;
                    string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                    loPersonal = new Personal(SesionSapei.Sistema);
                    loPersonal.Cargar(psUsuario);
                    string tipo = loPersonal.tipo_personal;
                    if (tipo == "X")
                    {
                        loDatos = loPersonal.RegresaDatosFirmaContrato(psUsuario);

                        if (loDatos.Rows.Count == 0) { status = 0; }
                        else
                        {
                            status = loDatos.Rows[0].RegresaValor<Int32>("status");
                        }

                        if (status <= 5)
                        {
                            ViewData["status"] = status;
                        }
                        else
                        {
                            ViewData["status"] = status;
                            DataTable loDatos2;
                            loDatos2 = loPersonal.RegresaDatosPersonalParaFirmaContrato(psUsuario, "H");
                            ViewData["txtRFC"] = loDatos2.Rows[0].RegresaValor<string>("rfc");
                            ViewData["txtCURP"] = loDatos2.Rows[0].RegresaValor<string>("curp");
                            ViewData["txtNombre"] = loDatos2.Rows[0].RegresaValor<string>("nombre");
                            ViewData["txtCalle"] = loDatos2.Rows[0].RegresaValor<string>("calle");
                            ViewData["txtNumero"] = loDatos2.Rows[0].RegresaValor<string>("numero");
                            ViewData["txtColonia"] = loDatos2.Rows[0].RegresaValor<string>("colonia");
                            ViewData["txtCP"] = loDatos2.Rows[0].RegresaValor<string>("cod_post");
                            ViewData["txtCD"] = loDatos2.Rows[0].RegresaValor<string>("ciudad_localidad");
                            ViewData["txtEstado"] = loDatos2.Rows[0].RegresaValor<string>("nombre_entidad");
                            ViewData["txtDescripcionPuesto"] = loDatos2.Rows[0].RegresaValor<string>("puesto");
                            ViewData["txtDepto"] = loDatos2.Rows[0].RegresaValor<string>("descripcion_area");
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
                            ViewData["txtFechaCreacion"] = loDatos2.Rows[0].RegresaValor<string>("fecha_creacion");
                            ViewData["txtClaveArea"] = loDatos2.Rows[0].RegresaValor<string>("clave_area");
                            

                        }

                    }
                 if (tipo == "B")
                    {
                        ViewData["status"] = 0;

                    }
                    if (tipo == "H")
                    {
                        loDatos = loPersonal.RegresaDatosFirmaContrato(psUsuario);

                        if (loDatos.Rows.Count == 0) { status = 0; }
                        else
                        {
                            status = loDatos.Rows[0].RegresaValor<Int32>("status");
                        }

                        if (status <= 5)
                        {
                            ViewData["status"] = status;
                        }
                        else
                        {
                            ViewData["status"] = status;
                            DataTable loDatos2;
                            loDatos2 = loPersonal.RegresaDatosPersonalParaFirmaContrato(psUsuario, "H");
                            ViewData["txtRFC"] = loDatos2.Rows[0].RegresaValor<string>("rfc");
                            ViewData["txtCURP"] = loDatos2.Rows[0].RegresaValor<string>("curp");
                            ViewData["txtNombre"] = loDatos2.Rows[0].RegresaValor<string>("nombre");
                            ViewData["txtCalle"] = loDatos2.Rows[0].RegresaValor<string>("calle");
                            ViewData["txtNumero"] = loDatos2.Rows[0].RegresaValor<string>("numero");
                            ViewData["txtColonia"] = loDatos2.Rows[0].RegresaValor<string>("colonia");
                            ViewData["txtCP"] = loDatos2.Rows[0].RegresaValor<string>("cod_post");
                            ViewData["txtCD"] = loDatos2.Rows[0].RegresaValor<string>("ciudad_localidad");
                            ViewData["txtEstado"] = loDatos2.Rows[0].RegresaValor<string>("nombre_entidad");
                            ViewData["txtDescripcionPuesto"] = loDatos2.Rows[0].RegresaValor<string>("puesto");
                            ViewData["txtDepto"] = loDatos2.Rows[0].RegresaValor<string>("descripcion_area");
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
                            ViewData["txtFechaCreacion"] = loDatos2.Rows[0].RegresaValor<string>("fecha_creacion");
                            ViewData["txtClaveArea"] = loDatos2.Rows[0].RegresaValor<string>("clave_area");

                        }
                        //ViewData["clavearea"] = loDatos.Rows[0].RegresaValor<string>("clave_area"); = loDatos2.Rows[0].RegresaValor<string>("");
                        //ViewData["rfc"] = psUsuario;
                        //ViewData["periodo"] = psPeriodo;
                    }*/
                    return PartialView("FirmaContratoPersonal");
             /*   }
             }
             catch (Exception ex)

             {
                 Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                 return PartialView("Index");
             }

           // return PartialView("FirmaContrato");
*/
        }

    }
  
     
}