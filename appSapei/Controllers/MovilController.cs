using appSapei.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
     
    public class MovilController : Controller
    {
         [SessionExpire]
        public PartialViewResult EstadisticasGnosis()
        {
             return PartialView("EstadisticasGnosis");
        }
         [SessionExpire]
         public PartialViewResult EstadisticasAnfei()
        {
             return PartialView("EstadisticasAnfei");
        }
    }
}
