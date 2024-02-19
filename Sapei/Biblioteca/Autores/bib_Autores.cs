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
	/// Clase bib_Autore generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Autores:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Autore.
		/// </summary>
		public Bib_Autores():base()
		{
			NombreTabla = "bib_autores";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Autore.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Autores(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_autores";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_autor.Sin descripcion para Id_autor 
		/// </summary>
		/// <value>
		/// Id_autor 
		/// </value>
		[Key]
		[Required]
		[MaxLength (80)]
		[DefaultValue(null)]
		public string Id_autor
		{
			get
			{
				return ObtenerValorPropiedad<string>("id_autor");
			}

			set
			{
				EstablecerValorPropiedad<string>("id_autor", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Nombre_autor.Sin descripcion para Nombre_autor 
		/// </summary>
		/// <value>
		/// Nombre_autor 
		/// </value>
		[DefaultValue(null)]
		public string Nombre_autor
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_autor");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_autor", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Apellido_p.Sin descripcion para Apellido_p 
		/// </summary>
		/// <value>
		/// Apellido_p 
		/// </value>
		[DefaultValue(null)]
		public string Apellido_p
		{
			get
			{
				return ObtenerValorPropiedad<string>("apellido_p");
			}

			set
			{
				EstablecerValorPropiedad<string>("apellido_p", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Apellido_m.Sin descripcion para Apellido_m 
		/// </summary>
		/// <value>
		/// Apellido_m 
		/// </value>
		[DefaultValue(null)]
		public string Apellido_m
		{
			get
			{
				return ObtenerValorPropiedad<string>("apellido_m");
			}

			set
			{
				EstablecerValorPropiedad<string>("apellido_m", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla bib_Autore.		/// </summary>
		/// <param name="psId_autor">Id_autor</param>
		public void Cargar(string psId_autor)
		{
			base.Cargar(psId_autor);
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
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("id_autor",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 80;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para Id_autor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("id_autor", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = -1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para Nombre_autor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_autor", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = -1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para Apellido_p";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("apellido_p", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = -1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para Apellido_m";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("apellido_m", loColstring);


            
        }
			#endregion

		}
	}
