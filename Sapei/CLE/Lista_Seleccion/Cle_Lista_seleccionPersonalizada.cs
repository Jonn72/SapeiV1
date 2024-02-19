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
	/// Clase cle_lista_seleccion generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Cle_Lista_Seleccion
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

          public DataTable RegresaLista(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select no_de_control, nivel, ini_seleccion, fin_seleccion ");
               lsConsulta.AppendFormat(" FROM {0} ", RutaTabla);
               lsConsulta.AppendFormat(" WHERE periodo = '{0}'",psPeriodo);
               lsConsulta.Append(" order by ini_seleccion asc");
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);
          }

          public void ActivaEstudiante(int piOpcion, string psControl, string psNivel, string psFecha)
          {
               StringBuilder lsQuery = new StringBuilder();
               switch (piOpcion)
               {
                    case 2:
                         lsQuery.AppendFormat("UPDATE {0} SET ", RutaTabla);
                         lsQuery.AppendFormat("ini_seleccion = convert(datetime,'{0}',120), fin_seleccion = dateadd(HOUR,2,convert(datetime,'{0}',120))",psFecha);
                         lsQuery.AppendFormat(" WHERE periodo = '{0}' and no_de_control = '{1}'",_oSistema.Sesion.Periodo.PeriodoActual,psControl);
                         break;
                    case 3:
                    case 4:
                         lsQuery.AppendFormat("INSERT INTO {0} (periodo, no_de_control, nivel, ini_seleccion, fin_seleccion)", RutaTabla);
                         lsQuery.AppendFormat(" VALUES ('{0}','{1}','{2}',convert(datetime,'{3}',120),dateadd(HOUR,2,convert(datetime,'{3}',120)))", _oSistema.Sesion.Periodo.PeriodoActual, psControl,psNivel,psFecha);
                         break;
               }
               _oSistema.Conexion.EjecutaComando(lsQuery);
          }
		#endregion

		}
	}
