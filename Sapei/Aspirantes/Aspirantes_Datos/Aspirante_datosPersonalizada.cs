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
	public partial class Aspirante_datos
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
          #region Consultas
          public bool ValidaExistenciaCorreo(string psCorreo)
          {
               StringBuilder lsConsulta;
               string lsResltado;
               lsConsulta = new StringBuilder();
               lsConsulta.AppendFormat("Select 1 From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, NombreTabla);
               lsConsulta.AppendFormat(" WHERE correo = '{0}'", psCorreo);
               lsResltado = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsConsulta));
               //string.IsNullOrEmpty(lsResltado)
               if (lsResltado == "")
                    return false;
               else
                    return true;
          }
          #endregion
		}
	}
