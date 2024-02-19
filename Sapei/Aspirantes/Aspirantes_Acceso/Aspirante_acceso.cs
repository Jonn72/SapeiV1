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
	/// Clase aspirantes_acceso generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Aspirante_acceso:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase aspirantes_acceso.
		/// </summary>
		public Aspirante_acceso():base()
		{
			NombreTabla = "aspirantes_acceso";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase aspirantes_acceso.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Aspirante_acceso(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "aspirantes_acceso";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece folio.folio 
		/// </summary>
		/// <value>
		/// folio 
		/// </value>
		[Key]
		[Required]
		[MaxLength (7)]
		[DefaultValue(null)]
		public string Folio
		{
			get
			{
				return ObtenerValorPropiedad<string>("folio");
			}

			set
			{
				EstablecerValorPropiedad<string>("folio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece contrasena.contrasena 
		/// </summary>
		/// <value>
		/// contrasena 
		/// </value>
		[Required]
		[MaxLength (100)]
		[DefaultValue(null)]
		public string Contrasena
		{
			get
			{
				return ObtenerValorPropiedad<string>("contrasena");
			}

			set
			{
				EstablecerValorPropiedad<string>("contrasena", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla aspirantes_acceso.		/// </summary>
		/// <param name="psfolio">folio</param>
		public void Cargar(string psfolio)
		{
			base.Cargar(psfolio);
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
			Propiedades = new Dictionary<string, Propiedad>(2);

			AgregaCampoLlave("folio",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 7;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "folio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("folio", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "contrasena";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("contrasena", loColstring); 
			}
			#endregion

		}
	}
