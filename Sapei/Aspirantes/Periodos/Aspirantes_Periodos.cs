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
	/// Clase aspirantes_periodo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Aspirantes_Periodos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase aspirantes_periodo.
		/// </summary>
		public Aspirantes_Periodos():base()
		{
			NombreTabla = "aspirantes_periodos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase aspirantes_periodo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Aspirantes_Periodos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "aspirantes_periodos";
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
		/// Obtiene o establece vuelta.Sin descripcion para vuelta 
		/// </summary>
		/// <value>
		/// vuelta 
		/// </value>
		[Key]
		[Required]
		[DefaultValue("1")]
		public Byte vuelta
		{
			get
			{
				return ObtenerValorPropiedad<Byte>("vuelta");
			}

			set
			{
				EstablecerValorPropiedad<Byte>("vuelta", value);
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
		/// Obtiene o establece activo.Sin descripcion para activo 
		/// </summary>
		/// <value>
		/// activo 
		/// </value>
		[Required]
		[DefaultValue(false)]
		public Boolean activo
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("activo");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("activo", value);
			}

		}
          /// <summary>
          /// Obtiene o establece fin_registro.Sin descripcion para fin_registro 
          /// </summary>
          /// <value>
          /// fin_registro 
          /// </value>
          [Required]
          public DateTime fecha_examen
          {
               get
               {
                    return ObtenerValorPropiedad<DateTime>("fecha_examen");
               }

               set
               {
                    EstablecerValorPropiedad<DateTime>("fecha_examen", value);
               }

          }
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla aspirantes_periodo.		/// </summary>
		/// <param name="psperiodo">periodo</param>
		/// <param name="pbyvuelta">vuelta</param>
		public void Cargar(string psperiodo,Byte pbyvuelta)
		{
			base.Cargar(psperiodo,pbyvuelta);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Byte> loColByte; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<Boolean> loColBoolean; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(2);
			Propiedades = new Dictionary<string, Propiedad>(6);

			AgregaCampoLlave("periodo",null);
			AgregaCampoLlave("vuelta",null);

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

			loColByte = new PropiedadesColumna<Byte>();
			loColByte.Valor = 1;
			loColByte.EsPrimaryKey = true;
			loColByte.Longitud = 1;
			loColByte.Precision = 3;
			loColByte.EsRequeridoBD = true;
			loColByte.CampoId = 1;
			loColByte.Descripcion = "Sin descripcion para vuelta";
			loColByte.EsIdentity = false;
			loColByte.Tipo = typeof(Byte);
			AgregarPropiedad<Byte>("vuelta", loColByte); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
               loColDateTime.IncluyeHoras = true;
			loColDateTime.Descripcion = "Sin descripcion para ini_registro";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("ini_registro", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 3;
               loColDateTime.IncluyeHoras = true;
			loColDateTime.Descripcion = "Sin descripcion para fin_registro";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fin_registro", loColDateTime); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.Valor = false;
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 4;
			loColBoolean.Descripcion = "Sin descripcion para activo";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("activo", loColBoolean);

               loColDateTime = new PropiedadesColumna<DateTime>();
               loColDateTime.EsPrimaryKey = false;
               loColDateTime.Longitud = 8;
               loColDateTime.Precision = 23;
               loColDateTime.EsRequeridoBD = true;
               loColDateTime.CampoId = 5;
               loColDateTime.IncluyeHoras = true;
               loColDateTime.Descripcion = "Sin descripcion para fecha_examen";
               loColDateTime.EsIdentity = false;
               loColDateTime.Tipo = typeof(DateTime);
               AgregarPropiedad<DateTime>("fecha_examen", loColDateTime); 
			}
			#endregion

		}
	}
