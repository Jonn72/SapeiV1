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
using Syncfusion;
namespace appSapei.Controllers
{
     public class DesarrolloAcademicoController : Controller
     {

          #region Aspirantes
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public PartialViewResult Registro()
          {
               try
               {
                    ViewData["Valida"] = "1";
                    return PartialView();
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public PartialViewResult EstadisticasAspirantes()
          {
               try
               {
                    DataTable loDt;
                    Aspirante loAspirantes;
                    Aspirantes_Periodos loPeriodo = new Aspirantes_Periodos(SesionSapei.Sistema);
                    int liTotalAspirantes;
                    int liAspirantesCarrera;
                    int liPorcentaje;
                    string lsPeriodo;
                    lsPeriodo = loPeriodo.RegresaPeriodoActivo();
                    loAspirantes = new Aspirante(SesionSapei.Sistema);
                    loDt = loAspirantes.CargarEstadisticas(lsPeriodo);
                    liTotalAspirantes = loDt.Rows[0].RegresaValor<int>("totalAspirantes");
                    ViewData["TotalAspirantes"] = liTotalAspirantes;

                    liAspirantesCarrera = loDt.Rows[0].RegresaValor<int>("ARQ");
                    liPorcentaje = (liAspirantesCarrera * 100) / (liTotalAspirantes + 1);
                    ViewData["TotalARQ"] = liAspirantesCarrera;
                    ViewData["PorcentajeARQ"] = liPorcentaje;

                    liAspirantesCarrera = loDt.Rows[0].RegresaValor<int>("ISA");
                    liPorcentaje = (liAspirantesCarrera * 100) / (liTotalAspirantes + 1);
                    ViewData["TotalISA"] = liAspirantesCarrera;
                    ViewData["PorcentajeISA"] = liPorcentaje;

                    liAspirantesCarrera = loDt.Rows[0].RegresaValor<int>("ISI");
                    liPorcentaje = (liAspirantesCarrera * 100) / (liTotalAspirantes + 1);
                    ViewData["TotalISI"] = liAspirantesCarrera;
                    ViewData["PorcentajeISI"] = liPorcentaje;

                    liAspirantesCarrera = loDt.Rows[0].RegresaValor<int>("IMC");
                    liPorcentaje = (liAspirantesCarrera * 100) / (liTotalAspirantes + 1);
                    ViewData["TotalIMC"] = liAspirantesCarrera;
                    ViewData["PorcentajeIMC"] = liPorcentaje;

                    liAspirantesCarrera = loDt.Rows[0].RegresaValor<int>("IEL");
                    liPorcentaje = (liAspirantesCarrera * 100) / (liTotalAspirantes + 1);
                    ViewData["TotalIEL"] = liAspirantesCarrera;
                    ViewData["PorcentajeIEL"] = liPorcentaje;

                    //Datos Tabla en Modal
                    loDt = loAspirantes.CargarVistaDatosCompletosTabla(lsPeriodo.Substring(2, 3));
                    ViewData["Titulo"] = "Lista de Aspirantes";
                    ViewData["Tabla"] = loDt;
                    ViewData["Encabezados"] = new List<string> { "Folio", "Nombre", "CURP", "Sexo", "Estatus", "Teléfono de casa", "Celular", "Carrera 1" };

                    return PartialView("EstadisticasAspirantes");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public PartialViewResult CambiarEstatus()
          {
               try
               {
                    return PartialView("CambiarEstatus");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public PartialViewResult ActivarPeriodo(string psPeriodo)
          {
               try
               {
                    DataTable loTabla;
                    Aspirantes_Periodos loPeriodos;
                    string lsPeriodo;
                    loPeriodos = new Aspirantes_Periodos(SesionSapei.Sistema);

                    loTabla = loPeriodos.RegresaTablaPeriodos();

                    if (loTabla.Rows.Count == 0)
                    {
                         return PartialView("ActivarPeriodo");
                    }
                    ViewData["txtPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    lsPeriodo = loTabla.Rows[0].RegresaValor<string>("periodo");
                    ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
                    ViewData["Tabla"] = loTabla;
                    ViewData["Titulo"] = "Periodos Registrados";
                    ViewData["Encabezados"] = new List<string> { "Periodo", "Vuelta", "Inicio Registro", "Fin Registro", "Fecha Examen", "Activo" };
                    ViewData["txtIniRegistro"] = loTabla.Rows[0].RegresaValor<string>("ini_registro");
                    ViewData["txtFinRegistro"] = loTabla.Rows[0].RegresaValor<string>("fin_registro");
                    ViewData["txtFechaExamen"] = loTabla.Rows[0].RegresaValor<string>("fecha_examen");
                    ViewData["cbxActivo"] = loTabla.Rows[0].RegresaValor<string>("activo");
                    ViewData["cbxActivo"] = loTabla.Rows[0].RegresaValor<string>("activo");
                    ViewData["cbxActivo"] = loTabla.Rows[0].RegresaValor<string>("activo");
                    return PartialView("ActivarPeriodo");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public JsonResult GuardaPeriodo(string psPeriodo, byte piVuelta, string psFechaExamen, string psIniRegistro, string psFinRegistro, bool pbActivo)
          {
               try
               {
                    Aspirantes_Periodos loPeriodo;
                    string lsPeriodo;
                    loPeriodo = new Aspirantes_Periodos(SesionSapei.Sistema);
                    lsPeriodo = psPeriodo;
                    if (!string.IsNullOrEmpty(lsPeriodo))
                         if (Convert.ToInt32(lsPeriodo) < Convert.ToInt32(SesionSapei.Sistema.Sesion.Periodo.PeriodoActual))
                              ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se puede generar periodos pasados al actual del sistema", false);

                    loPeriodo.Cargar(lsPeriodo, piVuelta);
                    if (loPeriodo.EOF)
                    {
                         loPeriodo.periodo = lsPeriodo;
                         loPeriodo.vuelta = piVuelta;
                    }
                    loPeriodo.ini_registro = Convert.ToDateTime(psIniRegistro);
                    loPeriodo.fin_registro = Convert.ToDateTime(psFinRegistro);
                    loPeriodo.fecha_examen = Convert.ToDateTime(psFechaExamen);
                    loPeriodo.activo = pbActivo;
                    loPeriodo.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
          #endregion
          #region Tutorias
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
          public PartialViewResult ActivarPeriodoTutorias(string psPeriodo = null)
          {
               try
               {
                    DataTable loTabla;
                    Tutorias_Periodos loPeriodos;
                    string lsPeriodo;
                    loPeriodos = new Tutorias_Periodos(SesionSapei.Sistema);
                    lsPeriodo = psPeriodo;
                    if (string.IsNullOrEmpty(psPeriodo))
                    {
                         lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    }
                    loTabla = loPeriodos.RegresaTablaPeriodos();
                    ViewData["txtPeriodo"] = lsPeriodo;
                    ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
                    ViewData["Tabla"] = loTabla;
                    ViewData["Titulo"] = "Periodos Registrados";
                    ViewData["Encabezados"] = new List<string> { "Periodo", "Inicio Registro Grupos", "Fin Registro Grupos", "Inicio Selección", "Fin Selección", "Inicio Captura", "Fin Captura" };
                    if (loTabla.Rows.Count == 0)
                    {
                         return PartialView();
                    }
                    foreach (DataRow loFila in loTabla.Rows)
                    {
                          if(loFila.RegresaValor<string>("periodo") == lsPeriodo)
                         {
                              ViewData["txtFechaIniGrupos"] = loTabla.Rows[0].RegresaValor<DateTime>("inicio_grupos");
                              ViewData["txtFechaFinGrupos"] = loTabla.Rows[0].RegresaValor<DateTime>("fin_grupos");
                              ViewData["txtFechaIni"] = loTabla.Rows[0].RegresaValor<DateTime>("inicio_seleccion");
                              ViewData["txtFechaFin"] = loTabla.Rows[0].RegresaValor<DateTime>("fin_seleccion");
                              ViewData["txtFechaIniCaptura"] = loTabla.Rows[0].RegresaValor<DateTime>("inicio_captura");
                              ViewData["txtFechaFinCaptura"] = loTabla.Rows[0].RegresaValor<DateTime>("fin_captura");
                              break;
                         }
                    }
                    
                    return PartialView();
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
          public JsonResult GuardaPeriodoTutorias(string psPeriodo, string psIniGrupos,string psFinGrupos,string psInicio,string psFin,string psInicioCaptura,string psFinCaptura)
          {
               try
               {
                    Tutorias_Periodos loActividad;
                    loActividad = new Tutorias_Periodos(SesionSapei.Sistema);
                    string lsPeriodo = psPeriodo;
                    loActividad.Cargar(lsPeriodo);
                    if (loActividad.EOF)
                    {
                         loActividad.periodo = lsPeriodo;
                    }
                    loActividad.inicio_grupos = Convert.ToDateTime(psIniGrupos);
                    loActividad.fin_grupos = Convert.ToDateTime(psFinGrupos).UltimaHoraDia();
                    loActividad.inicio_seleccion = Convert.ToDateTime(psInicio);
                    loActividad.fin_seleccion = Convert.ToDateTime(psFin).UltimaHoraDia();
                    loActividad.inicio_captura = Convert.ToDateTime(psInicioCaptura);
                    loActividad.fin_captura = Convert.ToDateTime(psFinCaptura).UltimaHoraDia();
                    loActividad.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public PartialViewResult RegistrarGrupos(string psPeriodo = null, bool pbSoloTabla = false)
          {
               try
               {
                    Tutorias_Grupos loGrupos;
                    Tutores loTutores;
                    loTutores = new Tutores(SesionSapei.Sistema);
                    string lsPeriodo;
                    string lsPeriodoDescripcion;
                    loGrupos = new Tutorias_Grupos(SesionSapei.Sistema);
                    lsPeriodo = psPeriodo;
                    if(string.IsNullOrEmpty(psPeriodo))
                         lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                    lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();

                    ViewData["Tabla"] = loGrupos.RegresaTablaGrupos(lsPeriodo);
                    ViewData["Titulo"] = "Lista de Grupos Registrados";
                    ViewData["Encabezados"] = new List<string> {"Grupo", "Tutor", "Capacidad", "Inscritos", "Horario"};

                    if (pbSoloTabla)
                    {
                         return PartialView("../Generales/TablaGeneral");
                    }

                    ViewData["cboTutores"] = loTutores.RegresaComboTutores();
                    ViewData["txtPeriodo"] = lsPeriodo;
                    ViewData["txtDescPeriodo"] = lsPeriodoDescripcion;

                    return PartialView();


               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index", "Home");
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
          public JsonResult GuardaGrupoTutoria(string psPeriodo, string psGrupo, short piCapacidad, string psTutor, string psAula, string psHorario)
          {
               try
               {
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {
                         int liValor = 0;
                         System.Data.SqlClient.SqlDataReader loReader;
                         List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                         loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                         loParametros.Add(new ParametrosSQL("@grupo", psGrupo));
                         loParametros.Add(new ParametrosSQL("@rfc", psTutor));
                         loParametros.Add(new ParametrosSQL("@capacidad", piCapacidad));
                         loParametros.Add(new ParametrosSQL("@aula", psAula));
                         loParametros.Add(new ParametrosSQL("@horario", psHorario));
                         loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_registra_grupo_tutoria", loParametros);
                         if (loReader.HasRows)
                         {
                              while (loReader.Read())
                              {
                                   liValor = loReader.GetInt32(0);
                              }
                         }
                         loReader.Close();
                         loReader.Dispose();
                         if (liValor == 1)
                              return ManejoMensajesJson.RegresaMensajeJsonBusqueda(Convert.ToString(psGrupo), true);
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se puede registrar el grupo, hay cruce de horarios del docente", false);
                    }

                   
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
		public JsonResult ActualizaGrupoTutoriaTutor(string psPeriodo, string psGrupo,string psTutor, short piCapacidad)
		{
			try
			{
				Tutorias_Grupos loActividad;
				loActividad = new Tutorias_Grupos(SesionSapei.Sistema);
				loActividad.Cargar(psPeriodo, psGrupo);
				loActividad.capacidad = piCapacidad;
				loActividad.rfc = psTutor;
				loActividad.Guardar();
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
			}
		}
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP)]
          public JsonResult EliminaTutoria(string psPeriodo, string psGrupo)
          {
               try
               {
                    Tutorias_Grupos loActividad;
                    Tutorias_Horarios loHorario;
                    loActividad = new Tutorias_Grupos(SesionSapei.Sistema);
                    loHorario = new Tutorias_Horarios(SesionSapei.Sistema);
                    loActividad.Cargar(psPeriodo, psGrupo);
                    loHorario.BorraHorarios(psPeriodo, psGrupo);
                    loActividad.Eliminar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public PartialViewResult Tutores()
          {
               try
               {
                    Tutores loTutores;
                    loTutores = new Tutores(SesionSapei.Sistema);
                    ViewData["Tabla"] = loTutores.RegresaTablaTutores();
                    ViewData["Titulo"] = "Tutores Registrados";
                    ViewData["Encabezados"] = new List<string> { "RFC", "Nombre","Fecha de ingreso" ,"Estatus" };
                    return PartialView();
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public JsonResult GuardaTutor(string psRFC, bool pbEstatus)
          {
               try
               {
                    Tutores loTutor;
                    loTutor = new Tutores(SesionSapei.Sistema);
                    Acceso loPersonal;
                    loPersonal = new Acceso(SesionSapei.Sistema);
                    loPersonal.Cargar(psRFC);
                    if (loPersonal.EOF)
                    {
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                    loTutor.Cargar(psRFC);
                    if (loTutor.EOF)
                    {
                         loTutor.rfc = psRFC;
                    }
                    loTutor.estatus = pbEstatus;
                    loTutor.fecha_registro = DateTime.Now;
                    loTutor.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public PartialViewResult InscritosTutorias(string psPeriodo = null)
          {
               try
               {
                    string lsPeriodo;
                    string lsCarrera;
                    DataTable loDatos;
                    Tutorias_Inscritos loInscritos;
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
                    loInscritos = new Tutorias_Inscritos(SesionSapei.Sistema);
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
                    ViewData["titulo_pagina"] = "Estudiantes en Tutorías";
                    ViewData["url"] = "DesarrolloAcademico/InscritosTutorias";
                    ViewData["url_estadistica"] = "RegresaEstadisticasInscritosTutorias";
                    
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
                    ViewData["Encabezados"] = new List<string> { "Periodo","Grupo", "No. Control", "Nombre", "Carrera", "Semestre","Fecha de Registro", "Fecha de Termino","Promedio","Desempeño","Tutor" };
                    ViewData["periodo"] = lsPeriodo;
                    ViewData["descPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
                    ViewData["cboEstadisticas"] = new List<string> { "Semestre", "Carrera"};
                    return PartialView("../Generales/Estadisticas");
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index");
               }
          }
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public PartialViewResult InscritosTutoriasGrupos(string psPeriodo = null)
          {
               try
               {
                    DataTable loTabla;
                    Tutorias_Grupos loLista;
                    Tutorias_Periodos loPeriodos;
                    string lsPeriodoActivo;
                    string lsPeriodo;
                    string lsPeriodoDescripcion;
                    loLista = new Tutorias_Grupos(SesionSapei.Sistema);
                    loPeriodos = new Tutorias_Periodos(SesionSapei.Sistema);
                    if (string.IsNullOrEmpty(psPeriodo))
                         lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                    else
                         lsPeriodo = psPeriodo;
                    lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();
                    lsPeriodoActivo = loPeriodos.ValidaPeriodoImpresionGruposInscritos(lsPeriodo, lsPeriodoDescripcion);
                    if (!string.IsNullOrEmpty(lsPeriodoActivo))
                    {
                         ViewData["Mensaje"] = lsPeriodoActivo;
                         return PartialView("../Generales/AvisosGenerales");
                    }
                    loTabla = loLista.RegresaTablaGruposInscritos(lsPeriodo);
                    ViewData["Tabla"] = loTabla;
                    ViewData["Titulo"] = "Grupos Registrados";
                    ViewData["Encabezados"] = new List<string> { "Grupo", "Tutor", "Capacidad", "Inscritos", "Descargar" };
                    ViewData["periodo"] = lsPeriodo;
                    ViewData["descPeriodo"] = lsPeriodoDescripcion;
                    return PartialView();
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index", "Personal");
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DOC)]
          public PartialViewResult CargaCalificacion()
          {
               try
               {
                    DataTable loTabla;
                    Tutorias_Grupos loLista;

                    string lsPeriodo;
                    string lsPeriodoDescripcion;
                    loLista = new Tutorias_Grupos(SesionSapei.Sistema);
					lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    lsPeriodoDescripcion = lsPeriodo.RegresaDescripcionPeriodo();

                    loTabla = loLista.RegresaListaGruposCargados(lsPeriodo);
                    ViewData["Tabla"] = loTabla;
                    ViewData["Titulo"] = "Lista";
                    ViewData["Encabezados"] = new List<string> { "Registrado", "Grupo", "Inscritos", "Descargar Lista", "Descargar Resultados" };

                    ViewData["periodo"] = lsPeriodo;
                    ViewData["descPeriodo"] = lsPeriodoDescripcion;
                    return PartialView();
               }
               catch (Exception ex)
               {
                    SesionSapei.Sistema.GrabaLog(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DOC)]
          public ActionResult CargaCalificacionJson()
          {
               string lsTextoArchivo;
               string lsMensaje;
               Stream loSt;
               HttpPostedFileBase loFile;
               Tutorias_Inscritos loDatos;
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
                         loDatos = new Tutorias_Inscritos(SesionSapei.Sistema);
                         using (StreamReader reader = new StreamReader(loSt, System.Text.Encoding.UTF8))
                         {
                              lsTextoArchivo = reader.ReadToEnd();
                         }

                         if (string.IsNullOrEmpty(lsTextoArchivo))
                         {
                              return Json("");
                         }
                         lsMensaje = loDatos.GuardaRegistrosTutorias(lsTextoArchivo);
                         if (!string.IsNullOrEmpty(lsMensaje))
                              return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);

                         loParametros.Add(new ParametrosSQL("@periodo", loDatos.periodo));
					loParametros.Add(new ParametrosSQL("@grupo", loDatos.grupo));
                         loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
                         loParametros.Add(new ParametrosSQL("@tipo_usuario", SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString()));
                         loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_tutorias_carga_calificacion_csv", loParametros);

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

          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public PartialViewResult CorregirCalificacionTutoria()
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
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public JsonResult CorregirCalificacionJson(string psPeriodo,string psControl, string psGrupo, float pfCalif)
          {
               try
               {
                    Tutorias_Inscritos loInscrito;
                    loInscrito = new Tutorias_Inscritos(SesionSapei.Sistema);
                    loInscrito.Cargar(psPeriodo, psControl, psGrupo);
                    loInscrito.promedio = pfCalif;
                    loInscrito.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public JsonResult RegresaTutoriaEstudiante(string psNoControl)
          {
               try
               {
                    DataTable loData;
                    Tutorias_Inscritos loInscrito;
                    loInscrito = new Tutorias_Inscritos(SesionSapei.Sistema);
                    loData = loInscrito.RegresaTablaEstudiantesIncritos(psNoControl,true);
                    if(loData.Rows.Count == 0)
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    return ManejoMensajesJson.RegresaJsonTabla(loData);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DOC)]
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

		#endregion

		#region Evaluacion Docente

          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public PartialViewResult FechaEvaluacionDocente()
          {
            try
            {
                DataTable loTabla, loTabla2;
                fecha_evaluacion loPeriodos;
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                loPeriodos = new fecha_evaluacion(SesionSapei.Sistema);
                ViewData["txtPeriodo"] = lsPeriodo;
                ViewData["txtDescPeriodo"] = SesionSapei.Sistema.Sesion.Periodo.Identificacionlarga;
                loTabla = loPeriodos.RegresaFechaPeriodoEvaluacion(lsPeriodo);

                if (loTabla.Rows.Count == 0)
                {
                    ViewData["vista1"] = "";
                    ViewData["vista2"] = "block";
                    ViewData["vista3"] = "none";

                    return PartialView("FechaEvaluacionDocente");
                }

                string f1 = loTabla.Rows[0].RegresaValor<string>("fecha_inicio");

                DateTime ini = DateTime.Parse(f1, System.Globalization.CultureInfo.GetCultureInfo(101));

                DateTime now = DateTime.Now;

                if (now >= ini)
                {
                    loTabla2 = loPeriodos.VerificaEvaluacion(lsPeriodo);
                    if (loTabla2.Rows.Count > 0)
                    {
                        ViewData["vista1"] = "disabled=\"disabled\"";
                        ViewData["vista2"] = "none";
                        ViewData["vista3"] = "block";
                        ViewData["txtFechaInicio"] = loTabla.Rows[0].RegresaValor<string>("fecha_inicio");
                        ViewData["txtFechaFin"] = loTabla.Rows[0].RegresaValor<string>("fecha_fin");
                        return PartialView("FechaEvaluacionDocente");
                    }
                }

                ViewData["vista1"] = "";
                ViewData["vista2"] = "block";
                ViewData["vista3"] = "none";
                ViewData["txtFechaInicio"] = loTabla.Rows[0].RegresaValor<string>("fecha_inicio");
                ViewData["txtFechaFin"] = loTabla.Rows[0].RegresaValor<string>("fecha_fin");

                return PartialView("FechaEvaluacionDocente");


            }
            catch (Exception ex)
              {
                  SesionSapei.Sistema.GrabaLog(ex);
                  return PartialView("Index", "Home");
              }
          }

          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
          public JsonResult GuardaFechaJSON(string psPeriodo, string psFechaInicio, string psFechaFin)
          {
              try
              {
                  DateTime lsFechaInicio, lsFechaFin;
                  fecha_evaluacion loFechas;
                  loFechas = new fecha_evaluacion(SesionSapei.Sistema);
                  loFechas.Cargar(psPeriodo, "A");
                  if (loFechas.EOF)
                  {
                      loFechas.periodo = psPeriodo;
                    loFechas.encuesta = "A";
                  }

                  lsFechaInicio = DateTime.Parse(psFechaInicio);
                  lsFechaFin = DateTime.Parse(psFechaFin);
                  loFechas.fecha_inicio = lsFechaInicio;
                  loFechas.fecha_fin = lsFechaFin;
                  loFechas.Guardar();
                  return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);

              }
              catch (Exception ex)
              {
                  Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                  return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
              }
          }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
        public PartialViewResult VerificaEvaluacionDocente(string psNoControl = null)
        {
            if (string.IsNullOrEmpty(psNoControl))
            {
                ViewData["vista1"] = "block";
                ViewData["vista2"] = "none";
                return PartialView("VerificaEvaluacionDocente");
            }

            DataTable datos, datos2;
            fecha_evaluacion lotabla, lodatos;
            string psPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
            lotabla = new fecha_evaluacion(SesionSapei.Sistema);
            lodatos = new fecha_evaluacion(SesionSapei.Sistema);
            datos = lodatos.Evaluacion(psPeriodo, psNoControl);
            ViewData["Titulo"] = "Evaluaciones Realizadas";
            ViewData["Encabezados"] = new List<string> { "Periodo", "Fecha" };
            datos2 = lotabla.TablaEvaluacion(psNoControl);
            datos2.Columns.Remove("periodo");
            ViewData["Tabla"] = datos2;
            ViewData["Nombre"] = datos.Rows[0].RegresaValor<string>("nombre");
            ViewData["Carrera"] = datos.Rows[0].RegresaValor<string>("carrera");
            ViewData["Semestre"] = datos.Rows[0].RegresaValor<string>("semestre");
            ViewData["Status"] = datos.Rows[0].RegresaValor<string>("evaluacion");
            ViewData["vista1"] = "none";
            ViewData["vista2"] = "block";
            return PartialView("VerificaEvaluacionDocente");
            
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDA)]
        public PartialViewResult LiberacionEvaluacionDocente(string psNoControl = null)
        {
            try
            {
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                ViewData["periodo_desc"] = lsPeriodo.RegresaDescripcionPeriodo();
                da_liberacion_evaluacion_docente loLiberacion;
                loLiberacion = new da_liberacion_evaluacion_docente(SesionSapei.Sistema);
                if (string.IsNullOrEmpty(psNoControl))
                {

                    ViewData["Titulo"] = "Evaluaciones Realizadas";
                    ViewData["Encabezados"] = new List<string> { "Periodo", "Numero de Control", "Nombre del Estudiante", "Fecha de liberacion", "" };
                    ViewData["Tabla"] = loLiberacion.RegresaListaAlumnosLiberados(lsPeriodo);
                }
                else
                {
                    ViewData["DatosEstudiante"] = loLiberacion.RegresaDatosEvaluacionAlumno(lsPeriodo, psNoControl);
                    ViewData["display"] = "display:none";
                }

                return PartialView("LiberacionEvaluacionDocente");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        public JsonResult EliminarLiberacion(string psPeriodo, string psNoControl)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    da_liberacion_evaluacion_docente loLiberacion;
                    loLiberacion = new da_liberacion_evaluacion_docente(SesionSapei.Sistema);
                    loLiberacion.Cargar(psPeriodo, psNoControl);
                    loLiberacion.Eliminar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }

        public JsonResult FirmaLiberacionEvaluacionDocente(string psFirma, string psNoControl, string psMotivo)
        {

            try
            {
                string firmaCifrada = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.RegresaFirmaPersonalMD5(psFirma);

                if (firmaCifrada.Trim() != SesionSapei.Sistema.Sesion.Usuario.Contraseña)
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se reconoce la contraseña", false);
                }
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    string psPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                    da_liberacion_evaluacion_docente loLiberacion;
                    loLiberacion = new da_liberacion_evaluacion_docente(SesionSapei.Sistema);
                    loLiberacion.Cargar(psPeriodo, psNoControl);
                    if (loLiberacion.EOF)
                    {
                        loLiberacion.periodo = psPeriodo;
                        loLiberacion.no_de_control = psNoControl;
                    }
                    loLiberacion.motivo = psMotivo;
                    loLiberacion.fecha = DateTime.Today;
                    loLiberacion.Guardar();

                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeAlertError();
            }
        }
        #endregion



        public PartialViewResult ResultadosEvaluacion()
        {
            return PartialView("ResultadosEvaluacion");
        }

        public PartialViewResult ListaEspecifica(string psOpc, string psPeriodo = null, string psDepto = null)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    DataTable loDt = new DataTable();
                    string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                    loParametros.Add(new ParametrosSQL("@opc", psOpc));
                    ViewData["periodo"] = lsPeriodo;
                    switch (psOpc)
                    {

                        case "cr":
                            ViewData["Titulo"] = "Lista de carreras evaluadas";
                            ViewData["Encabezados"] = new List<string> { "Carrera", "" };
                            loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                            break;

                        case "dp":
                            ViewData["Titulo"] = "Lista de departamentos evaluados";
                            ViewData["Encabezados"] = new List<string> { "Departamento", "" };
                            loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                            break;

                        case "mt":
                            ViewData["Titulo"] = "Lista de departamentos con materias evaluadas";
                            ViewData["Encabezados"] = new List<string> { "Departamento", "" };
                            loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                            break;

                        case "mt2":
                            ViewData["Titulo"] = "Lista de materias evaluadas";
                            
                            ViewData["Encabezados"] = new List<string> { "Clave de Materia", "N° de Grupos", "Nombre de la Materia", "Departamento", "" };
                            loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                            loParametros.Add(new ParametrosSQL("@depto", psDepto));
                            break;

                        case "dc":
                            ViewData["Titulo"] = "Lista de departamentos con docentes evaluados";
                            ViewData["Encabezados"] = new List<string> { "Departamento", "" };
                            loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                            break;

                        case "dc2":
                            ViewData["Titulo"] = "Lista de docentes evaluados";

                            ViewData["Encabezados"] = new List<string> { "RFC", "Apellidos", "Nombre", "Departamento", "" };
                            loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                            loParametros.Add(new ParametrosSQL("@depto", psDepto));
                            break;
                    }

                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_lista_dependiente", loParametros));
                    ViewData["Tabla"] = loDt;
                    return PartialView("../Generales/TablaGeneral");
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                ViewData["mensaje"] = "Error al descargar reporte";
                return PartialView("AvisosGenerales", "Generales");
            }
        }

        public PartialViewResult PorCarrera(string psPeriodo, string psCarrera, string psReticula)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    ViewData["periodo_desc"] = psPeriodo.RegresaDescripcionPeriodo();
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    DataTable loDt = new DataTable();
                    DataTable loDt2 = new DataTable();
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loParametros.Add(new ParametrosSQL("@carrera", psCarrera));
                    loParametros.Add(new ParametrosSQL("@reticula", int.Parse(psReticula)));

                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_encabezado_evl_carrera", loParametros));
                    loDt2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_evl_carrera", loParametros));

                    ViewData["encabezado"] = loDt;
                    ViewData["evaluacion"] = loDt2;

                    return PartialView("PorCarrera");
                }
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        public PartialViewResult PorDepartamento(string psPeriodo, string psDepto)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    ViewData["periodo_desc"] = psPeriodo.RegresaDescripcionPeriodo();
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    DataTable loDt = new DataTable();
                    DataTable loDt2 = new DataTable();
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loParametros.Add(new ParametrosSQL("@clave_area", psDepto));

                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_encabezado_evl_depto", loParametros));
                    loDt2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_evl_depto", loParametros));

                    ViewData["encabezado"] = loDt;
                    ViewData["evaluacion"] = loDt2;
                    ViewData["periodo"] = psPeriodo;
                    ViewData["depto"] = psDepto;

                    return PartialView("PorDepartamento");
                }
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        public PartialViewResult PorDocente(string psPeriodo, string psRFC)
        {
            try
            {
                //--pac_personal_datos_registro
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    ViewData["periodo_desc"] = psPeriodo.RegresaDescripcionPeriodo();
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    DataTable loDt = new DataTable();
                    DataTable loDt2 = new DataTable();
                    DataTable loDt3 = new DataTable();
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loParametros.Add(new ParametrosSQL("@rfc", psRFC));

                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_encabezado_evl_docente", loParametros));
                    loDt2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_evl_docente", loParametros));
                    loParametros.RemoveAt(0);

                    loDt3.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_datos_registro", loParametros));

                    ViewData["nombre_docente"] = loDt3.Rows[0].RegresaValor<string>("nombre_empleado") + " " + loDt3.Rows[0].RegresaValor<string>("apellido_paterno") + " " + loDt3.Rows[0].RegresaValor<string>("apellido_materno");


                    ViewData["encabezado"] = loDt;
                    ViewData["evaluacion"] = loDt2;

                    return PartialView("PorDocente");
                }
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        public PartialViewResult PorMateria(string psPeriodo, string psMateria)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    ViewData["periodo_desc"] = psPeriodo.RegresaDescripcionPeriodo();
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    DataTable loDt = new DataTable();
                    DataTable loDt2 = new DataTable();
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loParametros.Add(new ParametrosSQL("@materia", psMateria));

                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_encabezado_evl_materia", loParametros));
                    loDt2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_evl_materia", loParametros));

                    Materias loMateria = new Materias(SesionSapei.Sistema);

                    loMateria.Cargar(psMateria);
                    ViewData["nombre_materia"] = loMateria.nombre_completo_materia;
                    ViewData["encabezado"] = loDt;
                    ViewData["evaluacion"] = loDt2;

                    return PartialView("PorMateria");
                }
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }

        public PartialViewResult PorPlantel(string psPeriodo)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    ViewData["periodo_desc"] = psPeriodo.RegresaDescripcionPeriodo();
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    DataTable loDt = new DataTable();
                    DataTable loDt2 = new DataTable();
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));

                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_encabezado_evl_plantel", loParametros));
                    loDt2.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_da_evl_plantel", loParametros));

                    ViewData["encabezado"] = loDt;
                    ViewData["evaluacion"] = loDt2;

                    return PartialView("PorPlantel");
                }
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index", "Home");
            }
        }


        /*public void ExportChart(string Data, string ChartModel)
        {
            string path = "";
            //ChartProperties obj = ConvertChartObject(ChartModel);
            //string type = obj.ExportSettings.Type.ToString().ToLower();
            //string fileName = obj.ExportSettings.FileName;
            //string orientation = obj.ExportSettings.Orientation.ToString();
            Data = Data.Remove(0, Data.IndexOf(',') + 1);
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(Data));
            using (FileStream fs = new FileStream(path + "CoreChart.png", FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(Data);
                    bw.Write(data);
                    bw.Dispose();
                }

            }
        }
        */

        /*public void GeneraPDF(string Data, string ChartModel)
        {
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.WebKit);

            WebKitConverterSettings settings = new WebKitConverterSettings();

            //HTML string and Base URL 

            string baseUrl = @"C:/Temp/HTMLFiles/";

            //Set WebKit path
            settings.WebKitPath = @"/QtBinaries/";

            //Assign WebKit settings to HTML converter
            htmlConverter.ConverterSettings = settings;

            //Convert HTML string to PDF
            PdfDocument document = htmlConverter.Convert(htmlText, baseUrl);

            //Save and close the PDF document 
            document.Save("Output.pdf");

            document.Close(true);
        }
        */
       
    }
}
