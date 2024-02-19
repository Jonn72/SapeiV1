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
    /// Clase rp_estado_solicitud generada automáticamente desde el Generador de Código SII
    /// </summary>
    [Serializable]
	public partial class RP_Estado_Solicitud:CatalogoFijoElemento
	{
        #region Contructor
        /// <summary>
        /// Inicia una nueva instancia de la clase ss_estado_solicitud.
        /// </summary>
        public RP_Estado_Solicitud():base()
		{
			NombreTabla = "rp_estado_solicitud";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
        /// <summary>
        /// Inicia una nueva instancia de la clase ss_estado_solicitud.
        /// </summary>
        /// <param name="poSistema">Clase del Sistema</param>
        public RP_Estado_Solicitud(Sistema poSistema): base(poSistema)
		{
			NombreTabla = "rp_estado_solicitud";
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
        /// domicilio 
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
        /// Obtiene o establece rp_estado_solicitud.Sin descripcion para rp_estado_solicitud 
        /// </summary>
        /// <value>
        /// periodo
        /// </value>
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
        /// Obtiene o establece estado.Sin descripcion para estado 
        /// </summary>
        /// <value>
        /// numero 
        /// </value>
        [Required]
		public Int32 estado
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("estado");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("estado", value);
			}

		}
        #endregion
        #region Funciones
        /// <summary>
        /// Carga un registro especifico denotado por las llaves de la tabla rp_estado_solicitud.		/// </summary>
        /// <param name="psPeriodo">periodo</param>
        /// /// <param name="psNo_de_control">no_de_control</param>
        public void Cargar(string psNo_de_control)
		{
			base.Cargar(psNo_de_control);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32> loColInt32; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(3);

            AgregaCampoLlave("no_de_control", null);

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

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 5;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = true;
            loColstring.CampoId = 0;
            loColstring.Descripcion = "Sin descripcion para periodo";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("periodo", loColstring);

            loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "Sin descripcion para estado";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("estado", loColInt32); 
			}
			#endregion

		}
	}
