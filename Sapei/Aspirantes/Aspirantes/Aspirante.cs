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
     /// Clase aspirante generada automáticamente desde el Generador de Código SII
     /// </summary>
     [Serializable]
     public partial class Aspirante : CatalogoFijoElemento
     {
          #region Contructor
          /// <summary>
          /// Inicia una nueva instancia de la clase aspirante.
          /// </summary>
          public Aspirante()
               : base()
          {
               NombreTabla = "aspirantes";
               Propietario = "dbo";
               RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
               CargaPropiedadesdeColumna();

          }
          /// <summary>
          /// Inicia una nueva instancia de la clase aspirante.
          /// </summary>
          /// <param name="poSistema">Clase del Sistema</param>
          public Aspirante(Sistema poSistema)
               : base(poSistema)
          {
               NombreTabla = "aspirantes";
               Propietario = "dbo";
               RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
               CargaPropiedadesdeColumna();

          }
          #endregion
          #region Propiedades
          /// <summary>
          /// Obtiene o establece folio.Sin descripcion para folio 
          /// </summary>
          /// <value>
          /// folio 
          /// </value>
          [Key]
          [Required]
          [MaxLength(8)]
          [DefaultValue(null)]
          public string folio
          {
               get
               {
                    return ObtenerValorPropiedad<string>("folio");
               }

               set
               {
                    EstablecerValorPropiedad<string>("folio", value);
               }

          }
          /// <summary>
          /// Obtiene o establece estatusAspirante.Sin descripcion para estatusAspirante 
          /// </summary>
          /// <value>
          /// estatusAspirante 
          /// </value>
          [MaxLength(5)]
          [DefaultValue("0")]
          public string estatusAspirante
          {
               get
               {
                    return ObtenerValorPropiedad<string>("estatusAspirante");
               }

               set
               {
                    EstablecerValorPropiedad<string>("estatusAspirante", value);
               }

          }
          /// <summary>
          /// Obtiene o establece fecha.Sin descripcion para fecha 
          /// </summary>
          /// <value>
          /// fecha 
          /// </value>
          [DefaultValue(null)]
          public DateTime? fecha
          {
               get
               {
                    return ObtenerValorPropiedad<DateTime?>("fecha");
               }

               set
               {
                    EstablecerValorPropiedad<DateTime?>("fecha", value);
               }

          }
          /// <summary>
          /// Obtiene o establece no_de_control.Sin descripcion para no_de_control 
          /// </summary>
          /// <value>
          /// no_de_control 
          /// </value>
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
          /// Obtiene o establece vuelta.Sin descripcion para vuelta 
          /// </summary>
          /// <value>
          /// vuelta 
          /// </value>
          [DefaultValue(1)]
          public Int16? vuelta
          {
               get
               {
                    return ObtenerValorPropiedad<Int16?>("vuelta");
               }

               set
               {
                    EstablecerValorPropiedad<Int16?>("vuelta", value);
               }

          }
          /// <summary>
          /// Obtiene o establece periodo.Sin descripcion para periodo 
          /// </summary>
          /// <value>
          /// periodo 
          /// </value>
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
          #endregion
          #region Funciones
          /// <summary>
          /// Carga un registro especifico denotado por las llaves de la tabla aspirante.		/// </summary>
          /// <param name="psfolio">folio</param>
          public void Cargar(string psfolio)
          {
               base.Cargar(psfolio);
          }
          /// <summary>
          /// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
          /// otro metodo estatico. 
          /// </summary>
          protected override void CargaPropiedadesdeColumna()
          {
               PropiedadesColumna<string> loColstring;
               PropiedadesColumna<DateTime?> loColDateTimeN;
               PropiedadesColumna<Int16?> loColInt16N;
               if (!Object.Equals(CamposLlave, null) && !Object.Equals(Propiedades, null))
                    return;

               CamposLlave = new Dictionary<string, object>(1);
               Propiedades = new Dictionary<string, Propiedad>(6);

               AgregaCampoLlave("folio", null);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = true;
               loColstring.Longitud = 8;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 0;
               loColstring.Descripcion = "Sin descripcion para folio";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("folio", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = "0";
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 5;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = false;
               loColstring.CampoId = 2;
               loColstring.Descripcion = "Sin descripcion para estatusAspirante";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("estatusAspirante", loColstring);

               loColDateTimeN = new PropiedadesColumna<DateTime?>();
               loColDateTimeN.Valor = null;
               loColDateTimeN.EsPrimaryKey = false;
               loColDateTimeN.Longitud = 8;
               loColDateTimeN.Precision = 23;
               loColDateTimeN.EsRequeridoBD = false;
               loColDateTimeN.CampoId = 5;
               loColDateTimeN.Descripcion = "Sin descripcion para fecha";
               loColDateTimeN.EsIdentity = false;
               loColDateTimeN.Tipo = typeof(DateTime?);
               AgregarPropiedad<DateTime?>("fecha", loColDateTimeN);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 10;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = false;
               loColstring.CampoId = 6;
               loColstring.Descripcion = "Sin descripcion para no_de_control";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("no_de_control", loColstring);

               loColInt16N = new PropiedadesColumna<Int16?>();
               loColInt16N.Valor = 1;
               loColInt16N.EsPrimaryKey = false;
               loColInt16N.Longitud = 2;
               loColInt16N.Precision = 5;
               loColInt16N.EsRequeridoBD = false;
               loColInt16N.CampoId = 7;
               loColInt16N.Descripcion = "Sin descripcion para vuelta";
               loColInt16N.EsIdentity = false;
               loColInt16N.Tipo = typeof(Int16?);
               AgregarPropiedad<Int16?>("vuelta", loColInt16N);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 5;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = false;
               loColstring.CampoId = 8;
               loColstring.Descripcion = "Sin descripcion para periodo";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("periodo", loColstring);
          }
          #endregion

     }
}
