using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public partial class OtherErrors_UT
    {
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void CreateRecord_BadDataWithoutErrors()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                apiHelperObject.Endpoint = "/api/v2/Patients";


                //This will succeed, but not in the expected way. It will create a patient without a Patient_Note.
                apiHelperObject.APIBody = @"{
                  ""PatFirstName"" : ""api with Patient_Note"",
                  ""PatLastName"" : ""api with Patient_Note"",
                  ""patientINCORRECTNAME_NotesObjectWithoutID"": [
                          {
                                  ""PatNoteEvent"": ""Created"",
                                  ""PatNoteNote"": ""Patient Created by api"",
                                  ""PatNoteUserName"":""API Test""
                          }
                        ]
                }";

                runOtherErrorCall(ref apiHelperObject, HttpMethod.Post, true);

                Assert.Equal(200, apiHelperObject.ResponseStatus);
                Assert.Null(apiHelperObject.ResponseMessage);
            }
        }
    }
}
