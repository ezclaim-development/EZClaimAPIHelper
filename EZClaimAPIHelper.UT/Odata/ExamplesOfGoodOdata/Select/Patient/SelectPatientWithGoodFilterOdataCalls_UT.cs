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
        public void SelectPatientWithGoodFilterOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = @"$filter=Patfirstname ne \""this is a' test\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""this is a' test\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is a' test\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname ne \""o'lery\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname in (\""o'lery\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this\"", \""is\"") OR Patfirstname in (\""a\"", \""test\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is what some people call \""\""Irony\""\""\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is not \""\""Irony\""\""\"", \""nor this\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this\"", \""is\"") or Patfirstname eq \""test\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""The Mrs Learys' homes\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname ne \""this is a test\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is a' test\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is a' test\"", \""this is another' test\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (1,2)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (1, 2)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (1,  2)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter= patid in (1, 2)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @" $filter=patid in (1, 2)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 AND patid eq 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid ne 1 AND patid ne 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid ne \""1\"" AND patid ne \""1\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""1\"") AND patid in (1)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in ( 1 ) AND patid in ( 1 )";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (1 ,2) AND patid in (1 ,2)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (1,  2) AND patid in (1,  2)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (12,  12) AND patid in (12,  12)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 AND patFirstName eq 1";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 AND patFirstName eq \""1\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (12,  12) AND PatFirstName in (12,  12)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""this is a test\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this\"", \""that\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is what some people call \""\""Irony\""\""\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is not \""\""Irony\""\""\"", \""nor this\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname ne \""this is a test\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is a' test\"", \""this is another' test\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid ne \""1\"" AND patid ne \""1\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""1\"") AND patid in (\""1\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 AND patFirstName eq \""1\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""this is a test\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this\"", \""that\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""APIAdjustment\"" AND Patfirstname eq \""APIAdj\""\""ustment\"" OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"") AND Patfirstname eq 123 OR Patfirstname in (123, 23, \""45\"") OR Patfirstname eq null OR Patfirstname ne null OR Patfirstname eq \""null\"" OR Patfirstname ne \""null\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=PatCreatedUserGUID eq \""99306A64-9F3F-481B-9C7F-9F1DD0A9A8CB\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""123\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq 123";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq null";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""null\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""12 \""\""3\""";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (123)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (123, 234)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""12 \""\""3\"", \""234\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (123, \""abc\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""abc\"", 123)";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=PatCreatedUserGUID in (\""19306A64-9F3F-481B-9C7F-9F1DD0A9A8CD\"", \""99306A64-9F3F-481B-9C7F-9F1DD0A9A8CB\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=PatCreatedUserGUID eq \""99306A64-9F3F-481B-9C7F-9F1DD0A9A8CB\"" OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""123\"" OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq 123 OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq null OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""null\"" OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""12 \""\""3\"" OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (123) OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (123, 234) OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""12 \""\""3\"", \""234\"") OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (123, \""abc\"") OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""abc\"", 123) OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                Thread.Sleep(3000);

                queryValue = @"$filter=PatCreatedUserGUID in (\""19306A64-9F3F-481B-9C7F-9F1DD0A9A8CD\"", \""99306A64-9F3F-481B-9C7F-9F1DD0A9A8CB\"") OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                SelectPatientWithGoodOdata(ref apiHelperObject, queryValue);

                //These currently fail because of the extra comma. We might change this in the future. For the moment we are not sure. If you have a real world case where you need a comma, please let us know.
                //queryValue = @"$filter=Patfirstname in (\""12, 3\"", \""234\"")";
                //queryValue = @"$filter=Patfirstname in (\""12, 3\"", \""234\"") OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

            }
        }
    }
}
