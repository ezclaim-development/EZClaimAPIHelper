using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public partial class OtherErrors_UT
    {
        [Fact(Skip = "This is used for example purposes only. IT SHOULD NOT BE RUN!")]
        public void RateLimiting()
        {
            //Removing the following return statement and uncommenting out the below commented code will attempt to rate limit the api. This should only be done by someone that has permission to do so. If you do not have the proper permission you will run the risk of being blacklisted.
            return;

            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{}}";

                int count = 0;


                //Parallel.For(0, 200, x =>
                //{
                //    runOtherErrorCall_PrintOutcome(ref apiHelperObject, HttpMethod.Post, false, false);
                //    count++;
                //    output.WriteLine("Parallel count" + count.ToString());
                //});



                //apiHelperObject.Token = APIUnitTestHelperObject.s01Token_CreatePatient;
                //apiHelperObject.Endpoint = "/api/v2/Patients/GetList";

                //runOtherErrorCall_PrintOutcome(ref apiHelperObject, HttpMethod.Post, false, false);



                //for (int i = 0; i < 5; i++)
                //{
                //    runOtherErrorCall_PrintOutcome(ref apiHelperObject, HttpMethod.Post, false, false);
                //    count++;
                //    output.WriteLine("count " + count.ToString());
                //}





                output.WriteLine("Force rate limit");


                int inUseCounter = 0;

                long lastRunTicks = DateTime.Now.Ticks;
                Random rnd = new Random();

                //Parallel.For(0, 40, x =>
                //{
                //    Thread.Sleep(rnd.Next(0, 21) * 1000);
                //    int runTime = 0;

                //    while (runTime < 5)
                //    {
                //        if (inUseCounter <= 0 || (DateTime.Now.Ticks - lastRunTicks) > 10000000)
                //        {
                //            inUseCounter++;
                //            lastRunTicks = DateTime.Now.Ticks;
                //            runTime++;
                //            runOtherErrorCall_PrintOutcome(ref apiHelperObject, HttpMethod.Post, false, false);
                //            count++;
                //            output.WriteLine("count " + count.ToString());
                //            inUseCounter--;
                //        }
                //        else
                //        {
                //            Thread.Sleep(rnd.Next(1000, 5000));
                //        }
                //    }
                //});


                //output.WriteLine("Confirm rate limit happens for token");
                //output.WriteLine("token = s01Token " + count.ToString());

                //runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "API calls quota exceeded! maximum admitted 100 per 15m.", false);

                //output.WriteLine("Confirm rate limiting happens for the database");
                //output.WriteLine("token = s01Token_CreatePatient " + count.ToString());

                //apiHelperObject.Token = APIUnitTestHelperObject.s01Token_CreatePatient;

                //runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "API calls quota exceeded! maximum admitted 100 per 15m.", false);

                //output.WriteLine("Confirm rate limiting happens for the client");
                //output.WriteLine("token = s01Token_SameClientDifferentDatabase " + count.ToString());

                //apiHelperObject.Token = APIUnitTestHelperObject.s01Token_SameClientDifferentDatabase;

                //runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "API calls quota exceeded! maximum admitted 100 per 15m.", false);

                //output.WriteLine("Confirm rate limiting happens for the IP");
                //output.WriteLine("token = junk" + count.ToString());

                //apiHelperObject.Token = "junk";
                //inUseCounter = 0;

                //lastRunTicks = DateTime.Now.Ticks;
                //rnd = new Random();

                //Parallel.For(0, 40, x =>
                //{
                //    Thread.Sleep(rnd.Next(0, 21) * 1000);
                //    int runTime = 0;

                //    while (runTime < 5)
                //    {
                //        if (inUseCounter <= 0 || (DateTime.Now.Ticks - lastRunTicks) > 10000000)
                //        {
                //            inUseCounter++;
                //            lastRunTicks = DateTime.Now.Ticks;
                //            runTime++;
                //            runOtherErrorCall_PrintOutcome(ref apiHelperObject, HttpMethod.Post, false, false);
                //            count++;
                //            output.WriteLine("count " + count.ToString());
                //            inUseCounter--;
                //        }
                //        else
                //        {
                //            Thread.Sleep(rnd.Next(1000, 5000));
                //        }
                //    }
                //});

                //runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "API calls quota exceeded! maximum admitted 100 per 15m.", false);



                //apiHelperObject.Token = APIUnitTestHelperObject.s01Token_SameClientDifferentDatabase;


                //runOtherErrorCall_PrintOutcome(ref apiHelperObject, HttpMethod.Post, false, false);
                //runOtherErrorCall_PrintOutcome(ref apiHelperObject, HttpMethod.Post, false, false);
                //runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "Client rate limit of 2 per 1m hit.", false);
                //runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "Database rate limit of 3 per 1m hit.", false);
                //runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "Token rate limit of 4 per 1m hit.", false);
            }
        }
    }
}
