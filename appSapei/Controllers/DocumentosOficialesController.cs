using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using Sapei.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
	public class DocumentosOficialesController : Controller
	{
		#region Escolares
		[HttpGet]
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult Diplomas()
		{
			try
			{
				ViewData["cboPeriodo"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("periodos_escolares", "periodo", "identificacion_corta", "", "periodo desc", false, 5, null, true);
				ViewData["cboDocOficiales"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("documentos_oficiales", "TRIM(CONVERT(char(3),[clave]))+':'+TRIM(CONVERT(char(3),tipo_documento))+':'+TRIM(CONVERT(char(3),[requiere_periodo]))+':'+TRIM(CONVERT(char(3),[requiere_semestre]))+':'" +
					"+TRIM(CONVERT(char(3),[requiere_seguro]))+':'+ TRIM(CONVERT(char(3),[requiere_imss])) + ':'+ TRIM(CONVERT(char(3),[requiere_fecha_titulacion])) + ':'+ TRIM(CONVERT(char(3),[requiere_traslado])) + ':' + TRIM(CONVERT(char(3),[permite_masivo]))+':'+TRIM([nombre_reporte])", "descripcion", "tipo_documento = 1", "descripcion asc", false, 0, null, true);
				return PartialView("Diplomas");
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult RegresaDocumentoOficial(string psId, string psPeriodo, string psSemestre, string psControl, string psNombreReporte, string psTipoDoc, string psFechaEmision = null, string psOficio = null, string psNomSeguro = null, string psNoSeguro = null, string psIniSeguro = null, string psFinSeguro = null, string psImss = null, string psFechaTitulo = null, string psNombreDirector = null, string psNombreInstituto = null)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				if (psId == "0")
				{
					ViewData["mensaje"] = "Debe Selecconar algun documento";
					return PartialView("../Generales/VisorPDF");
				}
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales"), psNombreReporte + ".rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaDocOficial(psId, psControl, psPeriodo, psSemestre, psTipoDoc, psFechaEmision, psOficio, psNomSeguro, psNoSeguro, psIniSeguro, psFinSeguro, psImss, psFechaTitulo, psNombreDirector, psNombreInstituto));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		public PartialViewResult ConstanciaLiberacionAC(string psControl)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales"), "ConstanciaLiberacionAC.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.ConstanciaLiberacionAC(psControl));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult Certificados(string psControl, string psIniciales, string psNumero, string psLibro, string psFoja, string psTipo, string psRedondeo, string psDirector, string psFecha, string psExpedida, string psFolio, string psFechaEq)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				DataTable loDt = new DataTable();
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales"), "Certificados.rdlc");
				loReporte.RutaReportes = path;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{

					loParametros.Add(new ParametrosSQL("@no_de_control", psControl));

					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_certificado_datos", loParametros));
					ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaCertificado(loDt, psIniciales, psNumero, psLibro, psFoja, psTipo, psRedondeo, psDirector, psFecha,  psExpedida,  psFolio,  psFechaEq));
				}
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult Constancias()
		{
			try
			{
				ViewData["cboPeriodo"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("periodos_escolares", "periodo", "identificacion_corta", "", "periodo desc", false, 5, null, true);
				ViewData["cboDocOficiales"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("documentos_oficiales", "TRIM(CONVERT(char(3),[clave]))+':'+TRIM(CONVERT(char(3),tipo_documento))+':'+TRIM(CONVERT(char(3),[requiere_periodo]))+':'+TRIM(CONVERT(char(3),[requiere_semestre]))+':'+TRIM(CONVERT(char(3),[requiere_seguro]))+':'+TRIM(CONVERT(char(3),[requiere_imss]))+':'+TRIM(CONVERT(char(3),[requiere_fecha_titulacion]))+':'+TRIM(CONVERT(char(3),[requiere_traslado]))+':'+TRIM(CONVERT(char(3),[permite_masivo]))+':'+TRIM([nombre_reporte])", "descripcion", "tipo_documento = 2", "descripcion asc", false, 0, null, true);
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		#endregion
		#region Extraescolares
		public ActionResult RegistroParticipantes(string psPeriodo, string piIdActividad)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Extraescolares"), "RegistroParticipantes.rdlc");
				loReporte.RutaReportes = path;
				return File(loReporte.RegresaRegistroParticipantes(psPeriodo, Convert.ToInt32(piIdActividad)), "application/octet-stream", psPeriodo + "-" + piIdActividad + ".pdf");

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}
		public ActionResult ConstanciaCumplimiento(string psPeriodo, string psControl, string psNombre, string psActividad, string psEntrenador, string psCarrera, string psSemestre, string psDesempeño, string psPromedio, string psIngreso, string psCredito)
		{
			try
			{
				ReportesGenerales loReporte;
				string lsNombreReporte;
				string lsFolio = "";
				string lsDatos;
				string lsCadenaQR;
				byte[] lbysQR;
				bool lbEs15;
				lsNombreReporte = "ConstanciaCumplimiento15";
				lbEs15 = true;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				if (Convert.ToInt32(psIngreso) <= 20151)
				{
					lsNombreReporte = "ConstanciaCumplimiento";
					lbEs15 = false;
				}
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales"), lsNombreReporte + ".rdlc");
				loReporte.RutaReportes = path;
				if (lbEs15)
				{
					switch (psCredito)
					{
						case "TUT":
							Tutorias_Inscritos loInscrito = new Tutorias_Inscritos(SesionSapei.Sistema);
							lsFolio = loInscrito.GeneraFolioLiberacion(psPeriodo, psControl);
							break;
						default:
							Extra_actividades_inscrito loInscritoEx = new Extra_actividades_inscrito(SesionSapei.Sistema);
							lsFolio = loInscritoEx.GeneraFolioLiberacion(psPeriodo, psControl);
							break;

					}
				}
				lsDatos = string.Format("Fecha de emisión:{0}&Periodo:{1}&No Control:{2}&Nombre:{3}&Carrera:{4}&Credito:{5}&Promedio:{6}&Folio:{7}",
								 DateTime.Now, psPeriodo.RegresaDescripcionPeriodo(), psControl, psNombre, psCarrera, psActividad, psDesempeño, lsFolio);
				lsCadenaQR = lsDatos.MD5HASH();
				lbysQR = lsCadenaQR.RegresaQRValidacionDocumentos();
				SesionSapei.Sistema.GrabaValidacionDocumento(lsCadenaQR, lsDatos);

				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaConstanciaCumplimiento(psPeriodo, psControl, psNombre, psActividad, psEntrenador, psCarrera, psSemestre, psDesempeño, psPromedio, lsFolio, lbEs15, psCredito, lbysQR));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}
		public ActionResult ConstanciaCumplimientoPorGrupo(string psPeriodo, string psGrupo, bool pbEs15, string psCredito)
		{
			try
			{
				ReportesGenerales loReporte;
				string lsNombreReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				if (pbEs15)
					lsNombreReporte = "ConstanciaCumplimiento15";
				else
					lsNombreReporte = "ConstanciaCumplimiento";

				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales"), lsNombreReporte + ".rdlc");
				loReporte.RutaReportes = path;
				path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Generales"), "Vacio.rdlc");
				loReporte.RutaReporteVacio = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaConstanciaCumplimiento(psPeriodo, psGrupo, pbEs15, psCredito));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");

			}
			catch (Exception ex)
			{

				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}
		public ActionResult ResultadosPorActividad(string psPeriodo, string piIdActividad)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Extraescolares"), "ResultadoActividades.rdlc");
				loReporte.RutaReportes = path;
				return File(loReporte.RegresaResultadosPorActividad(psPeriodo, Convert.ToInt32(piIdActividad)), "application/octet-stream", psPeriodo + "-" + piIdActividad + ".pdf");

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}
		#endregion
		#region Tutorias
		public ActionResult ResultadosPorTutoria(string psPeriodo, string psGrupo)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Tutorias"), "ResultadoPorTutoria.rdlc");
				loReporte.RutaReportes = path;
				return File(loReporte.RegresaResultadosPorTutoria(psPeriodo, psGrupo), "application/octet-stream", psPeriodo + "-" + psGrupo + ".pdf");

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}

		#endregion
		#region Liberacion Credito Complementario
		public ActionResult LiberacionCredito(string psControl, string psPromedio, string psVoBo = "")
		{
			try
			{
				ReportesGenerales loReporte;
				Alumno loAlumno;
				Creditos_Complementarios loCreditos = new Creditos_Complementarios(SesionSapei.Sistema);

				loAlumno = new Alumno(SesionSapei.Sistema);
				string lsNombreReporte;
				string lsFolio;
				string lsActividad;
				string lsDesempeño;
				string lsCredito;
				string psPath;
				string lsNombre;
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				string lsNombreCarrera;
				string lsTipoCredito;
				string lsDatos;
				string lsCadenaQR;
				byte[] lbysQR;
				lsTipoCredito = "";
				lsNombreReporte = "ConstanciaCumplimiento15";
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				ViewData["pdfbase64"] = null;
				if (float.Parse(psPromedio) < 1)
				{
					ViewData["mensaje"] = "El promedio no es aprobatorio";
					return PartialView("../Generales/VisorPDF");

				}
				loAlumno.Cargar(psControl);
				if (loAlumno.EOF)
				{
					ViewData["mensaje"] = "El no. de control no existe";
					return PartialView("../Generales/VisorPDF");
				}
				lsNombreCarrera = Sapei.Framework.Utilerias.ManejoCarreras.RegresaNombreCompeto(loAlumno.carrera.ToEnum<enmCarreras>());
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.DAC && !SesionSapei.Sistema.TienePermisoCarrera(loAlumno.carrera))
				{
					ViewData["mensaje"] = "Usted no tiene permiso sobre estudiantes de la carrera " + lsNombreCarrera;
					return PartialView("../Generales/VisorPDF");
				}
				psPath = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales"), lsNombreReporte + ".rdlc");
				loReporte.RutaReportes = psPath;
				lsCredito = "";
				lsActividad = "";
				switch (SesionSapei.Sistema.Sesion.Usuario.RolUsuario)
				{
					case Sapei.Framework.Configuracion.enmRolUsuario.DAC:
						lsCredito = "ACA";
						lsTipoCredito = loAlumno.carrera;
						lsActividad = "ACADÉMICA";
						break;
					case Sapei.Framework.Configuracion.enmRolUsuario.ECO:
						lsCredito = "ECO";
						lsTipoCredito = "ECO";
						lsActividad = "CUIDADO AL MEDIO AMBIENTE";
						break;
					case Sapei.Framework.Configuracion.enmRolUsuario.EXT:
						return LiberaExtraescolar(psControl, psPromedio, psVoBo);
					case Sapei.Framework.Configuracion.enmRolUsuario.DDA:
						return LiberaTutoria(psControl, psPromedio);
					case Sapei.Framework.Configuracion.enmRolUsuario.GTV:
						return LiberaServicioSocial(psControl, psPromedio, psVoBo);

				}
				loCreditos.Cargar(psControl, lsCredito);
				if (loCreditos.EOF)
				{
					loCreditos.periodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
					loCreditos.no_de_control = psControl;
					loCreditos.tipo = lsCredito;
					loCreditos.fecha_registro = DateTime.Now;
					loCreditos.fecha_termino = DateTime.Now;
					loCreditos.semestre = loAlumno.semestre;
				}
				else 
				{
					lsPeriodo = loCreditos.periodo;
				}
				loCreditos.usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				loCreditos.promedio = Convert.ToDouble(psPromedio);
				loCreditos.Guardar();
				lsFolio = loCreditos.GeneraFolioLiberacion(lsPeriodo, psControl, lsCredito);

				lsNombre = loAlumno.apellido_paterno + " " + loAlumno.apellido_materno + " " + loAlumno.nombre_alumno;
				lsDesempeño = psPromedio.RegresaDesempeño();

				lsDatos = string.Format("Fecha de emisión:{0}&Periodo:{1}&No Control:{2}&Nombre:{3}&Carrera:{4}&Credito:{5}&Promedio:{6}&Desempeño:{7}&Folio:{8}",
								DateTime.Now.ToShortDateString(), loCreditos.periodo.RegresaDescripcionPeriodo(), loCreditos.no_de_control, lsNombre, lsNombreCarrera, lsActividad, psPromedio, lsDesempeño, lsFolio);

				lsCadenaQR = lsDatos.MD5HASH();

				lbysQR = lsCadenaQR.RegresaQRValidacionDocumentos();

				SesionSapei.Sistema.GrabaValidacionDocumento(lsCadenaQR, lsDatos);

				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaConstanciaCumplimiento(lsPeriodo, psControl, lsNombre.ToUpper(), lsActividad, psVoBo.ToUpper(), lsNombreCarrera, loCreditos.semestre.ToString(), lsDesempeño, psPromedio, lsFolio, true, lsTipoCredito, lbysQR));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");

			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}
		private ActionResult LiberaExtraescolar(string psControl, string psPromedio, string psActividad)
		{
			Extra_Actividades_Liberados loLibera = new Extra_Actividades_Liberados(SesionSapei.Sistema);
			loLibera.Cargar(psControl, psActividad);
			if (loLibera.EOF)
			{
				loLibera.periodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				loLibera.no_de_control = psControl;
				loLibera.tipo_actividad = psActividad;
			}
			loLibera.fecha_registro = DateTime.Now;
			loLibera.promedio = Convert.ToDouble(psPromedio);
			loLibera.usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
			loLibera.Guardar();
			ViewData["mensaje"] = "Se registro exitosamente";
			return PartialView("../Generales/AvisosGenerales");
		}
		private ActionResult LiberaTutoria(string psControl, string psPromedio)
		{
			Tutorias_Inscritos loLibera = new Tutorias_Inscritos(SesionSapei.Sistema);
			string lsPeriodo;
			string lsGrupo;
			lsGrupo = "TUT";
			lsPeriodo = loLibera.RegresaPeriodoGrupoVacio();
			if (string.IsNullOrEmpty(lsGrupo))
			{
				ViewData["mensaje"] = "No hay grupo vacio registrado, pongase en contacto con Centro de Cómputo";
				return PartialView("../Generales/AvisosGenerales");
			}
			loLibera.Cargar(lsPeriodo, psControl, lsGrupo);
			if (loLibera.EOF)
			{
				loLibera.periodo = lsPeriodo;
				loLibera.no_de_control = psControl;
				loLibera.grupo = lsGrupo;
			}
			loLibera.fecha_registro = DateTime.Now;
			loLibera.fecha_termino = DateTime.Now;
			loLibera.promedio = Convert.ToDouble(psPromedio);
			loLibera.semestre = 0;
			loLibera.Guardar();
			ViewData["mensaje"] = "Se registro exitosamente";
			return PartialView("../Generales/AvisosGenerales");
		}
		private ActionResult LiberaServicioSocial(string psControl, string psPromedio, string psRazonLibera)
		{
			System.Data.SqlClient.SqlDataReader loReader;
			string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
			string lsMensaje = "";
			using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
			{
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
				loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
				loParametros.Add(new ParametrosSQL("@promedio", psPromedio));
				loParametros.Add(new ParametrosSQL("@razon", psRazonLibera));
				loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
				loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_ss_liberacion_valida", loParametros);
				if (loReader.HasRows)
				{
					while (loReader.Read())
					{
						lsMensaje = loReader.GetString(0);
					}
				}
				loReader.Close();
				loReader.Dispose();
			}
			ViewData["mensaje"] = lsMensaje;
			return PartialView("../Generales/AvisosGenerales");
		}
		#endregion
		#region Servicio Social
		//REGRESA EN PDF LA CARTA DE PRESENTACIÓN POR PARTE DE VINCULACION          
		public PartialViewResult RegresaCartaPresentacionSS(string psNoControl, string psFolio)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				DataTable loDt = loSolicitud.ConsultaEstadosSS(psNoControl);
				int estado = loDt.RegresaValorFila<int>("estado");
				if (estado == 2)
				{
					estado = estado + 1;
					loSolicitud.ActualizarEstadoSS(estado, psNoControl);
				}
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Vinculacion"), "TecNM-VI-PO-002-03.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaCartaSS(psNoControl, psFolio));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
				//return File(loReporte.RegresaDiplomaEspecialidad(id), "application/octet-stream", "Diploma.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		//REGRESA EN PDF LA CARTA DE PRESENTACIÓN EXTERNO POR PARTE DE VINCULACION         
		public PartialViewResult RegresaCartaPresentacionExternoSS(string psNoControl, string psDependencia)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				DataTable loDt = loSolicitud.ConsultaEstadosSS(psNoControl);
				int estado = loDt.RegresaValorFila<int>("estado");
				if (estado == 2)
				{
					estado = estado + 1;
					loSolicitud.ActualizarEstadoSS(estado, psNoControl);
				}
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Vinculacion"), "TecNM-VI-PO-002-03.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaCartaExternoSS(psNoControl, psDependencia));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
				//return File(loReporte.RegresaDiplomaEspecialidad(id), "application/octet-stream", "Diploma.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		//REGRESA SOLICITUD DE SERVICIO SOCIAL EXTERNO
		public PartialViewResult RegresaSolicitudServicioSocialSS(string psNoControl)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				DataTable loDt = loSolicitud.ConsultaEstadosSS(psNoControl);
				int estado = loDt.RegresaValorFila<int>("estado");
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Vinculacion"), "TecNM-VI-PO-002-01.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.SolicitudServicioSocial(psNoControl));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
				//return File(loReporte.RegresaDiplomaEspecialidad(id), "application/octet-stream", "Diploma.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		public PartialViewResult CartaCompromiso(string psNoControl)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				DataTable loDt = loSolicitud.ConsultaEstadosSS(psNoControl);
				int estado = loDt.RegresaValorFila<int>("estado");
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Vinculacion"), "TecNM-VI-PO-002-02.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.CartaCompromiso(psNoControl));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
				//return File(loReporte.RegresaDiplomaEspecialidad(id), "application/octet-stream", "Diploma.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		public PartialViewResult CartaAsignacion(string psNoControl)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				DataTable loDt = loSolicitud.ConsultaEstadosSS(psNoControl);
				int estado = loDt.RegresaValorFila<int>("estado");
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Vinculacion"), "TecNM-VI-PO-002-07.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.CartaAsignacion(psNoControl));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
				//return File(loReporte.RegresaDiplomaEspecialidad(id), "application/octet-stream", "Diploma.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		//REGRESA EN PDF EL REPORTE BIMESTRAL DEL BIMESTRE 1         
		public PartialViewResult ReporteBimestral1SS(string id)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);

				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ServicioSocial"), "TecNM-VI-PO-002-04.rdlc");
				loReporte.RutaReportes = path;
				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.ReporteBimestral(lsNumero, id));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
				//return File(loReporte.RegresaDiplomaEspecialidad(id), "application/octet-stream", "Diploma.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		//REGRESA EN PDF EL REPORTE EVALUACION CUALITATIVA DEL BIMESTRE 1
		public PartialViewResult EvaluacionCualitativa1SS(string id)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);

				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ServicioSocial"), "TecNM-VI-PO-002-08.rdlc");
				loReporte.RutaReportes = path;
				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.EvaluacionCualitativa(lsNumero, id));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		//REGRESA EN PDF EL REPORTE AUTOEVALUACION CUALITATIVA DEL PRESTADOR DEL BIMESTRE 1
		public PartialViewResult AutoEvaluacionCualitativaPrestadorSS(string psReporte)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);

				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ServicioSocial"), "TecNM-VI-PO-002-09.rdlc");
				loReporte.RutaReportes = path;
				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.AutoEvaluacionCualitativaPrestador(lsNumero, psReporte));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		//REGRESA EN PDF EL REPORTE DE EVALUACION DE ACTIVIDADES POR EL PRESTADOR DE SERVICIO SOCIAL
		public PartialViewResult EvaluacionActividadesPrestadorSS(string psReporte)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);

				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ServicioSocial"), "TecNM-VI-PO-002-10.rdlc");
				loReporte.RutaReportes = path;
				string lsNumero = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.EvaluacionActividadesPrestador(lsNumero, psReporte));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}

		public PartialViewResult TarjetaControl(string psNoControl)
		{
			ReportesGenerales loReporte;
			loReporte = new ReportesGenerales(SesionSapei.Sistema);
			SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
			DataTable loDt = loSolicitud.ConsultaEstadosSS(psNoControl);
			int estado = loDt.RegresaValorFila<int>("estado");
			string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Vinculacion"), "TecNM-VI-PO-002-06.rdlc");
			loReporte.RutaReportes = path;
			ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.TarjetaControl(psNoControl));
			ViewData["mensaje"] = "Documento generado correctamente";
			return PartialView("../Generales/VisorPDF");
		}

		public PartialViewResult CartaTerminoSS(string psNoControl, string psFolio)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				DataTable loDt = loSolicitud.ConsultaEstadosSS(psNoControl);

				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Vinculacion"), "TecNM-VI-PO-002-05.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.CartaTerminoFinal(psNoControl, psFolio));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
				//return File(loReporte.RegresaDiplomaEspecialidad(id), "application/octet-stream", "Diploma.pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
				//return View("../Generales/AvisosGenerales");
			}
		}
		public ActionResult ReportePlanTrabajoSS()
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/ServicioSocial"), "PlanTrabajo.rdlc");
				loReporte.RutaReportes = path;
				string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				DataTable loSt = loSolicitud.ConsultaEstadosSS(lsNoControl);
				int estado = loSt.RegresaValorFila<int>("estado");
				if (estado == 6)
				{
					estado = estado + 1;
					loSolicitud.ActualizarEstadoSS(estado, lsNoControl);
				}
				return File(loReporte.RegresaPlanTrabajo(lsNoControl), "application/octet-stream", "PlanTrabajo" + lsNoControl + ".pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return View("../Generales/AvisosGenerales");
			}
		}
		#endregion
		#region Residencias Profesionales
		public PartialViewResult RegresaCartaPresentacionRP(string psNoControl, string psDependencia, string psFolio)
		{
			try
			{
				ReportesGenerales loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Vinculacion"), "TecNM-AC-PO-004-03.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaCartaRP(psNoControl, psDependencia, psFolio));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
		public PartialViewResult RegresaAsignacionInterno(int piProyecto)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
                string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Academicos"), "TecNM-AC-PO-004-02.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaAsignacionInterno(piProyecto));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
		public ActionResult ReporteSolicitudResidenciasRP()
		{
            try
            {
                ReportesGenerales loReporte = new ReportesGenerales(SesionSapei.Sistema);
                RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
                string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Residencias"), "TecNM-AC-PO-004-01.rdlc");
                loReporte.RutaReportes = path;
                string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                loEstado.Cargar(loNoControl);
                if (loEstado.estado == 9)
                {
                    loEstado.estado = 10;
                    loEstado.Guardar();
                }
				return File(loReporte.RegresaSolicitud(loNoControl), "application/octet-stream", "SolicitudResidencias" + loNoControl + ".pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return View("../Generales/AvisosGenerales");
			}
		}
		public PartialViewResult RegresaSolicitudResidenciasRP(string psNoControl)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Residencias"), "TecNM-AC-PO-004-01.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaSolicitud(psNoControl));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
		public PartialViewResult RegresaAsesoriaRP(int piNumeroAsesoria)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string loNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Residencias"), "TecNM-AC-PO-004-07.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaAsesoria(piNumeroAsesoria, loNoControl));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
		public PartialViewResult ReporteEvaluacionSeguimientoRP()
		{
			try
			{
				ReportesGenerales loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Residencias"), "TecNM-AC-PO-004-08.rdlc");
				loReporte.RutaReportes = path;
                ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaEvaluacionSeguimiento(lsNoControl));
                ViewData["mensaje"] = "Documento generado correctamente";
                return PartialView("../Generales/VisorPDF");
            }
			catch (Exception ex)
			{
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                ViewData["mensaje"] = "Error al descargar reporte";
                return PartialView("AvisosGenerales", "Generales");
            }
		}
        public PartialViewResult ReporteResidenciasProfesionalesRP()
        {
            try
            {
                ReportesGenerales loReporte = new ReportesGenerales(SesionSapei.Sistema);
                string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Residencias"), "TecNM-AC-PO-004-09.rdlc");
                loReporte.RutaReportes = path;
                ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.ReporteResidenciasProfesionales(lsNoControl));
                ViewData["mensaje"] = "Documento generado correctamente";
                return PartialView("../Generales/VisorPDF");
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                ViewData["mensaje"] = "Error al descargar reporte";
                return PartialView("AvisosGenerales", "Generales");
            }
        }
        public PartialViewResult RegresaLiberacionInformeRP()
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Residencias"), "FormatoLiberacion.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaLiberacionInforme(lsNoControl));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
		public PartialViewResult RegresaInformeSemestralRP()
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/Residencias"), "TecNM-AC-PO-004-06.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaInformeSemestral(lsNoControl));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
		#endregion
		#region Estudiantes
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult RegresaCargaAcademica(string psPeriodo, string psControl)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales"), "CargaAcademica.rdlc");
				loReporte.RutaReportes = path;
				ViewData["pdfbase64"] = System.Convert.ToBase64String(loReporte.RegresaCargaAcademica(psPeriodo, psControl));
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.ESC)]
		public PartialViewResult RegresaCargaAcademicaFirmaElectronica(string psPeriodo = null, string psControl = null)
		{
			try
			{
				ReportesGenerales loReporte;
				string lsPeriodo;
				string lsNoControl;
				byte[] lbyReporte;

				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales"), "CargaAcademicaFiEl.rdlc");
				loReporte.RutaReportes = path;
				if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
				{
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
					lsNoControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				}
				else {
					lsPeriodo = psPeriodo;
					lsNoControl = psControl;
				}
				lbyReporte = loReporte.RegresaCargaAcademica(lsPeriodo, lsNoControl, true);

				ViewData["pdfbase64"] = System.Convert.ToBase64String(lbyReporte);
				ViewData["mensaje"] = "Documento generado correctamente";
				return PartialView("../Generales/VisorPDF");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
		public ActionResult RegresaCargasAcademicasFirmaElectronica(string psPeriodo, string psCarrera, string psSemestre)
		{
			try
			{
				ReportesGenerales loReporte;
				loReporte = new ReportesGenerales(SesionSapei.Sistema);
				string path = Path.Combine(Server.MapPath("~/Reportes/RDLC/DocumentosOficiales"), "CargasAcademicasFiEl.rdlc");
				loReporte.RutaReportes = path;
				ViewData["mensaje"] = "Documento generado correctamente";
				return File(loReporte.RegresaCargasAcademicasFiel(psPeriodo, psCarrera, psSemestre), "application/octet-stream", psPeriodo+"_"+psCarrera + "_" + psSemestre + ".pdf");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return View("../Generales/AvisosGenerales");
			}
		}
		#endregion
		#region FirmaElectronica

		#endregion
	}
}
