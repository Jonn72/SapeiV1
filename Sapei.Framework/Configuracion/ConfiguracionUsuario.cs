using System;
using System.Collections.Generic;

namespace Sapei.Framework.Configuracion
{
	[Serializable]
	public class ConfiguracionUsuario
	{
		/// <summary>
		/// Tipo de usuario
		/// </summary>
		public enmTipoUsuario TipoUsuario { get; set; }

		/// <summary>
		/// Nombre del usuario
		/// </summary>
		public string Usuario { get; set; }
		/// <summary>
		/// Nombre del usuario
		/// </summary>
		public string Nombre { get; set; }
		/// <summary>
		/// Contraseña del usuario
		/// </summary>
		public string Contraseña { get; set; }

		/// <summary>
		/// Permisos del usuario
		/// </summary>
		public List<string> Permisos { get; set; }
		/// <summary>
		/// Permisos sobre funcion del usuario
		/// </summary>
		public List<string> PermisosFuncion { get; set; }
		/// <summary>
		/// Permisos sobre funcion del usuario
		/// </summary>
		public List<string> PermisosCarreras { get; set; }
		/// <summary>
		/// Permisos de funcion del usuario
		/// </summary>
		public List<string> FuncionesPermitidas { get; set; }
		/// <summary>
		/// Perfil de acceso a catálogos de Metadatos (Este usa el clsSICMedat)
		/// </summary>
		public int Perfil { get; set; }

		/// <summary>
		/// Indica si es Super usuario
		/// </summary>
		public bool EsSuperUsuario { get; set; }

		/// <summary>
		/// Correo del Usuario en el Login
		/// </summary>
		public string Correo { get; set; }

		/// <summary>
		/// Tipo de Acceso que tiene definido el Usuario.
		/// </summary>
		public enmRolUsuario RolUsuario { get; set; }
		
		/// <summary>
		/// Indica si es Super usuario
		/// </summary>
		public bool ValidacionFiElContraseña { get; set; }

		public bool SolicitudContraseñaActiva { get; set; }

		public string CodigoValidacion { get; set; }

	}
}
