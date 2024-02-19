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
	/// Clase grupo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Grupos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase grupo.
		/// </summary>
		public Grupos():base()
		{
			NombreTabla = "grupos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase grupo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Grupos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "grupos";
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
		/// Obtiene o establece grupo.Sin descripcion para grupo 
		/// </summary>
		/// <value>
		/// grupo 
		/// </value>
		[Key]
		[Required]
		[MaxLength (3)]
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
		/// Obtiene o establece estatus_grupo.Sin descripcion para estatus_grupo 
		/// </summary>
		/// <value>
		/// estatus_grupo 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string estatus_grupo
		{
			get
			{
				return ObtenerValorPropiedad<string>("estatus_grupo");
			}

			set
			{
				EstablecerValorPropiedad<string>("estatus_grupo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece capacidad_grupo.Sin descripcion para capacidad_grupo 
		/// </summary>
		/// <value>
		/// capacidad_grupo 
		/// </value>
		[Required]
		public Int32 capacidad_grupo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("capacidad_grupo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("capacidad_grupo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece alumnos_inscritos.Sin descripcion para alumnos_inscritos 
		/// </summary>
		/// <value>
		/// alumnos_inscritos 
		/// </value>
		[DefaultValue(null)]
		public Int32? alumnos_inscritos
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("alumnos_inscritos");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("alumnos_inscritos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece folio_acta.Sin descripcion para folio_acta 
		/// </summary>
		/// <value>
		/// folio_acta 
		/// </value>
		[MaxLength (12)]
		[DefaultValue(null)]
		public string folio_acta
		{
			get
			{
				return ObtenerValorPropiedad<string>("folio_acta");
			}

			set
			{
				EstablecerValorPropiedad<string>("folio_acta", value);
			}

		}
		/// <summary>
		/// Obtiene o establece paralelo_de.Sin descripcion para paralelo_de 
		/// </summary>
		/// <value>
		/// paralelo_de 
		/// </value>
		[MaxLength (10)]
		[DefaultValue(null)]
		public string paralelo_de
		{
			get
			{
				return ObtenerValorPropiedad<string>("paralelo_de");
			}

			set
			{
				EstablecerValorPropiedad<string>("paralelo_de", value);
			}

		}
		/// <summary>
		/// Obtiene o establece exclusivo_carrera.Sin descripcion para exclusivo_carrera 
		/// </summary>
		/// <value>
		/// exclusivo_carrera 
		/// </value>
		[MaxLength (3)]
		[DefaultValue(null)]
		public string exclusivo_carrera
		{
			get
			{
				return ObtenerValorPropiedad<string>("exclusivo_carrera");
			}

			set
			{
				EstablecerValorPropiedad<string>("exclusivo_carrera", value);
			}

		}
		/// <summary>
		/// Obtiene o establece exclusivo_reticula.Sin descripcion para exclusivo_reticula 
		/// </summary>
		/// <value>
		/// exclusivo_reticula 
		/// </value>
		[DefaultValue(null)]
		public Int32? exclusivo_reticula
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("exclusivo_reticula");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("exclusivo_reticula", value);
			}

		}
		/// <summary>
		/// Obtiene o establece rfc.Sin descripcion para rfc 
		/// </summary>
		/// <value>
		/// rfc 
		/// </value>
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
		/// Obtiene o establece seleccionado_en_bloque.Sin descripcion para seleccionado_en_bloque 
		/// </summary>
		/// <value>
		/// seleccionado_en_bloque 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string seleccionado_en_bloque
		{
			get
			{
				return ObtenerValorPropiedad<string>("seleccionado_en_bloque");
			}

			set
			{
				EstablecerValorPropiedad<string>("seleccionado_en_bloque", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_personal.Sin descripcion para tipo_personal 
		/// </summary>
		/// <value>
		/// tipo_personal 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string tipo_personal
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_personal");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_personal", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla grupo.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="psmateria">materia</param>
		/// <param name="psgrupo">grupo</param>
		public void Cargar(string psperiodo,string psmateria,string psgrupo)
		{
			base.Cargar(psperiodo,psmateria,psgrupo);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<Int32?> loColInt32N; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(3);
			Propiedades = new Dictionary<string, Propiedad>(13);

			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("materia",null);
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
			loColstring.Longitud = 7;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para materia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("materia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para grupo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("grupo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para estatus_grupo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estatus_grupo", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 4;
			loColInt32.Descripcion = "Sin descripcion para capacidad_grupo";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("capacidad_grupo", loColInt32); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 5;
			loColInt32N.Descripcion = "Sin descripcion para alumnos_inscritos";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("alumnos_inscritos", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 12;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para folio_acta";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("folio_acta", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para paralelo_de";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("paralelo_de", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para exclusivo_carrera";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("exclusivo_carrera", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 9;
			loColInt32N.Descripcion = "Sin descripcion para exclusivo_reticula";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("exclusivo_reticula", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Sin descripcion para rfc";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "Sin descripcion para seleccionado_en_bloque";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("seleccionado_en_bloque", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 12;
			loColstring.Descripcion = "Sin descripcion para tipo_personal";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_personal", loColstring); 
			}
			#endregion

		}
	}
