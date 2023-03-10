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
        public void SelectPatientWithGoodJoinWithFilterOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid eq 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid ne 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (1)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (1, 2)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid ne 1 and claid eq 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (1, 2) and claid ne 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname eq 'o'lery'";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname ne 'o'lery'";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname in ('o'lery')";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid eq 1 and claid eq 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (1) and claid eq 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (1, 2) and claid eq 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid eq null";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid ne null";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname eq 'o'lery'";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname in ('o'lery')";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid ne 1 and claid eq 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

            }
        }
    }
}
