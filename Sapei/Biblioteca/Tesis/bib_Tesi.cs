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
	/// Clase bib_Tesi generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Tesis:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Tesi.
		/// </summary>
		public Bib_Tesis():base()
		{
			NombreTabla = "bib_tesis";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Tesi.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Tesis(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_tesis";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_tesis.Sin descripcion para Id_tesis 
		/// </summary>
		/// <value>
		/// Id_tesis 
		/// </value>
		[Key]
		[Required]
		public Int32 Id_tesis
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_tesis");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_tesis", value);
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
		/// Obtiene o establece Fecha_p.Sin descripcion para Fecha_p 
		/// </summary>
		/// <value>
		/// Fecha_p 
		/// </value>
		[Required]
		public DateTime Fecha_p
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_p");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_p", value);
			}

		}
		/// <summary>
		/// Obtiene o establece No_paginas.Sin descripcion para No_paginas 
		/// </summary>
		/// <value>
		/// No_paginas 
		/// </value>
		[Required]
		public Int32 No_paginas
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("no_paginas");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("no_paginas", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla bib_Tesi.		/// </summary>
		/// <param name="piId_tesis">Id_tesis</param>
		public void Cargar(Int32 piId_tesis)
		{
			base.Cargar(piId_tesis);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("id_tesis",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para Id_tesis";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_tesis", loColInt32); 

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

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
			loColDateTime.Descripcion = "Sin descripcion para Fecha_p";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_p", loColDateTime); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "Sin descripcion para No_paginas";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("no_paginas", loColInt32); 
			}
			#endregion

		}
	}
