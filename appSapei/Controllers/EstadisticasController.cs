using appSapei.App_Start;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
     public class EstadisticasController : Controller
     {
          [SessionExpire]
          public JsonResult RegresaEstadisticasAspirantes(string psId)
          {
               string lsFiltro;
               string lsTablaConsulta;
               try
               {
                    lsFiltro = "SUBSTRING(folio,2,3) = (select substring(max(periodo),3,3) from aspirantes_periodos)";
                    switch (psId)
                    {
                         case "1":
                              lsFiltro += " AND ciudad is not null";
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("aspirantes_datos_completos", "ciudad",null, lsFiltro));
                         case "2":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("aspirantes_datos_completos", "sexo",null,lsFiltro));
                         case "3":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("aspirantes_escuela_procedencia", "(select nombre from escuelas_procedencia where id = id_escuela)", "id_escuela", lsFiltro));
                         case "4":

                              lsTablaConsulta = "select (cast(datediff(dd,fechaNacimiento,GETDATE()) / 365.25 as int)) as edad  from aspirantes_datos_completos where " + lsFiltro;
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica(lsTablaConsulta, "edad",1,0));
                         case "5":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("aspirantes_datos_solicitud", "(select descripcion from sisCombos where combo = 'cboComoEnteraste' and valor = enteraste)", "enteraste",lsFiltro));
                         default:
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboEstatusAlumno"));

                    }

               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [SessionExpire]
          public JsonResult RegresaEstadisticasActividadesExtraescolares(string psPeriodo, string psId)
          {
               string lsFiltro;
               try
               {
                    lsFiltro = string.Format("periodo = {0}", psPeriodo);
                    switch (psId)
                    {
                         case "1":                            
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("estudiantes_datos_extraescolares", "tipo", null, lsFiltro));
                         case "2":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("estudiantes_datos_extraescolares", "actividad", null, lsFiltro));
                         case "3":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("estudiantes_datos_extraescolares", "semestre", null, lsFiltro));
                         case "4":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("estudiantes_datos_extraescolares", "carrera", null, lsFiltro));
                         default:
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("estudiantes_datos_extraescolares", "tipo", null, lsFiltro));

                    }

               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }

          public JsonResult RegresaEstadisticasGnosis(string psId)
          {
              try
              {
                  switch(psId)
                  {
                      case "1":
                          return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("[192.168.9.245].[ITTGnosis].[dbo].[auth_logs_movil]", "ip_conexion","ip_conexion", null,2,15,"valor desc"));
                      case "2":
                          return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("[192.168.9.245].[ITTGnosis].[dbo].[auth_logs_movil]", "ultima_sesion", 2, 10));
                      case "3":
                          return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("[192.168.9.245].[ITTGnosis].[dbo].[publicacion]", "(select nombre_departamento from [192.168.9.245].[ITTGnosis].[dbo].[departamento] where token=token_publicacion)", "token_publicacion", " Tabla.token_publicacion in(select token from [192.168.9.245].[ITTGnosis].[dbo].[departamento])",2,100,null));
                  }
                  return null;
              }
              catch (Exception ex)
              {
                  Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                  return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
              }
          }

          [SessionExpire]
          public JsonResult RegresaEstadisticasInscritosTutorias(string psPeriodo, string psId)
          {
               string lsFiltro;
               try
               {
                    lsFiltro = string.Format("periodo = {0}", psPeriodo);

                    switch (psId)
                    {
                         case "1":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("tutorias_inscritos", "semestre", null, lsFiltro));
                         case "2":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("select periodo, (select carrera from alumnos where no_de_control = tutorias_inscritos.no_de_control) carrera from tutorias_inscritos", "carrera", null, lsFiltro, 1));

                    }
                    return null;
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }

          [SessionExpire]
          public JsonResult RegresaEstadisticasEncuestasServicios(string psPeriodo, string psId)
          {
               string[] lasID;
               string lsPeriodo;
               try
               {
                    lasID = psId.Split(new char[] { '-' });
                    lsPeriodo = String.Format("periodo = '{0}'",psPeriodo);
                    switch (lasID[0])
                    {
                         case "1":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("select (select nombre from en_encuesta where id = id_encuesta) encuesta ,id_encuesta  from (select distinct no_de_control, id_encuesta from alumnos_encuestas where periodo = '"+psPeriodo+"')A", "encuesta", "encuesta", null, 1));
                         case "2":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("select (select nombre from en_encuesta where id = id_encuesta) encuesta ,id_encuesta  from (select distinct no_de_control, id_encuesta from alumnos_encuestas where periodo = '" + psPeriodo + "' AND no_de_control in (select no_de_control from alumnos where carrera = '" + lasID[1] + "'))A", "encuesta", "encuesta", null, 1));
                         case "3":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("alumnos_encuestas", "(select pregunta from en_pregunta where id_pregunta = alumnos_encuestas.id_pregunta)", "id_pregunta", "id_encuesta = " + lasID[1] + " and id_pregunta <> '10' and periodo = '" + psPeriodo + "'", 0, 0, "id_pregunta", "round(sum(convert(float,id_respuesta))/(select count(distinct no_de_control)  from alumnos_encuestas where id_encuesta = " + lasID[1] + " ),2)"));
                         case "4":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("alumnos_encuestas", "(select pregunta from en_pregunta where id_pregunta = alumnos_encuestas.id_pregunta)", "id_pregunta", "id_encuesta = " + lasID[2] + " and id_pregunta <> '10' AND no_de_control in (select no_de_control from alumnos where carrera = '" + lasID[1] + "') AND periodo = '" + psPeriodo + "'", 0, 0, "id_pregunta", "round(sum(convert(float,id_respuesta))/(select count(distinct no_de_control)  from alumnos_encuestas where id_encuesta = " + lasID[2] + " AND no_de_control in (select no_de_control from alumnos where carrera = '" + lasID[1] + "')),2)"));
                         case "5":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("alumnos_encuestas", "(select valor from en_respuesta where id = id_respuesta)", "id_respuesta", lsPeriodo + String.Format(" and id_encuesta = {0} and id_pregunta <> 10 and SUBSTRING(CONVERT(char(3),id_pregunta+1),LEN(TRIM(CONVERT(char(3),id_pregunta))),1) = '{1}'", lasID[1], lasID[2])));
                         case "6":
                              return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaEstadistica("alumnos_encuestas", "(select valor from en_respuesta where id = id_respuesta)", "id_respuesta", lsPeriodo + String.Format(" and id_encuesta = {1} and and id_pregunta <> 10 SUBSTRING(CONVERT(char(3),id_pregunta+1),LEN(TRIM(CONVERT(char(3),id_pregunta))),1) = '{2}' and no_de_control in (select no_de_control from alumnos where carrera = '{0}')", lasID[1], lasID[2], lasID[3])));
                             
                    }
                    return null;
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
     }
}
