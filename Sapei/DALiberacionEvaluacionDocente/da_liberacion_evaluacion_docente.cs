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
	/// Clase rf_tabulador generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class da_liberacion_evaluacion_docente: CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase da_liberacion_evaluacion_docente.
		/// </summary>
		public da_liberacion_evaluacion_docente():base()
		{
			NombreTabla = "da_liberacion_evaluacion_docente";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase da_liberacion_evaluacion_docente.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public da_liberacion_evaluacion_docente(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "da_liberacion_evaluacion_docente";
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
		[MaxLength(5)]
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
		/// Obtiene o establece no_de_control.no_de_control 
		/// </summary>
		/// <value>
		/// no_de_control 
		/// </value>
		[Key]
		[Required]
		[MaxLength(10)]
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
		/// Obtiene o establece motivo.Sin descripcion para motivo 
		/// </summary>
		/// <value>
		/// motivo 
		/// </value>
		/// 
		[Required]
		[MaxLength (240)]
		public string motivo
		{
			get
			{
				return ObtenerValorPropiedad<string>("motivo");
			}

			set
			{
				EstablecerValorPropiedad<string>("motivo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha
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
		/// Carga un registro especifico denotado por las llaves de la tabla da_liberacion_evaluacion_docente.		/// </summary>
		public void Cargar(string psPeriodo, string psNoControl)
		{
			base.Cargar(psPeriodo, psNoControl);
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

			CamposLlave= new Dictionary<string,object>(2);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("periodo", null);
			AgregaCampoLlave("no_de_control", null);

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
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 240;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para motivo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("motivo", loColstring);

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 8;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 3;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha", loColDateTimeN);
		}
			#endregion

		}
	}
