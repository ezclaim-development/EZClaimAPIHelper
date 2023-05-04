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
    public class UpdatingColumnToNull_UT
    {
        private readonly ITestOutputHelper output;

        public UpdatingColumnToNull_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Gives examples of attempting to create or update auto columns and shows that they will not update
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void UpdatingColumnToNull()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                apiHelperObject.Endpoint = "/api/v2/Patients";

                apiHelperObject.APIBody = @$"{{
  ""patientsObject"": {{
    ""patZip"": null,
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
  }}
}}";

                apiHelperObject.RunAPICall(HttpMethod.Put);

                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.True(apiHelperObject.ResponseData.Count > 0);
            }
        }
    }
}
