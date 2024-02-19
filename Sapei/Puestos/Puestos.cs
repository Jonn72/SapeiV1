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
	/// Clase puesto generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Puestos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase puesto.
		/// </summary>
		public Puestos():base()
		{
			NombreTabla = "puestos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase puesto.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Puestos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "puestos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
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
		/// Obtiene o establece descripcion_puesto.Sin descripcion para descripcion_puesto 
		/// </summary>
		/// <value>
		/// descripcion_puesto 
		/// </value>
		[MaxLength (200)]
		[DefaultValue(null)]
		public string descripcion_puesto
		{
			get
			{
				return ObtenerValorPropiedad<string>("descripcion_puesto");
			}

			set
			{
				EstablecerValorPropiedad<string>("descripcion_puesto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nivel_puesto.Sin descripcion para nivel_puesto 
		/// </summary>
		/// <value>
		/// nivel_puesto 
		/// </value>
		[DefaultValue(null)]
		public Int32? nivel_puesto
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("nivel_puesto");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("nivel_puesto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_puesto.Sin descripcion para tipo_puesto 
		/// </summary>
		/// <value>
		/// tipo_puesto 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string tipo_puesto
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_puesto");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_puesto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece funciones_puesto.Sin descripcion para funciones_puesto 
		/// </summary>
		/// <value>
		/// funciones_puesto 
		/// </value>
		[MaxLength (16)]
		[DefaultValue(null)]
		public string funciones_puesto
		{
			get
			{
				return ObtenerValorPropiedad<string>("funciones_puesto");
			}

			set
			{
				EstablecerValorPropiedad<string>("funciones_puesto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece puesto_activo.Sin descripcion para puesto_activo 
		/// </summary>
		/// <value>
		/// puesto_activo 
		/// </value>
		[DefaultValue(null)]
		public Boolean? puesto_activo
		{
			get
			{
				return ObtenerValorPropiedad<Boolean?>("puesto_activo");
			}

			set
			{
				EstablecerValorPropiedad<Boolean?>("puesto_activo", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla puesto.		/// </summary>
		/// <param name="piclave_puesto">clave_puesto</param>
		public void Cargar(Int32 piclave_puesto)
		{
			base.Cargar(piclave_puesto);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32?> loColInt32N; 
			 PropiedadesColumna<Boolean?> loColBooleanN; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(6);

			AgregaCampoLlave("clave_puesto",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para clave_puesto";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("clave_puesto", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 200;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para descripcion_puesto";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion_puesto", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 2;
			loColInt32N.Descripcion = "Sin descripcion para nivel_puesto";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("nivel_puesto", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para tipo_puesto";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_puesto", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 16;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para funciones_puesto";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("funciones_puesto", loColstring); 

			loColBooleanN = new PropiedadesColumna<Boolean?>();
			loColBooleanN.Valor = null;
			loColBooleanN.EsPrimaryKey = false;
			loColBooleanN.Longitud = 1;
			loColBooleanN.Precision = 1;
			loColBooleanN.EsRequeridoBD = false;
			loColBooleanN.CampoId = 5;
			loColBooleanN.Descripcion = "Sin descripcion para puesto_activo";
			loColBooleanN.EsIdentity = false;
			loColBooleanN.Tipo = typeof(Boolean?);
			AgregarPropiedad<Boolean?>("puesto_activo", loColBooleanN); 
			}
			#endregion

		}
	}
