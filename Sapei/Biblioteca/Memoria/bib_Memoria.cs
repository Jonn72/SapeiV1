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
	/// Clase bib_Memoria generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Memorias:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Memoria.
		/// </summary>
		public Bib_Memorias():base()
		{
			NombreTabla = "bib_memorias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Memoria.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Memorias(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_memorias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_Mem_res.Sin descripcion para Id_Mem_res 
		/// </summary>
		/// <value>
		/// Id_Mem_res 
		/// </value>
		[Key]
		[Required]
		public Int32 Id_Mem_res
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_mem_res");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_mem_res", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Id_Mat_Bib.Sin descripcion para Id_Mat_Bib 
		/// </summary>
		/// <value>
		/// Id_Mat_Bib 
		/// </value>
		[Required]
		public Int32 Id_Mat_Bib
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
		/// Obtiene o establece Fecha_publicacion.Sin descripcion para Fecha_publicacion 
		/// </summary>
		/// <value>
		/// Fecha_publicacion 
		/// </value>
		[Required]
		public DateTime Fecha_publicacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_publicacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_publicacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Lugar_p.Sin descripcion para Lugar_p 
		/// </summary>
		/// <value>
		/// Lugar_p 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string Lugar_p
		{
			get
			{
				return ObtenerValorPropiedad<string>("lugar_p");
			}

			set
			{
				EstablecerValorPropiedad<string>("lugar_p", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla bib_Memoria.		/// </summary>
		/// <param name="piId_Mem_res">Id_Mem_res</param>
		public void Cargar(Int32 piId_Mem_res)
		{
			base.Cargar(piId_Mem_res);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("id_mem_res",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para Id_Mem_res";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_mem_res", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 1;
			loColInt32.Descripcion = "Sin descripcion para Id_Mat_Bib";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_mat_bib", loColInt32); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
			loColDateTime.Descripcion = "Sin descripcion para Fecha_publicacion";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_publicacion", loColDateTime); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para Lugar_p";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("lugar_p", loColstring); 
			}
			#endregion

		}
	}
