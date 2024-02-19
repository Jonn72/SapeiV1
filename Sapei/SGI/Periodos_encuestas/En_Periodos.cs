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
	/// Clase en_periodo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class En_Periodos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase en_periodo.
		/// </summary>
		public En_Periodos():base()
		{
			NombreTabla = "en_periodos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase en_periodo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public En_Periodos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "en_periodos";
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
		/// Obtiene o establece inicio.Sin descripcion para inicio 
		/// </summary>
		/// <value>
		/// inicio 
		/// </value>
		[Required]
		public DateTime inicio
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("inicio");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("inicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin.Sin descripcion para fin 
		/// </summary>
		/// <value>
		/// fin 
		/// </value>
		[Required]
		public DateTime fin
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla en_periodo.		/// </summary>
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
			loColDateTime.CampoId = 1;
			loColDateTime.Descripcion = "Sin descripcion para inicio";
			loColDateTime.EsIdentity = false;
               loColDateTime.IncluyeHoras = true;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("inicio", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
			loColDateTime.Descripcion = "Sin descripcion para fin";
			loColDateTime.EsIdentity = false;
               loColDateTime.IncluyeHoras = true;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin", loColDateTime); 
			}
			#endregion

		}
	}
