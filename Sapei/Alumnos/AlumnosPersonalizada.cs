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
	/// Clase alumnos generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Alumnos
	{
		#region variables
		#endregion
		#region Propiedades
		#endregion
		#region Contructor
		#endregion
		#region Metodos/Funciones
          //public DataTable RegresaBusquedaGeneral(string psValor, int piTipo)
          //{ 
          //     StringBuilder lsQuery;
          //     string lsFiltro;
          //     lsQuery = new StringBuilder();
          //     lsQuery.Append("Select [no_de_control],[apellido_paterno] + ' ' + [apellido_materno]+ ' ' + [nombre_alumno]");
          //     lsQuery.Append(" ,[carrera],[semestre],[sexo],[estatus_alumno]");
          //     lsQuery.AppendFormat(" From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, NombreTabla);
          //     lsQuery.Append(" where ");
          //     switch (piTipo)
          //     {
          //          case 1: lsFiltro = string.Format("no_de_control = '{0}'", psValor);
          //               break;
          //          case 2: lsFiltro = string.Format("UPPER([apellido_paterno] + ' ' + [apellido_materno]) like '%{0}%'", psValor.ToUpper());
          //               break;
          //          case 3: lsFiltro = string.Format("UPPER([nombre_alumno]) like '%{0}%'", psValor.ToUpper());
          //               break;
          //          case 4: lsFiltro = string.Format("UPPER([carrera]) like '%{0}%'", psValor.ToUpper());
          //               break;
          //          case 5: lsFiltro = string.Format("no_de_control = '{0}'", psValor);
          //               break;
          //     }


          //     lsQuery.AppendFormat(" no_de_control= ('{0}')", psNoControl);
          //     lsResultado = Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
          //     if (string.IsNullOrEmpty(lsResultado))
          //          return false;
          //     return true;
          //}
		#endregion

		}
	}
