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
	/// Clase sis_logo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Sis_Logos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase sis_logo.
		/// </summary>
		public Sis_Logos():base()
		{
			NombreTabla = "sis_logos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase sis_logo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Sis_Logos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "sis_logos";
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
		/// Obtiene o establece descripcion.Sin descripcion para descripcion 
		/// </summary>
		/// <value>
		/// descripcion 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string descripcion
		{
			get
			{
				return ObtenerValorPropiedad<string>("descripcion");
			}

			set
			{
				EstablecerValorPropiedad<string>("descripcion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece logo.Sin descripcion para logo 
		/// </summary>
		/// <value>
		/// logo 
		/// </value>
		[Required]
		[DefaultValue(null)]
		public Byte[] logo
		{
			get
			{
				return ObtenerValorPropiedad<Byte[]>("logo");
			}

			set
			{
				EstablecerValorPropiedad<Byte[]>("logo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece usuarios_permisos.Sin descripcion para usuarios_permisos 
		/// </summary>
		/// <value>
		/// usuarios_permisos 
		/// </value>
		[MaxLength (50)]
		[DefaultValue(null)]
		public string usuarios_permisos
		{
			get
			{
				return ObtenerValorPropiedad<string>("usuarios_permisos");
			}

			set
			{
				EstablecerValorPropiedad<string>("usuarios_permisos", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla sis_logo.		/// </summary>
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
			 PropiedadesColumna<Byte[]> loColByteA; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(4);

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
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para descripcion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion", loColstring); 

			loColByteA = new PropiedadesColumna<Byte[]>();
			loColByteA.Valor = null;
			loColByteA.EsPrimaryKey = false;
			loColByteA.Longitud = 16;
			loColByteA.Precision = 0;
			loColByteA.EsRequeridoBD = true;
			loColByteA.CampoId = 2;
			loColByteA.Descripcion = "Sin descripcion para logo";
			loColByteA.EsIdentity = false;
			loColByteA.Tipo = typeof(Byte[]);
			AgregarPropiedad<Byte[]>("logo", loColByteA); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para usuarios_permisos";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuarios_permisos", loColstring); 
			}
			#endregion

		}
	}
