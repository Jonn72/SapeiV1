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
	/// Clase fecha_evaluacion generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class fecha_evaluacion:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase fecha_evaluacion.
		/// </summary>
		public fecha_evaluacion():base()
		{
			NombreTabla = "fecha_evaluacion";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase fecha_evaluacion.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public fecha_evaluacion(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "fecha_evaluacion";
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
		/// Obtiene o establece encuesta.Sin descripcion para encuesta 
		/// </summary>
		/// <value>
		/// encuesta 
		/// </value>
		[Key]
		[Required]
		[MaxLength (1)]
		[DefaultValue(null)]
		public string encuesta
		{
			get
			{
				return ObtenerValorPropiedad<string>("encuesta");
			}

			set
			{
				EstablecerValorPropiedad<string>("encuesta", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_inicio.Sin descripcion para fecha_inicio 
		/// </summary>
		/// <value>
		/// fecha_inicio 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_inicio
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_inicio");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_inicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_fin.Sin descripcion para fecha_fin 
		/// </summary>
		/// <value>
		/// fecha_fin 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_fin
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_fin");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_fin", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_programada.Sin descripcion para fecha_programada 
		/// </summary>
		/// <value>
		/// fecha_programada 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_programada
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_programada");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_programada", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla fecha_evaluacion.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="psencuesta">encuesta</param>
		public void Cargar(string psperiodo,string psencuesta)
		{
			base.Cargar(psperiodo,psencuesta);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<DateTime?> loColDateTimeN; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(2);
			Propiedades = new Dictionary<string, Propiedad>(5);

			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("encuesta",null);

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
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para encuesta";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("encuesta", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 2;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_inicio";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_inicio", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 3;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_fin";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_fin", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 4;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_programada";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_programada", loColDateTimeN); 
			}
			#endregion

		}
	}
