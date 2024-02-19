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
	/// Clase rp_datos_programa generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class RP_Datos_Programa:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_datos_programa.
		/// </summary>
		public RP_Datos_Programa():base()
		{
			NombreTabla = "rp_datos_programa";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_datos_programa.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public RP_Datos_Programa(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "rp_datos_programa";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
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
		/// Obtiene o establece delimitacione.Sin descripcion para delimitacione 
		/// </summary>
		/// <value>
		/// delimitacione 
		/// </value>
		[Required]
		[MaxLength (700)]
		[DefaultValue(null)]
		public string delimitaciones
		{
			get
			{
				return ObtenerValorPropiedad<string>("delimitaciones");
			}

			set
			{
				EstablecerValorPropiedad<string>("delimitaciones", value);
			}

		}
		/// <summary>
		/// Obtiene o establece objetivo_general.Sin descripcion para objetivo_general 
		/// </summary>
		/// <value>
		/// objetivo_general 
		/// </value>
		[Required]
		[MaxLength (500)]
		[DefaultValue(null)]
		public string objetivo_general
		{
			get
			{
				return ObtenerValorPropiedad<string>("objetivo_general");
			}

			set
			{
				EstablecerValorPropiedad<string>("objetivo_general", value);
			}

		}
		/// <summary>
		/// Obtiene o establece objetivo_especificos.Sin descripcion para objetivo_especificos 
		/// </summary>
		/// <value>
		/// objetivo_especificos 
		/// </value>
		[Required]
		[MaxLength (1000)]
		[DefaultValue(null)]
		public string objetivo_especificos
		{
			get
			{
				return ObtenerValorPropiedad<string>("objetivo_especificos");
			}

			set
			{
				EstablecerValorPropiedad<string>("objetivo_especificos", value);
			}

		}
        /// <summary>
        /// Obtiene o establece objetivo_especificos.Sin descripcion para actividades 
        /// </summary>
        /// <value>
        /// objetivo_especificos 
        /// </value>
        [Required]
        [MaxLength(1000)]
        [DefaultValue(null)]
        public string actividades
        {
            get
            {
                return ObtenerValorPropiedad<string>("actividades");
            }

            set
            {
                EstablecerValorPropiedad<string>("actividades", value);
            }

        }
		/// <summary>
		/// Obtiene o establece duracion.Sin descripcion para duracion 
		/// </summary>
		/// <value>
		/// duracion 
		/// </value>
		[Required]
		public Int32 duracion
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("duracion");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("duracion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece justificacion.Sin descripcion para justificacion 
		/// </summary>
		/// <value>
		/// justificacion 
		/// </value>
		[Required]
		[MaxLength (2000)]
		[DefaultValue(null)]
		public string justificacion
		{
			get
			{
				return ObtenerValorPropiedad<string>("justificacion");
			}

			set
			{
				EstablecerValorPropiedad<string>("justificacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ubicacion.Sin descripcion para ubicacion 
		/// </summary>
		/// <value>
		/// ubicacion 
		/// </value>
		[Required]
		[DefaultValue(null)]
		public byte[] ubicacion
		{
			get
			{
				return ObtenerValorPropiedad<byte[]>("ubicacion");
			}

			set
			{
				EstablecerValorPropiedad<byte[]>("ubicacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece rfc_asesor.Sin descripcion para rfc_asesor 
		/// </summary>
		/// <value>
		/// rfc_asesor 
		/// </value>
		[Required]
		[MaxLength (13)]
		[DefaultValue(null)]
		public string rfc_asesor
		{
			get
			{
				return ObtenerValorPropiedad<string>("rfc_asesor");
			}

			set
			{
				EstablecerValorPropiedad<string>("rfc_asesor", value);
			}

		}
		/// <summary>
		/// Obtiene o establece rfc_revisor.Sin descripcion para rfc_revisor 
		/// </summary>
		/// <value>
		/// rfc_revisor 
		/// </value>
		[Required]
		[MaxLength (13)]
		[DefaultValue(null)]
		public string rfc_revisor
		{
			get
			{
				return ObtenerValorPropiedad<string>("rfc_revisor");
			}

			set
			{
				EstablecerValorPropiedad<string>("rfc_revisor", value);
			}

		}
        /// <summary>
        /// Obtiene o establece rfc_revisor.Sin descripcion para observaciones 
        /// </summary>
        /// <value>
        /// rfc_revisor 
        /// </value>
        [Required]
        [MaxLength(1000)]
        [DefaultValue(null)]
        public string observaciones
        {
            get
            {
                return ObtenerValorPropiedad<string>("observaciones");
            }

            set
            {
                EstablecerValorPropiedad<string>("observaciones", value);
            }

        }
        //#endregion
        //#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla rp_datos_programa.		/// </summary>
		/// <param name="piid_programa">id_programa</param>
		/// <param name="psperiodo_datos">periodo_datos</param>
		public void Cargar(Int32 piid_programa,string psperiodo_datos)
		{
			base.Cargar(piid_programa,psperiodo_datos);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Byte[]> loColByteA; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(2);
			Propiedades = new Dictionary<string, Propiedad>(13);

			AgregaCampoLlave("id_programa",null);
			AgregaCampoLlave("periodo_datos",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
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
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para periodo_datos";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo_datos", loColstring); 

            //loColstring = new PropiedadesColumna<string>();
            //loColstring.Valor = null;
            //loColstring.EsPrimaryKey = false;
            //loColstring.Longitud = 13;
            //loColstring.Precision = 0;
            //loColstring.EsRequeridoBD = true;
            //loColstring.CampoId = 2;
            //loColstring.Descripcion = "Sin descripcion para rfc_datos";
            //loColstring.EsIdentity = false;
            //loColstring.Tipo = typeof(string);
            //AgregarPropiedad<string>("rfc_datos", loColstring); 

            //loColstring = new PropiedadesColumna<string>();
            //loColstring.Valor = null;
            //loColstring.EsPrimaryKey = false;
            //loColstring.Longitud = 100;
            //loColstring.Precision = 0;
            //loColstring.EsRequeridoBD = true;
            //loColstring.CampoId = 3;
            //loColstring.Descripcion = "Sin descripcion para nombre";
            //loColstring.EsIdentity = false;
            //loColstring.Tipo = typeof(string);
            //AgregarPropiedad<string>("nombre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 700;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para delimitaciones";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("delimitaciones", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 500;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para objetivo_general";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("objetivo_general", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1000;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para objetivo_especificos";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("objetivo_especificos", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 1000;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 6;
            loColstring.Descripcion = "Sin descripcion para actividades";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("actividades", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 7;
			loColInt32.Descripcion = "Sin descripcion para duracion";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("duracion", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 2000;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para justificacion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("justificacion", loColstring); 

			loColByteA = new PropiedadesColumna<byte[]>();
			loColByteA.Valor = null;
			loColByteA.EsPrimaryKey = false;
			loColByteA.Longitud = -1;
			loColByteA.Precision = 0;
			loColByteA.EsRequeridoBD = true;
			loColByteA.CampoId = 9;
			loColByteA.Descripcion = "Sin descripcion para ubicacion";
			loColByteA.EsIdentity = false;
			loColByteA.Tipo = typeof(byte[]);
			AgregarPropiedad<byte[]>("ubicacion", loColByteA); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Sin descripcion para rfc_asesor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc_asesor", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "Sin descripcion para rfc_revisor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc_revisor", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 1000;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 11;
            loColstring.Descripcion = "Sin descripcion para observaciones";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("observaciones", loColstring); 

            //loColstring = new PropiedadesColumna<string>();
            //loColstring.Valor = null;
            //loColstring.EsPrimaryKey = false;
            //loColstring.Longitud = 13;
            //loColstring.Precision = 0;
            //loColstring.EsRequeridoBD = true;
            //loColstring.CampoId = 12;
            //loColstring.Descripcion = "Sin descripcion para rfc_externo";
            //loColstring.EsIdentity = false;
            //loColstring.Tipo = typeof(string);
            //AgregarPropiedad<string>("rfc_externo", loColstring); 
			}
			#endregion

		}
	}
