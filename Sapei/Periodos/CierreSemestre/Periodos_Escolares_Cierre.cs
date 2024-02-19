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
	/// Clase periodos_escolares_cierre generada automáticamente desde el Generador de Código Sapei
	/// </summary>
	[Serializable]
	public partial class Periodos_Escolares_Cierre:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase periodos_escolares_cierre.
		/// </summary>
		public Periodos_Escolares_Cierre():base()
		{
			NombreTabla = "periodos_escolares_cierre";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase periodos_escolares_cierre.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Periodos_Escolares_Cierre(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "periodos_escolares_cierre";
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
		/// Obtiene o establece usuario.Sin descripcion para usuario 
		/// </summary>
		/// <value>
		/// usuario 
		/// </value>
		[Required]
		[MaxLength (30)]
		[DefaultValue(null)]
		public string usuario
		{
			get
			{
				return ObtenerValorPropiedad<string>("usuario");
			}

			set
			{
				EstablecerValorPropiedad<string>("usuario", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha.Sin descripcion para fecha 
		/// </summary>
		/// <value>
		/// fecha 
		/// </value>
		[Required]
		public DateTime fecha
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha", value);
			}

		}
		/// <summary>
		/// Obtiene o establece paso1_historia_alumnos.Sin descripcion para paso1_historia_alumnos 
		/// </summary>
		/// <value>
		/// paso1_historia_alumnos 
		/// </value>
		[Required]
		public Boolean paso1_historia_alumnos
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("paso1_historia_alumnos");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("paso1_historia_alumnos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece paso1_procesados.Sin descripcion para paso1_procesados 
		/// </summary>
		/// <value>
		/// paso1_procesados 
		/// </value>
		[Required]
		public Int32 paso1_procesados
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("paso1_procesados");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("paso1_procesados", value);
			}

		}
		/// <summary>
		/// Obtiene o establece paso2_acumulado.Sin descripcion para paso2_acumulado 
		/// </summary>
		/// <value>
		/// paso2_acumulado 
		/// </value>
		[Required]
		public Boolean paso2_acumulado
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("paso2_acumulado");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("paso2_acumulado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece paso2_procesados.Sin descripcion para paso2_procesados 
		/// </summary>
		/// <value>
		/// paso2_procesados 
		/// </value>
		[Required]
		public Int32 paso2_procesados
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("paso2_procesados");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("paso2_procesados", value);
			}

		}
		/// <summary>
		/// Obtiene o establece paso3_avisos.Sin descripcion para paso3_avisos 
		/// </summary>
		/// <value>
		/// paso3_avisos 
		/// </value>
		[Required]
		public Boolean paso3_avisos
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("paso3_avisos");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("paso3_avisos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece paso3_procesados.Sin descripcion para paso3_procesados 
		/// </summary>
		/// <value>
		/// paso3_procesados 
		/// </value>
		[Required]
		public Int32 paso3_procesados
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("paso3_procesados");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("paso3_procesados", value);
			}

		}
		/// <summary>
		/// Obtiene o establece paso4_cierre.Sin descripcion para paso4_cierre 
		/// </summary>
		/// <value>
		/// paso4_cierre 
		/// </value>
		[Required]
		public Boolean paso4_cierre
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("paso4_cierre");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("paso4_cierre", value);
			}

		}
		[MaxLength(100)]
		[DefaultValue(null)]
		public string mensaje
		{
			get
			{
				return ObtenerValorPropiedad<string>("mensaje");
			}

			set
			{
				EstablecerValorPropiedad<string>("mensaje", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla periodos_escolare.		/// </summary>
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
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<Boolean> loColBoolean; 
			 PropiedadesColumna<Int32> loColInt32; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave = new Dictionary<string, object>(1);
			Propiedades = new Dictionary<string, Propiedad>(11);

			AgregaCampoLlave("periodo", null);

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
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
			loColDateTime.Descripcion = "Sin descripcion para fecha";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha", loColDateTime); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 3;
			loColBoolean.Descripcion = "Sin descripcion para paso1_historia_alumnos";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("paso1_historia_alumnos", loColBoolean); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 4;
			loColInt32.Descripcion = "Sin descripcion para paso1_procesados";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("paso1_procesados", loColInt32); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 5;
			loColBoolean.Descripcion = "Sin descripcion para paso2_acumulado";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("paso2_acumulado", loColBoolean); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 6;
			loColInt32.Descripcion = "Sin descripcion para paso2_procesados";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("paso2_procesados", loColInt32); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 7;
			loColBoolean.Descripcion = "Sin descripcion para paso3_avisos";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("paso3_avisos", loColBoolean); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 8;
			loColInt32.Descripcion = "Sin descripcion para paso3_procesados";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("paso3_procesados", loColInt32); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 9;
			loColBoolean.Descripcion = "Sin descripcion para paso4_cierre";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("paso4_cierre", loColBoolean);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Sin descripcion para usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("mensaje", loColstring);
		}
			#endregion

		}
	}
