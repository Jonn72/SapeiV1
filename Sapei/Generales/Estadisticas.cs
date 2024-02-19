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
     public partial class Estadisticas
     {
          /// <summary>
          /// Variable del sistema
          /// </summary>
          protected internal Sistema _oSistema;
          /// <summary>
          /// Propietario de la talba (schema)
          /// </summary>
          public string Propietario { get; set; }

          #region Contructor

          /// <summary>
          /// Inicia una nueva instancia de la clase aspirante.
          /// </summary>
          /// <param name="poSistema">Clase del Sistema</param>
          public Estadisticas(Sistema poSistema)
          {
               _oSistema = poSistema;
               Propietario = "dbo";
          }
          #endregion
 
          #region Funciones

          #endregion

     }
}
