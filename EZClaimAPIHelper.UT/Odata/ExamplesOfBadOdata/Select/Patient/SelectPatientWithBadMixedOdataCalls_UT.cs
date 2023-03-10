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
        public void SelectPatientWithBadMixedOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$select=;$filter=;$orderby=;$ids=;$top=;$join=";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$select=;$filter=;$orderby=;$ids=;$top=;$join='. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$select=patid,patfirstname , patlastname;$filter=patid eq 1;$orderby=patid;$ids=1;$top=10;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Unable to parse supplied select query string. The following was unable to be parsed ( , ). Select statements can be in the form of [Table], [Column], [Column] AS 'Alias'");

                Thread.Sleep(3000);

                queryValue = "$select=patid, patfirstname, patlastname;$filter=patid  eq 1;$orderby=patid ;$ids=1;$top=10; $join=LEFT Claim ClaPatFID EQ Patient PatID;";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$select=patid,patfirstname, patlastname;$filter=patid eq  1;$orderby=patid;$ids=1 ;$top=10;$join=LEFT  Claim  ClaPatFID EQ  Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$select=patid,asdf,patlastname;$filter=asdf eq 1;$orderby=asdf;$ids=1;$top=10;$join=LEFT Claim ClaPatFID EQ Patient asfd";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asfd) does not exist in the table (Patient) for statement 1.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$select=patid as 'asdfasdf,patfirstname,patlastname;$filter=patid eq 1;$orderby=patid;$ids=1;$top=10;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Unable to parse supplied select query string. The following was unable to be parsed (  '). Select statements can be in the form of [Table], [Column], [Column] AS 'Alias'");

                Thread.Sleep(3000);

                queryValue = "$select=patid,patfirstname,patlastname;$filter=patid eq 1;$orderby=patid;$ids=1;$top=10*;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid Top X condition: 10*. Value must be whole number.");

                Thread.Sleep(3000);

                queryValue = "$se lect=patid,patfir stname,patlastname;$filter=patid eq 1; $orderby=patid;$ids=1;$top=10 ;$join=LEFT Claim ClaPatFI D EQ Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : ($se lect=patid,patfir stname,patlastname ). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$s elect=patid,patfirstna me,patlastname;$filt er=patid eq 1;$orderb y=patid;$ids=1;$t op=10;$join=LEFT Claim ClaPatFID EQ Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : ($s elect=patid,patfirstna me,patlastname$filt er=patid eq 1$orderb y=patid$t op=10). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$select=patid,patfirstname,patlastname;$filter=patid eq asdf;$orderby=asdf;$ids=1;$top=10;$join=LEFT Claim asdf EQ Patient sadf";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdf) does not exist in the table (Claim) for statement 1.");
                expectedContainsValuesList.Add("The supplied column (sadf) does not exist in the table (Patient) for statement 1.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);
            }
        }
    }
}
