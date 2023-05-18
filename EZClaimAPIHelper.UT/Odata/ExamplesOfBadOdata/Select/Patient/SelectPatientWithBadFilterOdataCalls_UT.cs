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
        public void SelectPatientWithBadFilterOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = @"$filterS=PatLastName ne \""APIPatientLastName\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameter : '$filterS=PatLastName ne ""APIPatientLastName""'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = @"filter=PatLastName ne \""APIPatientLastName\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameter : 'filter=PatLastName ne ""APIPatientLastName""'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName \""APIPatientLastName\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter:PatLastName eq \""APIPatientLastName\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameter : '$filter:PatLastName eq ""APIPatientLastName""'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName eqs \""APIPatientLastName\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patasdf eq \""APIPatientLastName\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters : Patasdf");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName eq APIPatientLastName";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName in APIPatientLastName";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName in \""APIPatientLastName\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName in (\""APIPatientLastName\"",)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid statement in request : $filter=PatLastName in (""APIPatientLastName"",)");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName eq \""APIPatientLastName\"" junk PatLastName eq \""APIPatientLastName\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata (PatLastName eq ""APIPatientLastName"" junk PatLastName eq ""APIPatientLastName"") contains unescaped double quote.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName eq \""APIPatientLastName\"" s";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName in (APIPatientLastName)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - Quoted items in odata in list are not in correctly listed form (PatLastName in (APIPatientLastName))");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName in (\""APIPatientLastName\"",\""test)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName eq \""APIPatientLastName\"" junk";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName eq \""APIPatientLastName\"" And";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName eq \""APIPatientLastName\"" And PatFirstName";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatLastName eq \""APIPatientLastName\"" or and";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = @"$filter=junk";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname ne\""this\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname ne(\""this\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in(\""this\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this\"", \""that\"",)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid statement in request : $filter=Patfirstname in (""this"", ""that"",)");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (,\""this\"", \""that\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid statement in request : $filter=Patfirstname in (,""this"", ""that"")");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in  (1, 2)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid  in (1, 2)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter =patid in (1, 2)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameter : '$filter =patid in (1, 2)'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = @"$ filter=patid in (1, 2)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameter : '$ filter=patid in (1, 2)'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq a";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq (\""a\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq (a)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq (1)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 2,3";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (a,b)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - Quoted items in odata in list are not in correctly listed form (patid in (a,b))");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""1\"",\""b)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq \""a\"" AND patid eq \""a\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Failed to convert parameter value from a String to a Int32.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""1\"", \""b\"") AND patid in (\""1\"", \""b\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (1, \""b\"") AND patid in (1, \""b\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""1\"", \""b'on\"") AND patid in (\""1\"", \""b'on\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Conversion failed when converting the nvarchar value 'b'on' to data type int.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (1, \""b'on\"") AND patid in (1, \""b'on\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Conversion failed when converting the nvarchar value 'b'on' to data type int.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""1\"",\""b\"") AND patid in (\""1\"",\""b\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid   in   (\""1,  \""b\"")  AND      patid  in ( \""1,    \""b\"" )";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""1,  \""b) AND patid in (\""1,    \""b\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""1, \""b) AND patid in (\""1, \""b\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""1,\""b) AND patid in (\""1,\""b\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameter : '$filter='. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in (\""1\"",\""b\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Conversion failed when converting the nvarchar value 'b' to data type int.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 AND";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 OR";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters in '$filter', please provide valid conditions without extra spaces.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 test";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata doesn't match expected format.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 AND\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 OR\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 test\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 test patid eq 1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is a' test\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Query parameters yielded no results.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""this is a' test\"", \""this is another' test\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Query parameters yielded no results.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid ne \""1' AND patid ne \""1\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid in ('1') AND patid in ('1')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Conversion failed when converting the nvarchar value ''1'' to data type int.");

                Thread.Sleep(3000);

                queryValue = @"$filter=patid eq 1 AND patFirstName eq \""1\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Query parameters yielded no results.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""this is a test\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Query parameters yielded no results.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in ('this')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - Quoted items in odata in list are not in correctly listed form (Patfirstname in ('this'))");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in ('this', 'that')";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - Quoted items in odata in list are not in correctly listed form (Patfirstname in ('this', 'that'))");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatCreatedUserGUID eq \""APIAdjustment\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Failed to convert parameter value from a String to a Guid.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatCreatedUserGUID eq \""123\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Failed to convert parameter value from a String to a Guid.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""1\""23\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""12 \""3\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""12 \"" \""3\""";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata (Patfirstname eq ""12 "" ""3"") contains unescaped double quote.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (123 234)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - Quoted items in odata in list are not in correctly listed form (Patfirstname in (123 234))");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (123, , 234)";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - Quoted items in odata in list are not in correctly listed form (Patfirstname in (123, , 234))");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""123\"", , \""234\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - Quoted items in odata in list are not in correctly listed form (Patfirstname in (""123"", , ""234""))");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""12 \""3\"", \""234\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatCreatedUserGUID eq \""APIAdjustment\"" OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Failed to convert parameter value from a String to a Guid.");

                Thread.Sleep(3000);

                queryValue = @"$filter=PatCreatedUserGUID eq \""123\"" OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Failed to convert parameter value from a String to a Guid.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""1\""23\"" OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""12 \""3\"" OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname eq \""12 \"" \""3\"" OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - supplied odata (Patfirstname eq ""12 "" ""3"" OR) contains unescaped double quote.");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (123 234) OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - Quoted items in odata in list are not in correctly listed form (Patfirstname in (123 234) OR)");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (123, , 234) OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - Quoted items in odata in list are not in correctly listed form (Patfirstname in (123, , 234) OR)");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""123\"", , \""234\"") OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - Quoted items in odata in list are not in correctly listed form (Patfirstname in (""123"", , ""234"") OR)");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (\""12 \""3\"", \""234\"") OR Patfirstname eq \""APIA\""\""dju \""\""stment\"" AND Patfirstname In (\""APIAdjustment\"", \""APIAd\""\""justment\"", \""APIAdj ustment\"", \""APIAdj\""\"" test \""\"" ustment\"")";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters - quotes are not escaped properly.");

                queryValue = @"$filter=Patfirstname in (  1  ,,  \""a\""  )";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid statement in request : $filter=Patfirstname in (  1  ,,  ""a""  )");

                Thread.Sleep(3000);

                queryValue = @"$filter=Patfirstname in (  \""a\""  ,, 1  )";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid statement in request : $filter=Patfirstname in (  ""a""  ,, 1  )");


                //These work, but should fail
                //queryValue = @"$filter=Patfirstname in (  1  , ,  \""a\""  )";

                //selectPatientWithBadOdata_PrintOutcome(ref apiHelperObject, queryValue);

                //Thread.Sleep(3000);

                //queryValue = @"$filter=Patfirstname in (  \""a\""  , , 1  )";

                //selectPatientWithBadOdata_PrintOutcome(ref apiHelperObject, queryValue);
                
                
                //These fail but should work
                //queryValue = @"$filter=Patfirstname in (  \""a,,b,c\"")";

                //selectPatientWithBadOdata_PrintOutcome(ref apiHelperObject, queryValue);
                
                //Thread.Sleep(3000);

                //queryValue = @"$filter=Patfirstname eq \""a,,b,c\""";

                //selectPatientWithBadOdata_PrintOutcome(ref apiHelperObject, queryValue);

            }
        }
    }
}
