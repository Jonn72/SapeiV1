using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Sapei.Framework;
using Sapei.Framework.Utilerias;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sapei
{
	/// <summary>
	/// Clase extra_entrenador generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Extra_entrenador
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
          public DataTable RegresaDatosEntrenador(string psUsuario)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select [id],[nombre],[paterno],[materno]");
               lsConsulta.AppendFormat(" From {0}", RutaTabla);
               lsConsulta.AppendFormat(" WHERE usuario = '{0}'",psUsuario);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public List<string> RegresaPermisosFuncionRol(Sapei.Framework.Configuracion.enmRolUsuario enumRol)
          {
               StringBuilder lsQuery;
               List<string> loLista;
               DataTable loDatos;
               lsQuery = new StringBuilder();
               loLista = new List<string>();
               lsQuery.AppendFormat("Select clave From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "sis_menus_permisos_rol");
               lsQuery.Append(" where");
               lsQuery.AppendFormat(" tipo_usuario= ('{0}')", enumRol.ToString());
               loDatos = _oSistema.Conexion.RegresaDataTable(lsQuery);
               foreach (DataRow loRow in loDatos.Rows)
               {
                    loLista.Add(loRow.RegresaValor<string>("clave").Trim());
               }
               return loLista;
          }
          public DataTable RegresaTablaEntrenadores(string psCombo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select [id],[nombre],[paterno],[materno],CASE [estatus] WHEN 1 THEN 'ACTIVO' ELSE 'INACTIVO' END, usuario, contrasenia");
               lsConsulta.AppendFormat(" From {0}", RutaTabla);
               lsConsulta.Append(" WHERE id > 0");
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaComboEntrenadores()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select [id],([paterno]+' '+[materno]+' '+[nombre]) nombre ");
               lsConsulta.AppendFormat(" From {0}", RutaTabla);
               lsConsulta.Append(" WHERE estatus = 1");
               lsConsulta.Append(" ORDER BY paterno");
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }
          public DataTable RegresaMaximoID()
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("SELECT isnull(max(id)+1,1)");
               lsConsulta.AppendFormat(" From {0}", RutaTabla);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }

          #endregion
		}
	}
