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
    public partial class UpdatePatientWithGoodOdataCalls_UT
    {
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void UpdatePatientWithGoodFilterOdataCalls()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");


                queryValue = "$filter=Patfirstname ne 'this is a test'";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=Patfirstname in ('this is a' test')";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=Patfirstname in ('this is a' test', 'this is another' test')";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid in (1,2)";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid in (1, 2)";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid in (1,  2)";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter= patid in (1, 2)";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = " $filter=patid in (1, 2)";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid eq 1 AND patid eq 1";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid ne 1 AND patid ne 1";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid ne '1' AND patid ne '1'";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid in ('1') AND patid in ('1')";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid in ( 1 ) AND patid in ( 1 )";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid in (1 ,2) AND patid in (1 ,2)";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid in (1,  2) AND patid in (1,  2)";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid eq 1";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid in (12,  12) AND patid in (12,  12)";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid eq 1 AND patFirstName eq 1";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid eq 1 AND patFirstName eq '1'";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=patid in (12,  12) AND PatFirstName in (12,  12)";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=Patfirstname eq 'this is a test'";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=Patfirstname in ('this')";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                Thread.Sleep(3000);


                queryValue = "$filter=Patfirstname in ('this', 'that')";

                patientsObject = new();
                patientsObject.Add("patzip", "123456");
                updatePatientWithGoodOdata(ref apiHelperObject, queryValue, patientsObject);

                //These don't currently work but they should
                //queryValue = "$filter=Patfirstname ne 'this is a' test'";
                //queryValue = "$filter=Patfirstname eq 'this is a' test'";
            }
        }
    }
}
