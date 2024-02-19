using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sapei.Framework.BaseDatos;

namespace Sapei.Framework.Configuracion
{
	/// <summary>
	/// Clase que contiene la informacion de la base de datos del servidor
	/// </summary>
	[Serializable]
	public class ConfiguracionServidor
	{
		#region Conexion a la base de datos

		/// <summary>
		/// Gets or sets the base de datos.
		/// </summary>
		/// <value>
		/// The base de datos.
		/// </value>
		public Servidor Principal { get; set; }

		/// <summary>
		/// Gets or sets the otras bases.
		/// </summary>
		/// <value>
		/// The otras bases.
		/// </value>
		public Dictionary<string, Servidor> OtrasBases { get; set; }

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor sobrecargado para cargar la configuracion del servidor usando el web.config
		/// </summary>
		/// <param name="penSistema"></param>
		public ConfiguracionServidor(Sapei.Framework.Configuracion.enmSistema penSistema)
		{
			ConstructorInterno(penSistema);
			this.CargaParametrosConfig(penSistema);
		}

		#endregion

		#region Funciones

		/// <summary>
		/// Constructors the interno.
		/// </summary>
		/// <param name="penSistema">The pen sistema.</param>
		private void ConstructorInterno(Sapei.Framework.Configuracion.enmSistema penSistema)
		{
			Principal = new Servidor();
			OtrasBases = new Dictionary<string, Servidor>();
			Principal.VersionSistema = penSistema;
		}
		/// <summary>
		/// Funcion que carga los parametros apartir del archivo config
		/// </summary>
		/// <param name="penPlataforma">The pen plataforma.</param>
		private void CargaParametrosConfig(Sapei.Framework.Configuracion.enmSistema penSistema)
		{
			ArchivoConfig loArchivoConfig = null;
			string lsRespuesta;
			string lsNombreCadenaConexion;
			try
			{
				loArchivoConfig = new ArchivoConfig(penSistema);
				//Parametros                   
				lsNombreCadenaConexion = ManejadorConfiguraciones.Settings.BaseDatosDefault;
				lsRespuesta = loArchivoConfig.ObtenerValorParametro("BDPrefijo");
				Principal.BaseDatos.Prefijo = (lsRespuesta != "?" ? lsRespuesta : "");
				Principal.Motor = (enmMotor)2;
				lsRespuesta = loArchivoConfig.ObtenerValorParametro("BDMotor");
				if (!String.IsNullOrEmpty(lsRespuesta) && lsRespuesta != "?")
				{
					Principal.Motor = (enmMotor)int.Parse(lsRespuesta);
				}
				lsRespuesta = loArchivoConfig.ObtenerValorParametro("BDBitacora");
				Principal.BaseDatos.Bitacora = (lsRespuesta != "?" ? lsRespuesta : "");
				lsRespuesta = loArchivoConfig.ObtenerValorParametro("ModoDebug");
				lsRespuesta = loArchivoConfig.ObtenerValorParametro("ModoLog");
				Principal.BaseDatos.CadenaConexion = loArchivoConfig.ObtenerCadenaConexion(lsNombreCadenaConexion);
				ObtieneValoresCadenaConexion();
				lsRespuesta = loArchivoConfig.ObtenerValorParametro("ForzarActualizaciones");
				Principal.BaseDatos.Parametros.ForzarActualizaciones = Convert.ToBoolean(!String.IsNullOrEmpty(lsRespuesta) && lsRespuesta != "?" ? lsRespuesta : "false");
				Principal.EvoBanamex.GatewayHost = loArchivoConfig.ObtenerValorParametro("Evo:GatewayHost");
				Principal.EvoBanamex.Version = loArchivoConfig.ObtenerValorParametro("Evo:Version");
				Principal.EvoBanamex.MerchantId = loArchivoConfig.ObtenerValorParametro("Evo:MerchantId");
				Principal.EvoBanamex.Username = loArchivoConfig.ObtenerValorParametro("Evo:Username");
				Principal.EvoBanamex.Password = loArchivoConfig.ObtenerValorParametro("Evo:Password");
				Principal.EvoBanamex.ApiOperation = loArchivoConfig.ObtenerValorParametro("Evo:ApiOperation");
				Principal.EvoBanamex.Operation = loArchivoConfig.ObtenerValorParametro("Evo:Operation");
				Principal.EvoBanamex.BillingAddress = loArchivoConfig.ObtenerValorParametro("Evo:BillingAddress");
				Principal.EvoBanamex.Timeout = loArchivoConfig.ObtenerValorParametro("Evo:Timeout");
				Principal.EvoBanamex.MerchantName = loArchivoConfig.ObtenerValorParametro("Evo:MerchantName");
			}
			catch (Exception ex)
			{
				if (!(ex is Sapei.Framework.SolExcepcion))
					throw;
			}
			finally
			{
				loArchivoConfig = null;
			}
		}

		/// <summary>
		/// Obtienes the valores cadena conexion.
		/// </summary>
		private void ObtieneValoresCadenaConexion()
		{
			SqlConnectionStringBuilder loConexion;

			switch (Principal.Motor)
			{
				case enmMotor.Sql:
					loConexion = new SqlConnectionStringBuilder(Principal.BaseDatos.CadenaConexion);
					Principal.BaseDatos.Catalogo = loConexion.InitialCatalog;
					Principal.BaseDatos.Propietario = "dbo";
					Principal.BaseDatos.Usuario = loConexion.UserID;
					Principal.BaseDatos.Contraseña = loConexion.Password;
					Principal.Nombre = loConexion.DataSource;
					break;

				default:
					loConexion = new SqlConnectionStringBuilder(Principal.BaseDatos.CadenaConexion);
					Principal.BaseDatos.Catalogo = loConexion.InitialCatalog;
					Principal.BaseDatos.Propietario = "dbo";
					Principal.BaseDatos.Usuario = loConexion.UserID;
					Principal.BaseDatos.Contraseña = loConexion.Password;
					Principal.Nombre = loConexion.DataSource;
					break;
			}

		}



		/// <summary>
		/// Funcion que pertime parsear la cadena de conexion
		/// </summary>
		/// <param name="psServidor">The ps servidor.</param>
		/// <param name="psCatalogo">The ps catalogo.</param>
		/// <param name="psUsuario">The ps usuario.</param>
		/// <param name="psContraseña">The ps contraseña.</param>
		/// <returns></returns>
		private string ArmaCadenadeConexion(string psServidor, string psCatalogo, string psUsuario, string psContraseña)
		{
			StringBuilder lsConexion;
			lsConexion = new StringBuilder();
			switch (Principal.Motor)
			{
				case enmMotor.Sql:
					lsConexion.AppendFormat("Persist Security Info=True");
					lsConexion.AppendFormat(";Initial Catalog={0}", psCatalogo);
					lsConexion.AppendFormat(";Data Source={0}", psServidor);
					if (!string.IsNullOrEmpty(Principal.BaseDatos.Usuario))
					{
						lsConexion.AppendFormat(";User id={0}", psUsuario);
						lsConexion.AppendFormat(";Password={0}", psContraseña);
					}
					else
					{
						lsConexion.AppendFormat(" ;Integrated Security=True ");
					}
					lsConexion.AppendFormat(" ;MultipleActiveResultSets=True ");
					break;
			}
			//this.StrConectPrincipal = lsConexion.AppendFormat(" {0}", this._sConexionNETEspecificaciones).ToString();
			return lsConexion.ToString();
		}

		/// <summary>
		/// Agregas the servidor vinculado conexion principal.
		/// </summary>
		/// <param name="psServidorVinculado">The ps servidor vinculado.</param>
		public void AgregaServidorVinculadoConexionPrincipal(string psServidorVinculado)
		{
			Principal.ServidorVinculado = psServidorVinculado;
		}

		/// <summary>
		/// Creas the objeto de conexion.
		/// </summary>
		/// <param name="psNombreConexion">The ps nombre conexion.</param>
		/// <param name="psServidor">The ps servidor.</param>
		/// <param name="psCatalogo">The ps catalogo.</param>
		/// <param name="psUsuario">The ps usuario.</param>
		/// <param name="psContraseña">The ps contraseña.</param>
		/// <param name="penMotor">The pen motor.</param>
		public void CreaObjetoDeConexion(string psNombreConexion, string psServidor, string psCatalogo, string psUsuario, string psContraseña, enmMotor penMotor)
		{
			Servidor loServidor;
			loServidor = new Servidor();
			loServidor.Nombre = psServidor;
			loServidor.BaseDatos.Catalogo = psCatalogo;
			loServidor.BaseDatos.Usuario = psUsuario;
			loServidor.BaseDatos.Contraseña = psContraseña;
			loServidor.Motor = penMotor;
			OtrasBases[psNombreConexion] = loServidor;
		}

		#endregion
	}
}
