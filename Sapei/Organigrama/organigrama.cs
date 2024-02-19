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
	/// Clase organigrama generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class organigrama:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase organigrama.
		/// </summary>
		public organigrama():base()
		{
			NombreTabla = "organigrama";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase organigrama.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public organigrama(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "organigrama";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece clave_area.Sin descripcion para clave_area 
		/// </summary>
		/// <value>
		/// clave_area 
		/// </value>
		[Key]
		[Required]
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
		/// <summary>
		/// Obtiene o establece descripcion_area.Sin descripcion para descripcion_area 
		/// </summary>
		/// <value>
		/// descripcion_area 
		/// </value>
		[MaxLength (200)]
		[DefaultValue(null)]
		public string descripcion_area
		{
			get
			{
				return ObtenerValorPropiedad<string>("descripcion_area");
			}

			set
			{
				EstablecerValorPropiedad<string>("descripcion_area", value);
			}

		}
		/// <summary>
		/// Obtiene o establece area_depende.Sin descripcion para area_depende 
		/// </summary>
		/// <value>
		/// area_depende 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string area_depende
		{
			get
			{
				return ObtenerValorPropiedad<string>("area_depende");
			}

			set
			{
				EstablecerValorPropiedad<string>("area_depende", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nivel.Sin descripcion para nivel 
		/// </summary>
		/// <value>
		/// nivel 
		/// </value>
		[Required]
		[MaxLength (1)]
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
		/// Obtiene o establece tipo_area.Sin descripcion para tipo_area 
		/// </summary>
		/// <value>
		/// tipo_area 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string tipo_area
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_area");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_area", value);
			}

		}
		/// <summary>
		/// Obtiene o establece extension.Sin descripcion para extension 
		/// </summary>
		/// <value>
		/// extension 
		/// </value>
		[MaxLength (3)]
		[DefaultValue(null)]
		public string extension
		{
			get
			{
				return ObtenerValorPropiedad<string>("extension");
			}

			set
			{
				EstablecerValorPropiedad<string>("extension", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla organigrama.		/// </summary>
		/// <param name="psclave_area">clave_area</param>
		public void Cargar(string psclave_area)
		{
			base.Cargar(psclave_area);
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
			Propiedades = new Dictionary<string, Propiedad>(6);

			AgregaCampoLlave("clave_area",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para clave_area";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_area", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 200;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para descripcion_area";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion_area", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para area_depende";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("area_depende", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para nivel";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nivel", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para tipo_area";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_area", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "Sin descripcion para extension";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("extension", loColstring); 
			}
			#endregion

		}
	}
