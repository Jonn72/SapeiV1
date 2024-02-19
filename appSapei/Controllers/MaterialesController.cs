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
     public class MaterialesController : Controller
     {
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public PartialViewResult IndexControlVehicular()
          {
               try
               {
                    return PartialView("IndexControlVehicular");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          /// <summary>
          /// ActivarPeriodo
          /// </summary>
          /// <returns></returns>
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public PartialViewResult ActivarPeriodo()
          {
               try
               {
                    DataTable loTabla;
                    Control_Vehicular loPeriodos;
                    loPeriodos = new Control_Vehicular(SesionSapei.Sistema);
                    loTabla = loPeriodos.RegresaTablaPeriodos();
                    ViewData["txtPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    ViewData["txtDescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
                    if (loTabla.Rows.Count > 0 && SesionSapei.Sistema.Sesion.Periodo.PeriodoActual == loTabla.Rows[0].RegresaValor<string>("periodo"))
                    {
                         ViewData["txtMaxAutos"] = loTabla.Rows[0].RegresaValor<short>("max_autos");
                         ViewData["txtMaxMotos"] = loTabla.Rows[0].RegresaValor<short>("max_motos");
                    }
                    ViewData["Tabla"] = loTabla;
                    ViewData["Titulo"] = "Periodos Registrados";
                    ViewData["Encabezados"] = new List<string> { "Periodo", "Inicio Registro", "Fin Registro", "Inicio Entrega Tarjetón", "Fin Entrega Tarjetón", "Autos", "Autos Registrados", "Motos", "Motos Registradas" };
                    if (loTabla.Rows.Count == 0)
                    {
                         return PartialView("ActivarPeriodo");
                    }
                    if (loTabla.Rows[0].RegresaValor<string>("periodo") == SesionSapei.Sistema.Sesion.Periodo.PeriodoActual)
                    {
                         ViewData["txtFechaIni"] = loTabla.Rows[0].RegresaValor<DateTime>("ini_registro");
                         ViewData["txtFechaFin"] = loTabla.Rows[0].RegresaValor<DateTime>("fin_registro");
                         ViewData["txtIniEntrega"] = loTabla.Rows[0].RegresaValor<DateTime>("ini_entrega");
                         ViewData["txtFinEntrega"] = loTabla.Rows[0].RegresaValor<DateTime>("fin_entrega");
                    }
                    return PartialView("ActivarPeriodo");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public JsonResult GuardaPeriodo(string psPeriodo, string psInicio, string psFin, string psInicioEntrega, string psFinEntrega, short piMaxAutos, short piMaxMotos)
          {
               try
               {
                    Control_Vehicular loControl;
                    loControl = new Control_Vehicular(SesionSapei.Sistema);
                    loControl.Cargar(psPeriodo);
                    if (loControl.EOF)
                    {
                         loControl.periodo = psPeriodo;
                    }
                    loControl.ini_registro = Convert.ToDateTime(psInicio);
                    loControl.fin_registro = Convert.ToDateTime(psFin);
                    loControl.max_autos = piMaxAutos;
                    loControl.max_motos = piMaxMotos;
                    loControl.ini_entrega = Convert.ToDateTime(psInicioEntrega);
                    loControl.fin_entrega = Convert.ToDateTime(psFinEntrega);
                    loControl.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
          /// <summary>
          /// Marcas de vehiculos
          /// </summary>
          /// <returns></returns>
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public PartialViewResult AltaMarcasVehiculos()
          {
               try
               {
                    SisCombo loCombos;
                    loCombos = new SisCombo(SesionSapei.Sistema);
                    ViewData["Combo"] = "cboMarcasVehiculos";
                    ViewData["ComboPadre"] = "cboMarcasVehiculos";
                    ViewData["TablaComboPadre"] = loCombos.CargarValoresPorCombo("cboTipoVehiculo");
                    ViewData["Ruta"] = "Materiales/AltaMarcasVehiculos";
                    ViewData["Encabezado"] = "Marcas de Vehiculos";
                    ViewData["Tabla"] = loCombos.CargarValoresPorCombo("cboMarcasVehiculos", "cboTipoVehiculo");
                    ViewData["Titulo"] = "Lista de Marcas de Vehiculos";
                    ViewData["Encabezados"] = new List<string> { "Valor", "Descripción", "Tipo", "Descripción" };
                    return PartialView("../Generales/AgregaEnCombos");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          /// <summary>
          /// Sub Marcas de vehiculos
          /// </summary>
          /// <param name="id"></param>
          /// <returns></returns>
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public PartialViewResult AltaSubMarcas(string id)
          {
               if (id.ToUpper() == "AUT")
                    return RegresaComboSubMarca("Materiales/AltaSubMarcas/AUT", "AUT", "Submarcas de Autómoviles");
               else
                    return RegresaComboSubMarca("Materiales/AltaSubMarcas/MOT", "MOT", "Submarcas de Motocicletas");
          }
          private PartialViewResult RegresaComboSubMarca(string psRuta, string psValorPadre, string psEncabezado)
          {
               try
               {
                    SisCombo loCombos;
                    loCombos = new SisCombo(SesionSapei.Sistema);
                    ViewData["Combo"] = "cboSubMarcasVehiculos";
                    ViewData["ComboPadre"] = "cboMarcasVehiculos";
                    ViewData["TablaComboPadre"] = loCombos.CargarValoresPorCombo("cboMarcasVehiculos", null, psValorPadre);
                    ViewData["Ruta"] = psRuta;
                    ViewData["Encabezado"] = psEncabezado;
                    ViewData["Tabla"] = loCombos.CargarValoresPorCombo("cboSubMarcasVehiculos", "cboMarcasVehiculos", psValorPadre);
                    ViewData["Titulo"] = "Lista de Submarcas Registradas";
                    ViewData["Encabezados"] = new List<string> { "Valor", "Descripción", "Id Marca", "Marca" };
                    return PartialView("../Generales/AgregaEnCombos");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          /// <summary>
          /// Registro vehicular
          /// </summary>
          /// <returns></returns>
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.DOC, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
          public PartialViewResult RegistroVehicular()
          {
               try
               {
                    SisCombo loCombos;
                    Control_Vehicular loControl;
                    string lsPeriodoActivo;
                    loControl = new Control_Vehicular(SesionSapei.Sistema);
                    loControl.Cargar(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual);
                    lsPeriodoActivo = loControl.ValidaPeriodo();
                    if (!string.IsNullOrEmpty(lsPeriodoActivo))
                    {
                         ViewData["Mensaje"] = lsPeriodoActivo;
                         return PartialView("../Generales/AvisosGenerales");
                    }
                    if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario != Sapei.Framework.Configuracion.enmRolUsuario.MAT)
                    {
                         lsPeriodoActivo = loControl.ValidaUsuario();
                         if (!string.IsNullOrEmpty(lsPeriodoActivo))
                         {
                              ViewData["Mensaje"] = lsPeriodoActivo;
                              return PartialView("../Generales/AvisosGenerales");
                         }
                         /*if (loControl.autos_registrados >= loControl.max_autos)
                         {
                              ViewData["Mensaje"] = "Ya no hay espacios disponibles, pasa al Dep. de Recursos Materiales y solicita informes. Gracias";
                              return PartialView("../Generales/AvisosGenerales");
                         }*/
                    }
                    
                    loCombos = new SisCombo(SesionSapei.Sistema);
                    ViewData["cboTipoVehiculo"] = loCombos.CargarValoresPorCombo("cboTipoVehiculo");
                    ViewData["cboMarcasVehiculos"] = loCombos.CargarValoresPorCombo("cboMarcasVehiculos", "cboTipoVehiculo");
                    ViewData["cboSubMarcasVehiculos"] = loCombos.CargarValoresPorCombo("cboSubMarcasVehiculos", "cboMarcasVehiculos");
                    return PartialView("RegistroVehicular");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.DOC, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
          public JsonResult ActualizaRegistroVehicular(string psUsuario, string psTipo, string psMarca, string psSubmarca, string psPlacas, string psColor, string psModelo)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {

                         Control_Vehicular_Registro loControl;
                         Control_Vehicular loVehicular;
                         string lsCadenaCifrada;
                         loVehicular = new Control_Vehicular(SesionSapei.Sistema);
                         loControl = new Control_Vehicular_Registro(SesionSapei.Sistema);
                         string lsUsuario;
                         if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.DOC || SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
                              lsUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;                         
                         else
                              lsUsuario = psUsuario;

                         loVehicular.Cargar(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual);
                         loControl.Cargar(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual, lsUsuario, psTipo);

                         if (loControl.EOF)
                         {
                              if((psTipo == "AUT" && loVehicular.autos_registrados >= loVehicular.max_autos)||psTipo == "MOT" && loVehicular.motos_registradas >= loVehicular.max_motos)
                              return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Ya no hay espacios disponibles, pasa al Dep. de Recursos Materiales y solicita informes. Gracias", false);
                         }

                         if (loControl.EOF)
                         {
                              loControl.periodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                              loControl.usuario = lsUsuario;
                              loControl.tipo_vehiculo = psTipo;
                              loControl.fecha_registro = DateTime.Now;
                              loControl.estado_registro = "0";
                         }
                         else if (Convert.ToInt32(loControl.estado_registro.Trim()) >= 2 && SesionSapei.Sistema.Sesion.Usuario.RolUsuario != Sapei.Framework.Configuracion.enmRolUsuario.MAT)
                         {
                              return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Ya fue registrado y validado su vehículo por Servicios Materiales, por lo que ya no se permiten cambios", false);
                         }
                         loControl.marca = psMarca;
                         loControl.submarca = psSubmarca;
                         loControl.placas = psPlacas;
                         loControl.color = psColor;
                         loControl.modelo = psModelo;
                         if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.MAT)
                         {
                              if (lsUsuario.RegresaTipoUsuario() == Sapei.Framework.Configuracion.enmTipoUsuario.PERSONAL)
                                   loControl.estado_registro = "3";
                              else
                                   loControl.estado_registro = "2";
                              if(psTipo == "AUT")
                                   loVehicular.autos_registrados = Convert.ToInt16(loVehicular.autos_registrados + 1);
                              if (psTipo == "MOT")
                                   loVehicular.autos_registrados = Convert.ToInt16(loVehicular.motos_registradas + 1);
                         }                              
                         else
                              loControl.estado_registro = "1";
                         if (loControl.qr == null)
                         {
                              lsCadenaCifrada = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.EncryptRJ256(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual + "|" + lsUsuario + "|" + psTipo + "|" + psMarca + "|" + psSubmarca + "|" + psModelo + "|" + psPlacas);
                              loControl.cadena = lsCadenaCifrada;
                              loControl.qr = lsCadenaCifrada.RegresaCadenaQR();
                         }
                         loControl.Guardar();
                         loVehicular.Guardar();
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                    }
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Error al Guardar Registro", false);
               }
          }
          /// <summary>
          /// Registro de Tarjetones
          /// </summary>
          /// <returns></returns>
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public PartialViewResult RegistraTarjetonVehicular()
          {
               try
               {
                    SisCombo loCombos;
                    loCombos = new SisCombo(SesionSapei.Sistema);
                    ViewData["cboTipoVehiculo"] = loCombos.CargarValoresPorCombo("cboTipoVehiculo");
                    return PartialView("RegistraTarjetonVehicular");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public JsonResult ActualizaRegistroTarjetonVehicular(string psUsuario, string psTipo, string psTarjeton)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {

                         Control_Vehicular_Registro loControl;
                         loControl = new Control_Vehicular_Registro(SesionSapei.Sistema);
                         loControl.Cargar(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual, psUsuario, psTipo);
                         if (loControl.EOF)
                         {
                              return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                         }
                         loControl.tarjeton = psTarjeton;
                         loControl.estado_registro = "4";
                         loControl.Guardar();
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                    }
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
          /// <summary>
          /// Entrega de Tarjetones
          /// </summary>
          /// <returns></returns>
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public PartialViewResult EntregaTarjetonVehicular()
          {
               try
               {
                    SisCombo loCombos;
                    loCombos = new SisCombo(SesionSapei.Sistema);
                    ViewData["cboTipoVehiculo"] = loCombos.CargarValoresPorCombo("cboTipoVehiculo");
                    return PartialView("EntregaTarjetonVehicular");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public JsonResult RegistraEntregaTarjetonVehicular(string psUsuario, string psTipo)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {

                         Control_Vehicular_Registro loControl;
                         loControl = new Control_Vehicular_Registro(SesionSapei.Sistema);
                         loControl.Cargar(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual, psUsuario, psTipo);
                         if (loControl.EOF)
                         {
                              return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                         }
                         //loControl.fecha_entrga_tarjeton = DateTime.Now;
                         loControl.estado_registro = "5";
                         loControl.Guardar();
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                    }
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public JsonResult RegresaDatosTarjeton(string psTarjeton)
          {
               try
               {
                    Control_Vehicular_Registro loControl;
                    DataTable loDatos;
                    loControl = new Control_Vehicular_Registro(SesionSapei.Sistema);
                    loDatos = loControl.RegresaDatosSolicitanteVehicular(psTarjeton);
                    if (loDatos.Rows.Count == 1)
                    {
                         return ManejoMensajesJson.RegresaJsonTabla(loDatos);
                    }
                    if (loDatos.Rows.Count > 1)
                    {
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                    }
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
          /// <summary>
          /// Datos de solicitante
          /// </summary>
          /// <param name="psUsuario"></param>
          /// <param name="psTipoUsuario"></param>
          /// <param name="psTipoVehiculo"></param>
          /// <param name="pbConDescripcion"></param>
          /// <returns></returns>
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.DOC, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
          public JsonResult RegresaDatosSolicitanteVehicular(string psUsuario, string psTipoUsuario, string psTipoVehiculo, bool pbConDescripcion = false)
          {
               try
               {
                    Control_Vehicular_Registro loControl;
                    DataTable loDatos;
                    string lsMensaje;
                    loControl = new Control_Vehicular_Registro(SesionSapei.Sistema);
                    if (string.IsNullOrEmpty(psUsuario.Trim()))
                    {
                         psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                         psTipoUsuario = SesionSapei.Sistema.Sesion.Usuario.TipoUsuario.ToString().SubString(0,1);
                    }
                    loDatos = loControl.RegresaDatosSolicitanteVehicular(psUsuario, psTipoUsuario, psTipoVehiculo, pbConDescripcion);
                    if (loDatos.Columns.Count > 1)
                    {
                         return ManejoMensajesJson.RegresaJsonTabla(loDatos);
                    }
                    lsMensaje = loDatos.Rows[0].Field<string>(0);
                    if (lsMensaje == "ERROR")
                    {
                         if (psTipoUsuario == "D")
                              lsMensaje = "El docente no esta registrado con materias en el periodo " + SesionSapei.Sistema.Sesion.Periodo.PeriodoActual.RegresaDescripcionPeriodo();
                         else
                              lsMensaje = "El estudiante no tiene materias registradas en el periodo " + SesionSapei.Sistema.Sesion.Periodo.PeriodoActual.RegresaDescripcionPeriodo();
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
                    }
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
        
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.DOC, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
          public PartialViewResult EstatusRegistroVehicular()
          {
               try
               {                    
                    Control_Vehicular_Registro loControl;
                    DataTable loDt;
                    loControl = new Control_Vehicular_Registro(SesionSapei.Sistema);
                    loDt = loControl.RegresaRegistrosVehiculares();
                    if (loDt.Rows.Count == 0)
                    {
                         ViewData["Mensaje"] = "No tiene vehículos registrados en el periodo " + SesionSapei.Sistema.Sesion.Periodo.PeriodoActual.RegresaDescripcionPeriodo();
                         return PartialView("../Generales/AvisosGenerales");
                    }

                    ViewData["txtDescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual.RegresaDescripcionPeriodo();
                    ViewData["Tabla"] = loDt;
                    ViewData["Titulo"] = "Vehículos registrados";
                    if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.ALU)
                         ViewData["Encabezados"] = new List<string> { "Placas", "Marca", "Sub-marca", "Estado del proceso", "Acción a realizar", "Descargar" };
                    else
                         ViewData["Encabezados"] = new List<string> { "Placas", "Marca", "Sub-marca", "Estado del proceso"};
                    return PartialView("EstatusRegistroVehicular");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }

          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.MAT)]
          public PartialViewResult ConsultarRegistros(string psPeriodo)
          {
               try
               {
                    Control_Vehicular_Registro loControl;
                    DataTable loDt;
                    string lsPeriodo;
                    if (string.IsNullOrEmpty(psPeriodo))
                    {
                         lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    }
                    else
                         lsPeriodo = psPeriodo;
                    loControl = new Control_Vehicular_Registro(SesionSapei.Sistema);
                    loDt = loControl.RegresaDatosRegistros(lsPeriodo);
                    ViewData["periodo"] = lsPeriodo;
                    ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
                    ViewData["Tabla"] = loDt;
                    ViewData["Titulo"] = "Vehículos registrados";
                    ViewData["Encabezados"] = new List<string> { "Usuario", "Nombre", "Placas", "Tipo Vehículo" };
                    return PartialView("");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
     }
}
