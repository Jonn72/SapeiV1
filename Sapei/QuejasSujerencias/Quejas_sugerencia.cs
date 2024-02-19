using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using SII.Framework;
using SII.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SII
{
	/// <summary>
	/// Clase quejas_sugerencia generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Quejas_sugerencias:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase quejas_sugerencia.
		/// </summary>
		public Quejas_sugerencias():base()
		{
			NombreTabla = "quejas_sugerencias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase quejas_sugerencia.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Quejas_sugerencias(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "quejas_sugerencias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece periodo.periodo 
		/// </summary>
		/// <value>
		/// periodo 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string periodo
		{
			get
			{
				return ObtenerValorPropiedad<string>("periodo");
			}

			set
			{
				EstablecerValorPropiedad<string>("periodo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece no_de_control.no_de_control 
		/// </summary>
		/// <value>
		/// no_de_control 
		/// </value>
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public string no_de_control
		{
			get
			{
				return ObtenerValorPropiedad<string>("no_de_control");
			}

			set
			{
				EstablecerValorPropiedad<string>("no_de_control", value);
			}

		}
		/// <summary>
		/// Obtiene o establece mensaje.mensaje 
		/// </summary>
		/// <value>
		/// mensaje 
		/// </value>
		[MaxLength (16)]
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
		/// <summary>
		/// Obtiene o establece fecha.fecha 
		/// </summary>
		/// <value>
		/// fecha 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha", value);
			}

		}
		/// <summary>
		/// Obtiene o establece no.no 
		/// </summary>
		/// <value>
		/// no 
		/// </value>
		[DefaultValue(null)]
		public Int32? no
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("no");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("no", value);
			}

		}
		/// <summary>
		/// Obtiene o establece titulo.titulo 
		/// </summary>
		/// <value>
		/// titulo 
		/// </value>
		[MaxLength (255)]
		[DefaultValue(null)]
		public string titulo
		{
			get
			{
				return ObtenerValorPropiedad<string>("titulo");
			}

			set
			{
				EstablecerValorPropiedad<string>("titulo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece clave_area.clave_area 
		/// </summary>
		/// <value>
		/// clave_area 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string clave_area
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave_area");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave_area", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla quejas_sugerencia.		/// </summary>
		public void Cargar()
		{
			base.Cargar();
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<DateTime?> loColDateTimeN; 
			 PropiedadesColumna<Int32?> loColInt32N; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(0);
			Propiedades = new Dictionary<string, Propiedad>(7);


			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 16;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "mensaje";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("mensaje", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 3;
			loColDateTimeN.Descripcion = "fecha";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha", loColDateTimeN); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 4;
			loColInt32N.Descripcion = "no";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("no", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 255;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "titulo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("titulo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "clave_area";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_area", loColstring); 
			}
			#endregion

		}
	}
