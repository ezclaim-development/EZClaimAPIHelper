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
        public void SelectPatientWithBadOrderbyOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "orderby=patid";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : 'orderby=patid'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$orderb=patid";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$orderb=patid'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$orderby:patid";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$orderby:patid'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$orderby=patid1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid column in request : patid1");

                Thread.Sleep(3000);

                queryValue = "$orderby=patid des";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Condition in request : des");

                Thread.Sleep(3000);

                queryValue = "$orderby=patid kasjldfkas";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Condition in request : kasjldfkas");

                Thread.Sleep(3000);

                queryValue = "$orderby=patid desc, patfirstname asdfasd";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Condition in request : asdfasd");

                Thread.Sleep(3000);

                queryValue = "$orderby=patid desc, asdfasd";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid column in request : asdfasd");

                Thread.Sleep(3000);

                queryValue = "$orderby=patid patFirstName";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Condition in request : patFirstName");

                //The following currently work, but should fail
                /*
                 * $orderby=patid desc,
                 * $orderby=,patid desc,
                 * $orderby=,patid desc
                */
            }
        }
    }
}
