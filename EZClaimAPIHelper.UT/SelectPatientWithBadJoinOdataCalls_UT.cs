﻿using System.Collections.Generic;
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
        public void SelectPatientWithBadJoinOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, ExampleRSAPublicKey, s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "join=LEFT Claim ClaPatFID EQ Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameter : 'join=LEFT Claim ClaPatFID EQ Patient PatID'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$join LEFT Claim ClaPatFID EQ Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameter : '$join LEFT Claim ClaPatFID EQ Patient PatID'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$join:LEFT Claim ClaPatFID EQ Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameter : '$join:LEFT Claim ClaPatFID EQ Patient PatID'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$join=jlaksdjf Claim ClaPatFID EQ Patient PatID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied join type (jlaksdjf) for statement 1 is incorrect. The following types of join types are allowed (LEFT,RIGHT).");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT asdfasdf ClaPatFID EQ Patient PatID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied joining table (asdfasdf) for statement 1 is not supported.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim asdfasdf EQ Patient PatID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdfasdf) does not exist in the table (Claim) for statement 1.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID IN Patient PatID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied comparison type (IN) for statement 1 is incorrect. The following types of comparison types are allowed (EQ,NE).");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID asdfasdf Patient PatID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied comparison type (asdfasdf) for statement 1 is incorrect. The following types of comparison types are allowed (EQ,NE).");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ asdfasdf PatID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied existing table (asdfasdf) for statement 1 is not supported.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient asdfasdf";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdfasdf) does not exist in the table (Patient) for statement 1.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=Claim ClaPatFID EQ Patient PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, @"Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Patient PatID EQ Claim ClaPatFID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied joining table (Patient) for statement 1 is already in the list of joined tables. Tables can only be joined once each.");
                expectedContainsValuesList.Add("The supplied existing table (Claim) for statement 1 is not in the list of joined tables. The existing table must be a table that has already been joined to the list.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID jlaksjdflkasdf Service_Line SrvClaFID EQ Claim ClaID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied join type (jlaksjdflkasdf) for statement 2 is incorrect. The following types of join types are allowed (LEFT,RIGHT).");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT lkajsdlf SrvClaFID EQ Claim ClaID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied joining table (lkajsdlf) for statement 2 is not supported.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line asdfasdf EQ Claim ClaID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdfasdf) does not exist in the table (Service_Line) for statement 2.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID asdf Claim ClaID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied comparison type (asdf) for statement 2 is incorrect. The following types of comparison types are allowed (EQ,NE).");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID EQ asdf ClaID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied existing table (asdf) for statement 2 is not supported.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID EQ Claim asdfasf";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdfasf) does not exist in the table (Claim) for statement 2.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID LEFT Service_Line SrvClaFID EQ Patient ClaID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (ClaID) does not exist in the table (Patient) for statement 2.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaID EQ Service_Line SrvClaFID LEFT Claim ClaPatFID EQ Patient PatID";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied existing table (Service_Line) for statement 1 is not in the list of joined tables. The existing table must be a table that has already been joined to the list.");
                expectedContainsValuesList.Add("The supplied joining table (Claim) for statement 2 is already in the list of joined tables. Tables can only be joined once each.");

                selectPatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, expectedContainsValuesList);



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
