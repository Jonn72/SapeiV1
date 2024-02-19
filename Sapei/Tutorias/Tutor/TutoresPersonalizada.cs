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
	/// Clase tutorias_tutore generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Tutores
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

          public DataTable RegresaTablaTutores()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select rfc,(select apellido_paterno+' '+ apellido_materno +' '+ nombre_empleado from personal where rfc = T.rfc),fecha_registro,CASE [estatus] WHEN 1 THEN 'ACTIVO' ELSE 'INACTIVO' END estatus");
               lsConsulta.AppendFormat(" From {0} T", RutaTabla);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaComboTutores()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select rfc,(select apellido_paterno+' '+ apellido_materno +' '+ nombre_empleado from personal where rfc = T.rfc)");
               lsConsulta.AppendFormat(" From {0} T", RutaTabla);
               lsConsulta.Append(" WHERE estatus = 1");
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
		#endregion

		}
	}
