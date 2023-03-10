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
        public void SelectPatientWithGoodMixedOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$select=patid,patfirstname,patlastname, claid;$filter=patid eq 1 and claid eq 1;$orderby=patid;$ids=1;$top=10;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$select=patid, patfirstname,patlastname, claim;$filter=patid eq 1 or claid eq 1;$orderby=patid;$ids=1;$top=10;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$select=patid,patfirstname,patlastname, claim;$filter= patid eq 1 and claid eq 1;$orderby=patid;$ids=1;$top=10;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$select=patid,patfirstname,patlastname, claim; $filter=patid eq 1 and claid eq 1;$orderby=patid;$ids=1;$top=10;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$select=patid as 'id',patfirstname as 'name',patlastname as 'name', claim;$filter=patid eq 1;$orderby=patid;$ids=1;$top=10;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$select=patid,patfirstname,patlastname, claim;$filter=patid eq 1;$orderby=patid;$ids=1;$top=10;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);
            }
        }
    }
}
