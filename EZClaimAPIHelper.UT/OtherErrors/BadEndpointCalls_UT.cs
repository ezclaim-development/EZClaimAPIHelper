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
        public void BadEndpointCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Method Not Allowed");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "Method Not Allowed");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/NotRealTable/GetList";
                apiHelperObject.APIBody = $@"{{}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "Not Found");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/NotRealTable/GetList";
                apiHelperObject.APIBody = $@"{{}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Not Found");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/NotRealTable/GetList";
                apiHelperObject.APIBody = $@"{{}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "Not Found");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "No Objects Could Be Inserted - The following errors were encountered: Insert Columns Not Found - at least required column should be passed to insert the record.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "Method Not Allowed");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "Object reference not set to an instance of an object.");
            }
        }
    }
}
