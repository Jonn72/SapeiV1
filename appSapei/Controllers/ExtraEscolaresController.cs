using appSapei.App_Start;
using Sapei;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sapei.Framework.BaseDatos;
using System.IO;

namespace appSapei.Controllers
{
     public class ExtraEscolaresController : Controller
     {
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public PartialViewResult ActivarPeriodo()
          {
               try
               {
                    DataTable loTabla;
                    Extra_actividades_fecha loPeriodos;
                    loPeriodos = new Extra_actividades_fecha(SesionSapei.Sistema);
                    loTabla = loPeriodos.RegresaTablaPeriodos();
                    ViewData["txtPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    ViewData["txtDescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
                    ViewData["Tabla"] = loTabla;
                    ViewData["Titulo"] = "Periodos Registrados";
                    ViewData["Encabezados"] = new List<string> { "Periodo", "Fecha Inicio", "Fecha Fin" };
                    if (loTabla.Rows.Count == 0)
                    {
                         return PartialView("ActivarPeriodo");
                    }
                    if (loTabla.Rows[0].RegresaValor<string>("periodo") == SesionSapei.Sistema.Sesion.Periodo.PeriodoActual)
                    {
                         ViewData["txtFechaIni"] = loTabla.Rows[0].RegresaValor<DateTime>("fecha_ini_registro");
                         ViewData["txtFechaFin"] = loTabla.Rows[0].RegresaValor<DateTime>("fecha_fin_registro");
                    }
                    return PartialView("ActivarPeriodo");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public JsonResult GuardaPeriodo(string psPeriodo, string psInicio, string psFin)
          {
               try
               {
                    Extra_actividades_fecha loActividad;
                    loActividad = new Extra_actividades_fecha(SesionSapei.Sistema);
                    loActividad.Cargar(psPeriodo);
                    if (loActividad.EOF)
                    {
                         loActividad.periodo = psPeriodo;
                    }
                    loActividad.fecha_ini_registro = Convert.ToDateTime(psInicio);
                    loActividad.fecha_fin_registro = Convert.ToDateTime(psFin);
                    loActividad.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public PartialViewResult AltaTipoActividades()
          {
               try
               {
                    SisCombo loCombos;
                    loCombos = new SisCombo(SesionSapei.Sistema);
                    ViewData["Combo"] = "cboTipoActividades";
                    ViewData["Ruta"] = "ExtraEscolares/AltaTipoActividades";
                    ViewData["Encabezado"] = "Tipos de Actividad Extraescolar";
                    ViewData["Tabla"] = loCombos.CargarValoresPorCombo("cboTipoActividades");
                    ViewData["Titulo"] = "Lista de Actividades Registradas";
                    ViewData["Encabezados"] = new List<string> { "Valor", "Descripción" };
                    return PartialView("../Generales/AgregaEnCombos");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public PartialViewResult Actividades(bool psSoloTabla = false)
          {
               try
               {
                    Extra_actividad loActividad;
                    loActividad = new Extra_actividad(SesionSapei.Sistema);
                    ViewData["Tabla"] = loActividad.RegresaTablaActividades();
                    ViewData["Titulo"] = "Lista de Actividades Registradas";
                    ViewData["Encabezados"] = new List<string> { "No.", "Tipo", "Descripción", "Entrenador", "Capacidad", "Inscritos", "Horario" };
                    if (psSoloTabla)
                         return PartialView("../../Generales/TablaGeneral");
                    return PartialView("Actividades");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public PartialViewResult RegistrarActividades(bool pbSoloTabla = false)
          {
               try
               {
                    DataTable loTabla;
                    Cle_Grupos loGrupos;
                    Extra_actividad loActividad;
                    Extra_entrenador loEntrenador;
                    loActividad = new Extra_actividad(SesionSapei.Sistema);
                    loEntrenador = new Extra_entrenador(SesionSapei.Sistema);
                    string lsPeriodoActivo;
                    string lsPeriodo;
                    string lsPeriodoDescripcion;
                    loGrupos = new Cle_Grupos(SesionSapei.Sistema);
                    lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    lsPeriodoDescripcion = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;

                    ViewData["Tabla"] = loActividad.RegresaTablaActividades();
                    ViewData["Titulo"] = "Lista de Actividades Registradas";
                    ViewData["Encabezados"] = new List<string> { "No." ,"Tipo", "Descripción", "Entrenador", "Capacidad", "Inscritos", "Horario" };

                    if (pbSoloTabla)
                    {
                         return PartialView("../Generales/TablaGeneral");
                    }

                    ViewData["cboTipoActividad"] = loActividad.RegresaComboTipoActividades();
                    ViewData["cboEntrenador"] = loEntrenador.RegresaComboEntrenadores();
                    ViewData["txtPeriodo"] = lsPeriodo;
                    ViewData["txtDescPeriodo"] = lsPeriodoDescripcion;

                    return PartialView("RegistrarActividades");


               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index", "Home");
               }
          }
      
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public JsonResult GuardaActividad1(int piId, string psTipo, string psDescripcion, int piCapacidad, int piEntrenador, string psAula, string psHorario)
          {
               try
               {
                    Extra_actividad loActividad;
                    Extra_actividades_horario loHorario;

                    loActividad = new Extra_actividad(SesionSapei.Sistema);
                    loActividad.Cargar(piId);
                    if (piId == 0)
                    {
                         loActividad.Nuevo();
                         loActividad.inscritos = 0;
                    }
                    loActividad.tipo = psTipo;
                    loActividad.periodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    loActividad.descripcion = psDescripcion;
                    loActividad.id_entrenador = piEntrenador;
                    loActividad.capacidad = piCapacidad;
                    loActividad.Guardar();
                    if (piId == 0)
                         piId = loActividad.RegresaUltimoNuevoID();
                    loHorario = new Extra_actividades_horario(SesionSapei.Sistema);
                    loHorario.aula = psAula;
                    loHorario.GuardaHorario1(piId, psHorario);

                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(Convert.ToString(piId), true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public JsonResult EliminaActividad(int piId)
          {
               try
               {
                    Extra_actividad loActividad;
                    string lsMensaje;
                    loActividad = new Extra_actividad(SesionSapei.Sistema);
                    if (piId == 0)
                    {
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                    lsMensaje = loActividad.EliminaActividad(piId);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public PartialViewResult Entrenadores()
          {
               try
               {
                    Extra_entrenador loEntrenadores;
                    loEntrenadores = new Extra_entrenador(SesionSapei.Sistema);
                    ViewData["Tabla"] = loEntrenadores.RegresaTablaEntrenadores("cboTipoActividades");
                    ViewData["Titulo"] = "Entrenadores Registrados";
                    ViewData["Encabezados"] = new List<string> { "No.", "Nombre", "A. Paterno", "A. Materno", "Estatus", "Usuario", "Contraseña" };
                    return PartialView("Entrenadores");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public JsonResult GuardaEntrenador(int piId, string psNombre, string psPaterno, string psMaterno, bool pbEstatus, string psUsuario, string psContrasenia)
          {
               try
               {
                    Extra_entrenador loEntrenador;
                    loEntrenador = new Extra_entrenador(SesionSapei.Sistema);
                    if (piId == 0)
                    {
                         loEntrenador.Nuevo();
                    }
                    else
                         loEntrenador.Cargar(piId);
                    loEntrenador.nombre = psNombre;
                    loEntrenador.paterno = psPaterno;
                    loEntrenador.materno = psMaterno;
                    loEntrenador.estatus = pbEstatus;
                    loEntrenador.usuario = psUsuario;
                    loEntrenador.contrasenia = psContrasenia;
                    loEntrenador.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Entrenador Agregado", true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public PartialViewResult Inscritos(string psPeriodo = null)
          {
               try
               {
                    string lsPeriodo;
                    string lsCarrera;
                    DataTable loDatos;
                    Extra_actividades_inscrito loInscritos;
                    int liTotalInscritos;
                    int liISI = 0;
                    int liARQ = 0;
                    int liIEL = 0;
                    int liIMC = 0;
                    int liISA = 0;
                    int liPorcentaje = 0;

                    lsPeriodo = psPeriodo;
                    if (string.IsNullOrEmpty(lsPeriodo))
                         lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    loInscritos = new Extra_actividades_inscrito(SesionSapei.Sistema);
                    loDatos = loInscritos.RegresaTablaEstudiantesIncritos(lsPeriodo);

                    liTotalInscritos = loDatos.Rows.Count;

                    foreach (DataRow loDr in loDatos.Rows)
                    {
                         lsCarrera = loDr.RegresaValor<string>("carrera");
                         switch (lsCarrera)
                         {
                              case "ARQ":
                                   liARQ += 1;
                                   break;
                              case "ISA":
                                   liISA += 1;
                                   break;
                              case "ISI":
                                   liISI += 1;
                                   break;
                              case "IEL":
                                   liIEL += 1;
                                   break;
                              case "IMC":
                                   liIMC += 1;
                                   break;
                         }
                    }

                    ViewData["TotalEstudiantes"] = liTotalInscritos;
                    if (liTotalInscritos == 0)
                         liTotalInscritos += 1;
                    liPorcentaje = (liARQ * 100) / (liTotalInscritos);
                    ViewData["TotalARQ"] = liARQ;
                    ViewData["PorcentajeARQ"] = liPorcentaje;

                    liPorcentaje = (liISA * 100) / (liTotalInscritos);
                    ViewData["TotalISA"] = liISA;
                    ViewData["PorcentajeISA"] = liPorcentaje;

                    liPorcentaje = (liISI * 100) / (liTotalInscritos);
                    ViewData["TotalISI"] = liISI;
                    ViewData["PorcentajeISI"] = liPorcentaje;

                    liPorcentaje = (liIMC * 100) / (liTotalInscritos);
                    ViewData["TotalIMC"] = liIMC;
                    ViewData["PorcentajeIMC"] = liPorcentaje;

                    liPorcentaje = (liIEL * 100) / (liTotalInscritos);
                    ViewData["TotalIEL"] = liIEL;
                    ViewData["PorcentajeIEL"] = liPorcentaje;

                    ViewData["Tabla"] = loDatos;
                    ViewData["Titulo"] = "Estudiantes Registrados";
                    ViewData["Encabezados"] = new List<string> { "Periodo", "No. Control", "Nombre","Correo","Genero", "Carrera", "Semestre", "Tipo de Actividad", "Actividad", "Concluida", "Fecha de Registro", "Fecha de Termino" };
                    ViewData["periodo"] = lsPeriodo;
                    ViewData["descPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.Identificacionlarga;
                    return PartialView("Inscritos");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public PartialViewResult InscritosPorActividad(string psPeriodo = null)
          {
               try
               {
                    DataTable loTabla;
                    Extra_actividad loLista;
                    string lsPeriodo;
                    string lsPeriodoDescripcion;
                    loLista = new Extra_actividad(SesionSapei.Sistema);
                    if (string.IsNullOrEmpty(psPeriodo))
                         lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                    else
                         lsPeriodo = psPeriodo;
                    lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();
                    loTabla = loLista.RegresaTablaGruposInscritos(lsPeriodo);
                    ViewData["Tabla"] = loTabla;
                    ViewData["Titulo"] = "Grupos Registrados";
                    ViewData["Encabezados"] = new List<string> { "Tipo", "Descripción", "Capacidad", "Inscritos", "Descargar" };
                    ViewData["periodo"] = lsPeriodo;
                    ViewData["descPeriodo"] = lsPeriodoDescripcion;
                    return PartialView("InscritosPorActividad");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index", "Personal");
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public PartialViewResult BajaActividadEstudiante()
          {
               try
               {
                    Extra_actividades_inscrito loInscrito;
                    loInscrito = new Extra_actividades_inscrito(SesionSapei.Sistema);
                    ViewData["periodo"] = SesionSapei.Sistema.Sesion.Periodo.IdentificacionCorta;
                    ViewData["Titulo"] = "";
                    ViewData["Tabla"] = loInscrito.RegresaTablaActividadesEstudiantes();
                    ViewData["Encabezados"] = new List<string> { "Periodo", "Numero de Control", "Tipo", "Actividad", "Concluida", "Horario", "id" };
                    return PartialView("BajaActividadEstudiante");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public JsonResult BajaActividadEstudianteJSON(string psPeriodo, string psNodeControl, int psId)
          {
               try
               {
                    string lsMensaje;
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    DataTable loDt = new DataTable();
                    System.Data.SqlClient.SqlDataReader loReader;
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {

                         loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                         loParametros.Add(new ParametrosSQL("@no_de_control", psNodeControl));
                         loParametros.Add(new ParametrosSQL("@id", psId));

                         loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_extra_actividades_baja", loParametros);

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
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }

          #region Calificaciones
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.INS)]
          public PartialViewResult CargaCalificacionEstudiantesActividad(string psPeriodo = null)
          {
               try
               {
                    DataTable loTabla;
                    Extra_actividad loLista;

                    string lsPeriodo;
                    string lsPeriodoDescripcion;
                    loLista = new Extra_actividad(SesionSapei.Sistema);
					if (!string.IsNullOrEmpty(psPeriodo))
						lsPeriodo = psPeriodo;
					else
						lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                    lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();

                    loTabla = loLista.RegresaListaActividadesCargadas(lsPeriodo);
                    ViewData["Tabla"] = loTabla;
                    ViewData["Titulo"] = "Lista";
                    ViewData["Encabezados"] = new List<string> { "Registrado", "Clave de actividad", "Descripción", "Inscritos","Descargar Lista","Descargar Resultados" };

                    ViewData["periodo"] = lsPeriodo;
                    ViewData["descPeriodo"] = lsPeriodoDescripcion;
                    return PartialView("CargaCalificacionEstudiantesActividad");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.INS)]
          public ActionResult CargaCalificacionPorActividad()
          {
               string lsTextoArchivo;
               string lsMensaje;
               Stream loSt;
               HttpPostedFileBase loFile;
               Extra_actividad loDatos;
               //Se obtiene la fecha del ultimo dia cargado a base
               try
               {
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {
                         loFile = Request.Files[0];
                         loSt = loFile.InputStream;
                         loSt.Position = 0;
                         System.Data.SqlClient.SqlDataReader loReader;
                         List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                         loDatos = new Extra_actividad(SesionSapei.Sistema);
                         using (StreamReader reader = new StreamReader(loSt, System.Text.Encoding.UTF8))
                         {
                              lsTextoArchivo = reader.ReadToEnd();
                         }

                         if (string.IsNullOrEmpty(lsTextoArchivo))
                         {
                              return Json("");
                         }
                         lsMensaje = loDatos.GuardaRegistros(lsTextoArchivo);
                         if (!string.IsNullOrEmpty(lsMensaje))
                              return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);

                         loParametros.Add(new ParametrosSQL("@periodo", loDatos.periodo));
                         loParametros.Add(new ParametrosSQL("@id", loDatos.id));
                         loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
                         loParametros.Add(new ParametrosSQL("@tipo_usuario", SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString()));
                         loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_extra_carga_calificacion_csv", loParametros);

                         if (loReader.HasRows)
                         {
                              while (loReader.Read())
                              {
                                   lsMensaje = loReader.GetString(0);
                              }
                         }
                         loReader.Close();
                         loReader.Dispose();
                         if (lsMensaje.Trim().Length == 1)
                              return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
                    }
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se generó un error, pongase en contacto con Centro de Cómputo", false);
               }
          }

          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public PartialViewResult CorregirCalificacion()
          {
               try
               {                  
                    return PartialView();
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public JsonResult CorregirCalificacionJson(string psPeriodo, string psControl, int piActividad, float pfCalif)
          {
               try
               {
                    string lsMensaje;
                    Extra_actividades_inscrito loInscrito;
                    loInscrito = new Extra_actividades_inscrito(SesionSapei.Sistema);
                    lsMensaje = loInscrito.CorregirCalificacion(psPeriodo, psControl, piActividad, pfCalif);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
          #endregion
          #region Liberacion
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.INS)]
          public PartialViewResult Liberacion()
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
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.EXT)]
          public JsonResult RegresaActividadesInscritasEstudiante(string psPeriodo, string psNoControl)
          {
               try
               {
                    Extra_actividades_inscrito loInscrito;
                    loInscrito = new Extra_actividades_inscrito(SesionSapei.Sistema);
                    return ManejoMensajesJson.RegresaJsonTabla(loInscrito.RegresaActividadesPorEstudiante(psPeriodo,psNoControl,null,false,false,true),"No hay actividades registradas en este periodo por este estudiante");
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }

          #endregion
     }
}
