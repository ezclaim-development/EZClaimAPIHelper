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
        public void CreateRecordSuccessful_WithErrorMessage()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                    {{
                    ""PatFirstName"": ""APIPatientFirstName1"",
                    ""PatLastName"":""APIPatientLastName""
                    }},
                    {{
                    ""asjldfkasdjlf"": ""APIPatientFirstName2"",
                    ""PatLastName"":""APIPatientLastName""
                    }}
                ]";
                apiHelperObject.Token = APIUnitTestHelperObject.s01Token;

                //Call will be partially successful.
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors were encountered:");
                expectedContainsValuesList.Add("Column Cannot Be Set To Null - PatFirstName column cannot be set to null.");

                runOtherErrorCall_SuccessWithMessageContains(ref apiHelperObject, HttpMethod.Post, expectedContainsValuesList);
            }
        }
    }
}
