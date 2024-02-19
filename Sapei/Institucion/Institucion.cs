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
	/// Clase Institucion generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Institucion:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase Institucion.
		/// </summary>
		public Institucion():base()
		{
			NombreTabla = "institucion";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase Institucion.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Institucion(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "institucion";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece IdInstitucion.IdInstitucion 
		/// </summary>
		/// <value>
		/// IdInstitucion 
		/// </value>
		[Key]
		[Required]
		public Int16 IdInstitucion
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("IdInstitucion");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("IdInstitucion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Nombre.Nombre 
		/// </summary>
		/// <value>
		/// Nombre 
		/// </value>
		[MaxLength (70)]
		[DefaultValue(null)]
		public string Nombre
		{
			get
			{
				return ObtenerValorPropiedad<string>("Nombre");
			}

			set
			{
				EstablecerValorPropiedad<string>("Nombre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece RFC.RFC 
		/// </summary>
		/// <value>
		/// RFC 
		/// </value>
		[MaxLength (13)]
		[DefaultValue(null)]
		public string RFC
		{
			get
			{
				return ObtenerValorPropiedad<string>("RFC");
			}

			set
			{
				EstablecerValorPropiedad<string>("RFC", value);
			}

		}
		/// <summary>
		/// Obtiene o establece RazonSocial.RazonSocial 
		/// </summary>
		/// <value>
		/// RazonSocial 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string RazonSocial
		{
			get
			{
				return ObtenerValorPropiedad<string>("RazonSocial");
			}

			set
			{
				EstablecerValorPropiedad<string>("RazonSocial", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Tel1.Tel1 
		/// </summary>
		/// <value>
		/// Tel1 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string Tel1
		{
			get
			{
				return ObtenerValorPropiedad<string>("Tel1");
			}

			set
			{
				EstablecerValorPropiedad<string>("Tel1", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Tel2.Tel2 
		/// </summary>
		/// <value>
		/// Tel2 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string Tel2
		{
			get
			{
				return ObtenerValorPropiedad<string>("Tel2");
			}

			set
			{
				EstablecerValorPropiedad<string>("Tel2", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Logo.Logo 
		/// </summary>
		/// <value>
		/// Logo 
		/// </value>
		[DefaultValue(null)]
		public Byte[] Logo
		{
			get
			{
				return ObtenerValorPropiedad<Byte[]>("Logo");
			}

			set
			{
				EstablecerValorPropiedad<Byte[]>("Logo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Calle.Calle 
		/// </summary>
		/// <value>
		/// Calle 
		/// </value>
		[Required]
		[MaxLength (100)]
		[DefaultValue("")]
		public string Calle
		{
			get
			{
				return ObtenerValorPropiedad<string>("Calle");
			}

			set
			{
				EstablecerValorPropiedad<string>("Calle", value);
			}

		}
		/// <summary>
		/// Obtiene o establece NoExterior.NoExterior 
		/// </summary>
		/// <value>
		/// NoExterior 
		/// </value>
		[MaxLength (10)]
		[DefaultValue("")]
		public string NoExterior
		{
			get
			{
				return ObtenerValorPropiedad<string>("NoExterior");
			}

			set
			{
				EstablecerValorPropiedad<string>("NoExterior", value);
			}

		}
		/// <summary>
		/// Obtiene o establece NoInterior.NoInterior 
		/// </summary>
		/// <value>
		/// NoInterior 
		/// </value>
		[MaxLength (10)]
		[DefaultValue("")]
		public string NoInterior
		{
			get
			{
				return ObtenerValorPropiedad<string>("NoInterior");
			}

			set
			{
				EstablecerValorPropiedad<string>("NoInterior", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Colonia.Colonia 
		/// </summary>
		/// <value>
		/// Colonia 
		/// </value>
		[Required]
		[MaxLength (100)]
		[DefaultValue("")]
		public string Colonia
		{
			get
			{
				return ObtenerValorPropiedad<string>("Colonia");
			}

			set
			{
				EstablecerValorPropiedad<string>("Colonia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Municipio.Municipio 
		/// </summary>
		/// <value>
		/// Municipio 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue("")]
		public string Municipio
		{
			get
			{
				return ObtenerValorPropiedad<string>("Municipio");
			}

			set
			{
				EstablecerValorPropiedad<string>("Municipio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece CodPostal.CodPostal 
		/// </summary>
		/// <value>
		/// CodPostal 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue("")]
		public string CodPostal
		{
			get
			{
				return ObtenerValorPropiedad<string>("CodPostal");
			}

			set
			{
				EstablecerValorPropiedad<string>("CodPostal", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Estado.Estado 
		/// </summary>
		/// <value>
		/// Estado 
		/// </value>
		[MaxLength (30)]
		[DefaultValue("")]
		public string Estado
		{
			get
			{
				return ObtenerValorPropiedad<string>("Estado");
			}

			set
			{
				EstablecerValorPropiedad<string>("Estado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Pais.Pais 
		/// </summary>
		/// <value>
		/// Pais 
		/// </value>
		[Required]
		[MaxLength (30)]
		[DefaultValue("México")]
		public string Pais
		{
			get
			{
				return ObtenerValorPropiedad<string>("Pais");
			}

			set
			{
				EstablecerValorPropiedad<string>("Pais", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla Institucion.		/// </summary>
		/// <param name="piIdInstitucion">IdInstitucion</param>
		public void Cargar(Int16 piIdInstitucion)
		{
			base.Cargar(piIdInstitucion);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int16> loColInt16; 
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Byte[]> loColByteA; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(15);

			AgregaCampoLlave("IdInstitucion",null);

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = true;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 0;
			loColInt16.Descripcion = "IdInstitucion";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("IdInstitucion", loColInt16); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 70;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Nombre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("Nombre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "RFC";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("RFC", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "RazonSocial";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("RazonSocial", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Tel1";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("Tel1", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Tel2";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("Tel2", loColstring); 

			loColByteA = new PropiedadesColumna<Byte[]>();
			loColByteA.Valor = null;
			loColByteA.EsPrimaryKey = false;
			loColByteA.Longitud = 16;
			loColByteA.Precision = 0;
			loColByteA.EsRequeridoBD = false;
			loColByteA.CampoId = 6;
			loColByteA.Descripcion = "Logo";
			loColByteA.EsIdentity = false;
			loColByteA.Tipo = typeof(Byte[]);
			AgregarPropiedad<Byte[]>("Logo", loColByteA); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = "";
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Calle";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("Calle", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = "";
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "NoExterior";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("NoExterior", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = "";
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "NoInterior";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("NoInterior", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = "";
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Colonia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("Colonia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = "";
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "Municipio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("Municipio", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = "";
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 12;
			loColstring.Descripcion = "CodPostal";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("CodPostal", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = "";
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 13;
			loColstring.Descripcion = "Estado";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("Estado", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = "México";
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 14;
			loColstring.Descripcion = "Pais";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("Pais", loColstring); 
			}
			#endregion

		}
	}
