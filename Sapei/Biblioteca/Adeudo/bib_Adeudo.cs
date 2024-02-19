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
	/// Clase bib_Adeudo generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Adeudos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Adeudo.
		/// </summary>
		public Bib_Adeudos():base()
		{
			NombreTabla = "bib_adeudo";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Adeudo.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Adeudos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_adeudo";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece Id_adeudo.Sin descripcion para Id_adeudo 
		/// </summary>
		/// <value>
		/// Id_adeudo 
		/// </value>
		[Key]
		[Required]
		public Int32 Id_adeudo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_adeudo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_adeudo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Id_prestamo.Sin descripcion para Id_prestamo 
		/// </summary>
		/// <value>
		/// Id_prestamo 
		/// </value>
		[Required]
		public Int32 Id_prestamo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("id_prestamo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("id_prestamo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Monto.Sin descripcion para Monto 
		/// </summary>
		/// <value>
		/// Monto 
		/// </value>
		[Required]
		public Int32 Monto
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("monto");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("monto", value);
			}

		}
		/// <summary>
		/// Obtiene o establece D_retardo.Sin descripcion para D_retardo 
		/// </summary>
		/// <value>
		/// D_retardo 
		/// </value>
		[Required]
		public Int32 D_retardo
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("d_retardo");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("d_retardo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece Liquidado.Sin descripcion para Liquidado 
		/// </summary>
		/// <value>
		/// Liquidado 
		/// </value>
		[Required]
		public Boolean Liquidado
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("liquidado");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("liquidado", value);
			}

		}
        [Required]
        [MaxLength(20)]
        public string Tipo_Liberacion
        {
            get
            {
                return ObtenerValorPropiedad<string>("tipo_liberacion");
            }

            set
            {
                EstablecerValorPropiedad<string>("tipo_liberacion", value);
            }

        }
        public String Usuario
        {
            get
            {
                return ObtenerValorPropiedad<String>("usuario");
            }

            set
            {
                EstablecerValorPropiedad<String>("usuario", value);
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
        /// Carga un registro especifico denotado por las llaves de la tabla bib_Adeudo.		/// </summary>
        /// <param name="piId_adeudo">Id_adeudo</param>
        public void Cargar(Int32 piId_adeudo)
		{
			base.Cargar(piId_adeudo);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<Boolean> loColBoolean;
             PropiedadesColumna<String> loColString;
             
            if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(5);

			AgregaCampoLlave("id_adeudo",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para Id_adeudo";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_adeudo", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 1;
			loColInt32.Descripcion = "Sin descripcion para Id_prestamo";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("id_prestamo", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 2;
			loColInt32.Descripcion = "Sin descripcion para Monto";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("monto", loColInt32); 

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = false;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 3;
			loColInt32.Descripcion = "Sin descripcion para D_retardo";
			loColInt32.EsIdentity = false;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("d_retardo", loColInt32); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 4;
			loColBoolean.Descripcion = "Sin descripcion para Liquidado";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("liquidado", loColBoolean);


            loColString = new PropiedadesColumna<String>();
            loColString.Longitud = 3;
            loColString.EsRequeridoBD = false;
            loColString.CampoId = 5;
            loColString.Descripcion = "Sin descripcion para tipo_liberacion";
            loColString.EsIdentity = false;
            loColString.Tipo = typeof(String);
            AgregarPropiedad<String>("tipo_liberacion", loColString);
            
            loColString = new PropiedadesColumna<String>();
            loColString.Longitud = 30;
            loColString.EsRequeridoBD = true;
            loColString.CampoId = 6;
            loColString.Descripcion = "Sin descripcion para clave_usuario";
            loColString.EsIdentity = false;
            loColString.Tipo = typeof(String);
            AgregarPropiedad<String>("usuario", loColString);


            loColString = new PropiedadesColumna<String>();
            loColString.Longitud = 5;
            loColString.EsRequeridoBD = true;
            loColString.CampoId = 7;
            loColString.Descripcion = "Sin descripcion para periodo";
            loColString.EsIdentity = false;
            loColString.Tipo = typeof(String);
            AgregarPropiedad<String>("periodo", loColString);
        }
			#endregion

		}
	}
