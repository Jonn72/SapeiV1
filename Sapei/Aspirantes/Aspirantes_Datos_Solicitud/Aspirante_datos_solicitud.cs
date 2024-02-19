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
	/// Clase aspirantes_datos_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Aspirante_datos_solicitud:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase aspirantes_datos_solicitud.
		/// </summary>
		public Aspirante_datos_solicitud():base()
		{
			NombreTabla = "aspirantes_datos_solicitud";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase aspirantes_datos_solicitud.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Aspirante_datos_solicitud(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "aspirantes_datos_solicitud";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece folio.folio 
		/// </summary>
		/// <value>
		/// folio 
		/// </value>
		[Key]
		[Required]
		[MaxLength (8)]
		[DefaultValue(null)]
		public string folio
		{
			get
			{
				return ObtenerValorPropiedad<string>("folio");
			}

			set
			{
				EstablecerValorPropiedad<string>("folio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece enteraste.enteraste 
		/// </summary>
		/// <value>
		/// enteraste 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string enteraste
		{
			get
			{
				return ObtenerValorPropiedad<string>("enteraste");
			}

			set
			{
				EstablecerValorPropiedad<string>("enteraste", value);
			}

		}
		/// <summary>
		/// Obtiene o establece carrera1.carrera1 
		/// </summary>
		/// <value>
		/// carrera1 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string carrera1
		{
			get
			{
				return ObtenerValorPropiedad<string>("carrera1");
			}

			set
			{
				EstablecerValorPropiedad<string>("carrera1", value);
			}

		}
		/// <summary>
		/// Obtiene o establece carrera2.carrera2 
		/// </summary>
		/// <value>
		/// carrera2 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string carrera2
		{
			get
			{
				return ObtenerValorPropiedad<string>("carrera2");
			}

			set
			{
				EstablecerValorPropiedad<string>("carrera2", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla aspirantes_datos_solicitud.		/// </summary>
		/// <param name="psfolio">folio</param>
		public void Cargar(string psfolio)
		{
			base.Cargar(psfolio);
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
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("folio",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 8;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "folio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("folio", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "enteraste";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("enteraste", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "carrera1";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("carrera1", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "carrera2";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("carrera2", loColstring); 
			}
			#endregion

		}
	}
