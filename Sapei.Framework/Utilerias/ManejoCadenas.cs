using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using QRCoder;
using System.Drawing.Imaging;
using ZXing;

namespace Sapei.Framework.Utilerias
{
	/// <summary>
	/// Clase para el manejo de strings
	/// </summary>
	public static class ManejoCadenas
	{
		/// <summary>
		/// Verifica si la cadenas es null
		/// </summary>
		/// <param name="poObjeto"></param>
		/// <param name="psCadena"></param>
		/// <returns></returns>
		public static string VerificaNull(object poObjeto, string psCadena)
		{
			if (poObjeto == null)
			{
				psCadena = "";
			}
			else
			{
				psCadena = poObjeto.ToString();
			}
			return psCadena;
		}

		/// <summary>
		/// Verifica si una cadena es nula o vacia
		/// </summary>
		/// <param name="psCadena">Cadena</param>
		/// <returns></returns>
		public static bool EsNulaoVacia(this string psCadena)
		{
			return String.IsNullOrEmpty(psCadena.Trim());
		}

		/// <summary> 
		/// Metodo para determinar si una cadena se puede convertir en boolean.
		/// </summary>
		/// <param name="psCadena">La cadena.</param>
		/// <returns></returns>
		/// <exception cref="System.InvalidCastException">No se puede convertir en bool!</exception>
		public static bool ToBoolean(this string psCadena)
		{
			if (String.IsNullOrEmpty(psCadena))
				psCadena = "";
			switch (psCadena.ToLower())
			{
				case "true":
				case "t":
				case "1":
				case "si":
				case "yes":
				case "y":
				case "s":
					return true;
				case "0":
				case "false":
				case "f":
				case "":
				case "no":
				case "n":
					return false;
				default:
					return false;
			}
		}

		/// <summary>
		/// Funcion que regresa un array lista con las mascaras
		/// </summary>
		/// <param name="pbElementoVacio"></param>
		/// <returns></returns>
		public static ArrayList RegresaMascaras(bool pbElementoVacio = false)
		{
			ArrayList lasMascaras = null;
			lasMascaras = new ArrayList();
			if (pbElementoVacio)
			{
				lasMascaras.Add(",");
			}
			lasMascaras.Add("Texto, X");
			lasMascaras.Add("Entero, 9999");
			lasMascaras.Add("Decimal, 9999.00");
			lasMascaras.Add("Moneda, 9,999.00");
			lasMascaras.Add("Personalizada, @");
			return lasMascaras;
		}

		/// <summary>
		/// Esta función sirve para el formateo de un string, como en clipper
		/// </summary>
		/// <param name="psCadena"></param>
		/// <param name="psCaracterRelleno"></param>
		/// <param name="pbOrientacionDerecho"></param>
		/// <param name="liLongitud"></param>
		/// <returns></returns>
		public static string Pad(string psCadena, string psCaracterRelleno, bool pbOrientacionDerecho, int liLongitud)
		{
			string lsFunctionReturnValue;
			string lsEspacio;

			if (String.IsNullOrEmpty(psCadena))
			{
				psCadena = "";
			}
			if (psCadena.Length > liLongitud)
			{
				lsFunctionReturnValue = psCadena.Substring(1, liLongitud);
			}
			else
			{
				if (pbOrientacionDerecho)
				{
					lsEspacio = new String(' ', liLongitud - psCadena.ToString().Length);
					lsEspacio = lsEspacio.Replace(" ", psCaracterRelleno);
					lsFunctionReturnValue = string.Format("{0}{1}", lsEspacio, psCadena);
				}
				else
				{
					if (liLongitud == psCadena.Length)
					{
						lsFunctionReturnValue = psCadena;
					}
					else
					{
						lsEspacio = new String(' ', liLongitud - psCadena.Length);
						lsEspacio = lsEspacio.Replace(" ", psCaracterRelleno);
						lsFunctionReturnValue = string.Format("{0}{1}", psCadena, lsEspacio);
					}
				}
			}
			return lsFunctionReturnValue;
		}

		/// <summary>
		/// Funcion que elimina caracteres especiales de una cadena
		/// </summary>
		/// <param name="psMensaje"></param>
		/// <returns></returns>
		public static string LimpiaMensaje(this string psMensaje)
		{
			string lsPatron = null;
			System.Text.RegularExpressions.Regex loRegularExp = null;
			psMensaje = psMensaje.Replace("\\", "/");
			lsPatron = string.Format("&[^a-zA-Z0-9()@!¡?¿ñÑ.:,;áéíóúÁÉÍÓÚ\\/ {0}]", (char)34);
			loRegularExp = new System.Text.RegularExpressions.Regex(lsPatron);
			psMensaje = psMensaje.Replace(System.Environment.NewLine, "  ");
			psMensaje = psMensaje.Replace("\r", "  ");
			psMensaje = psMensaje.Replace("'", "");
			return loRegularExp.Replace(psMensaje, "  ");
		}

		/// <summary>
		/// Funcion que cambia las letra Ñ,ñ por N, N respectivamente
		/// </summary>
		/// <param name="psMensaje"></param>
		/// <returns></returns>
		public static string CambiaMensaje(this string psMensaje)
		{
			psMensaje = psMensaje.Replace("Ñ", "N");
			psMensaje = psMensaje.Replace("ñ", "n");
			return psMensaje;
		}

		/// <summary>
		/// Regresa un numero de caracteres de derecha a izquierda
		/// </summary>
		/// <param name="psValue"></param>
		/// <param name="piMaxLength"></param>
		/// <returns></returns>
		public static string StringRight(this string psValue, int piMaxLength)
		{
			//Check if the value is valid
			if (string.IsNullOrEmpty(psValue))
			{
				//Set valid empty string as string could be null
				psValue = string.Empty;
			}
			else if (psValue.Length > piMaxLength)
			{
				//Make the string no longer than the max length
				psValue = psValue.Substring(psValue.Length - piMaxLength, piMaxLength);
			}
			//Return the string
			return psValue;
		}

		/// <summary>
		/// Regresa un numero de caracteres de izquierda a derecha
		/// </summary>
		/// <param name="psValue"></param>
		/// <param name="piMaxLength"></param>
		/// <returns></returns>
		public static string StringLeft(this string psValue, int piMaxLength)
		{
			//Check if the value is valid
			if (string.IsNullOrEmpty(psValue))
			{
				//Set valid empty string as string could be null
				psValue = string.Empty;
			}
			psValue = psValue.Substring(0, Math.Min(piMaxLength, psValue.Length));
			//Return the string
			return psValue;
		}

		/// <summary>
		/// Funcion que limpia el texto de caracteres especiales
		/// </summary>
		/// <param name="psTextoALimpiar"></param>
		/// <param name="pbSoportaEñe"></param>
		/// <param name="pbCambiaÑporN"></param>
		/// <returns></returns>
		public static string LimpiaTexto(this string psTextoALimpiar, bool pbSoportaEñe = false, bool pbCambiaÑporN = false)
		{
			string lsTextoLimpio = null;
			int liContador = 0;
			string lsCaracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 abcdefghijklmnopqrstuvwxyz";
			lsTextoLimpio = "";
			psTextoALimpiar = psTextoALimpiar.Trim();
			for (liContador = 1; liContador <= psTextoALimpiar.Length; liContador++)
			{
				if (string.Format("{0}{1}", lsCaracteresPermitidos, pbCambiaÑporN || pbSoportaEñe ? "Ññ" : "").IndexOf(psTextoALimpiar.Substring(liContador, 1), 1) > 0)
				{
					lsTextoLimpio = string.Format("{0}{1}", lsTextoLimpio, psTextoALimpiar.Substring(liContador, 1));
				}
			}
			if (pbCambiaÑporN)
			{
				lsTextoLimpio = lsTextoLimpio.Replace("Ñ", "N");
				lsTextoLimpio = lsTextoLimpio.Replace("ñ", "n");
			}
			lsTextoLimpio = lsTextoLimpio.Trim();
			return lsTextoLimpio;
		}

		/// <summary>
		/// Funcion que limpiea el textosegun un patron enviado
		/// </summary>
		/// <param name="psTextoALimpiar"></param>
		/// <param name="psCaracteresPermitidos"></param>
		/// <param name="pbSoportaEñe"></param>
		/// <param name="pbCambiaÑporN"></param>
		/// <returns></returns>
		public static string LimpiaTextoPersonalizado(string psTextoALimpiar, string psCaracteresPermitidos, bool pbSoportaEñe = false, bool pbCambiaÑporN = false)
		{
			string lsTextoLimpio = null;
			int liContador;
			lsTextoLimpio = "";
			psTextoALimpiar = psTextoALimpiar.Trim();
			for (liContador = 0; liContador < psTextoALimpiar.Length - 1; liContador++)
			{
				//int i = (psCaracteresPermitidos + (pbCambiaÑporN || pbSoportaEñe ? "Ññ" : "")).IndexOf(psTextoALimpiar.Substring(liContador, 1),1);
				if (string.Format("{0}{1}", psCaracteresPermitidos, pbCambiaÑporN || pbSoportaEñe ? "Ññ" : "").IndexOf(psTextoALimpiar.Substring(liContador, 1), 1) > 0)
				{
					lsTextoLimpio = string.Format("{0}{1}", lsTextoLimpio, psTextoALimpiar.Substring(liContador, 1));
				}
			}
			if (pbCambiaÑporN)
			{
				lsTextoLimpio = lsTextoLimpio.Replace("Ñ", "N");
				lsTextoLimpio = lsTextoLimpio.Replace("ñ", "n");
			}
			lsTextoLimpio = lsTextoLimpio.Trim();
			return lsTextoLimpio;
		}

		/// <summary>
		/// Funcion que quita acentos
		/// </summary>
		/// <param name="psTexto"></param>
		/// <returns></returns>
		public static string QuitaAcento(this string psTexto)
		{
			psTexto = psTexto.Replace("Á", "A");
			psTexto = psTexto.Replace("É", "E");
			psTexto = psTexto.Replace("Í", "I");
			psTexto = psTexto.Replace("Ó", "O");
			psTexto = psTexto.Replace("Ú", "U");
			psTexto = psTexto.Replace("á", "a");
			psTexto = psTexto.Replace("é", "e");
			psTexto = psTexto.Replace("í", "i");
			psTexto = psTexto.Replace("ó", "o");
			psTexto = psTexto.Replace("ú", "u");
			psTexto = psTexto.Replace("Ñ", "N");
			psTexto = psTexto.Replace("ñ", "n");
			return psTexto;
		}

		/// <summary>
		/// Funcion que valida el RFC
		/// </summary>
		/// <param name="psRfc"></param>
		/// <returns></returns>
		public static bool ValidaRFC(this string psRfc)
		{
			int liIndex = 0;
			int liresult;
			switch (psRfc.Trim().Length)
			{
				case 13:
					break;
				case 12:
					psRfc = string.Format("X{0}", psRfc);
					//Se completan las 13 posiciones
					break;
				default:
					return false;
			}

			for (liIndex = 0; liIndex <= 13; liIndex++)
			{
				switch (liIndex)
				{
					case 1:
					case 2:
					case 3:
					case 4:
						if (Int32.TryParse(psRfc.Substring(liIndex, 1), out liresult))
						{
							return false;
						}
						break;
					case 5:
					case 6:
					case 7:
					case 8:
					case 10:
						if (!Int32.TryParse(psRfc.Substring(liIndex, 1), out liresult))
						{
							return false;
						}
						break;
				}
			}

			return true;
		}

		/// <summary>
		/// Funcion que valida el codigo postal
		/// </summary>
		/// <param name="psCodigoPostal"></param>
		/// <returns></returns>
		public static bool ValidaCP(this string psCodigoPostal)
		{
			int liresult;
			return Int32.TryParse(psCodigoPostal, out liresult) && psCodigoPostal.Trim().Length == 5;
		}
		/// <summary>
		/// Fucnion que valida los caracteres del rfc
		/// </summary>
		/// <param name="psRfc"></param>
		/// <returns></returns>
		public static bool ValidaCaracteresRFC(this string psRfc)
		{
			int liInteger = 0;
			string lsABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789&";
			psRfc = psRfc.ToUpper();
			for (liInteger = 1; liInteger <= psRfc.Length; liInteger++)
			{
				if (lsABC.IndexOf(psRfc.Substring(liInteger, 1)) == 0)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Determina si la primera letra de una cadena de 
		/// caracteres es una letra mayúscula (capitalizada):
		/// </summary>
		/// <param name="psCadena"></param>
		/// <returns></returns>
		public static bool EstaCapitalizada(this String psCadena)
		{
			if (String.IsNullOrEmpty(psCadena))
			{
				return false;
			}
			return Char.IsUpper(psCadena[0]);
		}
		/// <summary>
		/// Determina si la primera letra de una cadena de 
		/// caracteres es una letra mayúscula (capitalizada):
		/// </summary>
		/// <param name="psCadena"></param>
		/// <returns></returns>
		public static string Capitalizada(this String psCadena)
		{
			if (String.IsNullOrEmpty(psCadena))
			{
				return "";
			}
			psCadena = psCadena.ToLower();
			return Char.ToUpper(psCadena[0]) + psCadena.Remove(0, 1);
		}
		/// <summary>
		///  Capitaliza una cadena de multiples palabras
		/// </summary>
		/// <param name="psCadena"></param>
		/// <returns></returns>
		public static string Capitalizadas(this String psCadena)
		{
			if (String.IsNullOrEmpty(psCadena))
			{
				return "";
			}
			psCadena = psCadena.ToLower();
			foreach (string lsPalabra in psCadena.Split(' '))
			{
				psCadena = psCadena.Replace(lsPalabra + " ", Char.ToUpper(lsPalabra[0]) + lsPalabra.Remove(0, 1) + " ");
				psCadena = psCadena.Replace(" " + lsPalabra, " " + Char.ToUpper(lsPalabra[0]) + lsPalabra.Remove(0, 1));
			}
			return psCadena;
		}
		/// <summary>
		/// Pluraliza una palabra:
		/// </summary>
		/// <param name="psCadena"></param>
		/// <returns></returns>
		public static string Pluralizar(this String psCadena)
		{
			if (String.IsNullOrEmpty(psCadena))
			{
				return "";
			}
			return String.Format("{0}s", psCadena);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="psNombre"></param>
		/// <param name="psExtension"></param>
		/// <returns></returns>
		public static string GeneraNombreAleatorio(string psNombre, string psExtension)
		{
			StringBuilder lsNombreArchivo;
			Random loGeneraNumeroRandom;
			lsNombreArchivo = new StringBuilder();
			loGeneraNumeroRandom = new Random();
			lsNombreArchivo.AppendFormat("{0}", psNombre);
			lsNombreArchivo.AppendFormat("{0}{1}{2}{3}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Second);
			lsNombreArchivo.AppendFormat("{0}", loGeneraNumeroRandom.Next(1000, 9999));
			lsNombreArchivo.AppendFormat("{0}", psExtension);
			return lsNombreArchivo.ToString();
		}

		/// <summary>
		/// Funcion que regresa los valores que existan entre corchetes
		/// </summary>
		/// <param name="psCadena"></param>
		/// <returns></returns>
		public static string RegresaValorEntreCorchetes(this string psCadena)
		{
			if (psCadena.Contains("[") && psCadena.Contains("]"))
			{
				return psCadena.Substring(psCadena.IndexOf("[") + 1, psCadena.IndexOf("]") - 1).Trim();
			}
			return "";
		}

		/// <summary>
		/// Fucnion que regresa el valor desde de los corchetes
		/// </summary>
		/// <param name="psCadena"></param>
		/// <returns></returns>
		public static string RegresaValorDespuesDeCorchetes(this string psCadena)
		{
			if (psCadena.Contains("]"))
			{
				return psCadena.Substring(psCadena.IndexOf("]") + 1).Trim();
			}
			return "";
		}

		/// <summary>
		/// Fucnion que valida si es correo electronico valido
		/// </summary>
		/// <param name="psEmail"></param>
		/// <returns></returns>
		public static bool EsCorreoCorrecto(this string psEmail)
		{
			string lsExpresion;
			lsExpresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
			//psExpresion = "(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$";
			if (Regex.IsMatch(psEmail, lsExpresion))
			{
				if (Regex.Replace(psEmail, lsExpresion, String.Empty).Length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Validas the expresion regural.
		/// </summary>
		/// <param name="psCadena">The ps cadena.</param>
		/// <param name="psExpresion">The ps expresion.</param>
		/// <returns></returns>
		public static bool ValidaExpresionRegural(this string psCadena, string psExpresion)
		{
			if (Regex.IsMatch(psCadena, psExpresion))
			{
				if (Regex.Replace(psCadena, psExpresion, String.Empty).Length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Quita caracteres extraños en el texto usando un patron
		/// </summary>
		/// <param name="psCadena">The ps cadena.</param>
		/// <returns></returns>
		public static string QuitarCaracteresRaros(this string psCadena)
		{
			// Replace invalid characters with empty strings.
			try
			{
				return Regex.Replace(psCadena, @"[^\w\.@-]", " ",
					 RegexOptions.None, TimeSpan.FromSeconds(1.5));
			}
			// If we timeout when replacing invalid characters, 
			// we should return Empty.
			catch (RegexMatchTimeoutException)
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// Caractereses the especiales HTML.
		/// Adding special characters in HTML code
		/// http://www.telerik.com/support/kb/aspnet-ajax/details/adding-special-characters-in-html-code
		/// </summary>
		/// <param name="psCadena">Cadena.</param>
		/// <returns></returns>
		public static string CaracteresEspecialesHTML(this string psCadena)
		{
			StringBuilder lsCadena;
			lsCadena = new StringBuilder(psCadena);
			//Puntuacion
			lsCadena.Replace("¡", "&iexcl;");
			lsCadena.Replace("¿", "&iquest;");
			lsCadena.Replace("\"", "&quot;");
			lsCadena.Replace("\\", "_");
			lsCadena.Replace("/", "_");
			lsCadena.Replace("«", "&laquo");
			lsCadena.Replace("»", "&raquo;");
			//Simbolos
			lsCadena.Replace("&", "&amp;");
			lsCadena.Replace("¢", "&cent;");
			lsCadena.Replace("©", "&copy;");
			lsCadena.Replace("÷", "&divide;");
			lsCadena.Replace(">", "&gt;");
			lsCadena.Replace("<", "&lt;");
			lsCadena.Replace("µ", "");
			lsCadena.Replace("·", "&middot;");
			lsCadena.Replace("¶", "&para;");
			lsCadena.Replace("±", "&plusmn;");
			lsCadena.Replace("£", "&pound;");
			lsCadena.Replace("®", "&reg;");
			lsCadena.Replace("§", "&sect;");
			lsCadena.Replace("¥", "&yen;");
			//Acentos
			lsCadena.Replace("á", "&aacute;");
			lsCadena.Replace("Á", "&Aacute;");
			lsCadena.Replace("à", "&agrave;");
			lsCadena.Replace("À", "&Agrave;");
			lsCadena.Replace("â", "&acirc;");
			lsCadena.Replace("Â", "&Acric;");
			lsCadena.Replace("å", "&aring;");
			lsCadena.Replace("Å", "&Aring;");
			lsCadena.Replace("ã", "&atilde;");
			lsCadena.Replace("Ã", "&Atilde;");
			lsCadena.Replace("ä", "&auml;");
			lsCadena.Replace("Ä", "&Auml;");
			lsCadena.Replace("æ", "&aelig;");
			lsCadena.Replace("Æ", "&AElig;");
			lsCadena.Replace("ç", "&ccedil;");
			lsCadena.Replace("Ç", "&Ccedil;");
			lsCadena.Replace("é", "&eacute;");
			lsCadena.Replace("É", "&Eacute;");
			lsCadena.Replace("è", "&egrave;");
			lsCadena.Replace("È", "&Egrave;");
			lsCadena.Replace("ê", "&ecirc;");
			lsCadena.Replace("Ê", "&Ecirc;");
			lsCadena.Replace("ë", "&euml;");
			lsCadena.Replace("Ë", "&Euml;");
			lsCadena.Replace("í", "&iacute;");
			lsCadena.Replace("Í", " &Iacute;");
			lsCadena.Replace("ì", "&igrave;");
			lsCadena.Replace("Ì", "&Igrave;");
			lsCadena.Replace("î", "&icirc;");
			lsCadena.Replace("Î", "&Icirc;");
			lsCadena.Replace("ï", "&iuml;");
			lsCadena.Replace("Ï", "&Iuml;");
			lsCadena.Replace("ñ", "&ntilde;");
			lsCadena.Replace("Ñ", "&Ntilde;");
			lsCadena.Replace("ó", "&oacute;");
			lsCadena.Replace("Ó", "&Oacute;");
			lsCadena.Replace("ò", "&ograve;");
			lsCadena.Replace("Ò", "&Ograve;");
			lsCadena.Replace("ô", "&ocirc;");
			lsCadena.Replace("Ô", "&Ocirc;");
			lsCadena.Replace("Ø", "&oslash;");
			lsCadena.Replace("Ø", "&Oslash;");
			lsCadena.Replace("õ", "&otilde;");
			lsCadena.Replace("Õ", "&Otilde;");
			lsCadena.Replace("ö", "&ouml;");
			lsCadena.Replace("Ö", "&Ouml;");
			lsCadena.Replace("ß", "&szlig;");
			lsCadena.Replace("ú", "&uacute;");
			lsCadena.Replace("Ú", "&Uacute;");
			lsCadena.Replace("ù", "&ugrave;");
			lsCadena.Replace("Ù", "&Ugrave;");
			lsCadena.Replace("û", "&ucirc;");
			lsCadena.Replace("Û", "&Ucirc;");
			lsCadena.Replace("ü", "&uuml;");
			lsCadena.Replace("Ü", "&Uuml;");
			lsCadena.Replace("ÿ", "&yuml;");
			lsCadena.Replace("´", "`");
			return lsCadena.ToString();
		}

		/// <summary>
		/// Determina si la cadena contiene una cultura correcta
		/// </summary>
		/// <param name="psCultura">The ps cultura.</param>
		/// <returns></returns>
		public static bool EsCulturaValida(this string psCultura)
		{
			try
			{
				System.Globalization.CultureInfo.CreateSpecificCulture(psCultura);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Checks string object's value to array of string values
		/// </summary>                  
		/// <param name="psValue">The value.</param>
		/// <param name="psStringValues">Array of string values to compare</param>
		/// <returns>Return true if any string value matches</returns>
		public static bool In(this string psValue, params string[] psStringValues)
		{
			foreach (string otherValue in psStringValues)
			{
				if (string.Compare(psValue, otherValue) == 0)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Converts string to enum object
		/// </summary>
		/// <typeparam name="T">Type of enum</typeparam>
		/// <param name="psValue">String value to convert</param>
		/// <returns>Returns enum object</returns>
		public static T ToEnum<T>(this string psValue)
			 where T : struct
		{
			return (T)System.Enum.Parse(typeof(T), psValue, true);
		}

		/// <summary>
		///  Replaces the format item in a specified System.String with the text equivalent
		///  of the value of a specified System.Object instance.
		/// </summary>
		/// <param name="psValue">A composite format string</param>
		/// <param name="poArg">An System.Object to format</param>
		/// <returns>A copy of format in which the first format item has been replaced by the
		/// System.String equivalent of arg0</returns>
		public static string Format(this string psValue, object poArg)
		{
			return string.Format(psValue, poArg);
		}

		/// <summary>
		///  Replaces the format item in a specified System.String with the text equivalent
		///  of the value of a specified System.Object instance.
		/// </summary>
		/// <param name="psValue">A composite format string</param>
		/// <param name="paoArgs">An System.Object array containing zero or more objects to format.</param>
		/// <returns>A copy of format in which the format items have been replaced by the System.String
		/// equivalent of the corresponding instances of System.Object in args.</returns>
		public static string Format(this string psValue, params object[] paoArgs)
		{
			return string.Format(psValue, paoArgs);
		}

		/// <summary>
		/// Subs the string.
		/// </summary>
		/// <param name="psValue">The value.</param>
		/// <param name="startindex">The startindex.</param>
		/// <param name="piLenght">The lenght.</param>
		/// <returns></returns>
		public static string SubString(this string psValue, int startindex, int piLenght)
		{
			int liLongitud;
			if (psValue.Length < (piLenght + startindex))
			{
				liLongitud = psValue.Length - startindex;
				return psValue.Substring(startindex, liLongitud);
			}
			return psValue.Substring(startindex, piLenght);
		}

		/// <summary>
		/// To the exception.
		/// </summary>
		/// <param name="psMensaje">The ps mensaje.</param>
		/// <returns></returns>
		public static SolExcepcion ToException(this string psMensaje)
		{
			return new SolExcepcion(psMensaje.ToString());
		}

		/// <summary>
		/// Determines whether /[is strong password].
		/// </summary>
		/// <param name="psValor">The s.</param>
		/// <returns></returns>
		public static bool IsStrongPassword(this string psValor)
		{
			bool lbIsStrong = Regex.IsMatch(psValor, @"[\d]");
			if (lbIsStrong)
			{
				lbIsStrong = Regex.IsMatch(psValor, @"[a-z]");
			}
			if (lbIsStrong)
			{
				lbIsStrong = Regex.IsMatch(psValor, @"[A-Z]");
			}
			if (lbIsStrong)
			{
				lbIsStrong = Regex.IsMatch(psValor, @"[\s~!@#\$%\^&\*\(\)\{\}\|\[\]\\:;'?,.`+=<>\/]");
			}
			if (lbIsStrong)
			{
				lbIsStrong = psValor.Length > 7;
			}
			return lbIsStrong;
		}

		/// <summary>
		/// Truncates the string to a specified length and replace the truncated to a ...
		/// </summary>
		/// <param name="psText">string that will be truncated</param>
		/// <param name="piMaxLength">total length of characters to maintain before the truncate happens</param>
		/// <returns>truncated string</returns>
		public static string Truncate(this string psText, int piMaxLength)
		{
			// replaces the truncated string to a ...
			string lsSuffix = "...";
			string lsTruncatedString = psText;

			if (piMaxLength <= 0)
			{
				return lsTruncatedString;
			}
			int strLength = piMaxLength - lsSuffix.Length;

			if (strLength <= 0)
			{
				return lsTruncatedString;
			}

			if (psText == null || psText.Length <= piMaxLength)
			{
				return lsTruncatedString;
			}

			lsTruncatedString = psText.Substring(0, strLength);
			lsTruncatedString = lsTruncatedString.TrimEnd();
			lsTruncatedString += lsSuffix;
			return lsTruncatedString;
		}

		/// <summary>
		/// Obteners the ruta windows local.
		/// </summary>
		/// <param name="psVariableRutaIni">The ps variable ruta ini.</param>
		/// <param name="psComnplementoIni">The ps comnplemento ini.</param>
		/// <returns></returns>
		public static string ObtenerRutaWindowsLocal(string psVariableRutaIni, string psComnplementoIni)
		{
			string lsRuta;
			lsRuta = "";
			if (String.IsNullOrEmpty(psVariableRutaIni))
			{
				if (!String.IsNullOrEmpty(psComnplementoIni))
				{
					if (System.IO.Directory.Exists(psComnplementoIni))
					{
						return psComnplementoIni;
					}
				}
				return lsRuta;
			}
			else
			{
				if (String.IsNullOrEmpty(psComnplementoIni))
				{
					return lsRuta;
				}
			}
			lsRuta = string.Format("{0}\\{1}", System.Environment.GetEnvironmentVariable(psVariableRutaIni), psComnplementoIni);
			if (System.IO.Directory.Exists(lsRuta))
			{
				return lsRuta;
			}
			else
			{
				return "";
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="psText"></param>
		/// <returns></returns>
		public static string LTrim(this string psText)
		{
			int k;
			for (k = 0; k < psText.Length && EsEspacioEnBlanco(psText.ElementAt(k)); k++) ;
			return psText.Substring(k, psText.Length);
		}
		public static string LTrim(this string psText, char pcCaracter)
		{
			int k;
			for (k = 0; k < psText.Length && psText.ElementAt(k) == pcCaracter; k++) ;
			return psText.Substring(k, psText.Length - k);
		}
		public static string RTrim(this string psText)
		{
			int j;
			for (j = psText.Length - 1; j >= 0 && EsEspacioEnBlanco(psText.ElementAt(j)); j--) ;
			return psText.Substring(0, j + 1);
		}
		/// <summary>
		/// Valida si es espacio en blanco
		/// </summary>
		/// <param name="pcCaracter"></param>
		/// <returns></returns>
		public static bool EsEspacioEnBlanco(char pcCaracter)
		{
			var whitespaceChars = " \t\n\r\f";
			return (whitespaceChars.IndexOf(pcCaracter) != -1);
		}
		public static string EliminaEspaciosEnBlanco(this string psText)
		{
			string lsAnterior;
			string lsActual;
			bool lbFin;
			lbFin = false;
			lsAnterior = psText.Trim();
			do
			{
				lsActual = lsAnterior.Replace("  ", " ");
				if (lsAnterior != lsActual)
					lsAnterior = lsActual;
				else
					lbFin = true;
			} while (!lbFin);
			return lsActual;
		}

		public static string Base64Encode(this string psTexto)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(psTexto);
			return System.Convert.ToBase64String(plainTextBytes);
		}
		public static string Base64Decode(this string psTexto)
		{
			var base64EncodedBytes = System.Convert.FromBase64String(psTexto);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
		public static Sapei.Framework.Configuracion.enmTipoUsuario RegresaTipoUsuario(this string psTexto)
		{
			if (EsNoControl(psTexto))
				return Configuracion.enmTipoUsuario.ESTUDIANTE;
			if (EsRFC(psTexto))
				return Configuracion.enmTipoUsuario.PERSONAL;
			return Configuracion.enmTipoUsuario.NINGUNO;
		}
		public static bool EsNoControl(this string psTexto)
		{
			var lsRegex = @"^((R|C){1})*([1,2]{1})([0-9]{1})([0-9]{3})([0-9]{3,5})$";
			return Regex.IsMatch(psTexto, lsRegex);
		}
		public static bool EsPersonal(this string psTexto)
		{
			var lsRegex = @"^[A-ZÑ\x26|a-zñ\x26]{5,30}([0-9]{1,5})?$";
			return Regex.IsMatch(psTexto, lsRegex);
		}
		public static bool EsRFC(this string psTexto)
		{
			var lsRegex = @"^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$";
			return Regex.IsMatch(psTexto, lsRegex);
		}
		public static byte[] RegresaCadenaQR(this string psTexto)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				QRCodeGenerator qrGenerator = new QRCodeGenerator();
				QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(psTexto, QRCodeGenerator.ECCLevel.Q);
				using (Bitmap bitMap = qrCode.GetGraphic(20))
				{
					bitMap.Save(ms, ImageFormat.Png);
					//ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
					return ms.ToArray();
				}
			}
		}
		public static byte[] RegresaCodigoQR(this string psTexto, int piWidth = 500, int piHeigth = 500)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				BarcodeWriter loWriter = new BarcodeWriter() { Format = BarcodeFormat.QR_CODE };
				loWriter.Options = new ZXing.Common.EncodingOptions
				{
					Width = piWidth,
					Height = piHeigth
				};

				using (Bitmap bitMap = loWriter.Write(psTexto))
				{
					bitMap.Save(ms, ImageFormat.Png);
					return ms.ToArray();
				}
			}
		}
		public static byte[] RegresaCodigoBarras128(this string psTexto)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				BarcodeWriter loWriter = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
				using (Bitmap bitMap = loWriter.Write(psTexto))
				{
					bitMap.Save(ms, ImageFormat.Png);
					return ms.ToArray();
				}
			}
		}
		public static byte[] RegresaQRValidacionDocumentos(this string psTexto)
		{
			//Uri loURL = new Uri("http://192.168.9.248/ValidadorDocumentos/Index?psCadena=");
			Uri loURL = new Uri("https://sapei.tlahuac.tecnm.mx/ValidadorDocumentos/Index?psCadena=");
			string lsCadena = loURL.OriginalString + psTexto;
			return RegresaCodigoQR(lsCadena);
		}
		public static string RegresaCadenaQRValidacionDocumentos(this string psTexto)
		{
			//Uri loURL = new Uri("http://192.168.9.248/ValidadorDocumentos/Index?psCadena=");
			Uri loURL = new Uri("https://sapei.tlahuac.tecnm.mx/ValidadorDocumentos/Index?psCadena=");
			string lsCadena = loURL.OriginalString + psTexto;
			return Convert.ToBase64String(RegresaCodigoQR(lsCadena));
		}
		public static string RegresaUniqID(bool pbEntropy = false)
		{
			TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
			Random rnd = new Random();
			double t = ts.TotalMilliseconds / 1000;

			int a = (int)Math.Floor(t);
			int b = (int)((t - Math.Floor(t)) * 1000000);
			if (pbEntropy)
			{
				double c = (double)rnd.Next(10000000, 99999999) / 10000000;
				return a.ToString("x8") + b.ToString("x5") + c.ToString("f8");
			}
			return a.ToString("x8") + b.ToString("x5");
		}

		public static string UrlEncode(this string psUrl)
		{
			Dictionary<string, string> toBeEncoded = new Dictionary<string, string>() { { "%", "%25" }, { "!", "%21" }, { "#", "%23" }, { " ", "%2520" }, { "$", "%24" }, { "&", "%26" }, { "'", "%27" }, { "(", "%28" }, { ")", "%29" }, { "*", "%2A" }, { "+", "%2B" }, { ",", "%2C" }, { "/", "%2F" }, { ":", "%253A" }, { ";", "%3B" }, { "=", "%3D" }, { "?", "%3F" }, { "@", "%2540" }, { "[", "%5B" }, { "]", "%5D" } };
			Regex replaceRegex = new Regex(@"[%!# $&'()*+,/:;=?@\[\]]");
			MatchEvaluator matchEval = match => toBeEncoded[match.Value];
			string encoded = replaceRegex.Replace(psUrl, matchEval);
			return encoded;
		}

		public static string RegresaDesempeño(this string psPromedio)
		{
			float lfPromedio = float.Parse(psPromedio);

			if (lfPromedio >= 3.5)
				return "EXCELENTE";
			if (lfPromedio >= 2.5)
				return "NOTABLE";
			if (lfPromedio >= 1.5)
				return "BUENO";
			if (lfPromedio >= 1)
				return "SUFICIENTE";
			return "NA";
		}
		public static string SeparaFormatoCamello(this string psCadena)
		{
			string lsCadena = psCadena.Trim();
			if (string.IsNullOrEmpty(lsCadena))
				return "";
			if (lsCadena.All(c => char.IsUpper(c)))
				return lsCadena;
			lsCadena = System.Text.RegularExpressions.Regex.Replace(lsCadena, 
																	"([A-Z])", 
																	" $1", 
																	System.Text.RegularExpressions.RegexOptions.Compiled
																	).Trim();
			return lsCadena;
		}
		public static string CambiaComaPunto(this string psCadena)
		{
			return psCadena.Replace(',', '.');
		}
	}
}
