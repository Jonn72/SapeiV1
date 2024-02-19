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
	/// Clase acceso_torniquete generada automáticamente desde el Generador de Código SII
	/// </summary>
     public partial class Acceso_torniquete
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
          public string RegresaFechaUltimoRegistro(string psRegistro)
          {
               string[] lasRegistro;
               List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
               System.Data.SqlClient.SqlDataReader loReader;
               int liExisteRegistro = 1;
               int liTorniquete = 1;
               string lsUltimaFecha = "";
               using (var loConexion = new ManejaConexion(_oSistema.Conexion))
               {
                    lasRegistro = psRegistro.Trim().Split(new char[2] { ' ', '\t' });
                    loParametros.Add(new ParametrosSQL("@usuario", lasRegistro[0]));
                    loParametros.Add(new ParametrosSQL("@fecha", lasRegistro[1]));
                    loParametros.Add(new ParametrosSQL("@hora", lasRegistro[2]));

                    loReader = _oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_consulta_ultimo_registro ", loParametros);
                    if (loReader.HasRows)
                    {
                         while (loReader.Read())
                         {
                              liExisteRegistro = loReader.GetInt32(0);
                              liTorniquete = loReader.GetInt32(1);
                              lsUltimaFecha = loReader.GetString(2);
                         }
                    }
                    loReader.Close();
                    loReader.Dispose();
                    if (liExisteRegistro == 0)
                         return "1";
                    if (string.IsNullOrEmpty(lsUltimaFecha.Trim()))
                         return liTorniquete.ToString();
                    return liTorniquete + "|" + lsUltimaFecha;
               }

          }
          public void GuardaRegistros(string psCadena, int piTorniquete)
          {
               DataTable loTabla;
               DataRow loFila;
               string[] lasRegistro;
               loTabla = new DataTable();
               loTabla.Columns.Add("usuario");
               loTabla.Columns.Add("fecha");
               loTabla.Columns.Add("hora");
               loTabla.Columns.Add("torniquete");
               foreach (string lsRegistro in psCadena.Split('\n'))
               {
                    loFila = loTabla.NewRow();
                    lasRegistro = lsRegistro.Trim().Split(new char[2]{' ','\t'});
                    loFila["usuario"] = lasRegistro[0];
                    loFila["fecha"] = lasRegistro[1];
                    loFila["hora"] = lasRegistro[2];
                    loFila["torniquete"] = piTorniquete;
                    loTabla.Rows.Add(loFila);
               }
               _oSistema.Conexion.InsertBulkCopy(loTabla, NombreTabla);
          }
          public DataTable RegresaUltimosRegistros()
          {
               StringBuilder lsQuery;
               DataTable loDt;
               lsQuery = new StringBuilder();
               lsQuery.Append("SELECT top(7) fecha, count(hora) total");
               lsQuery.AppendFormat(" From [{0}].[{1}].[{2}] A", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, NombreTabla);
               lsQuery.Append(" group by  fecha");
               lsQuery.Append(" order by fecha desc");
               loDt = _oSistema.Conexion.RegresaDataTable(lsQuery);
               if (loDt.Rows.Count == 0)
                    return null;
               return loDt;
          }
          public DataTable RegresaRegistros(string psUsuario)
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.Append("SELECT fecha, hora");
               lsQuery.AppendFormat(" From [{0}].[{1}].[{2}] A", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, NombreTabla);
               lsQuery.AppendFormat("  where usuario = '{0}'",psUsuario);
               lsQuery.Append("  order by fecha asc");
               return _oSistema.Conexion.RegresaDataTable(lsQuery);
          }
          #endregion
     }
	}
