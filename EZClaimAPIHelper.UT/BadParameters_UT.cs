using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public class BadParameters_UT
    {
        private readonly ITestOutputHelper output;

        public BadParameters_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        private List<string> expectedContainsValuesList = new();

        private enum ParameterType
        {
            AESIV,
            AESKey,
            Body,
            Token
        }

        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void BadParameters()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                using (Aes aes2 = Aes.Create())
                {
                    apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                    apiHelperObject.APIBody = "{}";

                    callManually_Equal(ref apiHelperObject, null, null, null, null, "Username and Password or Token are required headers.");

                    Thread.Sleep(3000);

                    callManually_Equal(ref apiHelperObject, "", "", "", "", "The supplied access credential could not be found.");

                    Thread.Sleep(3000);

                    callManually_Equal(ref apiHelperObject, " ", " ", " ", " ", "The supplied access credential could not be found.");

                    Thread.Sleep(3000);

                    callManually_Equal(ref apiHelperObject, "asdf", "asdf", "asdf", "asdf", "The supplied token parameter is invalid.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.Token, null, "Username and Password or Token are required headers.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.Token, "", "The supplied access credential could not be found.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.Token, " ", "The supplied access credential could not be found.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.Token, "asdf", "The supplied token parameter is invalid.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESIV, "", "The supplied access credential could not be found.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESIV, "", "The supplied access credential could not be found.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESIV, " ", "The supplied access credential could not be found.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESIV, "asdf", "Specified initialization vector (IV) does not match the block size for this algorithm.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESKey, "", "The supplied access credential could not be found.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESKey, "", "The supplied access credential could not be found.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESKey, " ", "The supplied access credential could not be found.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESKey, "asdf", "The length of the data to decrypt is not valid for the size of this key.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.Body, "asdf", "The input data is not a complete block.");

                    Thread.Sleep(3000);

                    string value = Convert.ToBase64String(AESHelper.EncryptStringToBytes(apiHelperObject.APIBody, aes2.Key, aes2.IV));

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.Body, value, "Padding is invalid and cannot be removed.");

                    Thread.Sleep(3000);

                    if (apiHelperObject.RSAPublicKey == APIUnitTestHelperObject.ExampleRSAPublicKey)
                    {
                        value = RSAHelper.GetEncryptedAESKeyAsBase64(APIUnitTestHelperObject.ProductionRSAPublicKey, Convert.ToBase64String(apiHelperObject.AESKey));

                        callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESKey, value, "The parameter is incorrect.");

                        Thread.Sleep(3000);

                        value = RSAHelper.GetEncryptedAESKeyAsBase64(APIUnitTestHelperObject.ProductionRSAPublicKey, Convert.ToBase64String(aes2.Key));
                        callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESKey, value, "The parameter is incorrect.");
                    }
                    else
                    {
                        value = RSAHelper.GetEncryptedAESKeyAsBase64(APIUnitTestHelperObject.ExampleRSAPublicKey, Convert.ToBase64String(apiHelperObject.AESKey));

                        callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESKey, value, "The parameter is incorrect.");

                        Thread.Sleep(3000);

                        value = RSAHelper.GetEncryptedAESKeyAsBase64(APIUnitTestHelperObject.ExampleRSAPublicKey, Convert.ToBase64String(aes2.Key));
                        callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.AESKey, value, "The parameter is incorrect.");
                    }

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.Body, "", "Object reference not set to an instance of an object.");

                    Thread.Sleep(3000);

                    callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.Body, " ", "Object reference not set to an instance of an object.");

                    Thread.Sleep(3000);
                    callManually_WithDefaults_Equal_Title(ref apiHelperObject, ParameterType.Body, null, "Unsupported Media Type");
                }
            }
        }

        private void callManually_Print(ref APIUnitTestHelperObject apiHelperObject, string body, string token, string aesIV, string aesKey)
        {
            apiHelperObject.RunAPICall_Manually(HttpMethod.Post, body, token, aesIV, aesKey);

            output.WriteLine("");

            if (apiHelperObject.ResponseStatus == 200)
            {
                output.WriteLine($"callManually(ref apiHelperObject, \"{body}\", \"{token}\", \"{aesIV}\", \"{aesKey}\");");
                output.WriteLine("Call Successful");
            }
            else
            {
                string description = apiHelperObject.ResponseErrorResult["description"];

                string[] splitDescription = description.Replace("\r\n", "\n").Split("\n");

                if (splitDescription.Length == 1)
                {
                    output.WriteLine($"callManually_Equal(ref apiHelperObject, \"{body}\", \"{token}\", \"{aesIV}\", \"{aesKey}\", \"{description}\");");
                }
                else
                {
                    output.WriteLine("expectedContainsValuesList = new();");

                    foreach (string splitDescriptionItem in splitDescription)
                    {
                        if (!string.IsNullOrWhiteSpace(splitDescriptionItem))
                        {
                            output.WriteLine($"expectedContainsValuesList.Add(\"{splitDescriptionItem}\");");
                        }
                    }

                    output.WriteLine("");
                    output.WriteLine($"callManually_Contains(ref apiHelperObject, \"{body}\", \"{token}\", \"{aesIV}\", \"{aesKey}\", expectedContainsValuesList);");
                }

                output.WriteLine("");
                output.WriteLine("Thread.Sleep(3000);");
            }
        }

        private void callManually_Equal(ref APIUnitTestHelperObject apiHelperObject, string body, string token, string aesIV, string aesKey, string expectedValue)
        {
            apiHelperObject.RunAPICall_Manually(HttpMethod.Post, body, token, aesIV, aesKey);

            Assert.Equal(expectedValue, apiHelperObject.ResponseErrorResult["description"]);
        }

        private void callManually_Contains(ref APIUnitTestHelperObject apiHelperObject, string body, string token, string aesIV, string aesKey, List<string> expectedContainsValues)
        {
            apiHelperObject.RunAPICall_Manually(HttpMethod.Post, body, token, aesIV, aesKey);

            foreach (string expectedContainsValue in expectedContainsValues)
            {
                Assert.Contains(expectedContainsValue, apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        private void callManually_WithDefaults_Print(ref APIUnitTestHelperObject apiHelperObject, ParameterType parameterType, string value)
        {
            callManually_WithDefaults(ref apiHelperObject, parameterType, value);


            output.WriteLine("");

            if (apiHelperObject.ResponseStatus == 200)
            {
                output.WriteLine($"callManually(ref apiHelperObject, ParameterType.{parameterType}, \"{value}\");");
                output.WriteLine("Call Successful");
            }
            else
            {
                string description = apiHelperObject.ResponseErrorResult["description"];

                string[] splitDescription = description.Replace("\r\n", "\n").Split("\n");

                if (splitDescription.Length == 1)
                {
                    output.WriteLine($"callManually_WithDefaults_Equal(ref apiHelperObject, ParameterType.{parameterType}, \"{value}\", \"{description}\");");
                }
                else
                {
                    output.WriteLine("expectedContainsValuesList = new();");

                    foreach (string splitDescriptionItem in splitDescription)
                    {
                        if (!string.IsNullOrWhiteSpace(splitDescriptionItem))
                        {
                            output.WriteLine($"expectedContainsValuesList.Add(\"{splitDescriptionItem}\");");
                        }
                    }

                    output.WriteLine("");
                    output.WriteLine($"callManually_WithDefaults_Contains(ref apiHelperObject, ParameterType.{parameterType}, \"{value}\", expectedContainsValuesList);");
                }

                output.WriteLine("");
                output.WriteLine("Thread.Sleep(3000);");
            }
        }
        private void callManually_WithDefaults(ref APIUnitTestHelperObject apiHelperObject, ParameterType parameterType, string value)
        {
            string body = Convert.ToBase64String(AESHelper.EncryptStringToBytes(apiHelperObject.APIBody, apiHelperObject.AESKey, apiHelperObject.AESIV));
            string token = apiHelperObject.Token;
            string aesIV = Convert.ToBase64String(apiHelperObject.AESIV);
            string aesKey = apiHelperObject.GetEncryptedAESKey();

            switch (parameterType)
            {
                case ParameterType.AESIV:
                    aesIV = value;
                    break;
                case ParameterType.AESKey:
                    aesKey = value;
                    break;
                case ParameterType.Body:
                    body = value;
                    break;
                case ParameterType.Token:
                    token = value;
                    break;
            }

            apiHelperObject.RunAPICall_Manually(HttpMethod.Post, body, token, aesIV, aesKey);
        }

        private void callManually_WithDefaults_Equal(ref APIUnitTestHelperObject apiHelperObject, ParameterType parameterType, string value, string expectedValue)
        {
            callManually_WithDefaults(ref apiHelperObject, parameterType, value);

            Assert.Equal(expectedValue, apiHelperObject.ResponseErrorResult["description"]);
        }

        private void callManually_WithDefaults_Equal_Title(ref APIUnitTestHelperObject apiHelperObject, ParameterType parameterType, string value, string expectedValue)
        {
            callManually_WithDefaults(ref apiHelperObject, parameterType, value);

            Assert.Equal(expectedValue, apiHelperObject.ResponseErrorResult["title"]);
        }

        private void callManually_WithDefaults_Contains(ref APIUnitTestHelperObject apiHelperObject, ParameterType parameterType, string value, List<string> expectedContainsValues)
        {
            callManually_WithDefaults(ref apiHelperObject, parameterType, value);

            foreach (string expectedContainsValue in expectedContainsValues)
            {
                Assert.Contains(expectedContainsValue, apiHelperObject.ResponseErrorResult["description"]);
            }
        }
    }
}
