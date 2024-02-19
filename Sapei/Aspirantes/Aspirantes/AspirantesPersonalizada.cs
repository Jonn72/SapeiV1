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
	/// Clase aspirantes generada automáticamente desde el Generador de Código SII
	/// </summary>
	public partial class Aspirantes
	{
		#region variables
		#endregion
		#region Propiedades
		#endregion
		#region Contructor
		#endregion
		#region Metodos/Funciones
          public DataTable CargarEstadisticas(string psPeriodo)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               psPeriodo = psPeriodo.Substring(2, 3);
               lsConsulta.Append("select ");
               lsConsulta.AppendFormat("(select count(folio) from aspirantes where SUBSTRING(folio,2,3) = '{0}') totalAspirantes,",psPeriodo);
               lsConsulta.AppendFormat("(select count(sexo) from aspirantes_datos where SUBSTRING(folio,2,3) = '{0}' and sexo = 'H') totalHombres,",psPeriodo);
               lsConsulta.AppendFormat("(select count(sexo) from aspirantes_datos where SUBSTRING(folio,2,3) = '{0}' and sexo = 'M') totalMujeres,", psPeriodo);
               lsConsulta.AppendFormat("(select count(carrera1) from aspirantes_datos_solicitud where SUBSTRING(folio,2,3) = '{0}' and carrera1 = '1') as ARQ,", psPeriodo);
               lsConsulta.AppendFormat("(select count(carrera1) from aspirantes_datos_solicitud where SUBSTRING(folio,2,3) = '{0}' and carrera1 = '2') as ISA,", psPeriodo);
               lsConsulta.AppendFormat("(select count(carrera1) from aspirantes_datos_solicitud where SUBSTRING(folio,2,3) = '{0}' and carrera1 = '3') as ISI,", psPeriodo);
               lsConsulta.AppendFormat("(select count(carrera1) from aspirantes_datos_solicitud where SUBSTRING(folio,2,3) = '{0}' and carrera1 = '4') as IMC,", psPeriodo);
               lsConsulta.AppendFormat("(select count(carrera1) from aspirantes_datos_solicitud where SUBSTRING(folio,2,3) = '{0}' and carrera1 = '5') as IEL", psPeriodo);
               return _oSistema.Conexion.RegresaDataTable(lsConsulta);

          }
          public DataTable CargarVistaDatosCompletos(string psFolio)
          {
               StringBuilder lsConsulta;
               lsConsulta = new StringBuilder();
               lsConsulta.Append("select [nombre],[paterno],[materno],[fechaNacimiento],[entidad_nacimiento]");
               lsConsulta.Append(",[sexo],[curp],[estado_civil],[correo],[telefonoCasa],[celular],[calle]");
               lsConsulta.Append(",[numero],[id_cp],[ciudad],[colonia],[cp],[entidad],[carrera1],[carrera2]");
               lsConsulta.AppendFormat(" From [{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, _oSistema.Servidor.Principal.BaseDatos.Propietario, "aspirantes_datos_completos");
               lsConsulta.AppendFormat(" where substring(folio,2,3) = '{0}'", psFolio);
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
		#endregion

		}
	}
