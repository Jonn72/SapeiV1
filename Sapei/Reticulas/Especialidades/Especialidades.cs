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
	/// Clase especialidade generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Especialidades:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase especialidade.
		/// </summary>
		public Especialidades():base()
		{
			NombreTabla = "especialidades";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase especialidade.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Especialidades(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "especialidades";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece especialidad.Sin descripcion para especialidad 
		/// </summary>
		/// <value>
		/// especialidad 
		/// </value>
		[Key]
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string especialidad
		{
			get
			{
				return ObtenerValorPropiedad<string>("especialidad");
			}

			set
			{
				EstablecerValorPropiedad<string>("especialidad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece carrera.Sin descripcion para carrera 
		/// </summary>
		/// <value>
		/// carrera 
		/// </value>
		[MaxLength (3)]
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
		/// <summary>
		/// Obtiene o establece reticula.Sin descripcion para reticula 
		/// </summary>
		/// <value>
		/// reticula 
		/// </value>
		[DefaultValue(null)]
		public Int32? reticula
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("reticula");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("reticula", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_especialidad.Sin descripcion para nombre_especialidad 
		/// </summary>
		/// <value>
		/// nombre_especialidad 
		/// </value>
		[Required]
		[MaxLength (70)]
		[DefaultValue(null)]
		public string nombre_especialidad
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_especialidad");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_especialidad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece periodo_inicio.Sin descripcion para periodo_inicio 
		/// </summary>
		/// <value>
		/// periodo_inicio 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string periodo_inicio
		{
			get
			{
				return ObtenerValorPropiedad<string>("periodo_inicio");
			}

			set
			{
				EstablecerValorPropiedad<string>("periodo_inicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece periodo_termino.Sin descripcion para periodo_termino 
		/// </summary>
		/// <value>
		/// periodo_termino 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string periodo_termino
		{
			get
			{
				return ObtenerValorPropiedad<string>("periodo_termino");
			}

			set
			{
				EstablecerValorPropiedad<string>("periodo_termino", value);
			}

		}
		/// <summary>
		/// Obtiene o establece creditos_optativos.Sin descripcion para creditos_optativos 
		/// </summary>
		/// <value>
		/// creditos_optativos 
		/// </value>
		[DefaultValue(null)]
		public Int32? creditos_optativos
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("creditos_optativos");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("creditos_optativos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece creditos_especialidad.Sin descripcion para creditos_especialidad 
		/// </summary>
		/// <value>
		/// creditos_especialidad 
		/// </value>
		[DefaultValue(null)]
		public Int32? creditos_especialidad
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("creditos_especialidad");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("creditos_especialidad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece clave_especialidad.Sin descripcion para clave_especialidad 
		/// </summary>
		/// <value>
		/// clave_especialidad 
		/// </value>
		[MaxLength (16)]
		[DefaultValue(null)]
		public string clave_especialidad
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave_especialidad");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave_especialidad", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla especialidade.		/// </summary>
		/// <param name="psespecialidad">especialidad</param>
		public void Cargar(string psespecialidad)
		{
			base.Cargar(psespecialidad);
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
			Propiedades = new Dictionary<string, Propiedad>(9);

			AgregaCampoLlave("especialidad",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para especialidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("especialidad", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para carrera";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("carrera", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 2;
			loColInt32N.Descripcion = "Sin descripcion para reticula";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("reticula", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 70;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para nombre_especialidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_especialidad", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para periodo_inicio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo_inicio", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para periodo_termino";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo_termino", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 6;
			loColInt32N.Descripcion = "Sin descripcion para creditos_optativos";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("creditos_optativos", loColInt32N); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 7;
			loColInt32N.Descripcion = "Sin descripcion para creditos_especialidad";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("creditos_especialidad", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 16;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para clave_especialidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_especialidad", loColstring); 
			}
			#endregion

		}
	}
