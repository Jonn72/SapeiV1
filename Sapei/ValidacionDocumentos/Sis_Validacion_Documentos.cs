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
	/// Clase sis_validacion_documento generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Sis_Validacion_Documentos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase sis_validacion_documento.
		/// </summary>
		public Sis_Validacion_Documentos():base()
		{
			NombreTabla = "sis_validacion_documentos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase sis_validacion_documento.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Sis_Validacion_Documentos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "sis_validacion_documentos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
		/// </summary>
		/// <value>
		/// no_de_control 
		/// </value>
		[Required]
		[MaxLength (20)]
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
		/// Obtiene o establece cadena.Sin descripcion para cadena 
		/// </summary>
		/// <value>
		/// cadena 
		/// </value>
		[Required]
		[DefaultValue(null)]
		public string cadena
		{
			get
			{
				return ObtenerValorPropiedad<string>("cadena");
			}

			set
			{
				EstablecerValorPropiedad<string>("cadena", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha.Sin descripcion para fecha 
		/// </summary>
		/// <value>
		/// fecha 
		/// </value>
		[Required]
		public DateTime fecha
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha", value);
			}

		}
		/// <summary>
		/// Obtiene o establece validaciones.Sin descripcion para validaciones 
		/// </summary>
		/// <value>
		/// validaciones 
		/// </value>
		[Required]
		public Int16 validaciones
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("validaciones");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("validaciones", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla sis_validacion_documento.		/// </summary>
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
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<Int16> loColInt16; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(0);
			Propiedades = new Dictionary<string, Propiedad>(4);


			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 20;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = -1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para cadena";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("cadena", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
			loColDateTime.Descripcion = "Sin descripcion para fecha";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha", loColDateTime); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 3;
			loColInt16.Descripcion = "Sin descripcion para validaciones";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("validaciones", loColInt16); 
			}
			#endregion

		}
	}
