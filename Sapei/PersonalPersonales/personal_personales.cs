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
	/// Clase personal_personales generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class personal_personales:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_personale.
		/// </summary>
		public personal_personales():base()
		{
			NombreTabla = "personal_personales";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_personale.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public personal_personales(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "personal_personales";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece rfc.Sin descripcion para rfc 
		/// </summary>
		/// <value>
		/// rfc 
		/// </value>
		[Key]
		[Required]
		[MaxLength (13)]
		[DefaultValue(null)]
		public string rfc
		{
			get
			{
				return ObtenerValorPropiedad<string>("rfc");
			}

			set
			{
				EstablecerValorPropiedad<string>("rfc", value);
			}

		}
		/// <summary>
		/// Obtiene o establece curp.Sin descripcion para curp 
		/// </summary>
		/// <value>
		/// curp 
		/// </value>
		[Required]
		[MaxLength (18)]
		[DefaultValue(null)]
		public string curp
		{
			get
			{
				return ObtenerValorPropiedad<string>("curp");
			}

			set
			{
				EstablecerValorPropiedad<string>("curp", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estado_civil.Sin descripcion para estado_civil 
		/// </summary>
		/// <value>
		/// estado_civil 
		/// </value>
		[Required]
		[MaxLength (1)]
		[DefaultValue(null)]
		public string estado_civil
		{
			get
			{
				return ObtenerValorPropiedad<string>("estado_civil");
			}

			set
			{
				EstablecerValorPropiedad<string>("estado_civil", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nacionalidad.Sin descripcion para nacionalidad 
		/// </summary>
		/// <value>
		/// nacionalidad 
		/// </value>
		[Required]
		[MaxLength (2)]
		[DefaultValue(null)]
		public string nacionalidad
		{
			get
			{
				return ObtenerValorPropiedad<string>("nacionalidad");
			}

			set
			{
				EstablecerValorPropiedad<string>("nacionalidad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nivel_estudios.Sin descripcion para nivel_estudios 
		/// </summary>
		/// <value>
		/// nivel_estudios 
		/// </value>
		[Required]
		[MaxLength (1)]
		[DefaultValue(null)]
		public string nivel_estudios
		{
			get
			{
				return ObtenerValorPropiedad<string>("nivel_estudios");
			}

			set
			{
				EstablecerValorPropiedad<string>("nivel_estudios", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_carrera.Sin descripcion para nombre_carrera 
		/// </summary>
		/// <value>
		/// nombre_carrera 
		/// </value>
		[Required]
		[MaxLength (250)]
		[DefaultValue(null)]
		public string nombre_carrera
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_carrera");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_carrera", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_titulacion.Sin descripcion para fecha_titulacion 
		/// </summary>
		/// <value>
		/// fecha_titulacion 
		/// </value>
		[MaxLength (3)]
		[DefaultValue(null)]
		public string fecha_titulacion
		{
			get
			{
				return ObtenerValorPropiedad<string>("fecha_titulacion");
			}

			set
			{
				EstablecerValorPropiedad<string>("fecha_titulacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece cedula_profesional.Sin descripcion para cedula_profesional 
		/// </summary>
		/// <value>
		/// cedula_profesional 
		/// </value>
		[Required]
		[MaxLength (15)]
		[DefaultValue(null)]
		public string cedula_profesional
		{
			get
			{
				return ObtenerValorPropiedad<string>("cedula_profesional");
			}

			set
			{
				EstablecerValorPropiedad<string>("cedula_profesional", value);
			}

		}
		/// <summary>
		/// Obtiene o establece correo_electronico.Sin descripcion para correo_electronico 
		/// </summary>
		/// <value>
		/// correo_electronico 
		/// </value>
		[Required]
		[MaxLength (60)]
		[DefaultValue(null)]
		public string correo_electronico
		{
			get
			{
				return ObtenerValorPropiedad<string>("correo_electronico");
			}

			set
			{
				EstablecerValorPropiedad<string>("correo_electronico", value);
			}

		}
		/// <summary>
		/// Obtiene o establece telefono.Sin descripcion para telefono 
		/// </summary>
		/// <value>
		/// telefono 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string telefono
		{
			get
			{
				return ObtenerValorPropiedad<string>("telefono");
			}

			set
			{
				EstablecerValorPropiedad<string>("telefono", value);
			}

		}
		/// <summary>
		/// Obtiene o establece telefono_emergencia.Sin descripcion para telefono_emergencia 
		/// </summary>
		/// <value>
		/// telefono_emergencia 
		/// </value>
		[MaxLength (10)]
		[DefaultValue(null)]
		public string telefono_emergencia
		{
			get
			{
				return ObtenerValorPropiedad<string>("telefono_emergencia");
			}

			set
			{
				EstablecerValorPropiedad<string>("telefono_emergencia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece NSS.Sin descripcion para NSS 
		/// </summary>
		/// <value>
		/// NSS 
		/// </value>
		[MaxLength (11)]
		[DefaultValue(null)]
		public string NSS
		{
			get
			{
				return ObtenerValorPropiedad<string>("NSS");
			}

			set
			{
				EstablecerValorPropiedad<string>("NSS", value);
			}

		}
		/// <summary>
		/// Obtiene o establece genero.Sin descripcion para genero 
		/// </summary>
		/// <value>
		/// genero 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string genero
		{
			get
			{
				return ObtenerValorPropiedad<string>("genero");
			}

			set
			{
				EstablecerValorPropiedad<string>("genero", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_nacimiento.Sin descripcion para fecha_nacimiento 
		/// </summary>
		/// <value>
		/// fecha_nacimiento 
		/// </value>
		[MaxLength (3)]
		[DefaultValue(null)]
		public string fecha_nacimiento
		{
			get
			{
				return ObtenerValorPropiedad<string>("fecha_nacimiento");
			}

			set
			{
				EstablecerValorPropiedad<string>("fecha_nacimiento", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estado_nacimiento.Sin descripcion para estado_nacimiento 
		/// </summary>
		/// <value>
		/// estado_nacimiento 
		/// </value>
		[MaxLength (25)]
		[DefaultValue(null)]
		public string estado_nacimiento
		{
			get
			{
				return ObtenerValorPropiedad<string>("estado_nacimiento");
			}

			set
			{
				EstablecerValorPropiedad<string>("estado_nacimiento", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla personal_personale.		/// </summary>
		/// <param name="psrfc">rfc</param>
		public void Cargar(string psrfc)
		{
			base.Cargar(psrfc);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(15);

			AgregaCampoLlave("rfc",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para rfc";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 18;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para curp";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("curp", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para estado_civil";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estado_civil", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 2;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para nacionalidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nacionalidad", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para nivel_estudios";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nivel_estudios", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 250;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para nombre_carrera";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_carrera", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para fecha_titulacion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("fecha_titulacion", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 15;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para cedula_profesional";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("cedula_profesional", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para correo_electronico";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("correo_electronico", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "Sin descripcion para telefono";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("telefono", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 12;
			loColstring.Descripcion = "Sin descripcion para telefono_emergencia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("telefono_emergencia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 11;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 13;
			loColstring.Descripcion = "Sin descripcion para NSS";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("NSS", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 14;
			loColstring.Descripcion = "Sin descripcion para genero";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("genero", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 15;
			loColstring.Descripcion = "Sin descripcion para fecha_nacimiento";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("fecha_nacimiento", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 25;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 16;
			loColstring.Descripcion = "Sin descripcion para estado_nacimiento";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estado_nacimiento", loColstring); 
			}
			#endregion

		}
	}
