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
	/// Clase cle_grupo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Cle_Grupos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase cle_grupo.
		/// </summary>
		public Cle_Grupos():base()
		{
			NombreTabla = "cle_grupos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase cle_grupo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Cle_Grupos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "cle_grupos";
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
		/// Obtiene o establece nivel.Sin descripcion para nivel 
		/// </summary>
		/// <value>
		/// nivel 
		/// </value>
		[Key]
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string nivel
		{
			get
			{
				return ObtenerValorPropiedad<string>("nivel");
			}

			set
			{
				EstablecerValorPropiedad<string>("nivel", value);
			}

		}
		/// <summary>
		/// Obtiene o establece grupo.Sin descripcion para grupo 
		/// </summary>
		/// <value>
		/// grupo 
		/// </value>
		[Key]
		[Required]
		[MaxLength (6)]
		[DefaultValue(null)]
		public string grupo
		{
			get
			{
				return ObtenerValorPropiedad<string>("grupo");
			}

			set
			{
				EstablecerValorPropiedad<string>("grupo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece capacidad.Sin descripcion para capacidad 
		/// </summary>
		/// <value>
		/// capacidad 
		/// </value>
		[Required]
		[DefaultValue("0")]
		public Int16 capacidad
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("capacidad");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("capacidad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inscritos.Sin descripcion para inscritos 
		/// </summary>
		/// <value>
		/// inscritos 
		/// </value>
		[Required]
		[DefaultValue("0")]
		public Int16 inscritos
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("inscritos");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("inscritos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece registrados.Sin descripcion para registrados 
		/// </summary>
		/// <value>
		/// registrados 
		/// </value>
		[Required]
		[DefaultValue("0")]
		public Int16 registrados
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("registrados");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("registrados", value);
			}

		}
		/// <summary>
		/// Obtiene o establece altas.Sin descripcion para altas 
		/// </summary>
		/// <value>
		/// altas 
		/// </value>
		[Required]
		[DefaultValue("0")]
		public Int16 altas
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("altas");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("altas", value);
			}

		}
		/// <summary>
		/// Obtiene o establece bajas.Sin descripcion para bajas 
		/// </summary>
		/// <value>
		/// bajas 
		/// </value>
		[Required]
		[DefaultValue("0")]
		public Int16 bajas
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("bajas");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("bajas", value);
			}

		}


		/// <summary>
		/// Obtiene o establece facilitador.Sin descripcion para facilitador 
		/// </summary>
		/// <value>
		/// facilitador 
		/// </value>
		[DefaultValue(null)]
		public string facilitador
		{
			get
			{
				return ObtenerValorPropiedad<string>("facilitador");
			}

			set
			{
				EstablecerValorPropiedad<string>("facilitador", value);
			}
		}

			/// <summary>
			/// Obtiene o establece observaciones.Sin descripcion para observaciones 
			/// </summary>
			/// <value>
			/// observaciones 
			/// </value>
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
		/// <summary>
		/// Obtiene o establece fecha_carga_calificacion.Sin descripcion para fecha_carga_calificacion 
		/// </summary>
		/// <value>
		/// fecha_carga_calificacion 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_carga_calificacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_carga_calificacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_carga_calificacion", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla cle_grupo.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="psnivel">nivel</param>
		/// <param name="psgrupo">grupo</param>
		public void Cargar(string psperiodo,string psnivel,string psgrupo)
		{
			base.Cargar(psperiodo,psnivel,psgrupo);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int16> loColInt16; 
			 PropiedadesColumna<DateTime?> loColDateTimeN; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(3);
			Propiedades = new Dictionary<string, Propiedad>(11);

			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("nivel",null);
			AgregaCampoLlave("grupo",null);

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

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para nivel";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nivel", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para grupo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("grupo", loColstring); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.Valor = 0;
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 3;
			loColInt16.Descripcion = "Sin descripcion para capacidad";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("capacidad", loColInt16); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.Valor = 0;
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 4;
			loColInt16.Descripcion = "Sin descripcion para inscritos";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("inscritos", loColInt16); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.Valor = 0;
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 5;
			loColInt16.Descripcion = "Sin descripcion para registrados";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("registrados", loColInt16); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.Valor = 0;
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 6;
			loColInt16.Descripcion = "Sin descripcion para altas";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("altas", loColInt16); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.Valor = 0;
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 7;
			loColInt16.Descripcion = "Sin descripcion para bajas";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("bajas", loColInt16);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = -1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para facilitador";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("facilitador", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = -1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "Sin descripcion para observaciones";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("observaciones", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 8;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 10;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_carga_calificacion";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_carga_calificacion", loColDateTimeN); 
			}
			#endregion

		}
	}
