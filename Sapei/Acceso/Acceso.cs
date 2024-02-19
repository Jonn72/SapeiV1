using System;
using System.Collections.Generic;
using System.Linq;
using Sapei.Framework;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sapei
{
	/// <summary>
	/// Clase acceso generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Acceso:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase acceso.
		/// </summary>
		public Acceso():base()
		{
			NombreTabla = "acceso";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase acceso.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Acceso(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "acceso";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece usuario.usuario 
		/// </summary>
		/// <value>
		/// usuario 
		/// </value>
		[Key]
		[Required]
		[MaxLength (30)]
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
		/// Obtiene o establece nombre_usuario.nombre_usuario 
		/// </summary>
		/// <value>
		/// nombre_usuario 
		/// </value>
		[Required]
		[MaxLength (100)]
		[DefaultValue(null)]
		public string nombre_usuario
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_usuario");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_usuario", value);
			}

		}
		/// <summary>
		/// Obtiene o establece contrasena.contrasena 
		/// </summary>
		/// <value>
		/// contrasena 
		/// </value>
		[Required]
		[MaxLength (100)]
		[DefaultValue(null)]
		public string contrasena
		{
			get
			{
				return ObtenerValorPropiedad<string>("contrasena");
			}

			set
			{
				EstablecerValorPropiedad<string>("contrasena", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_usuario.tipo_usuario 
		/// </summary>
		/// <value>
		/// tipo_usuario 
		/// </value>
		[Required]
		[MaxLength (3)]
		[DefaultValue(null)]
		public string tipo_usuario
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_usuario");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_usuario", value);
			}

		}
		/// <summary>
		/// Obtiene o establece status.status 
		/// </summary>
		/// <value>
		/// status 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string status
		{
			get
			{
				return ObtenerValorPropiedad<string>("status");
			}

			set
			{
				EstablecerValorPropiedad<string>("status", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla acceso.		/// </summary>
		/// <param name="psusuario">usuario</param>
		public void Cargar(string psusuario)
		{
			base.Cargar(psusuario);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(5);

			AgregaCampoLlave("usuario",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "nombre_usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_usuario", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "contrasena";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("contrasena", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "tipo_usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_usuario", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "status";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("status", loColstring); 
			}
			#endregion

		}
	}
