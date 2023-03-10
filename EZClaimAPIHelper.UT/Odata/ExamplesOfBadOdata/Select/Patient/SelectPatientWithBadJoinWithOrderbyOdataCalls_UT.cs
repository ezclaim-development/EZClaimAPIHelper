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
        public void SelectPatientWithBadJoinWithOrderbyOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID&$Orderby=Patid";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (=Patid). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby=Patid1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid column in request : Patid1");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby=Patid asdf";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Condition in request : asdf");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby='patid' desc";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter, 'order by' should not contain single quote or double quote");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT asdf ClaPatFID EQ Patient PatID;$Orderby=Claid asdf";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied joining table (asdf) for statement 1 is not supported.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby=Claid, asdf";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid column in request : asdf");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby=asdf desc, Patid asc";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid column in request : asdf");
            }
        }
    }
}
