using appSapei.App_Start;
using appSapei.Clases;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syncfusion.EJ2;
using Syncfusion.EJ2.Schedule;
using System.Collections;
using Newtonsoft.Json;

namespace appSapei.Controllers
{
    public class RecursosHumanosController : Controller
    {
        // GET: RecursosHumanos
        public ActionResult Index()
        {
            return View();
        }


        #region RegistroPersonal
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public PartialViewResult RegistraPersonal()
        {
            //return PartialView("RegistraPersonal");
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable loDatos, loDatos2;

                    organigrama loDatosOrganigrama = new organigrama(SesionSapei.Sistema);
                    Personal loDatosPersonal = new Personal(SesionSapei.Sistema);
                    loDatos = loDatosOrganigrama.RegresaOrganigrama();
                    loDatos2 = loDatosPersonal.RegresaNivelEstudios();
                    ViewData["Tabla"] = loDatosPersonal.RegresaPersonalLista();
                    ViewData["TablaDeptos"] = loDatos;
                    ViewData["NivelEstudios"] = loDatos2;
                }
                return PartialView("RegistraPersonal");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public JsonResult RegresaPersonal(string psRFC)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loDatos;
                    loDatos = new Personal(SesionSapei.Sistema);

                    DataTable loDt;

                    loDt = loDatos.RegresaPersonalDatos(psRFC);

                    if (loDt.Rows.Count <= 0)
                    {

                        return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();

                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaJsonTabla(loDt);
                    }
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public JsonResult GuardaPersonal(string psRFC, string psEstatus, string psNombre, string psApePaterno, string psApeMaterno, int psNTarjeta, string psTPersonal, string psClaveArea, /*string psAreaAcad,*/ string psActividadLaboral, string psNSS, string psGenero, string psFechaNac, string psEstadoCivil, string psNacionalidad, string psEstadoNac, string psEstudios, string psCarrera, string psFechaTit, string psCedula, string psCorreo, string psTel, string psTelEmergencia, string psCURP, string psCalle, string psNDomicilio, int psId_cp, int psCol)
        {

            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loPersonal;
                    personal_domicilio loPersonal_dom;
                    personal_personales loPersonal_per;

                    loPersonal = new Personal(SesionSapei.Sistema);
                    loPersonal_dom = new personal_domicilio(SesionSapei.Sistema);
                    loPersonal_per = new personal_personales(SesionSapei.Sistema);

                    loPersonal.Cargar(psRFC);
                    loPersonal_dom.Cargar(psRFC);
                    loPersonal_per.Cargar(psRFC);

                    loPersonal.rfc = psRFC;
                    loPersonal_dom.rfc = psRFC;
                    loPersonal_per.rfc = psRFC;

                    loPersonal.clave_area = psClaveArea;
                    loPersonal.no_tarjeta = psNTarjeta;
                    loPersonal.apellidos_empleado = psApePaterno + " " + psApeMaterno;
                    loPersonal.nombre_empleado = psNombre;
                    loPersonal.status_empleado = psEstatus;
                    //loPersonal.area_academica = psAreaAcad;
                    loPersonal.actividad_laboral = psActividadLaboral;
                    loPersonal.area_academica = psClaveArea;
                    loPersonal.tipo_personal = psTPersonal;
                    loPersonal.apellido_paterno = psApePaterno;
                    loPersonal.apellido_materno = psApeMaterno;
                    loPersonal_dom.calle = psCalle;
                    loPersonal_dom.numero = psNDomicilio;
                    loPersonal_dom.id_cp = psId_cp;
                    loPersonal_dom.colonia = psCol;
                    loPersonal_per.curp = psCURP;
                    loPersonal_per.estado_civil = psEstadoCivil;
                    loPersonal_per.nacionalidad = psNacionalidad;
                    loPersonal_per.nivel_estudios = psEstudios;
                    loPersonal_per.nombre_carrera = psCarrera;
                    loPersonal_per.fecha_titulacion = psFechaTit;
                    loPersonal_per.cedula_profesional = psCedula;
                    loPersonal_per.correo_electronico = psCorreo;
                    loPersonal_per.telefono = psTel;
                    loPersonal_per.telefono_emergencia = psTelEmergencia;
                    loPersonal_per.NSS = psNSS;
                    loPersonal_per.genero = psGenero;
                    loPersonal_per.fecha_nacimiento = psFechaNac;
                    loPersonal_per.estado_nacimiento = psEstadoNac;
                    if (loPersonal.EOF)
                    {
                        loPersonal.fecha_registro = DateTime.Today;

                        System.Data.SqlClient.SqlDataReader loReader;
                        List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                        string lsMensaje, lsNombre;
                        lsNombre = psNombre + " " + psApePaterno + " " + psApeMaterno;
                        loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                        loParametros.Add(new ParametrosSQL("@nombre_usuario", lsNombre));
                        loParametros.Add(new ParametrosSQL("@actividad_laboral", psActividadLaboral));
                        loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pav_existe_usuario_acceso", loParametros);
                        lsMensaje = "";
                        if (loReader.HasRows)
                        {
                            while (loReader.Read())
                            {
                                lsMensaje = loReader.GetString(0);
                            }
                        }
                        loReader.Close();
                        loReader.Dispose();
                        if (lsMensaje.Trim() != "1")
                        {
                            ViewData["Mensaje"] = lsMensaje;
                            return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
                        }
                    }
                    loPersonal.Guardar();
                    loPersonal_dom.Guardar();
                    loPersonal_per.Guardar();

                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }


        }
        #endregion

        #region MarcadoParaEliminar
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public PartialViewResult PuestosPersonal(string psRFC = null)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loPersonal;
                    PersonalPuestos loPersonalPuesto;
                    loPersonal = new Personal(SesionSapei.Sistema);
                    loPersonalPuesto = new PersonalPuestos(SesionSapei.Sistema);
                    if (string.IsNullOrEmpty(psRFC))
                    {
                        ViewData["Titulo"] = "Lista de Personal Activo";
                        ViewData["Encabezados"] = new List<string> { "RFC", "Nombre", "Tipo de Personal", "  .  " };
                        ViewData["Tabla"] = loPersonal.RegresaPersonalActivoPuestos();
                        ViewData["css2"] = "div-hide";
                        return PartialView("PuestosPersonal");
                    }
                    else
                    {
                        DataTable loDatos;
                        loDatos = loPersonal.RegresaPersonalDatos(psRFC);
                        ViewData["RFC"] = loDatos.Rows[0].RegresaValor<string>("rfc");
                        ViewData["txtNombre"] = loDatos.Rows[0].RegresaValor<string>("nombre_empleado") + " " + loDatos.Rows[0].RegresaValor<string>("apellido_paterno") + " " + loDatos.Rows[0].RegresaValor<string>("apellido_materno");

                        string tper = loDatos.Rows[0].RegresaValor<string>("tipo_personal");

                        switch (tper)
                        {
                            case "B":
                                ViewData["txtTPersonal"] = "Base";
                                break;

                            case "X":
                                ViewData["txtTPersonal"] = "Mixto";
                                break;

                            case "H":
                                ViewData["txtTPersonal"] = "Honorarios";
                                break;
                        }

                        ViewData["Titulo"] = "Puestos Asignados";
                        ViewData["Encabezados"] = new List<string> { "Puesto", "Fecha de Ingreso", "Fecha de Termino", "  .  " };
                        ViewData["Tabla"] = loPersonalPuesto.RegresaPersonalPuestoRFC(psRFC);

                        Puestos loPuestos;
                        loPuestos = new Puestos(SesionSapei.Sistema);
                        ViewData["Puestos"] = loPuestos.RegresaPuesto(psRFC);

                        return PartialView("PuestosPersonal");
                    }
                }
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }


        public JsonResult GuardaPuesto(string psRFC, int psPuesto, int psBaja)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    PersonalPuestos loPPuestos;
                    loPPuestos = new PersonalPuestos(SesionSapei.Sistema);
                    loPPuestos.Cargar(psRFC, psPuesto);

                    if (loPPuestos.EOF)
                    {
                        loPPuestos.rfc = psRFC;
                        loPPuestos.clave_puesto = psPuesto;
                    }


                    if (psBaja == 1)
                    {
                        loPPuestos.fecha_termino_puesto = Convert.ToDateTime(DateTime.Now);
                    }
                    else
                    {
                        loPPuestos.fecha_termino_puesto = null;
                        loPPuestos.fecha_ingreso_puesto = Convert.ToDateTime(DateTime.Now);
                    }

                    loPPuestos.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);

                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        #endregion

        #region Recontratacion
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public PartialViewResult AutorizaRecontratacion()
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loPersonal;
                    loPersonal = new Personal(SesionSapei.Sistema);
                    ViewData["Titulo"] = "Personal Solicitado";
                    ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Area que Solicita", "Puesto", "Tipo de Contrato", "Inicio de Periodo", "Fin de Periodo", "Accion" };
                    ViewData["Tabla"] = loPersonal.RegresaPersonalSolicitados();
                    return PartialView("AutorizaRecontratacion");
                }
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }
        public JsonResult GuardaPersonalSolicitudRH(int psID, int psStatus, string psIdMonto = null)
        {

            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    PersonalSoicitud loPersonal_Solicitud;
                    loPersonal_Solicitud = new PersonalSoicitud(SesionSapei.Sistema);
                    loPersonal_Solicitud.Cargar(psID);
                    if (loPersonal_Solicitud.EOF)
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                    
                    if (string.IsNullOrEmpty(psIdMonto))
                    {
                        loPersonal_Solicitud.status = psStatus;
                        loPersonal_Solicitud.Guardar();
                    }
                    else
                    {
                        loPersonal_Solicitud.id_monto = Convert.ToInt32(psIdMonto);
                        System.Data.SqlClient.SqlDataReader loReader;
                        string lsMensaje;
                        List<ParametrosSQL> loParametros2 = new List<ParametrosSQL>();
                        loParametros2.Add(new ParametrosSQL("@id", psID));


                        loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_genera_id_contrato", loParametros2);
                        lsMensaje = "";
                        if (loReader.HasRows)
                        {
                            while (loReader.Read())
                            {
                                lsMensaje = loReader.GetString(0);
                            }
                        }
                        loReader.Close();
                        loReader.Dispose();

                        if (lsMensaje.Trim() != "1")
                        {
                            return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
                        }
                        loPersonal_Solicitud.Guardar();
                    }

                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }

        public PartialViewResult DetallesContrato(int psID = 0)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loPersonal;
                    string tipocontrato, puesto;
                    loPersonal = new Personal(SesionSapei.Sistema);
                    if (psID == 0)
                    {
                        ViewData["Titulo"] = "Lista de Personal Activo";
                        ViewData["Encabezados"] = new List<string> {  "Nombre de Personal", "Fecha de Creación", "Area que Solicita", "Puesto", "Tipo de Contrato", "Accion" };
                        ViewData["Tabla"] = loPersonal.RegresaListaDatosContrato();
                        ViewData["css2"] = "div-hide";
                        return PartialView("DetallesContrato");
                    }
                    else
                    {
                        DataTable loDatos, loDatos2;
                        loDatos = loPersonal.RegresaDatosPreContrato(psID);
                        ViewData["hidID"] = psID;
                        ViewData["txtRFC"] = loDatos.Rows[0].RegresaValor<string>("rfc");
                        ViewData["txtNombre"] = loDatos.Rows[0].RegresaValor<string>("nombre");
                        ViewData["txtTipoContrato"] = loDatos.Rows[0].RegresaValor<string>("tipo_contrato");
                        ViewData["txtDescripcionPuesto"] = loDatos.Rows[0].RegresaValor<string>("descripcion_puesto");
                        ViewData["txtPeriodoInicio"] = loDatos.Rows[0].RegresaValor<string>("periodo_inicio");
                        ViewData["txtPeriodoFin"] = loDatos.Rows[0].RegresaValor<string>("periodo_fin");
                        tipocontrato = loDatos.Rows[0].RegresaValor<string>("tipo_contrato");
                        puesto = loDatos.Rows[0].RegresaValor<string>("clave_puesto");
                        string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;

                        if (tipocontrato == "Asimilados")
                            {
                                if (puesto == "13")
                                {
                                    loDatos2 = loPersonal.RegresaFechasDetalles(lsPeriodo, "B");
                                    ViewData["hid"] = loDatos2;
                                ViewData["puesto"] = puesto;
                                }
                            ViewData["puesto"] = "X";
                            ViewData["pago"] = loPersonal.SelectMontosPago("B");
                            ViewData["tipo_contrato"] = "B";
                            }
                            else
                            {
                                if (puesto == "13")
                                {
                                    loDatos2 = loPersonal.RegresaFechasDetalles(lsPeriodo, "H");
                                    ViewData["hid"] = loDatos2;
                                ViewData["puesto"] = puesto;
                                }
                            ViewData["puesto"] = "X";
                            ViewData["pago"] = loPersonal.SelectMontosPago("H");
                            ViewData["tipo_contrato"] = "H";
                            }
                        
                        ViewData["css1"] = "div-hide";
                        return PartialView("DetallesContrato");
                    }
                }
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }

        public PartialViewResult FirmaContratoRH(string psRFC = null, string psFechaCreacion = null, string psClaveArea = null, string psTipoContrato = null)
        {
            /*try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                    DataTable loDatos, loDatos2;
                    Personal loPersonal = new Personal(SesionSapei.Sistema);
                    if (string.IsNullOrEmpty(psRFC))
                    {
                        ViewData["CSS2"] = "div-hide";
                        loDatos = loPersonal.RegresaDatosFirmaContratoRH(psUsuario);
                        ViewData["Titulo"] = "Contratos de Personal";
                        ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Departamento", "Tipo de Contrato", "Acción" };
                        ViewData["Tabla"] = loDatos;
                    }
                    else
                    {
                        ViewData["CSS1"] = "div-hide";
                        loDatos2 = loPersonal.RegresaDatosPersonalContratoGeneral(psFechaCreacion, psRFC, psClaveArea, psTipoContrato);
                        ViewData["txtRFC"] = psRFC;
                        ViewData["txtCURP"] = loDatos2.Rows[0].RegresaValor<string>("curp");
                        ViewData["txtNombre"] = loDatos2.Rows[0].RegresaValor<string>("nombre");
                        ViewData["txtCalle"] = loDatos2.Rows[0].RegresaValor<string>("calle");
                        ViewData["txtNumero"] = loDatos2.Rows[0].RegresaValor<string>("numero");
                        ViewData["txtColonia"] = loDatos2.Rows[0].RegresaValor<string>("colonia");
                        ViewData["txtCP"] = loDatos2.Rows[0].RegresaValor<string>("cod_post");
                        ViewData["txtCD"] = loDatos2.Rows[0].RegresaValor<string>("ciudad_localidad");
                        ViewData["txtEstado"] = loDatos2.Rows[0].RegresaValor<string>("nombre_entidad");
                        ViewData["txtPeriodoInicio"] = loDatos2.Rows[0].RegresaValor<string>("periodo_inicio");
                        ViewData["txtPeriodoFin"] = loDatos2.Rows[0].RegresaValor<string>("periodo_fin");
                        ViewData["txtHSemanales"] = loDatos2.Rows[0].RegresaValor<string>("horas_semanales");
                        ViewData["txtTNeto1"] = loDatos2.Rows[0].RegresaValor<string>("total_neto");
                        ViewData["txtCLetra"] = loDatos2.Rows[0].RegresaValor<string>("monto_letra");
                        ViewData["txtTBruto"] = loDatos2.Rows[0].RegresaValor<string>("total_bruto");
                        ViewData["txtIVA"] = loDatos2.Rows[0].RegresaValor<string>("iva_trans");
                        ViewData["txtSTotal"] = loDatos2.Rows[0].RegresaValor<string>("sub_total");
                        ViewData["txtRIVA"] = loDatos2.Rows[0].RegresaValor<string>("iva_ret");
                        ViewData["txtRISR"] = loDatos2.Rows[0].RegresaValor<string>("isr_ret");
                        ViewData["txtTNeto2"] = loDatos2.Rows[0].RegresaValor<string>("total_neto");
                        ViewData["txtFechaCreacion"] = psFechaCreacion;
                        ViewData["txtClaveArea"] = psClaveArea;
                        ViewData["txtTipoContrato"] = psTipoContrato;
                    }*/
                    return PartialView("FirmaContratoRH");
                /*}
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }*/
        }

        public PartialViewResult ContratosFirmados()
        {
            try
            {
                string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                DataTable loDatos;
                Personal loPersonal = new Personal(SesionSapei.Sistema);
                loDatos = loPersonal.RegresaContratosFirmados();
			    ViewData["Titulo"] = "Personal Solicitado";
			    ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Departamento que Solicita", "Tipo de Contrato",  "Acción" };
                ViewData["Tabla"] = loDatos;
			    return PartialView("ContratosFirmados");
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }


        #endregion

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.PLA)]

        public PartialViewResult Retardos(string psRFC = null)
        {
            try
            {
                string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                DataTable loDatos;
                Personal loPersonal = new Personal(SesionSapei.Sistema);
                ViewData["Personal"] = loPersonal.RegresaPersonalContratadoAdministrativo();
                loDatos = loPersonal.RegresaRetardosRegistrados();
                ViewData["Titulo"] = "Lista de Retardos";
                ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Puesto", "Fecha de Retardo", "Horas", "Acción" };
                ViewData["Tabla"] = loDatos;
                return PartialView("Retardos");
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.PLA)]
        public JsonResult RegresaDatosPersonalRetardo(int psID)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loDatos;
                    loDatos = new Personal(SesionSapei.Sistema);
                    DataTable loDt;

                    loDt = loDatos.RegresaDatosPersonalContratado(psID);

                    if (loDt.Rows.Count <= 0)
                    {

                        return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();

                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaJsonTabla(loDt);
                    }
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }

        public JsonResult GuardaRetardo(int psID, string psFechaRetardo, int psHoraRetardo, int psIDRetardo = 0)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DateTime lsFechaRetardo;
                    PersonalRetardos loPersonalRetardos = new PersonalRetardos(SesionSapei.Sistema);
                    if (psIDRetardo == 0)
                    {
                        loPersonalRetardos.Cargar(Convert.ToInt32(null));
                    }
                    else
                    {
                        loPersonalRetardos.Cargar(psIDRetardo);
                    }

                    lsFechaRetardo = DateTime.Parse(psFechaRetardo);
                    loPersonalRetardos.id = psID;
                    loPersonalRetardos.fecha_retardo = lsFechaRetardo;
                    loPersonalRetardos.horas_retardo = psHoraRetardo;
                    loPersonalRetardos.Guardar();


                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }

        public JsonResult EliminarRetardo(int psIDRetardo)
        {

            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    PersonalRetardos loPersonal_Retardos = new PersonalRetardos(SesionSapei.Sistema);
                    loPersonal_Retardos.Cargar(psIDRetardo);
                    loPersonal_Retardos.Eliminar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }

        #region ListaPersonal
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public PartialViewResult ListaPersonal()
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loPersonal;
                    string psPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                    loPersonal = new Personal(SesionSapei.Sistema);
                    ViewData["Titulo"] = "Personal Solicitado";
                    ViewData["Encabezados"] = new List<string> { "RFC", "Area", "Nº de tarjeta", "Nombre de Empleado", "Apellido Paterno", "Apellido Materno", "Estatus Empleado", "Area Academica", "Tipo de Personal", "CURP", "Estado Civil",  "Nacionalidad", "Nivel de Estudios", "Carrera", "Fecha de Titulación", "Cedula", "Correo Electronico", "Telefono", "Telefono de Emergencia", "Numero de Seguro Social", "Genero", "Fecha de Nacimiento", "Estado de Nacimiento", "Calle", "Numero", "CP" };
                    ViewData["Tabla"] = loPersonal.RegresaListaPersonal();
                    return PartialView("ListaPersonal");
                }
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }
        #endregion

        #region Control Personal
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public PartialViewResult ControlPersonal()
        {
            try
            {
                DataTable loDt = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                loParametros.Add(new ParametrosSQL("@tipo_consulta", 1));
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_personal",loParametros));
                    ViewData["ListaRFC"] = loDt;
                    loDt = new DataTable();
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_categorias_plazas"));
                    ViewData["ListaCategorias"] = loDt;

                }
                return PartialView();
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public JsonResult GuardaCategoriaPlaza(string psCategoria, string psDescripcion)
        {
            try
            {
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                loParametros.Add(new ParametrosSQL("@categoria", psCategoria));
                loParametros.Add(new ParametrosSQL("@descripcion", psDescripcion));

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    SesionSapei.Sistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pam_rh_categorias_plazas", loParametros);
                }
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public JsonResult GuardaPersonalPlaza(string psRFC,string psCategoria,string psInterinato, int piHoras, string psFecha)
        {
            try
            {
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                loParametros.Add(new ParametrosSQL("@categoria", psCategoria));
                loParametros.Add(new ParametrosSQL("@horas", piHoras));
                loParametros.Add(new ParametrosSQL("@interinato", Sapei.Framework.Utilerias.ManejoCadenas.ToBoolean(psInterinato)));
                loParametros.Add(new ParametrosSQL("@inicio", psFecha));

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    SesionSapei.Sistema.Conexion.EjecutaComandoProcedimientoAlmacenado("pam_rh_personal_plazas", loParametros);
                }
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public PartialViewResult MostrarCategoriasPlazas()
        {
            try
            {
                DataTable loDt = new DataTable();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                   loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_categorias_plazas"));
                }
                ViewData["Tabla"] = loDt;
                return PartialView("../Generales/TablaGeneral");
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public PartialViewResult MostrarPersonalPlazas()
        {
            try
            {
                DataTable loDt = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                loParametros.Add(new ParametrosSQL("@tipo_consulta", 1));
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_personal_plazas",loParametros));
                }
                ViewData["Tabla"] = loDt;
                return PartialView("../Generales/TablaGeneral");
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public JsonResult RegresaPlazas(string psRFC)
        {
            try
            {
                DataTable ldtTabla = new DataTable();
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    ldtTabla.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rh_personal_plazas", loParametros));
                }
                return ManejoMensajesJson.RegresaJsonTabla(ldtTabla);
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public JsonResult EliminaPlazasPersonal(string psId)
        {
            try
            {
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                loParametros.Add(new ParametrosSQL("@id", psId));
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_rh_personal_plazas", loParametros);
                }
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }

        #endregion

        #region Control de Asistencia
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]

        public PartialViewResult ControlAsistencia()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public PartialViewResult ProcesaControlAsistencia
            (int piTipoPersonal, DateTime poFechaInicio, DateTime poFechaFin)
        {
            try
            {
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                DataTable loDt = new DataTable();
                loParametros.Add(new ParametrosSQL("@tipo_personal", piTipoPersonal));
                loParametros.Add(new ParametrosSQL("@fecha_inicio", poFechaInicio));
                loParametros.Add(new ParametrosSQL("@fecha_fin", poFechaFin));
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
					loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_rh_procesa_asistencia", loParametros));
                }
                ViewData["Titulo"] = "Lista de Incidencias";
                ViewData["Tabla"] = loDt;
                return PartialView("../Generales/TablaGeneral");
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRH)]
        public PartialViewResult MuestraRegistrosPersonal(string psRFC)
        {
            try
            {
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                DataTable loDt = new DataTable();
                loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_rh_registro_checador", loParametros));
                }
                ViewData["Titulo"] = "Registro de checadas";
                ViewData["Tabla"] = loDt;
                return PartialView("../Generales/TablaGeneral");
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }
        }
        #endregion

        #region Jefes

        #endregion
    }
}
