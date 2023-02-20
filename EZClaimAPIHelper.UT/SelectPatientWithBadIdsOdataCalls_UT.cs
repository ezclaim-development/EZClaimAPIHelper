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
        public void SelectPatientWithBadIdsOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, ExampleRSAPublicKey, s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$ids=!";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Id in request : !");

                Thread.Sleep(3000);

                queryValue = "$id=a";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$id=a'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "ids=1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : 'ids=1'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$ids:1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$ids:1'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$ids=1,a";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Id in request : a");

                Thread.Sleep(3000);

                queryValue = "$ids='1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Id in request : '1");

                Thread.Sleep(3000);

                queryValue = "$ids=1'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Id in request : 1'");

                Thread.Sleep(3000);

                queryValue = "$ids='1'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Id in request : '1'");

                Thread.Sleep(3000);

                queryValue = "$ids=1,'2'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Id in request : '2'");

                Thread.Sleep(3000);

                queryValue = "$ids='972ef7f6-7d85-4c1f-a9b2-4abe32f7322e'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Id in request : '972ef7f6-7d85-4c1f-a9b2-4abe32f7322e'");

                Thread.Sleep(3000);



                //The following currently do not fail, but will in the future

                //queryValue = "$ids=1,";

                //selectPatientWithBadOdata_printOutcome(ref apiHelperObject, queryValue);

                //Thread.Sleep(3000);

                //queryValue = "$ids=,1";

                //selectPatientWithBadOdata_printOutcome(ref apiHelperObject, queryValue);

            }
        }
    }
}
