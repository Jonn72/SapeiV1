using System;


namespace Sapei.Framework.Configuracion
{
	[Serializable]
	public class ConfiguracionSesion
	{
		#region Informacion Institucion

		/// <summary>
		/// Gets or sets the institucion.
		/// </summary>
		/// <value>
		/// The institucion.
		/// </value>
		public ConfiguracionInstitucion Institucion { get; set; }

		#endregion
		#region Informacion General
		  /// <summary>
		  /// Pais de la institucion actual
		  /// </summary>
		  public Sapei.Framework.Configuracion.enmPais Pais
		{ get; set; }
		/// <summary>
		/// Número de Idioma
		/// </summary>
		public Sapei.Framework.Configuracion.enmLenguaje Lenguaje { get; set; }

		#endregion
		#region Informacion Usuario

		/// <summary>
		/// Gets or sets the usuario.
		/// </summary>
		/// <value>
		/// The usuario.
		/// </value>
		public ConfiguracionUsuario Usuario { get; set; }

		#endregion
		#region Inofrmacion Periodo Actual
		public ConfiguracionPeriodo Periodo { get; set; }
		#endregion
		#region Informacion del menu de usuario segun permisos y perfil
		/// <summary>
		/// Menu principal de usuario segun permisos
		/// </summary>
		public ConfiguracionMenu Menus { get; set; }
		#endregion

		#region Constructores

		/// <summary>
		/// Constructor basico para la clase. Solo carga los valores para las fechas y tipo de monedas
		/// </summary>
		public ConfiguracionSesion()
		{
			this.Usuario = new ConfiguracionUsuario();
			this.Institucion = new ConfiguracionInstitucion();
			this.Menus = new ConfiguracionMenu();
			this.Periodo = new ConfiguracionPeriodo();
		}

		#endregion

		#region Funciones
		/// <summary>
		/// valida la sesion
		/// </summary>
		/// <param name="penmTipoUsu"></param>
		/// <returns></returns>
		public bool ValidaSesion(Sapei.Framework.Configuracion.enmTipoUsuario penmTipoUsu)
		{
			if (string.IsNullOrEmpty(this.Usuario.Usuario))
			{
				return false;
			}
			if (penmTipoUsu != this.Usuario.TipoUsuario)
			{
				return false;
			}
			return true;
		}
		#endregion

	}
}