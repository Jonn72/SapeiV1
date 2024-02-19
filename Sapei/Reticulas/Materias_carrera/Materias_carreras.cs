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
	/// Clase materias_carrera generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Materias_carrera:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase materias_carrera.
		/// </summary>
		public Materias_carrera():base()
		{
			NombreTabla = "materias_carreras";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase materias_carrera.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Materias_carrera(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "materias_carreras";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece carrera.Sin descripcion para carrera 
		/// </summary>
		/// <value>
		/// carrera 
		/// </value>
		[Key]
		[Required]
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
		[Key]
		[Required]
		public Int32 reticula
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("reticula");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("reticula", value);
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
		/// Obtiene o establece creditos_materia.Sin descripcion para creditos_materia 
		/// </summary>
		/// <value>
		/// creditos_materia 
		/// </value>
		[DefaultValue(null)]
		public Int32? creditos_materia
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("creditos_materia");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("creditos_materia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece horas_teoricas.Sin descripcion para horas_teoricas 
		/// </summary>
		/// <value>
		/// horas_teoricas 
		/// </value>
		[Required]
		public Int32 horas_teoricas
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("horas_teoricas");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("horas_teoricas", value);
			}

		}
		/// <summary>
		/// Obtiene o establece horas_practicas.Sin descripcion para horas_practicas 
		/// </summary>
		/// <value>
		/// horas_practicas 
		/// </value>
		[Required]
		public Int32 horas_practicas
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("horas_practicas");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("horas_practicas", value);
			}

		}
		/// <summary>
		/// Obtiene o establece orden_certificado.Sin descripcion para orden_certificado 
		/// </summary>
		/// <value>
		/// orden_certificado 
		/// </value>
		[DefaultValue(null)]
		public Int32? orden_certificado
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("orden_certificado");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("orden_certificado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece semestre_reticula.Sin descripcion para semestre_reticula 
		/// </summary>
		/// <value>
		/// semestre_reticula 
		/// </value>
		[Required]
		public Int32 semestre_reticula
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("semestre_reticula");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("semestre_reticula", value);
			}

		}
		/// <summary>
		/// Obtiene o establece creditos_prerrequisito.Sin descripcion para creditos_prerrequisito 
		/// </summary>
		/// <value>
		/// creditos_prerrequisito 
		/// </value>
		[DefaultValue(null)]
		public Int32? creditos_prerrequisito
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("creditos_prerrequisito");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("creditos_prerrequisito", value);
			}

		}
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
		/// Obtiene o establece clave_oficial_materia.Sin descripcion para clave_oficial_materia 
		/// </summary>
		/// <value>
		/// clave_oficial_materia 
		/// </value>
		[MaxLength (10)]
		[DefaultValue(null)]
		public string clave_oficial_materia
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave_oficial_materia");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave_oficial_materia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estatus_materia_carrera.Sin descripcion para estatus_materia_carrera 
		/// </summary>
		/// <value>
		/// estatus_materia_carrera 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string estatus_materia_carrera
		{
			get
			{
				return ObtenerValorPropiedad<string>("estatus_materia_carrera");
			}

			set
			{
				EstablecerValorPropiedad<string>("estatus_materia_carrera", value);
			}

		}
		/// <summary>
		/// Obtiene o establece programa_estudios.Sin descripcion para programa_estudios 
		/// </summary>
		/// <value>
		/// programa_estudios 
		/// </value>
		[MaxLength (16)]
		[DefaultValue(null)]
		public string programa_estudios
		{
			get
			{
				return ObtenerValorPropiedad<string>("programa_estudios");
			}

			set
			{
				EstablecerValorPropiedad<string>("programa_estudios", value);
			}

		}
		/// <summary>
		/// Obtiene o establece renglon.Sin descripcion para renglon 
		/// </summary>
		/// <value>
		/// renglon 
		/// </value>
		[DefaultValue(null)]
		public Int32? renglon
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("renglon");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("renglon", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla materias_carrera.		/// </summary>
		/// <param name="pscarrera">carrera</param>
		/// <param name="pireticula">reticula</param>
		/// <param name="psmateria">materia</param>
		/// <param name="psespecialidad">especialidad</param>
		public void Cargar(string pscarrera,Int32 pireticula,string psmateria,string psespecialidad)
		{
			base.Cargar(pscarrera,pireticula,psmateria,psespecialidad);
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

			CamposLlave= new Dictionary<string,object>(4);
			Propiedades = new Dictionary<string, Propiedad>(14);

			AgregaCampoLlave("carrera",null);
			AgregaCampoLlave("reticula",null);
			AgregaCampoLlave("materia",null);
			AgregaCampoLlave("especialidad",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para carrera";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("carrera", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 1;
			loColInt32.Descripcion = "Sin descripcion para reticula";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("reticula", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 7;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para materia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("materia", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 3;
			loColInt32N.Descripcion = "Sin descripcion para creditos_materia";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("creditos_materia", loColInt32N); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 4;
			loColInt32.Descripcion = "Sin descripcion para horas_teoricas";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("horas_teoricas", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 5;
			loColInt32.Descripcion = "Sin descripcion para horas_practicas";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("horas_practicas", loColInt32); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 6;
			loColInt32N.Descripcion = "Sin descripcion para orden_certificado";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("orden_certificado", loColInt32N); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 7;
			loColInt32.Descripcion = "Sin descripcion para semestre_reticula";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("semestre_reticula", loColInt32); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 8;
			loColInt32N.Descripcion = "Sin descripcion para creditos_prerrequisito";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("creditos_prerrequisito", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "Sin descripcion para especialidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("especialidad", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Sin descripcion para clave_oficial_materia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_oficial_materia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "Sin descripcion para estatus_materia_carrera";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estatus_materia_carrera", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 16;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 12;
			loColstring.Descripcion = "Sin descripcion para programa_estudios";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("programa_estudios", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 13;
			loColInt32N.Descripcion = "Sin descripcion para renglon";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("renglon", loColInt32N); 
			}
			#endregion

		}
	}
