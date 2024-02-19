using System;

namespace Sapei.Framework.Configuracion
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class Servidor
	{
		/// <summary>
		/// Gets or sets the nombre.
		/// </summary>
		/// <value>
		/// The nombre.
		/// </value>
		public string Nombre { get; set; }

		/// <summary>
		/// Gets or sets the servidor vinculado.
		/// </summary>
		/// <value>
		/// The servidor vinculado.
		/// </value>
		public string ServidorVinculado { get; set; }

		/// <summary>
		/// Gets or sets the base datos.
		/// </summary>
		/// <value>
		/// The base datos.
		/// </value>
		public BaseDeDatos BaseDatos { get; set; }

		/// <summary>
		/// Tipo de motor de base de datos (1=Access,2=SQL)
		/// </summary>
		public Sapei.Framework.BaseDatos.enmMotor Motor { get; set; }

		/// <summary>
		/// Gets or sets the version sistema.
		/// </summary>
		/// <value>
		/// The version sistema.
		/// </value>
		public enmSistema VersionSistema { get; set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="Servidor"/> class.
		/// </summary>
		/// 		/// <summary>
		/// Valores de EvoBanamex para pago en linea
		/// </summary>
		public ConfiguracionEvoBanamex EvoBanamex { get; set; }
		public Servidor()
		{
			BaseDatos = new BaseDeDatos();
			EvoBanamex = new ConfiguracionEvoBanamex();
			//ServidorVinculado = new List<string>();
		}
	}
}
