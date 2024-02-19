using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sapei.Framework.Utilerias;

namespace Sapei
{
     public partial class Acceso
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

          #region "Metodos"
          /// <summary>
          /// Funcion que regresa la usuclave del usuario segun el correo y la contraseña
          /// </summary>
          /// <param name="psUsuarioCorreo">Correo de usuario</param>
          /// <param name="psContraseña">Contraseña del usuario</param>
          /// <returns></returns>
          public string RegresaClaveUsuario(string psUsuario, string psContraseña)
          {
               StringBuilder lsQuery;
               lsQuery = new StringBuilder();
               lsQuery.AppendFormat("Select top(1) usuClave From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, NombreTabla);
               lsQuery.Append(" where");
               lsQuery.AppendFormat(" usuUsuario= ('{0}')", psUsuario);
               lsQuery.AppendFormat(" and usuPasword =('{0}')", psContraseña);
               lsQuery.Append(" and UsuStatus = 0");
               return Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery));
          }
          public List<string> RegresaPermisosUsuario(string psUsuario)
          {
               StringBuilder lsQuery;
               List<string> loLista;
               DataTable loDatos;
               lsQuery = new StringBuilder();
               loLista = new List<string>();
               lsQuery.AppendFormat("Select ClaveMenu From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "SisPermisosFuncion");
               lsQuery.Append(" where");
               lsQuery.AppendFormat(" Usuario= ('{0}')", psUsuario);
               loDatos = _oSistema.Conexion.RegresaDataTable(lsQuery);
               foreach (DataRow loRow in loDatos.Rows)
               {
                    loLista.Add(loRow.RegresaValor<string>("ClaveMenu").Trim());
               }
               return loLista;
          }
          public List<string> RegresaPermisosFuncionUsuario(string psUsuario)
          {
               StringBuilder lsQuery;
               List<string> loLista;
               DataTable loDatos;
               lsQuery = new StringBuilder();
               loLista = new List<string>();
               lsQuery.AppendFormat("Select clave From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "sis_menus_permisos");
               lsQuery.Append(" where");
               lsQuery.AppendFormat(" usuario= ('{0}')", psUsuario);
               loDatos = _oSistema.Conexion.RegresaDataTable(lsQuery);
               foreach (DataRow loRow in loDatos.Rows)
               {
                    loLista.Add(loRow.RegresaValor<string>("clave").Trim());
               }
               return loLista;
          }
          public List<string> RegresaPermisosCarrerasUsuario(string psUsuario)
          {
               StringBuilder lsQuery;
               List<string> loLista;
               DataTable loDatos;
               lsQuery = new StringBuilder();
               loLista = new List<string>();
               lsQuery.AppendFormat("Select carrera From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "sis_permisos_carreras");
               lsQuery.Append(" where");
               lsQuery.AppendFormat(" usuario= ('{0}')", psUsuario);
               loDatos = _oSistema.Conexion.RegresaDataTable(lsQuery);
               foreach (DataRow loRow in loDatos.Rows)
               {
                    loLista.Add(loRow.RegresaValor<string>("carrera").Trim());
               }
               return loLista;
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
          #endregion
     }
		
}
