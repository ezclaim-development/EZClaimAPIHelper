using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public partial class DeletePatientWithBadOdataCalls_UT
    {
        private readonly ITestOutputHelper output;

        private List<string> expectedContainsValuesList = new();
        private string queryValue;

        public DeletePatientWithBadOdataCalls_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Method used by all DeletePatientWithBadFilterOdata methods.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePatientWithBadOdata(ref APIUnitTestHelperObject apiHelperObject, string query, bool skipAssert = false)
        {
            apiHelperObject.APIBody = $@"{{
                    ""Query"": ""{query}""
                }}";

            apiHelperObject.Endpoint = "/api/v2/Patients/query";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            if (!skipAssert)
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);
            }
        }

        private void deletePatientWithBadOdata_ExpectedOutcomeEquals(ref APIUnitTestHelperObject apiHelperObject, string query, string expectedValue)
        {
            deletePatientWithBadOdata(ref apiHelperObject, query);

            Assert.Equal(expectedValue, apiHelperObject.ResponseErrorResult["description"]);
        }

        private void deletePatientWithBadOdata_ExpectedOutcomeContains(ref APIUnitTestHelperObject apiHelperObject, string query, List<string> expectedContainsValues)
        {
            deletePatientWithBadOdata(ref apiHelperObject, query);

            foreach (string expectedContainsValue in expectedContainsValues)
            {
                Assert.Contains(expectedContainsValue, apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        private void deletePatientWithBadOdata_PrintOutcome(ref APIUnitTestHelperObject apiHelperObject, string query)
        {
            deletePatientWithBadOdata(ref apiHelperObject, query, true);

            output.WriteLine($"queryValue = @\"{query.Replace(@"\""", @"\""""")}\";");
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
                    output.WriteLine($"deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @\"{description.Replace(@"""", @"""""")}\");");
                }
                else
                {
                    output.WriteLine("expectedContainsValuesList = new();");

                    foreach (string splitDescriptionItem in splitDescription)
                    {
                        if (!string.IsNullOrWhiteSpace(splitDescriptionItem))
                        {
                            output.WriteLine($"expectedContainsValuesList.Add(@\"{splitDescriptionItem.Replace(@"""", @"""""")}\");");
                        }
                    }

                    output.WriteLine("");
                    output.WriteLine("deletePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);");
                }

                output.WriteLine("");
                output.WriteLine("Thread.Sleep(3000);");
            }

            output.WriteLine("");
        }
    }
}
