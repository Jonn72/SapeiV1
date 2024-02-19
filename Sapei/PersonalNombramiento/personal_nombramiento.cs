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
	/// Clase personal_nombramiento generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class personal_nombramiento:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_nombramiento.
		/// </summary>
		public personal_nombramiento():base()
		{
			NombreTabla = "personal_nombramiento";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_nombramiento.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public personal_nombramiento(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "personal_nombramiento";
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
		/// Obtiene o establece nombramiento.Sin descripcion para nombramiento 
		/// </summary>
		/// <value>
		/// nombramiento 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string nombramiento
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombramiento");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombramiento", value);
			}

		}
		/// <summary>
		/// Obtiene o establece horas_nombramiento.Sin descripcion para horas_nombramiento 
		/// </summary>
		/// <value>
		/// horas_nombramiento 
		/// </value>
		[DefaultValue(null)]
		public Int32? horas_nombramiento
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("horas_nombramiento");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("horas_nombramiento", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ingreso_rama.Sin descripcion para ingreso_rama 
		/// </summary>
		/// <value>
		/// ingreso_rama 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string ingreso_rama
		{
			get
			{
				return ObtenerValorPropiedad<string>("ingreso_rama");
			}

			set
			{
				EstablecerValorPropiedad<string>("ingreso_rama", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inicio_gobierno.Sin descripcion para inicio_gobierno 
		/// </summary>
		/// <value>
		/// inicio_gobierno 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string inicio_gobierno
		{
			get
			{
				return ObtenerValorPropiedad<string>("inicio_gobierno");
			}

			set
			{
				EstablecerValorPropiedad<string>("inicio_gobierno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inicio_sep.Sin descripcion para inicio_sep 
		/// </summary>
		/// <value>
		/// inicio_sep 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string inicio_sep
		{
			get
			{
				return ObtenerValorPropiedad<string>("inicio_sep");
			}

			set
			{
				EstablecerValorPropiedad<string>("inicio_sep", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inicio_plantel.Sin descripcion para inicio_plantel 
		/// </summary>
		/// <value>
		/// inicio_plantel 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string inicio_plantel
		{
			get
			{
				return ObtenerValorPropiedad<string>("inicio_plantel");
			}

			set
			{
				EstablecerValorPropiedad<string>("inicio_plantel", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla personal_nombramiento.		/// </summary>
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
			 PropiedadesColumna<Int32?> loColInt32N; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(7);

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
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para nombramiento";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombramiento", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 2;
			loColInt32N.Descripcion = "Sin descripcion para horas_nombramiento";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("horas_nombramiento", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para ingreso_rama";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("ingreso_rama", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para inicio_gobierno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("inicio_gobierno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para inicio_sep";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("inicio_sep", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para inicio_plantel";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("inicio_plantel", loColstring); 
			}
			#endregion

		}
	}
