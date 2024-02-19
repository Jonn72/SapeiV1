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
	/// Clase ss_activar_curso generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class SS_Activar_Curso
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
        public DataTable CursosSS()
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append(" select periodo_curso,(select identificacion_corta from periodos_escolares where periodo = P.periodo_curso),nombre,url ");
            lsQuery.AppendFormat("from {0} P", RutaTabla);    
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
        public DataTable ConsultaCurso(string lsPeriodo)
        {
            StringBuilder lsQuery = new StringBuilder();
            lsQuery.Append(" select periodo_curso,(select identificacion_corta from periodos_escolares where periodo = P.periodo_curso),nombre,url ");
            lsQuery.AppendFormat("from {0} P", RutaTabla);
            return _oSistema.Conexion.RegresaDataTable(lsQuery);
        }
    }
	}
