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
	/// Clase bib_Prestador generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Prestadores:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Prestador.
		/// </summary>
		public Bib_Prestadores():base()
		{
			NombreTabla = "bib_prestador";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Prestador.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Prestadores(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_prestador";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_prestador.Sin descripcion para Id_prestador 
		/// </summary>
		/// <value>
		/// Id_prestador 
		/// </value>
		[Key]
		[Required]
		public Int32 Id_prestador
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_prestador");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_prestador", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Id_tipo.Sin descripcion para Id_tipo 
		/// </summary>
		/// <value>
		/// Id_tipo 
		/// </value>
		[Required]
		public Int32 Id_tipo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_tipo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_tipo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Nombre.Sin descripcion para Nombre 
		/// </summary>
		/// <value>
		/// Nombre 
		/// </value>
		[MaxLength (40)]
		[DefaultValue(null)]
		public string Nombre
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece A_paterno.Sin descripcion para A_paterno 
		/// </summary>
		/// <value>
		/// A_paterno 
		/// </value>
		[MaxLength (40)]
		[DefaultValue(null)]
		public string A_paterno
		{
			get
			{
				return ObtenerValorPropiedad<string>("a_paterno");
			}

			set
			{
				EstablecerValorPropiedad<string>("a_paterno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece A_materno.Sin descripcion para A_materno 
		/// </summary>
		/// <value>
		/// A_materno 
		/// </value>
		[MaxLength (40)]
		[DefaultValue(null)]
		public string A_materno
		{
			get
			{
				return ObtenerValorPropiedad<string>("a_materno");
			}

			set
			{
				EstablecerValorPropiedad<string>("a_materno", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla bib_Prestador.		/// </summary>
		/// <param name="piId_prestador">Id_prestador</param>
		public void Cargar(Int32 piId_prestador)
		{
			base.Cargar(piId_prestador);
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
			Propiedades = new Dictionary<string, Propiedad>(5);

			AgregaCampoLlave("id_prestador",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para Id_prestador";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_prestador", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 1;
			loColInt32.Descripcion = "Sin descripcion para Id_tipo";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_tipo", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 40;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para Nombre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 40;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para A_paterno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("a_paterno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 40;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para A_materno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("a_materno", loColstring); 
			}
			#endregion

		}
	}
