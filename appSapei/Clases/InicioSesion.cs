using System;
using System.Data;
using System.Linq;
using Sapei;
using Sapei.Framework.Utilerias;
using Sapei.Framework.Utilerias.Funciones;

namespace appSapei
{
	public class InicioSesion
	{
		/// <summary>
		/// Con esta funcion validamos si es usuario master mandamos a configuraciones
		/// </summary>
		/// <returns></returns>
		public static bool EsUsuarioSapei(string psUsuario, string psContrasenia)
		{
			if (!Object.Equals(SesionSapei.AccesoConfiguracion, null))
				if (SesionSapei.AccesoConfiguracion)
				{
					//Eliminamos la variable del sistema si es que venimos de configuraciones y por alguna razon nos salimos
					//SesionSapei.SistemaDocentes = null;
					//SesionSapei.SistemaEstudiantes = null;
					//SesionSapei.SistemaPersonal = null;
				}
			SesionSapei.AccesoConfiguracion = false;
			if (psContrasenia == SesionSapei.Password && psUsuario == SesionSapei.Usuario)
			{
				//Cada vez que se ejecuta el comando Response.Redirect(url) en ASP.NET (para navegar hacia otra pagina), 
				//el framework lanza intencionalmente una excepción de tipo ThreadAbortException, con el único objetivo de terminar
				//absolutamente la ejecución del request actual.
				//Esto lo evitamos enviando el redirect con false para evitar que se termine la aplicaicon y no lance la
				//excepcion
				SesionSapei.AccesoConfiguracion = true;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Funcion que instancia la variable del sistema 
		/// </summary>
		public static void ValidaExisteVariableSistemaIniciada(bool pbLogin = false)
		{
			if (SesionSapei.Sistema == null)
			{
				SesionSapei.Sistema = new SistemaSapei(Sapei.Framework.Configuracion.enmSistema.SAPEI);
			}
			else if (!pbLogin)
				SesionSapei.Sistema.CerrarSesion();
			SesionSapei.Sistema.RenuevaParametros();

		}

		public static string SolicitaContraseña(string psUsuario, string psTipo)
		{
			string lsMensaje;
			lsMensaje = SesionSapei.Sistema.SolicitaContraseña(psUsuario,psTipo);
			if (!string.IsNullOrEmpty(lsMensaje))
			{
				return lsMensaje;
			}
			lsMensaje = ManejoCorreos.EnviaCodigoValidacion(SesionSapei.Sistema.Sesion.Usuario.Nombre, SesionSapei.Sistema.Sesion.Usuario.Correo);
			if (string.IsNullOrEmpty(lsMensaje))
			{
				return "No se puede código de validación a su correo registrado";
			}
			SesionSapei.Sistema.Sesion.Usuario.CodigoValidacion = lsMensaje;
			if(psTipo.Trim() == "A")
				SesionSapei.Sistema.Sesion.Usuario.TipoUsuario = Sapei.Framework.Configuracion.enmTipoUsuario.ESTUDIANTE;
			else
				SesionSapei.Sistema.Sesion.Usuario.TipoUsuario = Sapei.Framework.Configuracion.enmTipoUsuario.DOCENTE;
			lsMensaje = SesionSapei.Sistema.Sesion.Usuario.Correo;
			try
			{
				return "Se ha enviado el código a tu correo: " + lsMensaje.Split('@')[0].Substring(1, 3) + "***@" + lsMensaje.Split('@')[1];
			}
			catch (Exception) { }
			return "";
		}
	}
}