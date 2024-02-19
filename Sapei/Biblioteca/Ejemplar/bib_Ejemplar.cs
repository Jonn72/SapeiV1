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
	/// Clase bib_Ejemplar generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Ejemplares:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Ejemplar.
		/// </summary>
		public Bib_Ejemplares():base()
		{
			NombreTabla = "bib_ejemplar";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Ejemplar.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Ejemplares(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_ejemplar";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_ejemplar.Sin descripcion para Id_ejemplar 
		/// </summary>
		/// <value>
		/// Id_ejemplar 
		/// </value>
		[Key]
		[Required]
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
		/// Obtiene o establece Reserva.Sin descripcion para Reserva 
		/// </summary>
		/// <value>
		/// Reserva 
		/// </value>
		[Required]
		public Boolean Reserva
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("reserva");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("reserva", value);
			}

		}

        [Required]
        public Boolean Baja
        {
            get
            {
                return ObtenerValorPropiedad<Boolean>("baja");
            }

            set
            {
                EstablecerValorPropiedad<Boolean>("baja", value);
            }

        }
        #endregion
        #region Funciones
        /// <summary>
        /// Carga un registro especifico denotado por las llaves de la tabla bib_Ejemplar.		/// </summary>
        /// <param name="psId_ejemplar">Id_ejemplar</param>
        public void Cargar(string psId_ejemplar)
		{
			base.Cargar(psId_ejemplar);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<string> loColstring; 
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<Boolean> loColBoolean; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(3);

			AgregaCampoLlave("id_ejemplar",null);

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = true;
			loColstring.Longitud = 60;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 0;
			loColstring.Descripcion = "Sin descripcion para Id_ejemplar";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("id_ejemplar", loColstring); 

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

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 2;
			loColBoolean.Descripcion = "Sin descripcion para Reserva";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("reserva", loColBoolean);

            loColBoolean = new PropiedadesColumna<Boolean>();
            loColBoolean.EsPrimaryKey = false;
            loColBoolean.Longitud = 1;
            loColBoolean.Precision = 1;
            loColBoolean.EsRequeridoBD = true;
            loColBoolean.CampoId = 3;
            loColBoolean.Descripcion = "Sin descripcion para Baja";
            loColBoolean.EsIdentity = false;
            loColBoolean.Tipo = typeof(Boolean);
            AgregarPropiedad<Boolean>("baja", loColBoolean);
        }
			#endregion

		}
	}
