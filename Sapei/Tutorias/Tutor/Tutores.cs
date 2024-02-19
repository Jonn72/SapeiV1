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
	/// Clase tutorias_tutore generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Tutores:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase tutorias_tutore.
		/// </summary>
		public Tutores():base()
		{
			NombreTabla = "tutorias_tutores";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase tutorias_tutore.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Tutores(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "tutorias_tutores";
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
		/// Obtiene o establece fecha_registro.Sin descripcion para fecha_registro 
		/// </summary>
		/// <value>
		/// fecha_registro 
		/// </value>
		[Required]
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla tutorias_tutore.		/// </summary>
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
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<Boolean> loColBoolean; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(3);

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

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 1;
			loColDateTime.Descripcion = "Sin descripcion para fecha_registro";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_registro", loColDateTime); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.Valor = false;
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 2;
			loColBoolean.Descripcion = "Sin descripcion para estatus";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("estatus", loColBoolean); 
			}
			#endregion

		}
	}
