﻿using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public partial class OtherErrors_UT
    {
        private readonly ITestOutputHelper output;

        private List<string> expectedContainsValuesList = new();
        private string queryValue;

        public OtherErrors_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Method used by all SelectPatientWithBadFilterOdata methods.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void runOtherErrorCall(ref APIUnitTestHelperObject apiHelperObject, HttpMethod httpMethod, bool skipAssert = false, bool setResponseValues = true)
        {
            apiHelperObject.RunAPICall(httpMethod, setResponseValues);

            if (!skipAssert)
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);
            }
        }

        private void runOtherErrorCall_ExpectedOutcomeEquals(ref APIUnitTestHelperObject apiHelperObject, HttpMethod httpMethod, string expectedValue, bool setResponseValues = true)
        {
            runOtherErrorCall(ref apiHelperObject, httpMethod, false, setResponseValues);

            Assert.Equal(expectedValue, GetDescription(apiHelperObject));
        }

        private void runOtherErrorCall_ExpectedOutcomeContains(ref APIUnitTestHelperObject apiHelperObject, HttpMethod httpMethod, List<string> expectedContainsValues, bool setResponseValues = true)
        {
            runOtherErrorCall(ref apiHelperObject, httpMethod, false, setResponseValues);

            foreach (string expectedContainsValue in expectedContainsValues)
            {
                Assert.Contains(expectedContainsValue, GetDescription(apiHelperObject));
            }
        }

        private void runOtherErrorCall_SuccessWithMessageEquals(ref APIUnitTestHelperObject apiHelperObject, HttpMethod httpMethod, string expectedValue, bool setResponseValues = true)
        {
            runOtherErrorCall(ref apiHelperObject, httpMethod, true, setResponseValues);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(expectedValue, GetMessage(apiHelperObject));
        }

        private void runOtherErrorCall_SuccessWithMessageContains(ref APIUnitTestHelperObject apiHelperObject, HttpMethod httpMethod, List<string> expectedContainsValues, bool setResponseValues = true)
        {
            runOtherErrorCall(ref apiHelperObject, httpMethod, true, setResponseValues);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            foreach (string expectedContainsValue in expectedContainsValues)
            {
                Assert.Contains(expectedContainsValue, GetMessage(apiHelperObject));
            }
        }

        private void runOtherErrorCall_PrintOutcome(ref APIUnitTestHelperObject apiHelperObject, HttpMethod httpMethod, bool showDetails = true, bool setResponseValues = true)
        {
            runOtherErrorCall(ref apiHelperObject, httpMethod, true, setResponseValues);

            string tokenType = "";

            if (apiHelperObject.Token.Equals(APIUnitTestHelperObject.TestToken2_NotYetRegisteredToken))
            {
                tokenType = "TestToken2_NotYetRegisteredToken";
            }
            else if (apiHelperObject.Token.Equals(APIUnitTestHelperObject.TestToken_CreatePatient))
            {
                tokenType = "TestToken_CreatePatient";
            }
            else if (apiHelperObject.Token.Equals(APIUnitTestHelperObject.TestToken_DeletePatient))
            {
                tokenType = "TestToken_DeletePatient";
            }
            else if (apiHelperObject.Token.Equals(APIUnitTestHelperObject.TestToken_SelectPatient))
            {
                tokenType = "TestToken_SelectPatient";
            }
            else if (apiHelperObject.Token.Equals(APIUnitTestHelperObject.TestToken_UpdatePatient))
            {
                tokenType = "TestToken_UpdatePatient";
            }
            else if (apiHelperObject.Token.Equals(APIUnitTestHelperObject.s01Token))
            {
                tokenType = "s01Token";
            }

            if (showDetails)
            {
                output.WriteLine($"apiHelperObject.Endpoint = \"{apiHelperObject.Endpoint}\";");
                output.WriteLine($"apiHelperObject.APIBody = $@\"{apiHelperObject.APIBody.Replace("\"", "\"\"").Replace("{", "{{").Replace("}", "}}")}\";");

                if (!string.IsNullOrWhiteSpace(tokenType))
                {
                    output.WriteLine($"apiHelperObject.Token = APIUnitTestHelperObject.{tokenType};");
                }
                else
                {
                    output.WriteLine($"apiHelperObject.Token = {apiHelperObject.Token}");
                }

                output.WriteLine("");
            }

            string method = "";

            switch (httpMethod.ToString())
            {
                case "POST":
                    method = "Post";
                    break;
                case "PUT":
                    method = "Put";
                    break;
                case "DELETE":
                    method = "Delete";
                    break;
            }

            if (apiHelperObject.ResponseStatus == 200)
            {
                string message = GetMessage(apiHelperObject);

                if (!string.IsNullOrWhiteSpace(message))
                {
                    output.WriteLine("//Call will be partially successful.");

                    if (showDetails)
                    {
                        string[] splitDescription = message.Replace("\r\n", "\n").Split("\n");

                        if (splitDescription.Length == 1)
                        {
                            output.WriteLine($"runOtherErrorCall_SuccessWithMessageEquals(ref apiHelperObject, HttpMethod.{method}, \"{message}\");");
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
                            output.WriteLine($"runOtherErrorCall_SuccessWithMessageContains(ref apiHelperObject, HttpMethod.{method}, expectedContainsValuesList);");

                            output.WriteLine("");
                            output.WriteLine("Thread.Sleep(3000);");
                        }
                    }
                    else
                    {
                        output.WriteLine($"{message}");
                    }
                }
                else
                {
                    output.WriteLine("Call Successful");
                }
            }
            else
            {
                string description = GetDescription(apiHelperObject);

                string[] splitDescription = description.Replace("\r\n", "\n").Split("\n");

                if (showDetails)
                {
                    if (splitDescription.Length == 1)
                    {
                        output.WriteLine($"runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.{method}, \"{description}\");");
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
                        output.WriteLine($"runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.{method}, expectedContainsValuesList);");
                    }

                    output.WriteLine("");
                    output.WriteLine("Thread.Sleep(3000);");
                }
                else
                {
                    output.WriteLine($"{description}");
                }
            }

            output.WriteLine("");
        }

        private string GetDescription(APIUnitTestHelperObject apiHelperObject)
        {
            string description = "";

            if (apiHelperObject.ResponseErrorResult != null)
            {
                description = apiHelperObject.ResponseErrorResult["description"];
            }
            else
            {
                description = apiHelperObject.Response.ReasonPhrase;

                if (description.Equals("Too Many Requests"))
                {
                    description = apiHelperObject.ResponseContent;
                }
            }

            return description;
        }

        private string GetMessage(APIUnitTestHelperObject apiHelperObject)
        {
            string message = "";

            if (apiHelperObject.ResponseMessage != null)
            {
                message = apiHelperObject.ResponseMessage;
            }

            return message;
        }


    }
}
