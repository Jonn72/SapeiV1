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
	/// Clase extra_actividades_horario generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Extra_actividades_horario:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_actividades_horario.
		/// </summary>
		public Extra_actividades_horario():base()
		{
			NombreTabla = "extra_actividades_horarios";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_actividades_horario.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Extra_actividades_horario(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "extra_actividades_horarios";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece id_actividad.Sin descripcion para id_actividad 
		/// </summary>
		/// <value>
		/// id_actividad 
		/// </value>
		[Required]
		public Int32 id_actividad
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_actividad");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_actividad", value);
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
		/// Obtiene o establece hora_inicio.Sin descripcion para hora_inicio 
		/// </summary>
		/// <value>
		/// hora_inicio 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string hora_inicio
		{
			get
			{
				return ObtenerValorPropiedad<string>("hora_inicio");
			}

			set
			{
				EstablecerValorPropiedad<string>("hora_inicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece hora_fin.Sin descripcion para hora_fin 
		/// </summary>
		/// <value>
		/// hora_fin 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string hora_fin
		{
			get
			{
				return ObtenerValorPropiedad<string>("hora_fin");
			}

			set
			{
				EstablecerValorPropiedad<string>("hora_fin", value);
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
		/// <summary>
		/// Obtiene o establece periodo.Sin descripcion para periodo 
		/// </summary>
		/// <value>
		/// periodo 
		/// </value>
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla extra_actividades_horario.		/// </summary>
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
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(0);
			Propiedades = new Dictionary<string, Propiedad>(6);


			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para id_actividad";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_actividad", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
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
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para hora_inicio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("hora_inicio", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para hora_fin";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("hora_fin", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para aula";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("aula", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 
			}
			#endregion

		}
	}
