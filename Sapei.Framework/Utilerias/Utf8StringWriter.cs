using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Sapei.Framework.Utilerias
{
     /// <summary>
     ///  Clase para la codificaicon UTF8
     /// </summary>
     public sealed class Utf8StringWriter : StringWriter
     {
          /// <summary>
          /// 
          /// </summary>
          public override Encoding Encoding
          {
               get
               {
                    return Encoding.UTF8;
               }
          }
     }
}
