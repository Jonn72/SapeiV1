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
     public partial class ServiciosEscolares
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
          public ServiciosEscolares(Sistema poSistema)
          {
               _oSistema = poSistema;
               Propietario = "dbo";
          }
          #endregion

          #region Funciones
          public DataTable CargarAspirantes2Estudiantes(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select");
               lsConsulta.Append(" folio, vuelta, paterno + ' ' + materno + ' ' + nombre nombre, carrera1 carrera,");
               lsConsulta.Append(" isnull((select no_de_control from aspirantes where folio = A.folio),'') no_de_control, ");
                lsConsulta.Append(" isnull((select nip from alumnos where no_de_control = A.no_de_control),'') nip ");

            lsConsulta.AppendFormat(" From [{0}].[{1}].[{2}] A", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "aspirantes_datos_completos");
               lsConsulta.AppendFormat(" where estatusAspirante = 3 and periodo = '{0}'",psPeriodo);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);

          }
          #endregion

     }
}
