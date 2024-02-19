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
	/// Clase plaza generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Plaza:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase plaza.
		/// </summary>
		public Plaza():base()
		{
			NombreTabla = "plazas";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase plaza.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Plaza(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "plazas";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece unidad.Sin descripcion para unidad 
		/// </summary>
		/// <value>
		/// unidad 
		/// </value>
		[Key]
		[Required]
		[MaxLength (2)]
		[DefaultValue(null)]
		public string unidad
		{
			get
			{
				return ObtenerValorPropiedad<string>("unidad");
			}

			set
			{
				EstablecerValorPropiedad<string>("unidad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece subunidad.Sin descripcion para subunidad 
		/// </summary>
		/// <value>
		/// subunidad 
		/// </value>
		[Key]
		[Required]
		[MaxLength (2)]
		[DefaultValue(null)]
		public string subunidad
		{
			get
			{
				return ObtenerValorPropiedad<string>("subunidad");
			}

			set
			{
				EstablecerValorPropiedad<string>("subunidad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece categoria.Sin descripcion para categoria 
		/// </summary>
		/// <value>
		/// categoria 
		/// </value>
		[Key]
		[Required]
		[MaxLength (7)]
		[DefaultValue(null)]
		public string categoria
		{
			get
			{
				return ObtenerValorPropiedad<string>("categoria");
			}

			set
			{
				EstablecerValorPropiedad<string>("categoria", value);
			}

		}
		/// <summary>
		/// Obtiene o establece horas.Sin descripcion para horas 
		/// </summary>
		/// <value>
		/// horas 
		/// </value>
		[Key]
		[Required]
		[MaxLength (2)]
		[DefaultValue(null)]
		public string horas
		{
			get
			{
				return ObtenerValorPropiedad<string>("horas");
			}

			set
			{
				EstablecerValorPropiedad<string>("horas", value);
			}

		}
		/// <summary>
		/// Obtiene o establece diagonal.Sin descripcion para diagonal 
		/// </summary>
		/// <value>
		/// diagonal 
		/// </value>
		[Key]
		[Required]
		[MaxLength (8)]
		[DefaultValue(null)]
		public string diagonal
		{
			get
			{
				return ObtenerValorPropiedad<string>("diagonal");
			}

			set
			{
				EstablecerValorPropiedad<string>("diagonal", value);
			}

		}
		/// <summary>
		/// Obtiene o establece partida.Sin descripcion para partida 
		/// </summary>
		/// <value>
		/// partida 
		/// </value>
		[MaxLength (4)]
		[DefaultValue(null)]
		public string partida
		{
			get
			{
				return ObtenerValorPropiedad<string>("partida");
			}

			set
			{
				EstablecerValorPropiedad<string>("partida", value);
			}

		}
		/// <summary>
		/// Obtiene o establece centro_trabajo_creacion.Sin descripcion para centro_trabajo_creacion 
		/// </summary>
		/// <value>
		/// centro_trabajo_creacion 
		/// </value>
		[MaxLength (10)]
		[DefaultValue(null)]
		public string centro_trabajo_creacion
		{
			get
			{
				return ObtenerValorPropiedad<string>("centro_trabajo_creacion");
			}

			set
			{
				EstablecerValorPropiedad<string>("centro_trabajo_creacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estatus_plaza.Sin descripcion para estatus_plaza 
		/// </summary>
		/// <value>
		/// estatus_plaza 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string estatus_plaza
		{
			get
			{
				return ObtenerValorPropiedad<string>("estatus_plaza");
			}

			set
			{
				EstablecerValorPropiedad<string>("estatus_plaza", value);
			}

		}
		/// <summary>
		/// Obtiene o establece efectos_iniciales_plaza.Sin descripcion para efectos_iniciales_plaza 
		/// </summary>
		/// <value>
		/// efectos_iniciales_plaza 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string efectos_iniciales_plaza
		{
			get
			{
				return ObtenerValorPropiedad<string>("efectos_iniciales_plaza");
			}

			set
			{
				EstablecerValorPropiedad<string>("efectos_iniciales_plaza", value);
			}

		}
		/// <summary>
		/// Obtiene o establece efectos_finales_plaza.Sin descripcion para efectos_finales_plaza 
		/// </summary>
		/// <value>
		/// efectos_finales_plaza 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string efectos_finales_plaza
		{
			get
			{
				return ObtenerValorPropiedad<string>("efectos_finales_plaza");
			}

			set
			{
				EstablecerValorPropiedad<string>("efectos_finales_plaza", value);
			}

		}
		/// <summary>
		/// Obtiene o establece documento_de_creacion.Sin descripcion para documento_de_creacion 
		/// </summary>
		/// <value>
		/// documento_de_creacion 
		/// </value>
		[MaxLength (20)]
		[DefaultValue(null)]
		public string documento_de_creacion
		{
			get
			{
				return ObtenerValorPropiedad<string>("documento_de_creacion");
			}

			set
			{
				EstablecerValorPropiedad<string>("documento_de_creacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_de_creacion.Sin descripcion para fecha_de_creacion 
		/// </summary>
		/// <value>
		/// fecha_de_creacion 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_de_creacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_de_creacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_de_creacion", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla plaza.		/// </summary>
		/// <param name="psunidad">unidad</param>
		/// <param name="pssubunidad">subunidad</param>
		/// <param name="pscategoria">categoria</param>
		/// <param name="pshoras">horas</param>
		/// <param name="psdiagonal">diagonal</param>
		public void Cargar(string psunidad,string pssubunidad,string pscategoria,string pshoras,string psdiagonal)
		{
			base.Cargar(psunidad,pssubunidad,pscategoria,pshoras,psdiagonal);
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

			CamposLlave= new Dictionary<string,object>(5);
			Propiedades = new Dictionary<string, Propiedad>(12);

			AgregaCampoLlave("unidad",null);
			AgregaCampoLlave("subunidad",null);
			AgregaCampoLlave("categoria",null);
			AgregaCampoLlave("horas",null);
			AgregaCampoLlave("diagonal",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 2;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para unidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("unidad", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 2;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para subunidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("subunidad", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 7;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para categoria";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("categoria", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 2;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para horas";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("horas", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 8;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para diagonal";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("diagonal", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 4;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para partida";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("partida", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para centro_trabajo_creacion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("centro_trabajo_creacion", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para estatus_plaza";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estatus_plaza", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para efectos_iniciales_plaza";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("efectos_iniciales_plaza", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "Sin descripcion para efectos_finales_plaza";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("efectos_finales_plaza", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 20;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Sin descripcion para documento_de_creacion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("documento_de_creacion", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 11;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_de_creacion";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_de_creacion", loColDateTimeN); 
			}
			#endregion

		}
	}
