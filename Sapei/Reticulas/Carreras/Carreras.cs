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
	/// Clase carrera generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Carreras:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase carrera.
		/// </summary>
		public Carreras():base()
		{
			NombreTabla = "carreras";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase carrera.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Carreras(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "carreras";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
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
		/// Obtiene o establece reticula.Sin descripcion para reticula 
		/// </summary>
		/// <value>
		/// reticula 
		/// </value>
		[Key]
		[Required]
		public Int32 reticula
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("reticula");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("reticula", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nivel_escolar.Sin descripcion para nivel_escolar 
		/// </summary>
		/// <value>
		/// nivel_escolar 
		/// </value>
		[Required]
		[MaxLength (1)]
		[DefaultValue(null)]
		public string nivel_escolar
		{
			get
			{
				return ObtenerValorPropiedad<string>("nivel_escolar");
			}

			set
			{
				EstablecerValorPropiedad<string>("nivel_escolar", value);
			}

		}
		/// <summary>
		/// Obtiene o establece clave_oficial.Sin descripcion para clave_oficial 
		/// </summary>
		/// <value>
		/// clave_oficial 
		/// </value>
		[Required]
		[MaxLength (20)]
		[DefaultValue(null)]
		public string clave_oficial
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave_oficial");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave_oficial", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_carrera.Sin descripcion para nombre_carrera 
		/// </summary>
		/// <value>
		/// nombre_carrera 
		/// </value>
		[Required]
		[MaxLength (80)]
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
		/// Obtiene o establece nombre_reducido.Sin descripcion para nombre_reducido 
		/// </summary>
		/// <value>
		/// nombre_reducido 
		/// </value>
		[Required]
		[MaxLength (30)]
		[DefaultValue(null)]
		public string nombre_reducido
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_reducido");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_reducido", value);
			}

		}
		/// <summary>
		/// Obtiene o establece siglas.Sin descripcion para siglas 
		/// </summary>
		/// <value>
		/// siglas 
		/// </value>
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public string siglas
		{
			get
			{
				return ObtenerValorPropiedad<string>("siglas");
			}

			set
			{
				EstablecerValorPropiedad<string>("siglas", value);
			}

		}
		/// <summary>
		/// Obtiene o establece carga_maxima.Sin descripcion para carga_maxima 
		/// </summary>
		/// <value>
		/// carga_maxima 
		/// </value>
		[Required]
		public Int32 carga_maxima
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("carga_maxima");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("carga_maxima", value);
			}

		}
		/// <summary>
		/// Obtiene o establece carga_minima.Sin descripcion para carga_minima 
		/// </summary>
		/// <value>
		/// carga_minima 
		/// </value>
		[Required]
		public Int32 carga_minima
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("carga_minima");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("carga_minima", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_inicio.Sin descripcion para fecha_inicio 
		/// </summary>
		/// <value>
		/// fecha_inicio 
		/// </value>
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string fecha_inicio
		{
			get
			{
				return ObtenerValorPropiedad<string>("fecha_inicio");
			}

			set
			{
				EstablecerValorPropiedad<string>("fecha_inicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_termino.Sin descripcion para fecha_termino 
		/// </summary>
		/// <value>
		/// fecha_termino 
		/// </value>
		[MaxLength (3)]
		[DefaultValue(null)]
		public string fecha_termino
		{
			get
			{
				return ObtenerValorPropiedad<string>("fecha_termino");
			}

			set
			{
				EstablecerValorPropiedad<string>("fecha_termino", value);
			}

		}
		/// <summary>
		/// Obtiene o establece clave_cosnet.Sin descripcion para clave_cosnet 
		/// </summary>
		/// <value>
		/// clave_cosnet 
		/// </value>
		[MaxLength (2)]
		[DefaultValue(null)]
		public string clave_cosnet
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave_cosnet");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave_cosnet", value);
			}

		}
		/// <summary>
		/// Obtiene o establece creditos_totales.Sin descripcion para creditos_totales 
		/// </summary>
		/// <value>
		/// creditos_totales 
		/// </value>
		[DefaultValue(null)]
		public Int32? creditos_totales
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("creditos_totales");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("creditos_totales", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_area_carr.Sin descripcion para id_area_carr 
		/// </summary>
		/// <value>
		/// id_area_carr 
		/// </value>
		[MaxLength (25)]
		[DefaultValue(null)]
		public string id_area_carr
		{
			get
			{
				return ObtenerValorPropiedad<string>("id_area_carr");
			}

			set
			{
				EstablecerValorPropiedad<string>("id_area_carr", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_sub_area_carr.Sin descripcion para id_sub_area_carr 
		/// </summary>
		/// <value>
		/// id_sub_area_carr 
		/// </value>
		[MaxLength (25)]
		[DefaultValue(null)]
		public string id_sub_area_carr
		{
			get
			{
				return ObtenerValorPropiedad<string>("id_sub_area_carr");
			}

			set
			{
				EstablecerValorPropiedad<string>("id_sub_area_carr", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_nivel_carr.Sin descripcion para id_nivel_carr 
		/// </summary>
		/// <value>
		/// id_nivel_carr 
		/// </value>
		[MaxLength (25)]
		[DefaultValue(null)]
		public string id_nivel_carr
		{
			get
			{
				return ObtenerValorPropiedad<string>("id_nivel_carr");
			}

			set
			{
				EstablecerValorPropiedad<string>("id_nivel_carr", value);
			}

		}
		/// <summary>
		/// Obtiene o establece consecutivo_carrera.Sin descripcion para consecutivo_carrera 
		/// </summary>
		/// <value>
		/// consecutivo_carrera 
		/// </value>
		[MaxLength (25)]
		[DefaultValue(null)]
		public string consecutivo_carrera
		{
			get
			{
				return ObtenerValorPropiedad<string>("consecutivo_carrera");
			}

			set
			{
				EstablecerValorPropiedad<string>("consecutivo_carrera", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nivel.Sin descripcion para nivel 
		/// </summary>
		/// <value>
		/// nivel 
		/// </value>
		[MaxLength (25)]
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
		/// Obtiene o establece clave.Sin descripcion para clave 
		/// </summary>
		/// <value>
		/// clave 
		/// </value>
		[MaxLength (25)]
		[DefaultValue(null)]
		public string clave
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave", value);
			}

		}
		/// <summary>
		/// Obtiene o establece modalidad.Sin descripcion para modalidad 
		/// </summary>
		/// <value>
		/// modalidad 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string modalidad
		{
			get
			{
				return ObtenerValorPropiedad<string>("modalidad");
			}

			set
			{
				EstablecerValorPropiedad<string>("modalidad", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla carrera.		/// </summary>
		/// <param name="pscarrera">carrera</param>
		/// <param name="pireticula">reticula</param>
		public void Cargar(string pscarrera,Int32 pireticula)
		{
			base.Cargar(pscarrera,pireticula);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<Int32?> loColInt32N; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(2);
			Propiedades = new Dictionary<string, Propiedad>(20);

			AgregaCampoLlave("carrera",null);
			AgregaCampoLlave("reticula",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para carrera";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("carrera", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 1;
			loColInt32.Descripcion = "Sin descripcion para reticula";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("reticula", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para nivel_escolar";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nivel_escolar", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 20;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para clave_oficial";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_oficial", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 80;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para nombre_carrera";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_carrera", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para nombre_reducido";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_reducido", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para siglas";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("siglas", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 7;
			loColInt32.Descripcion = "Sin descripcion para carga_maxima";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("carga_maxima", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 8;
			loColInt32.Descripcion = "Sin descripcion para carga_minima";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("carga_minima", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 15;
			loColstring.Precision = 10;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "Sin descripcion para fecha_inicio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("fecha_inicio", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 15;
			loColstring.Precision = 10;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Sin descripcion para fecha_termino";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("fecha_termino", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 2;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "Sin descripcion para clave_cosnet";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_cosnet", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 12;
			loColInt32N.Descripcion = "Sin descripcion para creditos_totales";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("creditos_totales", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 25;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 13;
			loColstring.Descripcion = "Sin descripcion para id_area_carr";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("id_area_carr", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 25;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 14;
			loColstring.Descripcion = "Sin descripcion para id_sub_area_carr";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("id_sub_area_carr", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 25;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 15;
			loColstring.Descripcion = "Sin descripcion para id_nivel_carr";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("id_nivel_carr", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 25;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 16;
			loColstring.Descripcion = "Sin descripcion para consecutivo_carrera";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("consecutivo_carrera", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 25;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 17;
			loColstring.Descripcion = "Sin descripcion para nivel";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nivel", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 25;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 18;
			loColstring.Descripcion = "Sin descripcion para clave";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 19;
			loColstring.Descripcion = "Sin descripcion para modalidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("modalidad", loColstring); 
			}
			#endregion

		}
	}
