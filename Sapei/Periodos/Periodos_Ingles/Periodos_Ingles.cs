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
	/// Clase periodos_ingle generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Periodos_Ingles:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase periodos_ingle.
		/// </summary>
		public Periodos_Ingles():base()
		{
			NombreTabla = "periodos_ingles";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase periodos_ingle.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Periodos_Ingles(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "periodos_ingles";
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
		/// Obtiene o establece ini_registro_grupos.Sin descripcion para ini_registro_grupos 
		/// </summary>
		/// <value>
		/// ini_registro_grupos 
		/// </value>
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public DateTime ini_registro_grupos
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("ini_registro_grupos");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("ini_registro_grupos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_registro_grupos.Sin descripcion para fin_registro_grupos 
		/// </summary>
		/// <value>
		/// fin_registro_grupos 
		/// </value>
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public DateTime fin_registro_grupos
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_registro_grupos");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_registro_grupos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ini_seleccion.Sin descripcion para ini_seleccion 
		/// </summary>
		/// <value>
		/// ini_seleccion 
		/// </value>
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public DateTime ini_seleccion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("ini_seleccion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("ini_seleccion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_seleccion.Sin descripcion para fin_seleccion 
		/// </summary>
		/// <value>
		/// fin_seleccion 
		/// </value>
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public DateTime fin_seleccion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_seleccion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_seleccion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ini_captura_calif.Sin descripcion para ini_captura_calif 
		/// </summary>
		/// <value>
		/// ini_captura_calif 
		/// </value>
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public DateTime ini_captura_calif
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("ini_captura_calif");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("ini_captura_calif", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_captura_calif.Sin descripcion para fin_captura_calif 
		/// </summary>
		/// <value>
		/// fin_captura_calif 
		/// </value>
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public DateTime fin_captura_calif
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_captura_calif");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_captura_calif", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla periodos_ingle.		/// </summary>
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
			PropiedadesColumna<DateTime> loColDate;
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(7);

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

			loColDate = new PropiedadesColumna<DateTime>();
			loColDate.EsPrimaryKey = false;
			loColDate.Longitud = 10;
			loColDate.Precision = 10;
			loColDate.EsRequeridoBD = true;
			loColDate.CampoId = 1;
			loColDate.Descripcion = "Sin descripcion para ini_registro_grupos";
			loColDate.EsIdentity = false;
			loColDate.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("ini_registro_grupos", loColDate); 

			loColDate = new PropiedadesColumna<DateTime>();
			loColDate.EsPrimaryKey = false;
			loColDate.Longitud = 10;
			loColDate.Precision = 10;
			loColDate.EsRequeridoBD = true;
			loColDate.CampoId = 2;
			loColDate.Descripcion = "Sin descripcion para fin_registro_grupos";
			loColDate.EsIdentity = false;
			loColDate.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_registro_grupos", loColDate); 

			loColDate = new PropiedadesColumna<DateTime>();
			loColDate.EsPrimaryKey = false;
			loColDate.Longitud = 10;
			loColDate.Precision = 10;
			loColDate.EsRequeridoBD = true;
			loColDate.CampoId = 3;
			loColDate.Descripcion = "Sin descripcion para ini_seleccion";
			loColDate.EsIdentity = false;
			loColDate.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("ini_seleccion", loColDate); 

			loColDate = new PropiedadesColumna<DateTime>();
			loColDate.EsPrimaryKey = false;
			loColDate.Longitud = 10;
			loColDate.Precision = 10;
			loColDate.EsRequeridoBD = true;
			loColDate.CampoId = 4;
			loColDate.Descripcion = "Sin descripcion para fin_seleccion";
			loColDate.EsIdentity = false;
			loColDate.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_seleccion", loColDate); 

			loColDate = new PropiedadesColumna<DateTime>();
			loColDate.EsPrimaryKey = false;
			loColDate.Longitud = 10;
			loColDate.Precision = 10;
			loColDate.EsRequeridoBD = true;
			loColDate.CampoId = 5;
			loColDate.Descripcion = "Sin descripcion para ini_captura_calif";
			loColDate.EsIdentity = false;
			loColDate.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("ini_captura_calif", loColDate); 

			loColDate = new PropiedadesColumna<DateTime>();
			loColDate.EsPrimaryKey = false;
			loColDate.Longitud = 10;
			loColDate.Precision = 10;
			loColDate.EsRequeridoBD = true;
			loColDate.CampoId = 6;
			loColDate.Descripcion = "Sin descripcion para fin_captura_calif";
			loColDate.EsIdentity = false;
			loColDate.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_captura_calif", loColDate); 
			}
			#endregion

		}
	}
