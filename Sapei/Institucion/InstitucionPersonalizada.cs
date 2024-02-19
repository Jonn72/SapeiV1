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
	/// Clase Institucion generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Institucion
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

          public string RegresaNombreDirector()
          {
               StringBuilder lsConsulta;
               string lsNombre;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select jefe_area");
               lsConsulta.Append(" from jefes");
               lsConsulta.AppendFormat(" where clave_area = '100000'");
               lsNombre = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsConsulta));
               return lsNombre;
          }
		#endregion

		}
	}
