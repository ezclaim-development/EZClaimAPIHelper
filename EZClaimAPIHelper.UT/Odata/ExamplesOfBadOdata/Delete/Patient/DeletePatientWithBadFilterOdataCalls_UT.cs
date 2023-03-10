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
    public partial class DeletePatientWithBadOdataCalls_UT
    {
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void DeletePatientWithBadFilterOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Delete conditions Not Found - at least id column or one condition must be passed to delete the record.");

                Thread.Sleep(3000);

                queryValue = "filter=1";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (filter=1). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "filter=1";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (filter=1). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$filterS=PatLastName ne 'APIPatientLastName'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : ($filterS=PatLastName ne 'APIPatientLastName'). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "filter=PatLastName ne 'APIPatientLastName'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (filter=PatLastName ne 'APIPatientLastName'). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName 'APIPatientLastName'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter:PatLastName eq 'APIPatientLastName'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : ($filter:PatLastName eq 'APIPatientLastName'). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eqs 'APIPatientLastName'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : eqs");

                Thread.Sleep(3000);

                queryValue = "$filter=Patasdf eq 'APIPatientLastName'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : Patasdf");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq APIPatientLastName";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in APIPatientLastName";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in 'APIPatientLastName'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : APIPatientLastName");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in ('APIPatientLastName',)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : APIPatientLastName,");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' junk PatLastName eq 'APIPatientLastName'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' s";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in (APIPatientLastName)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in ('APIPatientLastName','test)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' junk";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' And";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' And PatFirstName";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' or and";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=junk";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname ne'this'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname ne('this')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname in('this')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname in ('this', 'that',)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : this, that,");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname in (,'this', 'that')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in  (1, 2)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid  in (1, 2)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter =patid in (1, 2)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : ($filter =patid in (1, 2)). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$ filter=patid in (1, 2)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : ($ filter=patid in (1, 2)). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq a";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq ('a')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq (a)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq (1)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 2,3";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in (a,b)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1','b)";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 'a' AND patid eq 'a'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Failed to convert parameter value from a String to a Int32.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1', 'b') AND patid in ('1', 'b')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in (1, 'b') AND patid in (1, 'b')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1', 'b'on') AND patid in ('1', 'b'on')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b'on' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in (1, 'b'on') AND patid in (1, 'b'on')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b'on' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1','b') AND patid in ('1','b')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid   in   ('1,  'b')  AND      patid  in ( '1,    'b' )";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1,  'b) AND patid in ('1,    'b')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : (1,");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1, 'b) AND patid in ('1, 'b')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters : (1,");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1,'b) AND patid in ('1,'b')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : ($filter=). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1','b')";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 AND";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 OR";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 test";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 AND'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 OR'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 test'";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 test patid eq 1";

                deletePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");
            }
        }
    }
}
