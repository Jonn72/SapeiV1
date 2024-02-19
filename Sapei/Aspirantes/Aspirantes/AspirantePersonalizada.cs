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
using Sapei.Framework.BaseDatos;

namespace Sapei
{
	/// <summary>
	/// Clase aspirante generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Aspirante
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
		public DataTable CargarVistaDatosCompletos(string psFolio)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select [nombre],[paterno],[materno],[fechaNacimiento],[entidad_nacimiento]");
			lsConsulta.Append(",[sexo],[curp],[estado_civil],[nss],[correo],[telefonoCasa],[celular],[calle]");
			lsConsulta.Append(",[numero],[id_cp],[ciudad],[colonia],[cp],[entidad],[carrera1],[carrera2],[estatus],estatusAspirante");
			lsConsulta.AppendFormat(" From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "aspirantes_datos_completos");
			lsConsulta.AppendFormat(" where folio = '{0}'", psFolio);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);

		}

		public string RegresaPeriodoActivo()
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select convert(char(4),YEAR(GETDATE())) + substring(max(folio),4,1)");
			lsConsulta.AppendFormat(" From {0}", RutaTabla);
			return Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsConsulta));
		}

		public DataTable CargarEstadisticas(string psPeriodo)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			psPeriodo = psPeriodo.Substring(2, 3);
			lsConsulta.Append("select ");
			lsConsulta.AppendFormat("(select count(folio) from aspirantes where SUBSTRING(folio,2,3) = '{0}') totalAspirantes,", psPeriodo);
			lsConsulta.AppendFormat("(select count(sexo) from aspirantes_datos where SUBSTRING(folio,2,3) = '{0}' and sexo = 'H') totalHombres,", psPeriodo);
			lsConsulta.AppendFormat("(select count(sexo) from aspirantes_datos where SUBSTRING(folio,2,3) = '{0}' and sexo = 'M') totalMujeres,", psPeriodo);
			lsConsulta.AppendFormat("(select count(carrera1) from aspirantes_datos_solicitud where SUBSTRING(folio,2,3) = '{0}' and carrera1 = '1') as ARQ,", psPeriodo);
			lsConsulta.AppendFormat("(select count(carrera1) from aspirantes_datos_solicitud where SUBSTRING(folio,2,3) = '{0}' and carrera1 = '2') as ISA,", psPeriodo);
			lsConsulta.AppendFormat("(select count(carrera1) from aspirantes_datos_solicitud where SUBSTRING(folio,2,3) = '{0}' and carrera1 = '3') as ISI,", psPeriodo);
			lsConsulta.AppendFormat("(select count(carrera1) from aspirantes_datos_solicitud where SUBSTRING(folio,2,3) = '{0}' and carrera1 = '4') as IMC,", psPeriodo);
			lsConsulta.AppendFormat("(select count(carrera1) from aspirantes_datos_solicitud where SUBSTRING(folio,2,3) = '{0}' and carrera1 = '5') as IEL", psPeriodo);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);

		}

		public DataTable CargarVistaDatosCompletosTabla(string psFolio)
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("select [folio],[nombre]+' '+[paterno] +' '+[materno]");
			lsConsulta.Append(",[curp],[sexo],[estatus],[telefonoCasa],[celular]");
			lsConsulta.Append(", [carrera1]");
			lsConsulta.AppendFormat(" From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "aspirantes_datos_completos");
			lsConsulta.AppendFormat(" where substring(folio,2,3) = '{0}'", psFolio);
			return _oSistema.Conexion.RegresaDataTable(lsConsulta);

		}

		public void GuardaLog()
		{
			StringBuilder lsConsulta;
			lsConsulta = new StringBuilder();
			lsConsulta.Append("insert into aspirantes_log (folio, estatusAspirante, no_de_control, usuario, fecha_modificacion)");
			lsConsulta.AppendFormat(" values ('{0}',{1},'{2}','{3}',GETDATE())", this.folio, this.estatusAspirante, this.no_de_control, _oSistema.Sesion.Usuario.Usuario);
			_oSistema.Conexion.EjecutaComando(lsConsulta);
		}

		public DataTable RegresaDatosCompletos(string psFolio, bool pbEsFicha = false)
		{
			DataTable loDt = new DataTable();
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			loParametros.Add(new ParametrosSQL("@folio",psFolio));
			loParametros.Add(new ParametrosSQL("@ficha", pbEsFicha));

			using (var loConexion = new ManejaConexion(_oSistema.Conexion))
			{
				loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_aspirantes_datos_completos", loParametros));
			}
			return loDt;
		}

		#endregion
	}
}
