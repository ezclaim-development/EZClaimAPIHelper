using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public partial class UpdatePatientWithGoodOdataCalls_UT
    {
        private readonly ITestOutputHelper output;

        private List<string> expectedContainsValuesList = new();
        private string queryValue;
        private Dictionary<string, string> patientsObject = new();

        public UpdatePatientWithGoodOdataCalls_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Method used by all UpdatePatientWithBadFilterOdata methods.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updatePatientWithGoodOdata(ref APIUnitTestHelperObject apiHelperObject, string query, Dictionary<string, string> keyValuePairs, bool skipAssert = false)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients";

            string patientsObjectValue = "";

            foreach (KeyValuePair<string, string> keyValuePair in keyValuePairs)
            {
                if (!string.IsNullOrWhiteSpace(patientsObjectValue))
                {
                    patientsObjectValue += $",\n";
                }

                patientsObjectValue += $"    \"{keyValuePair.Key}\" : \"{keyValuePair.Value}\"";
            }

            apiHelperObject.APIBody = @$"{{
  ""patientsObject"": {{
{patientsObjectValue}
  }},
  ""queryString"": {{
    ""query"": ""{query}""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            if (!skipAssert)
            {
                if (apiHelperObject.ResponseStatus.Equals(200))
                {
                    Assert.Equal(200, apiHelperObject.ResponseStatus);
                }
                else if (apiHelperObject.ResponseErrorResult["description"].Equals("No data found."))
                {
                    //Redundant check
                    Assert.Equal("No data found.", apiHelperObject.ResponseErrorResult["description"]);
                }
                else
                {
                    Assert.Contains("rows which is more than the allotted 50 rows permitted. Update the filter query to be more specific.", apiHelperObject.ResponseErrorResult["description"]);
                }
            }
        }

        private void updatePatientWithGoodOdata_PrintOutcome(ref APIUnitTestHelperObject apiHelperObject, string query, Dictionary<string, string> keyValuePairs)
        {
            updatePatientWithGoodOdata(ref apiHelperObject, query, keyValuePairs, true);

            output.WriteLine($"queryValue = @\"{query.Replace(@"\""", @"\""""")}\";");
            output.WriteLine("");
            output.WriteLine($"patientsObject = new();");

            foreach (KeyValuePair<string, string> keyValuePair in keyValuePairs)
            {
                output.WriteLine($"patientsObject.Add(\"{keyValuePair.Key}\", \"{keyValuePair.Value}\");");
            }

            if (apiHelperObject.ResponseStatus != 200 && !apiHelperObject.ResponseErrorResult["description"].Contains("rows which is more than the allotted 50 rows permitted. Update the filter query to be more specific.") && !apiHelperObject.ResponseErrorResult["description"].Equals("No data found."))
            {
                output.WriteLine("Call Failed");
                output.WriteLine(apiHelperObject.ResponseErrorResult["description"]);
            }
            else
            {
                output.WriteLine("updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);");
                output.WriteLine("");
                output.WriteLine("Thread.Sleep(3000);");
                output.WriteLine("");
            }

            output.WriteLine("");
        }
    }
}
