using appSapei.App_Start;
using Sapei;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class SGIController : Controller
    {
         #region Activa Encuestas
         /// <summary>
         /// ActivarPeriodo
         /// </summary>
         /// <returns></returns>
         [HttpGet]
         [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI)]
         public PartialViewResult ActivarPeriodo()
         {
              try
              {
                   DataTable loTabla;
                   En_Periodos loPeriodos;
                   string lsPeriodo;
                   loPeriodos = new En_Periodos(SesionSapei.Sistema);
                   lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                   loTabla = loPeriodos.RegresaTablaPeriodos();
                   ViewData["txtPeriodo"] = lsPeriodo;
                   ViewData["txtDescPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();
                   ViewData["Tabla"] = loTabla;
                   ViewData["Titulo"] = "Periodos Registrados";
                   ViewData["Encabezados"] = new List<string> { "Periodo", "Inicio", "Fin" };
                   if (loTabla.Rows.Count == 0)
                   {
                        return PartialView("ActivarPeriodo");
                   }
                   if (loTabla.Rows[0].RegresaValor<string>("periodo") == lsPeriodo)
                   {
                        ViewData["txtIniRegistro"] = loTabla.Rows[0].RegresaValor<string>("inicio");
                        ViewData["txtFinRegistro"] = loTabla.Rows[0].RegresaValor<string>("fin");
                   }
                   return PartialView("ActivarPeriodo");
              }
              catch (Exception ex)
              {
                   SesionSapei.Sistema.GrabaLog(ex);
                   return PartialView("Index", "Home");
              }
         }
         [HttpPost]
         [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI)]
         public JsonResult GuardaPeriodo(DateTime psIniRegistro, DateTime psFinRegistro)
         {
              try
              {
                   En_Periodos loControl;
                   loControl = new En_Periodos(SesionSapei.Sistema);
                   string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                   loControl.Cargar(lsPeriodo);
                   if (loControl.EOF)
                   {
                        loControl.periodo = lsPeriodo;
                   }
                   loControl.inicio = psIniRegistro;
                   loControl.fin = psFinRegistro.UltimaHoraDia();
                   loControl.Guardar();
                   return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
              }
              catch (Exception ex)
              {
                   SesionSapei.Sistema.GrabaLog(ex);
                   return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
              }
         }
         [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI)]
         public PartialViewResult EstadisticasEncuestas(string psPeriodo)
         {
              try
              {
                   string lsPeriodo;
                   string lsCarrera;
                   DataTable loDatos;
                   Alumnos_Encuesta loInscritos;
                   int liTotalInscritos;
                   int liISI = 0;
                   int liARQ = 0;
                   int liIEL = 0;
                   int liIMC = 0;
                   int liISA = 0;
                   int liCOP = 0;
                   int liIFE = 0;
                   int liADM = 0;
                   int liPorcentaje = 0;

                   lsPeriodo = psPeriodo;
                   if (string.IsNullOrEmpty(lsPeriodo))
                        lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                   loInscritos = new Alumnos_Encuesta(SesionSapei.Sistema);
                   loDatos = loInscritos.RegresaDatosEncuestaCarrea(lsPeriodo);
                   liTotalInscritos = 0;
                   foreach (DataRow loDr in loDatos.Rows)
                   {
                        lsCarrera = loDr.RegresaValor<string>("carrera");
                        liTotalInscritos += loDr.RegresaValor<int>("total");
                        switch (lsCarrera)
                        {
                             case "ARQ":
                                  liARQ = loDr.RegresaValor<int>("total");
                                  break;
                             case "ISA":
                                  liISA = loDr.RegresaValor<int>("total");
                                  break;
                             case "ISI":
                                  liISI = loDr.RegresaValor<int>("total");
                                  break;
                             case "IEL":
                                  liIEL = loDr.RegresaValor<int>("total");
                                  break;
                             case "IMC":
                                  liIMC = loDr.RegresaValor<int>("total");
                                  break;
                             case "COP":
                                  liCOP = loDr.RegresaValor<int>("total");
                                   break;
                             case "ADM":
                                  liADM = loDr.RegresaValor<int>("total");
                                    break;
                             case "IFE":
                                  liIFE = loDr.RegresaValor<int>("total");
                                   break;
                    }
                   }
                   ViewData["titulo_pagina"] = "Encuestas realizadas";
                   ViewData["url"] = "SGI/EstadisticasEncuestas";
                   ViewData["url_estadistica"] = "RegresaEstadisticasEncuestasServicios";

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

                   liPorcentaje = (liCOP * 100) / (liTotalInscritos);
                   ViewData["TotalCOP"] = liCOP;
                   ViewData["PorcentajeCOP"] = liPorcentaje;

                   liPorcentaje = (liADM * 100) / (liTotalInscritos);
                   ViewData["TotalADM"] = liADM;
                   ViewData["PorcentajeADM"] = liPorcentaje;

                   liPorcentaje = (liIFE * 100) / (liTotalInscritos);
                   ViewData["TotalIFE"] = liIFE;
                   ViewData["PorcentajeIFE"] = liPorcentaje;

                   ViewData["Tabla"] = loInscritos.RegresaDatosGeneralesEncuesta(lsPeriodo); ;
                   ViewData["Titulo"] = "Estudiantes Registrados";
                   ViewData["Encabezados"] = new List<string> { "Encuesta", "Pregunta", "Respuesta"};
                   ViewData["periodo"] = lsPeriodo;
                   ViewData["descPeriodo"] = lsPeriodo.RegresaDescripcionPeriodo();

                   ViewData["cboFiltros"] = true;

                   ViewData["cboFiltro1"] = new List<string> { "1-Todas","ARQ-Arquitectura", "IEL-Ing. Electrónica", "ISA-Ing. en Sis. Automotrices", "ISI-Ing. en Sistemas Computacionales", "IMC-Ing. Mecatrónica" , "COP-Lic. Contabilidad","ADM-Lic. Administración", "IFE-Ing.Ferroviaria"  };
                   ViewData["txtFiltro1"] = "Carrera";

                   ViewData["cboFiltro2"] = new List<string> { "0-Todas", "1-Servicio Social", "2-Centro de Información", "3-Centro de Cómputo", "4-Coordinación de Carreras", "5-Recursos Financieros", "6-Residencias Profesionales", "7-Servicios Escolares" };
                   ViewData["txtFiltro2"] = "Encuestas";

                   ViewData["cboFiltro3"] = new List<string> { "0-Todas", "1-1", "2-2", "3-3", "4-4", "5-5", "6-6", "7-7", "8-8","9-9"};
                   ViewData["txtFiltro3"] = "Pregunta";
                   return PartialView();
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
