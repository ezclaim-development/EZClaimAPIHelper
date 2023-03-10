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
        public void SelectPatientWithGoodJoinOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=RIGHT Claim ClaPatFID EQ Patient PatID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID NE Patient PatID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID EQ Claim ClaID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID RIGHT Service_Line SrvClaFID EQ Claim ClaID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID NE Claim ClaID";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);


                //The following are both technically legal, however they will either return junk data or fail depending on what is stored in the field you are looking at. PatFirstName, for example, is a string and cannot be converted to an int in most instances.
                //queryValue = "$join=LEFT Claim ClaID EQ Patient PatID";
                //queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatFirstName";
            }
        }
    }
}
