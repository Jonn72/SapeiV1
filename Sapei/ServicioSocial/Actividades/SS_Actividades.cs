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
	/// Clase ss_actividade generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class SS_Actividades:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_actividade.
		/// </summary>
		public SS_Actividades():base()
		{
			NombreTabla = "ss_actividades";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_actividade.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public SS_Actividades(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "ss_actividades";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece id.Sin descripcion para id 
		/// </summary>
		/// <value>
		/// id 
		/// </value>
		[Key]
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string id
		{
			get
			{
				return ObtenerValorPropiedad<string>("id");
			}

			set
			{
				EstablecerValorPropiedad<string>("id", value);
			}

		}
		/// <summary>
		/// Obtiene o establece descripcion.Sin descripcion para descripcion 
		/// </summary>
		/// <value>
		/// descripcion 
		/// </value>
		[Required]
		[MaxLength (50)]
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
		/// Carga un registro especifico denotado por las llaves de la tabla ss_actividade.		/// </summary>
		/// <param name="psid">id</param>
		public void Cargar(string psid)
		{
			base.Cargar(psid);
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
			Propiedades = new Dictionary<string, Propiedad>(3);

			AgregaCampoLlave("id",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para id";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("id", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para descripcion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion", loColstring);

			}
			#endregion

		}
	}
