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
	/// Clase alumnos_domicilio generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Alumnos_domicilio:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_domicilio.
		/// </summary>
		public Alumnos_domicilio():base()
		{
			NombreTabla = "alumnos_domicilios";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_domicilio.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Alumnos_domicilio(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "alumnos_domicilios";
			Propietario= "dbo";
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece id_cp.id_cp 
		/// </summary>
		/// <value>
		/// id_cp 
		/// </value>
		[Required]
		public Int32 id_cp
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_cp");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_cp", value);
			}

		}
		/// <summary>
		/// Obtiene o establece calle.calle 
		/// </summary>
		/// <value>
		/// calle 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string calle
		{
			get
			{
				return ObtenerValorPropiedad<string>("calle");
			}

			set
			{
				EstablecerValorPropiedad<string>("calle", value);
			}

		}
		/// <summary>
		/// Obtiene o establece numero.numero 
		/// </summary>
		/// <value>
		/// numero 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string numero
		{
			get
			{
				return ObtenerValorPropiedad<string>("numero");
			}

			set
			{
				EstablecerValorPropiedad<string>("numero", value);
			}

		}
		/// <summary>
		/// Obtiene o establece no_de_control.no_de_control 
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla alumnos_domicilio.		/// </summary>
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
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("no_de_control",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "id_cp";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_cp", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "calle";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("calle", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "numero";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("numero", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "no_de_control";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("no_de_control", loColstring); 
			}
			#endregion

		}
	}
