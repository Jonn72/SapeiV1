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
	/// Clase bib_Editorial generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Editoriales:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Editorial.
		/// </summary>
		public Bib_Editoriales():base()
		{
			NombreTabla = "bib_editorial";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Editorial.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Editoriales(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_editorial";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_editorial.Sin descripcion para Id_editorial 
		/// </summary>
		/// <value>
		/// Id_editorial 
		/// </value>
		[Key]
		[Required]
		public Int32 Id_editorial
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_editorial");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_editorial", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Nombre_editorial.Sin descripcion para Nombre_editorial 
		/// </summary>
		/// <value>
		/// Nombre_editorial 
		/// </value>
		[DefaultValue(null)]
		public string Nombre_editorial
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_editorial");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_editorial", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Edicion.Sin descripcion para Edicion 
		/// </summary>
		/// <value>
		/// Edicion 
		/// </value>
		[DefaultValue(null)]
		public string Edicion
		{
			get
			{
				return ObtenerValorPropiedad<string>("edicion");
			}

			set
			{
				EstablecerValorPropiedad<string>("edicion", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla bib_Editorial.		/// </summary>
		/// <param name="piId_editorial">Id_editorial</param>
		public void Cargar(Int32 piId_editorial)
		{
			base.Cargar(piId_editorial);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(3);

			AgregaCampoLlave("id_editorial",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para Id_editorial";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_editorial", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = -1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para Nombre_editorial";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_editorial", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = -1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para Edicion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("edicion", loColstring); 
			}
			#endregion

		}
	}
