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
    public partial class DeletePatientWithGoodOdataCalls_UT
    {
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void DeletePatientWithGoodFilterOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = @"$filter=Patfirstname ne \""this is a test\""";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is a' test\"")";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is a' test\"", \""this is another' test\"")";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (100,2)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (100, 2)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (100,  2)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter= patid in (100, 2)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @" $filter=patid in (100, 2)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 100 AND patid eq 100";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid ne 100 AND patid ne 100";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid ne \""100\"" AND patid ne \""100\""";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""100\"") AND patid in (\""100\"")";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in ( 100 ) AND patid in ( 100 )";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (100 ,2) AND patid in (100 ,2)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (100,  2) AND patid in (100,  2)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 100";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (12,  12) AND patid in (12,  12)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 100 AND patFirstName eq 100";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 100 AND patFirstName eq \""100\""";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (12,  12) AND PatFirstName in (12,  12)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""this is a test\""";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this\"")";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this\"", \""that\"")";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname ne \""this is a' test\""";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""this is a' test\""";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""this is a\""\"" test\""";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);
            }
        }
    }
}
