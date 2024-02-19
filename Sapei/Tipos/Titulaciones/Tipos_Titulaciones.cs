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
	/// Clase tipos_titulacione generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Tipos_Titulaciones:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase tipos_titulacione.
		/// </summary>
		public Tipos_Titulaciones():base()
		{
			NombreTabla = "tipos_titulaciones";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase tipos_titulacione.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Tipos_Titulaciones(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "tipos_titulaciones";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece id_tipo.Sin descripcion para id_tipo 
		/// </summary>
		/// <value>
		/// id_tipo 
		/// </value>
		[Key]
		[Required]
		public Int32 id_tipo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_tipo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_tipo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo.Sin descripcion para tipo 
		/// </summary>
		/// <value>
		/// tipo 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string tipo
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece activo.Sin descripcion para activo 
		/// </summary>
		/// <value>
		/// activo 
		/// </value>
		[DefaultValue(null)]
		public Boolean? activo
		{
			get
			{
				return ObtenerValorPropiedad<Boolean?>("activo");
			}

			set
			{
				EstablecerValorPropiedad<Boolean?>("activo", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla tipos_titulacione.		/// </summary>
		/// <param name="piid_tipo">id_tipo</param>
		public void Cargar(Int32 piid_tipo)
		{
			base.Cargar(piid_tipo);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Boolean?> loColBooleanN; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(3);

			AgregaCampoLlave("id_tipo",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para id_tipo";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_tipo", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para tipo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo", loColstring); 

			loColBooleanN = new PropiedadesColumna<Boolean?>();
			loColBooleanN.Valor = null;
			loColBooleanN.EsPrimaryKey = false;
			loColBooleanN.Longitud = 1;
			loColBooleanN.Precision = 1;
			loColBooleanN.EsRequeridoBD = false;
			loColBooleanN.CampoId = 2;
			loColBooleanN.Descripcion = "Sin descripcion para activo";
			loColBooleanN.EsIdentity = false;
			loColBooleanN.Tipo = typeof(Boolean?);
			AgregarPropiedad<Boolean?>("activo", loColBooleanN); 
			}
			#endregion

		}
	}
