using Sapei.Framework.Utilerias;
using System;
using System.Text;
using System.Web.Mvc;

namespace appSapei.Controllers
{
	public class ValidadorDocumentosController : Controller
	{
		public ActionResult Index(string psCadena)
		{
			try
			{
				ViewData["mensaje"] = "Validación de documentos del Instituto Tecnológico de Tláhuac";
				psCadena = psCadena.EliminaEspaciosEnBlanco();
				if (string.IsNullOrEmpty(psCadena))
				{
					ViewData["resultado"] = "Documento Incorrecto";
				}
				else
				{
					ViewData["resultado"] = GeneraHtmlDatos(psCadena);
				}
				return View();
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["resultado"] = "Lo sentimos, el código no se reconoce";
				return View();
			}		
		}
		private string GeneraHtmlDatos(string psDatos)
		{
			string[] lasCadena;
			string[] lasDato;
			string lsDatos;
			StringBuilder lsResultadoHtml = new StringBuilder();
			InicioSesion.ValidaExisteVariableSistemaIniciada();
			lsDatos = SesionSapei.Sistema.RecuperaValidacionDocumento(psDatos.Trim());
			if (string.IsNullOrEmpty(lsDatos))
				return "Código invalido o documento alterado";
			lasCadena = lsDatos.Split('&');
			foreach (string lsDato in lasCadena)
			{
				lasDato = lsDato.Split(':');
				lsResultadoHtml.Append("<div class=\"row\"> ");

				lsResultadoHtml.Append("<h2 class=\"title col-md-3\">");
				lsResultadoHtml.AppendFormat("<a> {0}:</a></h2>", lasDato[0]);
				lsResultadoHtml.Append("<h3 class=\"col-md-9\">");
				lsResultadoHtml.AppendFormat("<a><b> {0}</b></a></h3>", lasDato[1]);
				lsResultadoHtml.Append("</div>&nbsp;");
			}
			return lsResultadoHtml.ToString();
		}
	}
}
