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
	/// Clase alumnos_titulado generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Alumnos_Titulado:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_titulado.
		/// </summary>
		public Alumnos_Titulado():base()
		{
			NombreTabla = "alumnos_titulados";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_titulado.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Alumnos_Titulado(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "alumnos_titulados";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
		/// </summary>
		/// <value>
		/// no_de_control 
		/// </value>
		[Key]
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public string no_de_control
		{
			get
			{
				return ObtenerValorPropiedad<string>("no_de_control");
			}

			set
			{
				EstablecerValorPropiedad<string>("no_de_control", value);
			}

		}
		/// <summary>
		/// Obtiene o establece periodo_titulacion.Sin descripcion para periodo_titulacion 
		/// </summary>
		/// <value>
		/// periodo_titulacion 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string periodo_titulacion
		{
			get
			{
				return ObtenerValorPropiedad<string>("periodo_titulacion");
			}

			set
			{
				EstablecerValorPropiedad<string>("periodo_titulacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_acto.Sin descripcion para fecha_acto 
		/// </summary>
		/// <value>
		/// fecha_acto 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_acto
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_acto");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_acto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_tipo.Sin descripcion para id_tipo 
		/// </summary>
		/// <value>
		/// id_tipo 
		/// </value>
		[Required]
		public Int32 id_tipo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_tipo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_tipo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece usuario.Sin descripcion para usuario 
		/// </summary>
		/// <value>
		/// usuario 
		/// </value>
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
		/// Obtiene o establece fecha_modificacion.Sin descripcion para fecha_modificacion 
		/// </summary>
		/// <value>
		/// fecha_modificacion 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_modificacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_modificacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_modificacion", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla alumnos_titulado.		/// </summary>
		/// <param name="psno_de_control">no_de_control</param>
		public void Cargar(string psno_de_control)
		{
			base.Cargar(psno_de_control);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<DateTime?> loColDateTimeN; 
			 PropiedadesColumna<Int32> loColInt32; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(8);

			AgregaCampoLlave("no_de_control",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para periodo_titulacion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo_titulacion", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 8;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 2;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_acto";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_acto", loColDateTimeN); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "Sin descripcion para id_tipo";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_tipo", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 8;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 5;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_modificacion";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_modificacion", loColDateTimeN); 
			}
			#endregion

		}
	}
