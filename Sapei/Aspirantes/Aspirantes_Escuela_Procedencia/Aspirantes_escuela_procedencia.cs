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
	/// Clase aspirantes_escuela_procedencia generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Aspirante_escuela_procedencia:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase aspirantes_escuela_procedencia.
		/// </summary>
		public Aspirante_escuela_procedencia():base()
		{
			NombreTabla = "aspirantes_escuela_procedencia";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase aspirantes_escuela_procedencia.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Aspirante_escuela_procedencia(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "aspirantes_escuela_procedencia";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece anio_egreso.anio_egreso 
		/// </summary>
		/// <value>
		/// anio_egreso 
		/// </value>
		[Required]
		public Int16 anio_egreso
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("anio_egreso");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("anio_egreso", value);
			}

		}
		/// <summary>
		/// Obtiene o establece id_escuela.id_escuela 
		/// </summary>
		/// <value>
		/// id_escuela 
		/// </value>
		[Required]
		public Int32 id_escuela
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_escuela");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_escuela", value);
			}

		}
		/// <summary>
		/// Obtiene o establece promedio.promedio 
		/// </summary>
		/// <value>
		/// promedio 
		/// </value>
		[Required]
		public Double promedio
		{
			get
			{
				return ObtenerValorPropiedad<Double>("promedio");
			}

			set
			{
				EstablecerValorPropiedad<Double>("promedio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece folio.folio 
		/// </summary>
		/// <value>
		/// folio 
		/// </value>
		[Key]
		[Required]
		[MaxLength (8)]
		[DefaultValue(null)]
		public string folio
		{
			get
			{
				return ObtenerValorPropiedad<string>("folio");
			}

			set
			{
				EstablecerValorPropiedad<string>("folio", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla aspirantes_escuela_procedencia.		/// </summary>
		/// <param name="psfolio">folio</param>
		public void Cargar(string psfolio)
		{
			base.Cargar(psfolio);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int16> loColInt16; 
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<Double> loColDouble; 
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("folio",null);

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 1;
			loColInt16.Descripcion = "anio_egreso";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("anio_egreso", loColInt16); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "id_escuela";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_escuela", loColInt32); 

			loColDouble = new PropiedadesColumna<Double>();
			loColDouble.EsPrimaryKey = false;
			loColDouble.Longitud = 8;
			loColDouble.Precision = 53;
			loColDouble.EsRequeridoBD = true;
			loColDouble.CampoId = 2;
			loColDouble.Descripcion = "promedio";
			loColDouble.EsIdentity = false;
			loColDouble.Tipo = typeof(Double);
			AgregarPropiedad<Double>("promedio", loColDouble); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 8;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "folio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("folio", loColstring); 
			}
			#endregion

		}
	}
