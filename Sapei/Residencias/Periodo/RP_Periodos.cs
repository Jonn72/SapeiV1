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
	/// Clase rp_periodo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class RP_Periodos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_periodo.
		/// </summary>
		public RP_Periodos():base()
		{
			NombreTabla = "rp_periodos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_periodo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public RP_Periodos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "rp_periodos";
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
		/// Obtiene o establece fecha_inicio.Sin descripcion para fecha_inicio 
		/// </summary>
		/// <value>
		/// fecha_inicio 
		/// </value>
		[Required]
		[DefaultValue(null)]
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
		[DefaultValue(null)]
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
        /// Obtiene o establece primerS16.Sin descripcion para fecha_fin 
        /// </summary>
        /// <value>
        /// fecha_fin 
        /// </value>
        [Required]
        [DefaultValue(null)]
        public DateTime primerS16
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("primerS16");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("primerS16", value);
            }

        }
        /// <summary>
        /// Obtiene o establece segundoS16.Sin descripcion para fecha_fin 
        /// </summary>
        /// <value>
        /// fecha_fin 
        /// </value>
        [Required]
        [DefaultValue(null)]
        public DateTime segundoS16
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("segundoS16");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("segundoS16", value);
            }

        }
        /// <summary>
        /// Obtiene o establece tercerS16.Sin descripcion para fecha_fin 
        /// </summary>
        /// <value>
        /// fecha_fin 
        /// </value>
        [Required]
        [DefaultValue(null)]
        public DateTime tercerS16
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("tercerS16");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("tercerS16", value);
            }

        }
        /// <summary>
        /// Obtiene o establece primerS24.Sin descripcion para fecha_fin 
        /// </summary>
        /// <value>
        /// fecha_fin 
        /// </value>
        [Required]
        [DefaultValue(null)]
        public DateTime primerS24
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("primerS24");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("primerS24", value);
            }

        }
        /// <summary>
        /// Obtiene o establece segundoS24.Sin descripcion para fecha_fin 
        /// </summary>
        /// <value>
        /// fecha_fin 
        /// </value>
        [Required]
        [MaxLength(20)]
        [DefaultValue(null)]
        public DateTime segundoS24
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("segundoS24");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("segundoS24", value);
            }

        }
        /// <summary>
        /// Obtiene o establece tercerS24.Sin descripcion para fecha_fin 
        /// </summary>
        /// <value>
        /// fecha_fin 
        /// </value>
        [Required]
        [DefaultValue(null)]
        public DateTime tercerS24
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("tercerS24");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("tercerS24", value);
            }

        }
        /// <summary>
        /// Obtiene o establece nombre.Sin descripcion para nombre 
        /// </summary>
        /// <value>
        /// nombre 
        /// </value>
        [Required]
        [MaxLength(60)]
        [DefaultValue(null)]
        public string nombre
        {
            get
            {
                return ObtenerValorPropiedad<string>("nombre");
            }

            set
            {
                EstablecerValorPropiedad<string>("nombre", value);
            }

        }
        /// <summary>
        /// Obtiene o establece url.Sin descripcion para url 
        /// </summary>
        /// <value>
        /// url 
        /// </value>
        [Required]
        [MaxLength(300)]
        [DefaultValue(null)]
        public string url
        {
            get
            {
                return ObtenerValorPropiedad<string>("url");
            }

            set
            {
                EstablecerValorPropiedad<string>("url", value);
            }

        }
        #endregion
        #region Funciones
        /// <summary>
        /// Carga un registro especifico denotado por las llaves de la tabla rp_periodo.		/// </summary>
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
            PropiedadesColumna<DateTime> loColDateTime;
            if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(5);

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

            loColDateTime = new PropiedadesColumna<DateTime>();
            loColDateTime.EsPrimaryKey = false;
            loColDateTime.Longitud = 10;
            loColDateTime.Precision = 10;
            loColDateTime.EsRequeridoBD = true;
            loColDateTime.CampoId = 1;
            loColDateTime.Descripcion = "Sin descripcion para fecha_inicio";
            loColDateTime.EsIdentity = false;
            loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_inicio", loColDateTime);

            loColDateTime = new PropiedadesColumna<DateTime>();
            loColDateTime.EsPrimaryKey = false;
            loColDateTime.Longitud = 20;
            loColDateTime.Precision = 20;
            loColDateTime.EsRequeridoBD = true;
            loColDateTime.CampoId = 2;
			loColDateTime.Descripcion = "Sin descripcion para fecha_fin";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_fin", loColDateTime);

            loColDateTime = new PropiedadesColumna<DateTime>();
            loColDateTime.EsPrimaryKey = false;
            loColDateTime.Longitud = 20;
            loColDateTime.Precision = 20;
            loColDateTime.EsRequeridoBD = true;
            loColDateTime.CampoId = 2;
            loColDateTime.Descripcion = "Sin descripcion para primerS16";
            loColDateTime.EsIdentity = false;
            loColDateTime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("primerS16", loColDateTime);

            loColDateTime = new PropiedadesColumna<DateTime>();
            loColDateTime.EsPrimaryKey = false;
            loColDateTime.Longitud = 20;
            loColDateTime.Precision = 20;
            loColDateTime.EsRequeridoBD = true;
            loColDateTime.CampoId = 2;
            loColDateTime.Descripcion = "Sin descripcion para segundoS16";
            loColDateTime.EsIdentity = false;
            loColDateTime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("segundoS16", loColDateTime);

            loColDateTime = new PropiedadesColumna<DateTime>();
            loColDateTime.EsPrimaryKey = false;
            loColDateTime.Longitud = 20;
            loColDateTime.Precision = 20;
            loColDateTime.EsRequeridoBD = true;
            loColDateTime.CampoId = 2;
            loColDateTime.Descripcion = "Sin descripcion para tercerS16";
            loColDateTime.EsIdentity = false;
            loColDateTime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("tercerS16", loColDateTime);

            loColDateTime = new PropiedadesColumna<DateTime>();
            loColDateTime.EsPrimaryKey = false;
            loColDateTime.Longitud = 20;
            loColDateTime.Precision = 20;
            loColDateTime.EsRequeridoBD = true;
            loColDateTime.CampoId = 2;
            loColDateTime.Descripcion = "Sin descripcion para primerS24";
            loColDateTime.EsIdentity = false;
            loColDateTime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("primerS24", loColDateTime);

            loColDateTime = new PropiedadesColumna<DateTime>();
            loColDateTime.EsPrimaryKey = false;
            loColDateTime.Longitud = 20;
            loColDateTime.Precision = 20;
            loColDateTime.EsRequeridoBD = true;
            loColDateTime.CampoId = 2;
            loColDateTime.Descripcion = "Sin descripcion para segundoS24";
            loColDateTime.EsIdentity = false;
            loColDateTime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("segundoS24", loColDateTime);

            loColDateTime = new PropiedadesColumna<DateTime>();
            loColDateTime.EsPrimaryKey = false;
            loColDateTime.Longitud = 20;
            loColDateTime.Precision = 20;
            loColDateTime.EsRequeridoBD = true;
            loColDateTime.CampoId = 2;
            loColDateTime.Descripcion = "Sin descripcion para tercerS24";
            loColDateTime.EsIdentity = false;
            loColDateTime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("tercerS24", loColDateTime);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 60;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 1;
            loColstring.Descripcion = "Sin descripcion para nombre";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("nombre", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 300;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 2;
            loColstring.Descripcion = "Sin descripcion para url";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("url", loColstring);
        }
			#endregion

		}
	}
