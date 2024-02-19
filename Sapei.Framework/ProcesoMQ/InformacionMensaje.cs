using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.ProcesoMQ
{
     /// <summary>
     /// Clase para mandar la informacion de la cola de mensajes 
     /// </summary>
    [Serializable]
    public class InformacionMensaje
    {

        public int Folio { get; set; }
        /// <summary>
        /// Numero del modulo
        /// </summary>
        public int Modulo { get; set; }

        /// <summary>
        /// Numero del proceso
        /// </summary>
        public Utilerias.enmProcesosMQ Proceso { get; set; }

        /// <summary>
        /// Numero del proceso
        /// </summary>
        public string Periodo { get; set; }


    }
}
