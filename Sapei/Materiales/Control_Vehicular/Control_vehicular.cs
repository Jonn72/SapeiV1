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
	/// Clase control_vehicular generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Control_Vehicular:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase control_vehicular.
		/// </summary>
		public Control_Vehicular():base()
		{
			NombreTabla = "control_vehicular";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase control_vehicular.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Control_Vehicular(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "control_vehicular";
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
		/// Obtiene o establece max_autos.Sin descripcion para max_autos 
		/// </summary>
		/// <value>
		/// max_autos 
		/// </value>
		[Required]
		public Int16 max_autos
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("max_autos");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("max_autos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece autos_registrados.Sin descripcion para autos_registrados 
		/// </summary>
		/// <value>
		/// autos_registrados 
		/// </value>
		[Required]
		[DefaultValue("0")]
		public Int16 autos_registrados
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("autos_registrados");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("autos_registrados", value);
			}

		}
		/// <summary>
		/// Obtiene o establece max_motos.Sin descripcion para max_motos 
		/// </summary>
		/// <value>
		/// max_motos 
		/// </value>
		[Required]
		public Int16 max_motos
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("max_motos");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("max_motos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece motos_registradas.Sin descripcion para motos_registradas 
		/// </summary>
		/// <value>
		/// motos_registradas 
		/// </value>
		[Required]
		[DefaultValue("0")]
		public Int16 motos_registradas
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("motos_registradas");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("motos_registradas", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ini_registro.Sin descripcion para ini_registro 
		/// </summary>
		/// <value>
		/// ini_registro 
		/// </value>
		[Required]
		public DateTime ini_registro
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("ini_registro");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("ini_registro", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_registro.Sin descripcion para fin_registro 
		/// </summary>
		/// <value>
		/// fin_registro 
		/// </value>
		[Required]
		public DateTime fin_registro
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_registro");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_registro", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ini_entrega.Sin descripcion para ini_entrega 
		/// </summary>
		/// <value>
		/// ini_entrega 
		/// </value>
		[Required]
		public DateTime ini_entrega
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("ini_entrega");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("ini_entrega", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fin_entrega.Sin descripcion para fin_entrega 
		/// </summary>
		/// <value>
		/// fin_entrega 
		/// </value>
		[Required]
		public DateTime fin_entrega
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_entrega");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_entrega", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla control_vehicular.		/// </summary>
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
			 PropiedadesColumna<Int16> loColInt16; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(9);

			AgregaCampoLlave("periodo",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 1;
			loColInt16.Descripcion = "Sin descripcion para max_autos";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("max_autos", loColInt16); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.Valor = 0;
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 2;
			loColInt16.Descripcion = "Sin descripcion para autos_registrados";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("autos_registrados", loColInt16); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 3;
			loColInt16.Descripcion = "Sin descripcion para max_motos";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("max_motos", loColInt16); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.Valor = 0;
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 4;
			loColInt16.Descripcion = "Sin descripcion para motos_registradas";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("motos_registradas", loColInt16); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 5;
			loColDateTime.Descripcion = "Sin descripcion para ini_registro";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("ini_registro", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 6;
			loColDateTime.Descripcion = "Sin descripcion para fin_registro";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_registro", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 7;
			loColDateTime.Descripcion = "Sin descripcion para ini_entrega";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("ini_entrega", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 8;
			loColDateTime.Descripcion = "Sin descripcion para fin_entrega";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_entrega", loColDateTime); 
			}
			#endregion

		}
	}
