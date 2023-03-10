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
    public class FullCrudClaim_Notes_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudClaim_Notes_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Claim_Notes endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudClaim_Notes()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Claim_Notes(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestClaim_Notes(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Claim_Notes(ref apiHelperObject);

                Thread.Sleep(3000);

                selectClaim_NotesWithLastNameAPIClaim_NotesLastName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].ClaNoteID;
                int id2 = (int)apiHelperObject.ResponseData[1].ClaNoteID;
                int id3 = (int)apiHelperObject.ResponseData[2].ClaNoteID;

                Thread.Sleep(3000);

                selectClaim_NotesFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletClaim_NotesFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateClaim_NotesListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateClaim_NotesZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateClaim_NotesFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteClaim_NotesFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletClaim_NotesFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteClaim_NotesFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectClaim_NotesWithLastNameAPIClaim_NotesLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Claim_Notes record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Claim_Notes(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Notes";
            apiHelperObject.APIBody = @"{
                    ""ClaNoteClaFID"": ""1"",
                    ""ClaNoteEvent"": ""Created"",
                    ""ClaNoteUserName"":""APIClaim_NotesLastName""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Claim_Notes records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestClaim_Notes(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Notes/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""ClaNoteClaFID"": ""1"",
                    ""ClaNoteEvent"": ""Created"",
                    ""ClaNoteUserName"":""APIClaim_NotesLastName""
                    },
                    {
                    ""ClaNoteClaFID"": ""1"",
                    ""ClaNoteEvent"": ""Created"",
                    ""ClaNoteUserName"":""APIClaim_NotesLastName""
                    },
                    {
                    ""ClaNoteClaFID"": ""1"",
                    ""ClaNoteEvent"": ""Created"",
                    ""ClaNoteUserName"":""APIClaim_NotesLastName""
                    },
                    {
                    ""ClaNoteClaFID"": ""1"",
                    ""ClaNoteEvent"": ""Created"",
                    ""ClaNoteUserName"":""APIClaim_NotesLastName""
                    },
                    {
                    ""ClaNoteClaFID"": ""1"",
                    ""ClaNoteEvent"": ""Created"",
                    ""ClaNoteUserName"":""APIClaim_NotesLastName""
                    },
                    {
                    ""ClaNoteClaFID"": ""1"",
                    ""ClaNoteEvent"": ""Created"",
                    ""ClaNoteUserName"":""APIClaim_NotesLastName""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Claim_Notes records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteClaim_NotesFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Notes/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Claim_Notes records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteClaim_NotesFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Claim_Notes/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=ClaNoteUserName eq 'APIClaim_NotesLastName'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Claim_Notes based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletClaim_NotesFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Claim_Notes/{id1}";

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
        /// Select Claim_Notes based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectClaim_NotesWithLastNameAPIClaim_NotesLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Notes/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=ClaNoteUserName eq 'APIClaim_NotesLastName'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Claim_Notes/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal("$filter=ClaNoteUserName eq 'APIClaim_NotesLastName'", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Claim_Notes based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectClaim_NotesFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Claim_Notes record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Claim_Notes(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Notes/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Claim_Notes records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateClaim_NotesListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Notes/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""ClaNoteEvent"": ""Edited"",
                            ""ClaNoteID"": ""{id1}""
                        }},
                        {{
                            ""ClaNoteEvent"": ""Edited"",
                            ""ClaNoteID"": ""{id2}""
                        }},
                        {{
                            ""ClaNoteEvent"": ""Edited"",
                            ""ClaNoteID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal("Edited", (string)apiHelperObject.ResponseData[0].ClaNoteEvent);
            Assert.Equal("Edited", (string)apiHelperObject.ResponseData[1].ClaNoteEvent);
            Assert.Equal("Edited", (string)apiHelperObject.ResponseData[2].ClaNoteEvent);
        }

        /// <summary>
        /// Example of updating Claim_Notes based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateClaim_NotesZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Notes";

            apiHelperObject.APIBody = @$"{{
  ""Claim_NotesObject"": {{
    ""ClaNoteEvent"": ""Edited""
  }},
  ""queryString"": {{
    ""query"": ""$filter=ClaNoteUserName eq 'APIClaim_NotesLastName'""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal("Edited", (string)apiHelperObject.ResponseData[i].ClaNoteEvent);
            }
        }

        /// <summary>
        /// Example of updating 1 Claim_Notes record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateClaim_NotesFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Notes";

            apiHelperObject.APIBody = @$"{{
                  ""Claim_NotesObject"": {{
                    ""ClaNoteEvent"": ""Edited"",
                    ""ClaNoteID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal("Edited", (string)apiHelperObject.ResponseData[0].ClaNoteEvent);
        }
    }
}
