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
	/// Clase SisCombo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class SisCombo:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase SisCombo.
		/// </summary>
		public SisCombo():base()
		{
			NombreTabla = "sisCombos";
			Propietario= "dbo";
               RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase SisCombo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public SisCombo(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "sisCombos";
			Propietario= "dbo";
               RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece combo.combo 
		/// </summary>
		/// <value>
		/// combo 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string combo
		{
			get
			{
				return ObtenerValorPropiedad<string>("combo");
			}

			set
			{
				EstablecerValorPropiedad<string>("combo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece valor.valor 
		/// </summary>
		/// <value>
		/// valor 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string valor
		{
			get
			{
				return ObtenerValorPropiedad<string>("valor");
			}

			set
			{
				EstablecerValorPropiedad<string>("valor", value);
			}

		}
		/// <summary>
		/// Obtiene o establece descripcion.descripcion 
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
          /// Obtiene o establece valor.padre 
          /// </summary>
          /// <value>
          /// valor_padre 
          /// </value>
          [MaxLength(5)]
          [DefaultValue(null)]
          public string valor_padre
          {
               get
               {
                    return ObtenerValorPropiedad<string>("valor_padre");
               }

               set
               {
                    EstablecerValorPropiedad<string>("valor_padre", value);
               }

          }
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla SisCombo.		/// </summary>
		public void Cargar()
		{
			base.Cargar();
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

			CamposLlave= new Dictionary<string,object>(0);
			Propiedades = new Dictionary<string, Propiedad>(3);


			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "combo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("combo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "valor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("valor", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "descripcion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 5;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = false;
               loColstring.CampoId = 3;
               loColstring.Descripcion = "valor_padre";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("valor_padre", loColstring); 
			}
			#endregion

		}
	}
