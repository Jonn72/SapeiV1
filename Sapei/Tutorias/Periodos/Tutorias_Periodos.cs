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
	/// Clase extra_actividades_fecha generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Tutorias_Periodos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_actividades_fecha.
		/// </summary>
		public Tutorias_Periodos():base()
		{
			NombreTabla = "tutorias_periodos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_actividades_fecha.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
          public Tutorias_Periodos(Sistema poSistema)
               : base(poSistema)
		{
			NombreTabla = "tutorias_periodos";
			Propietario= "dbo";
               RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece periodo.periodo 
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
		/// Obtiene o establece fecha_ini_registro.fecha_ini_registro 
		/// </summary>
		/// <value>
		/// fecha_ini_registro 
		/// </value>
		[Required]
		public DateTime inicio_grupos
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("inicio_grupos");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("inicio_grupos", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_fin_registro.fecha_fin_registro 
		/// </summary>
		/// <value>
		/// fecha_fin_registro 
		/// </value>
		[Required]
		public DateTime fin_grupos
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fin_grupos");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fin_grupos", value);
			}

		}
          /// <summary>
          /// Obtiene o establece fecha_ini_registro.fecha_ini_registro 
          /// </summary>
          /// <value>
          /// fecha_ini_registro 
          /// </value>
          [Required]
          public DateTime inicio_seleccion
          {
               get
               {
                    return ObtenerValorPropiedad<DateTime>("inicio_seleccion");
               }

               set
               {
                    EstablecerValorPropiedad<DateTime>("inicio_seleccion", value);
               }

          }
          /// <summary>
          /// Obtiene o establece fecha_fin_registro.fecha_fin_registro 
          /// </summary>
          /// <value>
          /// fecha_fin_registro 
          /// </value>
          [Required]
          public DateTime fin_seleccion
          {
               get
               {
                    return ObtenerValorPropiedad<DateTime>("fin_seleccion");
               }

               set
               {
                    EstablecerValorPropiedad<DateTime>("fin_seleccion", value);
               }

          }
          /// <summary>
          /// Obtiene o establece fecha_ini_registro.fecha_ini_registro 
          /// </summary>
          /// <value>
          /// fecha_ini_registro 
          /// </value>
          [Required]
          public DateTime inicio_captura
          {
               get
               {
                    return ObtenerValorPropiedad<DateTime>("inicio_captura");
               }

               set
               {
                    EstablecerValorPropiedad<DateTime>("inicio_captura", value);
               }

          }
          /// <summary>
          /// Obtiene o establece fecha_fin_registro.fecha_fin_registro 
          /// </summary>
          /// <value>
          /// fecha_fin_registro 
          /// </value>
          [Required]
          public DateTime fin_captura
          {
               get
               {
                    return ObtenerValorPropiedad<DateTime>("fin_captura");
               }

               set
               {
                    EstablecerValorPropiedad<DateTime>("fin_captura", value);
               }

          }
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla extra_actividades_fecha.		/// </summary>
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
			 PropiedadesColumna<DateTime> loColDateTime; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(7);

			AgregaCampoLlave("periodo",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 1;
               loColDateTime.Descripcion = "inicio_grupos";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
               loColDateTime.IncluyeHoras = true;
			AgregarPropiedad<DateTime>("inicio_grupos", loColDateTime); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 2;
               loColDateTime.Descripcion = "fin_grupos";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
               loColDateTime.IncluyeHoras = true;
               AgregarPropiedad<DateTime>("fin_grupos", loColDateTime);

               loColDateTime = new PropiedadesColumna<DateTime>();
               loColDateTime.EsPrimaryKey = false;
               loColDateTime.Longitud = 8;
               loColDateTime.Precision = 23;
               loColDateTime.EsRequeridoBD = true;
               loColDateTime.CampoId = 3;
               loColDateTime.Descripcion = "inicio_seleccion";
               loColDateTime.EsIdentity = false;
               loColDateTime.Tipo = typeof(DateTime);
               loColDateTime.IncluyeHoras = true;
               AgregarPropiedad<DateTime>("inicio_seleccion", loColDateTime);

               loColDateTime = new PropiedadesColumna<DateTime>();
               loColDateTime.EsPrimaryKey = false;
               loColDateTime.Longitud = 8;
               loColDateTime.Precision = 23;
               loColDateTime.EsRequeridoBD = true;
               loColDateTime.CampoId = 4;
               loColDateTime.Descripcion = "fin_seleccion";
               loColDateTime.EsIdentity = false;
               loColDateTime.Tipo = typeof(DateTime);
               loColDateTime.IncluyeHoras = true;
               AgregarPropiedad<DateTime>("fin_seleccion", loColDateTime);

               loColDateTime = new PropiedadesColumna<DateTime>();
               loColDateTime.EsPrimaryKey = false;
               loColDateTime.Longitud = 8;
               loColDateTime.Precision = 23;
               loColDateTime.EsRequeridoBD = true;
               loColDateTime.CampoId = 5;
               loColDateTime.Descripcion = "inicio_captura";
               loColDateTime.EsIdentity = false;
               loColDateTime.Tipo = typeof(DateTime);
               loColDateTime.IncluyeHoras = true;
               AgregarPropiedad<DateTime>("inicio_captura", loColDateTime);

               loColDateTime = new PropiedadesColumna<DateTime>();
               loColDateTime.EsPrimaryKey = false;
               loColDateTime.Longitud = 8;
               loColDateTime.Precision = 23;
               loColDateTime.EsRequeridoBD = true;
               loColDateTime.CampoId = 6;
               loColDateTime.Descripcion = "fin_captura";
               loColDateTime.EsIdentity = false;
               loColDateTime.Tipo = typeof(DateTime);
               loColDateTime.IncluyeHoras = true;
               AgregarPropiedad<DateTime>("fin_captura", loColDateTime); 
			}
			#endregion

		}
	}
