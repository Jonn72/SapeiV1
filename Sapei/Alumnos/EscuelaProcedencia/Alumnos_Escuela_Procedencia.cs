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
	/// Clase alumnos_escuela_procedencia generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Alumnos_Escuela_Procedencia:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_escuela_procedencia.
		/// </summary>
		public Alumnos_Escuela_Procedencia():base()
		{
			NombreTabla = "alumnos_escuela_procedencia";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_escuela_procedencia.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Alumnos_Escuela_Procedencia(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "alumnos_escuela_procedencia";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
		/// </summary>
		/// <value>
		/// no_de_control 
		/// </value>
		[Key]
		[Required]
		[MaxLength (10)]
		[DefaultValue(null)]
		public string no_de_control
		{
			get
			{
				return ObtenerValorPropiedad<string>("no_de_control");
			}

			set
			{
				EstablecerValorPropiedad<string>("no_de_control", value);
			}

		}
		/// <summary>
		/// Obtiene o establece anio_egreso.Sin descripcion para anio_egreso 
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
		/// Obtiene o establece promedio.Sin descripcion para promedio 
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
		/// Obtiene o establece id_escuela.Sin descripcion para id_escuela 
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla alumnos_escuela_procedencia.		/// </summary>
		/// <param name="psno_de_control">no_de_control</param>
		public void Cargar(string psno_de_control)
		{
			base.Cargar(psno_de_control);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int16> loColInt16; 
			 PropiedadesColumna<Double> loColDouble; 
			 PropiedadesColumna<Int32> loColInt32; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("no_de_control",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 1;
			loColInt16.Descripcion = "Sin descripcion para anio_egreso";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("anio_egreso", loColInt16); 

			loColDouble = new PropiedadesColumna<Double>();
			loColDouble.EsPrimaryKey = false;
			loColDouble.Longitud = 8;
			loColDouble.Precision = 53;
			loColDouble.EsRequeridoBD = true;
			loColDouble.CampoId = 2;
			loColDouble.Descripcion = "Sin descripcion para promedio";
			loColDouble.EsIdentity = false;
			loColDouble.Tipo = typeof(Double);
			AgregarPropiedad<Double>("promedio", loColDouble); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "Sin descripcion para id_escuela";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_escuela", loColInt32); 
			}
			#endregion

		}
	}
