using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.ProcesoMQ
{
    public enum enmBitacoraProcesosEstatus
    {
        /// <summary>
        /// 
        /// </summary>
        ESPERA = 0,
        /// <summary>
        /// 
        /// </summary>
        INICIADO = 1, 
        /// <summary>
        /// 
        /// </summary>
        ENPROCESO = 2, 
        /// <summary>
        /// 
        /// </summary>
        TERMINADO = 3, 
        /// <summary>
        /// 
        /// </summary>
        TERMINADOERROR = 4, 
        /// <summary>
        /// 
        /// </summary>
        TERMINADOEIMPRESO = 5, 
        /// <summary>
        /// 
        /// </summary>
        TERMINADOPORSERVICIOFINALIZADO = 6

    }
}
