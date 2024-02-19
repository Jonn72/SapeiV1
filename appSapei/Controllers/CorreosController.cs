using appSapei.App_Start;
using System;
using System.Web.Mvc;
using Sapei.Framework.Utilerias.Funciones;
using Sapei.Framework.Utilerias;

namespace appSapei.Controllers
{
    public class CorreosController : Controller
    {
		public JsonResult ResetContraseñaCuentas(string psServicio, string psContraseña)
		{
			try
			{
				ManejoCorreos.EnviaSolicitudResetCuentaMesaAyuda(SesionSapei.Sistema.Sesion.Usuario.Usuario,
					SesionSapei.Sistema.Sesion.Usuario.Nombre,psServicio,psContraseña);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
	}
}