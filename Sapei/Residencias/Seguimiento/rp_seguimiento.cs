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
	/// Clase rp_seguimiento generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class RP_Seguimiento:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase RP_Seguimiento.
		/// </summary>
		public RP_Seguimiento():base()
		{
			NombreTabla = "RP_Seguimientos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_seguimiento.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public RP_Seguimiento(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "rp_seguimientos";
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
		/// Obtiene o establece periodo_datos.Sin descripcion para periodo_datos 
		/// </summary>
		/// <value>
		/// periodo_datos 
		/// </value>
		[Key]
        [Required]
		[MaxLength (5)]
		[DefaultValue(null)]
        public string periodo_seguimiento
		{
			get
			{
                return ObtenerValorPropiedad<string>("periodo_seguimiento");
			}

			set
			{
                EstablecerValorPropiedad<string>("periodo_seguimiento", value);
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
		/// Obtiene o establece fecha_evaluacion.Sin descripcion para fecha_evaluacion 
		/// </summary>
		/// <value>
		/// fecha_evaluacion 
		/// </value>
		[Required]
		public DateTime fecha_evaluacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_evaluacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_evaluacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece numero_seguimiento.Sin descripcion para numero_seguimiento 
		/// </summary>
		/// <value>
		/// numero_seguimiento 
		/// </value>
		[Required]
		public Int32 numero_seguimiento
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("numero_seguimiento");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("numero_seguimiento", value);
			}

		}
		/// <summary>
		/// Obtiene o establece evaluacion_interno.Sin descripcion para evaluacion_interno 
		/// </summary>
		/// <value>
		/// evaluacion_interno 
		/// </value>
		[Required]
		public Int32 evaluacion_interno
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("evaluacion_interno");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("evaluacion_interno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece evaluacion_externo.Sin descripcion para evaluacion_externo 
		/// </summary>
		/// <value>
		/// evaluacion_externo 
		/// </value>
		[Required]
		public Int32 evaluacion_externo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("evaluacion_externo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("evaluacion_externo", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla rp_seguimiento.		/// </summary>
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
			Propiedades = new Dictionary<string, Propiedad>(4);


            AgregaCampoLlave("id", null);
            AgregaCampoLlave("id_programa", null);
            AgregaCampoLlave("folio", null);
            AgregaCampoLlave("periodo_seguimiento", null);
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
            loColstring.Descripcion = "Sin descripcion para periodo_seguimiento";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("periodo_seguimiento", loColstring);

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
			loColDateTime.Descripcion = "Sin descripcion para fecha_evaluacion";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_evaluacion", loColDateTime); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 5;
			loColInt32.Descripcion = "Sin descripcion para numero_seguimiento";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("numero_seguimiento", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 6;
			loColInt32.Descripcion = "Sin descripcion para evaluacion_interno";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("evaluacion_interno", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 7;
			loColInt32.Descripcion = "Sin descripcion para evaluacion_externo";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("evaluacion_externo", loColInt32); 
			}
			#endregion

		}
	}
