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
     /// Clase buzon_mensaje generada automáticamente desde el Generador de Código SII
     /// </summary>
     [Serializable]
     public partial class Buzon_Mensaje : CatalogoFijoElemento
     {
          #region Contructor
          /// <summary>
          /// Inicia una nueva instancia de la clase buzon_mensaje.
          /// </summary>
          public Buzon_Mensaje()
               : base()
          {
               NombreTabla = "buzon_mensaje";
               Propietario = "dbo";
               RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
               CargaPropiedadesdeColumna();

          }
          /// <summary>
          /// Inicia una nueva instancia de la clase buzon_mensaje.
          /// </summary>
          /// <param name="poSistema">Clase del Sistema</param>
          public Buzon_Mensaje(Sistema poSistema)
               : base(poSistema)
          {
               NombreTabla = "buzon_mensaje";
               Propietario = "dbo";
               RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
               CargaPropiedadesdeColumna();

          }
          #endregion
          #region Propiedades
          /// <summary>
          /// Obtiene o establece id_mensaje.Sin descripcion para id_mensaje 
          /// </summary>
          /// <value>
          /// id_mensaje 
          /// </value>
          [Key]
          [Required]
          public Int32 id_mensaje
          {
               get
               {
                    return ObtenerValorPropiedad<Int32>("id_mensaje");
               }

               set
               {
                    EstablecerValorPropiedad<Int32>("id_mensaje", value);
               }

          }
          /// <summary>
          /// Obtiene o establece ticket.Sin descripcion para ticket 
          /// </summary>
          /// <value>
          /// ticket 
          /// </value>
          [Required]
          public Int32 ticket
          {
               get
               {
                    return ObtenerValorPropiedad<Int32>("ticket");
               }

               set
               {
                    EstablecerValorPropiedad<Int32>("ticket", value);
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
          /// Obtiene o establece rol.Sin descripcion para rol 
          /// </summary>
          /// <value>
          /// rol 
          /// </value>
          [DefaultValue(null)]
          public Boolean? rol
          {
               get
               {
                    return ObtenerValorPropiedad<Boolean?>("rol");
               }

               set
               {
                    EstablecerValorPropiedad<Boolean?>("rol", value);
               }

          }
          /// <summary>
          /// Obtiene o establece mensaje.Sin descripcion para mensaje 
          /// </summary>
          /// <value>
          /// mensaje 
          /// </value>
          [MaxLength(250)]
          [DefaultValue(null)]
          public string mensaje
          {
               get
               {
                    return ObtenerValorPropiedad<string>("mensaje");
               }

               set
               {
                    EstablecerValorPropiedad<string>("mensaje", value);
               }

          }
          /// <summary>
          /// Obtiene o establece status_mensaje.Sin descripcion para status_mensaje 
          /// </summary>
          /// <value>
          /// status_mensaje 
          /// </value>
          [DefaultValue(null)]
          public Boolean? status_mensaje
          {
               get
               {
                    return ObtenerValorPropiedad<Boolean?>("status_mensaje");
               }

               set
               {
                    EstablecerValorPropiedad<Boolean?>("status_mensaje", value);
               }

          }
          #endregion
          #region Funciones
          /// <summary>
          /// Carga un registro especifico denotado por las llaves de la tabla buzon_mensaje.		/// </summary>
          /// <param name="piid_mensaje">id_mensaje</param>
          public void Cargar(Int32 piid_mensaje)
          {
               base.Cargar(piid_mensaje);
          }
          /// <summary>
          /// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
          /// otro metodo estatico. 
          /// </summary>
          protected override void CargaPropiedadesdeColumna()
          {
               PropiedadesColumna<Int32> loColInt32;
               PropiedadesColumna<DateTime?> loColDateTimeN;
               PropiedadesColumna<Boolean?> loColBooleanN;
               PropiedadesColumna<string> loColstring;
               if (!Object.Equals(CamposLlave, null) && !Object.Equals(Propiedades, null))
                    return;

               CamposLlave = new Dictionary<string, object>(1);
               Propiedades = new Dictionary<string, Propiedad>(6);

               AgregaCampoLlave("id_mensaje", null);

               loColInt32 = new PropiedadesColumna<Int32>();
               loColInt32.EsPrimaryKey = true;
               loColInt32.Longitud = 4;
               loColInt32.Precision = 10;
               loColInt32.EsRequeridoBD = true;
               loColInt32.CampoId = 0;
               loColInt32.Descripcion = "Sin descripcion para id_mensaje";
               loColInt32.EsIdentity = true;
               loColInt32.Tipo = typeof(Int32);
               AgregarPropiedad<Int32>("id_mensaje", loColInt32);

               loColInt32 = new PropiedadesColumna<Int32>();
               loColInt32.EsPrimaryKey = false;
               loColInt32.Longitud = 4;
               loColInt32.Precision = 10;
               loColInt32.EsRequeridoBD = true;
               loColInt32.CampoId = 1;
               loColInt32.Descripcion = "Sin descripcion para ticket";
               loColInt32.EsIdentity = false;
               loColInt32.Tipo = typeof(Int32);
               AgregarPropiedad<Int32>("ticket", loColInt32);

               loColDateTimeN = new PropiedadesColumna<DateTime?>();
               loColDateTimeN.Valor = null;
               loColDateTimeN.EsPrimaryKey = false;
               loColDateTimeN.Longitud = 8;
               loColDateTimeN.Precision = 23;
               loColDateTimeN.EsRequeridoBD = false;
               loColDateTimeN.CampoId = 2;
               loColDateTimeN.Descripcion = "Sin descripcion para fecha";
               loColDateTimeN.EsIdentity = false;
               loColDateTimeN.Tipo = typeof(DateTime?);
               AgregarPropiedad<DateTime?>("fecha", loColDateTimeN);

               loColBooleanN = new PropiedadesColumna<Boolean?>();
               loColBooleanN.Valor = null;
               loColBooleanN.EsPrimaryKey = false;
               loColBooleanN.Longitud = 1;
               loColBooleanN.Precision = 1;
               loColBooleanN.EsRequeridoBD = false;
               loColBooleanN.CampoId = 3;
               loColBooleanN.Descripcion = "Sin descripcion para rol";
               loColBooleanN.EsIdentity = false;
               loColBooleanN.Tipo = typeof(Boolean?);
               AgregarPropiedad<Boolean?>("rol", loColBooleanN);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 250;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = false;
               loColstring.CampoId = 4;
               loColstring.Descripcion = "Sin descripcion para mensaje";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("mensaje", loColstring);

               loColBooleanN = new PropiedadesColumna<Boolean?>();
               loColBooleanN.Valor = null;
               loColBooleanN.EsPrimaryKey = false;
               loColBooleanN.Longitud = 1;
               loColBooleanN.Precision = 1;
               loColBooleanN.EsRequeridoBD = false;
               loColBooleanN.CampoId = 5;
               loColBooleanN.Descripcion = "Sin descripcion para status_mensaje";
               loColBooleanN.EsIdentity = false;
               loColBooleanN.Tipo = typeof(Boolean?);
               AgregarPropiedad<Boolean?>("status_mensaje", loColBooleanN);
          }
          #endregion

     }
}
