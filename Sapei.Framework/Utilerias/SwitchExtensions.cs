using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.Utilerias
{
     public static class SwitchExtensions
     {
          /// <summary>
          /// Cases the specified a.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="s">The s.</param>
          /// <param name="a">a.</param>
          /// <returns></returns>
          public static Switch Case<T>(this Switch s, Action<T> a) where T : class
          {
               return Case(s, o => true, a, false);
          }

          /// <summary>
          /// Cases the specified a.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="s">The s.</param>
          /// <param name="a">a.</param>
          /// <param name="fallThrough">if set to <c>true</c> [fall through].</param>
          /// <returns></returns>
          public static Switch Case<T>(this Switch s, Action<T> a, bool fallThrough) where T : class
          {
               return Case(s, o => true, a, fallThrough);
          }
          /// <summary>
          /// Cases the specified c.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="s">The s.</param>
          /// <param name="c">The c.</param>
          /// <param name="a">a.</param>
          /// <returns></returns>
          public static Switch Case<T>(this Switch s, Func<T, bool> c, Action<T> a) where T : class
          {
               return Case(s, c, a, false);
          }
          /// <summary>
          /// Cases the specified c.
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="poObject">The po object.</param>
          /// <param name="c">The c.</param>
          /// <param name="a">a.</param>
          /// <param name="fallThrough">if set to <c>true</c> [fall through].</param>
          /// <returns></returns>
          public static Switch Case<T>(this Switch poObject, Func<T, bool> c, Action<T> a, bool fallThrough) where T : class
          {
               if (poObject == null)
               {
                    return null;
               }

               T t = poObject.Object as T;
               if (t != null)
               {
                    if (c(t))
                    {
                         a(t);
                         return fallThrough ? poObject : null;
                    }
               }

               return poObject;
          }
     }

     /// <summary>
     /// 
     /// </summary>
     public class Switch
     {
          /// <summary>
          /// Gets the object.
          /// </summary>
          /// <value>
          /// The object.
          /// </value>
          public Object Object { get; private set; }

          /// <summary>
          /// Initializes a new instance of the <see cref="Switch"/> class.
          /// </summary>
          /// <param name="poObjeto">The po objeto.</param>
          public Switch(Object poObjeto)
          {
               Object = poObjeto;
          }
     }
}
