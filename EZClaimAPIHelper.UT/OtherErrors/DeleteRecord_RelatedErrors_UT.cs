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
        public void DeleteRecord_RelatedErrors()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                apiHelperObject.Endpoint = "/api/v2/Patients/ids";
                apiHelperObject.APIBody = $@"[""0"", ""0"", ""0""]";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Delete unsuccessful : No record(s) found for the provided condition.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/ids";
                apiHelperObject.APIBody = $@"[]";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Delete conditions Not Found - at least id column or one condition must be passed to delete the record.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/ids";
                apiHelperObject.APIBody = $@"[""0""]";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Delete unsuccessful : No record(s) found for the provided condition.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/ids";
                apiHelperObject.APIBody = $@"[""""]";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Error converting value {null} to type 'System.Int32'. Path '[0]', line 1, position 3.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/ids";
                apiHelperObject.APIBody = $@"[""abc""]";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Could not convert string to integer: abc. Path '[0]', line 1, position 6.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/ids";
                apiHelperObject.APIBody = $@"[""0"", ""0"", ""0""]";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Delete unsuccessful : No record(s) found for the provided condition.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/ids";
                apiHelperObject.APIBody = $@"[null]";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Error converting value {null} to type 'System.Int32'. Path '[0]', line 1, position 5.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/query";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$filter=PatLastName ne \""0\""""
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "This would delete 88 rows which is more than the allotted 50 rows permitted. Update the filter query to be more specific or supply less id's.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/query";
                apiHelperObject.APIBody = $@"{{
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Delete conditions Not Found - at least id column or one condition must be passed to delete the record.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/query";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$filter=PatLastName eq \""0\""""
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Delete unsuccessful : No record(s) found for the provided condition.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/query";
                apiHelperObject.APIBody = $@"{{
                    ""junk"": ""$filter=PatLastName ne \""0\""""
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Delete conditions Not Found - at least id column or one condition must be passed to delete the record.");
            }
        }
    }
}
