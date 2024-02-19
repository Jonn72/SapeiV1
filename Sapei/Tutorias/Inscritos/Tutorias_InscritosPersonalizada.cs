using System;
using System.Text;
using System.Data;
using Sapei.Framework.Utilerias;

namespace Sapei
{
	/// <summary>
	/// Clase tutorias_inscrito generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Tutorias_Inscritos
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
		public DataTable RegresaTablaEstudiantesIncritos(string psPeriodo)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("SELECT  [periodo],grupo,[no_de_control] ");
			lsConsulta.Append(",(select apellido_paterno +' '+apellido_materno+' '+nombre_alumno from alumnos where no_de_control = T.no_de_control) nombre");
			lsConsulta.Append(",(select carrera from alumnos where no_de_control = T.no_de_control) carrera,");
			lsConsulta.Append("semestre,fecha_registro,fecha_termino,promedio,");
			lsConsulta.Append("case when (4 >= promedio and promedio >= 3.5) then 'EXCELENTE' when (3.5 > promedio and promedio >= 2.5) then 'NOTABLE' when (2.5 > promedio and promedio >= 1.5) then 'BUENO' when (1.5 > promedio and promedio >= 1) then 'SUFICIENTE'  end desempeño,");
			lsConsulta.Append("(select apellido_paterno +' '+ apellido_materno +' '+ nombre_empleado from personal where rfc = (select rfc from tutorias_grupos where periodo = T.periodo and grupo = T.grupo)) entrenador ");
			
			lsConsulta.AppendFormat(" FROM {0} T", RutaTabla);
			lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);

			return _oSistema.Conexion.RegresaDataTable(lsConsulta);

		}
		public DataTable RegresaTablaEstudiantesIncritos(string psNoControl, bool pbAprobado = false)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("SELECT  [periodo],grupo,[no_de_control] ");
			lsConsulta.Append(",(select apellido_paterno +' '+apellido_materno+' '+nombre_alumno from alumnos where no_de_control = T.no_de_control) nombre");
			lsConsulta.Append(",(select (select distinct UPPER(nombre_carrera) from carreras where carrera = A.carrera) from alumnos A where no_de_control = T.no_de_control) carrera,");
			lsConsulta.Append("semestre,fecha_registro,fecha_termino,promedio,");
			lsConsulta.Append("case when (4 >= promedio and promedio >= 3.5) then 'EXCELENTE' when (3.5 > promedio and promedio >= 2.5) then 'NOTABLE' when (2.5 > promedio and promedio >= 1.5) then 'BUENO' when (1.5 > promedio and promedio >= 1) then 'SUFICIENTE'  end desempeño,");
			lsConsulta.Append("(select apellido_paterno +' '+ apellido_materno +' '+ nombre_empleado from personal where rfc = (select rfc from tutorias_grupos where periodo = T.periodo and grupo = T.grupo)) entrenador ");

			lsConsulta.AppendFormat(" FROM {0} T", RutaTabla);
			lsConsulta.AppendFormat(" WHERE no_de_control = '{0}'", psNoControl);
			if(pbAprobado)
				lsConsulta.AppendFormat(" AND promedio >= 1");
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);

		}
		public DataTable RegresaTablaEstudiantesIncritos(string psPeriodo, string psNoControl = null, string psGrupo = null, bool pbIncluyeFolio = false)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
			string lsDatos;
			string lsCadenaQR;
			byte[] lbysQR;
			DataTable loDt;
               lsConsulta.Append("SELECT (select identificacion_corta from periodos_escolares where periodo = T.periodo) periodo ,grupo,[no_de_control] ");
               lsConsulta.Append(",(select apellido_paterno +' '+apellido_materno+' '+nombre_alumno from alumnos where no_de_control = T.no_de_control) nombre,");
               lsConsulta.Append("(select (select distinct UPPER(nombre_carrera) from carreras where carrera = A.carrera) from alumnos A where no_de_control = T.no_de_control) carrera,");
               lsConsulta.Append("'TUTORIAS' descripcion,semestre,fecha_registro,fecha_termino,promedio,");
               lsConsulta.Append("case when (4 >= promedio and promedio >= 3.5) then 'EXCELENTE' when (3.5 > promedio and promedio >= 2.5) then 'NOTABLE' when (2.5 > promedio and promedio >= 1.5) then 'BUENO' when (1.5 > promedio and promedio >= 1) then 'SUFICIENTE'  end desempeño,");
               lsConsulta.Append("(select apellido_paterno +' '+ apellido_materno +' '+ nombre_empleado from personal where rfc = (select rfc from tutorias_grupos where periodo = T.periodo and grupo = T.grupo)) entrenador ");
			if (pbIncluyeFolio)
			{
				lsConsulta.Append(",(select folio from tutorias_folios where no_de_control = T.no_de_control) as folio");
				lsConsulta.Append(",(select fecha_registro from tutorias_folios where no_de_control = T.no_de_control) as folio_fecha");
			}
               lsConsulta.AppendFormat(" FROM {0} T",RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               if(!string.IsNullOrEmpty(psNoControl))
                    lsConsulta.AppendFormat(" AND no_de_control = '{0}'", psNoControl);
               if (!string.IsNullOrEmpty(psGrupo))
                    lsConsulta.AppendFormat(" AND grupo = '{0}' AND promedio >= 1", psGrupo);
               loDt = _oSistema.Conexion.RegresaDataTable(lsConsulta);

			loDt.Columns.Add("qr", typeof(byte[]));
			foreach (DataRow loRow in loDt.Rows)
			{
				lsDatos = string.Format("Fecha de emisión:{0}&Periodo:{1}&No Control:{2}&Nombre:{3}&Credito:{5}&Promedio:{6}&Folio:{6}",
				Convert.ToDateTime(loRow["folio_fecha"]).ToString("yyyy/MM/dd"), loRow["periodo"], loRow["no_de_control"], loRow["nombre"], "TUTORÍA", loRow["desempeño"], loRow["folio"]);
				lsCadenaQR = lsDatos.MD5HASH();
				loRow["qr"] = lsCadenaQR.RegresaQRValidacionDocumentos();
				_oSistema.GrabaValidacionDocumento(lsCadenaQR, lsDatos);
			}
			return loDt;
		}
		public DataTable RegresaListaInscritosPorGrupo(string psPeriodo, string psGrupo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT  grupo ,[no_de_control] control, (select  apellido_paterno +' '+ apellido_materno + ' '+ nombre_alumno from alumnos where no_de_control = C.no_de_control) as nombre,");
			lsConsulta.Append("(select usuario from alumnos_cuentas where no_de_control = C.no_de_control and tipo_cuenta = 1) 'correo',promedio");

			lsConsulta.Append(",case (select rfc from tutorias_grupos where periodo = C.periodo and grupo = C.grupo) when '0' then 'SIN TUTOR' else  (select UPPER(nombre_empleado + ' ' + apellido_paterno +' '+ apellido_materno) from personal where rfc = (select rfc from tutorias_grupos where periodo = C.periodo and grupo = C.grupo)) end tutor");
			   lsConsulta.AppendFormat(" From {0} C", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               lsConsulta.AppendFormat(" AND grupo = '{0}'", psGrupo);
               lsConsulta.Append(" ORDER BY nombre");
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public string GuardaRegistrosTutorias(string psCadena)
          {
               DataTable loTabla;
               DataRow loFila;
               string[] lasRegistro;
               string lsCadena;
               int liFIla;

               StringBuilder lsErrores;
               loTabla = new DataTable();
               loTabla.Columns.Add("grupo");
               loTabla.Columns.Add("no_de_control");
               loTabla.Columns.Add("promedio");
               lsCadena = psCadena.Remove(0, psCadena.IndexOf("\n") + 1);
               lsErrores = new StringBuilder();
               liFIla = 0;
			this.periodo = _oSistema.Sesion.Periodo.PeriodoActual;
               this.grupo = (lsCadena.Trim().Split(new char[2] { '\n', '\r' })[0]).Split(',')[0];
               foreach (string lsRegistro in lsCadena.Trim().Split(new char[2] { '\n', '\r' }))
               {
                    loFila = loTabla.NewRow();
                    if (string.IsNullOrWhiteSpace(lsRegistro))
                         continue;
                    lasRegistro = lsRegistro.Trim().Split(',');
                    if (lasRegistro.Length != loTabla.Columns.Count + 1)
                         return "El archivo no contiene el número de columnas requeridas en la fila " + Convert.ToString(liFIla);

                    if (this.grupo != lasRegistro[0])
                    {
                         return "El archivo no contiene registros del mismo grupo en la fila " + Convert.ToString(liFIla);
                    }
                    loFila["grupo"] = lasRegistro[0];
                    loFila["no_de_control"] = lasRegistro[1];
                    loFila["promedio"] = lasRegistro[3];

                    loTabla.Rows.Add(loFila);
                    liFIla = liFIla + 1;
               }
               _oSistema.Conexion.EjecutaEscalar(new StringBuilder("TRUNCATE TABLE tutorias_pre_calificacion"));
               _oSistema.Conexion.InsertBulkCopy(loTabla, "tutorias_pre_calificacion");
               return null;
          }
          public string GeneraFolioLiberacion(string psPeriodo,string psNoControl)
          {
               StringBuilder lsQuery = new StringBuilder();               
               lsQuery.AppendFormat("IF EXISTS (SELECT 1 FROM tutorias_folios WHERE no_de_control = '{0}')",psNoControl);
               lsQuery.AppendFormat(" SELECT folio FROM tutorias_folios WHERE no_de_control = '{0}'",psNoControl);
               lsQuery.Append(" ELSE ");
               lsQuery.Append(" BEGIN ");
               lsQuery.Append(" DECLARE @valor INT ");
               lsQuery.AppendFormat(" SELECT @valor = ISNULL(MAX(folio),0) + 1 FROM tutorias_folios WHERE periodo = '{0}'",psPeriodo);
               lsQuery.Append("INSERT INTO tutorias_folios (periodo, no_de_control, folio, fecha_registro)");
               lsQuery.AppendFormat(" values ('{0}','{1}',@valor,getdate())",psPeriodo,psNoControl);
               lsQuery.Append(" SELECT @valor");
               lsQuery.Append(" END ");
               return Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
          }

          public void GeneraFolioLiberacionGrupo(string psPeriodo, string psGrupo)
          {
               StringBuilder lsQuery = new StringBuilder();
               lsQuery.Append("insert into tutorias_folios(periodo,no_de_control,folio,fecha_registro)");
               lsQuery.Append("select periodo, no_de_control,");
               lsQuery.Append("(SELECT ISNULL(MAX(folio),0) FROM tutorias_folios WHERE periodo = E.periodo)");
               lsQuery.Append("+ROW_NUMBER() over (order by no_de_control),GETDATE() from tutorias_inscritos E");
               lsQuery.AppendFormat(" where grupo = '{0}' ",psGrupo);
               lsQuery.AppendFormat(" AND no_de_control not in (select no_de_control from tutorias_folios)", psPeriodo);
			lsQuery.AppendFormat(" AND promedio >= 1");
               _oSistema.Conexion.EjecutaEscalar(lsQuery);
          }
		public string RegresaPeriodoGrupoVacio()
		{
			StringBuilder lsQuery = new StringBuilder();
			lsQuery.Append(" SELECT periodo FROM tutorias_grupos WHERE grupo = 'TUT'");
			return Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
		}

		#endregion

		}
	}
