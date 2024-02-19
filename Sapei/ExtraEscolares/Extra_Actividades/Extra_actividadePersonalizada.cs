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
	/// Clase extra_actividade generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Extra_actividad
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
          public DataTable RegresaTablaActividades()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select id, (select descripcion from sisCombos where valor = E.tipo and combo = 'cboTipoActividades') tipo,");
               lsConsulta.Append("descripcion,(select paterno + ' ' + materno  + ' '+  nombre from extra_entrenador where id = id_entrenador) entrenador, capacidad, inscritos");
               lsConsulta.Append(" ,stuff(( ");
               lsConsulta.Append(" SELECT ', '+aula+' ' + B.dia + ' ' + B.hora_inicio + ' - ' + B.hora_fin ");
               lsConsulta.Append(" FROM extra_actividades_horarios B ");
               lsConsulta.Append(" WHERE E.id = B.id_actividad ");
               lsConsulta.Append(" FOR XML PATH('') ");
               lsConsulta.Append(" ), 1, 1, '') horario");
               lsConsulta.AppendFormat(" From {0} E", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}'", _oSistema.Sesion.Periodo.PeriodoActual);                
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }

          public int RegresaUltimoNuevoID()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select max(id)");
               lsConsulta.AppendFormat(" From {0} E", RutaTabla);
               return Convert.ToInt32(_oSistema.Conexion.EjecutaEscalar(lsConsulta));
          }

          public DataTable RegresaComboTipoActividades()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select valor, descripcion from sisCombos where  combo = 'cboTipoActividades'");
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public string EliminaActividad(int piId)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.AppendFormat("IF  EXISTS(SELECT 1 FROM extra_actividades_inscritos WHERE actividad = {0})",piId);
               lsConsulta.Append(" select 'Ya hay estudiantes registrados, no es posible eliminar'");
               lsConsulta.Append(" ELSE ");
               lsConsulta.Append("    BEGIN");
               lsConsulta.AppendFormat(" DELETE extra_actividades_horarios WHERE id_actividad = {0}", piId);
	          lsConsulta.AppendFormat(" DELETE extra_actividades WHERE id = {0}",piId);
		     lsConsulta.Append(" select 'Actividad Eliminada'");
		     lsConsulta.Append("    END");
               return Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsConsulta));
          }
          public DataTable RegresaTablaGruposInscritos(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT tipo, descripcion, capacidad, inscritos");
               lsConsulta.Append(",'<div class=\"col-md-1\"><span class=\"input-group-btn\"><a href=\"/DocumentosOficiales/RegistroParticipantes?psPeriodo='+periodo+'&amp;piIdActividad='+trim(convert(char(5),id))+'\" class=\"btn btn-info\" role=\"button\"><span class=\"fa fa-download\"></span></a></span></div>' descargar");
               lsConsulta.AppendFormat(" FROM {0} G", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaListaActividadesCargadas(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select  CASE capturado WHEN 0 THEN '<label class=\"fa fa-times-circle red\"></label>' ELSE '<label class=\"fa fa-check-circle-o blue\"></label>' END capturado, id, descripcion, inscritos");
               lsConsulta.Append(",'<div class=\"col-md-1\"><span class=\"input-group-btn\"><a href=\"/ReportesCSV/DescargaListaEstudiantesPorActividad?psPeriodo='+periodo+'&amp;psId='+TRIM(CONVERT(char(20),id))+'\" class=\"btn btn-info\" role=\"button\"><span class=\"fa fa-file-excel-o\"></span></a></span></div>' descargar,");
               lsConsulta.Append("case capturado when 1 then '<div class=\"col-md-1\"><span class=\"input-group-btn\"><a href=\"/DocumentosOficiales/ResultadosPorActividad?psPeriodo='+periodo+'&amp;piIdActividad='+TRIM(CONVERT(char(20),id))+'\" class=\"btn btn-info\" role=\"button\"><span class=\"fa fa-file-pdf-o\"></span></a></span></div>' else ' ' end resultados");
               lsConsulta.AppendFormat(" FROM {0} ", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}'", psPeriodo);
               if (_oSistema.Sesion.Usuario.RolUsuario == Framework.Configuracion.enmRolUsuario.INS)
                    lsConsulta.AppendFormat(" AND id_entrenador = {0}",_oSistema.Sesion.Usuario.Usuario);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public string GuardaRegistros(string psCadena)
          {
               DataTable loTabla;
               DataRow loFila;
               string[] lasRegistro;
               string lsCadena;
               int liFIla;

               StringBuilder lsErrores;
               loTabla = new DataTable();
               loTabla.Columns.Add("periodo");
               loTabla.Columns.Add("actividad");
               loTabla.Columns.Add("no_de_control");
               loTabla.Columns.Add("promedio");
               loTabla.Columns.Add("acredita");
               lsCadena = psCadena.Remove(0, psCadena.IndexOf("\n") + 1);
               lsErrores = new StringBuilder();
               liFIla = 0;
               this.periodo = _oSistema.Sesion.Periodo.PeriodoActual.Trim();
               this.id = Convert.ToInt32((lsCadena.Trim().Split(new char[2] { '\n', '\r' })[0]).Split(',')[0]);
               foreach (string lsRegistro in lsCadena.Trim().Split(new char[2] { '\n', '\r' }))
               {
                    loFila = loTabla.NewRow();
                    if (string.IsNullOrWhiteSpace(lsRegistro))
                         continue;
                    lasRegistro = lsRegistro.Trim().Split(',');
                    if (lasRegistro.Length != loTabla.Columns.Count-1)
                         return "El archivo no contiene el número de columnas requeridas en la fila " + Convert.ToString(liFIla);

                    if (this.id != Convert.ToInt32(lasRegistro[0]))
                    {
                         return "El archivo no contiene registros de la misma actividad en la fila " + Convert.ToString(liFIla);
                    }

                    loFila["periodo"] = this.periodo;
                    loFila["actividad"] = lasRegistro[0];
                    loFila["no_de_control"] = lasRegistro[1];
                    loFila["promedio"] = lasRegistro[3];
                    if(Convert.ToDouble(lasRegistro[3])<1)
                         loFila["acredita"] = false;
                    else
                         loFila["acredita"] = true;
                    loTabla.Rows.Add(loFila);
                    liFIla = liFIla + 1;
               }
               _oSistema.Conexion.EjecutaEscalar(new StringBuilder("TRUNCATE TABLE extra_actividades_pre_calificacion"));
               _oSistema.Conexion.InsertBulkCopy(loTabla, "extra_actividades_pre_calificacion");
               return null;
          }

          #endregion
     }
	}
