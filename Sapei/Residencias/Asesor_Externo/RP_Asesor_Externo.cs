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
	/// Clase rp_asesor_externo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class rp_asesor_externo:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_asesor_externo.
		/// </summary>
		public rp_asesor_externo():base()
		{
			NombreTabla = "rp_asesor_externo";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_asesor_externo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public rp_asesor_externo(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "rp_asesor_externo";
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
		/// Obtiene o establece nombre.Sin descripcion para nombre 
		/// </summary>
		/// <value>
		/// nombre 
		/// </value>
		[Required]
		[MaxLength (15)]
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
		/// Obtiene o establece apellido_paterno.Sin descripcion para apellido_paterno 
		/// </summary>
		/// <value>
		/// apellido_paterno 
		/// </value>
		[Required]
		[MaxLength (15)]
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
		[Required]
		[MaxLength (15)]
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
		/// Obtiene o establece puesto.Sin descripcion para puesto 
		/// </summary>
		/// <value>
		/// puesto 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string puesto
		{
			get
			{
				return ObtenerValorPropiedad<string>("puesto");
			}

			set
			{
				EstablecerValorPropiedad<string>("puesto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece correo.Sin descripcion para correo 
		/// </summary>
		/// <value>
		/// correo 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string correo
		{
			get
			{
				return ObtenerValorPropiedad<string>("correo");
			}

			set
			{
				EstablecerValorPropiedad<string>("correo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece telefono.Sin descripcion para telefono 
		/// </summary>
		/// <value>
		/// telefono 
		/// </value>
		[Required]
		public Int32 telefono
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("telefono");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("telefono", value);
			}

		}
		/// <summary>
		/// Obtiene o establece rfc_datos.Sin descripcion para rfc_datos 
		/// </summary>
		/// <value>
		/// rfc_datos 
		/// </value>
		[Required]
		[MaxLength (13)]
		[DefaultValue(null)]
		public string rfc_datos
		{
			get
			{
				return ObtenerValorPropiedad<string>("rfc_datos");
			}

			set
			{
				EstablecerValorPropiedad<string>("rfc_datos", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla rp_asesor_externo.		/// </summary>
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
			 PropiedadesColumna<Int32> loColInt32; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(8);

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
			loColstring.Longitud = 15;
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
			loColstring.Longitud = 15;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para apellido_paterno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("apellido_paterno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 15;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para apellido_materno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("apellido_materno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para puesto";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("puesto", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para correo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("correo", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 6;
			loColInt32.Descripcion = "Sin descripcion para telefono";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("telefono", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para rfc_datos";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc_datos", loColstring); 
			}
			#endregion

		}
	}
