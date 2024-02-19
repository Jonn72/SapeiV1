using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.Utilerias
{
     /// <summary>
     /// Clase para el manejor de fechas
     /// </summary>
     public static class ManejoFechas
     {
          private const int ANIO_BASE = 2009;
          /// <summary>
          /// Regresas the fecha formato largo.
          /// </summary>
          /// <param name="poFecha">The po fecha.</param>
          /// <returns></returns>
          public static string RegresaFechaFormatoLargo(this DateTime poFecha)
          {
               return poFecha.ToString("yyyyMMdd HH:mm:ss.fff").Replace(" ", "").Replace(".", "").Replace(":", "");
          }

          /// <summary>
          /// Obteners the nombre mes.
          /// </summary>
          /// <param name="pemnMes">The pemn mes.</param>
          /// <returns></returns>
          public static string ObtenerNombreMes(enmMes pemnMes)
          {
               switch (pemnMes)
               {                   
                    case enmMes.Enero:
                         return "Enero";
                    case enmMes.Febrero:
                         return "Febrero";
                    case enmMes.Marzo:
                         return "Marzo";
                    case enmMes.Abril:
                         return "Abril";
                    case enmMes.Mayo:
                         return "Mayo";
                    case enmMes.Junio:
                         return "Junio";
                    case enmMes.Julio:
                         return "Julio";
                    case enmMes.Agosto:
                         return "Agosto";
                    case enmMes.Septiembre:
                         return "Septiembre";
                    case enmMes.Octubre:
                         return "Octubre";
                    case enmMes.Noviembre:
                         return "Noviembre";
                    case enmMes.Diciembre:
                         return "Diciembre";
                    default:
                         return "Otro";
               }
          }

          /// <summary>
          /// Regresas the mes.
          /// </summary>
          /// <param name="piMes">The pi mes.</param>
          /// <returns></returns>
          public static int RegresaMes(int piMes)
          {
               if (piMes == 0)
               {
                    return 1;
               }

               return piMes;
          }

          /// <summary>
          /// Obteners the nombre mes.
          /// </summary>
          /// <param name="piMes">The pi mes.</param>
          /// <returns></returns>
          public static string ObtenerNombreMes(int piMes)
          {
               if (piMes >= 0 && piMes <= 13)
               {
                    return ObtenerNombreMes((enmMes)piMes);
               }
               return ObtenerNombreMes(enmMes.Otro);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="pdtDate"></param>
          /// <returns></returns>
          public static string PrimerDiaMes(this DateTime pdtDate)
          {
               return new DateTime(pdtDate.Year, pdtDate.Month, 1).ToString("dd");
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="pdtDate"></param>
          /// <returns></returns>
          public static string UltimoDiaMes(this DateTime pdtDate)
          {
               if (pdtDate.Month == 12)
               {
                    return "31";
               }
               return new DateTime(pdtDate.Year, pdtDate.Month + 1, 1).AddDays(-1).ToString("dd");
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static int NumeroDeDiasEnMesActual()
          {
               return DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="piANio"></param>
          /// <param name="pemMes"></param>
          /// <returns></returns>
          public static int NumeroDeDiasEnMesActual(int piANio, enmMes pemMes)
          {
               return DateTime.DaysInMonth(piANio, (int)pemMes);
          }

          /// <summary>
          /// Regresa el ultimo día del mes indicado en la fecha que se le pasa a la funcion
          /// </summary>
          /// <param name="pemMes"></param>
          /// <param name="piAño"></param>
          /// <returns></returns>
          public static System.DateTime UltimoDiaDelMes(enmMes pemMes, int piAño)
          {
               //La linea siguiente obtiene el ultimo dia del mes(revisar Documentacion de la funcion DateSerial,
               //ya que recalcula la fecha en funcion al día)
               return new DateTime(piAño, (int)(pemMes) + 1, 1).AddDays(-1);
          }

          /// <summary>
          /// Ultimoes the dia delete mes.
          /// </summary>
          /// <param name="piAño">The pi año.</param>
          /// <param name="piMes">The pi mes.</param>
          /// <returns></returns>
          public static System.DateTime UltimoDiaDelMes(int piAño, int piMes)
          {
               //La linea siguiente obtiene el ultimo dia del mes(revisar Documentacion de la funcion DateSerial,
               //ya que recalcula la fecha en funcion al día)
               return new DateTime(piAño, piMes + 1, 1).AddDays(-1);
          }

          /// <summary>
          /// obtiene el ultimo dia del mes(revisar Documentacion de la funcion DateSerial, ya que recalcula la fecha en funcion al día)
          /// </summary>
          /// <param name="pdFecha"></param>
          /// <returns></returns>
          public static System.DateTime UltimoDiaDelMes(System.DateTime pdFecha)
          {
               return new DateTime(pdFecha.Year, pdFecha.Month + 1, 1).AddDays(-1);
          }
          
          /// <summary>
          /// Permite saber si el mes indicado en el parametro es bimestre
          /// </summary>
          /// <param name="piMes"></param>
          /// <returns></returns>
          public static bool EsBimestre(int piMes)
          {
               switch (piMes)
               {
                    case 2:
                    case 4:
                    case 6:
                    case 8:
                    case 10:
                    case 12:
                         return true;
                    default:
                         return false;
               }
          }

          /// <summary>
          /// Permite obtener el primer dia del bimestre segun el mes indicado en el parametro
          /// </summary>
          /// <param name="piMes"></param>
          /// <param name="piAño"></param>
          /// <returns></returns>
          public static System.DateTime PrimerDiaBimestre(int piMes, int piAño)
          {
               switch (piMes)
               {
                    case 2:
                    case 4:
                    case 6:
                    case 8:
                    case 10:
                    case 12:
                         return new DateTime(piAño, piMes, 1);
                    default:
                         return DateTime.Now.Date;
               }
          }

          /// <summary>
          /// Permite obtener el primer dia del mes segun el mes indicado en el parametro
          /// </summary>
          /// <param name="piMes"></param>
          /// <param name="piAño"></param>
          /// <returns></returns>
          public static System.DateTime PrimerDiaMes(int piAño, int piMes)
          {
               return new DateTime(piAño, piMes, 1);
          }

          /// <summary>
          /// Permite saber el primer dia habil del mes y año indicados en los parametros
          /// </summary>
          /// <param name="piMes"></param>
          /// <param name="piAño"></param>
          /// <returns></returns>
          public static System.DateTime PrimerDiaHabilDelMes(int piMes, int piAño)
          {
               System.DateTime lddiaHabildelMes = default(System.DateTime);
               lddiaHabildelMes = new DateTime(piAño, piMes, 1);
               if (lddiaHabildelMes.DayOfWeek == System.DayOfWeek.Saturday)
               {
                    return lddiaHabildelMes.AddDays(2);
               }
               if (lddiaHabildelMes.DayOfWeek == DayOfWeek.Sunday)
               {
                    return lddiaHabildelMes.AddDays(1);
               }
               return lddiaHabildelMes;
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="psFecha"></param>
          /// <returns></returns>
          public static bool EsFecha(this string psFecha)
          {
               try
               {
                    DateTime ldFecha = DateTime.Parse(psFecha.ToString());
                    if (ldFecha != DateTime.MinValue && ldFecha != DateTime.MaxValue)
                    {
                         return true;
                    }
                    return false;
               }
               catch
               {
                    return false;
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="psAnio"></param>
          /// <param name="psMes"></param>
          /// <param name="psDia"></param>
          /// <returns></returns>
          public static bool EsFecha(string psAnio, string psMes, string psDia)
          {
               try
               {
                    DateTime ldFecha = new DateTime(Convert.ToInt32(psAnio), Convert.ToInt32(psMes), Convert.ToInt32(psDia));
                    if (ldFecha != DateTime.MinValue && ldFecha != DateTime.MaxValue)
                    {
                         return true;
                    }
                    return false;
               }
               catch
               {
                    return false;
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="piDia"></param>
          /// <returns></returns>
          public static string RegresaDiaSemanaEspañol(ref int piDia)
          {
               string[] lasDias =
               {
                    "DOMINGO",
                    "LUNES",
                    "MARTES",
                    "MIERCOLES",
                    "JUEVES",
                    "VIERNES",
                    "SABADO"
               };
               return lasDias[piDia - 1];
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="piTopeAñosInf"></param>
          /// <param name="piTopeAñosSup"></param>
          /// <returns></returns>
          public static ArrayList RegresaComboAños(int piTopeAñosInf = 0, int piTopeAñosSup = 0)
          {
               int liCounter = 0;
               ArrayList lasCombo = null;
               int liTope = 0;

               lasCombo = new ArrayList();
               if (piTopeAñosInf == 0)
               {
                    piTopeAñosInf = DateTime.Now.Year - 5;
               }
               if (piTopeAñosSup == 0)
               {
                    piTopeAñosSup = DateTime.Now.Year + 1;
               }
               if (piTopeAñosInf < piTopeAñosSup)
               {
                    for (liCounter = piTopeAñosSup; liCounter >= piTopeAñosInf; liCounter += -1)
                    {
                         lasCombo.Add(string.Format("{0},{1}", liCounter.ToString(), liCounter.ToString()));
                         liTope += 1;
                         if (liTope == 6)
                         {
                              break; // TODO: might not be correct. Was : Exit For
                         }
                    }
               }
               else
               {
                    lasCombo.Add(string.Format("{0},{1}", piTopeAñosInf, piTopeAñosInf));
               }

               return lasCombo;
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="pbTodos"></param>
          /// <returns></returns>
          public static ArrayList RegresaListaNombreMeses(bool pbTodos)
          {
               ArrayList lasMeses = null;
               lasMeses = new ArrayList();
               lasMeses.Add("ENERO,1");
               lasMeses.Add("FEBRERO,2");
               lasMeses.Add("MARZO,3");
               lasMeses.Add("ABRIL,4");
               lasMeses.Add("MAYO,5");
               lasMeses.Add("JUNIO,6");
               lasMeses.Add("JULIO,7");
               lasMeses.Add("AGOSTO,8");
               lasMeses.Add("SEPTIEMBRE,9");
               lasMeses.Add("OCTUBRE,10");
               lasMeses.Add("NOVIEMBRE,11");
               lasMeses.Add("DICIEMBRE,12");
               if (pbTodos)
               {
                    lasMeses.Add("TODOS,0");
               }
               return lasMeses;
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="pshAño"></param>
          /// <param name="pbyMes"></param>
          /// <param name="pbyDia"></param>
          /// <returns></returns>
          public static bool EsFechaValida(short pshAño, byte pbyMes, byte pbyDia)
          {
               System.DateTime ldFecha = default(System.DateTime);
               try
               {
                    ldFecha = new System.DateTime(pshAño, pbyMes, pbyDia);
                    return true;
               }
               catch
               {
                    return false;
               }
          }

          /// <summary>
          /// Regresas the lista meses entre fechas.
          /// </summary>
          /// <param name="pdtFechaInicial">fecha inicial.</param>
          /// <param name="pdtFechaFinal">fecha final.</param>
          /// <returns></returns>
          public static HashSet<int> RegresaListaMesesEntreFechas(DateTime pdtFechaInicial, DateTime pdtFechaFinal)
          {
               HashSet<int> loMeses;
               int liMeses;
               loMeses = new HashSet<int>();
               for (liMeses = 1; liMeses < 13; liMeses++)
               {
                    if (liMeses >= pdtFechaInicial.Month && liMeses <= pdtFechaFinal.Month)
                    {
                         loMeses.Add(liMeses);
                    }
               }
               return loMeses;
          }

          /// <summary>
          /// Regresas the lista entre periodos.
          /// </summary>
          /// <param name="piPeriodoInicial">The pi periodo inicial.</param>
          /// <param name="piPeriodoFinal">The pi periodo final.</param>
          /// <param name="pbMes13">if set to <c>true</c> [pb mes13].</param>
          /// <returns></returns>
          public static HashSet<int> RegresaListaEntrePeriodos(int piPeriodoInicial, int piPeriodoFinal, bool pbMes13 = false)
          {
               HashSet<int> loPeriodos;
               int liMeses;
               loPeriodos = new HashSet<int>();
               for (liMeses = 0; liMeses < 14; liMeses++)
               {
                    if (liMeses >= piPeriodoInicial && liMeses <= piPeriodoFinal)
                    {
                         if (liMeses == 0)
                              loPeriodos.Add(1);
                         else if (liMeses == 13 && !pbMes13)
                              loPeriodos.Add(12);
                         else
                              loPeriodos.Add(liMeses);
                    }
               }
               return loPeriodos;
          }

          /// <summary>
          /// Funcion que retorna las horas entre dos fechas
          /// </summary>
          /// <param name="pdFechaInicial">Fecha Inicial</param>
          /// <param name="pdFechaFinal">Fecha Final</param>
          /// <returns></returns>
          /// <remarks></remarks>
          public static int RegresaNumeroHorasentreFechas(this System.DateTime pdFechaInicial, System.DateTime pdFechaFinal)
          {
               return pdFechaFinal.Subtract(pdFechaInicial).Hours;
               //return Math.Abs(DateDiff(DateInterval.Hour, ldFechaFinal, ldFechaInicial, DayOfWeek.Monday));
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="pdFechaInicial"></param>
          /// <param name="pdFechaFinal"></param>
          /// <returns></returns>
          public static int RegresaNumeroDiasentreFechas(this System.DateTime pdFechaInicial, System.DateTime pdFechaFinal)
          {
               return pdFechaFinal.Subtract(pdFechaInicial).Days;
          }

          /// <summary>
          /// Fechas the SQL.
          /// </summary>
          /// <param name="pdFecha">The pd fecha.</param>
          /// <param name="pbIncluyeHHmmss">if set to <c>true</c> [pb incluye h HMMSS].</param>
          /// <param name="psCaracterSeparado">The ps caracter separado.</param>
          /// <returns></returns>
          public static string FechaSQL(this DateTime pdFecha, bool pbIncluyeHHmmss = false, string psCaracterSeparado = "")
          {
               StringBuilder lsFechaSQL;
               lsFechaSQL = new StringBuilder();
               lsFechaSQL.AppendFormat("'{0}{3}{1}{3}{2}", pdFecha.Year, (pdFecha.Month).ToString().PadLeft(2, '0'), pdFecha.Day.ToString().PadLeft(2, '0'), psCaracterSeparado);
               if (pbIncluyeHHmmss == true)
               {
                    lsFechaSQL.AppendFormat(" {0}:{1}:{2}'", pdFecha.Hour.ToString().PadLeft(2, '0'), pdFecha.Minute.ToString().PadLeft(2, '0'), pdFecha.Second.ToString().PadLeft(2, '0'));
               }
               else
               {
                    lsFechaSQL.Append("'");
               }
               return lsFechaSQL.ToString();
          }
          /// <summary>
          /// Fechas the SQL.
          /// </summary>
          /// <param name="pdFecha">The pd fecha.</param>
          /// <param name="pbIncluyeHHmmss">if set to <c>true</c> [pb incluye h HMMSS].</param>
          /// <param name="psCaracterSeparado">The ps caracter separado.</param>
          /// <returns></returns>
          public static string FechaSQL(this DateTimeOffset pdFecha, bool pbIncluyeHHmmss = false, string psCaracterSeparado = "")
          {
               StringBuilder lsFechaSQL;
               lsFechaSQL = new StringBuilder();
               lsFechaSQL.AppendFormat("'{0}{3}{1}{3}{2}", pdFecha.Year, (pdFecha.Month).ToString().PadLeft(2, '0'), pdFecha.Day.ToString().PadLeft(2, '0'), psCaracterSeparado);
               if (pbIncluyeHHmmss == true)
               {
                    lsFechaSQL.AppendFormat(" {0}:{1}:{2}'", pdFecha.Hour.ToString().PadLeft(2, '0'), pdFecha.Minute.ToString().PadLeft(2, '0'), pdFecha.Second.ToString().PadLeft(2, '0'));
               }
               else
               {
                    lsFechaSQL.Append("'");
               }
               return lsFechaSQL.ToString();
          }
          /// <summary>
          /// Fechas the SQL parametros.
          /// </summary>
          /// <param name="pdFecha">The pd fecha.</param>
          /// <param name="pbIncluyeHHmmss">if set to <c>true</c> [pb incluye h HMMSS].</param>
          /// <param name="psCaracterSeparado">The ps caracter separado.</param>
          /// <returns></returns>
          public static string FechaSqlParametros(this DateTime pdFecha, bool pbIncluyeHHmmss = false, string psCaracterSeparado = "")
          {
               return FechaSQL(pdFecha, pbIncluyeHHmmss, psCaracterSeparado).Replace("'", "");
          }
          /// <summary>
          /// Fechas the SQL parametros.
          /// </summary>
          /// <param name="pdFecha">The pd fecha.</param>
          /// <param name="pbIncluyeHHmmss">if set to <c>true</c> [pb incluye h HMMSS].</param>
          /// <param name="psCaracterSeparado">The ps caracter separado.</param>
          /// <returns></returns>
          public static string FechaSqlParametros(this DateTimeOffset pdFecha, bool pbIncluyeHHmmss = false, string psCaracterSeparado = "")
          {
               return FechaSQL(pdFecha, pbIncluyeHHmmss, psCaracterSeparado).Replace("'", "");
          }

          /// <summary>
          /// Converts the date time fecha SQL.
          /// </summary>
          /// <param name="psFecha">The ps fecha.</param>
          /// <returns></returns>
          public static DateTime ConvertDateTimeFechaSQL(string psFecha)
          {
               DateTime loFecha;
               try
               {
                    loFecha = new DateTime(Convert.ToInt32(psFecha.Substring(0, 4)), Convert.ToInt32(psFecha.Substring(5, 2)), Convert.ToInt32(psFecha.Substring(7, 2)));
                    return loFecha;
               }
               catch
               {
                    return DateTime.Now;
               }
          }

          /// <summary>
          /// Dates the time minimum.
          /// </summary>
          /// <param name="pdFecha1">The pd fecha1.</param>
          /// <param name="pdFecha2">The pd fecha2.</param>
          /// <returns></returns>
          public static DateTime DateTimeMin(DateTime pdFecha1, DateTime pdFecha2)
          {
               if (pdFecha1 < pdFecha2)
               {
                    return pdFecha1;
               }
               return pdFecha2;
          }

          /// <summary>
          /// Dates the time maximum.
          /// </summary>
          /// <param name="pdFecha1">The pd fecha1.</param>
          /// <param name="pdFecha2">The pd fecha2.</param>
          /// <returns></returns>
          public static DateTime DateTimeMax(DateTime pdFecha1, DateTime pdFecha2)
          {
               if (pdFecha1 > pdFecha2)
               {
                    return pdFecha1;
               }
               return pdFecha2;
          }

          /// <summary>
          /// Esta funcion confierte en formato para base de datos utilizando una cadena con formato dd/MM/yyyy como entrada
          /// </summary>
          /// <param name="psFecha">una cadena con formato dd/MM/yyyy</param>
          /// <returns></returns>
          public static DateTime FechaCortaSQL(this string psFecha)
          {
               string[] lasFecha;
               DateTime ldtFecha;
               try
               {
                    lasFecha = psFecha.Split('/');
                    ldtFecha = new DateTime();
                    if (lasFecha.Length == 3)
                    {
                         ldtFecha = new DateTime(Convert.ToInt32(lasFecha[2]), Convert.ToInt32(lasFecha[1]), Convert.ToInt32(lasFecha[0]));
                    }
                    return ldtFecha;
               }
               catch
               {
                    return DateTime.Now;
               }
          }

          /// <summary>
          /// To the universal format.
          /// </summary>
          /// <param name="date">The date.</param>
          /// <returns></returns>
          public static string ToUniversalFormat(this DateTime date)
          {
               return GetUniversalFormatValue(date, null);
          }

          /// <summary>
          /// Returns a <see cref="System.String" /> that represents this instance.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <returns>
          /// A <see cref="System.String" /> that represents this instance.
          /// </returns>
          public static string ToString(this DateTime? value)
          {
               return value.ToString(null as IFormatProvider);
          }

          /// <summary>
          /// Returns a <see cref="System.String" /> that represents this instance.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <param name="formatProvider">The format provider.</param>
          /// <returns>
          /// A <see cref="System.String" /> that represents this instance.
          /// </returns>
          public static string ToString(this DateTime? value, IFormatProvider formatProvider)
          {
               if (value.HasValue)
               {
                    return value.Value.ToString(formatProvider);
               }
               else
               {
                    return string.Empty;
               }
          }

          /// <summary>
          /// Returns a <see cref="System.String" /> that represents this instance.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <param name="formatSpecifier">The format specifier.</param>
          /// <returns>
          /// A <see cref="System.String" /> that represents this instance.
          /// </returns>
          public static string ToString(this DateTime? value, string formatSpecifier)
          {
               return value.ToString(formatSpecifier, null);
          }

          /// <summary>
          /// Returns a <see cref="System.String" /> that represents this instance.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <param name="formatSpecifier">The format specifier.</param>
          /// <param name="formatProvider">The format provider.</param>
          /// <returns>
          /// A <see cref="System.String" /> that represents this instance.
          /// </returns>
          public static string ToString(this DateTime? value, string formatSpecifier, IFormatProvider formatProvider)
          {
               if (value.HasValue)
               {
                    return value.Value.ToString(formatSpecifier, formatProvider);
               }
               else
               {
                    return string.Empty;
               }
          }

          /// <summary>
          /// To the universal format.
          /// </summary>
          /// <param name="date">The date.</param>
          /// <param name="formatProvider">The format provider.</param>
          /// <returns></returns>
          public static string ToUniversalFormat(this DateTime date, IFormatProvider formatProvider)
          {
               return GetUniversalFormatValue(date, formatProvider);
          }

          /// <summary>
          /// Gets the universal format value.
          /// </summary>
          /// <param name="date">The date.</param>
          /// <param name="formatProvider">The format provider.</param>
          /// <returns></returns>
          private static string GetUniversalFormatValue(DateTime date, IFormatProvider formatProvider)
          {
               string formatSpecifier = string.Empty;

               if (date.Year == DateTime.Now.Year)
               {
                    formatSpecifier = "{0:dddd, MMM d} at {0:h:mm tt}";
               }
               else
               {
                    formatSpecifier = "{0:dddd, MMM d, yyyy} at {0:h:mm tt}";
               }

               return string.Format(formatProvider, formatSpecifier, date);
          }
          /// <summary>
          /// Valida si la fecha actual esta entre dos fechas requeridas
          /// </summary>
          /// <param name="poInicio"></param>
          /// <param name="poFin"></param>
          /// <returns></returns>
          public static bool ToBetween(DateTime poInicio, DateTime poFin)
          {
               if (DateTime.Compare(DateTime.Now, poInicio) <= 0)
                    return false;
               if (DateTime.Compare(poFin, DateTime.Now) <= 0)
                    return false;
               return true;
          }
          public static bool EsMayorQue(DateTime pdFecha1, DateTime pdFecha2)
          {
               if (pdFecha1 > pdFecha2)
               {
                    return false;
               }
               return true;
          }
          public static DateTime UltimaHoraDia(this DateTime fecha)
          {
               return Convert.ToDateTime(fecha.ToString("d") + " 11:59:59 pm");
          }
          /// <summary>
          /// Equivalente PHP
          /// </summary>
          /// <param name="fecha"></param>
          /// <returns></returns>
          public static string GetTimestamp()
          {
               return ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
          }
          #region Quarters

          /// <summary>
          /// 
          /// </summary>
          /// <param name="piAnio"></param>
          /// <param name="penmCuatrimestre"></param>
          /// <returns></returns>
          public static DateTime ObtenerComienzodelCuatrimestre(int piAnio, enmCuatrimestre penmCuatrimestre)
          {
               switch (penmCuatrimestre)
               {
                    case enmCuatrimestre.Primero:    // 1st Quarter = January 1 to March 31
                         return new DateTime(piAnio, 1, 1, 0, 0, 0, 0);
                    case enmCuatrimestre.Segundo: // 2nd Quarter = April 1 to June 30
                         return new DateTime(piAnio, 4, 1, 0, 0, 0, 0);
                    case enmCuatrimestre.Tercero: // 3rd Quarter = July 1 to September 30
                         return new DateTime(piAnio, 7, 1, 0, 0, 0, 0);
                    default: // 4th Quarter = October 1 to December 31
                         return new DateTime(piAnio, 10, 1, 0, 0, 0, 0);
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="piAnio"></param>
          /// <param name="penmCuatrimestre"></param>
          /// <returns></returns>
          public static DateTime ObtenerelFinaldelCuatrimestre(int piAnio, enmCuatrimestre penmCuatrimestre)
          {
               switch (penmCuatrimestre)
               {
                    case enmCuatrimestre.Primero:       // 1st Quarter = January 1 to March 31
                         return new DateTime(piAnio, 3, DateTime.DaysInMonth(piAnio, 3), 23, 59, 59, 999);
                    case enmCuatrimestre.Segundo: // 2nd Quarter = April 1 to June 30
                         return new DateTime(piAnio, 6, DateTime.DaysInMonth(piAnio, 6), 23, 59, 59, 999);
                    case enmCuatrimestre.Tercero:  // 3rd Quarter = July 1 to September 30
                         return new DateTime(piAnio, 9, DateTime.DaysInMonth(piAnio, 9), 23, 59, 59, 999);
                    default: // 4th Quarter = October 1 to December 31
                         return new DateTime(piAnio, 12, DateTime.DaysInMonth(piAnio, 12), 23, 59, 59, 999);
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="penmMes"></param>
          /// <returns></returns>
          public static enmCuatrimestre ObtenerCuatrimestre(enmMes penmMes)
          {
               if (penmMes <= enmMes.Marzo)
               {
                    // 1st Quarter = January 1 to March 31
                    return enmCuatrimestre.Primero;
               }
               else if ((penmMes >= enmMes.Abril) && (penmMes <= enmMes.Junio))
               {
                    // 2nd Quarter = April 1 to June 30
                    return enmCuatrimestre.Segundo;
               }
               else if ((penmMes >= enmMes.Julio) && (penmMes <= enmMes.Septiembre))
               {
                    // 3rd Quarter = July 1 to September 30
                    return enmCuatrimestre.Tercero;
               }
               else // 4th Quarter = October 1 to December 31
               {
                    return enmCuatrimestre.Cuarto;
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime ObtenerFinaldelUltimoCuatrimestre()
          {
               if ((enmMes)DateTime.Now.Month <= enmMes.Marzo)
               {
                    //go to last quarter of previous year
                    return ObtenerelFinaldelCuatrimestre(DateTime.Now.Year - 1, enmCuatrimestre.Cuarto);
               }
               else //return last quarter of current year
               {
                    return ObtenerelFinaldelCuatrimestre(DateTime.Now.Year, ObtenerCuatrimestre((enmMes)DateTime.Now.Month));
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime ObtenerElComienzodelUltimoCuatrimestre()
          {
               if ((enmMes)DateTime.Now.Month <= enmMes.Marzo)
               {
                    //go to last quarter of previous year
                    return ObtenerComienzodelCuatrimestre(DateTime.Now.Year - 1, enmCuatrimestre.Cuarto);
               }
               else //return last quarter of current year
               {
                    return ObtenerelFinaldelCuatrimestre(DateTime.Now.Year, ObtenerCuatrimestre((enmMes)DateTime.Now.Month));
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime ObtenerElCOmienzodelCuatrimestreActual()
          {
               return ObtenerComienzodelCuatrimestre(DateTime.Now.Year, ObtenerCuatrimestre((enmMes)DateTime.Now.Month));
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime ObtenerElFinaldelCuatrimestreActual()
          {
               return ObtenerelFinaldelCuatrimestre(DateTime.Now.Year, ObtenerCuatrimestre((enmMes)DateTime.Now.Month));
          }

          #endregion

          #region Weeks

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime GetStartOfLastWeek()
          {
               int liDaysToSubtract = (int)DateTime.Now.DayOfWeek + 7;
               DateTime ldtFecha = DateTime.Now.Subtract(System.TimeSpan.FromDays(liDaysToSubtract));
               return new DateTime(ldtFecha.Year, ldtFecha.Month, ldtFecha.Day, 0, 0, 0, 0);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime GetEndOfLastWeek()
          {
               DateTime ldtFecha = GetStartOfLastWeek().AddDays(6);
               return new DateTime(ldtFecha.Year, ldtFecha.Month, ldtFecha.Day, 23, 59, 59, 999);
          }


         

          #endregion

          #region Months

          /// <summary>
          /// 
          /// </summary>
          /// <param name="penmMes"></param>
          /// <param name="piAnio"></param>
          /// <returns></returns>
          public static DateTime GetStartOfMonth(enmMes penmMes, int piAnio)
          {
               return new DateTime(piAnio, (int)penmMes, 1, 0, 0, 0, 0);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="penmMes"></param>
          /// <param name="piAnio"></param>
          /// <returns></returns>
          public static DateTime GetEndOfMonth(enmMes penmMes, int piAnio)
          {
               return new DateTime(piAnio, (int)penmMes, DateTime.DaysInMonth(piAnio, (int)penmMes), 23, 59, 59, 999);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime GetStartOfLastMonth()
          {
               if (DateTime.Now.Month == 1)
               {
                    return GetStartOfMonth((enmMes)12, DateTime.Now.Year - 1);
               }
               else
               {
                    return GetStartOfMonth((enmMes)(DateTime.Now.Month - 1), DateTime.Now.Year);
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime GetEndOfLastMonth()
          {
               if (DateTime.Now.Month == 1)
               {
                    return GetEndOfMonth((enmMes)12, DateTime.Now.Year - 1);
               }
               else
               {
                    return GetEndOfMonth((enmMes)(DateTime.Now.Month - 1), DateTime.Now.Year);
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime GetStartOfCurrentMonth()
          {
               return GetStartOfMonth((enmMes)DateTime.Now.Month, DateTime.Now.Year);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime GetEndOfCurrentMonth()
          {
               return GetEndOfMonth((enmMes)DateTime.Now.Month, DateTime.Now.Year);
          }

          #endregion

          #region Years

          /// <summary>
          /// 
          /// </summary>
          /// <param name="piAnio"></param>
          /// <returns></returns>
          public static DateTime GetStartOfYear(int piAnio)
          {
               return new DateTime(piAnio, 1, 1, 0, 0, 0, 0);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="piAnio"></param>
          /// <returns></returns>
          public static DateTime GetEndOfYear(int piAnio)
          {
               return new DateTime(piAnio, 12, DateTime.DaysInMonth(piAnio, 12), 23, 59, 59, 999);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime GetStartOfLastYear()
          {
               return GetStartOfYear(DateTime.Now.Year - 1);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime GetEndOfLastYear()
          {
               return GetEndOfYear(DateTime.Now.Year - 1);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime GetStartOfCurrentYear()
          {
               return GetStartOfYear(DateTime.Now.Year);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public static DateTime GetEndOfCurrentYear()
          {
               return GetEndOfYear(DateTime.Now.Year);
          }

          #endregion

          #region Days

          /// <summary>
          /// 
          /// </summary>
          /// <param name="pdtFecha"></param>
          /// <returns></returns>
          public static DateTime GetStartOfDay(DateTime pdtFecha)
          {
               return new DateTime(pdtFecha.Year, pdtFecha.Month, pdtFecha.Day, 0, 0, 0, 0);
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="pdtFecha"></param>
          /// <returns></returns>
          public static DateTime GetEndOfDay(DateTime pdtFecha)
          {
               return new DateTime(pdtFecha.Year, pdtFecha.Month, pdtFecha.Day, 23, 59, 59, 999);
          }

          #endregion
     }
}
