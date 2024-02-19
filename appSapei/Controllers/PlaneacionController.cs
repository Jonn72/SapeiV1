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
    public class PlaneacionController : Controller
    {
         [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.PLA)]
         public PartialViewResult Consultas()
         {
              try
              {
                   Sis_Consultas loSis;
                   loSis = new Sis_Consultas(SesionSapei.Sistema);
                   ViewData["Tabla"] = loSis.RegresaConsultaDocentesMateria();
                   ViewData["titulo_consultas"] = "Docentes";
                   ViewData["Encabezados"] = new List<string> { "Materia", "RFC", "Docente","Carrera","Escolaridad", "Asignación" };
                   return PartialView();
              }
              catch (Exception ex)
              {
                   SesionSapei.Sistema.GrabaLog(ex);
                   return PartialView("~/Personal/Index");
              }

         }

        public PartialViewResult ConsultaRetardos()
        {
            try
            {
                string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                Personal loPersonal = new Personal(SesionSapei.Sistema);
                ViewData["Personal"] = loPersonal.RegresaPersonalContratadoAdministrativo();
                return PartialView("ConsultaRetardos");
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }

        public JsonResult RegresaRetardos(int psID, string psFechaInicioRetardo, string psFechaFinRetardo)
        {
            try
            {
                DataTable loDt = new DataTable();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loPersonal = new Personal(SesionSapei.Sistema);
                    loDt = loPersonal.RegresaDatosRetardos(psID, psFechaInicioRetardo, psFechaFinRetardo);
                    if (loDt.Rows.Count <= 0)
                    {

                        return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();

                    }
                    else
                    {
                        if(loDt.Rows[0].RegresaValor<string>("no_retardos") == "" || loDt.Rows[0].RegresaValor<string>("no_retardos") == "0")
                        {
                            return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
                        }
                        else
                        {
                            return ManejoMensajesJson.RegresaJsonTabla(loDt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

    }
}
