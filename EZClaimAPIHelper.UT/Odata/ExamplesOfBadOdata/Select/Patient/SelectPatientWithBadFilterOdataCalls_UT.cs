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
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void SelectPatientWithBadFilterOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$filterS=PatLastName ne 'APIPatientLastName'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$filterS=PatLastName ne 'APIPatientLastName''. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "filter=PatLastName ne 'APIPatientLastName'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : 'filter=PatLastName ne 'APIPatientLastName''. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName 'APIPatientLastName'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter:PatLastName eq 'APIPatientLastName'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$filter:PatLastName eq 'APIPatientLastName''. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eqs 'APIPatientLastName'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : eqs");

                Thread.Sleep(3000);

                queryValue = "$filter=Patasdf eq 'APIPatientLastName'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : Patasdf");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq APIPatientLastName";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in APIPatientLastName";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in 'APIPatientLastName'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : APIPatientLastName");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in ('APIPatientLastName',)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : APIPatientLastName,");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' junk PatLastName eq 'APIPatientLastName'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' s";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in (APIPatientLastName)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in ('APIPatientLastName','test)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' junk";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' And";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' And PatFirstName";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' or and";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=junk";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname ne'this'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname ne('this')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname in('this')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname in ('this', 'that',)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : this, that,");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname in (,'this', 'that')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in  (1, 2)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid  in (1, 2)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter =patid in (1, 2)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$filter =patid in (1, 2)'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$ filter=patid in (1, 2)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$ filter=patid in (1, 2)'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq a";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq ('a')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq (a)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq (1)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 2,3";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in (a,b)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1','b)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 'a' AND patid eq 'a'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Failed to convert parameter value from a String to a Int32.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1', 'b') AND patid in ('1', 'b')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in (1, 'b') AND patid in (1, 'b')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1', 'b'on') AND patid in ('1', 'b'on')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b'on' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in (1, 'b'on') AND patid in (1, 'b'on')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b'on' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1','b') AND patid in ('1','b')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid   in   ('1,  'b')  AND      patid  in ( '1,    'b' )";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1,  'b) AND patid in ('1,    'b')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1, 'b) AND patid in ('1, 'b')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1,'b) AND patid in ('1,'b')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$filter='. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1','b')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 AND";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 OR";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 test";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 AND'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 OR'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 test'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 test patid eq 1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");
            }
        }
    }
}
