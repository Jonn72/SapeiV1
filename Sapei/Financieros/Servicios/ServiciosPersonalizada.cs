
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sapei
{
	/// <summary>
	/// Clase servicios generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Servicios
	{
		#region variables
		#endregion
		#region Propiedades
		#endregion
		#region Contructor
		#endregion
		#region Metodos/Funciones
		public DataTable CargarLista()
		{
			StringBuilder lsQuery;
			lsQuery = new StringBuilder();
			lsQuery.Append(" SELECT clave Clave, concepto Concepto, monto Monto, dias_vigencia Vigencia, case activo when 1 then 'SI' else 'NO' end Activo");
			lsQuery.Append(",'<a class=\"btn btn-primary btn-xs\"><i class=\"fa fa-pencil\" ></i></a>' Editar");
			lsQuery.AppendFormat(" FROM {0}", _oCatalogo.NombreTabla);
			return _oSistema.Conexion.RegresaDataTable(lsQuery);
		}
		#endregion

	}
	}
