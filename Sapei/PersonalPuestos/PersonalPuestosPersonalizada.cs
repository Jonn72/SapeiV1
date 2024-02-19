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
using Sapei.Framework.BaseDatos;

namespace Sapei
{
	/// <summary>
	/// Clase personal_puesto generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class PersonalPuestos
	{

		#region variables
		private List<ParametrosSQL> _oSqlParametros;
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

		public DataTable RegresaPersonalPuestoRFC(string psRFC)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			_oSqlParametros = new List<ParametrosSQL>();
			_oSqlParametros.Add(new ParametrosSQL("@rfc", psRFC));
			lsConsulta.Append(" select p.descripcion_puesto, convert (varchar,pp.fecha_ingreso_puesto,7) as fecha_ingreso_puesto, convert(varchar,fecha_termino_puesto,7) as fecha_termino,  ");
			lsConsulta.Append(" '<button type=\"button\"  onclick =\"eliminapuesto('''+ pp.rfc +''', ''' + convert(varchar,pp.clave_puesto) + ''');\" class=\"btn btn-danger\"><i class=\"fa fa-times-circle\"></i></button>' as boton ");
			lsConsulta.Append(" from personal_puestos pp, puestos p where p.clave_puesto = pp.clave_puesto and pp.fecha_termino_puesto is null and rfc = @rfc ");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta, _oSqlParametros);
		}

	}
	}
