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
	/// Clase personal_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class PersonalSoicitud:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_solicitud.
		/// </summary>
		public PersonalSoicitud():base()
		{
			NombreTabla = "personal_solicitud";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_solicitud.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public PersonalSoicitud(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "personal_solicitud";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece id.Sin descripcion para id 
		/// </summary>
		/// <value>
		/// id 
		/// </value>
		/// 		
		[Key]
		[Required]
		public Int32 id
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id", value);
			}

		}
		/// <summary>
		/// Obtiene o establece rfc.Sin descripcion para rfc 
		/// </summary>
		/// <value>
		/// rfc 
		/// </value>
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
		/// Obtiene o establece clave_area.Sin descripcion para clave_area 
		/// </summary>
		/// <value>
		/// clave_area 
		/// </value>
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
		/// Obtiene o establece status.Sin descripcion para tipo_contrato 
		/// </summary>
		/// <value>
		/// tipo_contrato 
		/// </value>
		[DefaultValue(null)]
		public string tipo_contrato
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_contrato");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_contrato", value);
			}

		}
		/// <summary>
		/// Obtiene o establece status.Sin descripcion para status 
		/// </summary>
		/// <value>
		/// clave_puesto 
		/// </value>
		[DefaultValue(null)]
		public Int32 clave_puesto
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("clave_puesto");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("clave_puesto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece periodo.Sin descripcion para fecha_creacion 
		/// </summary>
		/// <value>
		/// fecha_creacion 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_creacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_creacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_creacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_inicio.Sin descripcion para fecha_inicio 
		/// </summary>
		/// <value>
		/// periodo_inicio 
		/// </value>
		/// 
		[DefaultValue(null)]
		public DateTime? periodo_inicio
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("periodo_inicio");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("periodo_inicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_inicio.Sin descripcion para fecha_inicio 
		/// </summary>
		/// <value>
		/// periodo_fin 
		/// </value>
		[DefaultValue(null)]
		public DateTime? periodo_fin
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("periodo_fin");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("periodo_fin", value);
			}

		}
		/// <summary>
		/// Obtiene o establece status.Sin descripcion para id_monto 
		/// </summary>
		/// <value>
		/// id_monto 
		/// </value>
		[DefaultValue(null)]
		public Int32 id_monto
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_monto");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_monto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece status.Sin descripcion para status 
		/// </summary>
		/// <value>
		/// status 
		/// </value>
		[DefaultValue(null)]
		public Int32 status
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("status");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("status", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla personal_solicitud.		/// </summary>
		/// <param name="psid">id</param>
		public void Cargar(Int32 psid)
		{
			base.Cargar(psid);
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
			Propiedades = new Dictionary<string, Propiedad>(10);

			AgregaCampoLlave("id", null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para id";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id", loColInt32);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para rfc";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para clave_area";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_area", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para tipo_contrato";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_contrato", loColstring);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.Valor = 0;
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 1;
			loColInt32.EsRequeridoBD = false;
			loColInt32.CampoId = 4;
			loColInt32.Descripcion = "Sin descripcion para clave_puesto";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("clave_puesto", loColInt32);

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = true;
			loColDateTimeN.CampoId = 5;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_creacion";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_creacion", loColDateTimeN);

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 6;
			loColDateTimeN.Descripcion = "Sin descripcion para periodo_inicio";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("periodo_inicio", loColDateTimeN);

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 7;
			loColDateTimeN.Descripcion = "Sin descripcion para periodo_fin";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("periodo_fin", loColDateTimeN);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.Valor = 0;
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 1;
			loColInt32.Precision = 1;
			loColInt32.EsRequeridoBD = false;
			loColInt32.CampoId = 8;
			loColInt32.Descripcion = "Sin descripcion para id_monto";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_monto", loColInt32);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.Valor = 0;
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 1;
			loColInt32.Precision = 1;
			loColInt32.EsRequeridoBD = false;
			loColInt32.CampoId = 9;
			loColInt32.Descripcion = "Sin descripcion para status";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("status", loColInt32);
		}
			#endregion
		}
	}
