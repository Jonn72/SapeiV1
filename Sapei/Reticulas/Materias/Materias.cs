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
	/// Clase materia generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Materias:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase materia.
		/// </summary>
		public Materias():base()
		{
			NombreTabla = "materias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase materia.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Materias(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "materias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece materia.Sin descripcion para materia 
		/// </summary>
		/// <value>
		/// materia 
		/// </value>
		[Key]
		[Required]
		[MaxLength (7)]
		[DefaultValue(null)]
		public string materia
		{
			get
			{
				return ObtenerValorPropiedad<string>("materia");
			}

			set
			{
				EstablecerValorPropiedad<string>("materia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nivel_escolar.Sin descripcion para nivel_escolar 
		/// </summary>
		/// <value>
		/// nivel_escolar 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string nivel_escolar
		{
			get
			{
				return ObtenerValorPropiedad<string>("nivel_escolar");
			}

			set
			{
				EstablecerValorPropiedad<string>("nivel_escolar", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_materia.Sin descripcion para tipo_materia 
		/// </summary>
		/// <value>
		/// tipo_materia 
		/// </value>
		[DefaultValue(null)]
		public Int32? tipo_materia
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("tipo_materia");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("tipo_materia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece clave_area.Sin descripcion para clave_area 
		/// </summary>
		/// <value>
		/// clave_area 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string clave_area
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave_area");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave_area", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_completo_materia.Sin descripcion para nombre_completo_materia 
		/// </summary>
		/// <value>
		/// nombre_completo_materia 
		/// </value>
		[Required]
		[MaxLength (100)]
		[DefaultValue(null)]
		public string nombre_completo_materia
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_completo_materia");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_completo_materia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_abreviado_materia.Sin descripcion para nombre_abreviado_materia 
		/// </summary>
		/// <value>
		/// nombre_abreviado_materia 
		/// </value>
		[Required]
		[MaxLength (30)]
		[DefaultValue(null)]
		public string nombre_abreviado_materia
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_abreviado_materia");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_abreviado_materia", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla materia.		/// </summary>
		/// <param name="psmateria">materia</param>
		public void Cargar(string psmateria)
		{
			base.Cargar(psmateria);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32?> loColInt32N; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(6);

			AgregaCampoLlave("materia",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 7;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para materia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("materia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para nivel_escolar";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nivel_escolar", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 2;
			loColInt32N.Descripcion = "Sin descripcion para tipo_materia";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("tipo_materia", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para clave_area";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_area", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para nombre_completo_materia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_completo_materia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para nombre_abreviado_materia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_abreviado_materia", loColstring); 
			}
			#endregion

		}
	}
