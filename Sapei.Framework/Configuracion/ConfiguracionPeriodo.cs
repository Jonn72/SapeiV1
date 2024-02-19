using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.Configuracion
{
 
          [Serializable]
          public class ConfiguracionPeriodo
          {
               /// <summary>
               ///  Periodoactual    
               /// </summary>
               public string PeriodoActual { get; set; }

               public string PeriodoActualSinVerano { 
                    get {
                         if (PeriodoActual.Substring(4, 1) == "2")
                              return PeriodoActual.Substring(0, 4) + "3";
                         return PeriodoActual;
                    } 
               }

        /*public string PeriodoAnterior
        {
            get
            {
                if (PeriodoActual.Substring(4, 1) == "1")
                {
                    int result = Int32.Parse(PeriodoActual.Substring(2, 2));
                    result = result - 1;
                    return PeriodoActual.Substring(0, 2) + result.ToString() + "3";
                }

                if (PeriodoActual.Substring(4, 1) == "3")
                    return PeriodoActual.Substring(0, 4) + "1";
                
                if (PeriodoActual.Substring(4, 1) == "2")
                    return PeriodoActual.Substring(0, 4) + "1";

                return PeriodoAnterior;
            }
        }

        */
        /// <summary>
        /// Identificacion larga   
        /// </summary>
        public string Identificacionlarga { get; set; }
               /// <summary>
               /// Identificacion Corta
               /// </summary>
               public string IdentificacionCorta { get; set; }

               /// <summary>
               /// Gets or sets the direccion.
               /// </summary>
               /// <value>
               /// Estatus
               /// </value>
               public bool Status { get; set; }

               /// <summary>
               ///
               /// </summary>
               /// <value>
               /// Fecha de Inicio
               /// </value>
               public DateTime FechaInicio { get; set; }
               /// <summary>
               ///
               /// </summary>
               /// <value>
               /// Fecha de Inicio
               /// </value>
               public DateTime FechaFin { get; set; }
               /// <summary>
               ///
               /// </summary>
               /// <value>
               /// Fecha de Inicio ENcuesta
               /// </value>
               public DateTime FechaInicioEncuesta { get; set; }
               /// <summary>
               ///
               /// </summary>
               /// <value>
               /// Fecha de fin Encuesta
               /// </value>
               public DateTime FechaFinEncuesta { get; set; }

               public bool ActivaRegistroAspirantes { get; set; }
         
     }
}
