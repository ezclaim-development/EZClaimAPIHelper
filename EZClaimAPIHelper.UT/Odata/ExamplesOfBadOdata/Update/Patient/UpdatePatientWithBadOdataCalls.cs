using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public partial class UpdatePatientWithBadOdataCalls_UT
    {
        private readonly ITestOutputHelper output;

        private List<string> expectedContainsValuesList = new();
        private string queryValue;
        private Dictionary<string, string> patientsObject = new();

        public UpdatePatientWithBadOdataCalls_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Method used by all UpdatePatientWithBadFilterOdata methods.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updatePatientWithBadOdata(ref APIUnitTestHelperObject apiHelperObject, string query, Dictionary<string, string> keyValuePairs, bool skipAssert = false)
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
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);
            }
        }

        private void updatePatientWithBadOdata_ExpectedOutcomeEquals(ref APIUnitTestHelperObject apiHelperObject, string query, Dictionary<string, string> keyValuePairs, string expectedValue)
        {
            updatePatientWithBadOdata(ref apiHelperObject, query, keyValuePairs);

            Assert.Equal(expectedValue, apiHelperObject.ResponseErrorResult["description"]);
        }

        private void updatePatientWithBadOdata_ExpectedOutcomeContains(ref APIUnitTestHelperObject apiHelperObject, string query, Dictionary<string, string> keyValuePairs, List<string> expectedContainsValues)
        {
            updatePatientWithBadOdata(ref apiHelperObject, query, keyValuePairs);

            foreach (string expectedContainsValue in expectedContainsValues)
            {
                Assert.Contains(expectedContainsValue, apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        private void updatePatientWithBadOdata_PrintOutcome(ref APIUnitTestHelperObject apiHelperObject, string query, Dictionary<string, string> keyValuePairs)
        {
            updatePatientWithBadOdata(ref apiHelperObject, query, keyValuePairs, true);

            output.WriteLine($"queryValue = \"{query}\";");
            output.WriteLine("");
            output.WriteLine($"patientsObject = new();");

            foreach (KeyValuePair<string, string> keyValuePair in keyValuePairs)
            {
                output.WriteLine($"patientsObject.Add(\"{keyValuePair.Key}\", \"{keyValuePair.Value}\");");
            }

            if (apiHelperObject.ResponseStatus == 200)
            {
                output.WriteLine("Call Successful");
            }
            else
            {
                string description = apiHelperObject.ResponseErrorResult["description"];

                string[] splitDescription = description.Replace("\r\n", "\n").Split("\n");

                if (splitDescription.Length == 1)
                {
                    output.WriteLine($"updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, \"{description}\");");
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
                    output.WriteLine("updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);");
                }

                output.WriteLine("");
                output.WriteLine("Thread.Sleep(3000);");
            }

            output.WriteLine("");
        }
    }
}
