using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapei.Framework.Configuracion
{
	[Serializable]
	public class ConfiguracionEvoBanamex
	{
        public bool UseSsl { get; set; }
        public bool IgnoreSslErrors { get; set; }
        public string GatewayHost { get; set; }
        public string GatewayUrl { get; set; }

        public string Version { get; set; }
        public string MerchantId { get; set; }
        public string Password { get; set; }
        public string ApiOperation { get; set; }
        public string Operation { get; set; }
        public string Username { get; set; }
        public string BillingAddress { get; set; }
        public string Timeout { get; set; }
        public string MerchantName { get; set; }
    }
}
