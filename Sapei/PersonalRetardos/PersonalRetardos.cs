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
	/// Clase personal_solicitud generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class PersonalRetardos : CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_solicitud.
		/// </summary>
		public PersonalRetardos() : base()
		{
			NombreTabla = "personal_retardos";
			Propietario = "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase personal_solicitud.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public PersonalRetardos(Sistema poSistema) : base(poSistema)
		{
			NombreTabla = "personal_retardos";
			Propietario = "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece id_retardo.Sin descripcion para id_retardo 
		/// </summary>
		/// <value>
		/// id_retardo
		/// </value>
		/// 		
		[Key]
		[Required]
		public Int32 id_retardo
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
		/// Obtiene o establece id.Sin descripcion para id 
		/// </summary>
		/// <value>
		/// id 
		/// </value>
		/// 		
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
		/// Obtiene o establece fecha_inicio.Sin descripcion para fecha_inicio 
		/// </summary>
		/// <value>
		/// periodo_inicio 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_retardo
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_retardo");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_retardo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece status.Sin descripcion para id_monto 
		/// </summary>
		/// <value>
		/// id_monto 
		/// </value>
		[DefaultValue(null)]
		public Int32 horas_retardo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("horas_retardo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("horas_retardo", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla personal_solicitud.		/// </summary>
		/// <param name="psid_retardo">fecha_creacion</param>
		public void Cargar(int psid_retardo)
		{
			base.Cargar(psid_retardo);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			PropiedadesColumna<string> loColstring;
			PropiedadesColumna<DateTime?> loColDateTimeN;
			PropiedadesColumna<Int32> loColInt32;
			if (!Object.Equals(CamposLlave, null) && !Object.Equals(Propiedades, null))
				return;

			CamposLlave = new Dictionary<string, object>(1);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("id_retardo", null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para id_retardo";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_retardo", loColInt32);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.Valor = 0;
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 1;
			loColInt32.Precision = 1;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 1;
			loColInt32.Descripcion = "Sin descripcion para id";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id", loColInt32);

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 2;
			loColDateTimeN.Descripcion = "Sin descripcion para fecha_retardo";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_retardo", loColDateTimeN);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.Valor = 0;
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 1;
			loColInt32.Precision = 1;
			loColInt32.EsRequeridoBD = false;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "Sin descripcion para horas_retardo";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("horas_retardo", loColInt32);
		}
		#endregion
	}
}
