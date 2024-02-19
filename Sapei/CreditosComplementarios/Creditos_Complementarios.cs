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
	/// Clase creditos_complementario generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Creditos_Complementarios:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase creditos_complementario.
		/// </summary>
		public Creditos_Complementarios():base()
		{
			NombreTabla = "creditos_complementarios";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase creditos_complementario.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Creditos_Complementarios(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "creditos_complementarios";
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
		/// Obtiene o establece semestre.Sin descripcion para semestre 
		/// </summary>
		/// <value>
		/// semestre 
		/// </value>
		[Required]
		public Int32 semestre
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("semestre");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("semestre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo.Sin descripcion para tipo 
		/// </summary>
		/// <value>
		/// tipo 
		/// </value>
          [Key]
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string tipo
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece usuario.Sin descripcion para usuario 
		/// </summary>
		/// <value>
		/// usuario 
		/// </value>
		[Required]
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
		/// Carga un registro especifico denotado por las llaves de la tabla creditos_complementario.		/// </summary>
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
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<DateTime?> loColDateTimeN; 
			 PropiedadesColumna<Double> loColDouble; 
			 PropiedadesColumna<Int32> loColInt32;
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(2);
			Propiedades = new Dictionary<string, Propiedad>(8);

			AgregaCampoLlave("no_de_control",null);
               AgregaCampoLlave("tipo", null);

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

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
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
			loColDateTimeN.CampoId = 3;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_termino";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_termino", loColDateTimeN); 

			loColDouble = new PropiedadesColumna<Double>();
			loColDouble.EsPrimaryKey = false;
			loColDouble.Longitud = 8;
			loColDouble.Precision = 53;
			loColDouble.EsRequeridoBD = true;
			loColDouble.CampoId = 4;
			loColDouble.Descripcion = "Sin descripcion para promedio";
			loColDouble.EsIdentity = false;
			loColDouble.Tipo = typeof(Double);
			AgregarPropiedad<Double>("promedio", loColDouble); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 5;
			loColInt32.Descripcion = "Sin descripcion para semestre";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("semestre", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para tipo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring);

		}
			#endregion

		}
	}
