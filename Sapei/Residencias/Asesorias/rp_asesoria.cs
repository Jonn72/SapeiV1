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
	/// Clase rp_asesoria generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class RP_Asesoria:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_asesoria.
		/// </summary>
		public RP_Asesoria():base()
		{
			NombreTabla = "rp_asesorias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_asesoria.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public RP_Asesoria(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "rp_asesorias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
        /// <summary>
        /// Obtiene o establece id.Sin descripcion para id 
        /// </summary>
        /// <value>
        /// id 
        /// </value>
        [Key]
        [Required]
        public Int32 id
        {
            get
            {
                return ObtenerValorPropiedad<Int32>("id");
            }

            set
            {
                EstablecerValorPropiedad<Int32>("id", value);
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
        /// Obtiene o establece periodo_asesorias.Sin descripcion para periodo_asesorias 
		/// </summary>
		/// <value>
		/// periodo_datos 
		/// </value>
		[Key]
        [Required]
		[MaxLength (5)]
		[DefaultValue(null)]
        public string periodo_asesorias
		{
			get
			{
                return ObtenerValorPropiedad<string>("periodo_asesorias");
			}

			set
			{
                EstablecerValorPropiedad<string>("periodo_asesorias", value);
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
		/// Obtiene o establece fecha.Sin descripcion para fecha 
		/// </summary>
		/// <value>
		/// fecha 
		/// </value>
		[Required]
		public DateTime fecha
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha", value);
			}

		}
		/// <summary>
		/// Obtiene o establece numero_asesoria.Sin descripcion para numero_asesoria 
		/// </summary>
		/// <value>
		/// numero_asesoria 
		/// </value>
		[Required]
		public Int32 numero_asesoria
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("numero_asesoria");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("numero_asesoria", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo.Sin descripcion para tipo 
		/// </summary>
		/// <value>
		/// tipo 
		/// </value>
		[Required]
		[MaxLength (30)]
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
		/// Obtiene o establece descripcion.Sin descripcion para descripcion 
		/// </summary>
		/// <value>
		/// descripcion 
		/// </value>
		[Required]
		[MaxLength (1000)]
		[DefaultValue(null)]
		public string descripcion
		{
			get
			{
				return ObtenerValorPropiedad<string>("descripcion");
			}

			set
			{
				EstablecerValorPropiedad<string>("descripcion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece solucion.Sin descripcion para solucion 
		/// </summary>
		/// <value>
		/// solucion 
		/// </value>
		[Required]
		[MaxLength (1000)]
		[DefaultValue(null)]
		public string solucion
		{
			get
			{
				return ObtenerValorPropiedad<string>("solucion");
			}

			set
			{
				EstablecerValorPropiedad<string>("solucion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estado.Sin descripcion para estado 
		/// </summary>
		/// <value>
		/// estado 
		/// </value>
		[Required]
		public Int32 estado
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("estado");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("estado", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla rp_asesoria.		/// </summary>
		public void Cargar()
		{
			base.Cargar();
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(5);
			Propiedades = new Dictionary<string, Propiedad>(6);


            AgregaCampoLlave("id", null);
            AgregaCampoLlave("id_programa", null);
            AgregaCampoLlave("folio", null);
            AgregaCampoLlave("periodo_asesorias", null);
            AgregaCampoLlave("no_de_control", null);

            loColInt32 = new PropiedadesColumna<Int32>();
            loColInt32.EsPrimaryKey = true;
            loColInt32.Longitud = 4;
            loColInt32.Precision = 10;
            loColInt32.EsRequeridoBD = true;
            loColInt32.CampoId = 0;
            loColInt32.Descripcion = "Sin descripcion para id";
            loColInt32.EsIdentity = true;
            loColInt32.Tipo = typeof(Int32);
            AgregarPropiedad<Int32>("id", loColInt32);

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
            loColstring.Descripcion = "Sin descripcion para periodo_asesorias";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("periodo_asesorias", loColstring);

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

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 4;
			loColDateTime.Descripcion = "Sin descripcion para fecha";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha", loColDateTime); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 5;
			loColInt32.Descripcion = "Sin descripcion para numero_asesoria";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("numero_asesoria", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
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
			loColstring.Longitud = 1000;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para descripcion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1000;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para solucion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("solucion", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 9;
			loColInt32.Descripcion = "Sin descripcion para estado";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("estado", loColInt32); 
			}
			#endregion

		}
	}
