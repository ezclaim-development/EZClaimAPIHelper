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
        public void UpdatePatientWithBadJoinWithFilterOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "join=;filter=";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : (join=filter=). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID&$filter=Patid eq 1";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameter : (=Patid eq 1). Query can contain 'filter' or 'join')");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (1, a)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in eq null";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in ne null";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname eq ('o'lery')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patfirstname in 'o'lery'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters : o'lery");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in eq null and claid eq 1";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in ne null and claid eq 1";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=asdf Claim ClaPatFID EQ Patient PatID;$filter=Patid eq 1";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied join type (asdf) for statement 1 is incorrect. The following types of join types are allowed (LEFT,RIGHT).");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT asdf ClaPatFID EQ Patient PatID;$filter=Patid ne 1";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied joining table (asdf) for statement 1 is not supported.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim asdf EQ Patient PatID;$filter=Patid in (1)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdf) does not exist in the table (Claim) for statement 1.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID IN Patient PatID;$filter=Patid ON (1, 2)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied comparison type (IN) for statement 1 is incorrect. The following types of comparison types are allowed (EQ,NE).");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Claim PatID;$filter=Patid in (1, a)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied existing table (Claim) for statement 1 is not in the list of joined tables. The existing table must be a table that has already been joined to the list.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient ClaPatFID;$filter=Patid in eq null";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (ClaPatFID) does not exist in the table (Patient) for statement 1.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=asdf in ne null";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=asdf eq 'o'lery'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters : asdf");

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient asdf;$filter=Patfirstname eq ('o'lery')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdf) does not exist in the table (Patient) for statement 1.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient asdf;$filter=Patfirstname eq ('o'lery', 'test')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asdf) does not exist in the table (Patient) for statement 1.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient asfd;$filter=asdf in ('o'lery')";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied column (asfd) does not exist in the table (Patient) for statement 1.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT asdf ClaPatFID EQ Patient PatID;$filter=Patfirstname in 'o'lery'";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The following errors occurred while attempting to processes the supplied join statement:");
                expectedContainsValuesList.Add("The supplied joining table (asdf) for statement 1 is not supported.");

                updatePatientWithBadOdata_ExpectedOutcomeContains(ref apiHelperObject, queryValue, patientsObject, expectedContainsValuesList);

                Thread.Sleep(3000);

                queryValue = "$join=LEFT Claim ClaPatFID EQ Patient PatID;$filter=Patid in (null)";

                patientsObject = new();
                patientsObject.Add("patfirstname", "test");
                updatePatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, patientsObject, "Invalid input parameters.");


            }
        }
    }
}
