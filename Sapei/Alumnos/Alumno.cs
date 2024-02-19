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
	/// Clase alumno generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Alumno:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase alumno.
		/// </summary>
		public Alumno():base()
		{
			NombreTabla = "alumnos";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase alumno.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Alumno(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "alumnos";
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
		/// Obtiene o establece carrera.carrera 
		/// </summary>
		/// <value>
		/// carrera 
		/// </value>
		[MaxLength (3)]
		[DefaultValue(null)]
		public string carrera
		{
			get
			{
				return ObtenerValorPropiedad<string>("carrera");
			}

			set
			{
				EstablecerValorPropiedad<string>("carrera", value);
			}

		}
		/// <summary>
		/// Obtiene o establece reticula.reticula 
		/// </summary>
		/// <value>
		/// reticula 
		/// </value>
		[DefaultValue(null)]
		public Int32? reticula
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("reticula");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("reticula", value);
			}

		}
		/// <summary>
		/// Obtiene o establece especialidad.especialidad 
		/// </summary>
		/// <value>
		/// especialidad 
		/// </value>
		[MaxLength (5)]
		[DefaultValue(null)]
		public string especialidad
		{
			get
			{
				return ObtenerValorPropiedad<string>("especialidad");
			}

			set
			{
				EstablecerValorPropiedad<string>("especialidad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nivel_escolar.nivel_escolar 
		/// </summary>
		/// <value>
		/// nivel_escolar 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string nivel_escolar
		{
			get
			{
				return ObtenerValorPropiedad<string>("nivel_escolar");
			}

			set
			{
				EstablecerValorPropiedad<string>("nivel_escolar", value);
			}

		}
		/// <summary>
		/// Obtiene o establece semestre.semestre 
		/// </summary>
		/// <value>
		/// semestre 
		/// </value>
		[DefaultValue(null)]
		public Int32 semestre
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("semestre");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("semestre", value);
			}

		}
		/// <summary>
		/// Obtiene o establece clave_interna.clave_interna 
		/// </summary>
		/// <value>
		/// clave_interna 
		/// </value>
		[MaxLength (10)]
		[DefaultValue(null)]
		public string clave_interna
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave_interna");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave_interna", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estatus_alumno.estatus_alumno 
		/// </summary>
		/// <value>
		/// estatus_alumno 
		/// </value>
		[MaxLength (3)]
		[DefaultValue(null)]
		public string estatus_alumno
		{
			get
			{
				return ObtenerValorPropiedad<string>("estatus_alumno");
			}

			set
			{
				EstablecerValorPropiedad<string>("estatus_alumno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece plan_de_estudios.plan_de_estudios 
		/// </summary>
		/// <value>
		/// plan_de_estudios 
		/// </value>
		[Required]
		[MaxLength (1)]
		[DefaultValue(null)]
		public string plan_de_estudios
		{
			get
			{
				return ObtenerValorPropiedad<string>("plan_de_estudios");
			}

			set
			{
				EstablecerValorPropiedad<string>("plan_de_estudios", value);
			}

		}
		/// <summary>
		/// Obtiene o establece apellido_paterno.apellido_paterno 
		/// </summary>
		/// <value>
		/// apellido_paterno 
		/// </value>
		[MaxLength (45)]
		[DefaultValue(null)]
		public string apellido_paterno
		{
			get
			{
				return ObtenerValorPropiedad<string>("apellido_paterno");
			}

			set
			{
				EstablecerValorPropiedad<string>("apellido_paterno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece apellido_materno.apellido_materno 
		/// </summary>
		/// <value>
		/// apellido_materno 
		/// </value>
		[MaxLength (45)]
		[DefaultValue(null)]
		public string apellido_materno
		{
			get
			{
				return ObtenerValorPropiedad<string>("apellido_materno");
			}

			set
			{
				EstablecerValorPropiedad<string>("apellido_materno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nombre_alumno.nombre_alumno 
		/// </summary>
		/// <value>
		/// nombre_alumno 
		/// </value>
		[Required]
		[MaxLength (35)]
		[DefaultValue(null)]
		public string nombre_alumno
		{
			get
			{
				return ObtenerValorPropiedad<string>("nombre_alumno");
			}

			set
			{
				EstablecerValorPropiedad<string>("nombre_alumno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece curp_alumno.curp_alumno 
		/// </summary>
		/// <value>
		/// curp_alumno 
		/// </value>
		[MaxLength (18)]
		[DefaultValue(null)]
		public string curp_alumno
		{
			get
			{
				return ObtenerValorPropiedad<string>("curp_alumno");
			}

			set
			{
				EstablecerValorPropiedad<string>("curp_alumno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_nacimiento.fecha_nacimiento 
		/// </summary>
		/// <value>
		/// fecha_nacimiento 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_nacimiento
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_nacimiento");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_nacimiento", value);
			}

		}
		/// <summary>
		/// Obtiene o establece sexo.sexo 
		/// </summary>
		/// <value>
		/// sexo 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string sexo
		{
			get
			{
				return ObtenerValorPropiedad<string>("sexo");
			}

			set
			{
				EstablecerValorPropiedad<string>("sexo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estado_civil.estado_civil 
		/// </summary>
		/// <value>
		/// estado_civil 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string estado_civil
		{
			get
			{
				return ObtenerValorPropiedad<string>("estado_civil");
			}

			set
			{
				EstablecerValorPropiedad<string>("estado_civil", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_ingreso.tipo_ingreso 
		/// </summary>
		/// <value>
		/// tipo_ingreso 
		/// </value>
		[Required]
		public Double tipo_ingreso
		{
			get
			{
				return ObtenerValorPropiedad<Double>("tipo_ingreso");
			}

			set
			{
				EstablecerValorPropiedad<Double>("tipo_ingreso", value);
			}

		}
		/// <summary>
		/// Obtiene o establece periodo_ingreso_it.periodo_ingreso_it 
		/// </summary>
		/// <value>
		/// periodo_ingreso_it 
		/// </value>
		[Required]
		[MaxLength (5)]
		[DefaultValue(null)]
		public string periodo_ingreso_it
		{
			get
			{
				return ObtenerValorPropiedad<string>("periodo_ingreso_it");
			}

			set
			{
				EstablecerValorPropiedad<string>("periodo_ingreso_it", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ultimo_periodo_inscrito.ultimo_periodo_inscrito 
		/// </summary>
		/// <value>
		/// ultimo_periodo_inscrito 
		/// </value>
		[MaxLength (5)]
		[DefaultValue(null)]
		public string ultimo_periodo_inscrito
		{
			get
			{
				return ObtenerValorPropiedad<string>("ultimo_periodo_inscrito");
			}

			set
			{
				EstablecerValorPropiedad<string>("ultimo_periodo_inscrito", value);
			}

		}
		/// <summary>
		/// Obtiene o establece promedio_periodo_anterior.promedio_periodo_anterior 
		/// </summary>
		/// <value>
		/// promedio_periodo_anterior 
		/// </value>
		[DefaultValue(null)]
		public Double? promedio_periodo_anterior
		{
			get
			{
				return ObtenerValorPropiedad<Double?>("promedio_periodo_anterior");
			}

			set
			{
				EstablecerValorPropiedad<Double?>("promedio_periodo_anterior", value);
			}

		}
		/// <summary>
		/// Obtiene o establece promedio_aritmetico_acumulado.promedio_aritmetico_acumulado 
		/// </summary>
		/// <value>
		/// promedio_aritmetico_acumulado 
		/// </value>
		[DefaultValue(null)]
		public Double? promedio_aritmetico_acumulado
		{
			get
			{
				return ObtenerValorPropiedad<Double?>("promedio_aritmetico_acumulado");
			}

			set
			{
				EstablecerValorPropiedad<Double?>("promedio_aritmetico_acumulado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece creditos_aprobados.creditos_aprobados 
		/// </summary>
		/// <value>
		/// creditos_aprobados 
		/// </value>
		[DefaultValue(null)]
		public Int32? creditos_aprobados
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("creditos_aprobados");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("creditos_aprobados", value);
			}

		}
		/// <summary>
		/// Obtiene o establece creditos_cursados.creditos_cursados 
		/// </summary>
		/// <value>
		/// creditos_cursados 
		/// </value>
		[DefaultValue(null)]
		public Int32? creditos_cursados
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("creditos_cursados");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("creditos_cursados", value);
			}

		}
		/// <summary>
		/// Obtiene o establece promedio_final_alcanzado.promedio_final_alcanzado 
		/// </summary>
		/// <value>
		/// promedio_final_alcanzado 
		/// </value>
		[DefaultValue(null)]
		public Double? promedio_final_alcanzado
		{
			get
			{
				return ObtenerValorPropiedad<Double?>("promedio_final_alcanzado");
			}

			set
			{
				EstablecerValorPropiedad<Double?>("promedio_final_alcanzado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_servicio_medico.tipo_servicio_medico 
		/// </summary>
		/// <value>
		/// tipo_servicio_medico 
		/// </value>
		[MaxLength (1)]
		[DefaultValue(null)]
		public string tipo_servicio_medico
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_servicio_medico");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_servicio_medico", value);
			}

		}
		/// <summary>
		/// Obtiene o establece clave_servicio_medico.clave_servicio_medico 
		/// </summary>
		/// <value>
		/// clave_servicio_medico 
		/// </value>
		[MaxLength (20)]
		[DefaultValue(null)]
		public string clave_servicio_medico
		{
			get
			{
				return ObtenerValorPropiedad<string>("clave_servicio_medico");
			}

			set
			{
				EstablecerValorPropiedad<string>("clave_servicio_medico", value);
			}

		}
		/// <summary>
		/// Obtiene o establece escuela_procedencia.escuela_procedencia 
		/// </summary>
		/// <value>
		/// escuela_procedencia 
		/// </value>
		[MaxLength (200)]
		[DefaultValue(null)]
		public string escuela_procedencia
		{
			get
			{
				return ObtenerValorPropiedad<string>("escuela_procedencia");
			}

			set
			{
				EstablecerValorPropiedad<string>("escuela_procedencia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_escuela.tipo_escuela 
		/// </summary>
		/// <value>
		/// tipo_escuela 
		/// </value>
		[DefaultValue(null)]
		public Int32? tipo_escuela
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("tipo_escuela");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("tipo_escuela", value);
			}

		}
		/// <summary>
		/// Obtiene o establece domicilio_escuela.domicilio_escuela 
		/// </summary>
		/// <value>
		/// domicilio_escuela 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string domicilio_escuela
		{
			get
			{
				return ObtenerValorPropiedad<string>("domicilio_escuela");
			}

			set
			{
				EstablecerValorPropiedad<string>("domicilio_escuela", value);
			}

		}
		/// <summary>
		/// Obtiene o establece entidad_procedencia.entidad_procedencia 
		/// </summary>
		/// <value>
		/// entidad_procedencia 
		/// </value>
		[DefaultValue(null)]
		public Int32? entidad_procedencia
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("entidad_procedencia");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("entidad_procedencia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece ciudad_procedencia.ciudad_procedencia 
		/// </summary>
		/// <value>
		/// ciudad_procedencia 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string ciudad_procedencia
		{
			get
			{
				return ObtenerValorPropiedad<string>("ciudad_procedencia");
			}

			set
			{
				EstablecerValorPropiedad<string>("ciudad_procedencia", value);
			}

		}
		/// <summary>
		/// Obtiene o establece correo_electronico.correo_electronico 
		/// </summary>
		/// <value>
		/// correo_electronico 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string correo_electronico
		{
			get
			{
				return ObtenerValorPropiedad<string>("correo_electronico");
			}

			set
			{
				EstablecerValorPropiedad<string>("correo_electronico", value);
			}

		}
		/// <summary>
		/// Obtiene o establece foto.foto 
		/// </summary>
		/// <value>
		/// foto 
		/// </value>
		[DefaultValue(null)]
		public Byte[] foto
		{
			get
			{
				return ObtenerValorPropiedad<Byte[]>("foto");
			}

			set
			{
				EstablecerValorPropiedad<Byte[]>("foto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece firma.firma 
		/// </summary>
		/// <value>
		/// firma 
		/// </value>
		[DefaultValue(null)]
		public Byte[] firma
		{
			get
			{
				return ObtenerValorPropiedad<Byte[]>("firma");
			}

			set
			{
				EstablecerValorPropiedad<Byte[]>("firma", value);
			}

		}
		/// <summary>
		/// Obtiene o establece periodos_revalidacion.periodos_revalidacion 
		/// </summary>
		/// <value>
		/// periodos_revalidacion 
		/// </value>
		[DefaultValue(null)]
		public Int32? periodos_revalidacion
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("periodos_revalidacion");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("periodos_revalidacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece indice_reprobacion_acumulado.indice_reprobacion_acumulado 
		/// </summary>
		/// <value>
		/// indice_reprobacion_acumulado 
		/// </value>
		[DefaultValue(null)]
		public Double? indice_reprobacion_acumulado
		{
			get
			{
				return ObtenerValorPropiedad<Double?>("indice_reprobacion_acumulado");
			}

			set
			{
				EstablecerValorPropiedad<Double?>("indice_reprobacion_acumulado", value);
			}

		}
		/// <summary>
		/// Obtiene o establece becado_por.becado_por 
		/// </summary>
		/// <value>
		/// becado_por 
		/// </value>
		[MaxLength (100)]
		[DefaultValue(null)]
		public string becado_por
		{
			get
			{
				return ObtenerValorPropiedad<string>("becado_por");
			}

			set
			{
				EstablecerValorPropiedad<string>("becado_por", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nip.nip 
		/// </summary>
		/// <value>
		/// nip 
		/// </value>
		[DefaultValue(null)]
		public Int32? nip
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("nip");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("nip", value);
			}

		}
		/// <summary>
		/// Obtiene o establece tipo_alumno.tipo_alumno 
		/// </summary>
		/// <value>
		/// tipo_alumno 
		/// </value>
		[MaxLength (2)]
		[DefaultValue(null)]
		public string tipo_alumno
		{
			get
			{
				return ObtenerValorPropiedad<string>("tipo_alumno");
			}

			set
			{
				EstablecerValorPropiedad<string>("tipo_alumno", value);
			}

		}
		/// <summary>
		/// Obtiene o establece nacionalidad.nacionalidad 
		/// </summary>
		/// <value>
		/// nacionalidad 
		/// </value>
		[MaxLength (3)]
		[DefaultValue(null)]
		public string nacionalidad
		{
			get
			{
				return ObtenerValorPropiedad<string>("nacionalidad");
			}

			set
			{
				EstablecerValorPropiedad<string>("nacionalidad", value);
			}

		}
		/// <summary>
		/// Obtiene o establece usuario.usuario 
		/// </summary>
		/// <value>
		/// usuario 
		/// </value>
		[MaxLength (30)]
		[DefaultValue(null)]
		public string usuario
		{
			get
			{
				return ObtenerValorPropiedad<string>("usuario");
			}

			set
			{
				EstablecerValorPropiedad<string>("usuario", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_actualizacion.fecha_actualizacion 
		/// </summary>
		/// <value>
		/// fecha_actualizacion 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_actualizacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_actualizacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_actualizacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece folio.folio 
		/// </summary>
		/// <value>
		/// folio 
		/// </value>
		[DefaultValue(null)]
		public Int32? folio
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("folio");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("folio", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_titulacion.fecha_titulacion 
		/// </summary>
		/// <value>
		/// fecha_titulacion 
		/// </value>
		[DefaultValue(null)]
		public DateTime? fecha_titulacion
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("fecha_titulacion");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("fecha_titulacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece opcion_titulacion.opcion_titulacion 
		/// </summary>
		/// <value>
		/// opcion_titulacion 
		/// </value>
		[MaxLength (8)]
		[DefaultValue(null)]
		public string opcion_titulacion
		{
			get
			{
				return ObtenerValorPropiedad<string>("opcion_titulacion");
			}

			set
			{
				EstablecerValorPropiedad<string>("opcion_titulacion", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estatus_alumno_fecha.estatus_alumno_fecha 
		/// </summary>
		/// <value>
		/// estatus_alumno_fecha 
		/// </value>
		[DefaultValue(null)]
		public DateTime? estatus_alumno_fecha
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("estatus_alumno_fecha");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("estatus_alumno_fecha", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estatus_alumno_usuario.estatus_alumno_usuario 
		/// </summary>
		/// <value>
		/// estatus_alumno_usuario 
		/// </value>
		[MaxLength (100)]
		[DefaultValue(null)]
		public string estatus_alumno_usuario
		{
			get
			{
				return ObtenerValorPropiedad<string>("estatus_alumno_usuario");
			}

			set
			{
				EstablecerValorPropiedad<string>("estatus_alumno_usuario", value);
			}

		}
		/// <summary>
		/// Obtiene o establece estatus_alumno_anterior.estatus_alumno_anterior 
		/// </summary>
		/// <value>
		/// estatus_alumno_anterior 
		/// </value>
		[MaxLength (3)]
		[DefaultValue(null)]
		public string estatus_alumno_anterior
		{
			get
			{
				return ObtenerValorPropiedad<string>("estatus_alumno_anterior");
			}

			set
			{
				EstablecerValorPropiedad<string>("estatus_alumno_anterior", value);
			}

		}
		/// <summary>
		/// Obtiene o establece periodo_titulacion.periodo_titulacion 
		/// </summary>
		/// <value>
		/// periodo_titulacion 
		/// </value>
		[MaxLength (5)]
		[DefaultValue(null)]
		public string periodo_titulacion
		{
			get
			{
				return ObtenerValorPropiedad<string>("periodo_titulacion");
			}

			set
			{
				EstablecerValorPropiedad<string>("periodo_titulacion", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla alumno.		/// </summary>
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
                PropiedadesColumna<Int32> loColInt32; 
                PropiedadesColumna<DateTime?> loColDateTimeN; 
			 PropiedadesColumna<Double> loColDouble; 
			 PropiedadesColumna<Double?> loColDoubleN; 
			 PropiedadesColumna<Byte[]> loColByteA; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(49);

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

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "carrera";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("carrera", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 2;
			loColInt32N.Descripcion = "reticula";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("reticula", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "especialidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("especialidad", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "nivel_escolar";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nivel_escolar", loColstring); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.Valor = 1;
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = false;
			loColInt32.CampoId = 5;
			loColInt32.Descripcion = "semestre";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("semestre", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 10;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 6;
			loColstring.Descripcion = "clave_interna";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_interna", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 7;
			loColstring.Descripcion = "estatus_alumno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estatus_alumno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 8;
			loColstring.Descripcion = "plan_de_estudios";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("plan_de_estudios", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 45;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 9;
			loColstring.Descripcion = "apellido_paterno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("apellido_paterno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 45;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 10;
			loColstring.Descripcion = "apellido_materno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("apellido_materno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 35;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 11;
			loColstring.Descripcion = "nombre_alumno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nombre_alumno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 18;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 12;
			loColstring.Descripcion = "curp_alumno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("curp_alumno", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 13;
			loColDateTimeN.Descripcion = "fecha_nacimiento";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_nacimiento", loColDateTimeN); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 14;
			loColstring.Descripcion = "sexo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("sexo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 15;
			loColstring.Descripcion = "estado_civil";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estado_civil", loColstring); 

			loColDouble = new PropiedadesColumna<Double>();
			loColDouble.EsPrimaryKey = false;
			loColDouble.Longitud = 5;
			loColDouble.Precision = 1;
			loColDouble.EsRequeridoBD = true;
			loColDouble.CampoId = 16;
			loColDouble.Descripcion = "tipo_ingreso";
			loColDouble.EsIdentity = false;
			loColDouble.Tipo = typeof(Double);
			AgregarPropiedad<Double>("tipo_ingreso", loColDouble); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 17;
			loColstring.Descripcion = "periodo_ingreso_it";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo_ingreso_it", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 18;
			loColstring.Descripcion = "ultimo_periodo_inscrito";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("ultimo_periodo_inscrito", loColstring); 

			loColDoubleN = new PropiedadesColumna<Double?>();
			loColDoubleN.Valor = null;
			loColDoubleN.EsPrimaryKey = false;
			loColDoubleN.Longitud = 5;
			loColDoubleN.Precision = 5;
			loColDoubleN.EsRequeridoBD = false;
			loColDoubleN.CampoId = 19;
			loColDoubleN.Descripcion = "promedio_periodo_anterior";
			loColDoubleN.EsIdentity = false;
			loColDoubleN.Tipo = typeof(Double?);
			AgregarPropiedad<Double?>("promedio_periodo_anterior", loColDoubleN); 

			loColDoubleN = new PropiedadesColumna<Double?>();
			loColDoubleN.Valor = null;
			loColDoubleN.EsPrimaryKey = false;
			loColDoubleN.Longitud = 5;
			loColDoubleN.Precision = 5;
			loColDoubleN.EsRequeridoBD = false;
			loColDoubleN.CampoId = 20;
			loColDoubleN.Descripcion = "promedio_aritmetico_acumulado";
			loColDoubleN.EsIdentity = false;
			loColDoubleN.Tipo = typeof(Double?);
			AgregarPropiedad<Double?>("promedio_aritmetico_acumulado", loColDoubleN); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 21;
			loColInt32N.Descripcion = "creditos_aprobados";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("creditos_aprobados", loColInt32N); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 22;
			loColInt32N.Descripcion = "creditos_cursados";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("creditos_cursados", loColInt32N); 

			loColDoubleN = new PropiedadesColumna<Double?>();
			loColDoubleN.Valor = null;
			loColDoubleN.EsPrimaryKey = false;
			loColDoubleN.Longitud = 5;
			loColDoubleN.Precision = 5;
			loColDoubleN.EsRequeridoBD = false;
			loColDoubleN.CampoId = 23;
			loColDoubleN.Descripcion = "promedio_final_alcanzado";
			loColDoubleN.EsIdentity = false;
			loColDoubleN.Tipo = typeof(Double?);
			AgregarPropiedad<Double?>("promedio_final_alcanzado", loColDoubleN); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 1;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 24;
			loColstring.Descripcion = "tipo_servicio_medico";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_servicio_medico", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 20;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 25;
			loColstring.Descripcion = "clave_servicio_medico";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("clave_servicio_medico", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 200;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 26;
			loColstring.Descripcion = "escuela_procedencia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("escuela_procedencia", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 27;
			loColInt32N.Descripcion = "tipo_escuela";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("tipo_escuela", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 28;
			loColstring.Descripcion = "domicilio_escuela";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("domicilio_escuela", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 29;
			loColInt32N.Descripcion = "entidad_procedencia";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("entidad_procedencia", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 30;
			loColstring.Descripcion = "ciudad_procedencia";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("ciudad_procedencia", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 31;
			loColstring.Descripcion = "correo_electronico";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("correo_electronico", loColstring); 

			loColByteA = new PropiedadesColumna<Byte[]>();
			loColByteA.Valor = null;
			loColByteA.EsPrimaryKey = false;
			loColByteA.Longitud = 16;
			loColByteA.Precision = 0;
			loColByteA.EsRequeridoBD = false;
			loColByteA.CampoId = 32;
			loColByteA.Descripcion = "foto";
			loColByteA.EsIdentity = false;
			loColByteA.Tipo = typeof(Byte[]);
			AgregarPropiedad<Byte[]>("foto", loColByteA); 

			loColByteA = new PropiedadesColumna<Byte[]>();
			loColByteA.Valor = null;
			loColByteA.EsPrimaryKey = false;
			loColByteA.Longitud = 16;
			loColByteA.Precision = 0;
			loColByteA.EsRequeridoBD = false;
			loColByteA.CampoId = 33;
			loColByteA.Descripcion = "firma";
			loColByteA.EsIdentity = false;
			loColByteA.Tipo = typeof(Byte[]);
			AgregarPropiedad<Byte[]>("firma", loColByteA); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 34;
			loColInt32N.Descripcion = "periodos_revalidacion";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("periodos_revalidacion", loColInt32N); 

			loColDoubleN = new PropiedadesColumna<Double?>();
			loColDoubleN.Valor = null;
			loColDoubleN.EsPrimaryKey = false;
			loColDoubleN.Longitud = 5;
			loColDoubleN.Precision = 8;
			loColDoubleN.EsRequeridoBD = false;
			loColDoubleN.CampoId = 35;
			loColDoubleN.Descripcion = "indice_reprobacion_acumulado";
			loColDoubleN.EsIdentity = false;
			loColDoubleN.Tipo = typeof(Double?);
			AgregarPropiedad<Double?>("indice_reprobacion_acumulado", loColDoubleN); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 36;
			loColstring.Descripcion = "becado_por";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("becado_por", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 37;
			loColInt32N.Descripcion = "nip";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("nip", loColInt32N); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 2;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 38;
			loColstring.Descripcion = "tipo_alumno";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("tipo_alumno", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 39;
			loColstring.Descripcion = "nacionalidad";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("nacionalidad", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 30;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 40;
			loColstring.Descripcion = "usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 41;
			loColDateTimeN.Descripcion = "fecha_actualizacion";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_actualizacion", loColDateTimeN); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 42;
			loColInt32N.Descripcion = "folio";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("folio", loColInt32N); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 43;
			loColDateTimeN.Descripcion = "fecha_titulacion";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("fecha_titulacion", loColDateTimeN); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 8;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 44;
			loColstring.Descripcion = "opcion_titulacion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("opcion_titulacion", loColstring); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.Valor = null;
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 7;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 45;
			loColDateTimeN.Descripcion = "estatus_alumno_fecha";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("estatus_alumno_fecha", loColDateTimeN); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 100;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 46;
			loColstring.Descripcion = "estatus_alumno_usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estatus_alumno_usuario", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 3;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 47;
			loColstring.Descripcion = "estatus_alumno_anterior";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("estatus_alumno_anterior", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 5;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 48;
			loColstring.Descripcion = "periodo_titulacion";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("periodo_titulacion", loColstring); 
			}
			#endregion
		}
	}
