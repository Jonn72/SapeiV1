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
	/// Clase bib_CD generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_CDs:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_CD.
		/// </summary>
		public Bib_CDs():base()
		{
			NombreTabla = "bib_cds";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_CD.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_CDs(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_cds";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_Cds.Sin descripcion para Id_Cds 
		/// </summary>
		/// <value>
		/// Id_Cds 
		/// </value>
		[Key]
		[Required]
		public Int32 Id_Cds
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_cds");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_cds", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Id_mat_bib.Sin descripcion para Id_mat_bib 
		/// </summary>
		/// <value>
		/// Id_mat_bib 
		/// </value>
		[Required]
		public Int32 Id_mat_bib
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_mat_bib");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_mat_bib", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Duracion.Sin descripcion para Duracion 
		/// </summary>
		/// <value>
		/// Duracion 
		/// </value>
		[Required]
		public Single Duracion
		{
			get
			{
				return ObtenerValorPropiedad<Single>("duracion");
			}

			set
			{
				EstablecerValorPropiedad<Single>("duracion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Descripcion.Sin descripcion para Descripcion 
		/// </summary>
		/// <value>
		/// Descripcion 
		/// </value>
		[MaxLength (200)]
		[DefaultValue(null)]
		public string Descripcion
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
		/// Carga un registro especifico denotado por las llaves de la tabla bib_CD.		/// </summary>
		/// <param name="piId_Cds">Id_Cds</param>
		public void Cargar(Int32 piId_Cds)
		{
			base.Cargar(piId_Cds);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<Single> loColSingle; 
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("id_cds",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para Id_Cds";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_cds", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 1;
			loColInt32.Descripcion = "Sin descripcion para Id_mat_bib";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_mat_bib", loColInt32); 

			loColSingle = new PropiedadesColumna<Single>();
			loColSingle.EsPrimaryKey = false;
			loColSingle.Longitud = 4;
			loColSingle.Precision = 24;
			loColSingle.EsRequeridoBD = true;
			loColSingle.CampoId = 2;
			loColSingle.Descripcion = "Sin descripcion para Duracion";
			loColSingle.EsIdentity = false;
			loColSingle.Tipo = typeof(Single);
			AgregarPropiedad<Single>("duracion", loColSingle); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 200;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para Descripcion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion", loColstring); 
			}
			#endregion

		}
	}
