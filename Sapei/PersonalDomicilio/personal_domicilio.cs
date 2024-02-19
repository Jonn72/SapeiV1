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
	/// Clase personal_domicilio generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class personal_domicilio:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_domicilio.
		/// </summary>
		public personal_domicilio():base()
		{
			NombreTabla = "personal_domicilios";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_domicilio.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public personal_domicilio(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "personal_domicilios";
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
		/// Obtiene o establece calle.Sin descripcion para calle 
		/// </summary>
		/// <value>
		/// calle 
		/// </value>
		[Required]
		[MaxLength (60)]
		[DefaultValue(null)]
		public string calle
		{
			get
			{
				return ObtenerValorPropiedad<string>("calle");
			}

			set
			{
				EstablecerValorPropiedad<string>("calle", value);
			}

		}
		/// <summary>
		/// Obtiene o establece numero.Sin descripcion para numero 
		/// </summary>
		/// <value>
		/// numero 
		/// </value>
		[MaxLength (50)]
		[DefaultValue(null)]
		public string numero
		{
			get
			{
				return ObtenerValorPropiedad<string>("numero");
			}

			set
			{
				EstablecerValorPropiedad<string>("numero", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_cp.Sin descripcion para id_cp 
		/// </summary>
		/// <value>
		/// id_cp 
		/// </value>
		[DefaultValue(null)]
		public Int32? id_cp
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("id_cp");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("id_cp", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_cp.Sin descripcion para id_cp 
		/// </summary>
		/// <value>
		/// id_cp 
		/// </value>
		[DefaultValue(null)]
		public Int32? colonia
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("colonia");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("colonia", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla personal_domicilio.		/// </summary>
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
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(4);

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
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para calle";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("calle", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para numero";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("numero", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 3;
			loColInt32N.Descripcion = "Sin descripcion para id_cp";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("id_cp", loColInt32N);

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 4;
			loColInt32N.Descripcion = "Sin descripcion para colonia";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("colonia", loColInt32N);
		}
			#endregion

		}
	}
