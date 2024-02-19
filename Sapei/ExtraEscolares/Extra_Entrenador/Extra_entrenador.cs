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
	/// Clase extra_entrenador generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Extra_entrenador:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_entrenador.
		/// </summary>
		public Extra_entrenador():base()
		{
			NombreTabla = "extra_entrenador";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_entrenador.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Extra_entrenador(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "extra_entrenador";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece id.Sin descripcion para id 
		/// </summary>
		/// <value>
		/// id 
		/// </value>
		[Key]
		[Required]
		public Int32 id
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre.Sin descripcion para nombre 
		/// </summary>
		/// <value>
		/// nombre 
		/// </value>
		[Required]
		[MaxLength (35)]
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
		/// Obtiene o establece paterno.Sin descripcion para paterno 
		/// </summary>
		/// <value>
		/// paterno 
		/// </value>
		[Required]
		[MaxLength (45)]
		[DefaultValue(null)]
		public string paterno
		{
			get
			{
				return ObtenerValorPropiedad<string>("paterno");
			}

			set
			{
				EstablecerValorPropiedad<string>("paterno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece materno.Sin descripcion para materno 
		/// </summary>
		/// <value>
		/// materno 
		/// </value>
		[Required]
		[MaxLength (45)]
		[DefaultValue(null)]
		public string materno
		{
			get
			{
				return ObtenerValorPropiedad<string>("materno");
			}

			set
			{
				EstablecerValorPropiedad<string>("materno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estatus.Sin descripcion para estatus 
		/// </summary>
		/// <value>
		/// estatus 
		/// </value>
		[Required]
		[DefaultValue(false)]
		public Boolean estatus
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("estatus");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("estatus", value);
			}

		}
		/// <summary>
		/// Obtiene o establece usuario.Sin descripcion para usuario 
		/// </summary>
		/// <value>
		/// usuario 
		/// </value>
		[MaxLength (14)]
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
		/// Obtiene o establece contrasenia.Sin descripcion para contrasenia 
		/// </summary>
		/// <value>
		/// contrasenia 
		/// </value>
		[MaxLength (8)]
		[DefaultValue(null)]
		public string contrasenia
		{
			get
			{
				return ObtenerValorPropiedad<string>("contrasenia");
			}

			set
			{
				EstablecerValorPropiedad<string>("contrasenia", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla extra_entrenador.		/// </summary>
		/// <param name="piid">id</param>
		public void Cargar(Int32 piid)
		{
			base.Cargar(piid);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Boolean> loColBoolean; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(7);

			AgregaCampoLlave("id",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para id";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 35;
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
			loColstring.Longitud = 45;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para paterno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("paterno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 45;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para materno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("materno", loColstring); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.Valor = false;
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 4;
			loColBoolean.Descripcion = "Sin descripcion para estatus";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("estatus", loColBoolean); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 14;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 8;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "Sin descripcion para contrasenia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("contrasenia", loColstring); 
			}
			#endregion

		}
	}
