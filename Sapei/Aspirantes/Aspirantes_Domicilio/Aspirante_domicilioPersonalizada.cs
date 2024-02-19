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
using Sapei.Framework.Utilerias;
namespace Sapei
{
	/// <summary>
	/// Clase aspirantes_domicilio generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Aspirante_domicilio
	{
		#region variables
          private class DomicilioCompleto
          {
               public string calle;
               public string numero;
               public string cp;
               public string colonia;
               public string ciudad;
               public string entidad;
               public string id_cp;
          }
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
          public object CargaDatosDomicilio()
          {
               StringBuilder lsConsulta;
               DomicilioCompleto loDomicilio;
               DataTable loResltado;
               loDomicilio = null;
               lsConsulta = new StringBuilder();
               if (this.id_cp == 0)
                    return null;

               lsConsulta.Append("Select [calle],[numero],[id_cp], ");
               lsConsulta.Append(" (SELECT [cod_post] from c_p where id = A.id_cp) as cp, ");
               lsConsulta.Append(" (SELECT [ciudad_localidad] from c_p where id = A.id_cp) as ciudad, ");
               lsConsulta.Append(" (SELECT [colonia] from c_p where id = A.id_cp) as colonia,");
               lsConsulta.Append(" (SELECT (SELECT descripcion from sisCombos where valor = (convert(char(5),[ent_fed]))and combo = 'cboEstados') from c_p where id = A.id_cp) as entidad");
               lsConsulta.AppendFormat(" From [{0}].[{1}].[{2}] A", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, NombreTabla);
               lsConsulta.AppendFormat(" WHERE folio = '{0}'", this.folio);
               loResltado = _oSistema.Conexion.RegresaDataTable(lsConsulta);
               if (loResltado.Rows.Count == 0)
                    return null;
               foreach (DataRow loDr in loResltado.Rows)
               {
                    loDomicilio = new DomicilioCompleto()
                    {
                         calle = loDr.RegresaValor<string>("calle"),
                         numero = loDr.RegresaValor<string>("numero"),
                         cp = loDr.RegresaValor<string>("cp"),
                         colonia = loDr.RegresaValor<string>("colonia"),
                         ciudad = loDr.RegresaValor<string>("ciudad"),
                         entidad = loDr.RegresaValor<string>("entidad"),
                         id_cp = loDr.RegresaValor<string>("id_cp")
                    };
               }
               return loDomicilio;

          }
          #endregion
		}
	}
