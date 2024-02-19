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
	/// Clase cle_nivele generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Cle_Niveles:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase cle_nivele.
		/// </summary>
		public Cle_Niveles():base()
		{
			NombreTabla = "cle_niveles";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase cle_nivele.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Cle_Niveles(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "cle_niveles";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece nivel.Sin descripcion para nivel 
		/// </summary>
		/// <value>
		/// nivel 
		/// </value>
		[Key]
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string nivel
		{
			get
			{
				return ObtenerValorPropiedad<string>("nivel");
			}

			set
			{
				EstablecerValorPropiedad<string>("nivel", value);
			}

		}
		/// <summary>
		/// Obtiene o establece descripcion.Sin descripcion para descripcion 
		/// </summary>
		/// <value>
		/// descripcion 
		/// </value>
		[Required]
		[DefaultValue(null)]
		public short serie
		{
			get
			{
                    return ObtenerValorPropiedad<short>("serie");
			}

			set
			{
                    EstablecerValorPropiedad<short>("serie", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla cle_nivele.		/// </summary>
		/// <param name="psnivel">nivel</param>
		public void Cargar(string psnivel)
		{
			base.Cargar(psnivel);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring;
                PropiedadesColumna<short> loColshort; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(2);

			AgregaCampoLlave("nivel",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para nivel";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nivel", loColstring);

               loColshort = new PropiedadesColumna<short>();
               loColshort.EsPrimaryKey = false;
               loColshort.Precision = 0;
               loColshort.EsRequeridoBD = true;
               loColshort.CampoId = 1;
               loColshort.Descripcion = "Sin descripcion para descripcion";
               loColshort.EsIdentity = false;
               loColshort.Tipo = typeof(short);
               AgregarPropiedad<short>("serie", loColshort); 
			}
			#endregion

		}
	}
