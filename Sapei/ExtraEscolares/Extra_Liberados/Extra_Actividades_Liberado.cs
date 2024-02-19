using System;
using System.Collections.Generic;
using Sapei.Framework;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sapei
{
	/// <summary>
	/// Clase extra_actividades_liberado generada automáticamente desde el Generador de Código Sapei
	/// </summary>
	[Serializable]
	public partial class Extra_Actividades_Liberados:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_actividades_liberado.
		/// </summary>
		public Extra_Actividades_Liberados():base()
		{
			NombreTabla = "extra_actividades_liberados";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_actividades_liberado.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Extra_Actividades_Liberados(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "extra_actividades_liberados";
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
		/// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
		/// </summary>
		/// <value>
		/// no_de_control 
		/// </value>
		[Key]
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public string no_de_control
		{
			get
			{
				return ObtenerValorPropiedad<string>("no_de_control");
			}

			set
			{
				EstablecerValorPropiedad<string>("no_de_control", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_actividad.Sin descripcion para tipo_actividad 
		/// </summary>
		/// <value>
		/// tipo_actividad 
		/// </value>
		[Key]
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string tipo_actividad
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_actividad");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_actividad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_registro.Sin descripcion para fecha_registro 
		/// </summary>
		/// <value>
		/// fecha_registro 
		/// </value>
		[Required]
		public DateTime fecha_registro
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_registro");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_registro", value);
			}

		}
		/// <summary>
		/// Obtiene o establece promedio.Sin descripcion para promedio 
		/// </summary>
		/// <value>
		/// promedio 
		/// </value>
		[DefaultValue(null)]
		public Double? promedio
		{
			get
			{
				return ObtenerValorPropiedad<Double?>("promedio");
			}

			set
			{
				EstablecerValorPropiedad<Double?>("promedio", value);
			}

		}
		[Required]
		[MaxLength(30)]
		[DefaultValue(null)]
		public string usuario
		{
			get
			{
				return ObtenerValorPropiedad<string>("usuario");
			}

			set
			{
				EstablecerValorPropiedad<string>("usuario", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla extra_actividades_liberado.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="psno_de_control">no_de_control</param>
		/// <param name="pstipo_actividad">tipo_actividad</param>
		public void Cargar(string psperiodo,string psno_de_control,string pstipo_actividad)
		{
			base.Cargar(psperiodo,psno_de_control,pstipo_actividad);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<Double?> loColDoubleN; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(2);
			Propiedades = new Dictionary<string, Propiedad>(6);

			AgregaCampoLlave("no_de_control",null);
			AgregaCampoLlave("tipo_actividad",null);

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
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para tipo_actividad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_actividad", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 3;
			loColDateTime.Descripcion = "Sin descripcion para fecha_registro";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_registro", loColDateTime); 

			loColDoubleN = new PropiedadesColumna<Double?>();
			loColDoubleN.Valor = null;
			loColDoubleN.EsPrimaryKey = false;
			loColDoubleN.Longitud = 8;
			loColDoubleN.Precision = 53;
			loColDoubleN.EsRequeridoBD = false;
			loColDoubleN.CampoId = 4;
			loColDoubleN.Descripcion = "Sin descripcion para promedio";
			loColDoubleN.EsIdentity = false;
			loColDoubleN.Tipo = typeof(Double?);
			AgregarPropiedad<Double?>("promedio", loColDoubleN);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para tipo_actividad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring);
		}
			#endregion

		}
	}
