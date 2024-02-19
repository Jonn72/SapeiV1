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
	/// Clase rf_tabulador generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class rf_tabulador:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase rf_tabulador.
		/// </summary>
		public rf_tabulador():base()
		{
			NombreTabla = "rf_tabulador";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase rf_tabulador.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public rf_tabulador(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "rf_tabulador";
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
		/// Obtiene o establece status.Sin descripcion para tipo_monto 
		/// </summary>
		/// <value>
		/// tipo_monto 
		/// </value>
		[DefaultValue(null)]
		public string tipo_monto
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_monto");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_monto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece concepto.Sin descripcion para concepto 
		/// </summary>
		/// <value>
		/// concepto 
		/// </value>
		[MaxLength (100)]
		[DefaultValue(null)]
		public string concepto
		{
			get
			{
				return ObtenerValorPropiedad<string>("concepto");
			}

			set
			{
				EstablecerValorPropiedad<string>("concepto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece monto.Sin descripcion para monto 
		/// </summary>
		/// <value>
		/// monto 
		/// </value>
		[DefaultValue(null)]
		public Double? monto
		{
			get
			{
				return ObtenerValorPropiedad<Double?>("monto");
			}

			set
			{
				EstablecerValorPropiedad<Double?>("monto", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla rf_tabulador.		/// </summary>
		public void Cargar(Int32 id)
		{
			base.Cargar(id);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			PropiedadesColumna<Int32> loColInt32; 
			PropiedadesColumna<string> loColstring; 
			PropiedadesColumna<Double?> loColDoubleN; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(0);
			Propiedades = new Dictionary<string, Propiedad>(3);

			AgregaCampoLlave("id", null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para id";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id", loColInt32);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para tipo_monto";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_monto", loColstring);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para concepto";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("concepto", loColstring); 

			loColDoubleN = new PropiedadesColumna<Double?>();
			loColDoubleN.Valor = null;
			loColDoubleN.EsPrimaryKey = false;
			loColDoubleN.Longitud = 8;
			loColDoubleN.Precision = 19;
			loColDoubleN.EsRequeridoBD = false;
			loColDoubleN.CampoId = 3;
			loColDoubleN.Descripcion = "Sin descripcion para monto";
			loColDoubleN.EsIdentity = false;
			loColDoubleN.Tipo = typeof(Double?);
			AgregarPropiedad<Double?>("monto", loColDoubleN); 
			}
			#endregion

		}
	}
