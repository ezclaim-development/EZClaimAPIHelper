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
        public void SelectPatientWithBadSelectOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                queryValue = "$select=Claim";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : Claim");

                Thread.Sleep(3000);

                queryValue = "$select=Patient, Claim";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : Claim");

                Thread.Sleep(3000);

                queryValue = "$select=Patient, PatID, Claim";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : Claim");

                Thread.Sleep(3000);

                queryValue = "$select=Patient AS 'ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : Patient AS 'ID'");

                Thread.Sleep(3000);

                queryValue = "$select=PatID AS ID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : AS,ID");

                Thread.Sleep(3000);

                queryValue = "$select=PatID AS ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Unable to parse supplied select query string. The following was unable to be parsed (  '). Select statements can be in the form of [Table], [Column], [Column] AS 'Alias'");

                Thread.Sleep(3000);

                queryValue = "$select=PatID AS 'ID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Unable to parse supplied select query string. The following was unable to be parsed (  '). Select statements can be in the form of [Table], [Column], [Column] AS 'Alias'");

                Thread.Sleep(3000);

                queryValue = "$select=PatID AS 123";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : 123,AS");

                Thread.Sleep(3000);

                queryValue = "$select=PatID AS !@#";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Unable to parse supplied select query string. The following was unable to be parsed (  !@#). Select statements can be in the form of [Table], [Column], [Column] AS 'Alias'");

                Thread.Sleep(3000);

                queryValue = "$select=,PatID AS 'ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Unable to parse supplied select query string. The following was unable to be parsed (,). Select statements can be in the form of [Table], [Column], [Column] AS 'Alias'");

                Thread.Sleep(3000);

                queryValue = "$select=PatID of 'ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Unable to parse supplied select query string. The following was unable to be parsed (  ''). Select statements can be in the form of [Table], [Column], [Column] AS 'Alias'");

                Thread.Sleep(3000);

                queryValue = "$select=PatID 'ID'";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Unable to parse supplied select query string. The following was unable to be parsed ( ''). Select statements can be in the form of [Table], [Column], [Column] AS 'Alias'");

                Thread.Sleep(3000);

                queryValue = "$select=PatID1";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : PatID1");

                Thread.Sleep(3000);

                queryValue = "$select:PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$select:PatID'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$selects=PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : '$selects=PatID'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "select=PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid input parameter : 'select=PatID'. Query can contain '$select=' or '$filter=' or '$orderby=' or '$ids=' or '$top=' or 'join='");

                Thread.Sleep(3000);

                queryValue = "$select=,PatID";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Unable to parse supplied select query string. The following was unable to be parsed (,). Select statements can be in the form of [Table], [Column], [Column] AS 'Alias'");

                Thread.Sleep(3000);

                queryValue = "$select=junk";

                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid select statement in request : junk");

                Thread.Sleep(3000);

                queryValue = "$select=PatID AS 'ID',";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid statement in request : $select=PatID AS 'ID',");

                Thread.Sleep(3000);

                queryValue = "$select=PatID,";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid statement in request : $select=PatID,");

                Thread.Sleep(3000);

                queryValue = "$ids=1,";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid statement in request : $ids=1,");

                Thread.Sleep(3000);

                queryValue = "$ids=,1";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid statement in request : $ids=,1");

                Thread.Sleep(3000);

                queryValue = "$orderby=patid desc,";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid statement in request : $orderby=patid desc,");

                Thread.Sleep(3000);

                queryValue = "$orderby=,patid desc,";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid statement in request : $orderby=,patid desc,");

                Thread.Sleep(3000);

                queryValue = "$orderby=,patid desc";
                selectPatientWithBadOdata_ExpectedOutcomeEquals(ref apiHelperObject, queryValue, "Invalid statement in request : $orderby=,patid desc");
            }
        }
    }
}
