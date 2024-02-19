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
    public class DireccionController : Controller
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
        #region Recontratacion
        [HttpGet]
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DIR)]
        public PartialViewResult AutorizaDireccion()
        {
            DataTable loDatos;
            Personal loPersonal = new Personal(SesionSapei.Sistema);
            loDatos = loPersonal.RegresaPersonalConVB();
            ViewData["Titulo"] = "Personal Solicitado";
            ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Departamento que Solicita", "Acción" };
            ViewData["Tabla"] = loDatos;
            return PartialView("AutorizaDireccion");

        }
        public PartialViewResult FirmaContratoDir(string psRFC = null, string psFechaCreacion = null, string psClaveArea = null, string psTipoContrato = null)
        {
          /*  try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                    DataTable loDatos, loDatos2;
                    Personal loPersonal = new Personal(SesionSapei.Sistema);
                    if (string.IsNullOrEmpty(psRFC))
                    {
                        ViewData["CSS2"] = "div-hide";
                        loDatos = loPersonal.RegresaDatosFirmaContratoDireccion(psUsuario);
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
                    return PartialView("FirmaContratoDir");
            /*
                }
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("FirmaContratoDir");
            }*/
        }
        #endregion
    }
}
