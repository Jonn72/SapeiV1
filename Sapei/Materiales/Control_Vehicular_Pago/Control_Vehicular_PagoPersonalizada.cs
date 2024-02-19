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
     /// Clase control_vehicular_pago generada automáticamente desde el Generador de Código SII
     /// </summary>
     public partial class Control_Vehicular_Pago
     {
          #region variables
          #endregion
          #region Propiedades
          #endregion
          #region Contructor
          #endregion
          #region Metodos/Funciones
          /// <summary>
          /// Funcion para personalizar la validacion para un nuevo registro
          /// </summary>
          protected override void ValidacionNuevoPersonalizada()
          {
          }
          /// <summary>
          /// Funcion para personalizar el grabar en una los registros
          /// </summary>
          protected override void ValidacionGrabarPersonalizada()
          {
          }
          /// <summary>
          /// Funcion para personalizar la validacion de cambios en los registros
          /// </summary>
          protected override void ValidacionCambiosGrabarPersonalizada()
          {
          }
          /// <summary>
          /// Funcion para personalizar al eliminar los registros
          /// </summary>
          protected override void ValidacionEliminarPersonalizada()
          {
          }

          public DataTable RegresaDatosFichaPago(string psTipo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select (select identificacion_corta from periodos_escolares where periodo = C.periodo) periodo,");
               lsConsulta.Append(" C.usuario control ,(select apellido_paterno +' '+ apellido_materno +' '+ nombre_alumno from alumnos where trim(no_de_control) = trim(C.usuario)) usuario,");
               lsConsulta.Append(" case C.tipo_vehiculo when 'AUT' then 'AUTOMÓVIL' else 'MOTOCICLETA' end tipo, ");
               lsConsulta.Append(" (select descripcion from sisCombos where valor = marca and combo = 'cboMarcasVehiculos') marca,");
               lsConsulta.Append(" (select descripcion from sisCombos where valor = submarca and combo = 'cboSubMarcasVehiculos') submarca, placas, modelo");
               lsConsulta.Append(",case C.tipo_vehiculo when 'AUT' then (select convert(int,monto) from monto_servicios where clave = 401) 	else (select convert(int,monto) from monto_servicios where clave = 402) end monto");
               lsConsulta.Append(",qr,cadena");
               lsConsulta.Append(" from control_vehicular_registros C  ");
               lsConsulta.AppendFormat("where C.periodo = '{0}' AND C.usuario = '{1}' AND C.tipo_vehiculo = '{2}'", _oSistema.Sesion.Periodo.PeriodoActual, _oSistema.Sesion.Usuario.Usuario, psTipo);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          #endregion
     }
}
