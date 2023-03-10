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
    public partial class UpdatePatientWithBadOdataCalls_UT
    {
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void UpdatePatientWithBadFilterOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "";

                patientsObject = new();
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "No Object To Update - No object passed in to be updated.");

                Thread.Sleep(3000);

                queryValue = "filter=1";

                patientsObject = new();
                patientsObject.Add("patfirstname1", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "No Object To Update - No object passed in to be updated.");

                Thread.Sleep(3000);

                queryValue = "filter=1";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : (filter=1). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$filterS=PatLastName ne 'APIPatientLastName'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : ($filterS=PatLastName ne 'APIPatientLastName'). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "filter=PatLastName ne 'APIPatientLastName'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : (filter=PatLastName ne 'APIPatientLastName'). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName 'APIPatientLastName'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter:PatLastName eq 'APIPatientLastName'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : ($filter:PatLastName eq 'APIPatientLastName'). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eqs 'APIPatientLastName'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters : eqs");

                Thread.Sleep(3000);

                queryValue = "$filter=Patasdf eq 'APIPatientLastName'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters : Patasdf");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq APIPatientLastName";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in APIPatientLastName";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in 'APIPatientLastName'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters : APIPatientLastName");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in ('APIPatientLastName',)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters : APIPatientLastName,");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' junk PatLastName eq 'APIPatientLastName'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' s";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in (APIPatientLastName)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName in ('APIPatientLastName','test)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' junk";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' And";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' And PatFirstName";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=PatLastName eq 'APIPatientLastName' or and";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=junk";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname ne'this'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname ne('this')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname in('this')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname in ('this', 'that',)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters : this, that,");

                Thread.Sleep(3000);

                queryValue = "$filter=Patfirstname in (,'this', 'that')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in  (1, 2)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid  in (1, 2)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter =patid in (1, 2)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : ($filter =patid in (1, 2)). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$ filter=patid in (1, 2)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : ($ filter=patid in (1, 2)). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq a";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq ('a')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq (a)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq (1)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 2,3";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in (a,b)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1','b)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 'a' AND patid eq 'a'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Failed to convert parameter value from a String to a Int32.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1', 'b') AND patid in ('1', 'b')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in (1, 'b') AND patid in (1, 'b')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1', 'b'on') AND patid in ('1', 'b'on')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Conversion failed when converting the nvarchar value 'b'on' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in (1, 'b'on') AND patid in (1, 'b'on')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Conversion failed when converting the nvarchar value 'b'on' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1','b') AND patid in ('1','b')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid   in   ('1,  'b')  AND      patid  in ( '1,    'b' )";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1,  'b) AND patid in ('1,    'b')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters : (1,");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1, 'b) AND patid in ('1, 'b')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters : (1,");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1,'b) AND patid in ('1,'b')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : ($filter=). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$filter=patid in ('1','b')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 AND";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 OR";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 test";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 AND'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 OR'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 test'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1 test patid eq 1";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");
            }
        }
    }
}
