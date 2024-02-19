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
	/// Clase personal generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Personal:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase personal.
		/// </summary>
		public Personal():base()
		{
			NombreTabla = "personal";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase personal.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Personal(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "personal";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece rfc.Sin descripcion para rfc 
		/// </summary>
		/// <value>
		/// rfc 
		/// </value>
		[Key]
		[Required]
		[MaxLength (13)]
		[DefaultValue(null)]
		public string rfc
		{
			get
			{
				return ObtenerValorPropiedad<string>("rfc");
			}

			set
			{
				EstablecerValorPropiedad<string>("rfc", value);
			}

		}
		/// <summary>
		/// Obtiene o establece clave_area.Sin descripcion para clave_area 
		/// </summary>
		/// <value>
		/// clave_area 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string clave_area
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave_area");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave_area", value);
			}

		}
		/// <summary>
		/// Obtiene o establece no_tarjeta.Sin descripcion para no_tarjeta 
		/// </summary>
		/// <value>
		/// no_tarjeta 
		/// </value>
		[DefaultValue(null)]
		public Int32? no_tarjeta
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("no_tarjeta");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("no_tarjeta", value);
			}

		}
		/// <summary>
		/// Obtiene o establece apellidos_empleado.Sin descripcion para apellidos_empleado 
		/// </summary>
		/// <value>
		/// apellidos_empleado 
		/// </value>
		[MaxLength (45)]
		[DefaultValue(null)]
		public string apellidos_empleado
		{
			get
			{
				return ObtenerValorPropiedad<string>("apellidos_empleado");
			}

			set
			{
				EstablecerValorPropiedad<string>("apellidos_empleado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_empleado.Sin descripcion para nombre_empleado 
		/// </summary>
		/// <value>
		/// nombre_empleado 
		/// </value>
		[MaxLength (35)]
		[DefaultValue(null)]
		public string nombre_empleado
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_empleado");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_empleado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece status_empleado.Sin descripcion para status_empleado 
		/// </summary>
		/// <value>
		/// status_empleado 
		/// </value>
		[MaxLength (2)]
		[DefaultValue(null)]
		public string status_empleado
		{
			get
			{
				return ObtenerValorPropiedad<string>("status_empleado");
			}

			set
			{
				EstablecerValorPropiedad<string>("status_empleado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece area_academica.Sin descripcion para area_academica 
		/// </summary>
		/// <value>
		/// area_academica 
		/// </value>
		[MaxLength (6)]
		[DefaultValue(null)]
		public string area_academica
		{
			get
			{
				return ObtenerValorPropiedad<string>("area_academica");
			}

			set
			{
				EstablecerValorPropiedad<string>("area_academica", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_personal.Sin descripcion para tipo_personal 
		/// </summary>
		/// <value>
		/// tipo_personal 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string tipo_personal
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_personal");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_personal", value);
			}

		}

		/// <summary>
		/// Obtiene o establece actividad_laboral.Sin descripcion para actividad_laboral 
		/// </summary>
		/// <value>
		/// actividad_laboral 
		/// </value>
		[MaxLength(1)]
		[DefaultValue(null)]
		public string actividad_laboral
		{
			get
			{
				return ObtenerValorPropiedad<string>("actividad_laboral");
			}

			set
			{
				EstablecerValorPropiedad<string>("actividad_laboral", value);
			}

		}

		/// <summary>
		/// Obtiene o establece apellido_paterno.Sin descripcion para apellido_paterno 
		/// </summary>
		/// <value>
		/// apellido_paterno 
		/// </value>
		[MaxLength (100)]
		[DefaultValue(null)]
		public string apellido_paterno
		{
			get
			{
				return ObtenerValorPropiedad<string>("apellido_paterno");
			}

			set
			{
				EstablecerValorPropiedad<string>("apellido_paterno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece apellido_materno.Sin descripcion para apellido_materno 
		/// </summary>
		/// <value>
		/// apellido_materno 
		/// </value>
		[MaxLength (100)]
		[DefaultValue(null)]
		public string apellido_materno
		{
			get
			{
				return ObtenerValorPropiedad<string>("apellido_materno");
			}

			set
			{
				EstablecerValorPropiedad<string>("apellido_materno", value);
			}

		}

		/// <summary>
		/// Obtiene o establece fecha_registro.Sin descripcion para fecha_registro 
		/// </summary>
		/// <value>
		/// fecha_registro 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_registro
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_registro");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_registro", value);
			}

		}


		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla personal.		/// </summary>
		/// <param name="psrfc">rfc</param>
		public void Cargar(string psrfc)
		{
			base.Cargar(psrfc);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32?> loColInt32N;
			 PropiedadesColumna<DateTime?> loColDateTimeN;
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(12);

			AgregaCampoLlave("rfc",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para rfc";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para clave_area";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_area", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 2;
			loColInt32N.Descripcion = "Sin descripcion para no_tarjeta";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("no_tarjeta", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 45;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para apellidos_empleado";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("apellidos_empleado", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 35;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para nombre_empleado";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_empleado", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 2;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para status_empleado";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("status_empleado", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 6;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para area_academica";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("area_academica", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para tipo_personal";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_personal", loColstring);


			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para actividad_laboral";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("actividad_laboral", loColstring);


			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "Sin descripcion para apellido_paterno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("apellido_paterno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "Sin descripcion para apellido_materno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("apellido_materno", loColstring);

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 11;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_registro";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_registro", loColDateTimeN);
		}
			#endregion

		}
	}
