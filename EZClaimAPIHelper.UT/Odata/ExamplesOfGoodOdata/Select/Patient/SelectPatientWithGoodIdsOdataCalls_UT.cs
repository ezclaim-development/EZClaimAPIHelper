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
    public partial class SelectPatientWithGoodOdataCalls_UT
    {
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void SelectPatientWithGoodIdsOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$ids=1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$ids=1,1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$ids=1,2";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$ids=1, 2";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$ids=1 , 2";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$ids=1,2,3";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);
            }
        }
    }
}
