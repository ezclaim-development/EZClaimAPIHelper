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
        public void DeletePatientWithGoodJoinWithFilterOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid eq 100";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid ne 100";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (100)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (100, 2)";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid ne 100 and claid eq 1000";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (100, 2) and claid ne 1000";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname eq \""o'lery\""";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname ne \""o'lery\""";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname in (\""o'lery\"")";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid eq 100 and claid eq 1000";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (100) and claid eq 1000";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (100, 2) and claid eq 1000";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid eq null";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid ne null";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname eq \""o'lery\""";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname in (\""o'lery\"")";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid ne 100 and claid eq 1000";

                deletePatientWithGoodOdata(ref apiHelperObject, queryValue);
            }
        }
    }
}
