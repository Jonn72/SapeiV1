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
	/// Clase rp_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class RP_Solicitud:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_solicitud.
		/// </summary>
		public RP_Solicitud():base()
		{
			NombreTabla = "rp_solicitud";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_solicitud.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
        public RP_Solicitud(Sistema poSistema)
            : base(poSistema)
		{
			NombreTabla = "rp_solicitud";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
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
		/// Obtiene o establece fecha_fin.Sin descripcion para fecha_fin 
		/// </summary>
		/// <value>
		/// fecha_fin 
		/// </value>
		[Required]
		public DateTime fecha_fin
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_fin");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_fin", value);
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
        /// Obtiene o establece estado_solicitud.Sin descripcion para evaluacion_final 
        /// </summary>
        /// <value>
        /// evaluacion_final 
        /// </value>
        [Required]
        public Int32 estado_solicitud
        {
            get
            {
                return ObtenerValorPropiedad<Int32>("estado_solicitud");
            }

            set
            {
                EstablecerValorPropiedad<Int32>("estado_solicitud", value);
            }

        }
		/// <summary>
		/// Obtiene o establece evaluacion_final.Sin descripcion para evaluacion_final 
		/// </summary>
		/// <value>
		/// evaluacion_final 
		/// </value>
		[Required]
		public Int32 evaluacion_final
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("evaluacion_final");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("evaluacion_final", value);
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
		/// Obtiene o establece folio.Sin descripcion para folio 
		/// </summary>
		/// <value>
		/// folio 
		/// </value>
		[Key]
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
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
		/// Obtiene o establece periodo_datos.Sin descripcion para periodo_datos 
		/// </summary>
		/// <value>
		/// periodo_datos 
		/// </value>
		[Key]
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string periodo_datos
		{
			get
			{
				return ObtenerValorPropiedad<string>("periodo_datos");
			}

			set
			{
				EstablecerValorPropiedad<string>("periodo_datos", value);
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla rp_solicitud.		/// </summary>
		/// <param name="piid_programa">id_programa</param>
		/// <param name="psfolio">folio</param>
		/// <param name="psperiodo_datos">periodo_datos</param>
		/// <param name="psno_de_control">no_de_control</param>
		public void Cargar(Int32 piid_programa,string psfolio,string psperiodo_datos,string psno_de_control)
		{
			base.Cargar(piid_programa,psfolio,psperiodo_datos,psno_de_control);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32> loColInt32; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(4);
			Propiedades = new Dictionary<string, Propiedad>(12);

			AgregaCampoLlave("id_programa",null);
			AgregaCampoLlave("folio",null);
			AgregaCampoLlave("periodo_datos",null);
			AgregaCampoLlave("no_de_control",null);

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 0;
			loColDateTime.Descripcion = "Sin descripcion para fecha_inicio";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_inicio", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 1;
			loColDateTime.Descripcion = "Sin descripcion para fecha_fin";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_fin", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
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
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para modalidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("modalidad", loColstring);

            loColInt32 = new PropiedadesColumna<Int32>();
            loColInt32.EsPrimaryKey = false;
            loColInt32.Longitud = 4;
            loColInt32.Precision = 10;
            loColInt32.EsRequeridoBD = true;
            loColInt32.CampoId = 4;
            loColInt32.Descripcion = "Sin descripcion para estado";
            loColInt32.EsIdentity = false;
            loColInt32.Tipo = typeof(Int32);
            AgregarPropiedad<Int32>("estado_solicitud", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 7;
			loColInt32.Descripcion = "Sin descripcion para evaluacion_final";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("evaluacion_final", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 8;
			loColInt32.Descripcion = "Sin descripcion para id_programa";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_programa", loColInt32);

            loColInt32 = new PropiedadesColumna<Int32>();
            loColInt32.EsPrimaryKey = true;
            loColInt32.Longitud = 10;
            loColInt32.Precision = 0;
            loColInt32.EsRequeridoBD = true;
            loColInt32.CampoId = 9;
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
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Sin descripcion para periodo_datos";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo_datos", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "Sin descripcion para no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring); 
			}
			#endregion

		}
	}
