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
	/// Clase tutorias_inscrito generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Tutorias_Inscritos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase tutorias_inscrito.
		/// </summary>
		public Tutorias_Inscritos():base()
		{
			NombreTabla = "tutorias_inscritos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase tutorias_inscrito.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Tutorias_Inscritos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "tutorias_inscritos";
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
		/// Obtiene o establece grupo.Sin descripcion para grupo 
		/// </summary>
		/// <value>
		/// grupo 
		/// </value>
		[Key]
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string grupo
		{
			get
			{
				return ObtenerValorPropiedad<string>("grupo");
			}

			set
			{
				EstablecerValorPropiedad<string>("grupo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_registro.Sin descripcion para fecha_registro 
		/// </summary>
		/// <value>
		/// fecha_registro 
		/// </value>
		[Required]
		public DateTime fecha_registro
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_registro");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_registro", value);
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla tutorias_inscrito.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="psno_de_control">no_de_control</param>
		/// <param name="psgrupo">grupo</param>
		public void Cargar(string psperiodo,string psno_de_control,string psgrupo)
		{
			base.Cargar(psperiodo,psno_de_control,psgrupo);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<DateTime?> loColDateTimeN; 
			 PropiedadesColumna<Double?> loColDoubleN; 
			 PropiedadesColumna<Int32?> loColInt32N; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(3);
			Propiedades = new Dictionary<string, Propiedad>(7);

			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("no_de_control",null);
			AgregaCampoLlave("grupo",null);

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
			loColstring.Descripcion = "Sin descripcion para grupo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("grupo", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 3;
			loColDateTime.Descripcion = "Sin descripcion para fecha_registro";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_registro", loColDateTime); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 8;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 4;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_termino";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_termino", loColDateTimeN); 

			loColDoubleN = new PropiedadesColumna<Double?>();
			loColDoubleN.Valor = null;
			loColDoubleN.EsPrimaryKey = false;
			loColDoubleN.Longitud = 8;
			loColDoubleN.Precision = 53;
			loColDoubleN.EsRequeridoBD = false;
			loColDoubleN.CampoId = 5;
			loColDoubleN.Descripcion = "Sin descripcion para promedio";
			loColDoubleN.EsIdentity = false;
			loColDoubleN.Tipo = typeof(Double?);
			AgregarPropiedad<Double?>("promedio", loColDoubleN); 

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
			}
			#endregion

		}
	}
