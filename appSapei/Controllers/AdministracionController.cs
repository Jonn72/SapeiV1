using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class AdministracionController : Controller
    {
         #region Logos
         /// <summary>
         /// ActivarPeriodo
         /// </summary>
         /// <returns></returns>
         [HttpGet]
         [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
         public PartialViewResult Logos()
         {
              try
              {
                   Sis_Logos loLogos;
                   loLogos = new Sis_Logos(SesionSapei.Sistema);
                   ViewData["Tabla"] = loLogos.RegresaTablaLogos(); ;
                   ViewData["Titulo"] = "Logos registrados";
                   ViewData["Encabezados"] = new List<string> { "Descripción","Permisos" ,"Logo" };

                   return PartialView("Logos");
              }
              catch (Exception ex)
              {
                   SesionSapei.Sistema.GrabaLog(ex);
                   return PartialView("Index", "Home");
              }
         }
         [HttpPost]
         [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD)]
         public JsonResult LogosJson(string piId, string psDescripcion)
         {
              Stream loSt;
              HttpPostedFileBase loFile;
              Sis_Logos loLogo;
              short liId;
              liId = Convert.ToInt16(piId);
              if(string.IsNullOrEmpty(psDescripcion))
                   return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Debe ingresar la descripción", false);

              try
              {
                   byte[] loData = null;
                   loLogo = new Sis_Logos(SesionSapei.Sistema);
                   using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                   {
                        loFile = Request.Files[0];
                        loSt = loFile.InputStream;
                        loSt.Position = 0;

                        using (var binaryReader = new BinaryReader(loSt))
                        {
                             loData = binaryReader.ReadBytes(loFile.ContentLength);
                        }
                        if (liId == 0)
                        {
                             loLogo.Nuevo();
                             loLogo.descripcion = psDescripcion;
                        }
                        else
                             loLogo.Cargar(liId);
                        loLogo.logo = loData;
                        loLogo.Guardar();
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Registro Correcto", true);
                   }
              }
              catch (Exception ex)
              {
                   SesionSapei.Sistema.GrabaLog(ex);
                   return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se generó un error, pongase en contacto con Centro de Cómputo", false);
              }
         }
         #endregion


    }
}
