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
	/// Clase extra_actividade generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Extra_actividad:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_actividade.
		/// </summary>
		public Extra_actividad():base()
		{
			NombreTabla = "extra_actividades";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase extra_actividade.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Extra_actividad(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "extra_actividades";
			Propietario= "dbo";
               RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece id.id 
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
		/// Obtiene o establece periodo.periodo 
		/// </summary>
		/// <value>
		/// periodo 
		/// </value>
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
		/// Obtiene o establece tipo.tipo 
		/// </summary>
		/// <value>
		/// tipo 
		/// </value>
		[Required]
		[MaxLength (3)]
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
		/// Obtiene o establece descripcion.descripcion 
		/// </summary>
		/// <value>
		/// descripcion 
		/// </value>
		[Required]
		[MaxLength (100)]
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
		/// Obtiene o establece id_entrenador.id_entrenador 
		/// </summary>
		/// <value>
		/// id_entrenador 
		/// </value>
		[DefaultValue(null)]
		public Int32? id_entrenador
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("id_entrenador");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("id_entrenador", value);
			}

		}
		/// <summary>
		/// Obtiene o establece capacidad.capacidad 
		/// </summary>
		/// <value>
		/// capacidad 
		/// </value>
		[Required]
		public Int32 capacidad
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("capacidad");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("capacidad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece inscritos.inscritos 
		/// </summary>
		/// <value>
		/// inscritos 
		/// </value>
		[Required]
		public Int32 inscritos
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("inscritos");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("inscritos", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla extra_actividade.		/// </summary>
		/// <param name="piid">id</param>
		public void Cargar(Int32 piid)
		{
			base.Cargar(piid);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32?> loColInt32N; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(7);

			AgregaCampoLlave("id",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "id";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "periodo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "tipo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "descripcion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("descripcion", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 4;
			loColInt32N.Descripcion = "id_entrenador";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("id_entrenador", loColInt32N); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 5;
			loColInt32.Descripcion = "capacidad";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("capacidad", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 6;
			loColInt32.Descripcion = "inscritos";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("inscritos", loColInt32); 
			}
			#endregion

		}
	}
