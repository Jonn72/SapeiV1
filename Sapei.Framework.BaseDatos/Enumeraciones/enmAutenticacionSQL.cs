using System;
using System.Linq;

namespace Sapei.Framework.BaseDatos
{
     public enum enmAutenticacionSQL
     {
          /// <summary>
          /// Modo Mixto
          /// </summary>
          Mixto = 0,
          /// <summary>
          /// Usando credenciales de windows
          /// </summary>
          Windows = 1
     }
}