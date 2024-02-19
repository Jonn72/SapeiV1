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
	/// Clase ss_actividades_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class SS_Actividades_Solicitud:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_actividades_solicitud.
		/// </summary>
		public SS_Actividades_Solicitud():base()
		{
			NombreTabla = "ss_actividades_solicitud";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_actividades_solicitud.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public SS_Actividades_Solicitud(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "ss_actividades_solicitud";
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
        /// Obtiene o establece id_programa.Sin descripcion para id_programa 
        /// </summary>
        /// <value>
        /// id_programa 
        /// </value>
        [Key]
        [Required]
        [MaxLength(5)]
        [DefaultValue(null)]
        public string id_tipo_programa
        {
            get
            {
                return ObtenerValorPropiedad<string>("id_tipo_programa");
            }

            set
            {
                EstablecerValorPropiedad<string>("id_tipo_programa", value);
            }

        }
        /// <summary>
        /// Obtiene o establece tipo_actividad.Sin descripcion para id_actividades 
        /// </summary>
        /// <value>
        /// id_actividades 
        /// </value>
        [Key]
        [Required]
        [MaxLength(5)]
        [DefaultValue(null)]
        public string id_actividades
        {
            get
            {
                return ObtenerValorPropiedad<string>("id_actividades");
            }

            set
            {
                EstablecerValorPropiedad<string>("id_actividades", value);
            }

        }
		/// <summary>
		/// Obtiene o establece tipo_actividad.Sin descripcion para tipo_actividad 
		/// </summary>
		/// <value>
		/// tipo_actividad 
		/// </value>
		[Required]
		[MaxLength (500)]
		[DefaultValue(null)]
		public string tipo_actividades
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_actividades");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_actividades", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla ss_actividades_solicitud.		/// </summary>
		/// <param name="pifolio">folio</param>
		/// <param name="psperiodo">periodo</param>
	    /// <param name="piid_programa">id_programa</param>
        /// /// <param name="psiid_tipo_programa">id_programa</param>
        /// <param name="psid_activiades">id_programa</param>
        public void Cargar(Int32 pifolio, string psperiodo, Int32 piid_programa, string psiid_tipo_programa, string psid_actividades)
		{
            base.Cargar(pifolio, psperiodo, piid_programa, psiid_tipo_programa, psid_actividades);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32> loColInt32; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(4);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("folio",null);
			AgregaCampoLlave("periodo",null);			
            AgregaCampoLlave("id_programa", null);
            AgregaCampoLlave("id_tipo_programa", null);
            AgregaCampoLlave("id_actividades", null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32 .EsPrimaryKey = true;
			loColInt32 .Longitud = 4;
			loColInt32 .Precision = 0;
			loColInt32 .EsRequeridoBD = true;
			loColInt32 .CampoId = 0;
			loColInt32 .Descripcion = "Sin descripcion para folio";
			loColInt32 .EsIdentity = false;
			loColInt32 .Tipo = typeof(Int32);
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
            loColInt32.CampoId = 3;
            loColInt32.Descripcion = "Sin descripcion para id_programa";
            loColInt32.EsIdentity = false;
            loColInt32.Tipo = typeof(Int32);
            AgregarPropiedad<Int32>("id_programa", loColInt32);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = true;
            loColstring.Longitud = 5;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 0;
            loColstring.Descripcion = "Sin descripcion para id_tipo_programa";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("id_tipo_programa", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = true;
            loColstring.Longitud = 5;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 4;
            loColstring.Descripcion = "Sin descripcion para id_actividades";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("id_actividades", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 500;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para tipo_actividades";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_actividades", loColstring); 

			}
			#endregion

		}
	}
