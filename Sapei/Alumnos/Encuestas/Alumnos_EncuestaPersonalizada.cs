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
	/// Clase alumnos_encuesta generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Alumnos_Encuesta
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

          public DataTable RegresaDatosEncuestaCarrea(string psPeriodo)
          {
               StringBuilder lsQuery = new StringBuilder();
               lsQuery.Append(" select carrera, count(carrera) total from (");
               lsQuery.Append(" select (select carrera from alumnos where no_de_control = A.no_de_control) carrera");
               lsQuery.AppendFormat(" from {0} A", RutaTabla);
               lsQuery.AppendFormat(" WHERE periodo = '{0}' group by no_de_control) B", psPeriodo);
               lsQuery.Append(" group by carrera");
               return _oSistema.Conexion.RegresaDataTable(lsQuery);
          }
          public DataTable RegresaDatosGeneralesEncuesta(string psPeriodo)
          {
               StringBuilder lsQuery = new StringBuilder();
               lsQuery.Append(" select (select UPPER(nombre) from en_encuesta where id = A.id_encuesta ) encuesta,");
               lsQuery.Append(" SUBSTRING(CONVERT(char(2),id_pregunta),2,1) pregunta, case when (id_pregunta = 10) then id_respuesta else (select valor from en_respuesta where id = id_respuesta)end  respuesta");
               lsQuery.AppendFormat(" from {0} A",RutaTabla); 
               lsQuery.AppendFormat(" WHERE periodo = '{0}'",psPeriodo);
               lsQuery.Append(" order by id_encuesta");
               return _oSistema.Conexion.RegresaDataTable(lsQuery);
          }
		#endregion

		}
	}
