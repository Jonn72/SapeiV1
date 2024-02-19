using System;
using System.Collections.Generic;
using Sapei.Framework;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sapei
{
	/// <summary>
	/// Clase avisos_reinscripcion generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Avisos_Reinscripcion:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase avisos_reinscripcion.
		/// </summary>
		public Avisos_Reinscripcion():base()
		{
			NombreTabla = "avisos_reinscripcion";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase avisos_reinscripcion.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Avisos_Reinscripcion(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "avisos_reinscripcion";
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
		/// Obtiene o establece autoriza_escolar.Sin descripcion para autoriza_escolar 
		/// </summary>
		/// <value>
		/// autoriza_escolar 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string autoriza_escolar
		{
			get
			{
				return ObtenerValorPropiedad<string>("autoriza_escolar");
			}

			set
			{
				EstablecerValorPropiedad<string>("autoriza_escolar", value);
			}

		}
		/// <summary>
		/// Obtiene o establece recibo_pago.Sin descripcion para recibo_pago 
		/// </summary>
		/// <value>
		/// recibo_pago 
		/// </value>
		[MaxLength (10)]
		[DefaultValue(null)]
		public string recibo_pago
		{
			get
			{
				return ObtenerValorPropiedad<string>("recibo_pago");
			}

			set
			{
				EstablecerValorPropiedad<string>("recibo_pago", value);
			}

		}

		/// <summary>
		/// Obtiene o establece fecha_hora_seleccion.Sin descripcion para fecha_hora_seleccion 
		/// </summary>
		/// <value>
		/// fecha_hora_seleccion 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_hora_seleccion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_hora_seleccion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_hora_seleccion", value);
			}

		}

		/// <summary>
		/// Obtiene o establece creditos_autorizados.Sin descripcion para creditos_autorizados 
		/// </summary>
		/// <value>
		/// creditos_autorizados 
		/// </value>
		[DefaultValue(null)]
		public Int32? creditos_autorizados
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("creditos_autorizados");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("creditos_autorizados", value);
			}

		}
		/// <summary>
		/// Obtiene o establece semestre.Sin descripcion para semestre 
		/// </summary>
		/// <value>
		/// semestre 
		/// </value>
		[DefaultValue(null)]
		public Int32? semestre
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("semestre");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("semestre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece promedio.Sin descripcion para promedio 
		/// </summary>
		/// <value>
		/// promedio 
		/// </value>
		[DefaultValue(null)]
		public Double? promedio
		{
			get
			{
				return ObtenerValorPropiedad<Double?>("promedio");
			}

			set
			{
				EstablecerValorPropiedad<Double?>("promedio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_hora_fin.Sin descripcion para fecha_hora_fin 
		/// </summary>
		/// <value>
		/// fecha_hora_fin 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_hora_fin
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_hora_fin");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_hora_fin", value);
			}

		}
		/// <summary>
		/// Obtiene o establece prioridad_reinscripcion.Sin descripcion para prioridad_reinscripcion 
		/// </summary>
		/// <value>
		/// prioridad_reinscripcion 
		/// </value>
		[Required]
		[DefaultValue("0")]
		public Byte prioridad_reinscripcion
		{
			get
			{
				return ObtenerValorPropiedad<Byte>("prioridad_reinscripcion");
			}

			set
			{
				EstablecerValorPropiedad<Byte>("prioridad_reinscripcion", value);
			}

		}
	
		[Required]
		[DefaultValue(false)]
		public Boolean dep_autoriza_extemporaneo
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("dep_autoriza_extemporaneo");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("dep_autoriza_extemporaneo", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla avisos_reinscripcion.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="psno_de_control">no_de_control</param>
		public void Cargar(string psperiodo,string psno_de_control)
		{
			base.Cargar(psperiodo,psno_de_control);
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
			 PropiedadesColumna<Double?> loColDoubleN; 
			 PropiedadesColumna<Byte> loColByte; 
			 PropiedadesColumna<Boolean> loColBoolean; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(2);
			Propiedades = new Dictionary<string, Propiedad>(11);

			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("no_de_control",null);

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
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para autoriza_escolar";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("autoriza_escolar", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para recibo_pago";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("recibo_pago", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.IncluyeHoras = true;
			loColDateTimeN.CampoId = 4;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_hora_seleccion";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_hora_seleccion", loColDateTimeN); 


			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 5;
			loColInt32N.Descripcion = "Sin descripcion para creditos_autorizados";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("creditos_autorizados", loColInt32N); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 6;
			loColInt32N.Descripcion = "Sin descripcion para semestre";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("semestre", loColInt32N); 

			loColDoubleN = new PropiedadesColumna<Double?>();
			loColDoubleN.Valor = null;
			loColDoubleN.EsPrimaryKey = false;
			loColDoubleN.Longitud = 5;
			loColDoubleN.Precision = 5;
			loColDoubleN.EsRequeridoBD = false;
			loColDoubleN.CampoId = 7;
			loColDoubleN.Descripcion = "Sin descripcion para promedio";
			loColDoubleN.EsIdentity = false;
			loColDoubleN.Tipo = typeof(Double?);
			AgregarPropiedad<Double?>("promedio", loColDoubleN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.IncluyeHoras = true;
			loColDateTimeN.CampoId = 8;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_hora_fin";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_hora_fin", loColDateTimeN); 

			loColByte = new PropiedadesColumna<Byte>();
			loColByte.Valor = 0;
			loColByte.EsPrimaryKey = false;
			loColByte.Longitud = 1;
			loColByte.Precision = 3;
			loColByte.EsRequeridoBD = true;
			loColByte.CampoId = 9;
			loColByte.Descripcion = "Sin descripcion para prioridad_reinscripcion";
			loColByte.EsIdentity = false;
			loColByte.Tipo = typeof(Byte);
			AgregarPropiedad<Byte>("prioridad_reinscripcion", loColByte); 

		
			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.Valor = false;
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 10;
			loColBoolean.Descripcion = "Sin descripcion para carga_entregada";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("dep_autoriza_extemporaneo", loColBoolean);
		}
			#endregion

		}
	}
