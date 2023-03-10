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
        public void SelectPatientWithBadDuplicateOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$select=patid,patfirstname,patlastname;$select=patid,patfirstname,patlastname";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (select) was used multiple times. Each odata type can only be used once.");

                Thread.Sleep(3000);

                queryValue = "$filter=patid eq 1;$filter=patid eq 1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (filter) was used multiple times. Each odata type can only be used once.");

                Thread.Sleep(3000);

                queryValue = "$orderby=patid;$orderby=patid";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (orderby) was used multiple times. Each odata type can only be used once.");

                Thread.Sleep(3000);

                queryValue = "$ids=1;$ids=1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (ids) was used multiple times. Each odata type can only be used once.");

                Thread.Sleep(3000);

                queryValue = "$top=10;$top=10";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (top) was used multiple times. Each odata type can only be used once.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (join) was used multiple times. Each odata type can only be used once.");

                Thread.Sleep(3000);

                queryValue = "$select=patid,patfirstname,patlastname;$select=patid,patfirstname,patlastname;$filter=patid eq 1;$filter=patid eq 1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (select) was used multiple times. Each odata type can only be used once.");
            }
        }
    }
}
