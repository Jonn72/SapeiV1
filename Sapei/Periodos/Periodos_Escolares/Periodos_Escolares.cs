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
	/// Clase periodos_escolare generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Periodos_Escolares:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase periodos_escolare.
		/// </summary>
		public Periodos_Escolares():base()
		{
			NombreTabla = "periodos_escolares";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase periodos_escolare.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Periodos_Escolares(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "periodos_escolares";
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
		/// Obtiene o establece identificacion_larga.Sin descripcion para identificacion_larga 
		/// </summary>
		/// <value>
		/// identificacion_larga 
		/// </value>
		[Required]
		[MaxLength (30)]
		[DefaultValue(null)]
		public string identificacion_larga
		{
			get
			{
				return ObtenerValorPropiedad<string>("identificacion_larga");
			}

			set
			{
				EstablecerValorPropiedad<string>("identificacion_larga", value);
			}

		}
		/// <summary>
		/// Obtiene o establece identificacion_corta.Sin descripcion para identificacion_corta 
		/// </summary>
		/// <value>
		/// identificacion_corta 
		/// </value>
		[MaxLength (12)]
		[DefaultValue(null)]
		public string identificacion_corta
		{
			get
			{
				return ObtenerValorPropiedad<string>("identificacion_corta");
			}

			set
			{
				EstablecerValorPropiedad<string>("identificacion_corta", value);
			}

		}
		/// <summary>
		/// Obtiene o establece status.Sin descripcion para status 
		/// </summary>
		/// <value>
		/// status 
		/// </value>
		[Required]
		[MaxLength (1)]
		[DefaultValue(null)]
		public string status
		{
			get
			{
				return ObtenerValorPropiedad<string>("status");
			}

			set
			{
				EstablecerValorPropiedad<string>("status", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_inicio.Sin descripcion para fecha_inicio 
		/// </summary>
		/// <value>
		/// fecha_inicio 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_inicio
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_inicio");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_inicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_termino.Sin descripcion para fecha_termino 
		/// </summary>
		/// <value>
		/// fecha_termino 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_termino
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_termino");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_termino", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inicio_vacacional_ss.Sin descripcion para inicio_vacacional_ss 
		/// </summary>
		/// <value>
		/// inicio_vacacional_ss 
		/// </value>
		[DefaultValue(null)]
		public DateTime? inicio_vacacional_ss
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("inicio_vacacional_ss");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("inicio_vacacional_ss", value);
			}

		}
		/// <summary>
		/// Obtiene o establece termino_vacacional_ss.Sin descripcion para termino_vacacional_ss 
		/// </summary>
		/// <value>
		/// termino_vacacional_ss 
		/// </value>
		[DefaultValue(null)]
		public DateTime? termino_vacacional_ss
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("termino_vacacional_ss");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("termino_vacacional_ss", value);
			}

		}
		/// <summary>
		/// Obtiene o establece num_dias_clase.Sin descripcion para num_dias_clase 
		/// </summary>
		/// <value>
		/// num_dias_clase 
		/// </value>
		[DefaultValue(null)]
		public Int32? num_dias_clase
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("num_dias_clase");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("num_dias_clase", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inicio_especial.Sin descripcion para inicio_especial 
		/// </summary>
		/// <value>
		/// inicio_especial 
		/// </value>
		[DefaultValue(null)]
		public DateTime? inicio_especial
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("inicio_especial");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("inicio_especial", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_especial.Sin descripcion para fin_especial 
		/// </summary>
		/// <value>
		/// fin_especial 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fin_especial
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fin_especial");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fin_especial", value);
			}

		}
		/// <summary>
		/// Obtiene o establece cierre_horarios.Sin descripcion para cierre_horarios 
		/// </summary>
		/// <value>
		/// cierre_horarios 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string cierre_horarios
		{
			get
			{
				return ObtenerValorPropiedad<string>("cierre_horarios");
			}

			set
			{
				EstablecerValorPropiedad<string>("cierre_horarios", value);
			}

		}
		/// <summary>
		/// Obtiene o establece cierre_seleccion.Sin descripcion para cierre_seleccion 
		/// </summary>
		/// <value>
		/// cierre_seleccion 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string cierre_seleccion
		{
			get
			{
				return ObtenerValorPropiedad<string>("cierre_seleccion");
			}

			set
			{
				EstablecerValorPropiedad<string>("cierre_seleccion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inicio_enc_estudiantil.Sin descripcion para inicio_enc_estudiantil 
		/// </summary>
		/// <value>
		/// inicio_enc_estudiantil 
		/// </value>
		[DefaultValue(null)]
		public DateTime? inicio_enc_estudiantil
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("inicio_enc_estudiantil");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("inicio_enc_estudiantil", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_enc_estudiantil.Sin descripcion para fin_enc_estudiantil 
		/// </summary>
		/// <value>
		/// fin_enc_estudiantil 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fin_enc_estudiantil
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fin_enc_estudiantil");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fin_enc_estudiantil", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inicio_sele_alumnos.Sin descripcion para inicio_sele_alumnos 
		/// </summary>
		/// <value>
		/// inicio_sele_alumnos 
		/// </value>
		[DefaultValue(null)]
		public DateTime? inicio_sele_alumnos
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("inicio_sele_alumnos");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("inicio_sele_alumnos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_sele_alumnos.Sin descripcion para fin_sele_alumnos 
		/// </summary>
		/// <value>
		/// fin_sele_alumnos 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fin_sele_alumnos
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fin_sele_alumnos");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fin_sele_alumnos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inicio_vacacional.Sin descripcion para inicio_vacacional 
		/// </summary>
		/// <value>
		/// inicio_vacacional 
		/// </value>
		[DefaultValue(null)]
		public DateTime? inicio_vacacional
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("inicio_vacacional");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("inicio_vacacional", value);
			}

		}
		/// <summary>
		/// Obtiene o establece termino_vacacional.Sin descripcion para termino_vacacional 
		/// </summary>
		/// <value>
		/// termino_vacacional 
		/// </value>
		[DefaultValue(null)]
		public DateTime? termino_vacacional
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("termino_vacacional");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("termino_vacacional", value);
			}

		}
		/// <summary>
		/// Obtiene o establece parcial1_inicio.Sin descripcion para parcial1_inicio 
		/// </summary>
		/// <value>
		/// parcial1_inicio 
		/// </value>
		[DefaultValue(null)]
		public DateTime? parcial1_inicio
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("parcial1_inicio");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("parcial1_inicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece parcial1_fin.Sin descripcion para parcial1_fin 
		/// </summary>
		/// <value>
		/// parcial1_fin 
		/// </value>
		[DefaultValue(null)]
		public DateTime? parcial1_fin
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("parcial1_fin");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("parcial1_fin", value);
			}

		}
		/// <summary>
		/// Obtiene o establece parcial2_inicio.Sin descripcion para parcial2_inicio 
		/// </summary>
		/// <value>
		/// parcial2_inicio 
		/// </value>
		[DefaultValue(null)]
		public DateTime? parcial2_inicio
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("parcial2_inicio");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("parcial2_inicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece parcial2_fin.Sin descripcion para parcial2_fin 
		/// </summary>
		/// <value>
		/// parcial2_fin 
		/// </value>
		[DefaultValue(null)]
		public DateTime? parcial2_fin
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("parcial2_fin");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("parcial2_fin", value);
			}

		}
		/// <summary>
		/// Obtiene o establece parcial3_inicio.Sin descripcion para parcial3_inicio 
		/// </summary>
		/// <value>
		/// parcial3_inicio 
		/// </value>
		[DefaultValue(null)]
		public DateTime? parcial3_inicio
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("parcial3_inicio");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("parcial3_inicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece parcial3_fin.Sin descripcion para parcial3_fin 
		/// </summary>
		/// <value>
		/// parcial3_fin 
		/// </value>
		[DefaultValue(null)]
		public DateTime? parcial3_fin
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("parcial3_fin");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("parcial3_fin", value);
			}

		}
		/// <summary>
		/// Obtiene o establece filtro.Sin descripcion para filtro 
		/// </summary>
		/// <value>
		/// filtro 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string filtro
		{
			get
			{
				return ObtenerValorPropiedad<string>("filtro");
			}

			set
			{
				EstablecerValorPropiedad<string>("filtro", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nota_resp.Sin descripcion para nota_resp 
		/// </summary>
		/// <value>
		/// nota_resp 
		/// </value>
		[MaxLength (500)]
		[DefaultValue(null)]
		public string nota_resp
		{
			get
			{
				return ObtenerValorPropiedad<string>("nota_resp");
			}

			set
			{
				EstablecerValorPropiedad<string>("nota_resp", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nota.Sin descripcion para nota 
		/// </summary>
		/// <value>
		/// nota 
		/// </value>
		[MaxLength (16)]
		[DefaultValue(null)]
		public string nota
		{
			get
			{
				return ObtenerValorPropiedad<string>("nota");
			}

			set
			{
				EstablecerValorPropiedad<string>("nota", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inicio_cal_docentes.Sin descripcion para inicio_cal_docentes 
		/// </summary>
		/// <value>
		/// inicio_cal_docentes 
		/// </value>
		[DefaultValue(null)]
		public DateTime? inicio_cal_docentes
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("inicio_cal_docentes");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("inicio_cal_docentes", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_cal_docentes.Sin descripcion para fin_cal_docentes 
		/// </summary>
		/// <value>
		/// fin_cal_docentes 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fin_cal_docentes
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fin_cal_docentes");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fin_cal_docentes", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_ini_periodo.Sin descripcion para fecha_ini_periodo 
		/// </summary>
		/// <value>
		/// fecha_ini_periodo 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_ini_periodo
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_ini_periodo");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_ini_periodo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_fin_periodo.Sin descripcion para fecha_fin_periodo 
		/// </summary>
		/// <value>
		/// fecha_fin_periodo 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_fin_periodo
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_fin_periodo");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_fin_periodo", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla periodos_escolare.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		public void Cargar(string psperiodo)
		{
			base.Cargar(psperiodo);
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

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(32);

			AgregaCampoLlave("periodo",null);

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
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para identificacion_larga";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("identificacion_larga", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 12;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para identificacion_corta";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("identificacion_corta", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para status";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("status", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 4;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_inicio";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_inicio", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 5;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_termino";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_termino", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 6;
			loColDateTimeN.Descripcion = "Sin descripcion para inicio_vacacional_ss";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("inicio_vacacional_ss", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 7;
			loColDateTimeN.Descripcion = "Sin descripcion para termino_vacacional_ss";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("termino_vacacional_ss", loColDateTimeN); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 8;
			loColInt32N.Descripcion = "Sin descripcion para num_dias_clase";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("num_dias_clase", loColInt32N); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 9;
			loColDateTimeN.Descripcion = "Sin descripcion para inicio_especial";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("inicio_especial", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 10;
			loColDateTimeN.Descripcion = "Sin descripcion para fin_especial";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fin_especial", loColDateTimeN); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "Sin descripcion para cierre_horarios";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("cierre_horarios", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 12;
			loColstring.Descripcion = "Sin descripcion para cierre_seleccion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("cierre_seleccion", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 13;
			loColDateTimeN.Descripcion = "Sin descripcion para inicio_enc_estudiantil";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("inicio_enc_estudiantil", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 14;
			loColDateTimeN.Descripcion = "Sin descripcion para fin_enc_estudiantil";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fin_enc_estudiantil", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 15;
			loColDateTimeN.Descripcion = "Sin descripcion para inicio_sele_alumnos";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("inicio_sele_alumnos", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 16;
			loColDateTimeN.Descripcion = "Sin descripcion para fin_sele_alumnos";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fin_sele_alumnos", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 17;
			loColDateTimeN.Descripcion = "Sin descripcion para inicio_vacacional";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("inicio_vacacional", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 18;
			loColDateTimeN.Descripcion = "Sin descripcion para termino_vacacional";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("termino_vacacional", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 19;
			loColDateTimeN.Descripcion = "Sin descripcion para parcial1_inicio";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("parcial1_inicio", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 20;
			loColDateTimeN.Descripcion = "Sin descripcion para parcial1_fin";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("parcial1_fin", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 21;
			loColDateTimeN.Descripcion = "Sin descripcion para parcial2_inicio";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("parcial2_inicio", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 22;
			loColDateTimeN.Descripcion = "Sin descripcion para parcial2_fin";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("parcial2_fin", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 23;
			loColDateTimeN.Descripcion = "Sin descripcion para parcial3_inicio";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("parcial3_inicio", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 24;
			loColDateTimeN.Descripcion = "Sin descripcion para parcial3_fin";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("parcial3_fin", loColDateTimeN); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 25;
			loColstring.Descripcion = "Sin descripcion para filtro";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("filtro", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 500;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 26;
			loColstring.Descripcion = "Sin descripcion para nota_resp";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nota_resp", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 16;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 27;
			loColstring.Descripcion = "Sin descripcion para nota";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nota", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 28;
			loColDateTimeN.Descripcion = "Sin descripcion para inicio_cal_docentes";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("inicio_cal_docentes", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 29;
			loColDateTimeN.Descripcion = "Sin descripcion para fin_cal_docentes";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fin_cal_docentes", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 30;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_ini_periodo";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_ini_periodo", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 31;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_fin_periodo";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_fin_periodo", loColDateTimeN); 
			}
			#endregion

		}
	}
