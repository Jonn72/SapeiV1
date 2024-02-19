
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
	public partial class SS_Reportes:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_reportes.
		/// </summary>
		public SS_Reportes():base()
		{
			NombreTabla = "ss_reportes";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_reportes.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public SS_Reportes(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "ss_reportes";
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
        /// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
        /// </summary>
        /// <value>
        /// no_de_control 
        /// </value>
        [Key]
        [Required]
        [MaxLength(10)]
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
		/// Obtiene o establece folio.Sin descripcion para folio 
		/// </summary>
		/// <value>
		/// folio 
		/// </value>       
		[Key]
		[Required]
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
        /// Obtiene o establece seguimiento.Sin descripcion para no_reporte
        /// </summary>
        /// <value>
        /// no_reporte
        /// </value>
        [Required]
        public Int32 no_reporte
        {
            get
            {
                return ObtenerValorPropiedad<Int32>("no_reporte");
            }

            set
            {
                EstablecerValorPropiedad<Int32>("no_reporte", value);
            }

        }		
		/// <summary>
		/// Obtiene o establece seguimiento.Sin descripcion para p_1
		/// </summary>
		/// <value>
		/// p_1 
		/// </value>
		[Required]
		public Int32 p_1
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("p_1");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("p_1", value);
			}

		}
		/// <summary>
		/// Obtiene o establece seguimiento.Sin descripcion para p_2
		/// </summary>
		/// <value>
		/// p_2 
		/// </value>
		[Required]
		public Int32 p_2
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("p_2");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("p_2", value);
			}

		}
		/// <summary>
		/// Obtiene o establece seguimiento.Sin descripcion para p_3
		/// </summary>
		/// <value>
		/// p_3
		/// </value>
		[Required]
		public Int32 p_3
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("p_3");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("p_3", value);
			}

		}
		/// <summary>
		/// Obtiene o establece seguimiento.Sin descripcion para p_4
		/// </summary>
		/// <value>
		/// p_4
		/// </value>
		[Required]
		public Int32 p_4
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("p_4");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("p_4", value);
			}

		}
		/// <summary>
		/// Obtiene o establece seguimiento.Sin descripcion para p_5
		/// </summary>
		/// <value>
		/// p_5
		/// </value>
		[Required]
		public Int32 p_5
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("p_5");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("p_5", value);
			}

		}
		/// <summary>
		/// Obtiene o establece seguimiento.Sin descripcion para p_6
		/// </summary>
		/// <value>
		/// p_6 
		/// </value>
		[Required]
		public Int32 p_6
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("p_6");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("p_6", value);
			}

		}
		/// <summary>
		/// Obtiene o establece seguimiento.Sin descripcion para p_7
		/// </summary>
		/// <value>
		/// p_7 
		/// </value>
		[Required]
		public Int32 p_7
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("p_7");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("p_7", value);
			}

		}
		/// <summary>
		/// Obtiene o establece seguimiento.Sin descripcion para p_8
		/// </summary>
		/// <value>
		/// p_8 
		/// </value>
		[Required]
		public Int32 p_8
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("p_8");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("p_8", value);
			}

		}
		/// <summary>
		/// Obtiene o establece seguimiento.Sin descripcion para evaluacion
		/// </summary>
		/// <value>
		/// evaluacion 
		/// </value>
		public float evaluacion
		{
			get
			{
				return ObtenerValorPropiedad<float>("evaluacion");
			}

			set
			{
				EstablecerValorPropiedad<float>("evaluacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece seguimiento.Sin descripcion para evaluacion_final
		/// </summary>
		/// <value>
		/// evaluacion_final
		/// </value>
		public float evaluacion_final
		{
			get
			{
				return ObtenerValorPropiedad<float>("evaluacion_cualitativa");
			}

			set
			{
				EstablecerValorPropiedad<float>("evaluacion_cualitativa", value);
			}

		}

		
       	#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla ss_reportes.		
		/// </summary>
        /// <param name="psid">id</param>
        /// <param name="psno_de_control">no_de_control</param>
		/// <param name="pifolio">folio</param>
		/// <param name="psperiodo">periodo</param>
		/// <param name="piid_programa">id_programa</param>
        public void Cargar(Int32 psid, Int32 psno_de_control, Int32 pifolio, string psperiodo, Int32 piid_programa)
		{
			base.Cargar(psid,psno_de_control,pifolio,psperiodo,piid_programa);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32> loColInt32;
            PropiedadesColumna<float> loColfloat;
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(5);
			Propiedades = new Dictionary<string, Propiedad>(11);

            AgregaCampoLlave("id", null);
            AgregaCampoLlave("no_de_control", null);
			AgregaCampoLlave("folio",null);
			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("id_programa",null);

            loColInt32 = new PropiedadesColumna<Int32>();
            loColInt32.EsPrimaryKey = true;
            loColInt32.Longitud = 4;
            loColInt32.Precision = 10;
            loColInt32.EsRequeridoBD = true;
            loColInt32.CampoId = 2;
            loColInt32.Descripcion = "Sin descripcion para id";
            loColInt32.EsIdentity = true;
            loColInt32.Tipo = typeof(Int32);
            AgregarPropiedad<Int32>("id", loColInt32);

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

            loColInt32 = new PropiedadesColumna<Int32>();            
            loColInt32.EsPrimaryKey = true;
            loColInt32.Longitud = 5;
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

            loColInt32 = new PropiedadesColumna<Int32>();
            loColInt32.EsPrimaryKey = false;
            loColInt32.Longitud = 4;
            loColInt32.Precision = 10;
            loColInt32.EsRequeridoBD = true;
            loColInt32.CampoId = 2;
            loColInt32.Descripcion = "Sin descripcion para no_reporte";
            loColInt32.EsIdentity = false;
            loColInt32.Tipo = typeof(Int32);
            AgregarPropiedad<Int32>("no_reporte", loColInt32); 
			
			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para p_1";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("p_1", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para p_2";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("p_2", loColInt32); 			

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para p_3";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("p_3", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para p_4";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("p_4", loColInt32); 	
				
			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para p_5";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("p_5", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para p_6";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("p_6", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para p_7";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("p_7", loColInt32);
			
			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para p_8";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("p_8", loColInt32);

            loColfloat = new PropiedadesColumna<float>();
            loColfloat.EsPrimaryKey = false;
            loColfloat.Longitud = 5;
            loColfloat.Precision = 0;
            loColfloat.EsRequeridoBD = false;
            loColfloat.CampoId = 1;
            loColfloat.Descripcion = "Sin descripcion para evaluacion";
            loColfloat.EsIdentity = false;
            loColfloat.Tipo = typeof(float);
            AgregarPropiedad<float>("evaluacion", loColfloat);


            loColfloat = new PropiedadesColumna<float>();
            loColfloat.EsPrimaryKey = false;
            loColfloat.Longitud = 5;
            loColfloat.Precision = 0;
            loColfloat.EsRequeridoBD = false;
            loColfloat.CampoId = 1;
            loColfloat.Descripcion = "Sin descripcion para evaluacion_cualitativa";
            loColfloat.EsIdentity = false;
            loColfloat.Tipo = typeof(float);
            AgregarPropiedad<float>("evaluacion_cualitativa", loColfloat);


        }
			#endregion

    }
	}