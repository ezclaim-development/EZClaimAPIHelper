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
        public void UpdateRecordSuccessful_WithErrorMessage()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""patZip"": ""55551"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""patZip"": ""55552"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""patZip"": ""55553"",
                            ""patID"": ""3""
                        }}
                    ]";

                //Call will be partially successful.
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors were encountered:");
                expectedContainsValuesList.Add("Duplicate Record Detected - a duplicate record was detected for the object with id of 1. All records with this id have been skipped.");

                runOtherErrorCall_SuccessWithMessageContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""patFirstName"": ""ReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyLongName"",
    ""patZip"": ""55551""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
  }}
}}";

                //Call will be partially successful.
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors were encountered:");
                expectedContainsValuesList.Add("Column Too Long - PatFirstName cannot be set to a value greater than 50. This column will not be updated.");

                runOtherErrorCall_SuccessWithMessageContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""patFirstName"": ""ReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyLongName"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""patZip"": ""55551"",
                            ""patID"": ""3""
                        }}
                    ]";

                //Call will be partially successful.
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors were encountered:");
                expectedContainsValuesList.Add("Column Too Long - PatFirstName cannot be set to a value greater than 50. This column will not be updated.");

                runOtherErrorCall_SuccessWithMessageContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);
            }
        }
    }
}
