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
	/// Clase mantenimiento_orden generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class mantenimiento_orden:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase mantenimiento_orden.
		/// </summary>
		public mantenimiento_orden():base()
		{
			NombreTabla = "mantenimiento_orden";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase mantenimiento_orden.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public mantenimiento_orden(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "mantenimiento_orden";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece perdiodo.Sin descripcion para perdiodo 
		/// </summary>
		/// <value>
		/// perdiodo 
		/// </value>
		[Required]
		public Int32 perdiodo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("perdiodo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("perdiodo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece area_solicitada.Sin descripcion para area_solicitada 
		/// </summary>
		/// <value>
		/// area_solicitada 
		/// </value>
		[Required]
		public Int32 area_solicitada
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("area_solicitada");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("area_solicitada", value);
			}

		}
		/// <summary>
		/// Obtiene o establece folio.Sin descripcion para folio 
		/// </summary>
		/// <value>
		/// folio 
		/// </value>
		[Required]
		public Int16 folio
		{
			get
			{
				return ObtenerValorPropiedad<Int16>("folio");
			}

			set
			{
				EstablecerValorPropiedad<Int16>("folio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_mantenimiento.Sin descripcion para tipo_mantenimiento 
		/// </summary>
		/// <value>
		/// tipo_mantenimiento 
		/// </value>
		[Required]
		[MaxLength (2)]
		[DefaultValue(null)]
		public string tipo_mantenimiento
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_mantenimiento");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_mantenimiento", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_servicio.Sin descripcion para tipo_servicio 
		/// </summary>
		/// <value>
		/// tipo_servicio 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string tipo_servicio
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_servicio");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_servicio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece asignado.Sin descripcion para asignado 
		/// </summary>
		/// <value>
		/// asignado 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string asignado
		{
			get
			{
				return ObtenerValorPropiedad<string>("asignado");
			}

			set
			{
				EstablecerValorPropiedad<string>("asignado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_realizacion.Sin descripcion para fecha_realizacion 
		/// </summary>
		/// <value>
		/// fecha_realizacion 
		/// </value>
		[Required]
		public DateTime fecha_realizacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_realizacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_realizacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece trabajo_realizado.Sin descripcion para trabajo_realizado 
		/// </summary>
		/// <value>
		/// trabajo_realizado 
		/// </value>
		[Required]
		[DefaultValue(null)]
		public string trabajo_realizado
		{
			get
			{
				return ObtenerValorPropiedad<string>("trabajo_realizado");
			}

			set
			{
				EstablecerValorPropiedad<string>("trabajo_realizado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estatus.Sin descripcion para estatus 
		/// </summary>
		/// <value>
		/// estatus 
		/// </value>
		[Required]
		public Byte estatus
		{
			get
			{
				return ObtenerValorPropiedad<Byte>("estatus");
			}

			set
			{
				EstablecerValorPropiedad<Byte>("estatus", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla mantenimiento_orden.		/// </summary>
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
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<Int16> loColInt16; 
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<Byte> loColByte; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(0);
			Propiedades = new Dictionary<string, Propiedad>(9);


			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para perdiodo";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("perdiodo", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 1;
			loColInt32.Descripcion = "Sin descripcion para area_solicitada";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("area_solicitada", loColInt32); 

			loColInt16 = new PropiedadesColumna<Int16>();
			loColInt16.EsPrimaryKey = false;
			loColInt16.Longitud = 2;
			loColInt16.Precision = 5;
			loColInt16.EsRequeridoBD = true;
			loColInt16.CampoId = 2;
			loColInt16.Descripcion = "Sin descripcion para folio";
			loColInt16.EsIdentity = false;
			loColInt16.Tipo = typeof(Int16);
			AgregarPropiedad<Int16>("folio", loColInt16); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 2;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para tipo_mantenimiento";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_mantenimiento", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para tipo_servicio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_servicio", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 5;
			loColstring.Descripcion = "Sin descripcion para asignado";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("asignado", loColstring); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 6;
			loColDateTime.Descripcion = "Sin descripcion para fecha_realizacion";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_realizacion", loColDateTime); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = -1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "Sin descripcion para trabajo_realizado";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("trabajo_realizado", loColstring); 

			loColByte = new PropiedadesColumna<Byte>();
			loColByte.EsPrimaryKey = false;
			loColByte.Longitud = 1;
			loColByte.Precision = 3;
			loColByte.EsRequeridoBD = true;
			loColByte.CampoId = 8;
			loColByte.Descripcion = "Sin descripcion para estatus";
			loColByte.EsIdentity = false;
			loColByte.Tipo = typeof(Byte);
			AgregarPropiedad<Byte>("estatus", loColByte); 
			}
			#endregion

		}
	}
