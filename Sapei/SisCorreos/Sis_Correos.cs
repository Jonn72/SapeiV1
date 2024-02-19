using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Sapei.Framework;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sapei
{
	/// <summary>
	/// Clase sis_correo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Sis_Correos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase sis_correo.
		/// </summary>
		public Sis_Correos():base()
		{
			NombreTabla = "sis_correos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase sis_correo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Sis_Correos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "sis_correos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece clave.Sin descripcion para clave 
		/// </summary>
		/// <value>
		/// clave 
		/// </value>
		[Key]
		[Required]
		public Int16 clave
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("clave");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("clave", value);
			}

		}
		/// <summary>
		/// Obtiene o establece descripcion.Sin descripcion para descripcion 
		/// </summary>
		/// <value>
		/// descripcion 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string descripcion
		{
			get
			{
				return ObtenerValorPropiedad<string>("descripcion");
			}

			set
			{
				EstablecerValorPropiedad<string>("descripcion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece asunto.Sin descripcion para asunto 
		/// </summary>
		/// <value>
		/// asunto 
		/// </value>
		[Required]
		[MaxLength (150)]
		[DefaultValue(null)]
		public string asunto
		{
			get
			{
				return ObtenerValorPropiedad<string>("asunto");
			}

			set
			{
				EstablecerValorPropiedad<string>("asunto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece correo_emisor.Sin descripcion para correo_emisor 
		/// </summary>
		/// <value>
		/// correo_emisor 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string correo_emisor
		{
			get
			{
				return ObtenerValorPropiedad<string>("correo_emisor");
			}

			set
			{
				EstablecerValorPropiedad<string>("correo_emisor", value);
			}

		}
		/// <summary>
		/// Obtiene o establece contrasenia.Sin descripcion para contrasenia 
		/// </summary>
		/// <value>
		/// contrasenia 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string contrasenia
		{
			get
			{
				return ObtenerValorPropiedad<string>("contrasenia");
			}

			set
			{
				EstablecerValorPropiedad<string>("contrasenia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece mensaje.Sin descripcion para mensaje 
		/// </summary>
		/// <value>
		/// mensaje 
		/// </value>
		[Required]
		[MaxLength (3000)]
		[DefaultValue(null)]
		public string mensaje
		{
			get
			{
				return ObtenerValorPropiedad<string>("mensaje");
			}

			set
			{
				EstablecerValorPropiedad<string>("mensaje", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla sis_correo.		/// </summary>
		/// <param name="piclave">clave</param>
		public void Cargar(Int16 piclave)
		{
			base.Cargar(piclave);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int16> loColInt16; 
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(6);

			AgregaCampoLlave("clave",null);

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = true;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 0;
			loColInt16.Descripcion = "Sin descripcion para clave";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("clave", loColInt16); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para descripcion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 150;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para asunto";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("asunto", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para correo_emisor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("correo_emisor", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para contrasenia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("contrasenia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3000;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para mensaje";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("mensaje", loColstring); 
			}
			#endregion

		}
	}
