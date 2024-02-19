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
	/// Clase bib_Libro generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Bib_Libros:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Libro.
		/// </summary>
		public Bib_Libros():base()
		{
			NombreTabla = "bib_libros";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase bib_Libro.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Bib_Libros(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "bib_libros";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
          #region Propiedades
          /// <summary>
          /// Obtiene o establece id_libro.Sin descripcion para id_libro 
          /// </summary>
          /// <value>
          /// id_libro 
          /// </value>
          [Key]
          [Required]
          public Int32 id_libro
          {
               get
               {
                    return ObtenerValorPropiedad<Int32>("id_libro");
               }

               set
               {
                    EstablecerValorPropiedad<Int32>("id_libro", value);
               }

          }
          /// <summary>
          /// Obtiene o establece id_mat_bib.Sin descripcion para id_mat_bib 
          /// </summary>
          /// <value>
          /// id_mat_bib 
          /// </value>
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
          /// Obtiene o establece isbn.Sin descripcion para isbn 
          /// </summary>
          /// <value>
          /// isbn 
          /// </value>
          [Required]
          [MaxLength(50)]
          [DefaultValue(null)]
          public string isbn
          {
               get
               {
                    return ObtenerValorPropiedad<string>("isbn");
               }

               set
               {
                    EstablecerValorPropiedad<string>("isbn", value);
               }

          }
          /// <summary>
          /// Obtiene o establece clasificacion.Sin descripcion para clasificacion 
          /// </summary>
          /// <value>
          /// clasificacion 
          /// </value>
          [Required]
          [MaxLength(10)]
          [DefaultValue(null)]
          public string clasificacion
          {
               get
               {
                    return ObtenerValorPropiedad<string>("clasificacion");
               }

               set
               {
                    EstablecerValorPropiedad<string>("clasificacion", value);
               }

          }
          /// <summary>
          /// Obtiene o establece no_paginas.Sin descripcion para no_paginas 
          /// </summary>
          /// <value>
          /// no_paginas 
          /// </value>
          [Required]
          public Byte no_paginas
          {
               get
               {
                    return ObtenerValorPropiedad<Byte>("no_paginas");
               }

               set
               {
                    EstablecerValorPropiedad<Byte>("no_paginas", value);
               }

          }
          /// <summary>
          /// Obtiene o establece capitulos.Sin descripcion para capitulos 
          /// </summary>
          /// <value>
          /// capitulos 
          /// </value>
          [Required]
          public Byte capitulos
          {
               get
               {
                    return ObtenerValorPropiedad<Byte>("capitulos");
               }

               set
               {
                    EstablecerValorPropiedad<Byte>("capitulos", value);
               }

          }
          /// <summary>
          /// Obtiene o establece edicion.Sin descripcion para edicion 
          /// </summary>
          /// <value>
          /// edicion 
          /// </value>
          [MaxLength(50)]
          [DefaultValue(null)]
          public string edicion
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
          /// Carga un registro especifico denotado por las llaves de la tabla bib_libro.		/// </summary>
          /// <param name="piid_libro">id_libro</param>
          public void Cargar(Int32 piid_libro)
          {
               base.Cargar(piid_libro);
          }
          /// <summary>
          /// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
          /// otro metodo estatico. 
          /// </summary>
          protected override void CargaPropiedadesdeColumna()
          {
               PropiedadesColumna<Int32> loColInt32;
               PropiedadesColumna<string> loColstring;
               PropiedadesColumna<Byte> loColByte;
               if (!Object.Equals(CamposLlave, null) && !Object.Equals(Propiedades, null))
                    return;

               CamposLlave = new Dictionary<string, object>(1);
               Propiedades = new Dictionary<string, Propiedad>(7);

               AgregaCampoLlave("id_libro", null);

               loColInt32 = new PropiedadesColumna<Int32>();
               loColInt32.EsPrimaryKey = true;
               loColInt32.Longitud = 4;
               loColInt32.Precision = 10;
               loColInt32.EsRequeridoBD = true;
               loColInt32.CampoId = 0;
               loColInt32.Descripcion = "Sin descripcion para id_libro";
               loColInt32.EsIdentity = true;
               loColInt32.Tipo = typeof(Int32);
               AgregarPropiedad<Int32>("id_libro", loColInt32);

               loColInt32 = new PropiedadesColumna<Int32>();
               loColInt32.EsPrimaryKey = false;
               loColInt32.Longitud = 4;
               loColInt32.Precision = 10;
               loColInt32.EsRequeridoBD = true;
               loColInt32.CampoId = 1;
               loColInt32.Descripcion = "Sin descripcion para id_mat_bib";
               loColInt32.EsIdentity = false;
               loColInt32.Tipo = typeof(Int32);
               AgregarPropiedad<Int32>("id_mat_bib", loColInt32);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 50;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 2;
               loColstring.Descripcion = "Sin descripcion para isbn";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("isbn", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 10;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 3;
               loColstring.Descripcion = "Sin descripcion para clasificacion";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("clasificacion", loColstring);

               loColByte = new PropiedadesColumna<Byte>();
               loColByte.EsPrimaryKey = false;
               loColByte.Longitud = 1;
               loColByte.Precision = 3;
               loColByte.EsRequeridoBD = true;
               loColByte.CampoId = 4;
               loColByte.Descripcion = "Sin descripcion para no_paginas";
               loColByte.EsIdentity = false;
               loColByte.Tipo = typeof(Byte);
               AgregarPropiedad<Byte>("no_paginas", loColByte);

               loColByte = new PropiedadesColumna<Byte>();
               loColByte.EsPrimaryKey = false;
               loColByte.Longitud = 1;
               loColByte.Precision = 3;
               loColByte.EsRequeridoBD = true;
               loColByte.CampoId = 5;
               loColByte.Descripcion = "Sin descripcion para capitulos";
               loColByte.EsIdentity = false;
               loColByte.Tipo = typeof(Byte);
               AgregarPropiedad<Byte>("capitulos", loColByte);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 50;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = false;
               loColstring.CampoId = 6;
               loColstring.Descripcion = "Sin descripcion para edicion";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("edicion", loColstring);
          }
          #endregion

     }
}
