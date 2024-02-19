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
	/// Clase rp_programa generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class RP_Programa:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_programa.
		/// </summary>
		public RP_Programa():base()
		{
			NombreTabla = "rp_programa";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_programa.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public RP_Programa(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "rp_programa";
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
		/// Obtiene o establece periodo_programa.Sin descripcion para periodo_programa 
		/// </summary>
		/// <value>
		/// periodo_programa 
		/// </value>
		[Key]
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string periodo_programa
		{
			get
			{
				return ObtenerValorPropiedad<string>("periodo_programa");
			}

			set
			{
				EstablecerValorPropiedad<string>("periodo_programa", value);
			}

		}
        /// <summary>
        /// Obtiene o establece nombre.Sin descripcion para nombre 
        /// </summary>
        /// <value>
        /// nombre 
        /// </value>
        [Required]
        [MaxLength(200)]
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
		/// Obtiene o establece correo.Sin descripcion para correo 
		/// </summary>
		/// <value>
		/// correo 
		/// </value>
		[Required]
		[MaxLength (60)]
		[DefaultValue(null)]
		public string correo
		{
			get
			{
				return ObtenerValorPropiedad<string>("correo");
			}

			set
			{
				EstablecerValorPropiedad<string>("correo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece departamento.Sin descripcion para departamento 
		/// </summary>
		/// <value>
		/// departamento 
		/// </value>
		[Required]
		[MaxLength (100)]
		[DefaultValue(null)]
		public string departamento
		{
			get
			{
				return ObtenerValorPropiedad<string>("departamento");
			}

			set
			{
				EstablecerValorPropiedad<string>("departamento", value);
			}

		}
		/// <summary>
		/// Obtiene o establece responsable.Sin descripcion para responsable 
		/// </summary>
		/// <value>
		/// responsable 
		/// </value>
		[Required]
		[MaxLength (60)]
		[DefaultValue(null)]
		public string responsable
		{
			get
			{
				return ObtenerValorPropiedad<string>("responsable");
			}

			set
			{
				EstablecerValorPropiedad<string>("responsable", value);
			}

		}
		/// <summary>
		/// Obtiene o establece cargo.Sin descripcion para cargo 
		/// </summary>
		/// <value>
		/// cargo 
		/// </value>
		[Required]
		[MaxLength (60)]
		[DefaultValue(null)]
		public string cargo
		{
			get
			{
				return ObtenerValorPropiedad<string>("cargo");
			}

			set
			{
				EstablecerValorPropiedad<string>("cargo", value);
			}

		}
        /// <summary>
        /// Obtiene o establece opcion_programa.Sin descripcion para opcion_programa 
        /// </summary>
        /// <value>
        /// nombre 
        /// </value>
        [Required]
        [MaxLength(5)]
        [DefaultValue(null)]
        public string opcion_programa
        {
            get
            {
                return ObtenerValorPropiedad<string>("opcion_programa");
            }

            set
            {
                EstablecerValorPropiedad<string>("opcion_programa", value);
            }

        }
		/// <summary>
		/// Obtiene o establece rfc_dependencia.Sin descripcion para rfc_dependencia 
		/// </summary>
		/// <value>
		/// rfc_dependencia 
		/// </value>
		[Required]
		[MaxLength (13)]
		[DefaultValue(null)]
		public string rfc_dependencia
		{
			get
			{
				return ObtenerValorPropiedad<string>("rfc_dependencia");
			}

			set
			{
				EstablecerValorPropiedad<string>("rfc_dependencia", value);
			}

		}
        /// <summary>
        /// Obtiene o establece carrera.Sin descripcion para carrera 
        /// </summary>
        /// <value>
        /// carrera 
        /// </value>
        [Required]
        [MaxLength(3)]
        [DefaultValue(null)]
        public string carrera
        {
            get
            {
                return ObtenerValorPropiedad<string>("carrera");
            }

            set
            {
                EstablecerValorPropiedad<string>("carrera", value);
            }

        }
        /// Obtiene o establece numero_proyecto.Sin descripcion para id 
        /// </summary>
        /// <value>
        /// numero_proyecto
        /// </value>
		[Required]
        public Int32 numero_proyecto
        {
            get
            {
                return ObtenerValorPropiedad<Int32>("numero_proyecto");
            }

            set
            {
                EstablecerValorPropiedad<Int32>("numero_proyecto", value);
            }

        }
        #endregion
        #region Funciones
        /// <summary>
        /// Carga un registro especifico denotado por las llaves de la tabla rp_programa.		/// </summary>
        /// <param name="piid">id</param>
        /// <param name="psperiodo_programa">periodo_programa</param>
        public void Cargar(Int32 piid,string psperiodo_programa)
		{
			base.Cargar(piid,psperiodo_programa);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(2);
			Propiedades = new Dictionary<string, Propiedad>(7);

			AgregaCampoLlave("id",null);
			AgregaCampoLlave("periodo_programa",null);

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

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para periodo_programa";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo_programa", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 200;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 2;
            loColstring.Descripcion = "Sin descripcion para nombre";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("nombre", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para correo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("correo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para departamento";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("departamento", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para responsable";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("responsable", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para cargo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("cargo", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 5;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 2;
            loColstring.Descripcion = "Sin descripcion para opcion_programa";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("opcion_programa", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para rfc_dependencia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc_dependencia", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 3;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 6;
            loColstring.Descripcion = "Sin descripcion para carrera";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("carrera", loColstring);

            loColInt32 = new PropiedadesColumna<Int32>();
            loColInt32.EsPrimaryKey = false;
            loColInt32.Longitud = 4;
            loColInt32.Precision = 10;
            loColInt32.EsRequeridoBD = true;
            loColInt32.CampoId = 7;
            loColInt32.Descripcion = "Sin descripcion para numero_proyecto";
            loColInt32.EsIdentity = false;
            loColInt32.Tipo = typeof(Int32);
            AgregarPropiedad<Int32>("numero_proyecto", loColInt32);
        }
			#endregion

		}
	}
