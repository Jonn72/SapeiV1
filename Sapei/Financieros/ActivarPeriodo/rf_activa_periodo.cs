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
	/// Clase rf_activa_periodo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class RF_activa_periodo:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase rf_activa_periodo.
		/// </summary>
		public RF_activa_periodo():base()
		{
			NombreTabla = "rf_activa_periodo";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase rf_activa_periodo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public RF_activa_periodo(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "rf_activa_periodo";
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
		/// Obtiene o establece ini_imprime_ficha.Sin descripcion para ini_imprime_ficha 
		/// </summary>
		/// <value>
		/// ini_imprime_ficha 
		/// </value>
		[Required]
		[DefaultValue(null)]
		public DateTime ini_imprime_ficha
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("ini_imprime_ficha");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("ini_imprime_ficha", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_imprime_ficha.Sin descripcion para fin_imprime_ficha 
		/// </summary>
		/// <value>
		/// fin_imprime_ficha 
		/// </value>
		[Required]
		[DefaultValue(null)]
		public DateTime fin_imprime_ficha
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_imprime_ficha");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_imprime_ficha", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_pago.Sin descripcion para fin_pago 
		/// </summary>
		/// <value>
		/// fin_pago 
		/// </value>
		[Required]
		[DefaultValue(null)]
		public DateTime fin_pago
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_pago");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_pago", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_registro_pago_ordinario.Sin descripcion para fin_registro_pago_ordinario 
		/// </summary>
		/// <value>
		/// fin_registro_pago_ordinario 
		/// </value>
		[Required]
		[DefaultValue(null)]
		public DateTime fin_registro_pago_ordinario
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_registro_pago_ordinario");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_registro_pago_ordinario", value);
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla rf_activa_periodo.		/// </summary>
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
			PropiedadesColumna<DateTime> loColDatetime;
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(6);

			AgregaCampoLlave("periodo",null);

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

			loColDatetime = new PropiedadesColumna<DateTime>();
			loColDatetime.Valor = System.DateTime.Now;
			loColDatetime.EsPrimaryKey = false;
			loColDatetime.Longitud = 10;
			loColDatetime.Precision = 10;
			loColDatetime.EsRequeridoBD = true;
			loColDatetime.CampoId = 1;
			loColDatetime.Descripcion = "Sin descripcion para ini_imprime_ficha";
			loColDatetime.EsIdentity = false;
			loColDatetime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("ini_imprime_ficha", loColDatetime); 

			loColDatetime = new PropiedadesColumna<DateTime>();
			loColDatetime.Valor = System.DateTime.Now;
			loColDatetime.EsPrimaryKey = false;
			loColDatetime.Longitud = 10;
			loColDatetime.Precision = 10;
			loColDatetime.EsRequeridoBD = true;
			loColDatetime.CampoId = 2;
			loColDatetime.Descripcion = "Sin descripcion para fin_imprime_ficha";
			loColDatetime.EsIdentity = false;
			loColDatetime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_imprime_ficha", loColDatetime); 

			loColDatetime = new PropiedadesColumna<DateTime>();
			loColDatetime.Valor = System.DateTime.Now;
			loColDatetime.EsPrimaryKey = false;
			loColDatetime.Longitud = 10;
			loColDatetime.Precision = 10;
			loColDatetime.EsRequeridoBD = true;
			loColDatetime.CampoId = 3;
			loColDatetime.Descripcion = "Sin descripcion para fin_pago";
			loColDatetime.EsIdentity = false;
			loColDatetime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_pago", loColDatetime); 

			loColDatetime = new PropiedadesColumna<DateTime>();
			loColDatetime.Valor = System.DateTime.Now;
			loColDatetime.EsPrimaryKey = false;
			loColDatetime.Longitud = 10;
			loColDatetime.Precision = 10;
			loColDatetime.EsRequeridoBD = true;
			loColDatetime.CampoId = 4;
			loColDatetime.Descripcion = "Sin descripcion para fin_registro_pago_ordinario";
			loColDatetime.EsIdentity = false;
			loColDatetime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_registro_pago_ordinario", loColDatetime); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring); 
			}
			#endregion

		}
	}
