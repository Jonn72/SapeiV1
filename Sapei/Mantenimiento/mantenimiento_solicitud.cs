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
	/// Clase mantenimiento_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class mantenimiento_solicitud:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase mantenimiento_solicitud.
		/// </summary>
		public mantenimiento_solicitud():base()
		{
			NombreTabla = "mantenimiento_solicitud";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase mantenimiento_solicitud.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public mantenimiento_solicitud(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "mantenimiento_solicitud";
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
		/// Obtiene o establece tipo_solicitud.Sin descripcion para tipo_solicitud 
		/// </summary>
		/// <value>
		/// tipo_solicitud 
		/// </value>
		[Required]
		[MaxLength (2)]
		[DefaultValue(null)]
		public string tipo_solicitud
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_solicitud");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_solicitud", value);
			}

		}
		/// <summary>
		/// Obtiene o establece folio.Sin descripcion para folio 
		/// </summary>
		/// <value>
		/// folio 
		/// </value>
		[Required]
		public Int16 folio
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("folio");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("folio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece area_solicitante.Sin descripcion para area_solicitante 
		/// </summary>
		/// <value>
		/// area_solicitante 
		/// </value>
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string area_solicitante
		{
			get
			{
				return ObtenerValorPropiedad<string>("area_solicitante");
			}

			set
			{
				EstablecerValorPropiedad<string>("area_solicitante", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_solicitante.Sin descripcion para fecha_solicitante 
		/// </summary>
		/// <value>
		/// fecha_solicitante 
		/// </value>
		[Required]
		public DateTime fecha_solicitante
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_solicitante");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_solicitante", value);
			}

		}
		/// <summary>
		/// Obtiene o establece descripcion.Sin descripcion para descripcion 
		/// </summary>
		/// <value>
		/// descripcion 
		/// </value>
		[Required]
		[DefaultValue(null)]
		public string descripcion
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
		/// <summary>
		/// Obtiene o establece estatus.Sin descripcion para estatus 
		/// </summary>
		/// <value>
		/// estatus 
		/// </value>
		[Required]
		public Byte estatus
		{
			get
			{
				return ObtenerValorPropiedad<Byte>("estatus");
			}

			set
			{
				EstablecerValorPropiedad<Byte>("estatus", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla mantenimiento_solicitud.		/// </summary>
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
			 PropiedadesColumna<Int16> loColInt16; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<Byte> loColByte; 
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
			loColstring.Longitud = 2;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para tipo_solicitud";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_solicitud", loColstring); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 2;
			loColInt16.Descripcion = "Sin descripcion para folio";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("folio", loColInt16); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para area_solicitante";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("area_solicitante", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 4;
			loColDateTime.Descripcion = "Sin descripcion para fecha_solicitante";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_solicitante", loColDateTime); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = -1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para descripcion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion", loColstring); 

			loColByte = new PropiedadesColumna<Byte>();
			loColByte.EsPrimaryKey = false;
			loColByte.Longitud = 1;
			loColByte.Precision = 3;
			loColByte.EsRequeridoBD = true;
			loColByte.CampoId = 6;
			loColByte.Descripcion = "Sin descripcion para estatus";
			loColByte.EsIdentity = false;
			loColByte.Tipo = typeof(Byte);
			AgregarPropiedad<Byte>("estatus", loColByte); 
			}
			#endregion

		}
	}
