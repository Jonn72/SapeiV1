using System;
using System.Collections;
using System.ComponentModel;

namespace Sapei.Framework.Utilerias
{
     /// <summary>
     /// Clase para convertir una 
     /// </summary>
     public static class NumeroLetras
     {
          /// <summary>
          /// 
          /// </summary>
          private static bool _bCentavo;
          /// <summary>
          /// 
          /// </summary>
          private static string _sCanLe;
          /// <summary>
          /// 
          /// </summary>
          private static string _sUno;
          /// <summary>
          /// 
          /// </summary>
          private static string _sUnomil;
          /// <summary>
          /// 
          /// </summary>
          private static string _sUnob;
          /// <summary>
          /// 
          /// </summary>
          private static string _sDos;
          /// <summary>
          /// 
          /// </summary>
          private static string _sTres;
          /// <summary>
          /// 
          /// </summary>
          private static string _sUne1;
          /// <summary>
          /// 
          /// </summary>
          private static string _sCb;
          /// <summary>
          /// 
          /// </summary>
          private static string _sCm;
          /// <summary>
          /// 
          /// </summary>
          private static string _sCz;
          /// <summary>
          /// 
          /// </summary>
          private static string _sCy;
          /// <summary>
          /// 
          /// </summary>
          private static string _sCx;
          /// <summary>
          /// 
          /// </summary>
          private static string _sCc;
          /// <summary>
          /// 
          /// </summary>
          private static string _sCT;
          /// <summary>
          /// 
          /// </summary>
          private static string _sItem;
          /// <summary>
          /// 
          /// </summary>
          private static string _sIt;

          /// <summary>
          /// Esta rutina convierte una cantidad en número a letras
          /// </summary>
          /// <param name="psCantidad"></param>
          /// <returns></returns>
          public static string NumLetra(string psCantidad)
          {
               //string functionReturnValue = null;
               //
               string lsmillo_arriba = null;
               int liCont = 0;
               string lsLETRAS = null;
               int liTam = 0;
               int liIndice = 0;
               string lsA = null;
               int liSigno = 0;
               string lsValor = null;               
               //si llega con muchos decimales la cantidad
               //tiene problemas la función
               psCantidad = Convert.ToString(Math.Round(Convert.ToDouble(psCantidad), 2));               
               //Le hago una validación para importes negativos
               if (Convert.ToDouble(psCantidad) < 0)               
               {
                    liSigno = -1;
               }
               else 
               {
                    liSigno = 1;
               }
               //Le pone la multiplicación
               psCantidad = Convert.ToString((Convert.ToDecimal(psCantidad) * liSigno));
               lsmillo_arriba = "";
               _sUno = new String(' ', 8);
               _sUnomil = new String(' ', 8);               
               _sUnob = new String(' ', 8);               
               _sDos = new String(' ', 11);               
               _sTres = new String(' ', 13);
               _bCentavo = false;
               liCont = 0;
               lsLETRAS = "";
               if (double.Parse(psCantidad) <= 999999999999999.0)
               {
                    _sCc = new String(' ', 3);
                    liTam = psCantidad.Length;
                    for (liIndice = 0; liIndice <= (psCantidad.Trim().Length - 1); liIndice++)
                    {
                         lsA = psCantidad.Substring(liIndice + 1, 1);
                         liCont = liCont + 1;
                         if (psCantidad.Substring(liIndice + 1, 1) == ".")
                         {
                              _sCc = psCantidad.Substring(liIndice + 2, 2);
                              if (_sCc.Length == 2)
                              {
                                   liIndice = psCantidad.Trim().Length - 1;
                                   psCantidad = psCantidad.Substring(1, liTam - 3);
                                   liTam = psCantidad.Length;
                              }
                              else if (_sCc.Length == 1)
                              {
                                   liIndice = (psCantidad.Trim().Length - 1);
                                   psCantidad = psCantidad.Substring(1, liTam - 2);
                                   liTam = psCantidad.Length;
                              }
                         }
                    }
                    psCantidad = string.Format("{0}{1}", new String(' ', 15 - liTam), psCantidad);
                    liTam = psCantidad.Length;
                    //18
                    if (liTam == 15)
                    {
                         _sCb = psCantidad.Substring(1, 3);
                         // BILLONES
                         _sCm = psCantidad.Substring(4, 3);
                         // MILES DE MILLONES
                         _sCz = psCantidad.Substring(7, 3);
                         // MILLONES
                         _sCy = psCantidad.Substring(10, 3);
                         // MILES
                         _sCx = psCantidad.Substring(13, 3);
                         // CENTENAS
                    }
                    else 
                    {
                         throw new ApplicationException("Numletra: Cantidad incorrecta");
                    }
                    
                    if (!string.IsNullOrEmpty(_sCb.Trim()))
                    {
                         _sCanLe = lsLETRAS;
                         _sCT = _sCb;
                         Triple();
                         Impre();
                         if (Int32.Parse(_sCb) > 1)
                         {
                              lsLETRAS += " BILLONES ";
                         }
                         else 
                         {
                              lsLETRAS += " BILLON ";
                         }
                         if ((_sCm == "000") & (_sCz == "000") & (_sCy == "000") & (_sCx == "000"))                         
                         {
                              lsLETRAS += " DE ";
                         }
                    }
                    if ((!string.IsNullOrEmpty(_sCm.Trim())) & (_sCm.Trim() != "000"))
                    {
                         _sCT = _sCm;
                         Triple();
                         if ((_sCm.Substring(2, 1) == " ") & (_sCm.Substring(3, 1) == "1"))
                         {
                              _sUnob = new String(' ', 6);
                              _sUno = new String(' ', 8);
                         }
                         Impre();
                         lsLETRAS += " MIL ";
                         if ((_sCz == "000") & (_sCy == "000") & (_sCx == "000"))                         
                         {
                              lsLETRAS += " DE ";
                         }
                    }
                    if ((!string.IsNullOrEmpty(_sCz.Trim())) & (_sCz.Trim() != "000"))
                    {
                         _sCT = _sCz;
                         Triple();
                         Impre();
                         if (Int32.Parse(_sCz) > 1)                         
                         {
                              lsmillo_arriba = string.Format("{0} MILLONES ", _sCanLe);
                         }
                         else 
                         {
                              lsmillo_arriba = string.Format("{0} MILLON", _sCanLe);
                         }
                         if ((_sCy == "000") & (_sCx == "000"))                         
                         {
                              lsLETRAS += " DE  ";
                         }
                    }
                    if (!string.IsNullOrEmpty(_sCm.Trim()))                    
                    {
                         lsLETRAS += new String(' ', 2);
                    }
                    if ((!string.IsNullOrEmpty(_sCy.Trim())) & (_sCy.Trim() != "000"))
                    {
                         _sCT = _sCy;
                         Triple();
                         if ((string.IsNullOrEmpty(_sCy.Substring(2, 1).Trim())) & (_sCy.Substring(3, 1) == "1"))
                         {
                              _sUnomil = new String(' ', 6);
                              _sUno = new String(' ', 8);
                         }
                         Impre();
                         lsLETRAS = string.Format("{0}{1} MIL ", _sCanLe, lsLETRAS);
                    }
                    if (!string.IsNullOrEmpty(_sCx.Trim()))
                    {
                         _sCT = _sCx;
                         Triple();
                         Impre();
                         lsLETRAS += _sCanLe;
                    }
                    if (!string.IsNullOrEmpty(lsmillo_arriba.Trim()))                    
                    {
                         lsLETRAS = string.Format("{0} {1}", lsmillo_arriba, lsLETRAS);
                    }
                    
                    lsLETRAS += " PESOS";

                    if (!string.IsNullOrEmpty(_sCc.Trim()) && Int32.Parse(_sCc) > 0)
                    {
                         if (_sCc.Length == 2)
                         {
                              lsA = string.Format("{0}/100  M.N.", _sCc);
                              liTam = lsLETRAS.Length;
                              lsLETRAS = string.Format("( {0} {1}/100  M.N. )", lsLETRAS, _sCc);
                         }
                         else if (_sCc.Length == 1)
                         {
                              lsA = string.Format("{0}0/100  M.N.", _sCc);
                              liTam = lsLETRAS.Length;
                              lsLETRAS = string.Format("( {0} {1}0/100  M.N. )", lsLETRAS, _sCc);
                         }
                    }
                    else 
                    {
                         lsLETRAS = string.Format("( {0} 00/100  M.N. )", lsLETRAS);
                    }
               }

               lsValor = lsLETRAS;
               if (liSigno < 0)               
               {
                    lsValor = string.Format("(  MENOS {0}", lsValor.Substring(2).TrimStart());
               }
               return lsValor;               
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="Pen_Type"></param>
          /// <param name="psFormato"></param>
          /// <returns></returns>
          public static ArrayList LlenaArregloConEnumeracion(System.Enum Pen_Type, ref string psFormato)
          {
               int liIndex = 0;
               Array laoValores = null;
               ArrayList laoValorEnum = null;
               //Obtenemos los valores de la enumeracion 
               laoValores = System.Enum.GetValues(Pen_Type.GetType());
               laoValorEnum = new ArrayList();
               foreach (int liIndex_loopVariable in laoValores)
               {
                    liIndex = liIndex_loopVariable;
                    laoValorEnum.Add(string.Format("{0},{1}", System.Enum.Format(Pen_Type.GetType(), liIndex, psFormato), liIndex));
               }
               //Format(Description)
               //"G" or "g" If value is equal to a defined value of enumType , returns the element name defined for value. 
               //If the FlagsAttribute attribute is set on the Enum declaration and value is a built-in integer type and is equal 
               //to a summation of enumeration elements, the return value contains the element names in an unspecified order, 
               //separated by commas (e.g. "Red, Yellow"). Otherwise, value is returned in decimal format.
               //"X" or "x" Returns value in hexadecimal format, without a leading 0x. The value is padded with leading zeroes to ensure the returned value is at least eight digits in length. 
               //"F" or "f" Behaves identically to "G", except the FlagsAttribute is not required to be present on the Enum declaration. 
               //"D" or "d" Returns value in decimal format with no leading zeroes. 
               return laoValorEnum;
               //Regresa el diccionario
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="Pen_Type"></param>
          /// <param name="psFormato"></param>
          /// <param name="pbElementoVacio"></param>
          /// <returns></returns>
          public static ArrayList LlenaArregloConEnumeracionColores(System.Enum Pen_Type, ref string psFormato, bool pbElementoVacio = false)
          {
               Array laoValores = null;
               ArrayList laoValorEnum = null;
               //Obtenemos los valores de la enumeracion 
               laoValores = System.Enum.GetValues(Pen_Type.GetType());
               laoValorEnum = new ArrayList();
               laoValorEnum = LlenaArregloConEnumeracion(Pen_Type, ref psFormato);
               if (pbElementoVacio)
               {
                    laoValorEnum.Insert(0, "Sin Color,");
               }
               return laoValorEnum;
               //Regresa el diccionario
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="Pi_ValorMinimo"></param>
          /// <param name="Pi_ValorMaximo"></param>
          /// <returns></returns>
          public static int GeneraNumeroAleatorio(int Pi_ValorMinimo, int Pi_ValorMaximo)
          {
               Random loGeneraNumeroRandom = null;
               try
               {
                    loGeneraNumeroRandom = new Random();
                    return loGeneraNumeroRandom.Next(Pi_ValorMinimo, Pi_ValorMaximo);
               }
               finally
               {
                    loGeneraNumeroRandom = null;
               }
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="psValor"></param>
          /// <returns></returns>
          public static bool EsNumerico(this string psValor)
          {
               Double result;
               return Double.TryParse(psValor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out result);
          }

          /// <summary>
          /// Eses the numerico.
          /// </summary>
          /// <param name="t">The t.</param>
          /// <returns></returns>
          public static bool EsNumerico(this Type t)
          {
               var type = t.GetTypeWithoutNullability();
               return
                     type == typeof(Int16) ||
                     type == typeof(Int32) ||
                     type == typeof(Int64) ||
                     type == typeof(UInt16) ||
                     type == typeof(UInt32) ||
                     type == typeof(UInt64) ||
                     type == typeof(decimal) ||
                     type == typeof(float) ||
                     type == typeof(double);
          }

          /// <summary>
          /// Gets the type without nullability.
          /// </summary>
          /// <param name="t">The t.</param>
          /// <returns></returns>
          public static Type GetTypeWithoutNullability(this Type t)
          {
               return t.IsNullable() ? new NullableConverter(t).UnderlyingType : t;
          }

          /// <summary>
          /// Determines whether this instance is nullable.
          /// </summary>
          /// <param name="t">The t.</param>
          /// <returns></returns>
          public static bool IsNullable(this Type t)
          {
               return t.IsGenericType &&
                      t.GetGenericTypeDefinition() == typeof(Nullable<>);
          }

          /// <summary>
          /// Lengthes the specified i.
          /// </summary>
          /// <param name="i">The i.</param>
          /// <returns></returns>
          /// <exception cref="ArgumentOutOfRangeException"></exception>
          public static int Length(this int i)
          {
               if (i <= 0)
               {
                    throw new ArgumentOutOfRangeException();
               }

               return (int)Math.Floor(Math.Log10(i)) + 1;
          }

          /// <summary>
          /// Kilobytes
          /// </summary>
          /// <param name="value"></param>
          /// <returns></returns>
          public static int KB(this int value)
          {
               return value * 1024;
          }

          /// <summary>
          /// Megabytes
          /// </summary>
          /// <param name="value"></param>
          /// <returns></returns>
          public static int MB(this int value)
          {
               return value.KB() * 1024;
          }

          /// <summary>
          /// Gigabytes
          /// </summary>
          /// <param name="value"></param>
          /// <returns></returns>
          public static int GB(this int value)
          {
               return value.MB() * 1024;
          }

          /// <summary>
          /// Terabytes
          /// </summary>
          /// <param name="value"></param>
          /// <returns></returns>
          public static long TB(this int value)
          {
               return (long)value.GB() * (long)1024;
          }

          /// <summary>
          /// To the int32.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <returns></returns>
          public static int ToInt32(this double value)
          {
               return Convert.ToInt32(value);
          }

          /// <summary>
          /// Betweens the specified lower bound.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <param name="lowerBound">The lower bound.</param>
          /// <param name="upperBound">The upper bound.</param>
          /// <returns></returns>
          public static bool Between(this int value, int lowerBound, int upperBound)
          {
               return value >= lowerBound && value <= upperBound;
          }

          /// <summary>
          /// Betweens the exclusive.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <param name="lowerBound">The lower bound.</param>
          /// <param name="upperBound">The upper bound.</param>
          /// <returns></returns>
          public static bool BetweenExclusive(this int value, int lowerBound, int upperBound)
          {
               return value > lowerBound && value < upperBound;
          }

          /// <summary>
          /// To the abbreviated format.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <returns></returns>
          public static string ToAbbreviatedFormat(this int value)
          {
               return value.ToAbbreviatedFormat(null);
          }

          /// <summary>
          /// To the abbreviated format.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <param name="formatProvider">The format provider.</param>
          /// <returns></returns>
          public static string ToAbbreviatedFormat(this int value, IFormatProvider formatProvider)
          {
               if (value.BetweenExclusive(-1000, 1000))
               {
                    return value.ToString(formatProvider);
               }
               else
               {
                    return string.Format(formatProvider, "{0:N0}k", value / 1000);
               }
          }

          /// <summary>
          /// To the string as text.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <returns></returns>
          public static string ToStringAsText(this int value)
          {
               return value.ToStringAsText(null);
          }

          /// <summary>
          /// To the string as text.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <param name="formatProvider">The format provider.</param>
          /// <returns></returns>
          public static string ToStringAsText(this int value, IFormatProvider formatProvider)
          {
               return ToStringAsText(value, false, formatProvider);
          }

          /// <summary>
          /// To the string as text.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <param name="capitalizeText">if set to <c>true</c> [capitalize text].</param>
          /// <returns></returns>
          public static string ToStringAsText(this int value, bool capitalizeText)
          {
               return ToStringAsText(value, capitalizeText, null);
          }

          /// <summary>
          /// To the string as text.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <param name="capitalizeText">if set to <c>true</c> [capitalize text].</param>
          /// <param name="formatProvider">The format provider.</param>
          /// <returns></returns>
          public static string ToStringAsText(this int value, bool capitalizeText, IFormatProvider formatProvider)
          {
               switch (value)
               {
                    case 1:
                         if (capitalizeText)
                         {
                              return "One";
                         }
                         else
                         {
                              return "one";
                         }
                    case 2:
                         if (capitalizeText)
                         {
                              return "Two";
                         }
                         else
                         {
                              return "two";
                         }
                    case 3:
                         if (capitalizeText)
                         {
                              return "Three";
                         }
                         else
                         {
                              return "three";
                         }
                    case 4:
                         if (capitalizeText)
                         {
                              return "Four";
                         }
                         else
                         {
                              return "four";
                         }
                    case 5:
                         if (capitalizeText)
                         {
                              return "Five";
                         }
                         else
                         {
                              return "five";
                         }
                    case 6:
                         if (capitalizeText)
                         {
                              return "Six";
                         }
                         else
                         {
                              return "six";
                         }
                    case 7:
                         if (capitalizeText)
                         {
                              return "Seven";
                         }
                         else
                         {
                              return "seven";
                         }
                    case 8:
                         if (capitalizeText)
                         {
                              return "Eight";
                         }
                         else
                         {
                              return "eight";
                         }
                    case 9:
                         if (capitalizeText)
                         {
                              return "Nine";
                         }
                         else
                         {
                              return "nine";
                         }
                    case 10:
                         if (capitalizeText)
                         {
                              return "Ten";
                         }
                         else
                         {
                              return "ten";
                         }

                    default:
                         return value.ToString("N0", formatProvider);
               }
          }

          /// <summary>
          /// Centenas this instance.
          /// </summary>
          private static void CENTENA()
          {
               //Rutina auxiliar para la conversión de número a letra
               string lsF = null;

               lsF = _sIt.Substring(1, 1);
               if ((_sIt.Substring(3, 1) == "1") & (_sIt.Substring(2, 1) != "1"))               
               {
                    _sUno = "UN";
               }
               if (lsF == "1")
               {
                    if (_sIt.Substring(2, 2) == "00")                    
                    {
                         _sTres = "CIEN";
                    }
                    else 
                    {
                         _sTres = "CIENTO";
                    }
               }
               else if (lsF == "2")               
               {
                    _sTres = "DOSCIENTOS";
               }
               else if (lsF == "3")               
               {
                    _sTres = "TRESCIENTOS";
               }
               else if (lsF == "4")               
               {
                    _sTres = "CUATROCIENTOS";
               }
               else if (lsF == "5")               
               {
                    _sTres = "QUINIENTOS";
               }
               else if (lsF == "6")               
               {
                    _sTres = "SEISCIENTOS";
               }
               else if (lsF == "7")               
               {
                    _sTres = "SETECIENTOS";
               }
               else if (lsF == "8")               
               {
                    _sTres = "OCHOCIENTOS";
               }
               else if (lsF == "9")               
               {
                    _sTres = "NOVECIENTOS";
               }
          }

          /// <summary>
          /// 
          /// </summary>
          private static void Triple()
          {
               //Rutina auxiliar para la conversión de número a letra
               //al parecer estas dos variables son inservibles
               object d = null;
               object A = null;
               d = 0;
               if (_bCentavo == false)
               {
                    _sItem = _sCT.Substring(3, 1);
                    UNIDAD();
                    if ((_sCT.Substring(2, 1) != "0") & (!string.IsNullOrEmpty(_sCT.Substring(2, 1).Trim())))
                    {
                         _sIt = _sCT.Substring(2, 2);
                         DECENA();
                         A = d;
                    }
                    if ((_sCT.Substring(1, 1) != "0") & (!string.IsNullOrEmpty(_sCT.Substring(1, 1).Trim())))
                    {
                         _sIt = _sCT;
                         CENTENA();
                         A = d;
                    }
               }
               else
               {
                    _sItem = _sCT.Substring(2, 1);
                    UNIDAD();
                    _sIt = _sCT.Substring(1, 1);
                    DECENA();
               }
          }

          /// <summary>
          /// 
          /// </summary>
          private static void Impre()
          {
               //Rutina auxiliar para la conversión de número a letra
               //Parameters tres, dos, une1, uno, CanLe
               _sCanLe = "";
               if (_sTres != new String(' ', 13))               
               {
                    _sCanLe += string.Format("{0} ", _sTres);
               }
               
               if (_sDos != new String(' ', 11))               
               {
                    if (!string.IsNullOrEmpty(_sUno.Trim()))
                    {
                         if (_sUne1 == " Y ")                         
                         {
                              _sCanLe += string.Format("{0}{1}", _sDos, _sUne1);
                         }
                         else if (_sDos.ToUpper().Trim() == "VEINTI")                              
                         {
                              _sCanLe += string.Format("{0}{1}", _sDos, _sUne1);
                         }
                         else 
                         {
                              _sCanLe += string.Format("{0}{1} Y ", _sDos, _sUne1);
                         }
                    }
                    else 
                    {
                         _sCanLe += string.Format("{0}{1}", _sDos, _sUne1);
                    }
               }
               
               if (_sUno != new String(' ', 8))               
               {
                    if (_sCanLe.ToUpper().Trim() == "VEINTI")                    
                    {
                         _sCanLe += _sUno.Trim();
                    }
                    else 
                    {
                         _sCanLe += _sUno;
                    }
               }
               _sTres = new String(' ', 13);
               _sDos = new String(' ', 11);
               _sUno = new String(' ', 8);
          }

          /// <summary>
          /// 
          /// </summary>
          private static void UNIDAD()
          {
               //Rutina auxiliar para la conversión de número a letra (Parameters Item, uno, une1)
               switch (_sItem)
               {
                    case "0":
                         _sUne1 = new String(' ', 0);
                         break;
                    case "1":
                         _sUno = "UN";
                         break;
                    case "2":
                         _sUno = "DOS";
                         break;
                    case "3":
                         _sUno = "TRES";
                         break;
                    case "4":
                         _sUno = "CUATRO";
                         break;
                    case "5":
                         _sUno = "CINCO";
                         break;
                    case "6":
                         _sUno = "SEIS";
                         break;
                    case "7":
                         _sUno = "SIETE";
                         break;
                    case "8":
                         _sUno = "OCHO";
                         break;
                    case "9":
                         _sUno = "NUEVE";
                         break;
               }
          }

          /// <summary>
          /// 
          /// </summary>
          private static void DECENA()
          {
               //Rutina auxiliar para la conversión de número a letra
               //Parameters it, dos, uno, une1
               string F = null;
               if (Int32.Parse(_sIt) > 9 && Int32.Parse(_sIt) <= 19)
               {
                    if (_sIt == "10")                    
                    {
                         _sDos = "DIEZ";
                    }
                    else if (_sIt == "11")                    
                    {
                         _sDos = "ONCE";
                    }
                    else if (_sIt == "12")                    
                    {
                         _sDos = "DOCE";
                    }
                    else if (_sIt == "13")                    
                    {
                         _sDos = "TRECE";
                    }
                    else if (_sIt == "14")                    
                    {
                         _sDos = "CATORCE";
                    }
                    else if (_sIt == "15")                    
                    {
                         _sDos = "QUINCE";
                    }
                    else if (_sIt == "16")                    
                    {
                         _sDos = "DIECISEIS";
                    }
                    else if (_sIt == "17")                    
                    {
                         _sDos = "DIECISIETE";
                    }
                    else if (_sIt == "18")                    
                    {
                         _sDos = "DIECIOCHO";
                    }
                    else if (_sIt == "19")                    
                    {
                         _sDos = "DIECINUEVE";
                    }
                    _sUno = new String(' ', 8);
                    _sUne1 = new String(' ', 0);
               }
               else
               {
                    if (_sIt.Substring(2, 1) == "1")
                    {
                         _sUne1 = " Y ";
                         _sUno = " UN ";
                    }
                    F = _sIt.Substring(1, 1);
                    if (F == "2")
                    {
                         if (_sIt.Substring(2, 1) == "0")                         
                         {
                              _sDos = "VEINTE";
                         }
                         else
                         {
                              _sUne1 = "";
                              _sDos = "VEINTI";
                         }
                    }
                    else if (F == "3")                    
                    {
                         _sDos = "TREINTA";
                    }
                    else if (F == "4")                    
                    {
                         _sDos = "CUARENTA";
                    }
                    else if (F == "5")                    
                    {
                         _sDos = "CINCUENTA";
                    }
                    else if (F == "6")                    
                    {
                         _sDos = "SESENTA";
                    }
                    else if (F == "7")                    
                    {
                         _sDos = "SETENTA";
                    }
                    else if (F == "8")                    
                    {
                         _sDos = "OCHENTA";
                    }
                    else if (F == "9")                    
                    {
                         _sDos = "NOVENTA";
                    }
               }
          }
     }
}