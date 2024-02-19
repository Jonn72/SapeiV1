using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Sapei.Framework
{
     /// <summary>
     /// Excepcion personalizada de Solucionic
     /// </summary>
     public class SolExcepcion : Exception, ISerializable
     {
          //Diseñar Excepciones Personalizadas
          //http://msdn.microsoft.com/es-es/library/ms229064(v=vs.85).aspx
          //Las excepciones se derivan de System.Exception o de una de las demás excepciones de base comunes.
          //En el apartado Capturar y generar tipos de excepción estándar (http://msdn.microsoft.com/es-es/library/ms229007(v=vs.85).aspx) 
          //incluye una instrucción que especifica que no debería derivar excepciones personalizadas de ApplicationException.
          //Finalice los nombres de clases de excepción con el sufijo Exception.
          //Las convenciones de nomenclatura coherentes ayudan a suavizar la curva de aprendizaje para las nuevas bibliotecas.
          //Haga que las excepciones sean serializables. Una excepción debe ser serializable para funcionar correctamente atravesando dominios de aplicación y 
          //límites de interacción remota. 
          //ApplicationException
          //Si está diseñando una aplicación que necesita crear sus propias excepciones, es aconsejable que derive excepciones personalizadas de la clase Exception. 
          //Al principio, se pensaba que las excepciones personalizadas debían derivarse de la clase ApplicationException; sin embargo, en la práctica, se ha probado 
          //que esto no tiene ninguna importancia. 
          /// <summary>
          /// Error de la excepcion
          /// </summary>
          public string IdErrorExeception { get; set; }

          /// <summary>
          /// Excepcion sin descripcion
          /// </summary>
          public SolExcepcion()
               : base("Sin descripción")
          {
          }

          /// <summary>
          /// Excepcion con el msg
          /// </summary>
          /// <param name="psMensaje"></param>
          public SolExcepcion(string psMensaje)
               : base(psMensaje)
          {
          }

          /// <summary>
          /// Excepcion con id del Mensaje
          /// </summary>
          /// <param name="piIdError">Id del Mensaje</param>
          /// <param name="psMensaje">mensaje</param>
          public SolExcepcion(string piIdError, string psMensaje)
               : base(psMensaje)
          {
               IdErrorExeception = piIdError;
          }

          /// <summary>
          /// NUeva instancia con el mensaje y la excepcion
          /// </summary>
          public SolExcepcion(string psMensaje, Exception poExcepcion)
               : base(psMensaje, poExcepcion)
          {
          }

          /// <summary>
          /// Nueva isntancia con informacion sobre el error
          /// </summary>
          /// <param name="poInformacion"></param>
          /// <param name="poContexto"></param>
          public SolExcepcion(SerializationInfo poInformacion, StreamingContext poContexto)
               : base(poInformacion, poContexto)
          {
          }
     }
}
