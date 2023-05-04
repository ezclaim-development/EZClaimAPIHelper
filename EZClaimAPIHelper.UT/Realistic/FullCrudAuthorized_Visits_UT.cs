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
    public class FullCrudAuthorized_Visits_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudAuthorized_Visits_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Authorized_Visits endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudAuthorized_Visits()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Authorized_Visits(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestAuthorized_Visits(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Authorized_Visits(ref apiHelperObject);

                Thread.Sleep(3000);

                selectAuthorized_VisitsWithLastNameAPIAuthorized_VisitsLastName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].AuthVisID;
                int id2 = (int)apiHelperObject.ResponseData[1].AuthVisID;
                int id3 = (int)apiHelperObject.ResponseData[2].AuthVisID;

                Thread.Sleep(3000);

                selectAuthorized_VisitsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletAuthorized_VisitsFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateAuthorized_VisitsListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateAuthorized_VisitsZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateAuthorized_VisitsFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteAuthorized_VisitsFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletAuthorized_VisitsFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteAuthorized_VisitsFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectAuthorized_VisitsWithLastNameAPIAuthorized_VisitsLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Authorized_Visits record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Authorized_Visits(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Visits";
            apiHelperObject.APIBody = @"{
                    ""AuthVisPatFID"": ""1"",
                    ""AuthVisNumber"":""APIAuthorized_Visits"",
                    ""AuthVisNotes"":""note7"",
                    ""AuthVisStart"":""2023-04-24""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Authorized_Visits records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestAuthorized_Visits(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Visits/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""AuthVisPatFID"": ""1"",
                    ""AuthVisNumber"":""APIAuthorized_Visits"",
                    ""AuthVisNotes"":""note1"",
                    ""AuthVisStart"":""2023-04-24""
                    },
                    {
                    ""AuthVisPatFID"": ""1"",
                    ""AuthVisNumber"":""APIAuthorized_Visits"",
                    ""AuthVisNotes"":""note2"",
                    ""AuthVisStart"":""2023-04-24""
                    },
                    {
                    ""AuthVisPatFID"": ""1"",
                    ""AuthVisNumber"":""APIAuthorized_Visits"",
                    ""AuthVisNotes"":""note3"",
                    ""AuthVisStart"":""2023-04-24""
                    },
                    {
                    ""AuthVisPatFID"": ""1"",
                    ""AuthVisNumber"":""APIAuthorized_Visits"",
                    ""AuthVisNotes"":""note4"",
                    ""AuthVisStart"":""2023-04-24""
                    },
                    {
                    ""AuthVisPatFID"": ""1"",
                    ""AuthVisNumber"":""APIAuthorized_Visits"",
                    ""AuthVisNotes"":""note5"",
                    ""AuthVisStart"":""2023-04-24""
                    },
                    {
                    ""AuthVisPatFID"": ""1"",
                    ""AuthVisNumber"":""APIAuthorized_Visits"",
                    ""AuthVisNotes"":""note6"",
                    ""AuthVisStart"":""2023-04-24""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Authorized_Visits records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteAuthorized_VisitsFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Visits/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Authorized_Visits records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteAuthorized_VisitsFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Authorized_Visits/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=AuthVisNumber eq \""APIAuthorized_Visits\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Authorized_Visits based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletAuthorized_VisitsFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Authorized_Visits/{id1}";

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
        /// Select Authorized_Visits based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectAuthorized_VisitsWithLastNameAPIAuthorized_VisitsLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Visits/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=AuthVisNumber eq \""APIAuthorized_Visits\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Authorized_Visits/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal(@"$filter=AuthVisNumber eq ""APIAuthorized_Visits""", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Authorized_Visits based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectAuthorized_VisitsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Authorized_Visits record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Authorized_Visits(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Visits/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Authorized_Visits records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateAuthorized_VisitsListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Visits/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""AuthVisNotes"": ""E1"",
                            ""AuthVisID"": ""{id1}""
                        }},
                        {{
                            ""AuthVisNotes"": ""E2"",
                            ""AuthVisID"": ""{id2}""
                        }},
                        {{
                            ""AuthVisNotes"": ""E3"",
                            ""AuthVisID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal("E1", (string)apiHelperObject.ResponseData[0].AuthVisNotes);
            Assert.Equal("E2", (string)apiHelperObject.ResponseData[1].AuthVisNotes);
            Assert.Equal("E3", (string)apiHelperObject.ResponseData[2].AuthVisNotes);
        }

        /// <summary>
        /// Example of updating Authorized_Visits based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateAuthorized_VisitsZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Visits";

            apiHelperObject.APIBody = @$"{{
  ""Authorized_VisitsObject"": {{
    ""AuthVisNotes"": ""EE""
  }},
  ""queryString"": {{
    ""query"": ""$filter=AuthVisNumber eq \""APIAuthorized_Visits\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal("EE", (string)apiHelperObject.ResponseData[i].AuthVisNotes);
            }
        }

        /// <summary>
        /// Example of updating 1 Authorized_Visits record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateAuthorized_VisitsFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Visits";

            apiHelperObject.APIBody = @$"{{
                  ""Authorized_VisitsObject"": {{
                    ""AuthVisNotes"": ""EI"",
                    ""AuthVisID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal("EI", (string)apiHelperObject.ResponseData[0].AuthVisNotes);
        }
    }
}
