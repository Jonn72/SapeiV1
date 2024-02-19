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
	/// Clase personal_horario_indice generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class PersonalHorarioIndice : CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_horario_indice.
		/// </summary>
		public PersonalHorarioIndice():base()
		{
			NombreTabla = "personal_horario_indice";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_horario_indice.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public PersonalHorarioIndice(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "personal_horario_indice";
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
		/// Obtiene o establece rfc.Sin descripcion para rfc 
		/// </summary>
		/// <value>
		/// rfc 
		/// </value>
		[Key]
		[Required]
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
		/// Obtiene o establece clave_area.Sin descripcion para clave_area 
		/// </summary>
		/// <value>
		/// clave_area 
		/// </value>
		[Key]
		[Required]
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
		/// Obtiene o establece status.Sin descripcion para tipo_contrato 
		/// </summary>
		/// <value>
		/// tipo_contrato 
		/// </value>
		[Key]
		[Required]
		[MaxLength(1)]
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
		/// Obtiene o establece Id_doc.Sin descripcion para Id_doc 
		/// </summary>
		/// <value>
		/// Id_doc 
		/// </value>
		[Required]
		public Int32 Id_doc
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("Id_doc");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("Id_doc", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_registro.Sin descripcion para fecha_inicio 
		/// </summary>
		/// <value>
		/// periodo_inicio 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_registro
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_registro");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_registro", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla personal_horario_indice.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="psrfc">rfc</param>
		/// <param name="psclave_area">clave_area</param>
		public void Cargar(string psperiodo,string psrfc,string psclave_area)
		{
			base.Cargar(psperiodo,psrfc,psclave_area);
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

			CamposLlave= new Dictionary<string,object>(3);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("rfc",null);
			AgregaCampoLlave("clave_area",null);
			AgregaCampoLlave("tipo_contrato", null);

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
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 4;
			loColInt32.Descripcion = "Sin descripcion para Id_doc";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("Id_doc", loColInt32); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 5;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_registro";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_registro", loColDateTimeN);
			#endregion
		}
	}
}
