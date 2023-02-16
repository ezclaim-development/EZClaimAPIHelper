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
        [Fact]
        public void SelectPatientWithBadFilterOdataCalls()
        {
            //This does a full gambit test on the Patient endpoints.
            return;

            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, ExampleRSAPublicKey, s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.Client002844_ProviderPortalTestToken, "https://ezclaimapidev.azurewebsites.net");

                Thread.Sleep(3000);
                // Attempt to select a patient record using filters instead of filter
                queryValue = "$filterS=PatLastName ne 'APIPatientLastName'";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("Invalid input parameter : ");
                expectedContainsValuesList.Add("Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");
                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);
                
                Thread.Sleep(3000);

                // Attempt to select a patient record using filter without the required dollar symbol
                queryValue = "filter=PatLastName ne 'APIPatientLastName'";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("Invalid input parameter : ");
                expectedContainsValuesList.Add("Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");
                
                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);
                
                Thread.Sleep(3000);

                // Attempt to select a patient record without a comparison value
                queryValue = "$filter=PatLastName 'APIPatientLastName'";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                // Attempt to select a patient record without an = symbol
                queryValue = "$filter:PatLastName eq 'APIPatientLastName'"; 
                
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("Invalid input parameter : ");
                expectedContainsValuesList.Add("Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");
                
                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);
                
                Thread.Sleep(3000);

                // Attempt to select a patient record with incorrect comparison type. Valid types are eq, ne, in
                queryValue = "$filter=PatLastName eqs 'APIPatientLastName'";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : eqs");

                Thread.Sleep(3000);

                // Attempt to select a patient record with incorrect format
                queryValue = "$filter=PatLastName eq 'APIPatientLastName' s";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Query parameters yielded no results.");

                Thread.Sleep(3000);

                // Attempt to select a patient record with unknown field
                queryValue = "$filter=Patasdf eq 'APIPatientLastName'";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : Patasdf");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad eq format
                queryValue = "$filter=PatLastName eq APIPatientLastName";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad in format
                queryValue = "$filter=PatLastName in APIPatientLastName";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad in format
                queryValue = "$filter=PatLastName in (APIPatientLastName)";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Query parameters yielded no results.");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad in format
                queryValue = "$filter=PatLastName in 'APIPatientLastName'";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : APIPatientLastName");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad in format
                queryValue = "$filter=PatLastName in ('APIPatientLastName',)";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : APIPatientLastName,");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad in format
                queryValue = "$filter=PatLastName in ('APIPatientLastName','test)";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Query parameters yielded no results.");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad conjunction
                queryValue = "$filter=PatLastName eq 'APIPatientLastName' junk";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Query parameters yielded no results.");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad conjunction
                queryValue = "$filter=PatLastName eq 'APIPatientLastName' junk PatLastName eq 'APIPatientLastName'";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("Incorrect syntax near");
                expectedContainsValuesList.Add("Invalid usage of the option NEXT in the FETCH statement.");
                
                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);
                
                Thread.Sleep(3000);

                // Attempt to select a patient record with bad conjunction format
                queryValue = "$filter=PatLastName eq 'APIPatientLastName' And";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad conjunction format
                queryValue = "$filter=PatLastName eq 'APIPatientLastName' And PatFirstName";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad conjunction format
                queryValue = "$filter=PatLastName eq 'APIPatientLastName' or and";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                // Attempt to select a patient record with bad filter format
                queryValue = "$filter=junk";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");
            }
        }
    }
}
