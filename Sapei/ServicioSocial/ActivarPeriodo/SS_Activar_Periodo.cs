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
	/// Clase ss_activar_periodo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class SS_Activar_Periodo:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_activar_periodo.
		/// </summary>
		public SS_Activar_Periodo():base()
		{
			NombreTabla = "ss_activar_periodo";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_activar_periodo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public SS_Activar_Periodo(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "ss_activar_periodo";
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
        [MaxLength(10)]
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
        [MaxLength(16)]
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
        /// Obtiene o establece fecha_fin.Sin descripcion para fecha_fin 
        /// </summary>
        /// <value>
        /// fecha_fin 
        /// </value>
        [Required]
        [MaxLength(16)]
        [DefaultValue(null)]
        public DateTime fecha_cierre_registro
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("fecha_cierre_registro");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("fecha_cierre_registro", value);
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
        [MaxLength(70)]
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
        /// <summary>
        /// Obtiene o establece fecha_bimestre_1.Sin descripcion para fecha_inicio 
        /// </summary>
        /// <value>
        /// fecha_bimestre_1 
        /// </value>
        [MaxLength(10)]
        [DefaultValue(null)]
        public DateTime fecha_bimestre_1
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("fecha_bimestre_1");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("fecha_bimestre_1", value);
            }

        }
        /// <summary>
        /// Obtiene o establece fecha_bimestre_2.Sin descripcion para fecha_inicio 
        /// </summary>
        /// <value>
        /// fecha_bimestre_2 
        /// </value>
        [MaxLength(10)]
        [DefaultValue(null)]
        public DateTime fecha_bimestre_2
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("fecha_bimestre_2");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("fecha_bimestre_2", value);
            }

        }
        /// <summary>
        /// Obtiene o establece fecha_bimestre_3.Sin descripcion para fecha_inicio 
        /// </summary>
        /// <value>
        /// fecha_bimestre_3 
        /// </value>
        [MaxLength(10)]
        [DefaultValue(null)]
        public DateTime fecha_bimestre_3
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("fecha_bimestre_3");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("fecha_bimestre_3", value);
            }

        }
        #endregion
        #region Funciones
        /// <summary>
        /// Carga un registro especifico denotado por las llaves de la tabla ss_activar_periodo.		/// </summary>
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
            PropiedadesColumna<DateTime> loColDatetime;

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

            loColDatetime = new PropiedadesColumna<DateTime>();
            loColDatetime.EsPrimaryKey = false;
            loColDatetime.Longitud = 10;
            loColDatetime.Precision = 10;
            loColDatetime.EsRequeridoBD = true;
            loColDatetime.CampoId = 1;
            loColDatetime.Descripcion = "Sin descripcion para fecha_inicio";
            loColDatetime.EsIdentity = false;
            loColDatetime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_inicio", loColDatetime); 

			loColDatetime = new PropiedadesColumna<DateTime>();
			loColDatetime.EsPrimaryKey = false;
			loColDatetime.Longitud = 16;
			loColDatetime.Precision = 10;
			loColDatetime.EsRequeridoBD = true;
            loColDatetime.IncluyeHoras = true;
            loColDatetime.CampoId = 2;
			loColDatetime.Descripcion = "Sin descripcion para fecha_fin";
			loColDatetime.EsIdentity = false;
			loColDatetime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_fin", loColDatetime);

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
            loColstring.Longitud = 70;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 2;
            loColstring.Descripcion = "Sin descripcion para url";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("url", loColstring);

            loColDatetime = new PropiedadesColumna<DateTime>();
            loColDatetime.EsPrimaryKey = false;
            loColDatetime.Longitud = 10;
            loColDatetime.Precision = 10;
            loColDatetime.EsRequeridoBD = true;
            loColDatetime.CampoId = 1;
            loColDatetime.Descripcion = "Sin descripcion para fecha_bimestre_1";
            loColDatetime.EsIdentity = false;
            loColDatetime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("fecha_bimestre_1", loColDatetime);

            loColDatetime = new PropiedadesColumna<DateTime>();
            loColDatetime.EsPrimaryKey = false;
            loColDatetime.Longitud = 10;
            loColDatetime.Precision = 10;
            loColDatetime.EsRequeridoBD = true;
            loColDatetime.CampoId = 1;
            loColDatetime.Descripcion = "Sin descripcion para fecha_bimestre_2";
            loColDatetime.EsIdentity = false;
            loColDatetime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("fecha_bimestre_2", loColDatetime);

            loColDatetime = new PropiedadesColumna<DateTime>();
            loColDatetime.EsPrimaryKey = false;
            loColDatetime.Longitud = 10;
            loColDatetime.Precision = 10;
            loColDatetime.EsRequeridoBD = true;
            loColDatetime.CampoId = 1;
            loColDatetime.Descripcion = "Sin descripcion para fecha_bimestre_3";
            loColDatetime.EsIdentity = false;
            loColDatetime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("fecha_bimestre_3", loColDatetime);

            loColDatetime = new PropiedadesColumna<DateTime>();
            loColDatetime.EsPrimaryKey = false;
            loColDatetime.Longitud = 16;
            loColDatetime.Precision = 10;
            loColDatetime.EsRequeridoBD = true;
            loColDatetime.CampoId = 1;
            loColDatetime.IncluyeHoras = true;
            loColDatetime.Descripcion = "Sin descripcion para fecha_cierre_registro";
            loColDatetime.EsIdentity = false;
            loColDatetime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("fecha_cierre_registro", loColDatetime);
            

        }
			#endregion



    }
	}
