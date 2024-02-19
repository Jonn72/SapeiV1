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
	/// Clase bib_categoria_lc generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_categoria_lc:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_categoria_lc.
		/// </summary>
		public Bib_categoria_lc():base()
		{
			NombreTabla = "bib_categoria_lc";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_categoria_lc.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_categoria_lc(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_categoria_lc";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece id_categoria.Sin descripcion para id_categoria 
		/// </summary>
		/// <value>
		/// id_categoria 
		/// </value>
		[Key]
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string id_categoria
		{
			get
			{
				return ObtenerValorPropiedad<string>("id_categoria");
			}

			set
			{
				EstablecerValorPropiedad<string>("id_categoria", value);
			}

		}
		/// <summary>
		/// Obtiene o establece descripcion.Sin descripcion para descripcion 
		/// </summary>
		/// <value>
		/// descripcion 
		/// </value>
		[MaxLength (100)]
		[DefaultValue(null)]
		public string descripcion
		{
			get
			{
				return ObtenerValorPropiedad<string>("descripcion");
			}

			set
			{
				EstablecerValorPropiedad<string>("descripcion", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla bib_categoria_lc.		/// </summary>
		/// <param name="psid_categoria">id_categoria</param>
		public void Cargar(string psid_categoria)
		{
			base.Cargar(psid_categoria);
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

			AgregaCampoLlave("id_categoria",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para id_categoria";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("id_categoria", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para descripcion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion", loColstring); 
			}
			#endregion

		}
	}
