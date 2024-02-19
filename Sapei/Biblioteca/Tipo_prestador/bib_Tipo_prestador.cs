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
	/// Clase bib_Tipo_prestador generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Tipo_prestadores:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Tipo_prestador.
		/// </summary>
		public Bib_Tipo_prestadores():base()
		{
			NombreTabla = "bib_Tipo_prestador";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Tipo_prestador.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Tipo_prestadores(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_Tipo_prestador";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_tipo.Sin descripcion para Id_tipo 
		/// </summary>
		/// <value>
		/// Id_tipo 
		/// </value>
		[Key]
		[Required]
		public Int32 Id_tipo
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
		/// Obtiene o establece Descripcion.Sin descripcion para Descripcion 
		/// </summary>
		/// <value>
		/// Descripcion 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string Descripcion
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
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla bib_Tipo_prestador.		/// </summary>
		/// <param name="piId_tipo">Id_tipo</param>
		public void Cargar(Int32 piId_tipo)
		{
			base.Cargar(piId_tipo);
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
			Propiedades = new Dictionary<string, Propiedad>(2);

			AgregaCampoLlave("id_tipo",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para Id_tipo";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_tipo", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para Descripcion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion", loColstring); 
			}
			#endregion

		}
	}
