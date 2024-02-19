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
	/// Clase ss_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class SS_Solicitud:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_solicitud.
		/// </summary>
		public SS_Solicitud():base()
		{
			NombreTabla = "ss_solicitud";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_solicitud.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public SS_Solicitud(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "ss_solicitud";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece folio.Sin descripcion para folio 
		/// </summary>
		/// <value>
		/// folio 
		/// </value>
		[Key]
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
		/// Obtiene o establece id_programa.Sin descripcion para id_programa 
		/// </summary>
		/// <value>
		/// id_programa 
		/// </value>
		[Key]
		[Required]
		public Int32 id_programa
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_programa");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_programa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_inicio.Sin descripcion para fecha_inicio 
		/// </summary>
		/// <value>
		/// fecha_inicio 
		/// </value>
		[Required]
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
		/// Obtiene o establece fecha_termino.Sin descripcion para fecha_termino 
		/// </summary>
		/// <value>
		/// fecha_termino 
		/// </value>
		[Required]
		public DateTime fecha_termino
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_termino");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_termino", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_solicitud.Sin descripcion para fecha_solicitud 
		/// </summary>
		/// <value>
		/// fecha_solicitud 
		/// </value>
		[Required]
		public DateTime fecha_solicitud
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_solicitud");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_solicitud", value);
			}

		}
		/// <summary>
		/// Obtiene o establece modalidad.Sin descripcion para modalidad 
		/// </summary>
		/// <value>
		/// modalidad 
		/// </value>
		[Required]
		[MaxLength (3)]
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
		/// <summary>
		/// Obtiene o establece turno.Sin descripcion para turno 
		/// </summary>
		/// <value>
		/// turno 
		/// </value>
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string turno
		{
			get
			{
				return ObtenerValorPropiedad<string>("turno");
			}

			set
			{
				EstablecerValorPropiedad<string>("turno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estado.Sin descripcion para estado 
		/// </summary>
		/// <value>
		/// estado 
		/// </value>
		[Required]
		[MaxLength (1)]
		[DefaultValue(null)]
		public string estado
		{
			get
			{
				return ObtenerValorPropiedad<string>("estado");
			}

			set
			{
				EstablecerValorPropiedad<string>("estado", value);
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

		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla ss_solicitud.		/// </summary>
		/// <param name="pifolio">folio</param>
		/// <param name="psperiodo">periodo</param>
		/// <param name="piid_programa">id_programa</param>
		public void Cargar(Int32 pifolio,string psperiodo,Int32 piid_programa)
		{
			base.Cargar(pifolio,psperiodo,piid_programa);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(3);
			Propiedades = new Dictionary<string, Propiedad>(10);

			AgregaCampoLlave("folio",null);
			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("id_programa",null);

            loColInt32 = new PropiedadesColumna<Int32>();
            loColInt32.EsPrimaryKey = true;
            loColInt32.Longitud = 4;
            loColInt32.Precision = 0;
            loColInt32.EsRequeridoBD = true;
            loColInt32.CampoId = 0;
            loColInt32.Descripcion = "Sin descripcion para folio";
            loColInt32.EsIdentity = false;
            loColInt32.Tipo = typeof(Int32);
            AgregarPropiedad<Int32>("folio", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para id_programa";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_programa", loColInt32); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 3;
			loColDateTime.Descripcion = "Sin descripcion para fecha_inicio";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_inicio", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 4;
			loColDateTime.Descripcion = "Sin descripcion para fecha_termino";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_termino", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 5;
			loColDateTime.Descripcion = "Sin descripcion para fecha_solicitud";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_solicitud", loColDateTime); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para modalidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("modalidad", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para turno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("turno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para estado";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estado", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Sin descripcion para no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring);
			}
			#endregion

    }
	}
