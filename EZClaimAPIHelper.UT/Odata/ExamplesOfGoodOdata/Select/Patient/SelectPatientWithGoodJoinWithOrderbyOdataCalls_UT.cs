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
        public void SelectPatientWithGoodJoinWithOrderbyOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby=Patid";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby=Patid asc";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby=Claid desc";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby=Claid asc";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby=Claid, Patid";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$Orderby=Claid desc, Patid asc";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);
            }
        }
    }
}
