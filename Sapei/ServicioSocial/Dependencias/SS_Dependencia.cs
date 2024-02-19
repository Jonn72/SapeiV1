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
	/// Clase ss_dependencia generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class SS_Dependencia:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_dependencia.
		/// </summary>
		public SS_Dependencia():base()
		{
			NombreTabla = "ss_dependencias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_dependencia.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public SS_Dependencia(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "ss_dependencias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
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
		/// Obtiene o establece dependencia.Sin descripcion para dependencia 
		/// </summary>
		/// <value>
		/// dependencia 
		/// </value>
		[Required]
		[MaxLength (100)]
		[DefaultValue(null)]
		public string dependencia
		{
			get
			{
				return ObtenerValorPropiedad<string>("dependencia");
			}

			set
			{
				EstablecerValorPropiedad<string>("dependencia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece titular.Sin descripcion para titular 
		/// </summary>
		/// <value>
		/// titular 
		/// </value>
		[Required]
		[MaxLength (60)]
		[DefaultValue(null)]
		public string titular
		{
			get
			{
				return ObtenerValorPropiedad<string>("titular");
			}

			set
			{
				EstablecerValorPropiedad<string>("titular", value);
			}

		}
		/// <summary>
		/// Obtiene o establece puesto_cargo.Sin descripcion para puesto_cargo 
		/// </summary>
		/// <value>
		/// puesto_cargo 
		/// </value>
		[Required]
		[MaxLength (60)]
		[DefaultValue(null)]
		public string puesto_cargo
		{
			get
			{
				return ObtenerValorPropiedad<string>("puesto_cargo");
			}

			set
			{
				EstablecerValorPropiedad<string>("puesto_cargo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece telefono.Sin descripcion para telefono 
		/// </summary>
		/// <value>
		/// telefono 
		/// </value>
		[Required]
		[MaxLength (20)]
		[DefaultValue(null)]
		public string telefono
		{
			get
			{
				return ObtenerValorPropiedad<string>("telefono");
			}

			set
			{
				EstablecerValorPropiedad<string>("telefono", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla ss_dependencia.		/// </summary>
		/// <param name="psrfc">rfc</param>
		public void Cargar(string psrfc)
		{
			base.Cargar(psrfc);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(5);

			AgregaCampoLlave("rfc",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para rfc";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para dependencia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("dependencia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para titular";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("titular", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para puesto_cargo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("puesto_cargo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 20;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para telefono";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("telefono", loColstring); 
			}
			#endregion

		}
	}
