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
	/// Clase control_vehicular_registro generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Control_Vehicular_Registro:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase control_vehicular_registro.
		/// </summary>
		public Control_Vehicular_Registro():base()
		{
			NombreTabla = "control_vehicular_registros";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase control_vehicular_registro.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Control_Vehicular_Registro(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "control_vehicular_registros";
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
		/// Obtiene o establece usuario.Sin descripcion para usuario 
		/// </summary>
		/// <value>
		/// usuario 
		/// </value>
		[Key]
		[Required]
		[MaxLength (13)]
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
		/// <summary>
		/// Obtiene o establece tipo_vehiculo.Sin descripcion para tipo_vehiculo 
		/// </summary>
		/// <value>
		/// tipo_vehiculo 
		/// </value>
		[Key]
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string tipo_vehiculo
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_vehiculo");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_vehiculo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece marca.Sin descripcion para marca 
		/// </summary>
		/// <value>
		/// marca 
		/// </value>
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string marca
		{
			get
			{
				return ObtenerValorPropiedad<string>("marca");
			}

			set
			{
				EstablecerValorPropiedad<string>("marca", value);
			}

		}
		/// <summary>
		/// Obtiene o establece submarca.Sin descripcion para submarca 
		/// </summary>
		/// <value>
		/// submarca 
		/// </value>
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string submarca
		{
			get
			{
				return ObtenerValorPropiedad<string>("submarca");
			}

			set
			{
				EstablecerValorPropiedad<string>("submarca", value);
			}

		}
		/// <summary>
		/// Obtiene o establece placas.Sin descripcion para placas 
		/// </summary>
		/// <value>
		/// placas 
		/// </value>
		[Required]
		[MaxLength (7)]
		[DefaultValue(null)]
		public string placas
		{
			get
			{
				return ObtenerValorPropiedad<string>("placas");
			}

			set
			{
				EstablecerValorPropiedad<string>("placas", value);
			}

		}
		/// <summary>
		/// Obtiene o establece color.Sin descripcion para color 
		/// </summary>
		/// <value>
		/// color 
		/// </value>
		[Required]
		[MaxLength (7)]
		[DefaultValue(null)]
		public string color
		{
			get
			{
				return ObtenerValorPropiedad<string>("color");
			}

			set
			{
				EstablecerValorPropiedad<string>("color", value);
			}

		}
		/// <summary>
		/// Obtiene o establece modelo.Sin descripcion para modelo 
		/// </summary>
		/// <value>
		/// modelo 
		/// </value>
		[Required]
		[MaxLength (4)]
		[DefaultValue(null)]
		public string modelo
		{
			get
			{
				return ObtenerValorPropiedad<string>("modelo");
			}

			set
			{
				EstablecerValorPropiedad<string>("modelo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tarjeton.Sin descripcion para tarjeton 
		/// </summary>
		/// <value>
		/// tarjeton 
		/// </value>
		[MaxLength (10)]
		[DefaultValue(null)]
		public string tarjeton
		{
			get
			{
				return ObtenerValorPropiedad<string>("tarjeton");
			}

			set
			{
				EstablecerValorPropiedad<string>("tarjeton", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estado_registro.Sin descripcion para estado_registro 
		/// </summary>
		/// <value>
		/// estado_registro 
		/// </value>
		[Required]
		[MaxLength (3)]
		[DefaultValue("0")]
		public string estado_registro
		{
			get
			{
				return ObtenerValorPropiedad<string>("estado_registro");
			}

			set
			{
				EstablecerValorPropiedad<string>("estado_registro", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_registro.Sin descripcion para fecha_registro 
		/// </summary>
		/// <value>
		/// fecha_registro 
		/// </value>
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
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
		/// Obtiene o establece qr.Sin descripcion para qr 
		/// </summary>
		/// <value>
		/// qr 
		/// </value>
		[DefaultValue(null)]
		public Byte[] qr
		{
			get
			{
				return ObtenerValorPropiedad<Byte[]>("qr");
			}

			set
			{
				EstablecerValorPropiedad<Byte[]>("qr", value);
			}

		}
		/// <summary>
		/// Obtiene o establece cadena.Sin descripcion para cadena 
		/// </summary>
		/// <value>
		/// cadena 
		/// </value>
		[MaxLength (500)]
		[DefaultValue(null)]
		public string cadena
		{
			get
			{
				return ObtenerValorPropiedad<string>("cadena");
			}

			set
			{
				EstablecerValorPropiedad<string>("cadena", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla control_vehicular_registro.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="psusuario">usuario</param>
		/// <param name="pstipo_vehiculo">tipo_vehiculo</param>
		public void Cargar(string psperiodo,string psusuario,string pstipo_vehiculo)
		{
			base.Cargar(psperiodo,psusuario,pstipo_vehiculo);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Byte[]> loColByteA;
                PropiedadesColumna<DateTime> loColDateTime;
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(3);
			Propiedades = new Dictionary<string, Propiedad>(13);

			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("usuario",null);
			AgregaCampoLlave("tipo_vehiculo",null);

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

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para tipo_vehiculo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_vehiculo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para marca";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("marca", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para submarca";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("submarca", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 7;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para placas";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("placas", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 7;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para color";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("color", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 4;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para modelo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("modelo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "Sin descripcion para tarjeton";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tarjeton", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = "0";
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "Sin descripcion para estado_registro";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estado_registro", loColstring); 

               loColDateTime = new PropiedadesColumna<DateTime>();
               loColDateTime.EsPrimaryKey = false;
               loColDateTime.Longitud = 8;
               loColDateTime.Precision = 23;
               loColDateTime.EsRequeridoBD = true;
               loColDateTime.CampoId = 10;
               loColDateTime.Descripcion = "fecha_registro";
               loColDateTime.EsIdentity = false;
               loColDateTime.Tipo = typeof(DateTime);
               AgregarPropiedad<DateTime>("fecha_registro", loColDateTime);

               loColByteA = new PropiedadesColumna<Byte[]>();
               loColByteA.Valor = null;
               loColByteA.EsPrimaryKey = false;
               loColByteA.Longitud = 16;
               loColByteA.Precision = 0;
               loColByteA.EsRequeridoBD = false;
               loColByteA.CampoId = 11;
               loColByteA.Descripcion = "Sin descripcion para qr";
               loColByteA.EsIdentity = false;
               loColByteA.Tipo = typeof(Byte[]);
               AgregarPropiedad<Byte[]>("qr", loColByteA); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 500;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 12;
			loColstring.Descripcion = "Sin descripcion para cadena";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("cadena", loColstring); 
			}
			#endregion

		}
	}
