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
	/// Clase alumnos_encuesta generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Alumnos_Encuesta:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_encuesta.
		/// </summary>
		public Alumnos_Encuesta():base()
		{
			NombreTabla = "alumnos_encuestas";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_encuesta.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Alumnos_Encuesta(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "alumnos_encuestas";
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
		/// Obtiene o establece id_encuesta.Sin descripcion para id_encuesta 
		/// </summary>
		/// <value>
		/// id_encuesta 
		/// </value>
		[Required]
		public Int16 id_encuesta
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("id_encuesta");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("id_encuesta", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_pregunta.Sin descripcion para id_pregunta 
		/// </summary>
		/// <value>
		/// id_pregunta 
		/// </value>
		[Required]
		public Int16 id_pregunta
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("id_pregunta");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("id_pregunta", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_respuesta.Sin descripcion para id_respuesta 
		/// </summary>
		/// <value>
		/// id_respuesta 
		/// </value>
		[Required]
		[MaxLength (250)]
		[DefaultValue(null)]
		public string id_respuesta
		{
			get
			{
				return ObtenerValorPropiedad<string>("id_respuesta");
			}

			set
			{
				EstablecerValorPropiedad<string>("id_respuesta", value);
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla alumnos_encuesta.		/// </summary>
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
			 PropiedadesColumna<Int16> loColInt16; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(0);
			Propiedades = new Dictionary<string, Propiedad>(6);


			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
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
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 2;
			loColInt16.Descripcion = "Sin descripcion para id_encuesta";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("id_encuesta", loColInt16); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 3;
			loColInt16.Descripcion = "Sin descripcion para id_pregunta";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("id_pregunta", loColInt16); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 250;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para id_respuesta";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("id_respuesta", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 5;
			loColDateTime.Descripcion = "Sin descripcion para fecha";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha", loColDateTime); 
			}
			#endregion

		}
	}
