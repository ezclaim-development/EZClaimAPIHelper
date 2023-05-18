using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EZClaimAPIHelper
{
    public class APIHelper
    {
        public static HttpResponseMessage RunAPICall(string endpoint, HttpMethod httpMethod, string encryptedStringInBase64, string token, string aesIVAsBase64String, string encryptedAESKeyAsBase64String, string baseAddress)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 0, 30); //30 seconds
                httpClient.BaseAddress = new Uri(baseAddress);


                return httpClient.SendAsync(getRequestMessage(endpoint, httpMethod, encryptedStringInBase64, token, aesIVAsBase64String, encryptedAESKeyAsBase64String)).Result;
            }
        }

        public static void CreatePostmanFileForAPICall(string endpoint, HttpMethod httpMethod, string encryptedStringInBase64, string token, string aesIVAsBase64String, string encryptedAESKeyAsBase64String, string baseAddress)
        {
            PostmanHelper.GeneratePostmanScript(baseAddress, getRequestMessage(endpoint, httpMethod, encryptedStringInBase64, token, aesIVAsBase64String, encryptedAESKeyAsBase64String));
        }

        private static HttpRequestMessage getRequestMessage(string endpoint, HttpMethod httpMethod, string encryptedStringInBase64, string token, string aesIVAsBase64String, string encryptedAESKeyAsBase64String)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, endpoint);

            if (token != null)
            {
                requestMessage.Headers.Add("Token", token);
            }

            if (aesIVAsBase64String != null)
            {

                requestMessage.Headers.Add("AESIVAsBase64String", aesIVAsBase64String);
            }

            if (encryptedAESKeyAsBase64String != null)
            {

                requestMessage.Headers.Add("EncryptedAESKeyAsBase64String", encryptedAESKeyAsBase64String);
            }

            if (encryptedStringInBase64 != null)
            {
                requestMessage.Content = new StringContent($"\"{encryptedStringInBase64}\"", Encoding.UTF8, "application/json");
            }

            return requestMessage;
        }
    }
}
