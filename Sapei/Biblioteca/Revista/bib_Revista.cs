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
	/// Clase bib_Revista generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Revistas:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Revista.
		/// </summary>
		public Bib_Revistas():base()
		{
			NombreTabla = "bib_revistas";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Revista.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Revistas(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_revistas";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_Revista.Sin descripcion para Id_Revista 
		/// </summary>
		/// <value>
		/// Id_Revista 
		/// </value>
		[Key]
		[Required]
		public Int32 Id_Revista
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_revista");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_revista", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Id_mat_bib.Sin descripcion para Id_mat_bib 
		/// </summary>
		/// <value>
		/// Id_mat_bib 
		/// </value>
		[Required]
		public Int32 Id_mat_bib
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_mat_bib");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_mat_bib", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Secciones.Sin descripcion para Secciones 
		/// </summary>
		/// <value>
		/// Secciones 
		/// </value>
		[Required]
		public Int32 Secciones
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("secciones");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("secciones", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_p.Sin descripcion para fecha_p 
		/// </summary>
		/// <value>
		/// fecha_p 
		/// </value>
		[Required]
		public DateTime Fecha_p
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_p");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_p", value);
			}

		}
        [Required]
        public string Edicion
        {
            get
            {
                return ObtenerValorPropiedad<string>("edicion");
            }

            set
            {
                EstablecerValorPropiedad<string>("edicion", value);
            }

        }
        #endregion
        #region Funciones
        /// <summary>
        /// Carga un registro especifico denotado por las llaves de la tabla bib_Revista.		/// </summary>
        /// <param name="piId_Revista">Id_Revista</param>
        public void Cargar(Int32 piId_Revista)
		{
			base.Cargar(piId_Revista);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<DateTime> loColDateTime;
            PropiedadesColumna<string> loColstring;

            if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(4);

			AgregaCampoLlave("id_revista",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para Id_Revista";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_revista", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 1;
			loColInt32.Descripcion = "Sin descripcion para Id_mat_bib";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_mat_bib", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para Secciones";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("secciones", loColInt32); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 3;
			loColDateTime.Descripcion = "Sin descripcion para fecha_p";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_p", loColDateTime);


            loColstring = new PropiedadesColumna<string>();
            loColstring.Valor = null;
            loColstring.EsPrimaryKey = false;
            loColstring.Longitud = 100;
            loColstring.Precision = 0;
            loColstring.EsRequeridoBD = false;
            loColstring.CampoId = 4;
            loColstring.Descripcion = "Sin descripcion para Edicion";
            loColstring.EsIdentity = false;
            loColstring.Tipo = typeof(string);
            AgregarPropiedad<string>("edicion", loColstring);

        }
        #endregion

    }
	}
