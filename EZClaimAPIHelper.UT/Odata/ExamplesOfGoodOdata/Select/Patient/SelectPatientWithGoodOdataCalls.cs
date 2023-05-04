using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public partial class SelectPatientWithGoodOdataCalls_UT
    {
        private readonly ITestOutputHelper output;

        private List<string> expectedContainsValuesList = new();
        private string queryValue;

        public SelectPatientWithGoodOdataCalls_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Method used by all SelectPatientWithGoodFilterOdata methods.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void SelectPatientWithGoodOdata(ref APIUnitTestHelperObject apiHelperObject, string query, bool skipAssert = false)
        {
            apiHelperObject.APIBody = $@"{{
                    ""Query"": ""{query}""
                }}";

            apiHelperObject.Endpoint = "/api/v2/Patients/GetList";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (!skipAssert)
            {
                if (apiHelperObject.ResponseStatus.Equals(200))
                {
                    Assert.Equal(200, apiHelperObject.ResponseStatus);
                }
                else
                {
                    Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
                }
            }
        }

        private void SelectPatientWithGoodOdata_PrintOutcome(ref APIUnitTestHelperObject apiHelperObject, string query)
        {
            SelectPatientWithGoodOdata(ref apiHelperObject, query, true);

            output.WriteLine($"queryValue = @\"{query.Replace(@"\""", @"\""""")}\";");
            output.WriteLine("");

            if (apiHelperObject.ResponseStatus != 200 && !apiHelperObject.ResponseErrorResult["description"].Equals("Query parameters yielded no results."))
            {
                output.WriteLine("Call Failed");
                output.WriteLine(apiHelperObject.ResponseErrorResult["description"]);
            }
            else
            {
                output.WriteLine("SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);");
                output.WriteLine("");
                output.WriteLine("Thread.Sleep(3000);");
                output.WriteLine("");
            }
        }
    }
}
