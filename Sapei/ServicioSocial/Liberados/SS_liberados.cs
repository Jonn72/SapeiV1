using System;
using System.Collections.Generic;
using Sapei.Framework;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sapei
{
	/// <summary>
	/// Clase ss_liberado generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class SS_liberados:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_liberado.
		/// </summary>
		public SS_liberados():base()
		{
			NombreTabla = "ss_liberados";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_liberado.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public SS_liberados(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "ss_liberados";
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
		/// Obtiene o establece promedio.Sin descripcion para promedio 
		/// </summary>
		/// <value>
		/// promedio 
		/// </value>
		[Required]
		public Double promedio
		{
			get
			{
				return ObtenerValorPropiedad<Double>("promedio");
			}

			set
			{
				EstablecerValorPropiedad<Double>("promedio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece desempeño.Sin descripcion para desempeño 
		/// </summary>
		/// <value>
		/// desempeño 
		/// </value>
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public string desempeño
		{
			get
			{
				return ObtenerValorPropiedad<string>("desempeño");
			}

			set
			{
				EstablecerValorPropiedad<string>("desempeño", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_liberacion.Sin descripcion para fecha_liberacion 
		/// </summary>
		/// <value>
		/// fecha_liberacion 
		/// </value>
		[Required]
		public DateTime fecha_liberacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_liberacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_liberacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece usuario_gtv.Sin descripcion para usuario_gtv 
		/// </summary>
		/// <value>
		/// usuario_gtv 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string usuario_gtv
		{
			get
			{
				return ObtenerValorPropiedad<string>("usuario_gtv");
			}

			set
			{
				EstablecerValorPropiedad<string>("usuario_gtv", value);
			}

		}
		/// <summary>
		/// Obtiene o establece validado.Sin descripcion para validado 
		/// </summary>
		/// <value>
		/// validado 
		/// </value>
		[Required]
		public Boolean validado
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("validado");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("validado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_validacion.Sin descripcion para fecha_validacion 
		/// </summary>
		/// <value>
		/// fecha_validacion 
		/// </value>
		[Required]
		public DateTime fecha_validacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_validacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_validacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece folio.Sin descripcion para folio 
		/// </summary>
		/// <value>
		/// folio 
		/// </value>
		[Required]
		public Int32 folio
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("folio");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("folio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece usuario_se.Sin descripcion para usuario_se 
		/// </summary>
		/// <value>
		/// usuario_se 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string usuario_se
		{
			get
			{
				return ObtenerValorPropiedad<string>("usuario_se");
			}

			set
			{
				EstablecerValorPropiedad<string>("usuario_se", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla ss_liberado.		/// </summary>
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
			 PropiedadesColumna<Double> loColDouble; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<Boolean> loColBoolean; 
			 PropiedadesColumna<Int32> loColInt32; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(10);

			AgregaCampoLlave("no_de_control",null);

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
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring); 

			loColDouble = new PropiedadesColumna<Double>();
			loColDouble.EsPrimaryKey = false;
			loColDouble.Longitud = 8;
			loColDouble.Precision = 53;
			loColDouble.EsRequeridoBD = true;
			loColDouble.CampoId = 2;
			loColDouble.Descripcion = "Sin descripcion para promedio";
			loColDouble.EsIdentity = false;
			loColDouble.Tipo = typeof(Double);
			AgregarPropiedad<Double>("promedio", loColDouble); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para desempeño";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("desempeño", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 4;
			loColDateTime.Descripcion = "Sin descripcion para fecha_liberacion";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_liberacion", loColDateTime); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para usuario_gtv";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario_gtv", loColstring); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 6;
			loColBoolean.Descripcion = "Sin descripcion para validado";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("validado", loColBoolean); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 7;
			loColDateTime.Descripcion = "Sin descripcion para fecha_validacion";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_validacion", loColDateTime); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 8;
			loColInt32.Descripcion = "Sin descripcion para folio";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("folio", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "Sin descripcion para usuario_se";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario_se", loColstring); 
			}
			#endregion

		}
	}
