
using appSapei.App_Start;
using appSapei.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System.Data;
using appSapei.Clases;
using Sapei.Framework.Utilerias.ExpedienteDigital;
using System.Web.Helpers;
using System.IO;

namespace appSapei.Controllers
{

    public class GeneralesController : Controller
    {
        [SessionExpire]
        /// <summary>
        /// Regresa los datos de domicilio a partir del codigo postal
        /// </summary>
        /// <param name="psCodigoPostal"></param>
        /// <returns></returns>
        public JsonResult RegresaDomicilio(string psCodigoPostal)
        {
            try
            {
                return ManejoMensajesJson.RegresaMensajeJsonDomicilio(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaDomicilioCodigoPostal(psCodigoPostal));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        /// <summary>
        /// Funcion para regresar la lista de entidades federativas de México
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaComboEstadosMexico()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboEstados", "valor != '0'"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        public JsonResult RegresaComboComoEnteraste()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboComoEnteraste"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        public JsonResult RegresaComboTipoActividades(bool pbFiltradas = false)
        {
            string lsFiltro;
            try
            {
                lsFiltro = "";
                if (pbFiltradas)
                    lsFiltro = string.Format("valor not in (select tipo from extra_actividades_inscritos where no_de_control = '{0}' and ((periodo < '{1}' and concluida = 1) OR (periodo = '{1}' and concluida = 0)))", SesionSapei.Sistema.Sesion.Usuario.Usuario, SesionSapei.Sistema.Sesion.Periodo.PeriodoActual);
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboTipoActividades", lsFiltro));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        public JsonResult RegresaComboActividades(string psId, string psPeriodo = null)
        {
            try
            {
                string lsDescripcion;
                string lsFiltro;
                lsDescripcion = " C.descripcion+convert(varchar(max),stuff((SELECT  ', Aula '+B.aula+' ' + B.dia + ' ' + B.hora_inicio + ' - ' + B.hora_fin FROM extra_actividades_horarios B WHERE C.id = B.id_actividad FOR XML PATH('')), 1, 1, '') )";
                if (string.IsNullOrEmpty(psPeriodo))
                    lsFiltro = string.Format("tipo = '{0}' and inscritos < capacidad and periodo = '{1}'", psId, SesionSapei.Sistema.Sesion.Periodo.PeriodoActual);
                else
                    lsFiltro = string.Format("periodo = '{0}'", psPeriodo);
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("extra_actividades", "id", lsDescripcion, lsFiltro, "id", false, 0, "id, descripcion"), "Ya no hay grupos disponibles");
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        public JsonResult RegresaComboEntrenadores()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("extra_entrenador", "id", "nombre + ' ' + paterno + ' ' + materno"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        public JsonResult RegresaComboCarreras(bool pbOpcionTodas = false)
        {
            try
            {
                Carreras loCarreras = new Carreras(SesionSapei.Sistema);
                return ManejoMensajesJson.RegresaJsonTabla(loCarreras.RegresaComboCarreras(pbOpcionTodas));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        public JsonResult RegresaComboCarrerasOfertadas()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboCarrerasOfertadas"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        /// <summary>
        /// Funcion para regresar la lista de carreras con reticula 
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaComboCarreraReticula()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboCarreraReticula"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        /// <summary>
        /// Funcion para regresar la lista de carreras con reticula 
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaComboEspecialidad(string psCarrera, string psReticula)
        {
            try
            {
                string lsFiltro;
                lsFiltro = String.Format(" carrera = '{0}' and reticula = {1}", psCarrera, psReticula);
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("especialidades", "especialidad", "nombre_especialidad", lsFiltro));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        /// <summary>
        /// Funcion para cargar combo periodos
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaComboPeriodo(short piTop = 0, short piFiltro = 0)
        {
            string lsFiltro;
            try
            {
                lsFiltro = "";
                switch (piFiltro)
                {
                    case 0://Todos
                        break;
                    case 1://Sin veranos
                        lsFiltro = "SUBSTRING(periodo,5,1) != '2'";
                        break;
                    case 2: //Solo veranos
                        lsFiltro = "SUBSTRING(periodo,5,1) = '2'";
                        break;
                }
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("periodos_escolares", "periodo", "identificacion_corta", lsFiltro, "periodo desc", false, piTop));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        /// <summary>
        /// Funcion para cargar combo periodos
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaComboPeriodoAdmision()
        {
            string lsCampoDescripcion;
            try
            {
                lsCampoDescripcion = "case (SUBSTRING(periodo,3,1)) when '1' then 'Ene/Jun ' + SUBSTRING(periodo,2,2) else 'Ago/Dic ' + SUBSTRING(periodo,1,2) end";
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("select distinct SUBSTRING(folio,2,3) as periodo from aspirantes", "('20' + periodo)", lsCampoDescripcion, null, null, true));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        /// <summary>
        /// Funcion para regresar la lista de carreras con reticula 
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaComboTipoIngresoPlantel()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboIngresoPlantel"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        /// <summary>
        /// Funcion para regresar la lista opciones de plan de estudios
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaComboPlanEstudios()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboPlanEstudios"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        /// <summary>
        /// Funcion para regresar la lista opciones de nivel escoalr
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaComboNivelEscolar()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboNivelEscolar"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        /// <summary>
        /// Funcion para regresar la lista opciones de estatus del alumno
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaComboEstatusAlumno()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboEstatusAlumno"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }
        [SessionExpire]
        public JsonResult RegresaComboTipoEscuelaProcedencia()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("escuelas_procedencia", "id", "nombre", null, "nombre"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }
        [SessionExpire]
        public JsonResult RegresaComboEstatusAspirante()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("cboEstatusAspirante"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }
        [SessionExpire]
        /// <summary>
        /// Funcion para cargar combo periodos
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaComboServicios()
        {
            try
            {
                return ManejoMensajesJson.RegresaJsonTabla(Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("monto_servicios", "id", "concepto"));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        /// <summary>
        /// Funcion para cargar combo periodos
        /// </summary>
        /// <returns></returns>
        public JsonResult RegresaAulasConHorarios(string psPeriodo = null)
        {
            try
            {
                Cle_Horarios loHorario;
                string lsPeriodo = psPeriodo;
                if (string.IsNullOrEmpty(psPeriodo))
                    lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                loHorario = new Cle_Horarios(SesionSapei.Sistema);
                return ManejoMensajesJson.RegresaJsonTabla(loHorario.RegresaHorarios(lsPeriodo));
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        [SessionExpire]
        public PartialViewResult CambiarContraseña()
        {
            try
            {                
                return PartialView("CambiarContraseña");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }
        [SessionExpire]
        public JsonResult CambiaContraseña(string psActual, string psNueva, string psRNueva)
        {
            try
            {
                string anterior = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.RegresaFirmaPersonalMD5(psActual);
                string comp_actual = SesionSapei.Sistema.Sesion.Usuario.Contraseña;
                if (anterior == comp_actual)
                {
                    string lsMensaje;
                    List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                    System.Data.SqlClient.SqlDataReader loReader;
                    string Nueva = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.RegresaFirmaPersonalMD5(psNueva);
                    string Rnueva = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.RegresaFirmaPersonalMD5(psRNueva);
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {
                        loParametros.Add(new ParametrosSQL("@usuario", SesionSapei.Sistema.Sesion.Usuario.Usuario));
                        loParametros.Add(new ParametrosSQL("@anterior", anterior));
                        loParametros.Add(new ParametrosSQL("@nueva", Nueva));
                        loParametros.Add(new ParametrosSQL("@rnueva", Rnueva));

                        loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_cambio_contrasena", loParametros);
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
                        if (lsMensaje.Trim() == "Ok")
                        {
                            SesionSapei.Sistema.Sesion.Usuario.Contraseña = Nueva;
                            return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Contraseña Actualizada con Éxito", true);
                        }
                        else
                        {
                            
                            return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje,false);
                        }
                    }
                }
                else
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("La contraseña actual es erronea, verifique", false);
                }     
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }
        [SessionExpire]
        public PartialViewResult Contactanos()
        {
            try
            {
                return PartialView("Contactanos");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }

        [SessionExpire]
        public JsonResult EnviaContactanos(string psNombre, string psCorreo, string psAsunto, string psComentarios)
        {
            try
            {
                Sapei.Framework.Utilerias.Funciones.ManejoCorreos.EnviarCorreoContactanosAspirante(SesionSapei.Sistema.Sesion.Usuario.Usuario, psNombre, psCorreo, psAsunto, psComentarios);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Mensaje Enviado", true);
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }
        [HttpPost]
        [SessionExpire]
        public JsonResult GuardaSisCombos(string psValor, string psDescripcion, string psCombo, string psValorPadre = null)
        {
            try
            {
                SisCombo loCombo;
                loCombo = new SisCombo(SesionSapei.Sistema);
                loCombo.combo = psCombo;
                loCombo.descripcion = psDescripcion;
                loCombo.valor = psValor;
                loCombo.valor_padre = psValorPadre;
                loCombo.GuardaActualizaSisCombos();
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Actividad Agregada", true);
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }

        public PartialViewResult Tecnologico()
        {
            return PartialView();
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.DOC, Sapei.Framework.Configuracion.enmRolUsuario.PER)]
        public PartialViewResult PerfilUsuario()
        {
            DataTable loDt = new DataTable();
            try
            {
                string lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loParametros.Add(new ParametrosSQL("@usuario", lsControl));
                    loParametros.Add(new ParametrosSQL("@tipo_usuario", SesionSapei.Sistema.Sesion.Usuario.TipoUsuario.ToString()));

                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_perfil_usuario", loParametros));
                }
                ManejoArchivos loArchivos;

                if (loDt.Rows.Count > 0 && loDt.Rows[0].RegresaValor<int>("estado_foto") == 1)
				{
                    loArchivos = new ManejoArchivos(SesionSapei.Sistema);
                    ViewData["pdfbase64"] = System.Convert.ToBase64String(loArchivos.RegresaDocumentoAlumno(lsControl, 6));
				}
				ViewData["Datos"] = loDt;

                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Home");
            }
        }

		#region Quejasysugerencias

		public PartialViewResult QuejaySugerencia()
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable loDatos, loDatos2;
                    Buzon_Ticket loDatosTicket = new Buzon_Ticket(SesionSapei.Sistema);
                    Buzon_Mensaje loDatosMensaje = new Buzon_Mensaje(SesionSapei.Sistema);
                    loDatos = loDatosTicket.carga_lista_ticket();
                    ViewData["ticket"] = loDatos;
                    loDatos2 = loDatosMensaje.CargaListaMensajes();
                    ViewData["mensaje"] = loDatos2;
                }
                return PartialView("QuejaySugerencia");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }

        public JsonResult ContestaMensajeJSON(int psTicket, string psMensaje)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Buzon_Mensaje loBuzon_Mensaje;
                    loBuzon_Mensaje = new Buzon_Mensaje(SesionSapei.Sistema);
                    Enum psTipoUsuario = SesionSapei.Sistema.Sesion.Usuario.TipoUsuario;
                    loBuzon_Mensaje.respuesta_mensaje(psTicket);
                    loBuzon_Mensaje.inserta_mensaje(psTicket, psMensaje, psTipoUsuario);
                    return ManejoMensajesJson.RegresaJsonTabla(true);
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }
        }

        public PartialViewResult GeneraQS()
        {

            //return PartialView("GeneraQS");

            try
            {
                Buzon_Ticket loBuzonTicket;
                loBuzonTicket = new Buzon_Ticket(SesionSapei.Sistema);

                ViewData["Tabla"] = loBuzonTicket.tablaticket(SesionSapei.Sistema.Sesion.Usuario.Usuario);
                ViewData["Titulo"] = "Lista de Quejas y Sugerencias";
                ViewData["Encabezados"] = new List<string> { "Fecha de apertura", "Asunto", "Estatus", "Ultima actualizacion", "'" };
                DataTable loDatos, loDatos2;
                Buzon_Ticket loDatosTicket = new Buzon_Ticket(SesionSapei.Sistema);
                Buzon_Mensaje loDatosMensaje = new Buzon_Mensaje(SesionSapei.Sistema);
                loDatos = loDatosTicket.carga_lista_ticket_estudiante(SesionSapei.Sistema.Sesion.Usuario.Usuario);
                ViewData["ticket"] = loDatos;
                loDatos2 = loDatosMensaje.CargaListaMensajes();
                ViewData["mensaje"] = loDatos2;


                return PartialView("GeneraQS");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }


        }

        public JsonResult QuejasySugerenciasJSON(string psAsunto, string psMensaje)
        {

            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Buzon_Ticket loBuzon_Ticket;
                    loBuzon_Ticket = new Buzon_Ticket(SesionSapei.Sistema);
                    string psNControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                    String ticket = loBuzon_Ticket.inserta_ticket(psAsunto, psNControl);
                    int psTickete = Convert.ToInt32(ticket);
                    Buzon_Mensaje loBuzon_Mensaje;
                    loBuzon_Mensaje = new Buzon_Mensaje(SesionSapei.Sistema);
                    Enum psTipoUsuario = SesionSapei.Sistema.Sesion.Usuario.TipoUsuario;
                    loBuzon_Mensaje.inserta_mensaje(psTickete, psMensaje, psTipoUsuario);
                    return ManejoMensajesJson.RegresaJsonTabla(true);
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }

        public JsonResult CierraTicketJSON(int psTicket)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Buzon_Ticket loBuzon_Ticket;
                    loBuzon_Ticket = new Buzon_Ticket(SesionSapei.Sistema);
                    loBuzon_Ticket.cierreticket(psTicket);
                    return ManejoMensajesJson.RegresaJsonTabla(true);
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }
        #endregion

        #region Correos
        public JsonResult EnviaCodigoValidacionCorreo(string psNombre, string psCorreo)
        {
            try
            {
                string lsCodigo;
                lsCodigo = Sapei.Framework.Utilerias.Funciones.ManejoCorreos.EnviaCodigoValidacion(psNombre, psCorreo);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsCodigo, true);

            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }

        #endregion

        #region Encuestas
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.ALU)]
        public PartialViewResult RegresaEncuesta(int piTipo)
        {
            try
            {
                ViewData["encuesta"] = Sapei.Encuestas.RegresaHtmlEncuesta(piTipo, SesionSapei.Sistema);
                return PartialView("Encuestas");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }
        #endregion
 
        #region Recontratacion
        [HttpGet]
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]

        public PartialViewResult SolicitudPersonal()
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable loDatos2, loDatos3, loDatos4;
                    string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                    Personal loPersonal = new Personal(SesionSapei.Sistema);
                    Puestos loPuestos = new Puestos(SesionSapei.Sistema);
                    ViewData["Personal"] = Sapei.Framework.Utilerias.Funciones.FuncionesWeb.RegresaElementosSelect("personal", "rfc", "rfc+ ' - ' +apellido_paterno+ ' ' +apellido_materno+ ' ' +nombre_empleado", "status_empleado = '02'", "apellido_paterno", false, 0, null, true);

                    loDatos2 = loPersonal.RegresaClaveArea(psUsuario);
                    loDatos4 = loPuestos.RegresaListaPuestos();

                    ViewData["ClaveArea"] = loDatos2.Rows[0].RegresaValor<string>("clave_area");
                    ViewData["Puestos"] = loDatos4;
                    loDatos3 = loPersonal.RegresaPersonalSolicitadoStatus(psUsuario);

                    ViewData["Titulo"] = "Personal Solicitado";
                    ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Puesto", "Tipo de Contrato", "Inicio de Periodo", "Fin de Periodo", "Estado" };
                    ViewData["Tabla"] = loDatos3;

                    ViewData["Datos"] = loDatos3.Rows.Count.ToString();

                    return PartialView("SolicitudPersonal");

                }

            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public JsonResult RegresaPersonalSolicitud(string psRFC)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loDatos;
                    loDatos = new Personal(SesionSapei.Sistema);
                    DataTable loDt;

                    loDt = loDatos.RegresaPersonalDatosRegistro(psRFC);

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

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public JsonResult RegresaDatosActualizacion(int psID)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loDatos;
                    loDatos = new Personal(SesionSapei.Sistema);
                    DataTable loDt;

                    loDt = loDatos.RegresaPersonalDatosModificacion(psID);

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

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public JsonResult GuardaPersonalSolicitud(string psRFC, int psClavePuesto, string psPeriodoInicio, string psPeriodoFin, string psClaveArea, string psTipoContrato, string psID = null)
        {

            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DateTime lsPeriodoInicio, lsPeriodoFin, lsfecha_creacion;
                    lsfecha_creacion = DateTime.Now;
                    PersonalSoicitud loPersonal_Solicitud;
                    loPersonal_Solicitud = new PersonalSoicitud(SesionSapei.Sistema);
                    if (string.IsNullOrEmpty(psID))
                    {
                        loPersonal_Solicitud.Cargar(Convert.ToInt32(null));
                    }
                    else
                    {
                        loPersonal_Solicitud.Cargar(Convert.ToInt32(psID));
                    }
                    loPersonal_Solicitud.fecha_creacion = lsfecha_creacion;
                    loPersonal_Solicitud.rfc = psRFC;
                    loPersonal_Solicitud.clave_area = psClaveArea;
                    loPersonal_Solicitud.tipo_contrato = psTipoContrato;
                    lsPeriodoInicio = DateTime.Parse(psPeriodoInicio);
                    lsPeriodoFin = DateTime.Parse(psPeriodoFin);
                    loPersonal_Solicitud.clave_puesto = psClavePuesto;
                    loPersonal_Solicitud.periodo_inicio = lsPeriodoInicio;
                    loPersonal_Solicitud.periodo_fin = lsPeriodoFin;
                    loPersonal_Solicitud.id_monto = -1;
                    loPersonal_Solicitud.status = 1;
                    loPersonal_Solicitud.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public JsonResult EliminarSolicitud(int psID)
        {

            try
            {
                string lsMensaje;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                System.Data.SqlClient.SqlDataReader loReader;

                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    if (SesionSapei.Sistema.Sesion.Usuario.RolUsuario == Sapei.Framework.Configuracion.enmRolUsuario.DRH)
                    {
                        string tipo_personal = SesionSapei.Sistema.Sesion.Usuario.RolUsuario.ToString();
                        loParametros.Add(new ParametrosSQL("@id", psID));
                        loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_personal_cancela_solicitud", loParametros);
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
                        if (lsMensaje.Trim() == "delete")
                        {
                            ViewData["Mensaje"] = lsMensaje;
                            return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                        }
                        else if (lsMensaje.Trim() == "NE")
                        {
                            ViewData["Mensaje"] = lsMensaje;
                            return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                        }
                        else
                        {
                            return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                        }
                    }
                    else { 
                    PersonalSoicitud loPersonal_Solicitud;
                    loPersonal_Solicitud = new PersonalSoicitud(SesionSapei.Sistema);
                    loPersonal_Solicitud.Cargar(psID);
                    loPersonal_Solicitud.Eliminar();                
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                
                    }

                }

            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
            }

        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public JsonResult CancelarSolicitud(int psID)
        {
            try
            {
                string lsMensaje;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                System.Data.SqlClient.SqlDataReader loReader;


                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {

                    loParametros.Add(new ParametrosSQL("@id", psID));
                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_personal_cancela_solicitud", loParametros);
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
                    if (lsMensaje.Trim() == "delete")
                    {
                        ViewData["Mensaje"] = lsMensaje;
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else if (lsMensaje.Trim() == "NE")
                    {
                        ViewData["Mensaje"] = lsMensaje;
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        public class ButtonModel
        {
            public string content { get; set; }
            public string cssClass { get; set; }
            public bool isPrimary { get; set; }
        }

        [HttpGet]
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public PartialViewResult HorariosAdministrativos(int psID = 0, string psClavePuesto = null)
        {
            string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
            if (psID == 0)
            {
                DataTable loDatos;
                Personal loPersonal = new Personal(SesionSapei.Sistema);
                loDatos = loPersonal.RegresaPersonalAutorizadoRecontratacion(psUsuario);
                ViewData["Periodo"] = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                ViewData["Titulo"] = "Personal Solicitado";
                ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Departamento que Solicita", "Tipo de Contrato", "Puesto", "Acción" };
                ViewData["Tabla"] = loDatos;
                return PartialView("HorariosAdministrativos");
            }
            else
            {
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                ViewBag.button1 = new ButtonModel { content = "Si", isPrimary = true };
                ViewBag.button2 = new ButtonModel { content = "No" };
                ViewBag.datasource = GetScheduleData(psID, lsPeriodo, psClavePuesto);
                ViewData["ID"] = psID;
                ViewData["Usuario"] = psUsuario;
                ViewData["Puesto"] = psClavePuesto;
                ViewData["RFC"] = "0";
                ViewData["Verificacion"] = "0";
                return PartialView("RegistraHorarioPersonal");
            }
        }

        public PartialViewResult HorarioJefe()
        {
            return PartialView();
        }

            [HttpGet]
        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public PartialViewResult CargaHorarioJefe()
         {
            int psID = 0;
            string psRFC;
            string psClavePuesto = null;
            string lsUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
            string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
            using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
            {
                Personal loPersonal;
                loPersonal = new Personal(SesionSapei.Sistema);
                DataTable loDatos, loDatos2;
                loDatos = loPersonal.RegresaRFCJefe(lsUsuario);
                psRFC = loDatos.Rows[0].RegresaValor<string>("rfc");
                loDatos2 = loPersonal.VerificaExistenciaHorarioJefe(lsPeriodo, psRFC);
                ViewData["Verificacion"] = loDatos2.Rows[0].RegresaValor<string>("resultado");
            }
            
            ViewBag.button1 = new ButtonModel { content = "Si", isPrimary = true };
            ViewBag.button2 = new ButtonModel { content = "No" };
            ViewBag.datasource = GetScheduleData(psID, lsPeriodo, psClavePuesto, psRFC);
            ViewData["ID"] = psID;
            ViewData["RFC"] = psRFC;
            ViewData["Puesto"] = psClavePuesto;
            ViewData["Periodo"] = lsPeriodo;
            return PartialView("RegistraHorarioPersonal");
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public PartialViewResult ControlHorario()
        {
            return PartialView();
        }

            private List<AppointmentData> GetScheduleData(int psID = 0, string psPeriodo = null, string psTipoPersonal = null, string psRFC = "0")
        {

            using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
            {
                Personal loPersonalH;
                loPersonalH = new Personal(SesionSapei.Sistema);

                var loDatos2 = loPersonalH.RegresaHorarioAdministrativo(psID, psPeriodo, psRFC);

                List<AppointmentData> appData = new List<AppointmentData>();
                int i = 1;

                if (psTipoPersonal.NotEmpty())
                {
                    var loDatos = loPersonalH.RegresaHorarioPorHora(psPeriodo, psID);

                    foreach (DataRow row in loDatos.Rows)
                    {
                        int dia = Convert.ToInt32(row[1]);
                        switch (dia)
                        {
                            case 2:
                                appData.Add(new AppointmentData
                                { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString() + '/' + row[7].ToString(), StartTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 1, Convert.ToInt32(row[4]), Convert.ToInt16(row[5]), 0), Grupo = row[7].ToString(), Materia = row[8].ToString(), RFC = row[9].ToString(), Periodo = row[10].ToString(), Selected = row[11].ToString(), IsReadonly = true });
                                break;

                            case 3:
                                appData.Add(new AppointmentData
                                { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString() + '/' + row[7].ToString(), StartTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 2, Convert.ToInt32(row[4]), Convert.ToInt16(row[5]), 0), Grupo = row[7].ToString(), Materia = row[8].ToString(), RFC = row[9].ToString(), Periodo = row[10].ToString(), Selected = row[11].ToString(), IsReadonly = true });
                                break;

                            case 4:
                                appData.Add(new AppointmentData
                                { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString() + '/' + row[7].ToString(), StartTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 3, Convert.ToInt32(row[4]), Convert.ToInt16(row[5]), 0), Grupo = row[7].ToString(), Materia = row[8].ToString(), RFC = row[9].ToString(), Periodo = row[10].ToString(), Selected = row[11].ToString(), IsReadonly = true });
                                break;

                            case 5:
                                appData.Add(new AppointmentData
                                { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString() + '/' + row[7].ToString(), StartTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 4, Convert.ToInt32(row[4]), Convert.ToInt16(row[5]), 0), Grupo = row[7].ToString(), Materia = row[8].ToString(), RFC = row[9].ToString(), Periodo = row[10].ToString(), Selected = row[11].ToString(), IsReadonly = true });
                                break;

                            case 6:
                                appData.Add(new AppointmentData
                                { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString() + '/' + row[7].ToString(), StartTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 5, Convert.ToInt32(row[4]), Convert.ToInt16(row[5]), 0), Grupo = row[7].ToString(), Materia = row[8].ToString(), RFC = row[9].ToString(), Periodo = row[10].ToString(), Selected = row[11].ToString(), IsReadonly = true });
                                break;

                            case 7:
                                appData.Add(new AppointmentData
                                { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString() + '/' + row[7].ToString(), StartTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 6, Convert.ToInt32(row[4]), Convert.ToInt16(row[5]), 0), Grupo = row[7].ToString(), Materia = row[8].ToString(), RFC = row[9].ToString(), Periodo = row[10].ToString(), Selected = row[11].ToString(), IsReadonly = true });
                                break;

                            case 8:
                                appData.Add(new AppointmentData
                                { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString() + '/' + row[7].ToString(), StartTime = new DateTime(2018, 1, 7, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 7, Convert.ToInt32(row[4]), Convert.ToInt16(row[5]), 0), Grupo = row[7].ToString(), Materia = row[8].ToString(), RFC = row[9].ToString(), Periodo = row[10].ToString(), Selected = row[11].ToString(), IsReadonly = true });
                                break;
                        }
                        i++;

                    }
                }
                else
                {
                    var loDatos = loPersonalH.RegresaHorarioSimple(psID, psPeriodo, psRFC);

                    foreach (DataRow row in loDatos.Rows)
                    {
                        int dia = Convert.ToInt32(row[0]);
                        switch (dia)
                        {
                            case 2:
                                appData.Add(new AppointmentData
                                { Id = i, Subject = row[5].ToString(), StartTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 1, Convert.ToInt32(row[3]), Convert.ToInt16(row[4]), 0), IsBlock = true, IsReadonly = false });
                                break;

                            case 3:
                                appData.Add(new AppointmentData
                                { Id = i, Subject = row[5].ToString(), StartTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 2, Convert.ToInt32(row[3]), Convert.ToInt16(row[4]), 0), IsBlock = true, IsReadonly = false });
                                break;

                            case 4:
                                appData.Add(new AppointmentData
                                { Id = i, Subject = row[5].ToString(), StartTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 3, Convert.ToInt32(row[3]), Convert.ToInt16(row[4]), 0), IsBlock = true, IsReadonly = false });
                                break;

                            case 5:
                                appData.Add(new AppointmentData
                                { Id = i, Subject = row[5].ToString(), StartTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 4, Convert.ToInt32(row[3]), Convert.ToInt16(row[4]), 0), IsBlock = true, IsReadonly = false });
                                break;

                            case 6:
                                appData.Add(new AppointmentData
                                { Id = i, Subject = row[5].ToString(), StartTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 5, Convert.ToInt32(row[3]), Convert.ToInt16(row[4]), 0), IsBlock = true, IsReadonly = false });
                                break;

                            case 7:
                                appData.Add(new AppointmentData
                                { Id = i, Subject = row[5].ToString(), StartTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 6, Convert.ToInt32(row[3]), Convert.ToInt16(row[4]), 0), IsBlock = true, IsReadonly = false });
                                break;

                            case 8:
                                appData.Add(new AppointmentData
                                { Id = i, Subject = row[5].ToString(), StartTime = new DateTime(2018, 1, 7, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 7, Convert.ToInt32(row[3]), Convert.ToInt16(row[4]), 0), IsBlock = true, IsReadonly = false });
                                break;
                        }
                        i++;
                    }

                }
                foreach (DataRow row in loDatos2.Rows)
                {
                    int dia = Convert.ToInt32(row[1]);
                    switch (dia)
                    {
                        case 2:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0) });
                            break;

                        case 3:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0) });
                            break;

                        case 4:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0) });
                            break;

                        case 5:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0) });
                            break;

                        case 6:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0) });
                            break;

                        case 7:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0) });
                            break;

                        case 8:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[0]), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 7, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 7, Convert.ToInt32(row[4]), Convert.ToInt16(row[5]), 0) });
                            break;
                    }
                }
                return appData;
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public JsonResult RegistraHorarioJSON(string lsobjeto, int psID, int psPuesto = 0, string psRFC = "0")
        {
            try
            {
                string lsMensaje;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                DataTable loDt = new DataTable();
                System.Data.SqlClient.SqlDataReader loReader;


                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                    loParametros.Add(new ParametrosSQL("@objeto", lsobjeto));
                    loParametros.Add(new ParametrosSQL("@periodo", lsPeriodo));
                    loParametros.Add(new ParametrosSQL("@id", psID));
                    loParametros.Add(new ParametrosSQL("@puesto", psPuesto));
                    loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_registra_horario_personal", loParametros);
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
                    else
                    {

                        if (psRFC == "0")
                        {
                            List<ParametrosSQL> loParametros2 = new List<ParametrosSQL>();
                            loParametros2.Add(new ParametrosSQL("@id", psID));
                            loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_genera_id_horario_personal", loParametros2);
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
                            else
                            {
                                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                            }
                        }
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public JsonResult EliminaHorarioJSON(string psIdHorario, int psID)
        {
            try
            {
                string lsMensaje;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                DataTable loDt = new DataTable();
                System.Data.SqlClient.SqlDataReader loReader;
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loParametros.Add(new ParametrosSQL("@idHorario", psIdHorario));
                    loParametros.Add(new ParametrosSQL("@id", psID));

                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_elimina_horario_personal", loParametros);
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
                    if (lsMensaje.Trim() == "delete")
                    {
                        ViewData["Mensaje"] = lsMensaje;
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else if (lsMensaje.Trim() == "NE")
                    {
                        ViewData["Mensaje"] = lsMensaje;
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public JsonResult FirmaHorarioPersonal(string psFirma, int psID = 0, int psStatus = 0)
        {

            try
            {
                string firmaCifrada = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.RegresaFirmaPersonalMD5(psFirma);

                if (firmaCifrada.Trim() != SesionSapei.Sistema.Sesion.Usuario.Contraseña)
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se reconoce la contraseña", false);
                }

                if(psID == 0)
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
                else { 
                    using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                    {
                        string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                        PersonalSoicitud loPersonal_Solicitud;
                        loPersonal_Solicitud = new PersonalSoicitud(SesionSapei.Sistema);
                        loPersonal_Solicitud.RegistraCadenaFIEL(psID, psUsuario, enmTiposDocumentos.HorarioPersonal, psStatus);
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true); 
                    }
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeAlertError();
            }
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public PartialViewResult FirmaContrato(string psRFC = null, string psFechaCreacion = null, string psClaveArea = null, string psTipoContrato = null)
        {
          /*  try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                    Personal loPersonal = new Personal(SesionSapei.Sistema);
                    DataTable loDatos, loDatos2;
                    if (string.IsNullOrEmpty(psRFC))
                    {
                        ViewData["CSS2"] = "div-hide"; 
                        loDatos = loPersonal.RegresaDatosFirmaContratoJefe(psUsuario);
                        ViewData["Titulo"] = "Contratos de Personal";
                        ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "Tipo de Contrato", "Acción" };
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
                    }
                    */
                    return PartialView("FirmaContrato");
             /*   }
            }
            catch (Exception ex)

            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return PartialView("Index");
            }*/
        }

        [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DDM, Sapei.Framework.Configuracion.enmRolUsuario.CDI, Sapei.Framework.Configuracion.enmRolUsuario.CYD, Sapei.Framework.Configuracion.enmRolUsuario.DAC, Sapei.Framework.Configuracion.enmRolUsuario.DDA, Sapei.Framework.Configuracion.enmRolUsuario.DEP, Sapei.Framework.Configuracion.enmRolUsuario.DIR, Sapei.Framework.Configuracion.enmRolUsuario.DRF, Sapei.Framework.Configuracion.enmRolUsuario.DRH, Sapei.Framework.Configuracion.enmRolUsuario.ECO, Sapei.Framework.Configuracion.enmRolUsuario.ESC, Sapei.Framework.Configuracion.enmRolUsuario.EXT, Sapei.Framework.Configuracion.enmRolUsuario.GTV, Sapei.Framework.Configuracion.enmRolUsuario.MAT, Sapei.Framework.Configuracion.enmRolUsuario.PLA, Sapei.Framework.Configuracion.enmRolUsuario.SAD, Sapei.Framework.Configuracion.enmRolUsuario.SGI, Sapei.Framework.Configuracion.enmRolUsuario.SUB, Sapei.Framework.Configuracion.enmRolUsuario.DOC, Sapei.Framework.Configuracion.enmRolUsuario.PER)]        
        public JsonResult FirmaContratoGeneral(string psFirma, string psFechaCreacion, string psRFC, string psClaveArea, string psTipoContrato, int psStatus)
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
                    string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                    PersonalSoicitud loPersonal_Solicitud;
                    loPersonal_Solicitud = new PersonalSoicitud(SesionSapei.Sistema);
                    loPersonal_Solicitud.RegistraCadenaFIELContrato(psFechaCreacion, psRFC, psClaveArea, psTipoContrato, psUsuario, enmTiposDocumentos.Contrato, psStatus);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeAlertError();
            }
        }

        [HttpGet]
        [SessionExpire( Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public PartialViewResult VisorHorariosJefes(string psRFC, string psPeriodo)
        {
            try
            {     
                ViewBag.datasource = GetScheduleData(0, psPeriodo, null, psRFC);
                ViewData["V_Jefes"] = "1";
                return PartialView("../Generales/VisorSchedule");
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                ViewData["mensaje"] = "Error al Generar Horario";
                return PartialView("AvisosGenerales", "Generales");
            }
        }

        [SessionExpire( Sapei.Framework.Configuracion.enmRolUsuario.SUB)]
        public JsonResult LiberaHorarioJefes(string psRFC, string psPeriodo)
        {

            try
            {
                DataTable loDt = new DataTable();
                string lsMensaje;
                System.Data.SqlClient.SqlDataReader loReader;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo)); 
                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_personal_libera_horario_jefes", loParametros);
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

                    if (lsMensaje.Trim() == "1")
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Horario Liberado", true);
                    }
                    else 
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Error Al Liberar Horario", false);
                    }
                }
            }

            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeAlertError();
            }
        }
        //

        public PartialViewResult ActividadesApoyo()
        {
            try
            {
                string lsControl = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                string lsUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loPersonal = new Personal(SesionSapei.Sistema);
                    ViewData["Titulo"] = "Personal Solicitado";
                    ViewData["Encabezados"] = new List<string> { "RFC", "Nombre de Personal", "N. de Horas Plaza", "Acción" };
                    ViewData["Tabla"] = loPersonal.RegresaPesonalSeleccionActividadesApoyo(lsPeriodo, lsUsuario);
                }
                
                return PartialView();
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }

        public PartialViewResult CargaActividadesApoyo(string psRFC, string psVisualizar = null)
        {
            try
            {
                ViewBag.DialogButtons1 = new ButtonModel() { cssClass = "e-primary", content = "OK" };
                string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActualSinVerano;
                string lsUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                DataTable loDatosClases = new DataTable();
                DataTable loDatosActividadesApoyo = new DataTable();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    Personal loPersonal;
                    loPersonal = new Personal(SesionSapei.Sistema);
                    loDatosClases = loPersonal.RegresaGruposPersonalActividadesApoyo(psRFC, lsPeriodo);
                    loDatosActividadesApoyo = loPersonal.RegresaPesonalActividadesApoyo(psRFC, lsPeriodo);
                    AppointmentData loHorarioApoyo;
                    loHorarioApoyo = new AppointmentData();
                    ViewBag.datasource = loHorarioApoyo.RegresaCalendarioActividadesApoyo(loDatosClases, loDatosActividadesApoyo, psRFC, lsPeriodo);
                    ViewData["objeto"] = loHorarioApoyo.RegresaActividadesApoyo();
                    ViewData["rfc"] = psRFC;
                    ViewData["periodo"] = lsPeriodo;
                }
                if (lsUsuario == "RecursosHumanos" || psVisualizar == "1")
                {
                    ViewData["RH"] = "S";
                    if (psVisualizar == "1") 
                    {
                        ViewData["User"] = "S";
                    }
                    else
                    {
                        ViewData["User"] = "N";
                    }
                }
                else
                {
                    ViewData["User"] = "N";
                    ViewData["RH"] = "N";
                }
                return PartialView("CargaActividadesApoyo");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }


        public JsonResult RegistraHorarioActividadesApoyoJSON(string lsobjeto, string psRFC, string psPeriodo)
        {
            try
            {
                string lsMensaje;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                DataTable loDt = new DataTable();
                System.Data.SqlClient.SqlDataReader loReader;


                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loParametros.Add(new ParametrosSQL("@objeto", lsobjeto));
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loParametros.Add(new ParametrosSQL("@rfc", psRFC));
                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_registra_horario_actividades_apoyo", loParametros);
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
                    else
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        public JsonResult EliminaHorarioActividadApoyoJSON(string psIdHorario, string psPeriodo, string psRFC)
        {
            try
            {
                string lsMensaje;
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                DataTable loDt = new DataTable();
                System.Data.SqlClient.SqlDataReader loReader;
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loParametros.Add(new ParametrosSQL("@idHorario", psIdHorario));
                    loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                    loParametros.Add(new ParametrosSQL("@rfc", psRFC));

                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_elimina_horario_actividad_apoyo", loParametros);
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
                    if (lsMensaje.Trim() == "delete")
                    {
                        ViewData["Mensaje"] = lsMensaje;
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else if (lsMensaje.Trim() == "NE")
                    {
                        ViewData["Mensaje"] = lsMensaje;
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        public JsonResult LiberaActividadesApoyo(string psRFC, string psPeriodo)
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
                    loParametros.Add(new ParametrosSQL("@rfc", psRFC));

                    loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_libera_horario_actividad_apoyo", loParametros);
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
                    if (lsMensaje.Trim() == "delete")
                    {
                        ViewData["Mensaje"] = lsMensaje;
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, true);
                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
            }
        }

        #endregion

        #region DatosGeneralesTrabajador

        public PartialViewResult DatosGeneralesTrabajador(string psRFC = null)
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable loDatos;
                    string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                    Personal loPersonal = new Personal(SesionSapei.Sistema);
                    loDatos = loPersonal.RegresaDatosParaPersonal(psUsuario);
                    ViewData["RFC"] = loDatos.Rows[0].RegresaValor<string>("rfc");
                    ViewData["NTarjeta"] = loDatos.Rows[0].RegresaValor<string>("no_tarjeta");
                    ViewData["CURP"] = loDatos.Rows[0].RegresaValor<string>("curp");
                    ViewData["Nombre"] = loDatos.Rows[0].RegresaValor<string>("nombre_empleado");
                    ViewData["ApeP"] = loDatos.Rows[0].RegresaValor<string>("apellido_paterno");
                    ViewData["ApeM"] = loDatos.Rows[0].RegresaValor<string>("apellido_materno");
                    ViewData["DepAd"] = loDatos.Rows[0].RegresaValor<string>("adscripcion");
                    ViewData["DepAc"] = loDatos.Rows[0].RegresaValor<string>("area_academica");
                    ViewData["NSS"] = loDatos.Rows[0].RegresaValor<string>("NSS");
                    ViewData["Genero"] = loDatos.Rows[0].RegresaValor<string>("genero");
                    ViewData["FechaNac"] = loDatos.Rows[0].RegresaValor<string>("fecha_nacimiento");
                    ViewData["EdoCiv"] = loDatos.Rows[0].RegresaValor<string>("estado_civil");
                    ViewData["Nacion"] = loDatos.Rows[0].RegresaValor<string>("nacionalidad");
                    ViewData["EdoNac"] = loDatos.Rows[0].RegresaValor<string>("estado_nacimiento");
                    ViewData["NivelEst"] = loDatos.Rows[0].RegresaValor<string>("nivel_estudios");
                    ViewData["Carrera"] = loDatos.Rows[0].RegresaValor<string>("nombre_carrera");
                    ViewData["FechaTit"] = loDatos.Rows[0].RegresaValor<string>("fecha_titulacion");
                    ViewData["Cedula"] = loDatos.Rows[0].RegresaValor<string>("cedula_profesional");
                    ViewData["Correo"] = loDatos.Rows[0].RegresaValor<string>("correo_electronico");
                    ViewData["Tel"] = loDatos.Rows[0].RegresaValor<string>("telefono");
                    ViewData["TelEm"] = loDatos.Rows[0].RegresaValor<string>("telefono_emergencia");
                    ViewData["txtCalle"] = loDatos.Rows[0].RegresaValor<string>("calle");
                    ViewData["txtNoDomicilio"] = loDatos.Rows[0].RegresaValor<string>("numero");
                    ViewData["txtCodPostal"] = loDatos.Rows[0].RegresaValor<string>("id_cp");

                    return PartialView("DatosGeneralesTrabajador");
                }
            }

            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }

        public PartialViewResult ComboGeneral(string psId)
        {
            using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
            {
                Plaza loPlaza = new Plaza(SesionSapei.Sistema);
                ViewData["datos"] = loPlaza.RegresaSubUnidad(psId);
                return PartialView("ComboGeneral");
            }
        }

        #endregion

        #region FiEl
        #endregion

    }
}