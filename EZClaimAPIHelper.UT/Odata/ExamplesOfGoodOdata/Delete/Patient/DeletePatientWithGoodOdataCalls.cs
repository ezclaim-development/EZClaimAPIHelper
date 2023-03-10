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
    public partial class DeletePatientWithGoodOdataCalls_UT
    {
        private readonly ITestOutputHelper output;

        private List<string> expectedContainsValuesList = new();
        private string queryValue;

        public DeletePatientWithGoodOdataCalls_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Method used by all DeletePatientWithGoodFilterOdata methods.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePatientWithGoodOdata(ref APIUnitTestHelperObject apiHelperObject, string query, bool skipAssert = false)
        {
            apiHelperObject.APIBody = $@"{{
                    ""Query"": ""{query}""
                }}";

            apiHelperObject.Endpoint = "/api/v2/Patients/query";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            if (!skipAssert)
            {
                if (apiHelperObject.ResponseStatus.Equals(200))
                {
                    Assert.Equal(200, apiHelperObject.ResponseStatus);
                }
                else if (apiHelperObject.ResponseErrorResult["description"].Equals("Delete unsuccessful : No record(s) found for the provided condition."))
                {
                    //Redundant check
                    Assert.Equal("Delete unsuccessful : No record(s) found for the provided condition.", apiHelperObject.ResponseErrorResult["description"]);
                }
                else
                {
                    Assert.Contains("rows which is more than the allotted 50 rows permitted. Update the filter query to be more specific or supply less id's.", apiHelperObject.ResponseErrorResult["description"]);
                }
            }
        }

        private void deletePatientWithGoodOdata_PrintOutcome(ref APIUnitTestHelperObject apiHelperObject, string query)
        {
            deletePatientWithGoodOdata(ref apiHelperObject, query, true);

            output.WriteLine($"queryValue = \"{query}\";");
            output.WriteLine("");

            if (apiHelperObject.ResponseStatus != 200 && !apiHelperObject.ResponseErrorResult["description"].Contains("rows which is more than the allotted 50 rows permitted. Update the filter query to be more specific or supply less id's.") && !apiHelperObject.ResponseErrorResult["description"].Equals("Delete unsuccessful : No record(s) found for the provided condition."))
            {
                output.WriteLine("Call Failed");
                output.WriteLine(apiHelperObject.ResponseErrorResult["description"]);
            }
            else
            {
                output.WriteLine("deletePatientWithGoodOdata(ref apiHelperObject, queryValue);");
                output.WriteLine("");
                output.WriteLine("Thread.Sleep(3000);");
                output.WriteLine("");
            }
        }
    }
}
