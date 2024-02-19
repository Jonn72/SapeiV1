using System;
using System.Collections.Generic;
using System.Collections;

using Sapei.Framework;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sapei
{
	/// <summary>
	/// Clase alumnos_historial_servicio generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Alumnos_Historial_Servicios:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_historial_servicio.
		/// </summary>
		public Alumnos_Historial_Servicios():base()
		{
			NombreTabla = "alumnos_historial_servicios";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_historial_servicio.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Alumnos_Historial_Servicios(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "alumnos_historial_servicios";
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
		/// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
		/// </summary>
		/// <value>
		/// no_de_control 
		/// </value>
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
		/// Obtiene o establece servicio.Sin descripcion para servicio 
		/// </summary>
		/// <value>
		/// servicio 
		/// </value>
		[MaxLength (10)]
		[DefaultValue(null)]
		public string servicio
		{
			get
			{
				return ObtenerValorPropiedad<string>("servicio");
			}

			set
			{
				EstablecerValorPropiedad<string>("servicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece linea_captura.Sin descripcion para linea_captura 
		/// </summary>
		/// <value>
		/// linea_captura 
		/// </value>
		[Required]
		[MaxLength (46)]
		[DefaultValue(null)]
		public string linea_captura
		{
			get
			{
				return ObtenerValorPropiedad<string>("linea_captura");
			}

			set
			{
				EstablecerValorPropiedad<string>("linea_captura", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_solicitud.Sin descripcion para fecha_solicitud 
		/// </summary>
		/// <value>
		/// fecha_solicitud 
		/// </value>
		[Required]
		public DateTime fecha_solicitud
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_solicitud");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_solicitud", value);
			}

		}
		/// <summary>
		/// Obtiene o establece pago_registrado.Sin descripcion para pago_registrado 
		/// </summary>
		/// <value>
		/// pago_registrado 
		/// </value>
		[Required]
		[DefaultValue(false)]
		public Boolean pago_registrado
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("pago_registrado");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("pago_registrado", value);
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
		/// Obtiene o establece fecha_pago.Sin descripcion para fecha_pago 
		/// </summary>
		/// <value>
		/// fecha_pago 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_pago
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_pago");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_pago", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla alumnos_historial_servicio.		/// </summary>
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
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<Boolean> loColBoolean; 
			 PropiedadesColumna<DateTime?> loColDateTimeN; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(0);
			Propiedades = new Dictionary<string, Propiedad>(8);


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
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para servicio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("servicio", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 46;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para linea_captura";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("linea_captura", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 4;
			loColDateTime.Descripcion = "Sin descripcion para fecha_solicitud";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_solicitud", loColDateTime); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.Valor = false;
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 5;
			loColBoolean.Descripcion = "Sin descripcion para pago_registrado";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("pago_registrado", loColBoolean); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 6;
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
			loColDateTimeN.CampoId = 7;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_pago";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_pago", loColDateTimeN); 
			}
			#endregion

		}
	}
