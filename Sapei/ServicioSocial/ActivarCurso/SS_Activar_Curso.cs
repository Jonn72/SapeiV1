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
	/// Clase ss_activar_curso generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class SS_Activar_Curso:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_activar_curso.
		/// </summary>
		public SS_Activar_Curso():base()
		{
			NombreTabla = "ss_activar_curso";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase ss_activar_curso.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public SS_Activar_Curso(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "ss_activar_curso";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece periodo_curso.Sin descripcion para periodo_curso 
		/// </summary>
		/// <value>
		/// periodo_curso 
		/// </value>
		[Key]
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string periodo_curso
		{
			get
			{
				return ObtenerValorPropiedad<string>("periodo_curso");
			}

			set
			{
				EstablecerValorPropiedad<string>("periodo_curso", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre.Sin descripcion para nombre 
		/// </summary>
		/// <value>
		/// nombre 
		/// </value>
		[Required]
		[MaxLength (60)]
		[DefaultValue(null)]
		public string nombre
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
		/// Obtiene o establece url.Sin descripcion para url 
		/// </summary>
		/// <value>
		/// url 
		/// </value>
		[Required]
		[MaxLength (70)]
		[DefaultValue(null)]
		public string url
		{
			get
			{
				return ObtenerValorPropiedad<string>("url");
			}

			set
			{
				EstablecerValorPropiedad<string>("url", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla ss_activar_curso.		/// </summary>
		/// <param name="psperiodo_curso">periodo_curso</param>
		public void Cargar(string psperiodo_curso)
		{
			base.Cargar(psperiodo_curso);
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

			AgregaCampoLlave("periodo_curso",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para periodo_curso";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo_curso", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para nombre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 70;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para url";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("url", loColstring); 
			}
			#endregion

		}
	}
