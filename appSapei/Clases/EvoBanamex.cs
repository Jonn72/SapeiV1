using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.UI.HtmlControls;
using Sapei;
using Sapei.Framework.Utilerias;
using Sapei.Framework.Utilerias.Funciones;

namespace appSapei.Clases
{
	public class EvoBanamex
	{
        public string SessionID { get; set; }
        public string SuccessIndicator { get; set; }
        public string Error { get; set; }

        public void EnviaTransaccion(string psMonto, string psReferencia, string psDescripcion, string psId)
        {
            String result = null;
            String response = null;

            // get the request form and make sure to UrlDecode each value in case special characters used
            NameValueCollection loDatos = new NameValueCollection();

            loDatos.Add("apiOperation", SesionSapei.Sistema.Servidor.Principal.EvoBanamex.ApiOperation);

            loDatos.Add("interaction.operation", SesionSapei.Sistema.Servidor.Principal.EvoBanamex.Operation);
            loDatos.Add("interaction.displayControl.billingAddress", SesionSapei.Sistema.Servidor.Principal.EvoBanamex.BillingAddress);
            loDatos.Add("interaction.timeout", SesionSapei.Sistema.Servidor.Principal.EvoBanamex.Timeout);
            loDatos.Add("interaction.cancelUrl", "https://image.shutterstock.com/z/stock-vector-grunge-red-cancelled-with-star-icon-round-rubber-seal-stamp-on-white-background-789630058.jpg");
            loDatos.Add("interaction.returnUrl", "http://sapei.tlahuac.tecnm.mx/Estudiante/RegistraPagoBanca/");

            loDatos.Add("interaction.merchant.name", SesionSapei.Sistema.Servidor.Principal.EvoBanamex.MerchantName);
            loDatos.Add("order.amount", psMonto);
            loDatos.Add("order.reference", psReferencia);
            loDatos.Add("order.currency", "MXN");
            loDatos.Add("order.description", psDescripcion);
            loDatos.Add("order.id", psId);


            // [Snippet] howToConfigureURL - start
            StringBuilder url = new StringBuilder();
            url.Append(SesionSapei.Sistema.Servidor.Principal.EvoBanamex.GatewayHost);
            url.Append("/api/rest/version/");
            url.Append(SesionSapei.Sistema.Servidor.Principal.EvoBanamex.Version);
            url.Append("/merchant/");
            url.Append(SesionSapei.Sistema.Servidor.Principal.EvoBanamex.MerchantId);
            url.Append("/session");

            SesionSapei.Sistema.Servidor.Principal.EvoBanamex.GatewayUrl = url.ToString();


            // [Snippet] howToConvertFormData -- start
            String data = ManejoMensajesJson.BuildJsonFromNVC(loDatos);
            // [Snippet] howToConvertFormData -- end

         

            // send request/get results
            String operation = loDatos["apiOperation"];
            if (operation.Equals("RETRIEVE_TRANSACTION"))
            {
                response = this.GetTransaction();
            }
            else
            {
                response = this.SendTransaction(data);
            }

            // now convert JSON result string into a NameValueCollection
            NameValueCollection respValues = new NameValueCollection();
            respValues = ManejoMensajesJson.BuildNVCFromJson(response);

            // get overall success of transaction
            result = respValues["result"];

            //// Form error string if error is triggered
            //if (result != null && result.Equals("ERROR"))
            //{
            //    String errorMessage = null;
            //    String errorCode = null;

            //    String failureExplanations = respValues["explanation"];
            //    String supportCode = respValues["supportCode"];

            //    if (failureExplanations != null)
            //    {
            //        errorMessage = failureExplanations;
            //    }
            //    else if (supportCode != null)
            //    {
            //        errorMessage = supportCode;
            //    }
            //    else
            //    {
            //        errorMessage = "Reason unspecified.";
            //    }

            //    String failureCode = respValues["failureCode"];
            //    if (failureCode != null)
            //    {
            //        errorCode = "Error (" + failureCode + ")";
            //    }
            //    else
            //    {
            //        errorCode = "Error (No especificado)";
            //    }

            //}

            if (result != null && result.Equals("ERROR"))
                this.Error = "Error no específicado";

            this.SessionID = respValues["session.id"];
            this.SuccessIndicator = respValues["successIndicator"];
        }
        public String SendTransaction(String data)
        {
            var responseFields = new Dictionary<string, string>();

            string body = String.Empty;

            // code to validate certificate in an SSL conversation
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            HttpWebRequest request = WebRequest.Create(SesionSapei.Sistema.Servidor.Principal.EvoBanamex.GatewayUrl) as HttpWebRequest;

            request.Method = "POST";
            request.ContentType = "application/json;";
     

            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(SesionSapei.Sistema.Servidor.Principal.EvoBanamex.Username + ":" + SesionSapei.Sistema.Servidor.Principal.EvoBanamex.Password));
            request.Headers.Add("Authorization", "Basic " + credentials);

            // Create a byte array of the data we want to send
            byte[] utf8bytes = Encoding.UTF8.GetBytes(data);
            byte[] iso8859bytes = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("iso-8859-1"), utf8bytes);

            // Set the content length in the request headers
            request.ContentLength = iso8859bytes.Length;

            // Ignore format error checks before sending body
            request.ServicePoint.Expect100Continue = false;

            try
            {
                // [Snippet] executeSendTransaction - start
                // Write data
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(iso8859bytes, 0, iso8859bytes.Length);
                }
                // [Snippet] executeSendTransaction - end

                // Get response
                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        // Get the response stream
                        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("iso-8859-1"));
                        body = reader.ReadToEnd();
                    }
                }
                catch (WebException wex)
                {
                    StreamReader reader = new StreamReader(wex.Response.GetResponseStream(), Encoding.GetEncoding("iso-8859-1"));
                    body = reader.ReadToEnd();
                }
                return body;
            }
            catch (Exception ex)
            {
                return ex.Message + "\n\naddress:\n" + request.Address.ToString() + "\n\nheader:\n" + request.Headers.ToString() + "data submitted:\n" + data;
            }
        }

        public String GetTransaction()
        {
            var responseFields = new Dictionary<string, string>();
            string body = String.Empty;
            //ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            //{
            //    if (_merchant.IgnoreSslErrors)
            //    {
            //        // allow any certificate ... should be used for diagnostic purposes only
            //        return true;
            //    }
            //    else
            //    {
            //        return sslPolicyErrors == System.Net.Security.SslPolicyErrors.None;
            //    }
            //};
            //if (_merchant.UseProxy)
            //{
            //    WebProxy proxy = new WebProxy(_merchant.ProxyHost, true);
            //    if (!String.IsNullOrEmpty(_merchant.ProxyUser))
            //    {
            //        if (String.IsNullOrEmpty(_merchant.ProxyDomain))
            //        {
            //            proxy.Credentials = new NetworkCredential(_merchant.ProxyUser, _merchant.ProxyPassword);
            //        }
            //        else
            //        {
            //            proxy.Credentials = new NetworkCredential(_merchant.ProxyUser, _merchant.ProxyPassword, _merchant.ProxyDomain);
            //        }
            //    }
            //    WebRequest.DefaultWebProxy = proxy;
            //}
            // Create the web request
            HttpWebRequest request = WebRequest.Create(SesionSapei.Sistema.Servidor.Principal.EvoBanamex.GatewayUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json; charset=iso-8859-1";
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(SesionSapei.Sistema.Servidor.Principal.EvoBanamex.Username + ":" + SesionSapei.Sistema.Servidor.Principal.EvoBanamex.Password));
            request.Headers.Add("Authorization", "Basic " + credentials);
            request.ServicePoint.Expect100Continue = false;
            try
            {
                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        // Get the response stream
                        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("iso-8859-1"));
                        body = reader.ReadToEnd();
                    }
                }
                catch (WebException wex)
                {
                    StreamReader reader = new StreamReader(wex.Response.GetResponseStream(), Encoding.GetEncoding("iso-8859-1"));
                    body = reader.ReadToEnd();
                }
                return body;
            }
            catch (Exception ex)
            {
                return ex.Message + "\n\naddress:\n" + request.Address.ToString() + "\n\nheader:\n" + request.Headers.ToString();
            }
        }
    }
}