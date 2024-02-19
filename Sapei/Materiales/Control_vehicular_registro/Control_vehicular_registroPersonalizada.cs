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
	/// Clase control_vehicular_registro generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Control_Vehicular_Registro
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
		#endregion
          #region Consulta
          public DataTable RegresaDatosSolicitanteVehicular(string psUsuario, string psTiposUsuario, string psTipoVehiculo, bool pbConDescripcion = false)
          {
               StringBuilder lsConsulta;
               string lsNombre;
               string lsMarca = "marca";
               string lsSubMarca = "submarca";
               lsConsulta = new StringBuilder();
               if (pbConDescripcion)
               {
                    lsMarca = "(select descripcion from sisCombos where combo = 'cboMarcasVehiculos' and valor = marca) marca";
                    lsSubMarca = "(select descripcion from sisCombos where combo = 'cboSubMarcasVehiculos' and valor = submarca) submarca";
               }
               lsConsulta.Append(" declare @nombre varchar(100) ");
               if(psTiposUsuario == "D")
                    lsConsulta.AppendFormat("select @nombre = nombre_empleado + ' ' + apellido_paterno +' '+ apellido_materno from personal P where rfc = '{0}'",psUsuario);
               else
                    lsConsulta.AppendFormat("select @nombre = nombre_alumno + ' ' + apellido_paterno +' '+ apellido_materno from alumnos where no_de_control = '{0}' and no_de_control in (select no_de_control from seleccion_materias where periodo = '{1}' and no_de_control = '{0}')", psUsuario, _oSistema.Sesion.Periodo.PeriodoActual);
               lsConsulta.Append(" if (len(@nombre)>0) ");
               lsConsulta.Append(" begin ");
               lsConsulta.AppendFormat("  	if exists(select usuario from control_vehicular_registros where usuario = '{0}' and periodo = '{1}' and tipo_vehiculo = '{2}')",psUsuario,_oSistema.Sesion.Periodo.PeriodoActual, psTipoVehiculo);
               lsConsulta.AppendFormat(" select 	@nombre nombre,placas, tipo_vehiculo, {3}, {4}, color, modelo, tarjeton from control_vehicular_registros where usuario = '{0}' and periodo = '{1}' and tipo_vehiculo = '{2}'", psUsuario, _oSistema.Sesion.Periodo.PeriodoActual, psTipoVehiculo,lsMarca,lsSubMarca);

               lsConsulta.Append(" else select @nombre  end else select 'ERROR'");
         
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaDatosEstudianteSolicitanteVehicular(string psUsuario,  string psTipoVehiculo)
          {
               StringBuilder lsConsulta;
               DataTable loData;
               lsConsulta = new StringBuilder();
               lsConsulta.AppendFormat(" select 	 usuario,tipo_vehiculo,(select nombre_alumno + ' ' + apellido_paterno +' '+ apellido_materno from alumnos where no_de_control = '{0}')  nombre,placas, case tipo_vehiculo when 'AUT' then (select monto from monto_servicios where clave = 401) else (select monto from monto_servicios where clave = 402) end monto_pagar,(select monto from control_vehicular_pagos where periodo = R.periodo and usuario = R.usuario and tipo_vehiculo = R.tipo_vehiculo) monto, estado_registro from control_vehicular_registros R where usuario = '{0}' and periodo = '{1}' and tipo_vehiculo = '{2}' and estado_registro >= 2", psUsuario, _oSistema.Sesion.Periodo.PeriodoActual, psTipoVehiculo);
               loData = _oSistema.Conexion.RegresaDataTable(lsConsulta);
               if (loData.Rows.Count == 0)
                    return null;
               return loData;
          }
          public DataTable RegresaDatosSolicitanteVehicular(string psTarjeton)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.AppendFormat(" select 	usuario,placas,tipo_vehiculo from control_vehicular_registros where tarjeton = '{0}' and periodo = '{1}'", psTarjeton, _oSistema.Sesion.Periodo.PeriodoActual);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaRegistrosVehiculares()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               string lsMarca = "(select descripcion from sisCombos where combo = 'cboMarcasVehiculos' and valor = marca) marca";
               string lsSubMarca = "(select descripcion from sisCombos where combo = 'cboSubMarcasVehiculos' and valor = submarca) submarca";
               string lsEstatus = "(select descripcion from sisCombos where combo in ('cboEstadoProcesoRegistro','cboEstadoProcesoRegistroVehicular') and valor = estado_registro) estado";
               lsConsulta.AppendFormat(" select placas,{0},{1},{2} ", lsMarca,lsSubMarca,lsEstatus);
               lsConsulta.Append(",case estado_registro when 1 then 'Debe pasar a Recursos Materiales y presentar su tarjetón anterior y su tarjeta decirculación para validación'when 2 then 'Debe realizar el pago correspondiente y entregarlo en Recursos Financieros'when 3 then 'Debe esperar a que se le registre su tarjetón.'when 4 then 'Debe pasar a Recursos Materiales por su tarjetón.'when 5 then 'Ya le fue entregado su tarjetón. Proceso terminado' end");
               if (_oSistema.Sesion.Usuario.RolUsuario == Framework.Configuracion.enmRolUsuario.ALU)
               {
                    lsConsulta.Append(", (select case when (estado_registro < 2) then 'Para descargar su ficha de pago, primero debe validar su vehículo en Recursos Materiales' when (estado_registro = 2) then '<div class=\"col-md-1\"><span class=\"input-group-btn\"><a class=\"btn btn-info '+CASE TRIM(tipo_vehiculo) WHEN 'AUT' THEN 'AUTO' ELSE 'MOTO' END+'\" role=\"button\"><span class=\"fa fa-download\"></span></a></span></div>'else 'Usted ya ha realizado su pago. Gracias' end) descargar");
               }

               lsConsulta.AppendFormat(" FROM {0}",RutaTabla);
               lsConsulta.AppendFormat(" WHERE  periodo = '{0}' AND usuario = '{1}'",_oSistema.Sesion.Periodo.PeriodoActual,_oSistema.Sesion.Usuario.Usuario);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaDatosRegistros(string psPeriodo)
          {
               StringBuilder lsQuery = new StringBuilder();
               lsQuery.Append("select usuario,"); 
               lsQuery.Append("(select paterno +' '+ materno+' '+ nombre from estudiantes_datos_completos where no_de_control = usuario) nombre ,");
               lsQuery.Append("placas, tipo_vehiculo");
               lsQuery.AppendFormat(" from {0}",RutaTabla); 
               lsQuery.AppendFormat(" where periodo = '{0}' and usuario in (select no_de_control from seleccion_materias where periodo = '{0}')", psPeriodo);
               lsQuery.AppendFormat(" UNION ");
               lsQuery.Append(" select usuario, (select apellido_paterno +' '+ apellido_materno+' '+ nombre_empleado from personal where rfc = usuario) nombre , placas, tipo_vehiculo");
               lsQuery.AppendFormat(" from {0}", RutaTabla); 
               lsQuery.AppendFormat(" where periodo = '{0}' and usuario in (select rfc from personal)", psPeriodo);
               return _oSistema.Conexion.RegresaDataTable(lsQuery);
          }
          #endregion
		}
	}
