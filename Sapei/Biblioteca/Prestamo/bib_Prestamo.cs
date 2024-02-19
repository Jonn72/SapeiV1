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
	/// Clase bib_Prestamo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Prestamos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Prestamo.
		/// </summary>
		public Bib_Prestamos():base()
		{
			NombreTabla = "bib_prestamo";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Prestamo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Prestamos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_prestamo";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_prestamo.Sin descripcion para Id_prestamo 
		/// </summary>
		/// <value>
		/// Id_prestamo 
		/// </value>
		[Key]
		[Required]
		public Int32 Id_prestamo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("Id_prestamo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_prestamo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Usuario.Sin descripcion para Usuario 
		/// </summary>
		/// <value>
		/// Usuario 
		/// </value>
		[MaxLength (20)]
		[DefaultValue(null)]
		public string Usuario
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
		/// Obtiene o establece Id_ejemplar.Sin descripcion para Id_ejemplar 
		/// </summary>
		/// <value>
		/// Id_ejemplar 
		/// </value>
		[MaxLength (60)]
		[DefaultValue(null)]
		public string Id_ejemplar
		{
			get
			{
				return ObtenerValorPropiedad<string>("id_ejemplar");
			}

			set
			{
				EstablecerValorPropiedad<string>("id_ejemplar", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Id_prestador.Sin descripcion para Id_prestador 
		/// </summary>
		/// <value>
		/// Id_prestador 
		/// </value>
		[DefaultValue(null)]
		public Int32? Id_prestador
		{
			get
			{
				return ObtenerValorPropiedad<Int32?>("id_prestador");
			}

			set
			{
				EstablecerValorPropiedad<Int32?>("id_prestador", value);
			}

		}
		/// <summary>
		/// Obtiene o establece F_prestamo.Sin descripcion para F_prestamo 
		/// </summary>
		/// <value>
		/// F_prestamo 
		/// </value>
		[Required]
		public DateTime F_prestamo
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("f_prestamo");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("f_prestamo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece F_entrega.Sin descripcion para F_entrega 
		/// </summary>
		/// <value>
		/// F_entrega 
		/// </value>
		public DateTime? F_entrega
		{
			get
			{
				return ObtenerValorPropiedad<DateTime?>("f_entrega");
			}

			set
			{
				EstablecerValorPropiedad<DateTime?>("f_entrega", value);
			}

		}
        //
        /// <summary>
		/// Obtiene o establece F_entrega.Sin descripcion para F_entrega 
		/// </summary>
		/// <value>
		/// F_limite
		/// </value>
		public DateTime F_limite
        {
            get
            {
                return ObtenerValorPropiedad<DateTime>("f_limite");
            }

            set
            {
                EstablecerValorPropiedad<DateTime>("f_limite", value);
            }

        }
        [MaxLength(5)]
        public String Periodo
        {
            get
            {
                return ObtenerValorPropiedad<String>("periodo");
            }

            set
            {
                EstablecerValorPropiedad<String>("periodo", value);
            }

        }

        #endregion
        #region Funciones
        /// <summary>
        /// Carga un registro especifico denotado por las llaves de la tabla bib_Prestamo.		/// </summary>
        /// <param name="piId_prestamo">Id_prestamo</param>
        public void Cargar(Int32 piId_prestamo)
		{
			base.Cargar(piId_prestamo);
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
			 PropiedadesColumna<DateTime> loColDateTime;
            PropiedadesColumna<DateTime?> loColDateTimeN;

            if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(6);

			AgregaCampoLlave("id_prestamo",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para Id_prestamo";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_prestamo", loColInt32); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 20;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 1;
			loColstring.Descripcion = "Sin descripcion para Usuario";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("usuario", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = false;
			loColstring.CampoId = 2;
			loColstring.Descripcion = "Sin descripcion para Id_ejemplar";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("id_ejemplar", loColstring); 

			loColInt32N = new PropiedadesColumna<Int32?>();
			loColInt32N.Valor = null;
			loColInt32N.EsPrimaryKey = false;
			loColInt32N.Longitud = 4;
			loColInt32N.Precision = 10;
			loColInt32N.EsRequeridoBD = false;
			loColInt32N.CampoId = 3;
			loColInt32N.Descripcion = "Sin descripcion para Id_prestador";
			loColInt32N.EsIdentity = false;
			loColInt32N.Tipo = typeof(Int32?);
			AgregarPropiedad<Int32?>("id_prestador", loColInt32N); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 4;
			loColDateTime.Descripcion = "Sin descripcion para F_prestamo";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("f_prestamo", loColDateTime); 

			loColDateTimeN = new PropiedadesColumna<DateTime?>();
			loColDateTimeN.EsPrimaryKey = false;
			loColDateTimeN.Longitud = 8;
			loColDateTimeN.Precision = 23;
			loColDateTimeN.EsRequeridoBD = false;
			loColDateTimeN.CampoId = 5;
			loColDateTimeN.Descripcion = "Sin descripcion para F_entrega";
			loColDateTimeN.EsIdentity = false;
			loColDateTimeN.Tipo = typeof(DateTime?);
			AgregarPropiedad<DateTime?>("f_entrega", loColDateTimeN);

            loColDateTime = new PropiedadesColumna<DateTime>();
            loColDateTime.EsPrimaryKey = false;
            loColDateTime.Longitud = 8;
            loColDateTime.Precision = 23;
            loColDateTime.EsRequeridoBD = true;
            loColDateTime.CampoId = 6;
            loColDateTime.Descripcion = "Sin descripcion para F_prestamo";
            loColDateTime.EsIdentity = false;
            loColDateTime.Tipo = typeof(DateTime);
            AgregarPropiedad<DateTime>("f_limite", loColDateTime);

            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 5;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = false;
            loColstring.CampoId = 7;
            loColstring.Descripcion = "Sin descripcion para Periodo";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("periodo", loColstring);

        }
        #endregion

    }
	}
