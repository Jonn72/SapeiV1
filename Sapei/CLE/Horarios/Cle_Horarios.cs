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
	/// Clase cle_horario generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Cle_Horarios:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase cle_horario.
		/// </summary>
		public Cle_Horarios():base()
		{
			NombreTabla = "cle_horarios";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase cle_horario.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Cle_Horarios(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "cle_horarios";
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
		/// Obtiene o establece nivel.Sin descripcion para nivel 
		/// </summary>
		/// <value>
		/// nivel 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string nivel
		{
			get
			{
				return ObtenerValorPropiedad<string>("nivel");
			}

			set
			{
				EstablecerValorPropiedad<string>("nivel", value);
			}

		}
		/// <summary>
		/// Obtiene o establece grupo.Sin descripcion para grupo 
		/// </summary>
		/// <value>
		/// grupo 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string grupo
		{
			get
			{
				return ObtenerValorPropiedad<string>("grupo");
			}

			set
			{
				EstablecerValorPropiedad<string>("grupo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece dia.Sin descripcion para dia 
		/// </summary>
		/// <value>
		/// dia 
		/// </value>
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string dia
		{
			get
			{
				return ObtenerValorPropiedad<string>("dia");
			}

			set
			{
				EstablecerValorPropiedad<string>("dia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece hora_inicial.Sin descripcion para hora_inicial 
		/// </summary>
		/// <value>
		/// hora_inicial 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string hora_inicial
		{
			get
			{
				return ObtenerValorPropiedad<string>("hora_inicial");
			}

			set
			{
				EstablecerValorPropiedad<string>("hora_inicial", value);
			}

		}
		/// <summary>
		/// Obtiene o establece hora_final.Sin descripcion para hora_final 
		/// </summary>
		/// <value>
		/// hora_final 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string hora_final
		{
			get
			{
				return ObtenerValorPropiedad<string>("hora_final");
			}

			set
			{
				EstablecerValorPropiedad<string>("hora_final", value);
			}

		}
		/// <summary>
		/// Obtiene o establece aula.Sin descripcion para aula 
		/// </summary>
		/// <value>
		/// aula 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string aula
		{
			get
			{
				return ObtenerValorPropiedad<string>("aula");
			}

			set
			{
				EstablecerValorPropiedad<string>("aula", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla cle_horario.		/// </summary>
		public void Cargar()
		{
			base.Cargar();
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

			CamposLlave= new Dictionary<string,object>(0);
			Propiedades = new Dictionary<string, Propiedad>(7);


			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para nivel";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nivel", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para grupo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("grupo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para dia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("dia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para hora_inicial";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("hora_inicial", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para hora_final";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("hora_final", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para aula";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("aula", loColstring); 
			}
			#endregion

		}
	}
