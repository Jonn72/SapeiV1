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
	/// Clase historia_alumno_ingle generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Historia_alumno_ingles:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase historia_alumno_ingle.
		/// </summary>
		public Historia_alumno_ingles():base()
		{
			NombreTabla = "historia_alumno_ingles";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase historia_alumno_ingle.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Historia_alumno_ingles(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "historia_alumno_ingles";
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
		/// Obtiene o establece promedio.Sin descripcion para promedio 
		/// </summary>
		/// <value>
		/// promedio 
		/// </value>
		[Required]
		public Int32 promedio
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("promedio");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("promedio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece asistencia.Sin descripcion para asistencia 
		/// </summary>
		/// <value>
		/// asistencia 
		/// </value>
		[Required]
		public Int32 asistencia
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("asistencia");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("asistencia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece speaking.Sin descripcion para speaking 
		/// </summary>
		/// <value>
		/// speaking 
		/// </value>
		[Required]
		public Int32 speaking
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("speaking");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("speaking", value);
			}

		}
		/// <summary>
		/// Obtiene o establece writing.Sin descripcion para writing 
		/// </summary>
		/// <value>
		/// writing 
		/// </value>
		[Required]
		public Int32 writing
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("writing");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("writing", value);
			}

		}
		/// <summary>
		/// Obtiene o establece listening.Sin descripcion para listening 
		/// </summary>
		/// <value>
		/// listening 
		/// </value>
		[Required]
		public Int32 listening
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("listening");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("listening", value);
			}

		}
		/// <summary>
		/// Obtiene o establece reading.Sin descripcion para reading 
		/// </summary>
		/// <value>
		/// reading 
		/// </value>
		[Required]
		public Int32 reading
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("reading");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("reading", value);
			}

		}
		/// <summary>
		/// Obtiene o establece colocacion.Sin descripcion para colocacion 
		/// </summary>
		/// <value>
		/// colocacion 
		/// </value>
		[Required]
		public Boolean colocacion
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("colocacion");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("colocacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_calificacion.Sin descripcion para fecha_calificacion 
		/// </summary>
		/// <value>
		/// fecha_calificacion 
		/// </value>
		[Required]
		public DateTime fecha_calificacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_calificacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_calificacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece usuario.Sin descripcion para usuario 
		/// </summary>
		/// <value>
		/// usuario 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string usuario
		{
			get
			{
				return ObtenerValorPropiedad<string>("usuario");
			}

			set
			{
				EstablecerValorPropiedad<string>("usuario", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla historia_alumno_ingle.		/// </summary>
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
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<Boolean> loColBoolean; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(3);
			Propiedades = new Dictionary<string, Propiedad>(12);

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

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "Sin descripcion para promedio";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("promedio", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 4;
			loColInt32.Descripcion = "Sin descripcion para asistencia";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("asistencia", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 5;
			loColInt32.Descripcion = "Sin descripcion para speaking";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("speaking", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 6;
			loColInt32.Descripcion = "Sin descripcion para writing";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("writing", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 7;
			loColInt32.Descripcion = "Sin descripcion para listening";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("listening", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 8;
			loColInt32.Descripcion = "Sin descripcion para reading";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("reading", loColInt32); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 9;
			loColBoolean.Descripcion = "Sin descripcion para colocacion";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("colocacion", loColBoolean); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 10;
			loColDateTime.Descripcion = "Sin descripcion para fecha_calificacion";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_calificacion", loColDateTime); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "Sin descripcion para usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring); 
			}
			#endregion

		}
	}
