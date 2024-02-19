using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using SII.Framework;
using SII.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SII
{
	/// <summary>
	/// Clase quejas_sugerencia generada automáticamente desde el Generador de Código SII
	/// </summary>
    public partial class Quejas_sugerencias
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



        public DataTable CargarVistaQuejasSugerencias(string psNoControl, string psPeriodo)
        {
            StringBuilder lsConsulta;
            lsConsulta = new StringBuilder();
            lsConsulta.Append("select fecha, titulo, mensaje");
            lsConsulta.AppendFormat(" From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "quejas_sugerencias");
            lsConsulta.AppendFormat(" where no_de_control = '{0}'", psNoControl);
            lsConsulta.AppendFormat("and periodo = '{0}'", psPeriodo);
            return _oSistema.Conexion.RegresaDataTable(lsConsulta);

        }
    }
	}
