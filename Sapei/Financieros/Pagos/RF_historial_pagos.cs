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
	/// Clase aviso_reinscripcion_pago generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class RF_historial_pagos : CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase aviso_reinscripcion_pago.
		/// </summary>
		public RF_historial_pagos():base()
		{
			NombreTabla = "rf_historial_pagos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase aviso_reinscripcion_pago.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public RF_historial_pagos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "rf_historial_pagos";
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
		[MaxLength(5)]
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
		/// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
		/// </summary>
		/// <value>
		/// no_de_control 
		/// </value>
		[Required]
		[MaxLength(10)]
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
		/// Obtiene o establece fecha_corte.Sin descripcion para fecha_corte 
		/// </summary>
		/// <value>
		/// fecha_corte 
		/// </value>
		[Required]
		[MaxLength(6)]
		[DefaultValue(null)]
		public string fecha_corte
		{
			get
			{
				return ObtenerValorPropiedad<string>("fecha_corte");
			}

			set
			{
				EstablecerValorPropiedad<string>("fecha_corte", value);
			}

		}
		/// <summary>
		/// Obtiene o establece referencia_1.Sin descripcion para referencia_1 
		/// </summary>
		/// <value>
		/// referencia_1 
		/// </value>
		[Required]
		[MaxLength(20)]
		[DefaultValue(null)]
		public string referencia_1
		{
			get
			{
				return ObtenerValorPropiedad<string>("referencia_1");
			}

			set
			{
				EstablecerValorPropiedad<string>("referencia_1", value);
			}

		}
		/// <summary>
		/// Obtiene o establece sucursal.Sin descripcion para sucursal 
		/// </summary>
		/// <value>
		/// sucursal 
		/// </value>
		[Required]
		[MaxLength(3)]
		[DefaultValue(null)]
		public string sucursal
		{
			get
			{
				return ObtenerValorPropiedad<string>("sucursal");
			}

			set
			{
				EstablecerValorPropiedad<string>("sucursal", value);
			}

		}
		/// <summary>
		/// Obtiene o establece importe.Sin descripcion para importe 
		/// </summary>
		/// <value>
		/// importe 
		/// </value>
		[Required]
		public Double importe
		{
			get
			{
				return ObtenerValorPropiedad<Double>("importe");
			}

			set
			{
				EstablecerValorPropiedad<Double>("importe", value);
			}

		}
		/// <summary>
		/// Obtiene o establece autorizacion.Sin descripcion para autorizacion 
		/// </summary>
		/// <value>
		/// autorizacion 
		/// </value>
		[Required]
		[MaxLength(10)]
		[DefaultValue(null)]
		public string autorizacion
		{
			get
			{
				return ObtenerValorPropiedad<string>("autorizacion");
			}

			set
			{
				EstablecerValorPropiedad<string>("autorizacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece forma_pago.Sin descripcion para forma_pago 
		/// </summary>
		/// <value>
		/// forma_pago 
		/// </value>
		[Required]
		[MaxLength(2)]
		[DefaultValue(null)]
		public string forma_pago
		{
			get
			{
				return ObtenerValorPropiedad<string>("forma_pago");
			}

			set
			{
				EstablecerValorPropiedad<string>("forma_pago", value);
			}

		}

		/// <summary>
		/// Obtiene o establece fecha_registro.Sin descripcion para fecha_registro 
		/// </summary>
		/// <value>
		/// fecha_registro 
		/// </value>
		[Required]
		public DateTime fecha_registro
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_registro");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_registro", value);
			}

		}
		/// <summary>
		/// Obtiene o establece usuario.Sin descripcion para usuario 
		/// </summary>
		/// <value>
		/// usuario 
		/// </value>
		[Required]
		[MaxLength(30)]
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla aviso_reinscripcion_pago.		/// </summary>
		public void Cargar()
		{
			base.Cargar();
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			PropiedadesColumna<string> loColstring;
			PropiedadesColumna<Double> loColDouble;
			PropiedadesColumna<Boolean> loColBoolean;
			PropiedadesColumna<DateTime> loColDateTime;
			if (!Object.Equals(CamposLlave, null) && !Object.Equals(Propiedades, null))
				return;

			CamposLlave = new Dictionary<string, object>(0);
			Propiedades = new Dictionary<string, Propiedad>(10);


			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
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
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para fecha_corte";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("fecha_corte", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 20;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para referencia_1";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("referencia_1", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para sucursal";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("sucursal", loColstring);

			loColDouble = new PropiedadesColumna<Double>();
			loColDouble.EsPrimaryKey = false;
			loColDouble.Longitud = 8;
			loColDouble.Precision = 19;
			loColDouble.EsRequeridoBD = true;
			loColDouble.CampoId = 5;
			loColDouble.Descripcion = "Sin descripcion para importe";
			loColDouble.EsIdentity = false;
			loColDouble.Tipo = typeof(Double);
			AgregarPropiedad<Double>("importe", loColDouble);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para autorizacion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("autorizacion", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 2;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para forma_pago";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("forma_pago", loColstring);

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 9;
			loColDateTime.Descripcion = "Sin descripcion para fecha_registro";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_registro", loColDateTime);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Sin descripcion para usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring);
		}
		#endregion

	}
}
