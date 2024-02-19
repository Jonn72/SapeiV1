using appSapei.App_Start;
using Sapei.Framework.Utilerias;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
	public class PaginaWebController : Controller
	{
		[SessionExpire]
		public PartialViewResult Banners()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire]
		public PartialViewResult Noticias()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire]
		public PartialViewResult Sitios_Interes()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire]
		public PartialViewResult Links_Interes()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				return PartialView("Index", "Home");
			}
		}
		public PartialViewResult Videos()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				return PartialView("Index", "Home");
			}
		}
		public PartialViewResult Convocatorias()
		{
			try
			{
				return PartialView();
			}
			catch (Exception ex)
			{
				return PartialView("Index", "Home");
			}
		}
	}
}
