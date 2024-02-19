using System;
using System.Collections.Generic;
using Sapei.Framework;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sapei
{
	/// <summary>
	/// Clase alumnos_historial_pago generada automáticamente desde el Generador de Código Sapei
	/// </summary>
	[Serializable]
	public partial class Alumnos_Historial_Pagos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_historial_pago.
		/// </summary>
		public Alumnos_Historial_Pagos():base()
		{
			NombreTabla = "alumnos_historial_pagos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_historial_pago.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Alumnos_Historial_Pagos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "alumnos_historial_pagos";
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
		/// Obtiene o establece clave.Sin descripcion para clave 
		/// </summary>
		/// <value>
		/// clave 
		/// </value>
		[Required]
		public Int16 clave
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("clave");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("clave", value);
			}

		}
		/// <summary>
		/// Obtiene o establece monto.Sin descripcion para monto 
		/// </summary>
		/// <value>
		/// monto 
		/// </value>
		[Required]
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
		/// Obtiene o establece abonado.Sin descripcion para abonado 
		/// </summary>
		/// <value>
		/// abonado 
		/// </value>
		[Required]
		[DefaultValue("0.00")]
		public Double abonado
		{
			get
			{
				return ObtenerValorPropiedad<Double>("abonado");
			}

			set
			{
				EstablecerValorPropiedad<Double>("abonado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece condonacion.Sin descripcion para condonacion 
		/// </summary>
		/// <value>
		/// condonacion 
		/// </value>
		[Required]
		[DefaultValue("0.00")]
		public Double condonacion
		{
			get
			{
				return ObtenerValorPropiedad<Double>("condonacion");
			}

			set
			{
				EstablecerValorPropiedad<Double>("condonacion", value);
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
		[DefaultValue(null)]
		public DateTime? fecha
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla alumnos_historial_pago.		/// </summary>
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
			 PropiedadesColumna<Int16> loColInt16; 
			 PropiedadesColumna<Double> loColDouble; 
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

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 2;
			loColInt16.Descripcion = "Sin descripcion para clave";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("clave", loColInt16); 

			loColDouble = new PropiedadesColumna<Double>();
			loColDouble.EsPrimaryKey = false;
			loColDouble.Longitud = 8;
			loColDouble.Precision = 19;
			loColDouble.EsRequeridoBD = true;
			loColDouble.CampoId = 3;
			loColDouble.Descripcion = "Sin descripcion para monto";
			loColDouble.EsIdentity = false;
			loColDouble.Tipo = typeof(Double);
			AgregarPropiedad<Double>("monto", loColDouble); 

			loColDouble = new PropiedadesColumna<Double>();
			loColDouble.Valor = 0.00;
			loColDouble.EsPrimaryKey = false;
			loColDouble.Longitud = 8;
			loColDouble.Precision = 19;
			loColDouble.EsRequeridoBD = true;
			loColDouble.CampoId = 4;
			loColDouble.Descripcion = "Sin descripcion para abonado";
			loColDouble.EsIdentity = false;
			loColDouble.Tipo = typeof(Double);
			AgregarPropiedad<Double>("abonado", loColDouble); 

			loColDouble = new PropiedadesColumna<Double>();
			loColDouble.Valor = 0.00;
			loColDouble.EsPrimaryKey = false;
			loColDouble.Longitud = 8;
			loColDouble.Precision = 19;
			loColDouble.EsRequeridoBD = true;
			loColDouble.CampoId = 5;
			loColDouble.Descripcion = "Sin descripcion para condonacion";
			loColDouble.EsIdentity = false;
			loColDouble.Tipo = typeof(Double);
			AgregarPropiedad<Double>("condonacion", loColDouble); 

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
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 8;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha", loColDateTimeN); 
			}
			#endregion

		}
	}
