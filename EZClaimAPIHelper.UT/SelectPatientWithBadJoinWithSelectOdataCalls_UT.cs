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
        public void SelectPatientWithBadJoinWithSelectOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, ExampleRSAPublicKey, s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID&$select=Claim";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (=Claim). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID&$select=Claim,Patid";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (=Claim,Patid). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID&$select=ClaID,Patient";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (=ClaID,Patient). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID&$select=Claim,Patient";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (=Claim,Patient). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID&$select=ClaID AS 'Claim ID',PAtID AS 'Patient ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (=ClaID AS 'Claim ID',PAtID AS 'Patient ID'). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;&$select=Claim,Patid";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (&). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;&$select=ClaID,Patient";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (&). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;&$select=Claim,Patient";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (&). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;&$select=ClaID AS 'Claim ID',PAtID AS 'Patient ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : (&). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$select=asdf,Patid";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : asdf");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$select=Claim,asdf";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : asdf");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID asdf Patient PatID;$select=Claim,Patid";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied comparison type (asdf) for statement 1 is incorrect. The following types of comparison types are allowed (EQ,NE).");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$select=asdf,Patient";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : asdf");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$select=ClaID,asdf";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : asdf");

                Thread.Sleep(3000);

                queryValue = "$join=asdf Claim ClaPatFID EQ Patient PatID;$select=ClaID,Patient";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied join type (asdf) for statement 1 is incorrect. The following types of join types are allowed (LEFT,RIGHT).");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$select=asdf,Patient";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : asdf");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$select=Claim,asdf";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : asdf");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Service_Line ClaPatFID EQ Patient PatID;$select=Claim,Patient";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (ClaPatFID) does not exist in the table (Service_Line) for statement 1.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$select:ClaID AS 'Claim ID',PAtID AS 'Patient ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : ($select:ClaID AS 'Claim ID',PAtID AS 'Patient ID'). Query can contain 'select' or 'filter' or 'ids' or 'orderby' or 'top' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$select=asdf AS 'Claim ID',PAtID AS 'Patient ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : asdf AS 'Claim ID'");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$select=ClaID AS 'Claim ID',asdf AS 'Patient ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : asdf AS 'Patient ID'");

                Thread.Sleep(3000);

                queryValue = "$join=Lasdf;$select=ClaID AS 'Claim ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameters.");
            }
        }
    }
}
