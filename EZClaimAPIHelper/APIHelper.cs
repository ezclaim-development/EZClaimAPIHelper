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

                HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, endpoint);

                requestMessage.Headers.Add("Token", token);
                requestMessage.Headers.Add("AESIVAsBase64String", aesIVAsBase64String);
                requestMessage.Headers.Add("EncryptedAESKeyAsBase64String", encryptedAESKeyAsBase64String);

                requestMessage.Content = new StringContent($"\"{encryptedStringInBase64}\"", Encoding.UTF8, "application/json");
                return httpClient.SendAsync(requestMessage).Result;
            }
        }
    }
}
