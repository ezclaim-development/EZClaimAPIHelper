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
    public class FullCrudPatient_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudPatient_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the patient endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact]
        public void LiveCall_FullCrudPatient()
        {
            //This does a full gambit test on the Patient endpoints.
            return;

            //The API handles this part
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, ExampleRSAPublicKey, s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.Client002844_ProviderPortalTestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Patient(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestPatients(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Patient(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPatientsWithLastNameAPIPatientLastName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].PatID;
                int id2 = (int)apiHelperObject.ResponseData[1].PatID;
                int id3 = (int)apiHelperObject.ResponseData[2].PatID;

                Thread.Sleep(3000);

                selectPatientsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletPatientFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updatePatientListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updatePatientZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updatePatientFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deletePatientFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletPatientFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deletePatientFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPatientsWithLastNameAPIPatientLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 patient record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Patient(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients";
            apiHelperObject.APIBody = @"{
                    ""PatFirstName"": ""APIPatientFirstName7"",
                    ""PatLastName"":""APIPatientLastName""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 patient records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestPatients(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""PatFirstName"": ""APIPatientFirstName1"",
                    ""PatLastName"":""APIPatientLastName""
                    },
                    {
                    ""PatFirstName"": ""APIPatientFirstName2"",
                    ""PatLastName"":""APIPatientLastName""
                    },
                    {
                    ""PatFirstName"": ""APIPatientFirstName3"",
                    ""PatLastName"":""APIPatientLastName""
                    },
                    {
                    ""PatFirstName"": ""APIPatientFirstName4"",
                    ""PatLastName"":""APIPatientLastName""
                    },
                    {
                    ""PatFirstName"": ""APIPatientFirstName5"",
                    ""PatLastName"":""APIPatientLastName""
                    },
                    {
                    ""PatFirstName"": ""APIPatientFirstName6"",
                    ""PatLastName"":""APIPatientLastName""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 patient records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deletePatientFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients/ids";

            apiHelperObject.APIBody = @$"[{id1}, {id2}, {id3}]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting patient records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePatientFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Patients/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PatLastName eq 'APIPatientLastName'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a patient based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletPatientFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Patients/{id1}";

            apiHelperObject.APIBody = @"{}";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(1, apiHelperObject.ResponseData.Count);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Select patients based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectPatientsWithLastNameAPIPatientLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PatLastName eq 'APIPatientLastName'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Patients/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal("$filter=PatLastName eq 'APIPatientLastName'", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a patient based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPatientsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first patient record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Patient(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 patient records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updatePatientListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""patZip"": ""55551"",
                            ""patID"": {id1}
                        }},
                        {{
                            ""patZip"": ""55552"",
                            ""patID"": {id2}
                        }},
                        {{
                            ""patZip"": ""55553"",
                            ""patID"": {id3}
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal(55551, (int)apiHelperObject.ResponseData[0].PatZip);
            Assert.Equal(55552, (int)apiHelperObject.ResponseData[1].PatZip);
            Assert.Equal(55553, (int)apiHelperObject.ResponseData[2].PatZip);
        }

        /// <summary>
        /// Example of updating patients based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updatePatientZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients";

            apiHelperObject.APIBody = @$"{{
  ""patientsObject"": {{
    ""patZip"": ""55555""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq 'APIPatientLastName'""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(55555, (int)apiHelperObject.ResponseData[i].PatZip);
            }
        }

        /// <summary>
        /// Example of updating 1 patient record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updatePatientFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients";

            apiHelperObject.APIBody = @$"{{
                  ""patientsObject"": {{
                    ""patZip"": ""55551"",
                    ""patID"": {id1}
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal(55551, (int)apiHelperObject.ResponseData[0].PatZip);
        }
    }
}
