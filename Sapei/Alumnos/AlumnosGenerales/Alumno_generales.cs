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
	/// Clase alumnos_generale generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Alumno_generales:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_generale.
		/// </summary>
		public Alumno_generales():base()
		{
			NombreTabla = "alumnos_generales";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase alumnos_generale.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
          public Alumno_generales(Sistema poSistema)
               : base(poSistema)
		{
			NombreTabla = "alumnos_generales";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
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
		/// <summary>
		/// Obtiene o establece lugar_nacimiento.lugar_nacimiento 
		/// </summary>
		/// <value>
		/// lugar_nacimiento 
		/// </value>
		[DefaultValue(null)]
		public Int32? lugar_nacimiento
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("lugar_nacimiento");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("lugar_nacimiento", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_calle.domicilio_calle 
		/// </summary>
		/// <value>
		/// domicilio_calle 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string domicilio_calle
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_calle");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_calle", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_colonia.domicilio_colonia 
		/// </summary>
		/// <value>
		/// domicilio_colonia 
		/// </value>
		[MaxLength (40)]
		[DefaultValue(null)]
		public string domicilio_colonia
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_colonia");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_colonia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ciudad.ciudad 
		/// </summary>
		/// <value>
		/// ciudad 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string ciudad
		{
			get
			{
				return ObtenerValorPropiedad<string>("ciudad");
			}

			set
			{
				EstablecerValorPropiedad<string>("ciudad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece entidad_federativa.entidad_federativa 
		/// </summary>
		/// <value>
		/// entidad_federativa 
		/// </value>
		[DefaultValue(null)]
		public Int32? entidad_federativa
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("entidad_federativa");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("entidad_federativa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece codigo_postal.codigo_postal 
		/// </summary>
		/// <value>
		/// codigo_postal 
		/// </value>
		[DefaultValue(null)]
		public Int32? codigo_postal
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("codigo_postal");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("codigo_postal", value);
			}

		}
		/// <summary>
		/// Obtiene o establece telefono.telefono 
		/// </summary>
		/// <value>
		/// telefono 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string telefono
		{
			get
			{
				return ObtenerValorPropiedad<string>("telefono");
			}

			set
			{
				EstablecerValorPropiedad<string>("telefono", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_padre.nombre_padre 
		/// </summary>
		/// <value>
		/// nombre_padre 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string nombre_padre
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_padre");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_padre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ocupacion_padre.ocupacion_padre 
		/// </summary>
		/// <value>
		/// ocupacion_padre 
		/// </value>
		[MaxLength (50)]
		[DefaultValue(null)]
		public string ocupacion_padre
		{
			get
			{
				return ObtenerValorPropiedad<string>("ocupacion_padre");
			}

			set
			{
				EstablecerValorPropiedad<string>("ocupacion_padre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_calle_padre.domicilio_calle_padre 
		/// </summary>
		/// <value>
		/// domicilio_calle_padre 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string domicilio_calle_padre
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_calle_padre");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_calle_padre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_colonia_padre.domicilio_colonia_padre 
		/// </summary>
		/// <value>
		/// domicilio_colonia_padre 
		/// </value>
		[MaxLength (40)]
		[DefaultValue(null)]
		public string domicilio_colonia_padre
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_colonia_padre");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_colonia_padre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_ciudad_padre.domicilio_ciudad_padre 
		/// </summary>
		/// <value>
		/// domicilio_ciudad_padre 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string domicilio_ciudad_padre
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_ciudad_padre");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_ciudad_padre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_entidad_fed_padre.domicilio_entidad_fed_padre 
		/// </summary>
		/// <value>
		/// domicilio_entidad_fed_padre 
		/// </value>
		[DefaultValue(null)]
		public Int32? domicilio_entidad_fed_padre
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("domicilio_entidad_fed_padre");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("domicilio_entidad_fed_padre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_telefono_padre.domicilio_telefono_padre 
		/// </summary>
		/// <value>
		/// domicilio_telefono_padre 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string domicilio_telefono_padre
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_telefono_padre");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_telefono_padre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_madre.nombre_madre 
		/// </summary>
		/// <value>
		/// nombre_madre 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string nombre_madre
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_madre");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_madre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ocupacion_madre.ocupacion_madre 
		/// </summary>
		/// <value>
		/// ocupacion_madre 
		/// </value>
		[MaxLength (50)]
		[DefaultValue(null)]
		public string ocupacion_madre
		{
			get
			{
				return ObtenerValorPropiedad<string>("ocupacion_madre");
			}

			set
			{
				EstablecerValorPropiedad<string>("ocupacion_madre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_calle_madre.domicilio_calle_madre 
		/// </summary>
		/// <value>
		/// domicilio_calle_madre 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string domicilio_calle_madre
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_calle_madre");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_calle_madre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_colonia_madre.domicilio_colonia_madre 
		/// </summary>
		/// <value>
		/// domicilio_colonia_madre 
		/// </value>
		[MaxLength (40)]
		[DefaultValue(null)]
		public string domicilio_colonia_madre
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_colonia_madre");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_colonia_madre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_ciudad_madre.domicilio_ciudad_madre 
		/// </summary>
		/// <value>
		/// domicilio_ciudad_madre 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string domicilio_ciudad_madre
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_ciudad_madre");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_ciudad_madre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_entidad_fed_madre.domicilio_entidad_fed_madre 
		/// </summary>
		/// <value>
		/// domicilio_entidad_fed_madre 
		/// </value>
		[DefaultValue(null)]
		public Int32? domicilio_entidad_fed_madre
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("domicilio_entidad_fed_madre");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("domicilio_entidad_fed_madre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_telefono_madre.domicilio_telefono_madre 
		/// </summary>
		/// <value>
		/// domicilio_telefono_madre 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string domicilio_telefono_madre
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_telefono_madre");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_telefono_madre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_empresa.nombre_empresa 
		/// </summary>
		/// <value>
		/// nombre_empresa 
		/// </value>
		[MaxLength (100)]
		[DefaultValue(null)]
		public string nombre_empresa
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_empresa");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_empresa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece cargo_desempenado.cargo_desempenado 
		/// </summary>
		/// <value>
		/// cargo_desempenado 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string cargo_desempenado
		{
			get
			{
				return ObtenerValorPropiedad<string>("cargo_desempenado");
			}

			set
			{
				EstablecerValorPropiedad<string>("cargo_desempenado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ingreso_mensual.ingreso_mensual 
		/// </summary>
		/// <value>
		/// ingreso_mensual 
		/// </value>
		[DefaultValue(null)]
		public Double? ingreso_mensual
		{
			get
			{
				return ObtenerValorPropiedad<Double?>("ingreso_mensual");
			}

			set
			{
				EstablecerValorPropiedad<Double?>("ingreso_mensual", value);
			}

		}
		/// <summary>
		/// Obtiene o establece turno.turno 
		/// </summary>
		/// <value>
		/// turno 
		/// </value>
		[DefaultValue(null)]
		public Int32? turno
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("turno");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("turno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece antiguedad.antiguedad 
		/// </summary>
		/// <value>
		/// antiguedad 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string antiguedad
		{
			get
			{
				return ObtenerValorPropiedad<string>("antiguedad");
			}

			set
			{
				EstablecerValorPropiedad<string>("antiguedad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_jefe.nombre_jefe 
		/// </summary>
		/// <value>
		/// nombre_jefe 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string nombre_jefe
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_jefe");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_jefe", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_calle_empresa.domicilio_calle_empresa 
		/// </summary>
		/// <value>
		/// domicilio_calle_empresa 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string domicilio_calle_empresa
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_calle_empresa");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_calle_empresa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_colonia_empresa.domicilio_colonia_empresa 
		/// </summary>
		/// <value>
		/// domicilio_colonia_empresa 
		/// </value>
		[MaxLength (40)]
		[DefaultValue(null)]
		public string domicilio_colonia_empresa
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_colonia_empresa");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_colonia_empresa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_ciudad_empresa.domicilio_ciudad_empresa 
		/// </summary>
		/// <value>
		/// domicilio_ciudad_empresa 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string domicilio_ciudad_empresa
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_ciudad_empresa");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_ciudad_empresa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_entidad_fed_empresa.domicilio_entidad_fed_empresa 
		/// </summary>
		/// <value>
		/// domicilio_entidad_fed_empresa 
		/// </value>
		[DefaultValue(null)]
		public Int32? domicilio_entidad_fed_empresa
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("domicilio_entidad_fed_empresa");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("domicilio_entidad_fed_empresa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_telefono_empresa.domicilio_telefono_empresa 
		/// </summary>
		/// <value>
		/// domicilio_telefono_empresa 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string domicilio_telefono_empresa
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_telefono_empresa");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_telefono_empresa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_esposa.nombre_esposa 
		/// </summary>
		/// <value>
		/// nombre_esposa 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string nombre_esposa
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_esposa");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_esposa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ocupacion_esposa.ocupacion_esposa 
		/// </summary>
		/// <value>
		/// ocupacion_esposa 
		/// </value>
		[MaxLength (50)]
		[DefaultValue(null)]
		public string ocupacion_esposa
		{
			get
			{
				return ObtenerValorPropiedad<string>("ocupacion_esposa");
			}

			set
			{
				EstablecerValorPropiedad<string>("ocupacion_esposa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece no_dependientes.no_dependientes 
		/// </summary>
		/// <value>
		/// no_dependientes 
		/// </value>
		[DefaultValue(null)]
		public Int32? no_dependientes
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("no_dependientes");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("no_dependientes", value);
			}

		}
		/// <summary>
		/// Obtiene o establece comunidad_indigena.comunidad_indigena 
		/// </summary>
		/// <value>
		/// comunidad_indigena 
		/// </value>
		[MaxLength (100)]
		[DefaultValue(null)]
		public string comunidad_indigena
		{
			get
			{
				return ObtenerValorPropiedad<string>("comunidad_indigena");
			}

			set
			{
				EstablecerValorPropiedad<string>("comunidad_indigena", value);
			}

		}
		/// <summary>
		/// Obtiene o establece lengua_indigena.lengua_indigena 
		/// </summary>
		/// <value>
		/// lengua_indigena 
		/// </value>
		[MaxLength (100)]
		[DefaultValue(null)]
		public string lengua_indigena
		{
			get
			{
				return ObtenerValorPropiedad<string>("lengua_indigena");
			}

			set
			{
				EstablecerValorPropiedad<string>("lengua_indigena", value);
			}

		}
		/// <summary>
		/// Obtiene o establece municipio_nac.municipio_nac 
		/// </summary>
		/// <value>
		/// municipio_nac 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string municipio_nac
		{
			get
			{
				return ObtenerValorPropiedad<string>("municipio_nac");
			}

			set
			{
				EstablecerValorPropiedad<string>("municipio_nac", value);
			}

		}
		/// <summary>
		/// Obtiene o establece municipio_dom.municipio_dom 
		/// </summary>
		/// <value>
		/// municipio_dom 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string municipio_dom
		{
			get
			{
				return ObtenerValorPropiedad<string>("municipio_dom");
			}

			set
			{
				EstablecerValorPropiedad<string>("municipio_dom", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_calle_tutor.domicilio_calle_tutor 
		/// </summary>
		/// <value>
		/// domicilio_calle_tutor 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string domicilio_calle_tutor
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_calle_tutor");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_calle_tutor", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_tutor.nombre_tutor 
		/// </summary>
		/// <value>
		/// nombre_tutor 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string nombre_tutor
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_tutor");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_tutor", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_ciudad_tutor.domicilio_ciudad_tutor 
		/// </summary>
		/// <value>
		/// domicilio_ciudad_tutor 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string domicilio_ciudad_tutor
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_ciudad_tutor");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_ciudad_tutor", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_colonia_tutor.domicilio_colonia_tutor 
		/// </summary>
		/// <value>
		/// domicilio_colonia_tutor 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string domicilio_colonia_tutor
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_colonia_tutor");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_colonia_tutor", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_telefono_tutor.domicilio_telefono_tutor 
		/// </summary>
		/// <value>
		/// domicilio_telefono_tutor 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string domicilio_telefono_tutor
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_telefono_tutor");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_telefono_tutor", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_entidad_federativa_t.domicilio_entidad_federativa_t 
		/// </summary>
		/// <value>
		/// domicilio_entidad_federativa_t 
		/// </value>
		[DefaultValue(null)]
		public Int32? domicilio_entidad_federativa_t
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("domicilio_entidad_federativa_t");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("domicilio_entidad_federativa_t", value);
			}

		}
		/// <summary>
		/// Obtiene o establece clave_preparatoria.clave_preparatoria 
		/// </summary>
		/// <value>
		/// clave_preparatoria 
		/// </value>
		[MaxLength (15)]
		[DefaultValue(null)]
		public string clave_preparatoria
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave_preparatoria");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave_preparatoria", value);
			}

		}
		/// <summary>
		/// Obtiene o establece municipio.municipio 
		/// </summary>
		/// <value>
		/// municipio 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string municipio
		{
			get
			{
				return ObtenerValorPropiedad<string>("municipio");
			}

			set
			{
				EstablecerValorPropiedad<string>("municipio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece entidad_federativa_prepa.entidad_federativa_prepa 
		/// </summary>
		/// <value>
		/// entidad_federativa_prepa 
		/// </value>
		[DefaultValue(null)]
		public Int32? entidad_federativa_prepa
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("entidad_federativa_prepa");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("entidad_federativa_prepa", value);
			}

		}
		/// <summary>
		/// Obtiene o establece celular.celular 
		/// </summary>
		/// <value>
		/// celular 
		/// </value>
		[MaxLength (10)]
		[DefaultValue(null)]
		public string celular
		{
			get
			{
				return ObtenerValorPropiedad<string>("celular");
			}

			set
			{
				EstablecerValorPropiedad<string>("celular", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nss.nss 
		/// </summary>
		/// <value>
		/// nss 
		/// </value>
		[MaxLength (11)]
		[DefaultValue(null)]
		public string nss
		{
			get
			{
				return ObtenerValorPropiedad<string>("nss");
			}

			set
			{
				EstablecerValorPropiedad<string>("nss", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla alumnos_generale.		/// </summary>
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
			 PropiedadesColumna<Int32?> loColInt32N; 
			 PropiedadesColumna<Double?> loColDoubleN; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(51);

			AgregaCampoLlave("no_de_control",null);

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

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 1;
			loColInt32N.Descripcion = "lugar_nacimiento";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("lugar_nacimiento", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "domicilio_calle";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_calle", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 40;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "domicilio_colonia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_colonia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "ciudad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("ciudad", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 5;
			loColInt32N.Descripcion = "entidad_federativa";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("entidad_federativa", loColInt32N); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 6;
			loColInt32N.Descripcion = "codigo_postal";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("codigo_postal", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "telefono";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("telefono", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "nombre_padre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_padre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "ocupacion_padre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("ocupacion_padre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "domicilio_calle_padre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_calle_padre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 40;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "domicilio_colonia_padre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_colonia_padre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 12;
			loColstring.Descripcion = "domicilio_ciudad_padre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_ciudad_padre", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 13;
			loColInt32N.Descripcion = "domicilio_entidad_fed_padre";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("domicilio_entidad_fed_padre", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 14;
			loColstring.Descripcion = "domicilio_telefono_padre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_telefono_padre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 15;
			loColstring.Descripcion = "nombre_madre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_madre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 16;
			loColstring.Descripcion = "ocupacion_madre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("ocupacion_madre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 17;
			loColstring.Descripcion = "domicilio_calle_madre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_calle_madre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 40;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 18;
			loColstring.Descripcion = "domicilio_colonia_madre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_colonia_madre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 19;
			loColstring.Descripcion = "domicilio_ciudad_madre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_ciudad_madre", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 20;
			loColInt32N.Descripcion = "domicilio_entidad_fed_madre";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("domicilio_entidad_fed_madre", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 21;
			loColstring.Descripcion = "domicilio_telefono_madre";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_telefono_madre", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 22;
			loColstring.Descripcion = "nombre_empresa";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_empresa", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 23;
			loColstring.Descripcion = "cargo_desempenado";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("cargo_desempenado", loColstring); 

			loColDoubleN = new PropiedadesColumna<Double?>();
			loColDoubleN.Valor = null;
			loColDoubleN.EsPrimaryKey = false;
			loColDoubleN.Longitud = 8;
			loColDoubleN.Precision = 19;
			loColDoubleN.EsRequeridoBD = false;
			loColDoubleN.CampoId = 24;
			loColDoubleN.Descripcion = "ingreso_mensual";
			loColDoubleN.EsIdentity = false;
			loColDoubleN.Tipo = typeof(Double?);
			AgregarPropiedad<Double?>("ingreso_mensual", loColDoubleN); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 25;
			loColInt32N.Descripcion = "turno";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("turno", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 26;
			loColstring.Descripcion = "antiguedad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("antiguedad", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 27;
			loColstring.Descripcion = "nombre_jefe";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_jefe", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 28;
			loColstring.Descripcion = "domicilio_calle_empresa";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_calle_empresa", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 40;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 29;
			loColstring.Descripcion = "domicilio_colonia_empresa";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_colonia_empresa", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 30;
			loColstring.Descripcion = "domicilio_ciudad_empresa";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_ciudad_empresa", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 31;
			loColInt32N.Descripcion = "domicilio_entidad_fed_empresa";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("domicilio_entidad_fed_empresa", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 32;
			loColstring.Descripcion = "domicilio_telefono_empresa";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_telefono_empresa", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 33;
			loColstring.Descripcion = "nombre_esposa";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_esposa", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 34;
			loColstring.Descripcion = "ocupacion_esposa";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("ocupacion_esposa", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 35;
			loColInt32N.Descripcion = "no_dependientes";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("no_dependientes", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 36;
			loColstring.Descripcion = "comunidad_indigena";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("comunidad_indigena", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 37;
			loColstring.Descripcion = "lengua_indigena";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("lengua_indigena", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 38;
			loColstring.Descripcion = "municipio_nac";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("municipio_nac", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 39;
			loColstring.Descripcion = "municipio_dom";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("municipio_dom", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 40;
			loColstring.Descripcion = "domicilio_calle_tutor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_calle_tutor", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 41;
			loColstring.Descripcion = "nombre_tutor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_tutor", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 42;
			loColstring.Descripcion = "domicilio_ciudad_tutor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_ciudad_tutor", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 43;
			loColstring.Descripcion = "domicilio_colonia_tutor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_colonia_tutor", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 44;
			loColstring.Descripcion = "domicilio_telefono_tutor";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_telefono_tutor", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 45;
			loColInt32N.Descripcion = "domicilio_entidad_federativa_t";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("domicilio_entidad_federativa_t", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 15;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 46;
			loColstring.Descripcion = "clave_preparatoria";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_preparatoria", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 47;
			loColstring.Descripcion = "municipio";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("municipio", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 48;
			loColInt32N.Descripcion = "entidad_federativa_prepa";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("entidad_federativa_prepa", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 49;
			loColstring.Descripcion = "celular";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("celular", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 11;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 50;
			loColstring.Descripcion = "nss";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nss", loColstring); 
			}
			#endregion

		}
	}
