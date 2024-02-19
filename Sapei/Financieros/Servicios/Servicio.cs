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
	/// Clase servicio generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Servicio:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase servicio.
		/// </summary>
		public Servicio():base()
		{
			NombreTabla = "servicios";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();
		}
		/// <summary>
		/// Inicia una nueva instancia de la clase servicio.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Servicio(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "servicios";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece clave.Sin descripcion para clave 
		/// </summary>
		/// <value>
		/// clave 
		/// </value>
		[Key]
		[Required]
		[MaxLength(10)]
		[DefaultValue(null)]
		[Display(Name = "Clave")]
		public string clave
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave", value);
			}

		}
		/// <summary>
		/// Obtiene o establece concepto.Sin descripcion para concepto 
		/// </summary>
		/// <value>
		/// concepto 
		/// </value>
		[Required]
		[MaxLength(100)]
		[DefaultValue(null)]
		[Display(Name = "Concepto")]
		public string concepto
		{
			get
			{
				return ObtenerValorPropiedad<string>("concepto");
			}

			set
			{
				EstablecerValorPropiedad<string>("concepto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece monto.Sin descripcion para monto 
		/// </summary>
		/// <value>
		/// monto 
		/// </value>
		[Required]
		[Display(Name = "Monto")]
		public Double monto
		{
			get
			{
				return ObtenerValorPropiedad<Double>("monto");
			}

			set
			{
				EstablecerValorPropiedad<Double>("monto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece dias_vigencia.Sin descripcion para dias_vigencia 
		/// </summary>
		/// <value>
		/// dias_vigencia 
		/// </value>
		[Required]
		[Display(Name = "Vigencia (Días)")]
		public Byte dias_vigencia
		{
			get
			{
				return ObtenerValorPropiedad<Byte>("dias_vigencia");
			}

			set
			{
				EstablecerValorPropiedad<Byte>("dias_vigencia", value);
			}

		}
		[Required]
		[Display(Name = "Activo")]
		public Boolean activo
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("activo");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("activo", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla servicio.		/// </summary>
		/// <param name="psclave">clave</param>
		public void Cargar(string psclave)
		{
			base.Cargar(psclave);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			PropiedadesColumna<string> loColstring;
			PropiedadesColumna<Double> loColDouble;
			PropiedadesColumna<Byte> loColByte;
			PropiedadesColumna<Boolean> loColBool;
			if (!Object.Equals(CamposLlave, null) && !Object.Equals(Propiedades, null))
				return;

			CamposLlave = new Dictionary<string, object>(1);
			Propiedades = new Dictionary<string, Propiedad>(5);

			AgregaCampoLlave("clave", null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para clave";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para concepto";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("concepto", loColstring);

			loColDouble = new PropiedadesColumna<Double>();
			loColDouble.EsPrimaryKey = false;
			loColDouble.Longitud = 8;
			loColDouble.Precision = 19;
			loColDouble.EsRequeridoBD = true;
			loColDouble.CampoId = 2;
			loColDouble.Descripcion = "Sin descripcion para monto";
			loColDouble.EsIdentity = false;
			loColDouble.Tipo = typeof(Double);
			AgregarPropiedad<Double>("monto", loColDouble);

			loColByte = new PropiedadesColumna<Byte>();
			loColByte.EsPrimaryKey = false;
			loColByte.Longitud = 1;
			loColByte.Precision = 3;
			loColByte.EsRequeridoBD = true;
			loColByte.CampoId = 3;
			loColByte.Descripcion = "Sin descripcion para dias_vigencia";
			loColByte.EsIdentity = false;
			loColByte.Tipo = typeof(Byte);
			AgregarPropiedad<Byte>("dias_vigencia", loColByte);

			loColBool = new PropiedadesColumna<Boolean>();
			loColBool.EsPrimaryKey = false;
			loColBool.EsRequeridoBD = true;
			loColBool.CampoId = 4;
			loColBool.Descripcion = "Sin descripcion para activo";
			loColBool.EsIdentity = false;
			loColBool.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("activo", loColBool);
		}
		#endregion

	}
}
