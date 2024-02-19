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
	/// Clase ss_programa generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class SS_Programa:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_programa.
		/// </summary>
		public SS_Programa():base()
		{
			NombreTabla = "ss_programa";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_programa.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public SS_Programa(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "ss_programa";
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
		/// Obtiene o establece nombre.Sin descripcion para nombre 
		/// </summary>
		/// <value>
		/// nombre 
		/// </value>
		[Required]
		[MaxLength (150)]
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
		/// Obtiene o establece correo_titular.Sin descripcion para correo_titular 
		/// </summary>
		/// <value>
		/// correo_titular 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string correo_titular
		{
			get
			{
				return ObtenerValorPropiedad<string>("correo_titular");
			}

			set
			{
				EstablecerValorPropiedad<string>("correo_titular", value);
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
		/// Obtiene o establece cargo_responsable.Sin descripcion para cargo_responsable 
		/// </summary>
		/// <value>
		/// cargo_responsable 
		/// </value>
		[Required]
		[MaxLength (150)]
		[DefaultValue(null)]
		public string cargo_responsable
		{
			get
			{
				return ObtenerValorPropiedad<string>("cargo_responsable");
			}

			set
			{
				EstablecerValorPropiedad<string>("cargo_responsable", value);
			}

		}
		/// <summary>
		/// Obtiene o establece objetivo.Sin descripcion para objetivo		/// </summary>
		/// <value>
		/// objetivo 
		/// </value>
		[Required]
		[MaxLength (200)]
		[DefaultValue(null)]
		public string objetivo
		{
			get
			{
				return ObtenerValorPropiedad<string>("objetivo");
			}

			set
			{
				EstablecerValorPropiedad<string>("objetivo", value);
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
		/// Obtiene o establece rfc.Sin descripcion para rfc 
		/// </summary>
		/// <value>
		/// rfc 
		/// </value>
		[Required]
		[MaxLength (13)]
		[DefaultValue(null)]
		public string rfc
		{
			get
			{
				return ObtenerValorPropiedad<string>("rfc");
			}

			set
			{
				EstablecerValorPropiedad<string>("rfc", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_tipo_programa.Sin descripcion para id_tipo_programa 
		/// </summary>
		/// <value>
		/// id_tipo_programa 
		/// </value>
		[Required]
		[MaxLength (5)]
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla ss_programa.		/// </summary>
		/// <param name="piid">id</param>
		/// <param name="psperiodo">periodo</param>
		public void Cargar(Int32 piid,string psperiodo)
		{
			base.Cargar(piid,psperiodo);
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
			Propiedades = new Dictionary<string, Propiedad>(10);

			AgregaCampoLlave("id",null);
			AgregaCampoLlave("periodo",null);

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
			loColstring.Descripcion = "Sin descripcion para periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 150;
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
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para correo_titular";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("correo_titular", loColstring); 

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
			loColstring.Longitud = 150;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para cargo_responsable";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("cargo_responsable", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 500;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para objetivo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("objetivo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para departamento";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("departamento", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para rfc";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "Sin descripcion para id_tipo_programa";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("id_tipo_programa", loColstring); 
			}
			#endregion

		}
	}
