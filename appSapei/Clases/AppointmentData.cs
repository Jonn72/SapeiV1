using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using Sapei.Framework.Utilerias.Funciones;

namespace appSapei.Clases
{
    public class AppointmentData
    {
        public int Id { get; set; }
        public string Rfc { get; set; }
        public string Subject { get; set; }
        public string EventType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Periodo { get; set; }
        public string RFC { get; set; }
        public string Materia { get; set; }
        public string Grupo { get; set; }
        public string Aula { get; set; }
        public string Carrera { get; set; }
        public string Reticula { get; set; }
        public string Selected { get; set; }
        public Boolean IsBlock { get; set; }
        public Boolean IsReadonly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poDatos">
        ///     fila 0: ID de usuario
        ///     fila 1: dia_semana
        ///     fila 2: hora_inicial
        ///     fila 3: minuto inicial
        ///     fila 4: hora final
        ///     fila 5: minuto final
        ///     fila 6: descripcion de la actividad
        /// </param>
        /// <returns></returns>
        /// 
        /*
        public List<AppointmentData> RegresaCalendarioSemanalRFC(DataTable poDatos, bool pbSoloLectura, bool pbBlock )
        {
            try
            {
                List<AppointmentData> appData = new List<AppointmentData>();
                
                foreach (DataRow row in poDatos.Rows)
                {
                    int dia = Convert.ToInt32(row[1]);
                    switch (dia)
                    {
                        case 2:
                            appData.Add(new AppointmentData
                            { Rfc = row[0].ToString(), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0), IsBlock = pbBlock, IsReadonly = pbSoloLectura });
                            break;

                        case 3:
                            appData.Add(new AppointmentData
                            { Rfc = row[0].ToString(), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0), IsBlock = pbBlock, IsReadonly = pbSoloLectura });
                            break;

                        case 4:
                            appData.Add(new AppointmentData
                            { Rfc = row[0].ToString(), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0), IsBlock = pbBlock, IsReadonly = pbSoloLectura });
                            break;

                        case 5:
                            appData.Add(new AppointmentData
                            { Rfc = row[0].ToString(), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0), IsBlock = pbBlock, IsReadonly = pbSoloLectura });
                            break;

                        case 6:
                            appData.Add(new AppointmentData
                            { Rfc = row[0].ToString(), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0), IsBlock = pbBlock, IsReadonly = pbSoloLectura });
                            break;

                        case 7:
                            appData.Add(new AppointmentData
                            { Rfc = row[0].ToString(), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[4]), Convert.ToInt16(row[5]), 0), IsBlock = pbBlock, IsReadonly = pbSoloLectura });
                            break;

                        case 8:
                            appData.Add(new AppointmentData
                            { Rfc = row[0].ToString(), Subject = row[6].ToString(), StartTime = new DateTime(2018, 1, 7, Convert.ToInt16(row[2]), Convert.ToInt16(row[3]), 0), EndTime = new DateTime(2018, 1, 7, Convert.ToInt32(row[4]), Convert.ToInt16(row[5]), 0), IsBlock = pbBlock, IsReadonly = pbSoloLectura });
                            break;
                    }
                }
                return appData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        */

        public List<AppointmentData> RegresaCalendarioActividadesApoyo(DataTable psDatosClases, DataTable psDatosActividadesApoyo, string psRFC, string psPeriodo)
        {
            try
            {
                List<AppointmentData> appData = new List<AppointmentData>();
                int i = 1;
                foreach (DataRow row in psDatosClases.Rows)
                {
                    int dia = Convert.ToInt32(row[0]);
                    switch (dia)
                    {
                        case 2:
                            appData.Add(new AppointmentData
                            { Id = i, Rfc = psRFC, Subject = row[5].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), Periodo = psPeriodo, IsBlock = true });
                            break;

                        case 3:
                            appData.Add(new AppointmentData
                            { Id = i, Rfc = psRFC, Subject = row[5].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), Periodo = psPeriodo, IsBlock = true });
                            break;

                        case 4:
                            appData.Add(new AppointmentData
                            { Id = i, Rfc = psRFC, Subject = row[5].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), Periodo = psPeriodo, IsBlock = true });
                            break;

                        case 5:
                            appData.Add(new AppointmentData
                            { Id = i, Rfc = psRFC, Subject = row[5].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), Periodo = psPeriodo, IsBlock = true });
                            break;

                        case 6:
                            appData.Add(new AppointmentData
                            { Id = i, Rfc = psRFC, Subject = row[5].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), Periodo = psPeriodo, IsBlock = true });
                            break;

                        case 7:
                            appData.Add(new AppointmentData
                            { Id = i, Rfc = psRFC, Subject = row[5].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), Periodo = psPeriodo, IsBlock = true });
                            break;

                        case 8:
                            appData.Add(new AppointmentData
                            { Id = i, Rfc = psRFC, Subject = row[5].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 7, Convert.ToInt16(row[1]), Convert.ToInt16(row[2]), 0), EndTime = new DateTime(2018, 1, 7, Convert.ToInt32(row[3]), Convert.ToInt16(row[4]), 0), Periodo = psPeriodo, IsBlock = true });
                            break;
                    }
                    i++;
                }


                foreach (DataRow row in psDatosActividadesApoyo.Rows)
                {
                    int dia = Convert.ToInt32(row[5]);
                    switch (dia)
                    {
                        case 2:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[2]), Rfc = row[1].ToString(), Subject = row[4].ToString(), EventType = row[3].ToString(), StartTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[6]), Convert.ToInt16(row[7]), 0), EndTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[8]), Convert.ToInt16(row[9]), 0)});
                            break;

                        case 3:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[2]), Rfc = row[1].ToString(), Subject = row[4].ToString(), EventType = row[3].ToString(), StartTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[6]), Convert.ToInt16(row[7]), 0), EndTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[8]), Convert.ToInt16(row[9]), 0)});
                            break;

                        case 4:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[2]), Rfc = row[1].ToString(), Subject = row[4].ToString(), EventType = row[3].ToString(), StartTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[6]), Convert.ToInt16(row[7]), 0), EndTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[8]), Convert.ToInt16(row[9]), 0)});
                            break;

                        case 5:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[2]), Rfc = row[1].ToString(), Subject = row[4].ToString(), EventType = row[3].ToString(), StartTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[6]), Convert.ToInt16(row[7]), 0), EndTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[8]), Convert.ToInt16(row[9]), 0)});
                            break;

                        case 6:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[2]), Rfc = row[1].ToString(), Subject = row[4].ToString(), EventType = row[3].ToString(), StartTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[6]), Convert.ToInt16(row[7]), 0), EndTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[8]), Convert.ToInt16(row[9]), 0)});
                            break;

                        case 7:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[2]), Rfc = row[1].ToString(), Subject = row[4].ToString(), EventType = row[3].ToString(), StartTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[6]), Convert.ToInt16(row[7]), 0), EndTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[8]), Convert.ToInt16(row[9]), 0)});
                            break;

                        case 8:
                            appData.Add(new AppointmentData
                            { Id = Convert.ToInt16(row[2]), Rfc = row[1].ToString(), Subject = row[4].ToString(), EventType = row[3].ToString(), StartTime = new DateTime(2018, 1, 7, Convert.ToInt16(row[6]), Convert.ToInt16(row[7]), 0), EndTime = new DateTime(2018, 1, 7, Convert.ToInt32(row[8]), Convert.ToInt16(row[9]), 0)});
                            break;
                    }
                    i++;
                }

                return appData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable RegresaActividadesApoyo()
        {
            DataTable loDt = new DataTable();
            try
            {
                List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
                using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
                {
                    loDt.Load(SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_personal_regresa_lista_actividades", loParametros));
                }
                return loDt;
            }
            catch (Exception ex)
            {
                SesionSapei.Sistema.GrabaLog(ex);
                return null;
            }
        }


        public List<AppointmentData> RegresaHorasClase(DataTable psDatosAula, DataTable psDatosGrupo, string psPeriodo, string psMateria, string psGrupo, string psAula, string psCarrera, string psReticula)
        {
            try
            {
                List<AppointmentData> appData = new List<AppointmentData>();
                int i = 1;
                foreach (DataRow row in psDatosAula.Rows)
                {
                    int dia = Convert.ToInt32(row[2]);
                    switch (dia)
                    {
                        case 2:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsBlock = true });
                            break;

                        case 3:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsBlock = true });
                            break;

                        case 4:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsBlock = true });
                            break;

                        case 5:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsBlock = true });
                            break;

                        case 6:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsBlock = true });
                            break;

                        case 7:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsBlock = true });
                            break;

                        case 8:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 7, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 7, Convert.ToInt32(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsBlock = true });
                            break;
                    }
                    i++;
                }


                foreach (DataRow row in psDatosGrupo.Rows)
                {
                    int dia = Convert.ToInt32(row[2]);
                    switch (dia)
                    {
                        case 2:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), Carrera = psCarrera, Reticula = psReticula, StartTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0) });
                            break;

                        case 3:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), Carrera = psCarrera, Reticula = psReticula, StartTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0) });
                            break;

                        case 4:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), Carrera = psCarrera, Reticula = psReticula, StartTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0) });
                            break;

                        case 5:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), Carrera = psCarrera, Reticula = psReticula, StartTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0) });
                            break;

                        case 6:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), Carrera = psCarrera, Reticula = psReticula, StartTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0) });
                            break;

                        case 7:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), Carrera = psCarrera, Reticula = psReticula, StartTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0) });
                            break;

                        case 8:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), Carrera = psCarrera, Reticula = psReticula, StartTime = new DateTime(2018, 1, 7, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 7, Convert.ToInt32(row[5]), Convert.ToInt16(row[6]), 0) });
                            break;
                    }
                    i++;
                }

                return appData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<AppointmentData> RegresaHorarioGrupoMateria(DataTable psDatosMateria)
        {
            try
            {
                List<AppointmentData> appData = new List<AppointmentData>();
                int i = 1;
                foreach (DataRow row in psDatosMateria.Rows)
                {
                    int dia = Convert.ToInt32(row[2]);
                    switch (dia)
                    {
                        case 2:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 1, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsReadonly = true, IsBlock = true });
                            break;

                        case 3:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 2, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsReadonly = true, IsBlock = true });
                            break;

                        case 4:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 3, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsReadonly = true, IsBlock = true });
                            break;

                        case 5:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 4, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsReadonly = true, IsBlock = true });
                            break;

                        case 6:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 5, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsReadonly = true, IsBlock = true });
                            break;

                        case 7:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 6, Convert.ToInt16(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsReadonly = true, IsBlock = true });
                            break;

                        case 8:
                            appData.Add(new AppointmentData
                            { Id = i, Subject = row[1].ToString(), EventType = "", StartTime = new DateTime(2018, 1, 7, Convert.ToInt16(row[3]), Convert.ToInt16(row[4]), 0), EndTime = new DateTime(2018, 1, 7, Convert.ToInt32(row[5]), Convert.ToInt16(row[6]), 0), Periodo = row[0].ToString(), Grupo = row[7].ToString(), Aula = row[8].ToString(), Materia = row[9].ToString(), IsReadonly = true, IsBlock = true });
                            break;
                    }
                    i++;
                }

                return appData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}