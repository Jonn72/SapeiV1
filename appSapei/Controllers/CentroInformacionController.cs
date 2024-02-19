using appSapei.App_Start;
using Newtonsoft.Json;
using Sapei;
using Sapei.Framework.Configuracion;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace appSapei.Controllers
{


     public class CentroInformacionController : Controller
     {
          #region Catalogos
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Libros()
          {
               try
               {
                    Bib_Libros loLibro = new Bib_Libros(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "Clave", "ISBN", "No. Páginas", "Capítulos", "Titulo", "Material", "Edición", "Editar" };
                    ViewData["Titulo"] = "Tabla de libros";
                    ViewData["Tabla"] = loLibro.RegresaLibros();
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }

          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Revistas()
          {
               try
               {
                    Bib_Revistas loCDs = new Bib_Revistas(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "id", "Titulo", "Secciones", "Fecha de publicación", "idMaterial", "Edición", "Editar" };
                    ViewData["Titulo"] = "Tabla de libros";
                    ViewData["Tabla"] = loCDs.RegresaRevistas();
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }

          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Tesis()
          {
               try
               {
                    Bib_Tesis loTesis = new Bib_Tesis(SesionSapei.Sistema);

                    ViewData["Encabezados"] = new List<string> { "id", "Título", "Fecha de publicacion", "Paginas", "Material", "Editar" };
                    ViewData["Titulo"] = "Tesis";
                    ViewData["Tabla"] = loTesis.RegresaTesis();
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          #endregion
          #region Adeudo
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Adeudos(string psUsuario)
          {
               try
               {
                    Bib_Adeudos loAdeudos = new Bib_Adeudos(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "Clave A", "Clave P", "Usuario", "Fecha de entrega", "Fecha limite", "Monto", "Liquidado", "Dias de retardo" };

                    ViewData["Titulo"] = "Adeudos";
                    ViewData["Tabla"] = loAdeudos.RegresaAdeudos(psUsuario);

                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Adeudo()
          {
               try
               {
                    Bib_Adeudos loAdeudos = new Bib_Adeudos(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "Clave A", "Clave P", "Usuario", "Fecha de entrega", "Fecha limite", "Monto", "Liquidado", "Dias de retardo" };
                    ViewData["Titulo"] = "Adeudos";
                    return PartialView("Adeudos");
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          #endregion
          #region Prestamo
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Prestamo()
          {
               try
               {
                    Bib_Prestamos loPrestamo = new Bib_Prestamos(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "Clave", "Título", "Fecha de prestamo", "Fecha limite", "Usuario", "Clave", "Acción" };
                    ViewData["Titulo"] = "Prestamos";
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }

          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult ConsultaPrestamoJson(string psUsuario)
          {
               try
               {
                    Bib_Prestamos loPrestamo = new Bib_Prestamos(SesionSapei.Sistema);
                    ViewData["Adeudo"] = false;
                    if (loPrestamo.VerificaAdeudo(psUsuario) > 0)
                    {
                         ViewData["usuario"] = psUsuario;
                         ViewData["Adeudo"] = true;
                    }

                    DataTable prestamos = loPrestamo.VerificaPrestamo(psUsuario);//verifica si el usuario tiene prestamos pendientes
                    ViewData["Encabezados"] = new List<string> { "Clave", "Título", "Fecha de prestamo", "Fecha limite", "Usuario", "Clave", "Acción" };
                    ViewData["Titulo"] = "Prestamos";
                    ViewData["Tabla"] = prestamos;

                    return PartialView("Prestamo");
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }

          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult GuardaPrestamoJson(string psUsuario, string psEjemplar, string psFechaEntrega)
          {
               try
               {
                    Bib_Ejemplares loEjemplar = new Bib_Ejemplares(SesionSapei.Sistema);


                    loEjemplar.Cargar(psEjemplar);
                    if (!loEjemplar.EOF)
                    {//existe el ejemplar

                         //si el ejemplar no se encuentra disponible; ya está prestado
                         if (!loEjemplar.EjemplarDisponible(psEjemplar))
                         {
                              return ManejoMensajesJson.RegresaMensajeJsonBusqueda("El ejemplar ya se encuentra prestado, verifica la clave!!!", false);
                         }

                         Bib_Prestamos loPrestamo = new Bib_Prestamos(SesionSapei.Sistema);
                         Acceso loAcceso = new Acceso(SesionSapei.Sistema);
                         loAcceso.Cargar(psUsuario);

                         //validar la clave del usuario si existe
                         DateTime loHoy = DateTime.Now;
                         DateTime loLimite = Convert.ToDateTime(psFechaEntrega);

                         //numero de prestamos permitidos
                         int loNumPrestamos = 0;
                         switch (loAcceso.tipo_usuario)
                         {
                              case "DOC":
                                   loNumPrestamos = 3;
                                   break;
                              default://estudiante
                                   loNumPrestamos = 2;
                                   break;
                         }
                         //si es reserva se entrega el mismo dia
                         if (loEjemplar.Reserva)
                         {
                              loLimite = loHoy;
                         }

                         bool loPrestamosPendientes = loPrestamo.PrestamosPendientes(psUsuario) < loNumPrestamos;
                         bool loFechaValida = loHoy <= loLimite;
                         bool loAdeudos = loPrestamo.VerificaAdeudo(psUsuario) == 0;

                         //si tiene menos de numero de prestamos permitidos
                         //y la fecha limite no es antes de la actual
                         if (loPrestamosPendientes && loFechaValida && loAdeudos)
                         {

                              loPrestamo.Cargar(0);
                              if (loPrestamo.EOF)
                              {
                                   loPrestamo.Nuevo();
                                   loPrestamo.Usuario = psUsuario;
                                   loPrestamo.Id_ejemplar = loEjemplar.Id_ejemplar;
                                   loPrestamo.Id_prestador = (int)Sapei.Framework.Configuracion.enmRolUsuario.CDI;
                                   loPrestamo.F_prestamo = loHoy;//DateTime.Now;
                                   loPrestamo.F_limite = loLimite;//Convert.ToDateTime(psFechaEntrega);
                                   loPrestamo.Periodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                                   loPrestamo.Guardar();
                                   return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);

                              }
                         }
                         else
                         {
                              string msg = "";
                              if (!loPrestamosPendientes)
                              {
                                   msg = "El usuario tiene el limite de prestaciones permitidas <br>";
                              }
                              if (!loAdeudos)//tiene adeudos
                              {
                                   msg = msg + " El usuario tiene adeudos pendientes <br>";
                              }
                              if (!loFechaValida)//tiene adeudos
                              {
                                   msg = msg + "Verifca la fecha limite";
                              }
                              return ManejoMensajesJson.RegresaMensajeJsonBusqueda(msg, false);
                         }
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda("La clave del ejemplar no existe", false);
                    }
                    else
                    {

                    }

               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Hubo un problema al realizar el prestamo", false);
               }
               return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Hubo un problema al realizar el prestamo", false);
          }

          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult DevolucionEjemplarJson(string piId)
          {
               try
               {
                    Bib_Prestamos loPrestamos = new Bib_Prestamos(SesionSapei.Sistema);
                    loPrestamos.Cargar(piId);

                    if (!loPrestamos.EOF)
                    {//existe el ejemplar
                         loPrestamos.F_entrega = Convert.ToDateTime("14/06/19");//DateTime.Now;
                         loPrestamos.Guardar();

                         loPrestamos.VerificaAdeudo(loPrestamos.Usuario);
                    }
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Prestamos()
          {
               try
               {
                    Bib_Prestamos loPrestamo = new Bib_Prestamos(SesionSapei.Sistema);
                    //Id_prestamo,Titulo, F_prestamo, F_entrega, Usuario,tipo

                    ViewData["Encabezados"] = new List<string> { "Clave", "Título", "F. de prestamo", "F. limite", "F. Entrega", "Usuario", "Clave" };
                    ViewData["Titulo"] = "Prestamos";
                    string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    ViewData["Tabla"] = loPrestamo.RegresaPrestamos(lsPeriodo);
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          #endregion
          #region Ejemplar
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Ejemplar()
          {
               try
               {
                    Bib_Ejemplares loEjemplar = new Bib_Ejemplares(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "Clave", "Reserva", "Titulo", "Tipo", "Eliminar" };
                    ViewData["Titulo"] = "Ejemplares";
                    ViewData["Tabla"] = loEjemplar.RegresaEjemplar();
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult EliminaEjemplarJson(string psId)
          {
               try
               {
                    Bib_Ejemplares loEjemplar = new Bib_Ejemplares(SesionSapei.Sistema);
                    loEjemplar.Cargar(psId);
                    if (!loEjemplar.EOF)
                    {
                         loEjemplar.Baja = true;
                         loEjemplar.Guardar();
                    }
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }

          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ReservaEjemplarJson(string psId, bool pbReserva)
          {
               try
               {
                    Bib_Ejemplares loEditorial = new Bib_Ejemplares(SesionSapei.Sistema);
                    loEditorial.Cargar(psId);
                    if (!loEditorial.EOF)
                    {
                         loEditorial.Reserva = pbReserva;
                    }
                    loEditorial.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ConsultaEjemplarJson()
          {
               try
               {
                    Bib_Ejemplares loEjemplares = new Bib_Ejemplares(SesionSapei.Sistema);
                    string JSONresult;
                    System.Data.DataTable tabla = loEjemplares.RegresaEjemplares();
                    JSONresult = JsonConvert.SerializeObject(tabla);
                    //Response.Write(JSONresult);
                    return ManejoMensajesJson.RegresaJsonObjeto(JSONresult);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }

          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ConsultaEjemplar(string psEjemplar)
          {
               try
               {
                    Bib_Ejemplares loEjemplares = new Bib_Ejemplares(SesionSapei.Sistema);
                    string JSONresult;
                    loEjemplares.Cargar(psEjemplar);
                    if (!loEjemplares.EOF)
                    {
                         //string JSONresult;
                         //si el ejemplar no se encuentra disponible
                         if (!loEjemplares.EjemplarDisponible(loEjemplares.Id_ejemplar))
                         {
                              var Data = new { Mensaje = "El ejemplar ya se encuentra prestado, verifica la clave", Success = false };
                              JSONresult = JsonConvert.SerializeObject(Data);
                              return ManejoMensajesJson.RegresaJsonObjeto(JSONresult);
                         }


                         System.Data.DataTable tabla = loEjemplares.RegresaEjemplar(loEjemplares.Id_ejemplar);
                         JSONresult = JsonConvert.SerializeObject(tabla);
                         return ManejoMensajesJson.RegresaJsonObjeto(JSONresult);
                    }
                    else
                    {
                         var Data = new { Mensaje = "La clave del ejemplar no existe, verifica la clave", Success = false };
                         JSONresult = JsonConvert.SerializeObject(Data);
                         return ManejoMensajesJson.RegresaJsonObjeto(JSONresult);
                    }


               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }

          }
          #endregion
          #region Material
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Material(bool pbTabla = false)
          {
               try
               {
                    Bib_MaterialesBibliograficos loMaterial = new Bib_MaterialesBibliograficos(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "Clave", "Titulo", "Existencia", "Autor", "Editorial", "Carrera", "Tipo", "Editar" };
                    ViewData["Titulo"] = "Material Bibliográfico";
                    ViewData["Tabla"] = loMaterial.RegresarMaterial();
                    if(!pbTabla)
                         return PartialView();
                    return PartialView("../Generales/TablaGeneral");
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ConsultaMaterialJson()
          {
               try
               {
                    Bib_MaterialesBibliograficos loMaterial = new Bib_MaterialesBibliograficos(SesionSapei.Sistema);
                    string JSONresult;
                    System.Data.DataTable tabla = loMaterial.RegresarMaterial(false);
                    JSONresult = JsonConvert.SerializeObject(tabla);
                    return ManejoMensajesJson.RegresaJsonObjeto(JSONresult);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }

          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public int GuardaMaterialJson(int piId, short piAutor,
                  string psCarrera, int piEdicion,
                   string psTitulo,
                  string psFecha, int piExistencia, TipoMaterial tipo)
          {
               try
               {
                    if (piExistencia > 0)
                    {
                         Bib_MaterialesBibliograficos loMaterial = new Bib_MaterialesBibliograficos(SesionSapei.Sistema);
                         loMaterial.Cargar(piId);
                         if (loMaterial.EOF)
                         {
                              loMaterial.Nuevo();
                              loMaterial.f_ingreso = Convert.ToDateTime(psFecha);
                         }
                         loMaterial.tipo_material = (int)tipo;
                         loMaterial.id_autor = piAutor;
                         loMaterial.id_carrera = psCarrera;
                         loMaterial.id_editorial = piEdicion;
                         loMaterial.titulo = psTitulo;
                         loMaterial.existencia = piExistencia;
                         loMaterial.baja = false;
                         loMaterial.Guardar();
                         if (piId <= 0)
                              loMaterial.id_mat_bib = loMaterial.RegresaId();

                         return loMaterial.id_mat_bib;
                    }
                    else
                    {
                         return 0;
                    }
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return 0;
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult EliminaMaterialJson(int piId, int piTipoMaterial)
          {
               try
               {
                    Bib_MaterialesBibliograficos loMaterial = new Bib_MaterialesBibliograficos(SesionSapei.Sistema);
                    loMaterial.Cargar(piId);

                    //switch (piTipoMaterial) {
                    //    case (int)TipoMaterial.Memorias://Memorias
                    //        Bib_Memorias loMemorias = new Bib_Memorias(SesionSapei.Sistema);
                    //        int idMemorias = loMemorias.IdentidificadorMemoria(loMaterial.Id_mat_bib);
                    //        loMemorias.Cargar(idMemorias);
                    //        if (!loMemorias.EOF)
                    //        {
                    //            loMemorias.Eliminar();
                    //        }
                    //        break;
                    //    case (int)TipoMaterial.Libros://Libros
                    //        Bib_Libros loLibros = new Bib_Libros(SesionSapei.Sistema);
                    //        int idLibro=loLibros.IdentidificadorLibro(loMaterial.Id_mat_bib);
                    //        loLibros.Cargar(idLibro);
                    //        if (!loLibros.EOF)
                    //        {
                    //            loLibros.Eliminar();
                    //        }
                    //        break;
                    //    case (int)TipoMaterial.Cds://CDs
                    //        Bib_CDs loCds = new Bib_CDs(SesionSapei.Sistema);
                    //        int idCD = loCds.IdentidificadorCds(loMaterial.Id_mat_bib);
                    //        loCds.Cargar(idCD);
                    //        if (!loCds.EOF)
                    //        {
                    //            loCds.Eliminar();
                    //        }
                    //        break;
                    //    case (int)TipoMaterial.Revistas://Revistas
                    //        Bib_Revistas loRevistas = new Bib_Revistas(SesionSapei.Sistema);
                    //        int idRevista = loRevistas.IdentidificadorRevista(loMaterial.Id_mat_bib);
                    //        loRevistas.Cargar(idRevista);
                    //        if (!loRevistas.EOF)
                    //        {
                    //            loRevistas.Eliminar();
                    //        }
                    //        break;
                    //    case (int)TipoMaterial.Tesis://Tesis
                    //        Bib_Tesis loTesis = new Bib_Tesis(SesionSapei.Sistema);
                    //        int idTesis = loTesis.IdentidificadorTesis(loMaterial.Id_mat_bib);
                    //        loTesis.Cargar(idTesis);
                    //        break;
                    //}


                    if (!loMaterial.EOF)
                    {
                         //el material no se elimina.. solo cambia su estado a baja
                         loMaterial.Eliminar();
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
          #region Catalogos
          #region Editorial
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Editorial()
          {
               try
               {
                    Bib_Editoriales loEditoriales = new Bib_Editoriales(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "id", "Nombre", "Edición", "Editar" };
                    ViewData["Titulo"] = "Editoriales";
                    ViewData["Tabla"] = loEditoriales.RegresarEditoriales();
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult GuardaEditorialJson(int piId, string psNombre)
          {
               try
               {
                    Bib_Editoriales loEditorial = new Bib_Editoriales(SesionSapei.Sistema);
                    loEditorial.Cargar(piId);
                    if (loEditorial.EOF)
                    {
                         loEditorial.Nuevo();
                    }
                    loEditorial.Nombre_editorial = psNombre;
                    //loEditorial.Edicion = psEdicion;
                    loEditorial.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }

          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult EliminaEditorialJson(int piId)
          {
               try
               {
                    Bib_Editoriales loEditorial = new Bib_Editoriales(SesionSapei.Sistema);
                    loEditorial.Cargar(piId);
                    if (!loEditorial.EOF)
                    {
                         loEditorial.Eliminar();
                    }
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ConsultaEditorialJson()
          {
               try
               {
                    Bib_Editoriales loEditorial = new Bib_Editoriales(SesionSapei.Sistema);
                    return ManejoMensajesJson.RegresaJsonTabla(loEditorial.RegresarEditoriales(false));
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }

          }
          #endregion
          #region Autores
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Autores()
          {
               try
               {
                    Bib_Autores loAutores = new Bib_Autores(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "id", "Nombre", "Apellido Paterno", "Apellido Materno", "Editar" };
                    ViewData["Titulo"] = "Autores";
                    ViewData["Tabla"] = loAutores.RegresarAutores();
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult GuardaAutorJson(int piId, string psNombre, string psPaterno, string psMaterno)
          {
               try
               {
                    Bib_Autores loAutor = new Bib_Autores(SesionSapei.Sistema);
                    loAutor.Cargar(piId);
                    if (loAutor.EOF)
                    {
                         loAutor.Id_autor = loAutor.RegresarMaxId();
                    }
                    loAutor.Nombre_autor = psNombre;
                    loAutor.Apellido_p = psPaterno;
                    loAutor.Apellido_m = psMaterno;
                    loAutor.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }

          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult EliminaAutorJson(int piId)
          {
               try
               {
                    Bib_Autores loAutor = new Bib_Autores(SesionSapei.Sistema);
                    loAutor.Cargar(piId);
                    if (!loAutor.EOF)
                    {

                         loAutor.Eliminar();
                    }

                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }

          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ConsultaAutoresJson()
          {
               try
               {
                    Bib_Autores loEditorial = new Bib_Autores(SesionSapei.Sistema);
                    return ManejoMensajesJson.RegresaJsonTabla(loEditorial.RegresarAutores(false));
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }

          }
          #endregion
          
          #region Categorias
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Categorias()
          {
               try
               {
                    Bib_categoria_lc loCategorias = new Bib_categoria_lc(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "Clave", "Descripción" };
                    ViewData["Titulo"] = "Categorias";
                    ViewData["Tabla"] = loCategorias.RegresarCategoriasLCs();
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }

          //[HttpGet]
          //[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          //public JsonResult ConsultaCategoriasJson()
          //{
          //     try
          //     {
          //          Bib_categoria_lc loCategorias = new Bib_categoria_lc(SesionSapei.Sistema);
          //          return ManejoMensajesJson.RegresaJsonTabla(loCategorias.RegresarTiposCategoria(false));
          //     }
          //     catch (Exception ex)
          //     {
          //          Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
          //          return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
          //     }

          //}
          #endregion

          #endregion
          #region Libros
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult GuardaLibroJson(int piId, short piAutor,
                  string psCarrera, int piEdicion,
                   string psTitulo,
                  string psFecha, int piExistencia,
                  int piIdL, string psISBN,
                  byte piNo_paginas, byte piCapitulos, string psEdicion, string psClasificacion)
          {

               piId = GuardaMaterialJson(piId, piAutor, psCarrera,
               piEdicion,  psTitulo,
               psFecha, piExistencia, TipoMaterial.Libros);

               try
               {
                    Bib_Libros loLibro = new Bib_Libros(SesionSapei.Sistema);
                    loLibro.Cargar(piIdL);
                    if (loLibro.EOF)
                    {
                         loLibro.Nuevo();
                    }

                    loLibro.id_mat_bib = piId;
                    loLibro.isbn = psISBN;
                    loLibro.no_paginas = piNo_paginas;
                    loLibro.capitulos = piCapitulos;
                    loLibro.edicion = psEdicion;
                    loLibro.clasificacion = psClasificacion;
                    loLibro.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ConsultaLibroJson(int piId_Mat)
          {
               try
               {
                    Bib_Libros loLibros = new Bib_Libros(SesionSapei.Sistema);
                    return ManejoMensajesJson.RegresaJsonTabla(loLibros.RegresaLibro(piId_Mat));
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult EditaLibroJson(int piId, int piId_mat_bib, string psISBN, byte piNo_paginas, byte piCapitulos, string psEdicion, string psClasificacion)
          {
               try
               {
                    Bib_Libros loLibro = new Bib_Libros(SesionSapei.Sistema);
                    loLibro.Cargar(piId);
                    //solo modificación
                    if (!loLibro.EOF)
                    {

                         loLibro.id_mat_bib = piId_mat_bib;
                         loLibro.isbn = psISBN;
                         loLibro.no_paginas = piNo_paginas;
                         loLibro.capitulos = piCapitulos;
                         loLibro.edicion = psEdicion;
                         loLibro.clasificacion = psClasificacion;
                         loLibro.Guardar();
                    }
                    else
                    {
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
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
          #region Revistas
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult GuardaRevistaJson(int piId, short piAutor,
                         string psCarrera, int piEdicion,
                          string psTitulo,
                         string psFecha, int piExistencia,
                         int piIdR, int piSecciones,
                         string psFechaP, string psEdicion)
          {

               piId = GuardaMaterialJson(piId, piAutor, psCarrera,
               piEdicion,  psTitulo,
               psFecha, piExistencia, TipoMaterial.Revistas);
               try
               {
                    Bib_Revistas loRevista = new Bib_Revistas(SesionSapei.Sistema);

                    loRevista.Cargar(piIdR);
                    if (loRevista.EOF)
                    {
                         loRevista.Nuevo();
                    }
                    loRevista.Id_mat_bib = piId;
                    loRevista.Secciones = piSecciones;
                    loRevista.Edicion = psEdicion;
                    loRevista.Fecha_p = Convert.ToDateTime(psFechaP);
                    loRevista.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }



          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult EditaRevistaJson(int piId, int piSecciones,
                         string psFechaP, int piId_Mat, string psEdicion)
          {
               try
               {
                    Bib_Revistas loRevista = new Bib_Revistas(SesionSapei.Sistema);

                    loRevista.Cargar(piId);
                    if (loRevista.EOF)
                    {
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                    loRevista.Id_mat_bib = piId;
                    loRevista.Secciones = piSecciones;
                    loRevista.Fecha_p = Convert.ToDateTime(psFechaP);
                    loRevista.Edicion = psEdicion;
                    loRevista.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }

          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ConsultaRevistaJson(int piId_Mat)
          {
               try
               {
                    Bib_Revistas loLibros = new Bib_Revistas(SesionSapei.Sistema);
                    return ManejoMensajesJson.RegresaJsonTabla(loLibros.RegresaRevista(piId_Mat));
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          #endregion
          #region Tesis
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ConsultaTesisJson(int piId_Mat)
          {
               try
               {
                    Bib_Tesis loTesis = new Bib_Tesis(SesionSapei.Sistema);
                    return ManejoMensajesJson.RegresaJsonTabla(loTesis.RegresaTesis(piId_Mat));
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult GuardaTesisJson(int piId, short piAutor,
                         string psCarrera, int piEdicion,
                          string psTitulo,
                         string psFecha, int piExistencia,
                         int piIdT, string psFechaP,
                         int piPaginas)
          {

               piId = GuardaMaterialJson(piId, piAutor, psCarrera,
               piEdicion,  psTitulo,
               psFecha, piExistencia, TipoMaterial.Tesis);
               try
               {
                    Bib_Tesis loTesis = new Bib_Tesis(SesionSapei.Sistema);

                    loTesis.Cargar(piIdT);
                    if (loTesis.EOF)
                    {
                         loTesis.Nuevo();
                    }
                    loTesis.Id_mat_bib = piId;
                    loTesis.Fecha_p = Convert.ToDateTime(psFechaP);
                    loTesis.No_paginas = piPaginas;
                    loTesis.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult EditaTesisJson(int piId, string psFechaP,
                         int piPaginas, int piMaterial)
          {
               try
               {
                    Bib_Tesis loTesis = new Bib_Tesis(SesionSapei.Sistema);

                    loTesis.Cargar(piId);
                    if (!loTesis.EOF)
                    {
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                    loTesis.Id_tesis = piId;
                    loTesis.Fecha_p = Convert.ToDateTime(psFechaP);
                    loTesis.No_paginas = piPaginas;
                    loTesis.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          #endregion
          #region Memorias
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Memorias()
          {
               try
               {
                    Bib_Memorias loMemorias = new Bib_Memorias(SesionSapei.Sistema);

                    ViewData["Encabezados"] = new List<string> { "Clave", "Fecha de publicación", "Lugar de publicación", "Titulo", "Material", "Editar" };
                    ViewData["Titulo"] = "Memorias";
                    ViewData["Tabla"] = loMemorias.RegresaMemorias();
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }

          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult GuardaMemoriasJson(int piId, short piAutor,
                  string psCarrera, int piEdicion,
                   string psTitulo,
                  string psFecha, int piExistencia, int piIdMemorias, string psFechaPb, string psLugar)
          {
               try
               {
                    piId = GuardaMaterialJson(piId, piAutor, psCarrera,
                    piEdicion,  psTitulo,
                    psFecha, piExistencia, TipoMaterial.Memorias);

                    Bib_Memorias loMemorias = new Bib_Memorias(SesionSapei.Sistema);
                    loMemorias.Cargar(piIdMemorias);
                    if (loMemorias.EOF)
                    {
                         loMemorias.Nuevo();
                    }
                    loMemorias.Id_Mat_Bib = piId;
                    loMemorias.Fecha_publicacion = Convert.ToDateTime(psFecha);
                    loMemorias.Lugar_p = psLugar;
                    loMemorias.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }

          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ConsultaMemoriasJson(int piId_Mat)
          {
               try
               {
                    Bib_Memorias loMemorias = new Bib_Memorias(SesionSapei.Sistema);
                    return ManejoMensajesJson.RegresaJsonTabla(loMemorias.RegresaMemoria(piId_Mat));
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]

          public JsonResult EditaMemoriasJson(int piId, int piMaterial, string psFecha, string psLugar)
          {
               try
               {
                    Bib_Memorias loMemorias = new Bib_Memorias(SesionSapei.Sistema);
                    loMemorias.Cargar(piId);
                    if (loMemorias.EOF)
                    {
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                    loMemorias.Id_Mat_Bib = piId;
                    loMemorias.Fecha_publicacion = Convert.ToDateTime(psFecha);
                    loMemorias.Lugar_p = psLugar;
                    loMemorias.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }


          #endregion
          #region CDs
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult CDs()
          {
               try
               {
                    Bib_CDs loCDs = new Bib_CDs(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "Clave", "Título", "Descripción", "Duración", "Material", "Editar" };
                    ViewData["Titulo"] = "CD's";
                    ViewData["Tabla"] = loCDs.RegresarCDs();
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult GuardaCDJson(int piId, short piAutor,
                  string psCarrera, int piEdicion,
                   string psTitulo,
                  string psFecha, int piExistencia, int piId_Cd,
                  string psDescripcion, float pfDuracion)
          {
               try
               {
                    int id_Material = GuardaMaterialJson(piId, piAutor, psCarrera, piEdicion,  psTitulo, psFecha, piExistencia, TipoMaterial.Cds);
                    Bib_CDs loCd = new Bib_CDs(SesionSapei.Sistema);

                    loCd.Cargar(piId);
                    if (loCd.EOF)
                    {
                         loCd.Nuevo();
                    }
                    loCd.Id_mat_bib = id_Material;
                    loCd.Descripcion = psDescripcion;
                    loCd.Duracion = pfDuracion;

                    loCd.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);

               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult ConsultaCDJson(int piId_Mat)
          {
               try
               {
                    Bib_CDs loMemorias = new Bib_CDs(SesionSapei.Sistema);
                    return ManejoMensajesJson.RegresaJsonTabla(loMemorias.RegresarCD(piId_Mat));
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public JsonResult EditaCDJson(int piId,
                  string psDescripcion, float pfDuracion, int piId_Mat)
          {
               try
               {
                    Bib_CDs loCd = new Bib_CDs(SesionSapei.Sistema);

                    loCd.Cargar(piId);
                    if (loCd.EOF)
                    {
                         return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    }
                    loCd.Descripcion = psDescripcion;
                    loCd.Duracion = pfDuracion;
                    loCd.Guardar();
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
               }
          }
          #endregion
          #region Financieros
          //cambiar el usuario por el de financieros
          //[HttpGet]
          //[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.FN)]
          //public PartialViewResult FinancierosAdeudosFecha(String psFechaI, String psFechaF)
          //{
          //    try
          //    {
          //        Bib_Adeudos loAdeudos= new Bib_Adeudos(SesionSapei.Sistema);
          //        ViewData["Encabezados"] = new List<string> { "Clave", "Título", "Descripción", "Duración", "Material", "Acciones" };
          //        ViewData["Titulo"] = "Adeudos Biblioteca";
          //        DateTime loFechaI = Convert.ToDateTime(psFechaI);
          //        DateTime loFechaF = Convert.ToDateTime(psFechaF);
          //        ViewData["Tabla"] = loAdeudos.RegresaAdeudos(loFechaI,loFechaF);
          //        return PartialView("FinancierosAdeudos");
          //    }
          //    catch (Exception ex)
          //    {
          //        Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
          //        return PartialView("Index", "Home");
          //    }
          //}
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
          public PartialViewResult FinancierosAdeudosTodos()
          {
               try
               {

                    Bib_Adeudos loAdeudos = new Bib_Adeudos(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "Clave", "Título", "Descripción", "Duración", "Material", "Acciones" };
                    ViewData["Titulo"] = "Adeudos Biblioteca";
                    string lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                    ViewData["Tabla"] = loAdeudos.RegresaAdeudosPeriodo(lsPeriodo);
                    return PartialView("FinancierosAdeudos");
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
          public PartialViewResult FinancierosAdeudos(string psUsuario)
          {
               try
               {
                    Bib_Adeudos loAdeudos = new Bib_Adeudos(SesionSapei.Sistema);
                    ViewData["Encabezados"] = new List<string> { "Clave A", "Clave P", "Usuario", "Fecha de entrega", "Fecha limite", "Monto", "Liquidado", "Dias de retardo", "Acciones" };
                    ViewData["Titulo"] = "Adeudos Biblioteca";
                    //verificar usuario
                    DateTime ldF_inicio = SesionSapei.Sistema.Sesion.Periodo.FechaInicio;
                    DateTime ldF_fin = SesionSapei.Sistema.Sesion.Periodo.FechaFin;

                    if (psUsuario != null)
                         ViewData["Tabla"] = loAdeudos.RegresaAdeudosUsuario(psUsuario, ldF_inicio, ldF_fin);
                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }

          [HttpPost]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DRF)]
          public JsonResult LiquidaAdeudoJson(string piIdAdeudo, string psStatus)
          {
               try
               {
                    Sapei.Framework.Utilerias.enmEstadosPago leEstado = Sapei.Framework.Utilerias.enmEstadosPago.CAN;
                    switch (psStatus)
                    {
                         case "Cancelar":
                              leEstado = Sapei.Framework.Utilerias.enmEstadosPago.CAN;
                              break;
                         case "Condonar":
                              leEstado = Sapei.Framework.Utilerias.enmEstadosPago.CON;
                              break;
                         case "Pagar":
                              leEstado = Sapei.Framework.Utilerias.enmEstadosPago.PAG;
                              break;
                         default:
                              return ManejoMensajesJson.RegresaMensajeJsonErrorServidor();
                    }
                    Bib_Adeudos loAdeudos = new Bib_Adeudos(SesionSapei.Sistema);

                    loAdeudos.Cargar(piIdAdeudo);
                    if (!loAdeudos.EOF)
                    {//existe el registro
                         loAdeudos.Liquidado = true;
                         loAdeudos.Usuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
                         loAdeudos.Tipo_Liberacion = leEstado.ToString();
                         loAdeudos.Periodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
                         loAdeudos.Guardar();
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
          #region AlumnoProfesor
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.ALU, Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Alumno(string psUsuario)
          {
               try
               {

                    //string psUsuario= SesionSapei.Sistema.Sesion.Usuario.Usuario;
                    Bib_Prestamos loAdeudos = new Bib_Prestamos(SesionSapei.Sistema);
                    //verificar si existe usuario
                    ViewData["Encabezados"] = new List<string> { "Titulo", "F. Prestamo", "F. Devolución", "F. Límite", "Entregado", "Adeudo", "Pagado" };

                    ViewData["Titulo"] = "Registro de Prestamos";
                    if (psUsuario != null)
                         ViewData["Tabla"] = loAdeudos.RegresaPrestamosUsuario(psUsuario, enmRolUsuario.ALU);

                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          [HttpGet]
          [SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.DOC, Sapei.Framework.Configuracion.enmRolUsuario.CDI)]
          public PartialViewResult Profesor(string psUsuario)
          {
               try
               {
                    Bib_Prestamos loAdeudos = new Bib_Prestamos(SesionSapei.Sistema);
                    //string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;

                    //verificar si existe usuario
                    ViewData["Encabezados"] = new List<string> { "Titulo", "F. Prestamo", "F. Devolución", "F. Límite", "Entregado" };

                    ViewData["Titulo"] = "Registro de Prestamos";
                    if (psUsuario != "")
                         ViewData["Tabla"] = loAdeudos.RegresaPrestamosUsuario(psUsuario, enmRolUsuario.DOC);

                    return PartialView();
               }
               catch (Exception ex)
               {
                    Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
                    return PartialView("Index", "Home");
               }
          }
          #endregion
     }

}
