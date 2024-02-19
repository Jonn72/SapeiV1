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
	/// Clase rp_domicilio generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class RP_domicilio_dependencias:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_domicilio.
		/// </summary>
		public RP_domicilio_dependencias():base()
		{
			NombreTabla = "rp_domicilio_dependencias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase rp_domicilio.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
        public RP_domicilio_dependencias(Sistema poSistema)
            : base(poSistema)
		{
			NombreTabla = "rp_domicilio_dependencias";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece rfc_domicilio.Sin descripcion para rfc_domicilio 
		/// </summary>
		/// <value>
		/// rfc_domicilio 
		/// </value>
		[Key]
		[Required]
		[MaxLength (13)]
		[DefaultValue(null)]
		public string rfc_domicilio
		{
			get
			{
				return ObtenerValorPropiedad<string>("rfc_domicilio");
			}

			set
			{
				EstablecerValorPropiedad<string>("rfc_domicilio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio.Sin descripcion para domicilio 
		/// </summary>
		/// <value>
		/// domicilio 
		/// </value>
		[Required]
		[MaxLength (60)]
		[DefaultValue(null)]
		public string domicilio
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece numero.Sin descripcion para numero 
		/// </summary>
		/// <value>
		/// numero 
		/// </value>
		[Required]
		public Int32 numero
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("numero");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("numero", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_cp.Sin descripcion para id_cp 
		/// </summary>
		/// <value>
		/// id_cp 
		/// </value>
		[Required]
		public Int32 id_cp
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_cp");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_cp", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla rp_domicilio.		/// </summary>
		/// <param name="psrfc_domicilio">rfc_domicilio</param>
		public void Cargar(string psrfc_domicilio)
		{
			base.Cargar(psrfc_domicilio);
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
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("rfc_domicilio",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para rfc_domicilio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("rfc_domicilio", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para domicilio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para numero";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("numero", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "Sin descripcion para id_cp";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_cp", loColInt32); 
			}
			#endregion

		}
	}
