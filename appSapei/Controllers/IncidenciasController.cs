using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace appSapei.Controllers
{
    public class IncidenciasController : Controller
    {
        //
        // GET: /Incidencias/

        public PartialViewResult Index()
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable dt;
                    StringBuilder lsConsulta = new StringBuilder();
                    lsConsulta.Append("select rfc, CONCAT(nombre_empleado,' ',apellido_paterno,' ',apellido_materno), curp_empleado from personal order by nombre_empleado");

                    dt = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);

                    ViewData["Titulo"] = "Lista Docentes";
                    ViewData["Tabla"] = dt;
                    ViewData["Encabezados"] = new List<string> { "RFC", "NOMBRE COMPLETO", "CURP" };


                }
            }
            catch (Exception)
            {

            }
            return PartialView();

        }

        // 
        //CARGAR PUESTOS
        // 
        public PartialViewResult RHcargarpuestos()
        {
            //declarar variables
            DataTable ldTabla;

            //crear una variable para generar una consulta
            StringBuilder lsConsulta = new StringBuilder();
            //lsConsulta.Append("select no_tarjeta, CONCAT(nombre_empleado,' ', apellidos_empleado) as nombre_empleado from personal");
            lsConsulta.Append("select no_tarjeta, CONCAT(apellidos_empleado,' ', nombre_empleado) as nombre_empleado from personal");

            //generar la conexion con la base de datos
            ldTabla = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);

            ViewData["empleado"] = ldTabla;

            lsConsulta = new StringBuilder();
            lsConsulta.Append("select clave_puesto, descripcion_puesto from puestos order by descripcion_puesto");
            ldTabla = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);

            ViewData["Puesto"] = ldTabla;
            return PartialView();

        }

        /// <summary>
        /// Retorna la vista para poder agregar un nuevo horario
        /// </summary>
        /// <returns></returns>
        public PartialViewResult DPcargarhorario()
        {

            DataTable ldTabla;

            //crear una variable para generar una consulta
            StringBuilder lsConsulta = new StringBuilder();
            lsConsulta.Append("select distinct clave_puesto,(select descripcion_puesto from puestos where clave_puesto= t2.clave_puesto) from puestos_personal t2");

            //generar la conexion con la base de datos
            ldTabla = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);

            ViewData["puestos"] = ldTabla;


            lsConsulta = new StringBuilder();
            lsConsulta.Append("select periodo, identificacion_larga from periodos_escolares where year (fecha_inicio)=(select year (GETDATE ())-1)");

            //generar la conexion con la base de datos
            ldTabla = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);

            ViewData["periodo"] = ldTabla;
            
            return PartialView();
        }


        //AGREGAR HORARIO
        
        public JsonResult RHAgregarCargaHorario(string psNombre, string psRFC, int piDia, string psHinicio, string psHfin, string psPeriodo)
        {
            try
            {
                if(!string.IsNullOrEmpty(psNombre)&&!string.IsNullOrEmpty(psRFC))
                {
                    
                    if (piDia >=0 && piDia <7)
                    {
                        if(!string.IsNullOrEmpty(psHinicio)&&!string.IsNullOrEmpty(psHfin))
                        {
                            if(!string.IsNullOrEmpty(psPeriodo))
                            {
                                //Parametros para el procedimiento almacenado
                                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                                loParametros.Add(new ParametrosSQL("Nombre", psNombre));
                                loParametros.Add(new ParametrosSQL("RFC", psRFC));
                                loParametros.Add(new ParametrosSQL("dia_semana", piDia));
                                loParametros.Add(new ParametrosSQL("Hora_Inicial", psHinicio));
                                loParametros.Add(new ParametrosSQL("Hora_Final", psHfin));
                                loParametros.Add(new ParametrosSQL("Periodo", psPeriodo));


                                var loID=SesionSapei.Sistema.Conexion.EjecutaEscalarProcedimientoAlmacenado("[Incidencias].[dbo].[HorarioCarga]", loParametros);
                                if(loID!=null)
                                {
                                    if(!Convert.ToBoolean(loID))
                                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Registro Exitoso", true);
                                }
                                return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se  pudo realizar el registro, Ya se encuentran horarios registrados en esa horas",false);
                            }
                            return ManejoMensajesJson.RegresaMensajeJsonBusqueda("El periodo escolar no es valido",false);
                        }
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Las horas seleccionadas no son validas",false);
                        
                    }
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Los dias seleccionados no son validos",false);
                }
                else
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("La informacion del empleado no es valida, Intentelo mas tarde",false);
                }
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se puedo realizar el registro " + ex.Message, false);
            }
        }


        //CONSULTAR HORARIO
        public PartialViewResult RHConsultarHorario()
        {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                  
                    StringBuilder lsConsulta = new StringBuilder();
                    lsConsulta.Append("select newID() as ID,Nombre, RFC, Dia_Semana, FORMAT(Hora_Inicial,'HH:mm:ss') Hora_Inicial,FORMAT(Hora_Final,'HH:mm:ss')Hora_Final, Periodo from [Incidencias].[dbo].[Horarios_Admin_Sapei]");


                    DataTable loTableHorarios = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);
                    ViewData["Titulo"] = "Carga de Horario";
                    ViewData["Tabla"] = loTableHorarios;
                    ViewData["Encabezados"] = new List<string> { "ID","NOMBRE", "RFC", "DIA DE LA SEMANA", "HORA INICIAL", "HORA FINAL", "PERIODO", "ACCIONES" };



                }
                return PartialView("RHConsultarTablaAcciones");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }

        //public JsonResult 


        public JsonResult CargarEmpleadoPuesto(string psClavePuesto) 
        {


            DataTable loTable = new DataTable();
            try{
                StringBuilder lsConsulta = new StringBuilder();
                lsConsulta.AppendFormat("select rfc, CONCAT(nombre_empleado,' ', apellidos_empleado) as nombre from personal where rfc in (select rfc from puestos_personal where clave_puesto = '{0}')", psClavePuesto);
                loTable = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);
               
                return ManejoMensajesJson.RegresaJsonTabla(loTable);
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
            }

            return ManejoMensajesJson.RegresaJsonTabla(loTable);
        }

        public PartialViewResult RHConsultarDiaFeriado() {
            try
            {
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    DataTable loDataTable;
                    StringBuilder loConsulta = new StringBuilder();
                    loConsulta.Append(" SELECT [Id],[Descripcion],[Dia_Feriado],[Periodo] FROM [Incidencias].[dbo].[Dias_Feriados]");
                    loDataTable = SesionSapei.Sistema.Conexion.RegresaDataTable(loConsulta);

                    if (loDataTable.Rows.Count == 0)
                        return PartialView("Index", "Home");
                    ViewData["Titulo"] = "Dias Feriados";
                    ViewData["Encabezados"] = new List<string> { "ID", "Descripcion", "Fecha", "Periodo","Acciones" };
                      
                    ViewData["Tabla"] = loDataTable;


                }
                return PartialView("RHConsultarTablaAcciones");
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }


        /// <summary>
        /// Vista que permite agregar un dia feriado
        /// </summary>
        /// <returns></returns>
        public PartialViewResult RHcargardiasferiados()
        {
            try
            {
                
                //declarar variables
                DataTable ldTabla;

                //crear una variable para generar una consulta
                StringBuilder lsConsulta = new StringBuilder();
                lsConsulta.Append("select periodo, identificacion_larga from periodos_escolares where year (fecha_inicio)=(select year (GETDATE ())-1)");

                //generar la conexion con la base de datos
                ldTabla = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);

                ViewData["periodos"] = ldTabla;

                return PartialView();

            }catch(Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }

        /// <summary>
        /// Metodo que se encarga de agregar un dia feriado a la base de datos
        /// </summary>
        /// <param name="psFecha"></param>
        /// <param name="psPeriodo"></param>
        /// <param name="psDescripcion"></param>
        /// <returns></returns>
        public JsonResult RHAgregarDiaFeriado(string psFecha,string psPeriodo,string psDescripcion)
        {
            try {
                if(psFecha.Trim().Length>0)
                {
                    
                    if(psPeriodo.Trim().Length>0)
                    {
                        if (psDescripcion.Trim().Length > 0)
                        {
                            StringBuilder loQuery = new StringBuilder();
                            loQuery.AppendFormat(" INSERT INTO [Incidencias].[dbo].[Dias_Feriados] ([Dia_Feriado],[Periodo],[Descripcion]) VALUES ('{0}','{1}','{2}')",psFecha,psPeriodo,psDescripcion);
                            SesionSapei.Sistema.Conexion.EjecutaEscalar(loQuery);
                            return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Registro Exitoso", true);
                        }
                        else
                        {
                            return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Verifique la descripcion", false);
                        }
                    }
                    else
                    {
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Verifique el periodo", false);
                    }
                }
                else
                {
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Verifique la fecha", false);
                }
            }catch(Exception ex){
                SesionSapei.Sistema.GrabaLog(ex);
                return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Error de conexion con el servidor",false);
            }
        }


        public JsonResult RHEliminarDiaFeriado(string psId) 
        {

            try
            {
                if (!string.IsNullOrEmpty(psId))
                {
                    StringBuilder loQuery = new StringBuilder();
                    loQuery.AppendFormat("DELETE [Incidencias].[dbo].[Dias_Feriados] WHERE [Id]='{0}'", psId);
                    SesionSapei.Sistema.Conexion.EjecutaEscalar(loQuery);
                    return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Se ha eliminado el registro con el folio :"+psId, true);
                }
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
            }
            return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Error de conexion con el servidor", false);
            
        }

        /// <summary>
        /// Metodo encargado de generar la vista para añadir una nueva incidencia
        /// </summary>
        /// <returns></returns>
        public PartialViewResult RHgenerarincidencias()
        {
            try
            {

                //declarar variables
                DataTable ldTabla;

                //crear una variable para generar una consulta
                StringBuilder lsConsulta = new StringBuilder();
                lsConsulta.Append("select periodo, identificacion_larga from periodos_escolares where year (fecha_inicio)=(select year (GETDATE ())-1)");

                //generar la conexion con la base de datos
                ldTabla = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);

                ViewData["periodo"] = ldTabla;

                
                lsConsulta = new StringBuilder();
                
                lsConsulta.Append("SELECT distinct t1.[clave_puesto] as clave,t2.[descripcion_puesto] as valor FROM [bdtec].[dbo].[puestos] t2 , ");
                lsConsulta.Append("[bdtec].[dbo].[puestos_personal] t1 where t1.clave_puesto=t2.clave_puesto");

                ldTabla = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);

                ViewData["puesto"] = ldTabla;


                return PartialView("RHgenerarincidencias");
            }catch(Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return PartialView("Index");
            }
        }

        /// <summary>
        /// Metodo encargado de generar la busqueda de un empleado
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> BuscarEmpleado() {

            return null;
        }

        public JsonResult RHCargarArchivoIncidencias()
        {
            try
            {
                //Declaracion de objeto de tipo datatable
                DataTable loTable = new DataTable();
                //Declaracion de tipo file
                HttpPostedFileBase psFile;
                //Se inicializa el objeto 
                psFile = Request.Files[0];
                //Variable de tipo string
                string lsTextoArchivo;
                //se valida que exista informacion del archivo
                if (psFile != null) {
                    //Variable para leer el archivo
                    Stream loSt;
                    //se pone en modo lectura
                    loSt = psFile.InputStream;
                    //se asigna el cursor en la posicion 0
                    loSt.Position = 0;
                    //se genera la lectura del archivo
                    using (StreamReader reader = new StreamReader(loSt, Encoding.UTF8))
                    {
                        lsTextoArchivo = reader.ReadToEnd();
                    }

                    //validamos que el archivo contenga informacion
                    if (string.IsNullOrEmpty(lsTextoArchivo))
                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
                    

                    //Añadimos la estructura de la tabla
                    loTable.Columns.Add("ID");
                    loTable.Columns.Add("Fecha");
                    loTable.Columns.Add("Hora");
                    loTable.Columns.Add("Huella");
                    loTable.Columns.Add("Entrada_Salida");
                    loTable.Columns.Add("Num_U");
                    loTable.Columns.Add("Num_D");

                    //leer la cadena del archivo y separar en una nueva cadena hasta encontrar un salto de linea 
                    foreach(string lsRegistro in lsTextoArchivo.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(lsRegistro))
                        {
                            string[] lsRow = lsRegistro.Replace("\r", "").Split('\t');
                            string[] loHorario = lsRow[1].Split(' ');
                            loTable.Rows.Add(lsRow[0].Trim(), loHorario[0].Trim(), loHorario[1].Trim(), lsRow[2].Trim(), lsRow[3].Trim(), lsRow[4].Trim(), lsRow[5].Trim());
                        }
                    }
                    if(loTable.Rows.Count>0)
                    {
                        SesionSapei.Sistema.Conexion.InsertBulkCopy(loTable, "[Incidencias].[dbo].[Horario_Checador]");

                        return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
                    }
                }

            }
            catch (Exception ex)
            {

                SesionSapei.Sistema.GrabaLog(ex);
            }

            return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);

        }

        public PartialViewResult RHCargaPeriodo()
        {


            //declarar variables
            DataTable ldTabla;

            //crear una variable para generar una consulta
            StringBuilder lsConsulta = new StringBuilder();
            lsConsulta.Append("select periodo, identificacion_larga from periodos_escolares where year (fecha_inicio)=(select year (GETDATE ())-1)");

            //generar la conexion con la base de datos
            ldTabla = SesionSapei.Sistema.Conexion.RegresaDataTable(lsConsulta);

            ViewData["periodo"] = ldTabla;

            return PartialView("RHCargaPeriodo");
        }

                public PartialViewResult RHModificar()
        {
            return PartialView("RHModificar");

          }



        

        //public JsonResult RHBuscarhorario(string psRFC, int piDía_semana, string psPeriodo)
        //{
        //    try
        //   {
        //        StringBuilder loQuery = new StringBuilder();
        //        //loQuery.AppendFormat(" INSERT INTO Horarios_Admin_Sapei ([Nombre],[RFC],[Dia_Semana],[Hora_Inicial],[Hora_Final],[Periodo]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", nombre_, RFC_, dia_semana_, hora_inicio_, hora_fin_, periodo_);
        //        loQuery.AppendFormat("EXEC [Incidencias].[dbo].[Horarios_Admin_Sapei_Consulta] " + "'" + psRFC + "', '" + piDía_semana + "', '" + psPeriodo + "'");
        //        SesionSapei.Sistema.Conexion.EjecutaEscalar(loQuery);
        //        return ManejoMensajesJson.RegresaMensajeJsonBusqueda("Registro Exitoso", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        SesionSapei.Sistema.GrabaLog(ex);
        //        return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se puedo realizar el registro " + ex.Message, false);
        //    }
        //}


    }

}



