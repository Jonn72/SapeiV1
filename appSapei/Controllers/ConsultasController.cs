using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
	public class ConsultasController : Controller
	{
		#region Estudiantes
		public PartialViewResult Estudiantes()
		{
			try
			{
				Sis_Consultas loSis;
				Periodos_Escolares loPeriodos;
				loSis = new Sis_Consultas(SesionSapei.Sistema);
				loPeriodos = new Periodos_Escolares(SesionSapei.Sistema);
				ViewData["Modulo"] = "CON";
				ViewData["cboPeriodos"] = loPeriodos.RegresaComboPeriodo(12);
				ViewData["cboConsultas"] = loSis.RegresaConsultasCombo();
				ViewData["Titulo"] = "Estudiantes Inscritos";
				ViewData["Tabla"] = null;
				ViewData["Encabezados"] = new List<string> { "No. Control", "Nombre", "A. Paterno", "A. Materno", "F. Nacimiento", "NSS", "Genero", "CURP", "Estado Civil", "Correo", "Teléfono", "Celular", "Estatus", "Periodo de Ingreso", "Carrera", "Especialidad" };
				return PartialView("../Generales/Consultas");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("~/Personal/Index");
			}
		}


		#endregion
		#region Indicadores
		[SessionExpire]
		[HttpGet]
		public PartialViewResult Indicadores()
		{
			try
			{
				string lsFiltro = string.Format("departamento like '%{0}%'", SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString());
				ViewData["cboConsultas"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("sis_indicadores", "clave", "descripcion", lsFiltro, true);
				lsFiltro = "SUBSTRING(periodo,5,1) != '2'";
				ViewData["cboPeriodos"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("periodos_escolares", "periodo", "identificacion_corta", lsFiltro, "periodo desc", false, 20, null, true);
				ViewData["Titulo"] = "Estudiantes Inscritos";
				ViewData["Tabla"] = null;
				ViewData["Modulo"] = "IND";
				return PartialView("../Generales/Consultas");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		public PartialViewResult RegresaDatos(string psTipo, int piID, string psPeriodo)
		{
			try
			{
				string lsProcedure = "";
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loDt = new DataTable();
				loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
				loParametros.Add(new ParametrosSQL("@tipo_consulta", piID));
				loParametros.Add(new ParametrosSQL("@tipo_usuario", SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString()));

				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					switch (psTipo)
					{
						case "IND":
							lsProcedure = "pac_sis_indicadores";
							break;
						case "CON":
							loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
							lsProcedure = "pac_sis_consultas";
							break;
					}
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader(lsProcedure, loParametros));
					ViewData["Tabla"] = loDt;

					ViewData["Titulo"] = "Valores";
					ViewData["titulo_consulta"] = "Indicadores";
					return PartialView("../Generales/TablaGeneral");
				}
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		#endregion
	}
}
