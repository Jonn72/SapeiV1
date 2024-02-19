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
	/// Clase sis_consulta generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Sis_Consultas:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase sis_consulta.
		/// </summary>
		public Sis_Consultas():base()
		{
			NombreTabla = "sis_consultas";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase sis_consulta.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Sis_Consultas(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "sis_consultas";
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
		public Byte id
		{
			get
			{
				return ObtenerValorPropiedad<Byte>("id");
			}

			set
			{
				EstablecerValorPropiedad<Byte>("id", value);
			}

		}
		/// <summary>
		/// Obtiene o establece titulo.Sin descripcion para titulo 
		/// </summary>
		/// <value>
		/// titulo 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string titulo
		{
			get
			{
				return ObtenerValorPropiedad<string>("titulo");
			}

			set
			{
				EstablecerValorPropiedad<string>("titulo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece url.Sin descripcion para url 
		/// </summary>
		/// <value>
		/// url 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string url
		{
			get
			{
				return ObtenerValorPropiedad<string>("url");
			}

			set
			{
				EstablecerValorPropiedad<string>("url", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla sis_consulta.		/// </summary>
		/// <param name="pbyid">id</param>
		public void Cargar(Byte pbyid)
		{
			base.Cargar(pbyid);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Byte> loColByte; 
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(3);

			AgregaCampoLlave("id",null);

			loColByte = new PropiedadesColumna<Byte>();
			loColByte.EsPrimaryKey = true;
			loColByte.Longitud = 1;
			loColByte.Precision = 3;
			loColByte.EsRequeridoBD = true;
			loColByte.CampoId = 0;
			loColByte.Descripcion = "Sin descripcion para id";
			loColByte.EsIdentity = false;
			loColByte.Tipo = typeof(Byte);
			AgregarPropiedad<Byte>("id", loColByte); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para titulo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("titulo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para url";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("url", loColstring); 
			}
			#endregion

		}
	}
