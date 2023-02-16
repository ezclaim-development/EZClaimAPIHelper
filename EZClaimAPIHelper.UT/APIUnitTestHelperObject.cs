using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EZClaimAPIHelper.UT
{
    public class APIUnitTestHelperObject
    {
        public static string ExampleRSAPublicKey = ConfigurationManager.AppSettings["ExampleRSAPublicKey"].ToString();
        public static string ProductionRSAPublicKey = ConfigurationManager.AppSettings["ProductionRSAPublicKey"].ToString();

        public static string s01Token = ConfigurationManager.AppSettings["s01Token"].ToString();
        public static string Client002844_ProviderPortalTestToken = ConfigurationManager.AppSettings["Client002844_ProviderPortalTestToken"].ToString();


        public byte[] AESKey;
        public byte[] AESIV;
        public string Endpoint;
        public string APIBody;
        public string RSAPublicKey;
        public string Token;
        public string BaseAddress;

        public HttpResponseMessage Response;
        public int ResponseStatus;
        public string ResponseContent;
        public dynamic ResponseDynamicResult;
        public dynamic ResponseData;
        public Dictionary<string, string> ResponseErrorResult;

        private string DecryptedResponseContent;
        private string EncryptedAESKey;

        public APIUnitTestHelperObject(byte[] aesKey, byte[] aesIV, string rsaPublicKey, string token, string baseAddress)
        {
            AESKey = aesKey;
            AESIV = aesIV;
            RSAPublicKey = rsaPublicKey;
            Token = token;
            BaseAddress = baseAddress;
        }

        public string GetEncryptedAESKey()
        {
            if (string.IsNullOrWhiteSpace(EncryptedAESKey))
            {
                EncryptedAESKey = RSAHelper.GetEncryptedAESKeyAsBase64(RSAPublicKey, Convert.ToBase64String(AESKey));
            }

            return EncryptedAESKey;
        }

        public void RunAPICall(HttpMethod method, bool setResponseValues = true)
        {
            string encryptedString = Convert.ToBase64String(AESHelper.EncryptStringToBytes(APIBody, AESKey, AESIV));

            Response = APIHelper.RunAPICall(Endpoint, method, encryptedString, Token, Convert.ToBase64String(AESIV), GetEncryptedAESKey(), BaseAddress);
            ResponseStatus = (int)Response.StatusCode;
            ResponseContent = Response.Content.ReadAsStringAsync().Result;

            if (setResponseValues)
            {
                if (ResponseStatus.Equals(200))
                {
                    SetData();
                }
                else
                {
                    SetResponseError();
                }
            }
        }

        public string GetDecryptedResponseContent()
        {
            DecryptedResponseContent = AESHelper.DecryptStringFromBytes(Convert.FromBase64String(ResponseContent), AESKey, AESIV);

            return DecryptedResponseContent;
        }

        public void SetData()
        {
            ResponseDynamicResult = JsonConvert.DeserializeObject<dynamic>(GetDecryptedResponseContent());
            ResponseData = ResponseDynamicResult.Data;
        }

        public void SetResponseError()
        {
            ResponseErrorResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponseContent);

        }
    }
}