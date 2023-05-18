using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace EZClaimAPIHelper.UT
{
    public class APIUnitTestHelperObject
    {
        public static IConfigurationRoot config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

        public static string ExampleRSAPublicKey = config["ExampleRSAPublicKey"].ToString();
        public static string ProductionRSAPublicKey = config["ProductionRSAPublicKey"].ToString();

        public static string s01Token = config["s01Token"].ToString();
        public static string TestToken = config["TestToken"].ToString();

        public static string TestToken_SelectPatient = config["TestToken_SelectPatient"].ToString();
        public static string TestToken_CreatePatient = config["TestToken_CreatePatient"].ToString();
        public static string TestToken_UpdatePatient = config["TestToken_UpdatePatient"].ToString();
        public static string TestToken_DeletePatient = config["TestToken_DeletePatient"].ToString();
        public static string TestToken2_NotYetRegisteredToken = config["TestToken2_NotYetRegisteredToken"].ToString();

        public static string s01Token_CreatePatient = config["s01Token_CreatePatient"].ToString();
        public static string s01Token_SameClientDifferentDatabase = config["s01Token_SameClientDifferentDatabase"].ToString();

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
        public dynamic ResponseMessage;
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

        public void GeneratePostmanCallForAPICall(HttpMethod method)
        {
            string encryptedString = Convert.ToBase64String(AESHelper.EncryptStringToBytes(APIBody, AESKey, AESIV));

            APIHelper.CreatePostmanFileForAPICall(Endpoint, method, encryptedString, Token, Convert.ToBase64String(AESIV), GetEncryptedAESKey(), BaseAddress);
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

            SetupResponseValues(setResponseValues);
        }

        public void RunAPICall_Manually(HttpMethod method, string body, string token, string aesIV, string aesKey, bool setResponseValues = true)
        {
            Response = APIHelper.RunAPICall(Endpoint, method, body, token, aesIV, aesKey, BaseAddress);

            SetupResponseValues(setResponseValues);
        }

        public void SetupResponseValues(bool setResponseValues = true)
        {
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
            try
            {
                ResponseMessage = ResponseDynamicResult.Message;
            }
            catch { }
        }

        public void SetResponseError()
        {
            ResponseErrorResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponseContent);

        }
    }
}