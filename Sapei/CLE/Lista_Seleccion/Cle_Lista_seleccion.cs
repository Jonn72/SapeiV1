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
	/// Clase cle_lista_seleccion generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Cle_Lista_Seleccion:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase cle_lista_seleccion.
		/// </summary>
		public Cle_Lista_Seleccion():base()
		{
			NombreTabla = "cle_lista_seleccion";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase cle_lista_seleccion.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Cle_Lista_Seleccion(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "cle_lista_seleccion";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece periodo.Sin descripcion para periodo 
		/// </summary>
		/// <value>
		/// periodo 
		/// </value>
		[Key]
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
		/// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
		/// </summary>
		/// <value>
		/// no_de_control 
		/// </value>
		[Key]
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
		/// Obtiene o establece nivel.Sin descripcion para nivel 
		/// </summary>
		/// <value>
		/// nivel 
		/// </value>
		[Key]
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string nivel
		{
			get
			{
				return ObtenerValorPropiedad<string>("nivel");
			}

			set
			{
				EstablecerValorPropiedad<string>("nivel", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ini_seleccion.Sin descripcion para ini_seleccion 
		/// </summary>
		/// <value>
		/// ini_seleccion 
		/// </value>
		[Required]
		public DateTime ini_seleccion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("ini_seleccion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("ini_seleccion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_seleccion.Sin descripcion para fin_seleccion 
		/// </summary>
		/// <value>
		/// fin_seleccion 
		/// </value>
		[Required]
		public DateTime fin_seleccion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_seleccion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_seleccion", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla cle_lista_seleccion.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="psno_de_control">no_de_control</param>
		/// <param name="psnivel">nivel</param>
		public void Cargar(string psperiodo,string psno_de_control,string psnivel)
		{
			base.Cargar(psperiodo,psno_de_control,psnivel);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(3);
			Propiedades = new Dictionary<string, Propiedad>(5);

			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("no_de_control",null);
			AgregaCampoLlave("nivel",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para nivel";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nivel", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 3;
			loColDateTime.Descripcion = "Sin descripcion para ini_seleccion";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("ini_seleccion", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 4;
			loColDateTime.Descripcion = "Sin descripcion para fin_seleccion";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_seleccion", loColDateTime); 
			}
			#endregion

		}
	}
