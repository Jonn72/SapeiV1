using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class EncuestasController : Controller
    {
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

    }
}
