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
    /// Clase cle_liberacion_ingle generada automáticamente desde el Generador de Código SII
    /// </summary>
    [Serializable]
    public partial class Cle_Liberacion_Ingles : CatalogoFijoElemento
    {
        #region Contructor
        /// <summary>
        /// Inicia una nueva instancia de la clase cle_liberacion_ingle.
        /// </summary>
        public Cle_Liberacion_Ingles() : base()
        {
            NombreTabla = "cle_liberacion_ingles";
            Propietario = "dbo";
            RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
            CargaPropiedadesdeColumna();

        }
        /// <summary>
        /// Inicia una nueva instancia de la clase cle_liberacion_ingle.
        /// </summary>
        /// <param name="poSistema">Clase del Sistema</param>
        public Cle_Liberacion_Ingles(Sistema poSistema) : base(poSistema)
        {
            NombreTabla = "cle_liberacion_ingles";
            Propietario = "dbo";
            RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
            CargaPropiedadesdeColumna();

        }
        #endregion
        #region Propiedades
        /// <summary>
        /// Obtiene o establece periodo.Sin descripcion para periodo 
        /// </summary>
        /// <value>
        /// periodo 
        /// </value>
        [Key]
        [Required]
        [MaxLength(5)]
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
        /// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
        /// </summary>
        /// <value>
        /// no_de_control 
        /// </value>
        [Key]
        [Required]
        [MaxLength(10)]
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
        /// Obtiene o establece id_liberacion.Sin descripcion para id_liberacion 
        /// </summary>
        /// <value>
        /// id_liberacion 
        /// </value>
        [Required]
        [MaxLength(3)]
        [DefaultValue(null)]
        public string id_liberacion
        {
            get
            {
                return ObtenerValorPropiedad<string>("id_liberacion");
            }

            set
            {
                EstablecerValorPropiedad<string>("id_liberacion", value);
            }

        }
        /// <summary>
        /// Obtiene o establece fecha_liberacion.Sin descripcion para fecha_liberacion 
        /// </summary>
        /// <value>
        /// fecha_liberacion 
        /// </value>
        [Required]
        [MaxLength(3)]
        [DefaultValue(null)]
        public string fecha_liberacion
        {
            get
            {
                return ObtenerValorPropiedad<string>("fecha_liberacion");
            }

            set
            {
                EstablecerValorPropiedad<string>("fecha_liberacion", value);
            }

        }
        /// <summary>
        /// Obtiene o establece promedio_o_puntos.Sin descripcion para promedio_o_puntos 
        /// </summary>
        /// <value>
        /// promedio_o_puntos 
        /// </value>
        [Required]
        [MaxLength(20)]
        [DefaultValue(null)]
        public string promedio_o_puntos
        {
            get
            {
                return ObtenerValorPropiedad<string>("promedio_o_puntos");
            }

            set
            {
                EstablecerValorPropiedad<string>("promedio_o_puntos", value);
            }

        }
        /// <summary>
        /// Obtiene o establece usuario.Sin descripcion para usuario 
        /// </summary>
        /// <value>
        /// usuario 
        /// </value>
        [Required]
        [MaxLength(30)]
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
        /// Obtiene o establece fecha_registro.Sin descripcion para fecha_registro 
        /// </summary>
        /// <value>
        /// fecha_registro 
        /// </value>
        [Required]
        public DateTime fecha_registro
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("fecha_registro");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("fecha_registro", value);
            }

        }
        #endregion
        #region Funciones
        /// <summary>
        /// Carga un registro especifico denotado por las llaves de la tabla cle_liberacion_ingle.		/// </summary>
        /// <param name="psperiodo">periodo</param>
        /// <param name="psno_de_control">no_de_control</param>
        public void Cargar(string psperiodo, string psno_de_control)
        {
            base.Cargar(psperiodo, psno_de_control);
        }
        /// <summary>
        /// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
        /// otro metodo estatico. 
        /// </summary>
        protected override void CargaPropiedadesdeColumna()
        {
            PropiedadesColumna<string> loColstring;
            PropiedadesColumna<DateTime> loColDateTime;
            if (!Object.Equals(CamposLlave, null) && !Object.Equals(Propiedades, null))
                return;

            CamposLlave = new Dictionary<string, object>(2);
            Propiedades = new Dictionary<string, Propiedad>(7);

            AgregaCampoLlave("periodo", null);
            AgregaCampoLlave("no_de_control", null);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = true;
            loColstring.Longitud = 5;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 0;
            loColstring.Descripcion = "Sin descripcion para periodo";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("periodo", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = true;
            loColstring.Longitud = 10;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 1;
            loColstring.Descripcion = "Sin descripcion para no_de_control";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("no_de_control", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 3;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 2;
            loColstring.Descripcion = "Sin descripcion para id_liberacion";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("id_liberacion", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 23;
            loColstring.Precision = 10;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 3;
            loColstring.Descripcion = "Sin descripcion para fecha_liberacion";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("fecha_liberacion", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 20;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 4;
            loColstring.Descripcion = "Sin descripcion para promedio_o_puntos";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("promedio_o_puntos", loColstring);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 30;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 5;
            loColstring.Descripcion = "Sin descripcion para usuario";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("usuario", loColstring);

            loColDateTime = new PropiedadesColumna<DateTime>();
            loColDateTime.EsPrimaryKey = false;
            loColDateTime.Longitud = 8;
            loColDateTime.Precision = 27;
            loColDateTime.EsRequeridoBD = true;
            loColDateTime.CampoId = 6;
            loColDateTime.Descripcion = "Sin descripcion para fecha_registro";
            loColDateTime.EsIdentity = false;
            loColDateTime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("fecha_registro", loColDateTime);
        }
        #endregion

    }
}

