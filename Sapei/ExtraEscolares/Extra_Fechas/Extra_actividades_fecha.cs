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
	/// Clase extra_actividades_fecha generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Extra_actividades_fecha:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_actividades_fecha.
		/// </summary>
		public Extra_actividades_fecha():base()
		{
			NombreTabla = "extra_actividades_fechas";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_actividades_fecha.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Extra_actividades_fecha(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "extra_actividades_fechas";
			Propietario= "dbo";
               RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece periodo.periodo 
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
		/// Obtiene o establece fecha_ini_registro.fecha_ini_registro 
		/// </summary>
		/// <value>
		/// fecha_ini_registro 
		/// </value>
		[Required]
		public DateTime fecha_ini_registro
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_ini_registro");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_ini_registro", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_fin_registro.fecha_fin_registro 
		/// </summary>
		/// <value>
		/// fecha_fin_registro 
		/// </value>
		[Required]
		public DateTime fecha_fin_registro
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_fin_registro");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_fin_registro", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla extra_actividades_fecha.		/// </summary>
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
			loColstring.Descripcion = "periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 1;
			loColDateTime.Descripcion = "fecha_ini_registro";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
               loColDateTime.IncluyeHoras = true;
			AgregarPropiedad<DateTime>("fecha_ini_registro", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
			loColDateTime.Descripcion = "fecha_fin_registro";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
               loColDateTime.IncluyeHoras = true;
			AgregarPropiedad<DateTime>("fecha_fin_registro", loColDateTime); 
			}
			#endregion

		}
	}
