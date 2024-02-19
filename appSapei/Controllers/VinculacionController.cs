using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
	public class VinculacionController : Controller
	{
		//
		// GET: /Vinculacion/
        #region Servicio Social
        //Tabla de periodo cargado por el usuario de vinculacion de ss
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
		public PartialViewResult ActivarPeriodoSS(string id)
		{
			try
			{
				SS_Activar_Periodo loPeriodos = new SS_Activar_Periodo(SesionSapei.Sistema);
				DataTable loDt;
				string lsPeriodo;
				lsPeriodo = id;
				if(string.IsNullOrEmpty(id))
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				
				loDt = loPeriodos.PeriodosSS();
				ViewData["periodo"] = lsPeriodo;
				ViewData["descPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Periodos Registrados";
				ViewData["Encabezados"] = new List<string> { "Periodo","Descripcion", "Fecha inicio", "Fecha fin","Fecha cierre","Nombre de curso", "url","Fecha Bimestre 1", "Fecha Bimestre 2", "Fecha Bimestre 3" };
                if (loDt.Rows.Count > 0 && loDt.Rows[0].RegresaValor<string>("periodo") == lsPeriodo)
                {
                    ViewData["txtFechaInicio"] = loDt.Rows[0].RegresaValor<string>("fecha_inicio");
                    ViewData["txtFechaFin"] = loDt.Rows[0].RegresaValor<string>("fecha_fin");
                    ViewData["txtFechaCierre"] = loDt.Rows[0].RegresaValor<string>("fecha_cierre_registro");
                    ViewData["txtTitulo"] = loDt.Rows[0].RegresaValor<string>("nombre");
                    ViewData["txtUrl"] = loDt.Rows[0].RegresaValor<string>("url");
                    ViewData["txtB1"] = loDt.Rows[0].RegresaValor<string>("fecha_bimestre_1");
                    ViewData["txtB2"] = loDt.Rows[0].RegresaValor<string>("fecha_bimestre_2");
                    ViewData["txtB3"] = loDt.Rows[0].RegresaValor<string>("fecha_bimestre_3");
                }
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");

			}
		}
		//El departamento de vinculacion captura el periodo de ss
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
		public JsonResult ActivarPeriodoJsonSS(string psPeriodo,DateTime psInicio, DateTime psFin, DateTime psCierre, string psNombre, string psUrl, DateTime psB1, DateTime psB2, DateTime psB3)
		{
			try
			{
				SS_Activar_Periodo loPeriodos = new SS_Activar_Periodo(SesionSapei.Sistema);
				string lsPeriodo = psPeriodo;
				loPeriodos.Cargar(lsPeriodo);
				if (loPeriodos.EOF)
				{
					loPeriodos.periodo = lsPeriodo;
				}
				loPeriodos.fecha_inicio = psInicio;
				loPeriodos.fecha_fin = psFin.UltimaHoraDia();
                loPeriodos.fecha_cierre_registro = psCierre.UltimaHoraDia();
                loPeriodos.nombre = psNombre;
                loPeriodos.url = psUrl;
                loPeriodos.fecha_bimestre_1 = psB1;
                loPeriodos.fecha_bimestre_2 = psB2;
                loPeriodos.fecha_bimestre_3 = psB3;
                loPeriodos.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
        
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult ControlEscolarSS()
        {
            try
            {
                string lsPeriodo;
                lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                ViewData["periodo"] = lsPeriodo;
                ViewData["descPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;              
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult AltaDependenciaSS()
		{
			try
			{
				SS_Dependencia loPeriodos = new SS_Dependencia(SesionSapei.Sistema);
				DataTable loDt;
				loDt = loPeriodos.DependenciasCompletoSS();
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Dependencias Registradas";
				ViewData["Encabezados"] = new List<string> { " ", "RFC", "Dependencia", "Titular", "Puesto del Titular", "Teléfono", "Domicilio", "Numero", "Colonia", "CP" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

        //Elimina la dependencia del usuario vinculación
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult EliminaDependenciaJsonSS(string psRfc)
		{
			try
			{
				SS_Domicilio_Programa loDomicilio = new SS_Domicilio_Programa(SesionSapei.Sistema);
				loDomicilio.Cargar(psRfc);
				loDomicilio.Eliminar();
				SS_Dependencia loDependencia = new SS_Dependencia(SesionSapei.Sistema);
				loDependencia.Cargar(psRfc);
				loDependencia.Eliminar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

        //El departamento de vinculacion puede capturar las dependencias *****
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult AltaDependenciaJsonSS(string psDependencia, string psRfc, string psTitular, string psCargotitular,
											  string psTelefono, string psCalle, int psNodomicilio, string psCodigopostal, 
                                              int psColonia)
		{
			try
			{
				SS_Dependencia loDependencia = new SS_Dependencia(SesionSapei.Sistema);
				loDependencia.Cargar(psRfc);
				if (loDependencia.EOF)
				{
					loDependencia.rfc = psRfc;
				}
				loDependencia.dependencia = psDependencia.Trim();
				loDependencia.titular = psTitular;
				loDependencia.puesto_cargo = psCargotitular;
				loDependencia.telefono = psTelefono;
				loDependencia.Guardar();

				SS_Domicilio_Programa loDomicilio = new SS_Domicilio_Programa(SesionSapei.Sistema);
				loDomicilio.Cargar(psRfc);
				if (loDomicilio.EOF)
				{
					loDomicilio.rfc_domicilio = psRfc;
				}
				loDomicilio.domicilio = psCalle;
				loDomicilio.numero = psNodomicilio;
                loDomicilio.id_cp = psColonia;
				loDomicilio.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

        //Registro de los alumnos que vieron el video de curso de induccion de servicio social *****
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult RegistroCursoInduccionSS()
		{
			try
			{
				SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
				DataTable loDt;
				loDt = loSolicitud.ListaCursoInduccionSS();
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Alumnos Registrados";
				ViewData["Encabezados"] = new List<string> { "Número de Control", "Nombre", "Apellido Paterno", "Apellido Materno", "Carrera" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}

        //VISTA PARCIAL DE VINCULACIÓN DONDE EL ESTUDIANTE SOLICITA CARTA DE PRESENTACIÓN INTERNA
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult CartaPresentacion()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {

                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        //VISTA PARCIAL DE VINCULACIÓN DONDE EL ESTUDIANTE SOLICITA CARTA DE PRESENTACIÓN INTERNA
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult CartaPresentacionInternoSS()
        {
            try
            {
                SS_Solicitud loSolicitudPresentacion = new SS_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loSolicitudPresentacion.ListaCartaPresentacionInternoSS();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Solicitud de Carta de Presentación";
                ViewData["Encabezados"] = new List<string> { " ", "Folio", "Número de Control", "Nombre Completo", "Carrera", "Turno" };
                return PartialView();
            }
            catch (Exception ex)
            {

                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult ActualizarProgramaInternoSSJson(int piPrograma, string psControl)
        {
            try
            {
                SS_Solicitud loActualizar = new SS_Solicitud(SesionSapei.Sistema);
                loActualizar.ActualizarIdPrograma(piPrograma,psControl);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(ActualizaEstado(psControl, 3));
            }
            catch (Exception ex)
            {

                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        //Controlador para generar y guardar un nuevo programa por parte de vinculacion ***** 
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult AltaProgramaInternoSSJson(string psDepartamento, string psPrograma, string psCargoResponsablePrograma, string psCorreo, string psResponsable, string psTipoprograma, string psObjetivo)
        {
            try
            {
                SS_Programa loPrograma = new SS_Programa(SesionSapei.Sistema);
                SS_Activar_Periodo loPeriodo = new SS_Activar_Periodo(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loPeriodo.RegresaPeriodoSS();
                string lsPeriodo = loDt.RegresaValorFila<string>("periodo");
                int lsid = 0;
                string RFC = "TNM140723GFA";
                loDt = loPrograma.ConsultaProgramaSS(psPrograma, psDepartamento, RFC);
                if (loDt == null)
                {
                    loPrograma.Cargar(lsid, lsPeriodo);
                    if (loPrograma.EOF)
                    {
                        loPrograma.id = lsid;
                        loPrograma.periodo = lsPeriodo;
                    }

                    loPrograma.rfc = RFC;
                    loPrograma.nombre = psPrograma;
                    loPrograma.id_tipo_programa = psTipoprograma;
                    loPrograma.correo_titular = psCorreo;
                    loPrograma.responsable = psResponsable;
                    loPrograma.cargo_responsable = psCargoResponsablePrograma;
                    loPrograma.objetivo = psObjetivo;
                    loPrograma.departamento = psDepartamento;
                    loPrograma.Guardar();
                }
                else
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                }
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult CartaAceptacionSS()
        {
            try
            {
                SS_Solicitud loCartaAceptacion = new SS_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loCartaAceptacion.ListaCartaAceptacionSS();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Cartas Entregadas";
                ViewData["Encabezados"] = new List<string> { " ", "Número de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult ValidarCartaAceptacionSS(string psControl)
        {
            try
            {
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(ActualizaEstado(psControl, 4));
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult Bimestre1SS()
        {
            try
            {
                SS_Estado_Solicitud loReporte = new SS_Estado_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loReporte.ListaReporteBimestre1SS();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Reportes-Bimestre 1";
                ViewData["Encabezados"] = new List<string> { " ", "Numero de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult ValidarBimestre1SS(string psControl, double psCalificacion)
        {
            try
            {
                SS_Solicitud loBimestre1 = new SS_Solicitud(SesionSapei.Sistema);
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                int psReporte = 1;
                loBimestre1.RegistroCalificacionCualitativa(psControl, psCalificacion, psReporte);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(ActualizaEstado(psControl, 8));
			}
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }

        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult Bimestre2SS()
        {
            try
            {
                SS_Estado_Solicitud loReporte = new SS_Estado_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loReporte.ListaReporteBimestre2SS();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Reportes-Bimestre 2";
                ViewData["Encabezados"] = new List<string> { " ", "Numero de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult ValidarBimestre2SS(string psControl, double psCalificacion)
        {
            try
            {
                SS_Solicitud loBimestre2 = new SS_Solicitud(SesionSapei.Sistema);
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                int psReporte = 2;
                loBimestre2.RegistroCalificacionCualitativa(psControl, psCalificacion, psReporte);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(ActualizaEstado(psControl, 9));
			}
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult Bimestre3SS()
        {
            try
            {
                SS_Estado_Solicitud loReporte = new SS_Estado_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loReporte.ListaReporteBimestre3SS();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Reportes-Bimestre 3";
                ViewData["Encabezados"] = new List<string> { " ", "Numero de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult ReportesSS()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult ValidarBimestre3SS(string psControl, double psCalificacion)
        {
            SS_Solicitud loBimestre3 = new SS_Solicitud(SesionSapei.Sistema);
            string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
            int psReporte = 3;
            loBimestre3.RegistroCalificacionCualitativa(psControl, psCalificacion, psReporte);
			return ManejoMensajesJson.RegresaMensajeJsonBusqueda(ActualizaEstado(psControl, 10));
		}

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult ReporteFinalSS()
        {
            try
            {
                SS_Estado_Solicitud loReporte = new SS_Estado_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loReporte.ListaReporteBimestre4SS();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Reportes-Bimestre Final";
                ViewData["Encabezados"] = new List<string> { " ", "Numero de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult ValidarReporteFInalSS(string psControl)
        {			
			return ManejoMensajesJson.RegresaMensajeJsonBusqueda(ActualizaEstado(psControl, 11));
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult CartaTerminoSS()
        {
            try
            {
                SS_Estado_Solicitud loCartaTermino = new SS_Estado_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loCartaTermino.ListaCartaTerminoSS();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Carta de Termino de Servicio Social";
                ViewData["Encabezados"] = new List<string> { " ", "Número de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult ValidarCartaTerminacionSS(string psControl)
        {
            try
            {
				bool pbResultado;
				pbResultado = ActualizaEstado(psControl, 12);
				if(pbResultado)
					LiberaServicioSocial(psControl);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(pbResultado);
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }
		private void LiberaServicioSocial(string psControl)
		{
			try
			{
				string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
					loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
					loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
					loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
					SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_ss_liberacion", loParametros);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
			}
		}
        //REGRESA LOS PROGRAMAS QUE PERTENECEN AL DEPARTAMENTO SELECCIONADO
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult RegresaNombreDepartamentoSS(string psDepartamento)
        {
            try
            {
                SS_Programa loPrograma = new SS_Programa(SesionSapei.Sistema);
                return ManejoMensajesJson.RegresaJsonTabla(loPrograma.RegresaNombreDepartamentoSS(psDepartamento));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }

        //REGRESA LOS DATOS DEL PROGRAMA REGISTRADO EN EL DEPARTAMENTO SELECCIONADO
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult RegresaDatosProgramaDepartamentoSS(string psPrograma)
        {
            {
                try
                {
                    SS_Programa loPrograma = new SS_Programa(SesionSapei.Sistema);
                    return ManejoMensajesJson.RegresaJsonTabla(loPrograma.RegresaProgramaDepartamento(psPrograma));
                }
                catch (Exception ex)
                {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
                }

            }

        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult SolicitudServicioSocialSS()
        {
            try
            {
                SS_Estado_Solicitud loSolicitud = new SS_Estado_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loSolicitud.ListaSolicitudSS();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Solicitudes";
                ViewData["Encabezados"] = new List<string> { " ", "Folio", "Número de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult CartaCompromisoSS()
        {
            try
            {
                SS_Estado_Solicitud loSolicitud = new SS_Estado_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loSolicitud.ListaCartaCompromisoSS();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Carta Compromiso";
                ViewData["Encabezados"] = new List<string> { " ", "Folio", "Número de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult CartaAsignacionSS()
        {
            try
            {
                SS_Estado_Solicitud loSolicitud = new SS_Estado_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loSolicitud.ListaCartaAsignacion();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Carta de Asignación";
                ViewData["Encabezados"] = new List<string> { " ", "Número de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult ValidarReporteFinal(string psControl)
        {
            SS_Solicitud loReporteFinal = new SS_Solicitud(SesionSapei.Sistema);
            string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
            DataTable loDt = loReporteFinal.ConsultaEstadosSS(psControl);
            int liestado = loDt.RegresaValorFila<int>("estado");
            if (liestado == 9)
            {
                liestado = liestado + 1;
                loReporteFinal.ActualizarEstadoSS(liestado, psControl);
            }
            return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult TarjetaControlSS()
        {
            try
            {
                SS_Solicitud loSolicitud = new SS_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loSolicitud.ListaTarjetaControl();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Tarjeta de Control";
                ViewData["Encabezados"] = new List<string> { " ", "Número de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult CartaTerminoFinalSS()
        {
            try
            {
                SS_Estado_Solicitud loCartaTerminoFinal = new SS_Estado_Solicitud(SesionSapei.Sistema);
                DataTable loDt;
                loDt = loCartaTerminoFinal.ListaCartaTerminoFinalSS();
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Constancia de Termino de Servicio Social";
                ViewData["Encabezados"] = new List<string> { " ", "Folio", "Número de Control", "Nombre Completo", "Carrera" };
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        //Registro de los alumnos que ya generaron su solicitud para la carta de presentacion de servicio social *****
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult CartaPresentacionExternoSS()
		{

			try
			{
				SS_Solicitud loSolicitudPresentacion = new SS_Solicitud(SesionSapei.Sistema);
                SS_Dependencia loDependencia = new SS_Dependencia(SesionSapei.Sistema);
				DataTable loDt;
                loDt = loDependencia.DependenciasSS();
                if (loDt != null)
                {ViewData["Dependencias"] = loDt;}                
				loDt = loSolicitudPresentacion.ListaCartaPresentacionExternoSS();
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Solicitud de Carta de Presentación";
				ViewData["Encabezados"] = new List<string> { " ", "Folio", "Número de Control", "Nombre Completo", "Carrera" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		private bool ActualizaEstado(string psControl, int piEstado)
		{
			SS_Estado_Solicitud loEstado = new SS_Estado_Solicitud(SesionSapei.Sistema);
			loEstado.Cargar(psControl);
			if (loEstado.EOF)
			{
				return false;
			}
			loEstado.estado = piEstado;
			loEstado.Guardar();
			return true;
		}
       
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult EliminarProcesoSS(string psNoControl, string psContraseña, string psJustificacion)
        {
            string lsMensaje;
            try
            {
                if (string.IsNullOrEmpty(psContraseña) || !SesionSapei.Sistema.Sesion.Usuario.Contraseña.Equals(psContraseña.Trim()))
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("La contraseña es invalida",false);
                }
                SS_Solicitud loSolicitud= new SS_Solicitud(SesionSapei.Sistema);
                lsMensaje = loSolicitud.EliminaProceso(psNoControl, psJustificacion, SesionSapei.Sistema.Sesion.Usuario.Usuario);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje,true);
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Error de servidor",false);
            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult AvanceProcesoSS(string psNoControl = null)
        {
            try
            {
                DataTable loDt = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));

                if (string.IsNullOrEmpty(psNoControl))
                {
                    loParametros.Add(new ParametrosSQL("@no_de_control", DBNull.Value));

                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {
                        loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_ss_avance_proceso", loParametros));
                    }
                    ViewData["Tabla"] = loDt;
                    ViewData["Titulo"] = "Avance del proceso";
                    ViewData["Modulo"] = "AvanceProcesoSS";
                    ViewData["Vista"] = "Vinculacion/AvanceProcesoSS";
                    ViewData["periodo_desc"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
                    return PartialView("BuscaNoControl");
                }
                loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_ss_avance_proceso", loParametros));
                    ViewData["DatosEstudiante"] = loDt;
                }
                return PartialView("AvanceProcesoSS");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult RegistroExtenporaneoSS(string psNoControl = null)
        {
            try
            {
                DataTable loDt = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

                if (string.IsNullOrEmpty(psNoControl))
                {
                    loParametros.Add(new ParametrosSQL("@no_de_control", DBNull.Value));

                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {
                        loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_ss_candidatos",loParametros));
                    }
                    ViewData["Tabla"] = loDt;
                    ViewData["Titulo"] = "Inscripción Extemporanea";
                    ViewData["Modulo"] = "InscripcionExtemporanea";
                    ViewData["Vista"] = "Vinculacion/RegistroExtenporaneoSS";
                    ViewData["periodo_desc"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
                    return PartialView("BuscaNoControl");
                }
                loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_ss_candidatos",loParametros));
                    ViewData["DatosEstudiante"] = loDt;
                }
                return PartialView("RegistroExtenporaneoSS");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult PreregistroSS(string psNoControl = null)
        {
            try
            {
                DataTable loDt = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();

                if (string.IsNullOrEmpty(psNoControl))
                {
                    loParametros.Add(new ParametrosSQL("@no_de_control", DBNull.Value));

                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {
                        loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_ss_candidatos", loParametros));
                    }
                    ViewData["Tabla"] = loDt;
                    ViewData["Titulo"] = "Candidatos de Servicio Social";
                    ViewData["Modulo"] = "Preregistro";
                    ViewData["Vista"] = "Vinculacion/PreregistroSS";
                    ViewData["periodo_desc"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
                    return PartialView("BuscaNoControl");
                }
                loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_ss_candidatos", loParametros));
                    ViewData["DatosEstudiante"] = loDt;
                }
                return PartialView("PreregistroSS");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");

            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult AutorizaPreRegistroJson(string psNoControl)
        {
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));

            try
            {

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pai_ss_proceso", loParametros);
                }
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Error de servidor", false);
            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult AutorizaRegistroExtenporaneoJson(string psNoControl)
        {
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            loParametros.Add(new ParametrosSQL("@no_de_control", psNoControl));

            try
            {

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pai_ss_extenporaneo", loParametros);
                }
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Error de servidor", false);
            }
        }
        


        #endregion
        #region Residencias Profesionales
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
		public PartialViewResult ControlEscolarRP()
		{
			try
			{
				ViewData["txtPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				ViewData["txtDescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
		//Carga la vista para la captura de convenios(dependencias)
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult AltaDependenciaRP()
		{
			try
			{
				RP_Dependencias loPeriodos = new RP_Dependencias(SesionSapei.Sistema);
				DataTable loDt;
				loDt = loPeriodos.DependenciasCompletoRP();
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Dependencias Registradas";
				ViewData["Encabezados"] = new List<string> { " ", " RFC ", "Dependencia", "Titular", "Puesto del Titular", "Teléfono" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
        //Función para eleminar las dependencias, recibe el rfc de la dependencia a eleminar
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult EliminaDependenciaJsonRP(string psRfc)
		{
			try
			{
				RP_domicilio_dependencias loDomicilio = new RP_domicilio_dependencias(SesionSapei.Sistema);
				loDomicilio.Cargar(psRfc);
				loDomicilio.Eliminar();
				RP_Dependencias loDependencia = new RP_Dependencias(SesionSapei.Sistema);
				loDependencia.Cargar(psRfc);
				loDependencia.Eliminar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
        //Función para el registro de convenios(depednecias)
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult AltaDependenciaJsonRP(string psDependencia, string psGiro, string psMision, string psRfc, string psTitular, string psCargotitular,
									    string psTelefono, string psCalle, int psNodomicilio, string psCodigopostal, int psIdCodigo)
		{
			try
			{
				RP_Dependencias loDependencia = new RP_Dependencias(SesionSapei.Sistema);
				loDependencia.Cargar(psRfc);
				if (loDependencia.EOF)
				{
					loDependencia.rfc = psRfc;
				}
				loDependencia.dependencia = psDependencia;
				loDependencia.giro = psGiro;
				loDependencia.mision = psMision;
				loDependencia.titular = psTitular;
				loDependencia.puesto_cargo = psCargotitular;
				loDependencia.telefono = psTelefono;
				loDependencia.Guardar();

				RP_domicilio_dependencias loDomicilio = new RP_domicilio_dependencias(SesionSapei.Sistema);
				loDomicilio.Cargar(psRfc);
				if (loDomicilio.EOF)
				{
					loDomicilio.rfc_domicilio = psRfc;
				}
				loDomicilio.domicilio = psCalle;
				loDomicilio.numero = psNodomicilio;
                loDomicilio.id_cp = psIdCodigo;
                loDomicilio.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult AltaDependenciaOficinaJsonRP(string psRfc, string psOficina, string psResponsable)
        {
            try
            {
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                loParametros.Add(new ParametrosSQL("@rfc",psRfc));
                loParametros.Add(new ParametrosSQL("@oficina", psOficina));
                loParametros.Add(new ParametrosSQL("@responsable", psResponsable));
                using (var conexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    SesionSapei.Sistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pai_rp_dependencias_oficina", loParametros);
                }

                 return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }
        //Función para consultar los datos de una dependencia, recibe el rfc a consultar
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult RegresaDatosDependenciaRP(string psRFC)
		{
			try
			{
				RP_Dependencias loDependencia = new RP_Dependencias(SesionSapei.Sistema);
				return ManejoMensajesJson.RegresaJsonTabla(loDependencia.RegresaDatos(psRFC));
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}

		}
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult RegresaDatosDependenciaOficinasRP(string psRFC)
        {
            try
            {
                RP_Dependencias loDependencia = new RP_Dependencias(SesionSapei.Sistema);
                return ManejoMensajesJson.RegresaJsonTabla(loDependencia.RegresaDatosDependenciasRP(psRFC, 1));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        //Carla la vista para la generación de cartas de presentación por residente
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult SolicitudesRP()
		{
			try
			{
				RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
                RP_Dependencias loDependencia = new RP_Dependencias(SesionSapei.Sistema); 
				DataTable loDt;
                loDt = loDependencia.DependenciasRP();
                if (loDt != null)
                { ViewData["Dependencias"] = loDt; }
                loDt = loSolicitud.CargaSolicitudesRP();
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Solicitudes de Carta de Presentación - Residentes";
				ViewData["Encabezados"] = new List<string> { " ", "Número de Control", "Nombre de Residente", "Carrera" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
        //Carga la vista para la validación de cartas de aceptación por residente
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult CartaAceptacionRP()
		{
			try
			{
				RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
				DataTable loDt = loEstado.ValidacionCartaAceptacionRP();
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Validación de carta de aceptación " + lsPeriodo.RegresaDescripcionLargaPeriodo();
				ViewData["Encabezados"] = new List<string> { " ", "Número de Control", "Nombre de Residente" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
        //Función para validar la carta de aceptación, recibe el número de control del residente
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult ValidaAceptacionJsonRP(string psNoControl)
		{
			try
			{
                RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
                loEstado.Cargar(psNoControl);
                if (loEstado.estado == 2)
                {
                    loEstado.estado = 3;
                    loEstado.Guardar();
                }
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
        //Carga la vista para la validación de la carta de termino
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult CartaTerminoRP()
		{
			try
			{
				RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
				DataTable loDt;
				loDt = loEstado.ValidaCartaTermino();
				ViewData["Tabla"] = loDt;
				ViewData["Titulo"] = "Validación carta termino";
				ViewData["Encabezados"] = new List<string> { " ", "Número de Control", "Nombre Residente" };
				return PartialView();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index", "Home");
			}
		}
        //Función para validar la carta de termino del residente, recibe el número de control
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public JsonResult ValidaCartaTerminoJsonRP(string psNoControl)
		{
			try
			{
                RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
                RP_Solicitud loSolicitud = new RP_Solicitud(SesionSapei.Sistema);
                DataTable loPr;
                loPr = loSolicitud.ValidaTermino(psNoControl);
                if (loPr.Columns.Count == 1)
                {
                    string mensaje = loPr.RegresaValorFila<string>("mensaje");
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(mensaje ,false);
                }
                loEstado.Cargar(psNoControl);
                if (loEstado.estado == 11)
                {
                    loEstado.estado = 12;
                    loEstado.Guardar();
                }
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}
        //Carga la vista para ver el estado del residente
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.GTV)]
        public PartialViewResult ConsultaEstadoRP()
        {
            try
            {
                RP_Estado_Solicitud loEstado = new RP_Estado_Solicitud(SesionSapei.Sistema);
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                string usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                DataTable loDt;
                loDt = loEstado.ConsultaEstadoRP(usuario, lsPeriodo);
                ViewData["Tabla"] = loDt;
                ViewData["Titulo"] = "Estado de Residentes";
                ViewData["Encabezados"] = new List<string> { "N° de Control", "Nombre Residente", "Estado" };
                ViewData["periodo"] = lsPeriodo;
                return PartialView();
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }
        #endregion



    }

}
