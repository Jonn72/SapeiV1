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
	/// Clase bib_MaterialBibliografico generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_MaterialesBibliograficos:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_MaterialBibliografico.
		/// </summary>
		public Bib_MaterialesBibliograficos():base()
		{
			NombreTabla = "bib_materialbibliografico";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_MaterialBibliografico.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_MaterialesBibliograficos(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_materialbibliografico";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
          #region Propiedades
          /// <summary>
          /// Obtiene o establece id_mat_bib.Sin descripcion para id_mat_bib 
          /// </summary>
          /// <value>
          /// id_mat_bib 
          /// </value>
          [Key]
          [Required]
          public Int32 id_mat_bib
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
          /// Obtiene o establece id_autor.Sin descripcion para id_autor 
          /// </summary>
          /// <value>
          /// id_autor 
          /// </value>
          [Required]
          public Int16 id_autor
          {
               get
               {
                    return ObtenerValorPropiedad<Int16>("id_autor");
               }

               set
               {
                    EstablecerValorPropiedad<Int16>("id_autor", value);
               }

          }
          /// <summary>
          /// Obtiene o establece id_carrera.Sin descripcion para id_carrera 
          /// </summary>
          /// <value>
          /// id_carrera 
          /// </value>
          [Required]
          [MaxLength(3)]
          [DefaultValue(null)]
          public string id_carrera
          {
               get
               {
                    return ObtenerValorPropiedad<string>("id_carrera");
               }

               set
               {
                    EstablecerValorPropiedad<string>("id_carrera", value);
               }

          }
          /// <summary>
          /// Obtiene o establece id_editorial.Sin descripcion para id_editorial 
          /// </summary>
          /// <value>
          /// id_editorial 
          /// </value>
          [Required]
          public Int32 id_editorial
          {
               get
               {
                    return ObtenerValorPropiedad<Int32>("id_editorial");
               }

               set
               {
                    EstablecerValorPropiedad<Int32>("id_editorial", value);
               }

          }
          /// <summary>
          /// Obtiene o establece titulo.Sin descripcion para titulo 
          /// </summary>
          /// <value>
          /// titulo 
          /// </value>
          [DefaultValue(null)]
          public string titulo
          {
               get
               {
                    return ObtenerValorPropiedad<string>("titulo");
               }

               set
               {
                    EstablecerValorPropiedad<string>("titulo", value);
               }

          }
          /// <summary>
          /// Obtiene o establece f_ingreso.Sin descripcion para f_ingreso 
          /// </summary>
          /// <value>
          /// f_ingreso 
          /// </value>
          [Required]
          public DateTime f_ingreso
          {
               get
               {
                    return ObtenerValorPropiedad<DateTime>("f_ingreso");
               }

               set
               {
                    EstablecerValorPropiedad<DateTime>("f_ingreso", value);
               }

          }
          /// <summary>
          /// Obtiene o establece existencia.Sin descripcion para existencia 
          /// </summary>
          /// <value>
          /// existencia 
          /// </value>
          [Required]
          public Int32 existencia
          {
               get
               {
                    return ObtenerValorPropiedad<Int32>("existencia");
               }

               set
               {
                    EstablecerValorPropiedad<Int32>("existencia", value);
               }

          }
          /// <summary>
          /// Obtiene o establece tipo_material.Sin descripcion para tipo_material 
          /// </summary>
          /// <value>
          /// tipo_material 
          /// </value>
          [Required]
          public Int32 tipo_material
          {
               get
               {
                    return ObtenerValorPropiedad<Int32>("tipo_material");
               }

               set
               {
                    EstablecerValorPropiedad<Int32>("tipo_material", value);
               }

          }
          /// <summary>
          /// Obtiene o establece baja.Sin descripcion para baja 
          /// </summary>
          /// <value>
          /// baja 
          /// </value>
          [Required]
          [DefaultValue(false)]
          public Boolean baja
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
          /// Carga un registro especifico denotado por las llaves de la tabla bib_materialbibliografico.		/// </summary>
          /// <param name="piid_mat_bib">id_mat_bib</param>
          public void Cargar(Int32 piid_mat_bib)
          {
               base.Cargar(piid_mat_bib);
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
               PropiedadesColumna<Boolean> loColBoolean;
               if (!Object.Equals(CamposLlave, null) && !Object.Equals(Propiedades, null))
                    return;

               CamposLlave = new Dictionary<string, object>(1);
               Propiedades = new Dictionary<string, Propiedad>(9);

               AgregaCampoLlave("id_mat_bib", null);

               loColInt32 = new PropiedadesColumna<Int32>();
               loColInt32.EsPrimaryKey = true;
               loColInt32.Longitud = 4;
               loColInt32.Precision = 10;
               loColInt32.EsRequeridoBD = true;
               loColInt32.CampoId = 0;
               loColInt32.Descripcion = "Sin descripcion para id_mat_bib";
               loColInt32.EsIdentity = true;
               loColInt32.Tipo = typeof(Int32);
               AgregarPropiedad<Int32>("id_mat_bib", loColInt32);

               loColInt16 = new PropiedadesColumna<Int16>();
               loColInt16.EsPrimaryKey = false;
               loColInt16.Longitud = 2;
               loColInt16.Precision = 5;
               loColInt16.EsRequeridoBD = true;
               loColInt16.CampoId = 1;
               loColInt16.Descripcion = "Sin descripcion para id_autor";
               loColInt16.EsIdentity = false;
               loColInt16.Tipo = typeof(Int16);
               AgregarPropiedad<Int16>("id_autor", loColInt16);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 3;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 2;
               loColstring.Descripcion = "Sin descripcion para id_carrera";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("id_carrera", loColstring);

               loColInt32 = new PropiedadesColumna<Int32>();
               loColInt32.EsPrimaryKey = false;
               loColInt32.Longitud = 4;
               loColInt32.Precision = 10;
               loColInt32.EsRequeridoBD = true;
               loColInt32.CampoId = 3;
               loColInt32.Descripcion = "Sin descripcion para id_editorial";
               loColInt32.EsIdentity = false;
               loColInt32.Tipo = typeof(Int32);
               AgregarPropiedad<Int32>("id_editorial", loColInt32);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = -1;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = false;
               loColstring.CampoId = 4;
               loColstring.Descripcion = "Sin descripcion para titulo";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("titulo", loColstring);

               loColDateTime = new PropiedadesColumna<DateTime>();
               loColDateTime.EsPrimaryKey = false;
               loColDateTime.Longitud = 8;
               loColDateTime.Precision = 23;
               loColDateTime.EsRequeridoBD = true;
               loColDateTime.CampoId = 5;
               loColDateTime.Descripcion = "Sin descripcion para f_ingreso";
               loColDateTime.EsIdentity = false;
               loColDateTime.Tipo = typeof(DateTime);
               AgregarPropiedad<DateTime>("f_ingreso", loColDateTime);

               loColInt32 = new PropiedadesColumna<Int32>();
               loColInt32.EsPrimaryKey = false;
               loColInt32.Longitud = 4;
               loColInt32.Precision = 10;
               loColInt32.EsRequeridoBD = true;
               loColInt32.CampoId = 6;
               loColInt32.Descripcion = "Sin descripcion para existencia";
               loColInt32.EsIdentity = false;
               loColInt32.Tipo = typeof(Int32);
               AgregarPropiedad<Int32>("existencia", loColInt32);

               loColInt32 = new PropiedadesColumna<Int32>();
               loColInt32.EsPrimaryKey = false;
               loColInt32.Longitud = 4;
               loColInt32.Precision = 10;
               loColInt32.EsRequeridoBD = true;
               loColInt32.CampoId = 7;
               loColInt32.Descripcion = "Sin descripcion para tipo_material";
               loColInt32.EsIdentity = false;
               loColInt32.Tipo = typeof(Int32);
               AgregarPropiedad<Int32>("tipo_material", loColInt32);

               loColBoolean = new PropiedadesColumna<Boolean>();
               loColBoolean.Valor = false;
               loColBoolean.EsPrimaryKey = false;
               loColBoolean.Longitud = 1;
               loColBoolean.Precision = 1;
               loColBoolean.EsRequeridoBD = true;
               loColBoolean.CampoId = 8;
               loColBoolean.Descripcion = "Sin descripcion para baja";
               loColBoolean.EsIdentity = false;
               loColBoolean.Tipo = typeof(Boolean);
               AgregarPropiedad<Boolean>("baja", loColBoolean);
          }
          #endregion

     }
}
