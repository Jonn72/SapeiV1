using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sapei.Framework.Utilerias.SeguridadGnosis
{
    class Configuraciones
    {
        public static string KEY_GNOSIS = "SII-ITT-GNOSIS-2017";
        public static string KEY_IP_SERVER = "GNOSISITTLAHUACEDUMX";
        public static string KEY_IV_SERVER_LOGIN_GNOSIS = "SII-ITT-GNOSIS-2017-2018-DMM";
        public static string EXP_NO_DE_CONTROL = "((R|C){1})*([1,2]{1})([0-9]{1})([0-9]{3})([0-9]{3,5})";
        public static string EXP_PASSWORD = "(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])([A-Za-z\\d$@$!%*?&]|[^ ]){8,15}$";

        //metodo para obtener la ip del cliente
        public static string GetIPAdress()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
    }
}
