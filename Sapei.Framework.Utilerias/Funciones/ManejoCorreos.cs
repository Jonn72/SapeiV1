
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;

namespace Sapei.Framework.Utilerias.Funciones
{
	public static class ManejoCorreos
	{
		#region Contructores
		#endregion

		#region Metodos
		public static void EnviarCorreo(string psMensaje, string psAsunto, string psCorreo)
		{
			var lsCorreoEmisor = SesionSapei.Sistema.GetParametro("CorreoParaAspirantes");
			var lsContraseñaEmisor = SesionSapei.Sistema.GetParametro("ContraseñaCorreoParaAspirantes");
			var poEmisor = new MailAddress(lsCorreoEmisor, SesionSapei.Sistema.Sesion.Institucion.Nombre);
			var poReceptor = new MailAddress(psCorreo, "Receptor");
			var poSMTP = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(poEmisor.Address, lsContraseñaEmisor)

			};

			using (var mess = new MailMessage(poEmisor, poReceptor)
			{
				Subject = psAsunto,
				Body = psMensaje
			}
			)
			{
				poSMTP.Send(mess);
			}
		}
		public static void EnviarCorreo(string psMensaje, string psAsunto, string psCorreo, string psEmisor, string psContraseña, Configuracion.enmSistema penmSistema = Configuracion.enmSistema.SAPEI)
		{
			var lsCorreoEmisor = psEmisor;
			var lsContraseñaEmisor = psContraseña;
			string lsNombreEmisor = "";
			try
			{
				if (penmSistema == Configuracion.enmSistema.SAPEI)
					lsNombreEmisor = SesionSapei.Sistema.Sesion.Institucion.Nombre;
				else if (penmSistema == Configuracion.enmSistema.PROCESO)
					lsNombreEmisor = "Sistema SapeiMQ";

				var poEmisor = new MailAddress(lsCorreoEmisor, lsNombreEmisor);
				var poReceptor = new MailAddress(psCorreo, "Receptor");
				var poSMTP = new SmtpClient
				{
					Host = "smtp.gmail.com",
					Port = 587,
					EnableSsl = true,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(poEmisor.Address, lsContraseñaEmisor)

				};

				using (var mess = new MailMessage(poEmisor, poReceptor)
				{
					Subject = psAsunto,
					IsBodyHtml = true,
					Body = psMensaje
				}
				)
				{
					//mess.AlternateViews.Add(psMensaje);
					poSMTP.Send(mess);
				}
			}
			catch (Exception ex)
			{
				return;
			}

		}

		public static void EnviarCorreoContactanosAspirante(string psFolio, string psNombre, string psCorreo, string psAsunto, string psComentarios)
		{
			StringBuilder lsMensaje;
			lsMensaje = new StringBuilder();
			string lsAsunto;
			lsMensaje.Append("Datos del Contacto \n\n");
			lsMensaje.AppendFormat("\t Nombre: {0}\n", psNombre);
			lsMensaje.AppendFormat("\t Folio: {0}\n", psFolio);
			lsMensaje.AppendFormat("\t Correo: {0}\n", psCorreo);
			lsMensaje.AppendFormat("\t Asunto: {0}\n", psAsunto);
			lsMensaje.AppendFormat("\t Comentario: \n{0}", psComentarios);
			lsAsunto = "Contactanos";
			EnviarCorreo(lsMensaje.ToString(), lsAsunto, SesionSapei.Sistema.GetParametro("CorreoParaAspirantes"));
		}
		public static void EnviaRegistroAspirante(string psNombre, string psPaterno, string psMaterno, string psCorreo, string psContraseña, string psFolio)
		{
			string lsMensaje;
			string lsAsunto;
			Sis_Correos loCorreos;
			loCorreos = new Sis_Correos(SesionSapei.Sistema);
			loCorreos.Cargar(1);
			lsMensaje = string.Format(loCorreos.mensaje, psNombre + " " + psPaterno + " " + psMaterno, psFolio, psContraseña);
			lsAsunto = loCorreos.asunto;
			EnviarCorreo(lsMensaje, lsAsunto, psCorreo, loCorreos.correo_emisor, loCorreos.contrasenia);
		}
		public static string EnviaCodigoValidacion(string psNombre, string psCorreo)
		{
			string lsMensaje;
			string lsAsunto;
			string lsCodigo;
			Random loRand;
			Sis_Correos loCorreos;
			loCorreos = new Sis_Correos(SesionSapei.Sistema);
			loRand = new Random();
			loCorreos.Cargar(2);
			lsCodigo = Convert.ToString(loRand.Next(10000, 99999));
			lsMensaje = string.Format(loCorreos.mensaje, psNombre, lsCodigo);
			lsAsunto = loCorreos.asunto;

			EnviarCorreo(lsMensaje, lsAsunto, psCorreo, loCorreos.correo_emisor, loCorreos.contrasenia);
			return lsCodigo;
		}
		public static void EnviaSolicitudResetCuentaMesaAyuda(string psNoControl, string psNombre, string psServicio, string psContraseña)
		{
			string lsMensaje;
			string lsAsunto;
			Random loRand;
			Sis_Correos loCorreos;
			loCorreos = new Sis_Correos(SesionSapei.Sistema);
			loRand = new Random();
			loCorreos.Cargar(4);
			lsMensaje = string.Format(loCorreos.mensaje, psServicio, psNombre, psNoControl, psContraseña);
			lsAsunto = loCorreos.asunto;

			EnviarCorreo(lsMensaje, lsAsunto, loCorreos.correo_emisor, loCorreos.correo_emisor, loCorreos.contrasenia);
		}
		public static void EnviaRecuperacionContraseña(string psNombre, string psCorreo, string psContraseña)
		{
			string lsMensaje;
			string lsAsunto;
			Random loRand;
			Sis_Correos loCorreos;
			loCorreos = new Sis_Correos(SesionSapei.Sistema);
			loRand = new Random();
			loCorreos.Cargar(5);
			lsMensaje = string.Format(loCorreos.mensaje, psNombre, psContraseña);
			lsAsunto = loCorreos.asunto;

			EnviarCorreo(lsMensaje, lsAsunto, psCorreo, loCorreos.correo_emisor, loCorreos.contrasenia);
		}
		public static void NotificaPagoServicio(string psNombre, string psCorreo, string psIndicador, string psServicio, string psMonto)
		{
			string lsMensaje;
			string lsAsunto;
			Random loRand;
			Sis_Correos loCorreos;
			loCorreos = new Sis_Correos(SesionSapei.Sistema);
			loRand = new Random();
			loCorreos.Cargar(7);
			lsMensaje = loCorreos.mensaje.Replace("{0}", psNombre);
			lsMensaje = lsMensaje.Replace("{1}", psIndicador);
			lsMensaje = lsMensaje.Replace("{2}", psServicio);
			lsMensaje = lsMensaje.Replace("{3}", psMonto);
			lsMensaje = lsMensaje.Replace("{4}", psIndicador.RegresaCadenaQRValidacionDocumentos());

			lsAsunto = loCorreos.asunto;

			EnviarCorreo(lsMensaje, lsAsunto, psCorreo, loCorreos.correo_emisor, loCorreos.contrasenia);
		}
		#endregion

		#region Notificacion a administrador
		public static void EnviaNotificacionExpedianteDigital(string psMensaje, Configuracion.enmSistema penmSistema = Configuracion.enmSistema.SAPEI, SistemaSapei poSistema = null)
		{
			string lsMensaje;
			string lsAsunto;
			Random loRand;
			Sis_Correos loCorreos = null;
			if (penmSistema == Configuracion.enmSistema.SAPEI)
				loCorreos = new Sis_Correos(SesionSapei.Sistema);
			else if (penmSistema == Configuracion.enmSistema.PROCESO)
				loCorreos = new Sis_Correos(poSistema);
			loRand = new Random();
			loCorreos.Cargar(6);
			lsMensaje = string.Format(loCorreos.mensaje, psMensaje);
			lsAsunto = loCorreos.asunto;

			EnviarCorreo(lsMensaje, lsAsunto, "ccomputo@tlahuac.tecnm.mx", loCorreos.correo_emisor, loCorreos.contrasenia, penmSistema);
		}
		#endregion
	}


	//Paso 4. Mandar Email con los datos del alumno

}
