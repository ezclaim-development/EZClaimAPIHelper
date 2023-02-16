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
        private void selectPatientWithBadOdata(ref APIUnitTestHelperObject apiHelperObject, string query)
        {
            apiHelperObject.APIBody = $@"{{
                    ""Query"": ""{query}""
                }}";

            apiHelperObject.Endpoint = "/api/v2/Patients/GetList";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.NotEqual(200, apiHelperObject.ResponseStatus);
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
            selectPatientWithBadOdata(ref apiHelperObject, query);

            output.WriteLine(query);
            output.WriteLine(apiHelperObject.ResponseErrorResult["description"]);
            output.WriteLine("\n\n");
        }
    }
}
