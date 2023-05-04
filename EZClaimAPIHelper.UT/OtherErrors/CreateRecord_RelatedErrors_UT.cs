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
    public partial class OtherErrors_UT
    {
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void CreateRecord_RelatedErrors()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "No Objects Could Be Inserted - The following errors were encountered: Insert Columns Not Found - at least required column should be passed to insert the record.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                    ""PatLastName"":""APIPatientLastName""
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "No Objects Could Be Inserted - The following errors were encountered: Column Cannot Be Set To Null - PatFirstName column cannot be set to null.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                    ""PatFirstName"": ""ReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyLongName"",
                    ""PatLastName"":""APIPatientLastName""
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "No Objects Could Be Inserted - The following errors were encountered: Column (PatFirstName) Cannot Be Set To Value With Length Larger Than - 50.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                    ""PatID"": ""null"",
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "No Objects Could Be Inserted - The following errors were encountered: Insert Columns Not Found - at least required column should be passed to insert the record.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                    ""PatID"": ""0"",
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "No Objects Could Be Inserted - The following errors were encountered: Insert Columns Not Found - at least required column should be passed to insert the record.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                    ""PatID"": ""1"",
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "No Objects Could Be Inserted - The following errors were encountered: Insert Columns Not Found - at least required column should be passed to insert the record.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                    ""PatID"": ""abc"",
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "No Objects Could Be Inserted - The following errors were encountered: Insert Columns Not Found - at least required column should be passed to insert the record.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                    ""asdfasdf"": ""1"",
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "No Objects Could Be Inserted - The following errors were encountered: Insert Columns Not Found - at least required column should be passed to insert the record.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                    ""PatFirstName"": ""abc"",
                    ""PatLastName"":""APIPatientLastName"",
                    ""PatBirthDate"": ""abc""
                }}";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to DateTime: abc. Path 'PatBirthDate', line 4, position 41.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Post, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                    ""PatFirstName"": ""abc"",
                    ""PatLastName"":""APIPatientLastName"",
                    ""PatCoPayAmount"": ""abc""
                }}";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to decimal: abc. Path 'PatCoPayAmount', line 4, position 43.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Post, expectedContainsValuesList);
            }
        }
    }
}
