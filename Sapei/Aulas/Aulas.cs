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
	/// Clase aula generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Aulas:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase aula.
		/// </summary>
		public Aulas():base()
		{
			NombreTabla = "aulas";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase aula.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Aulas(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "aulas";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece aula.Sin descripcion para aula 
		/// </summary>
		/// <value>
		/// aula 
		/// </value>
		[Key]
		[Required]
		[MaxLength (6)]
		[DefaultValue(null)]
		public string aula
		{
			get
			{
				return ObtenerValorPropiedad<string>("aula");
			}

			set
			{
				EstablecerValorPropiedad<string>("aula", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ubicacion.Sin descripcion para ubicacion 
		/// </summary>
		/// <value>
		/// ubicacion 
		/// </value>
		[MaxLength (10)]
		[DefaultValue(null)]
		public string ubicacion
		{
			get
			{
				return ObtenerValorPropiedad<string>("ubicacion");
			}

			set
			{
				EstablecerValorPropiedad<string>("ubicacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece capacidad_aula.Sin descripcion para capacidad_aula 
		/// </summary>
		/// <value>
		/// capacidad_aula 
		/// </value>
		[Required]
		public Int32 capacidad_aula
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("capacidad_aula");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("capacidad_aula", value);
			}

		}
		/// <summary>
		/// Obtiene o establece observaciones.Sin descripcion para observaciones 
		/// </summary>
		/// <value>
		/// observaciones 
		/// </value>
		[MaxLength (255)]
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
		/// Obtiene o establece permite_cruce.Sin descripcion para permite_cruce 
		/// </summary>
		/// <value>
		/// permite_cruce 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string permite_cruce
		{
			get
			{
				return ObtenerValorPropiedad<string>("permite_cruce");
			}

			set
			{
				EstablecerValorPropiedad<string>("permite_cruce", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estatus.Sin descripcion para estatus 
		/// </summary>
		/// <value>
		/// estatus 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string estatus
		{
			get
			{
				return ObtenerValorPropiedad<string>("estatus");
			}

			set
			{
				EstablecerValorPropiedad<string>("estatus", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla aula.		/// </summary>
		/// <param name="psaula">aula</param>
		public void Cargar(string psaula)
		{
			base.Cargar(psaula);
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

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(6);

			AgregaCampoLlave("aula",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para aula";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("aula", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para ubicacion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("ubicacion", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para capacidad_aula";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("capacidad_aula", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 255;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para observaciones";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("observaciones", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para permite_cruce";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("permite_cruce", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para estatus";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estatus", loColstring); 
			}
			#endregion

		}
	}
