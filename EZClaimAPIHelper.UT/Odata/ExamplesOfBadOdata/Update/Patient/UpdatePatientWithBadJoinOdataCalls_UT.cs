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
    public partial class UpdatePatientWithBadOdataCalls_UT
    {
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void UpdatePatientWithBadJoinOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");


                queryValue = "join=";

                patientsObject = new();

                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : (join=). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "join=LEFT Claim ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : (join=LEFT Claim ClaPatFID EQ Patient PatID). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join LEFT Claim ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : ($join LEFT Claim ClaPatFID EQ Patient PatID). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join:LEFT Claim ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : ($join:LEFT Claim ClaPatFID EQ Patient PatID). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=jlaksdjf Claim ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied join type (jlaksdjf) for statement 1 is incorrect. The following types of join types are allowed (LEFT,RIGHT).");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT asdfasdf ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied joining table (asdfasdf) for statement 1 is not supported.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim asdfasdf EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdfasdf) does not exist in the table (Claim) for statement 1.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID IN Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied comparison type (IN) for statement 1 is incorrect. The following types of comparison types are allowed (EQ,NE).");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID asdfasdf Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied comparison type (asdfasdf) for statement 1 is incorrect. The following types of comparison types are allowed (EQ,NE).");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ asdfasdf PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied existing table (asdfasdf) for statement 1 is not supported.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient asdfasdf";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdfasdf) does not exist in the table (Patient) for statement 1.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=Claim ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Patient PatID EQ Claim ClaPatFID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied joining table (Patient) for statement 1 is already in the list of joined tables. Tables can only be joined once each.");
                expectedContainsValuesList.Add("The supplied existing table (Claim) for statement 1 is not in the list of joined tables. The existing table must be a table that has already been joined to the list.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID jlaksjdflkasdf Service_Line SrvClaFID EQ Claim ClaID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied join type (jlaksjdflkasdf) for statement 2 is incorrect. The following types of join types are allowed (LEFT,RIGHT).");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT lkajsdlf SrvClaFID EQ Claim ClaID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied joining table (lkajsdlf) for statement 2 is not supported.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line asdfasdf EQ Claim ClaID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdfasdf) does not exist in the table (Service_Line) for statement 2.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID asdf Claim ClaID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied comparison type (asdf) for statement 2 is incorrect. The following types of comparison types are allowed (EQ,NE).");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID EQ asdf ClaID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied existing table (asdf) for statement 2 is not supported.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID EQ Claim asdfasf";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdfasf) does not exist in the table (Claim) for statement 2.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID EQ Patient ClaID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (ClaID) does not exist in the table (Patient) for statement 2.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaID EQ Service_Line SrvClaFID LEFT Claim ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied existing table (Service_Line) for statement 1 is not in the list of joined tables. The existing table must be a table that has already been joined to the list.");
                expectedContainsValuesList.Add("The supplied joining table (Claim) for statement 2 is already in the list of joined tables. Tables can only be joined once each.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$Select=patid;$join=LEFT Claim ClaID EQ Service_Line SrvClaFID LEFT Claim ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : ($Select=patid). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$top=1;$join=LEFT Claim ClaID EQ Service_Line SrvClaFID LEFT Claim ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : ($top=1). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "No Objects Could Be Updated - The following errors were encountered: Identification Column Not Found - PatID must be filled in with a valid value. Object has been skipped.");

                Thread.Sleep(3000);

                queryValue = "$join=RIGHT Claim ClaPatFID EQ Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "No Objects Could Be Updated - The following errors were encountered: Identification Column Not Found - PatID must be filled in with a valid value. Object has been skipped.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID NE Patient PatID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "No Objects Could Be Updated - The following errors were encountered: Identification Column Not Found - PatID must be filled in with a valid value. Object has been skipped.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID EQ Claim ClaID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "No Objects Could Be Updated - The following errors were encountered: Identification Column Not Found - PatID must be filled in with a valid value. Object has been skipped.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID RIGHT Service_Line SrvClaFID EQ Claim ClaID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "No Objects Could Be Updated - The following errors were encountered: Identification Column Not Found - PatID must be filled in with a valid value. Object has been skipped.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID NE Claim ClaID";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "No Objects Could Be Updated - The following errors were encountered: Identification Column Not Found - PatID must be filled in with a valid value. Object has been skipped.");




                //The following are both technically legal, however they will either return junk data or fail depending on what is stored in the field you are looking at. PatFirstName, for example, is a string and cannot be converted to an int in most instances.
                //queryValue = "$join=LEFT Claim ClaID EQ Patient PatID";
                //queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatFirstName";

                //Should fail but currently works...
                //$join = LEFT Claim ClaPatFID EQ Patient PatID asdfas
                //$join = LEFT Claim ClaPatFID EQ Patient PatID,
                //$join = LEFT Claim ClaPatFID EQ Patient PatID LEFT 
                //$join = LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line
                //$join = LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID 
                //$join = LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID EQ
                //$join = LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID EQ Claim
            }
        }
    }
}
