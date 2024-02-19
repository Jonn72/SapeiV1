using System;
using System.Xml.Serialization;
using System.Xml;

namespace Sapei.Framework.ProcesoMQ
{
    /// <summary>
    /// Clase para serializarlos parasmetro de entrada, salida o error en la bitacora de procesos
    /// </summary>
    [Serializable]
    [XmlRootAttribute("Parametro", Namespace = "", IsNullable = false)]
    public class ParametroProceso
    {
        /// <summary>
        /// Nombre del parametro
        /// </summary>
        [XmlAttribute("Nombre")]
        public string Nombre { get; set; }
        /// <summary>
        /// Valor del parametro
        /// </summary>
        [XmlElementAttribute("Valor")]
        public string Valor { get; set; }
 
        /// <summary>
        /// Debe de existir para la serializacion
        /// </summary>
        public ParametroProceso() { }
        /// <summary>
        /// Constructor  que recibe el nombre del proceso y el valor
        /// </summary>
        /// <param name="psNombre">Nombre</param>
        /// <param name="psValor">Valor</param>
        public ParametroProceso(string psNombre, string psValor)
        {
            Nombre = psNombre;
            Valor = Convert.ToString(psValor);
        }
    }
}
