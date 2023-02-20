using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public partial class SelectPatientWithBadOdataCalls_UT
    {
        private readonly ITestOutputHelper output;

        private List<string> expectedContainsValuesList = new();
        private string queryValue;

        public SelectPatientWithBadOdataCalls_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Method used by all SelectPatientWithBadFilterOdata methods.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPatientWithBadOdata(ref APIUnitTestHelperObject apiHelperObject, string query, bool skipAssert = false)
        {
            apiHelperObject.APIBody = $@"{{
                    ""Query"": ""{query}""
                }}";

            apiHelperObject.Endpoint = "/api/v2/Patients/GetList";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (!skipAssert)
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);
            }
        }

        private void selectPatientWithBadOdata_ExpectedOutcomeEquals(ref APIUnitTestHelperObject apiHelperObject, string query, string expectedValue)
        {
            selectPatientWithBadOdata(ref apiHelperObject, query);

            Assert.Equal(expectedValue, apiHelperObject.ResponseErrorResult["description"]);
        }

        private void selectPatientWithBadOdata_ExpectedOutcomeContains(ref APIUnitTestHelperObject apiHelperObject, string query, List<string> expectedContainsValues)
        {
            selectPatientWithBadOdata(ref apiHelperObject, query);

            foreach (string expectedContainsValue in expectedContainsValues)
            {
                Assert.Contains(expectedContainsValue, apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        private void selectPatientWithBadOdata_printOutcome(ref APIUnitTestHelperObject apiHelperObject, string query)
        {
            selectPatientWithBadOdata(ref apiHelperObject, query, true);

            output.WriteLine($"queryValue = \"{query}\";");
            output.WriteLine("");

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
                    output.WriteLine($"selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, \"{description}\");");
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
                    output.WriteLine("selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);");
                }

                output.WriteLine("");
                output.WriteLine("Thread.Sleep(3000);");
            }

            output.WriteLine("");
        }
    }
}
