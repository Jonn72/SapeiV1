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
	/// Clase dep_fechas_carrera generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Dep_Fechas_Carreras_Seleccion:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase dep_fechas_carrera.
		/// </summary>
		public Dep_Fechas_Carreras_Seleccion():base()
		{
			NombreTabla = "dep_fechas_carreras_seleccion";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase dep_fechas_carrera.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Dep_Fechas_Carreras_Seleccion(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "dep_fechas_carreras_seleccion";
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
		/// Obtiene o establece carrera.Sin descripcion para carrera 
		/// </summary>
		/// <value>
		/// carrera 
		/// </value>
		[Key]
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string carrera
		{
			get
			{
				return ObtenerValorPropiedad<string>("carrera");
			}

			set
			{
				EstablecerValorPropiedad<string>("carrera", value);
			}

		}
	
		/// <summary>
		/// Obtiene o establece fecha_inicio.Sin descripcion para fecha_inicio 
		/// </summary>
		/// <value>
		/// fecha_inicio 
		/// </value>
		[Required]
		[MaxLength (15)]
		[DefaultValue(null)]
		public DateTime fecha_inicio
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_inicio");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_inicio", value);
			}

		}

		/// <summary>
		/// Obtiene o establece intervalo.Sin descripcion para intervalo 
		/// </summary>
		/// <value>
		/// intervalo 
		/// </value>
		[Required]
		public Int32 intervalo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("intervalo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("intervalo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece personas.Sin descripcion para personas 
		/// </summary>
		/// <value>
		/// personas 
		/// </value>
		[Required]
		public Int32 personas
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("personas");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("personas", value);
			}

		}
		/// <summary>
		/// Obtiene o establece personas.Sin descripcion para personas 
		/// </summary>
		/// <value>
		/// personas 
		/// </value>
		[Required]
		public bool generada
		{
			get
			{
				return ObtenerValorPropiedad<bool>("generada");
			}

			set
			{
				EstablecerValorPropiedad<bool>("generada", value);
			}

		}
		/// <summary>
		/// Obtiene o establece personas.Sin descripcion para personas 
		/// </summary>
		/// <value>
		/// personas 
		/// </value>
		[Required]
		public bool publicada
		{
			get
			{
				return ObtenerValorPropiedad<bool>("publicada");
			}

			set
			{
				EstablecerValorPropiedad<bool>("publicada", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla dep_fechas_carrera.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="pscarrera">carrera</param>
		public void Cargar(string psperiodo,string pscarrera)
		{
			base.Cargar(psperiodo,pscarrera);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			PropiedadesColumna<string> loColstring;
			PropiedadesColumna<Int32> loColInt32;
			PropiedadesColumna<Boolean> loColBool;
			PropiedadesColumna<DateTime> loColDateTime;
			if (!Object.Equals(CamposLlave, null) && !Object.Equals(Propiedades, null))
				return;

			CamposLlave = new Dictionary<string, object>(2);
			Propiedades = new Dictionary<string, Propiedad>(7);

			AgregaCampoLlave("periodo", null);
			AgregaCampoLlave("carrera", null);

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
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para carrera";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("carrera", loColstring);

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 15;
			loColDateTime.IncluyeHoras = true;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
			loColDateTime.Descripcion = "Sin descripcion para fecha_inicio";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_inicio", loColDateTime);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "Sin descripcion para intervalo";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("intervalo", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 4;
			loColInt32.Descripcion = "Sin descripcion para personas";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("personas", loColInt32);

			loColBool = new PropiedadesColumna<Boolean>();
			loColBool.EsPrimaryKey = false;
			loColBool.EsRequeridoBD = true;
			loColBool.CampoId = 5;
			loColBool.Descripcion = "Sin descripcion para personas";
			loColBool.EsIdentity = false;
			loColBool.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("generada", loColBool);

			loColBool = new PropiedadesColumna<Boolean>();
			loColBool.EsPrimaryKey = false;
			loColBool.EsRequeridoBD = true;
			loColBool.CampoId = 6;
			loColBool.Descripcion = "Sin descripcion para personas";
			loColBool.EsIdentity = false;
			loColBool.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("publicada", loColBool);
		}
			#endregion

		}
	}
