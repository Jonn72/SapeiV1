using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;


namespace Sapei.Framework.Utilerias.Funciones
{
     public static class  FuncionesCifrado
     {
          private const string _sKey = "lkirwf897+22#bbtrm8814z5qq=498j5"; //32 chr shared ascii string (32 * 8 = 256 bit)
          private const string _sIV = "741952hheeyy66#cs!9hjv887mxx7@8y"; //32 chr shared ascii string (32 * 8 = 256 bit)
          public static string EncryptRJ256(string prm_text_to_encrypt)
          {

               string sToEncrypt = prm_text_to_encrypt;

               RijndaelManaged myRijndael = new RijndaelManaged()
               {
                    Padding = PaddingMode.Zeros,
                    Mode = CipherMode.CBC,
                    KeySize = 256,
                    BlockSize = 256
               };

               byte[] key = Encoding.ASCII.GetBytes(_sKey);
               byte[] IV = Encoding.ASCII.GetBytes(_sIV);

               var encryptor = myRijndael.CreateEncryptor(key, IV);

               var msEncrypt = new MemoryStream();
               var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

               var toEncrypt = Encoding.ASCII.GetBytes(sToEncrypt);

               csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
               csEncrypt.FlushFinalBlock();

               var encrypted = msEncrypt.ToArray();

               return (Convert.ToBase64String(encrypted));
          }

          public static string DecryptRJ256(string prm_text_to_decrypt)
          {

               var sEncryptedString = prm_text_to_decrypt;

               var myRijndael = new RijndaelManaged()
               {
                    Padding = PaddingMode.Zeros,
                    Mode = CipherMode.CBC,
                    KeySize = 256,
                    BlockSize = 256
               };

               var key = Encoding.ASCII.GetBytes(_sKey);
               var IV = Encoding.ASCII.GetBytes(_sIV);

               var decryptor = myRijndael.CreateDecryptor(key, IV);

               var sEncrypted = Convert.FromBase64String(sEncryptedString);

               var fromEncrypt = new byte[sEncrypted.Length];

               var msDecrypt = new MemoryStream(sEncrypted);
               var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

               csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

               return (Encoding.ASCII.GetString(fromEncrypt));
          }

          public static string RegresaMensajeCifrado(string psDestinatario, string psUsuario, string psTitulo, string psMensaje, string psSeparador = "|")
          {
               string lsDestinatarioCifrado;
               string lsUsuarioCifrado;
               string lsTituloCifrado;
               string lsMensajeCifrado;

               lsDestinatarioCifrado = EncryptRJ256(psDestinatario);
               lsUsuarioCifrado = EncryptRJ256(psUsuario);
               lsTituloCifrado = EncryptRJ256(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(psTitulo)));
               lsMensajeCifrado = EncryptRJ256(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(psMensaje)));

               return string.Format("{1}{0}{2}{0}{3}{0}{4}",psSeparador,lsDestinatarioCifrado,lsUsuarioCifrado,lsTituloCifrado,lsMensajeCifrado);
          }

		public static string RegresaDatosDocumentoOficialCifrado(string psPeriodo, enmTiposDocumentos penmTipo, string psNoControl, string psNombre, string psOficio, string psFecha)
		{
			string lsCadena = String.Format("{0}&{1}&{2}&{3}&{4}&{5}",psPeriodo,penmTipo.ToString(),psNoControl,psNombre,psOficio,psFecha);
			return EncryptRJ256(lsCadena);
		}

		public static string RegresaCadenaFiEl(string psPeriodo, enmTiposDocumentos penmTipo, string psUsuario, string psNombre)
		{
			string lsCadena = String.Format("{0}&{1}&{2}&{3}&{4}", psPeriodo, penmTipo.ToString(), psUsuario, psNombre, DateTime.Now.ToShortDateString());
			return EncryptRJ256(lsCadena);
		}

        public static string RegresaFirmaPersonalMD5(string psFirma)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(psFirma);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            String hash = s.ToString();
            return hash;
        }

	}

}
