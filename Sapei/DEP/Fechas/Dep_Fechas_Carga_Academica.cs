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
	/// Clase dep_fecha generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Dep_Fechas_Carga_Academica:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase dep_fecha.
		/// </summary>
		public Dep_Fechas_Carga_Academica():base()
		{
			NombreTabla = "dep_fechas_carga_academica";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase dep_fecha.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Dep_Fechas_Carga_Academica(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "dep_fechas_carga_academica";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece periodo.Sin descripcion para periodo 
		/// </summary>
		/// <value>
		/// periodo 
		/// </value>
		[Key]
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string periodo
		{
			get
			{
				return ObtenerValorPropiedad<string>("periodo");
			}

			set
			{
				EstablecerValorPropiedad<string>("periodo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ini_carga_academica.Sin descripcion para ini_carga_academica 
		/// </summary>
		/// <value>
		/// ini_carga_academica 
		/// </value>
		[Required]
		public DateTime ini_carga_academica
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("ini_carga_academica");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("ini_carga_academica", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_carga_academica.Sin descripcion para fin_carga_academica 
		/// </summary>
		/// <value>
		/// fin_carga_academica 
		/// </value>
		[Required]
		public DateTime fin_carga_academica
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_carga_academica");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_carga_academica", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla dep_fecha.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		public void Cargar(string psperiodo)
		{
			base.Cargar(psperiodo);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(3);

			AgregaCampoLlave("periodo",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.IncluyeHoras = true;
			loColDateTime.CampoId = 1;
			loColDateTime.Descripcion = "Sin descripcion para ini_carga_academica";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("ini_carga_academica", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.IncluyeHoras = true;
			loColDateTime.CampoId = 2;
			loColDateTime.Descripcion = "Sin descripcion para fin_carga_academica";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_carga_academica", loColDateTime); 
			}
			#endregion

		}
	}
