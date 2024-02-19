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
     /// Clase aspirantes_dato generada automáticamente desde el Generador de Código SII
     /// </summary>
     [Serializable]
     public partial class Aspirante_datos : CatalogoFijoElemento
     {
          #region Contructor
          /// <summary>
          /// Inicia una nueva instancia de la clase aspirantes_dato.
          /// </summary>
          public Aspirante_datos()
               : base()
          {
               NombreTabla = "aspirantes_datos";
               Propietario = "dbo";
               CargaPropiedadesdeColumna();

          }
          /// <summary>
          /// Inicia una nueva instancia de la clase aspirantes_dato.
          /// </summary>
          /// <param name="poSistema">Clase del Sistema</param>
          public Aspirante_datos(Sistema poSistema)
               : base(poSistema)
          {
               NombreTabla = "aspirantes_datos";
               Propietario = "dbo";
               CargaPropiedadesdeColumna();

          }
          #endregion
          #region Propiedades
          /// <summary>
          /// Obtiene o establece fechaNacimiento.fechaNacimiento 
          /// </summary>
          /// <value>
          /// fechaNacimiento 
          /// </value>
          [Required]
          [MaxLength(3)]
          [DefaultValue(null)]
          public DateTime fechaNacimiento
          {
               get
               {
                    return ObtenerValorPropiedad<DateTime>("fechaNacimiento");
               }

               set
               {
                    EstablecerValorPropiedad<DateTime>("fechaNacimiento", value);
               }

          }
          /// <summary>
          /// Obtiene o establece entidad_nacimiento.entidad_nacimiento 
          /// </summary>
          /// <value>
          /// entidad_nacimiento 
          /// </value>
          [Required]
          public Int32 entidad_nacimiento
          {
               get
               {
                    return ObtenerValorPropiedad<Int32>("entidad_nacimiento");
               }

               set
               {
                    EstablecerValorPropiedad<Int32>("entidad_nacimiento", value);
               }

          }
          /// <summary>
          /// Obtiene o establece estado_civil.estado_civil 
          /// </summary>
          /// <value>
          /// estado_civil 
          /// </value>
          [Required]
          public string estado_civil
          {
               get
               {
                    return ObtenerValorPropiedad<string>("estado_civil");
               }

               set
               {
                    EstablecerValorPropiedad<string>("estado_civil", value);
               }

          }
          /// <summary>
          /// Obtiene o establece foto.foto 
          /// </summary>
          /// <value>
          /// foto 
          /// </value>
          [DefaultValue(null)]
          public Byte[] foto
          {
               get
               {
                    return ObtenerValorPropiedad<Byte[]>("foto");
               }

               set
               {
                    EstablecerValorPropiedad<Byte[]>("foto", value);
               }

          }
          /// <summary>
          /// Obtiene o establece nombre.nombre 
          /// </summary>
          /// <value>
          /// nombre 
          /// </value>
          [Required]
          [MaxLength(100)]
          [DefaultValue(null)]
          public string nombre
          {
               get
               {
                    return ObtenerValorPropiedad<string>("nombre");
               }

               set
               {
                    EstablecerValorPropiedad<string>("nombre", value);
               }

          }
          /// <summary>
          /// Obtiene o establece paterno.paterno 
          /// </summary>
          /// <value>
          /// paterno 
          /// </value>
          [Required]
          [MaxLength(45)]
          [DefaultValue(null)]
          public string paterno
          {
               get
               {
                    return ObtenerValorPropiedad<string>("paterno");
               }

               set
               {
                    EstablecerValorPropiedad<string>("paterno", value);
               }

          }
          /// <summary>
          /// Obtiene o establece materno.materno 
          /// </summary>
          /// <value>
          /// materno 
          /// </value>
          [Required]
          [MaxLength(45)]
          [DefaultValue(null)]
          public string materno
          {
               get
               {
                    return ObtenerValorPropiedad<string>("materno");
               }

               set
               {
                    EstablecerValorPropiedad<string>("materno", value);
               }

          }
          /// <summary>
          /// Obtiene o establece correo.correo 
          /// </summary>
          /// <value>
          /// correo 
          /// </value>
          [MaxLength(60)]
          [DefaultValue(null)]
          public string correo
          {
               get
               {
                    return ObtenerValorPropiedad<string>("correo");
               }

               set
               {
                    EstablecerValorPropiedad<string>("correo", value);
               }

          }
          /// <summary>
          /// Obtiene o establece folio.folio 
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
          /// Obtiene o establece curp.curp 
          /// </summary>
          /// <value>
          /// curp 
          /// </value>
          [Required]
          [MaxLength(18)]
          [DefaultValue(null)]
          public string curp
          {
               get
               {
                    return ObtenerValorPropiedad<string>("curp");
               }

               set
               {
                    EstablecerValorPropiedad<string>("curp", value);
               }

          }
          /// <summary>
          /// Obtiene o establece sexo.sexo 
          /// </summary>
          /// <value>
          /// sexo 
          /// </value>
          [Required]
          [MaxLength(1)]
          [DefaultValue(null)]
          public string sexo
          {
               get
               {
                    return ObtenerValorPropiedad<string>("sexo");
               }

               set
               {
                    EstablecerValorPropiedad<string>("sexo", value);
               }

          }
          /// <summary>
          /// Obtiene o establece telefonoCasa.telefonoCasa 
          /// </summary>
          /// <value>
          /// telefonoCasa 
          /// </value>
          [Required]
          [MaxLength(10)]
          [DefaultValue(null)]
          public string telefonoCasa
          {
               get
               {
                    return ObtenerValorPropiedad<string>("telefonoCasa");
               }

               set
               {
                    EstablecerValorPropiedad<string>("telefonoCasa", value);
               }

          }
          /// <summary>
          /// Obtiene o establece telefonoEmergencia.telefonoEmergencia 
          /// </summary>
          /// <value>
          /// telefonoEmergencia 
          /// </value>
          [Required]
          [MaxLength(10)]
          [DefaultValue(null)]
          public string telefonoEmergencia
          {
               get
               {
                    return ObtenerValorPropiedad<string>("telefonoEmergencia");
               }

               set
               {
                    EstablecerValorPropiedad<string>("telefonoEmergencia", value);
               }

          }
          /// <summary>
          /// Obtiene o establece celular.celular 
          /// </summary>
          /// <value>
          /// celular 
          /// </value>
          [MaxLength(10)]
          [DefaultValue(null)]
          public string celular
          {
               get
               {
                    return ObtenerValorPropiedad<string>("celular");
               }

               set
               {
                    EstablecerValorPropiedad<string>("celular", value);
               }

          }
          /// <summary>
          /// Obtiene o establece nss.nss 
          /// </summary>
          /// <value>
          /// nss 
          /// </value>
          [MaxLength(11)]
          [DefaultValue(null)]
          public string nss
          {
               get
               {
                    return ObtenerValorPropiedad<string>("nss");
               }

               set
               {
                    EstablecerValorPropiedad<string>("nss", value);
               }

          }
          #endregion
          #region Funciones
          /// <summary>
          /// Carga un registro especifico denotado por las llaves de la tabla aspirantes_dato.		/// </summary>
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
               PropiedadesColumna<Int32> loColInt32;
               PropiedadesColumna<Byte[]> loColByteA;
               PropiedadesColumna<DateTime> loDateTime;

               if (!Object.Equals(CamposLlave, null) && !Object.Equals(Propiedades, null))
                    return;

               CamposLlave = new Dictionary<string, object>(1);
               Propiedades = new Dictionary<string, Propiedad>(15);

               AgregaCampoLlave("folio", null);

               loDateTime = new PropiedadesColumna<DateTime>();
               loDateTime.EsPrimaryKey = false;
               loDateTime.Longitud = 3;
               loDateTime.Precision = 10;
               loDateTime.EsRequeridoBD = true;
               loDateTime.CampoId = 7;
               loDateTime.Descripcion = "fechaNacimiento";
               loDateTime.EsIdentity = false;
               loDateTime.Tipo = typeof(DateTime);
               AgregarPropiedad<DateTime>("fechaNacimiento", loDateTime);

               loColInt32 = new PropiedadesColumna<Int32>();
               loColInt32.EsPrimaryKey = false;
               loColInt32.Longitud = 4;
               loColInt32.Precision = 10;
               loColInt32.EsRequeridoBD = true;
               loColInt32.CampoId = 7;
               loColInt32.Descripcion = "entidad_nacimiento";
               loColInt32.EsIdentity = false;
               loColInt32.Tipo = typeof(Int32);
               AgregarPropiedad<Int32>("entidad_nacimiento", loColInt32);

               loColstring = new PropiedadesColumna<String>();
               loColstring.EsPrimaryKey = false;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 8;
               loColstring.Descripcion = "estado_civil";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(String);
               AgregarPropiedad<String>("estado_civil", loColstring);

               loColByteA = new PropiedadesColumna<Byte[]>();
               loColByteA.Valor = null;
               loColByteA.EsPrimaryKey = false;
               loColByteA.Longitud = -1;
               loColByteA.Precision = 0;
               loColByteA.EsRequeridoBD = false;
               loColByteA.CampoId = 13;
               loColByteA.Descripcion = "foto";
               loColByteA.EsIdentity = false;
               loColByteA.Tipo = typeof(Byte[]);
               AgregarPropiedad<Byte[]>("foto", loColByteA);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 100;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 1;
               loColstring.Descripcion = "nombre";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("nombre", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 45;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 2;
               loColstring.Descripcion = "paterno";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("paterno", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 45;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 3;
               loColstring.Descripcion = "materno";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("materno", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 60;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = false;
               loColstring.CampoId = 9;
               loColstring.Descripcion = "correo";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("correo", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = true;
               loColstring.Longitud = 8;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 0;
               loColstring.Descripcion = "folio";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("folio", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 18;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 4;
               loColstring.Descripcion = "curp";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("curp", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 1;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 5;
               loColstring.Descripcion = "sexo";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("sexo", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 10;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 10;
               loColstring.Descripcion = "telefonoCasa";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("telefonoCasa", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 10;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = true;
               loColstring.CampoId = 11;
               loColstring.Descripcion = "telefonoEmergencia";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("telefonoEmergencia", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 10;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = false;
               loColstring.CampoId = 12;
               loColstring.Descripcion = "celular";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("celular", loColstring);

               loColstring = new PropiedadesColumna<string>();
               loColstring.Valor = null;
               loColstring.EsPrimaryKey = false;
               loColstring.Longitud = 11;
               loColstring.Precision = 0;
               loColstring.EsRequeridoBD = false;
               loColstring.CampoId = 14;
               loColstring.Descripcion = "nss";
               loColstring.EsIdentity = false;
               loColstring.Tipo = typeof(string);
               AgregarPropiedad<string>("nss", loColstring); 
          }
          #endregion

     }
}
