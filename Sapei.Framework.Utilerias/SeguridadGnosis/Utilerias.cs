using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Sapei.Framework.Utilerias.SeguridadGnosis;

namespace Sapei.Framework.Utilerias.SeguridadGnosis
{
    public class Utilerias
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public static string ObtenerTokenServer(string nuevo_token)
        {
            if (!nuevo_token.Trim().Equals(""))
            {
                CryptLib crypt = new CryptLib();
                String key = CryptLib.GetHashSha256(Configuraciones.KEY_GNOSIS, 32);
                return crypt.Encrypt(nuevo_token, key, Configuraciones.KEY_IV_SERVER_LOGIN_GNOSIS);
            }
            return null;
        }

        public static string[] DecifrarTokenServer(String token)
        {
            CryptLib _crypt = new CryptLib();
            String key = CryptLib.GetHashSha256(Configuraciones.KEY_GNOSIS, 32); //32 bytes = 256 bits
            String decypherText = _crypt.Decrypt(token, key, Configuraciones.KEY_IV_SERVER_LOGIN_GNOSIS);
            String[] substrings = decypherText.Split(' ');
            return substrings;
        }


    }
}
