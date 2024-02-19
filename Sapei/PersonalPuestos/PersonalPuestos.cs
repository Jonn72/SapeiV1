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
	/// Clase personal_puesto generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class PersonalPuestos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_puesto.
		/// </summary>
		public PersonalPuestos():base()
		{
			NombreTabla = "personal_puestos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_puesto.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public PersonalPuestos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "personal_puestos";
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
		/// Obtiene o establece clave_puesto.Sin descripcion para clave_puesto 
		/// </summary>
		/// <value>
		/// clave_puesto 
		/// </value>
		[Key]
		[Required]
		public Int32 clave_puesto
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("clave_puesto");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("clave_puesto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece horas_asignadas.Sin descripcion para horas_asignadas 
		/// </summary>
		/// <value>
		/// horas_asignadas 
		/// </value>
		[DefaultValue(null)]
		public Int32? horas_asignadas
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("horas_asignadas");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("horas_asignadas", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_ingreso_puesto.Sin descripcion para fecha_ingreso_puesto 
		/// </summary>
		/// <value>
		/// fecha_ingreso_puesto 
		/// </value>
		[Required]
		public DateTime fecha_ingreso_puesto
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_ingreso_puesto");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_ingreso_puesto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_termino_puesto.Sin descripcion para fecha_termino_puesto 
		/// </summary>
		/// <value>
		/// fecha_termino_puesto 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_termino_puesto
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_termino_puesto");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_termino_puesto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_de_ratificacion_puesto.Sin descripcion para fecha_de_ratificacion_puesto 
		/// </summary>
		/// <value>
		/// fecha_de_ratificacion_puesto 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_de_ratificacion_puesto
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_de_ratificacion_puesto");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_de_ratificacion_puesto", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla personal_puesto.		/// </summary>
		/// <param name="psrfc">rfc</param>
		/// <param name="piclave_puesto">clave_puesto</param>
		public void Cargar(string psrfc,Int32 piclave_puesto)
		{
			base.Cargar(psrfc,piclave_puesto);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<Int32?> loColInt32N; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<DateTime?> loColDateTimeN; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(2);
			Propiedades = new Dictionary<string, Propiedad>(6);

			AgregaCampoLlave("rfc",null);
			AgregaCampoLlave("clave_puesto",null);

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

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 1;
			loColInt32.Descripcion = "Sin descripcion para clave_puesto";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("clave_puesto", loColInt32); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 2;
			loColInt32N.Descripcion = "Sin descripcion para horas_asignadas";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("horas_asignadas", loColInt32N); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 7;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 3;
			loColDateTime.Descripcion = "Sin descripcion para fecha_ingreso_puesto";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_ingreso_puesto", loColDateTime); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 4;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_termino_puesto";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_termino_puesto", loColDateTimeN); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 5;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_de_ratificacion_puesto";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_de_ratificacion_puesto", loColDateTimeN); 
			}
			#endregion

		}
	}
