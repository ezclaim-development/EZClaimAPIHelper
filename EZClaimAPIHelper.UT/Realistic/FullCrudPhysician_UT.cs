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
    public class FullCrudPhysician_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudPhysician_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Physician endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudPhysician()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Physician(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestPhysicians(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Physician(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPhysicianSimpleList(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPhysicianSimpleListPage(ref apiHelperObject, 1);

                Thread.Sleep(3000);

                selectPhysiciansWithLastNameAPIPhysicianLastName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].PhyID;
                int id2 = (int)apiHelperObject.ResponseData[1].PhyID;
                int id3 = (int)apiHelperObject.ResponseData[2].PhyID;

                Thread.Sleep(3000);

                selectPhysiciansFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletPhysicianFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updatePhysicianListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updatePhysicianZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updatePhysicianFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deletePhysicianFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletPhysicianFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deletePhysicianFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPhysiciansWithLastNameAPIPhysicianLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Physician record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Physician(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Physicians";
            apiHelperObject.APIBody = @"{
                    ""PhyFirstName"": ""APIPhysicianFirstName7"",
                    ""PhyLastName"":""APIPhysicianLastName""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Physician records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestPhysicians(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Physicians/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""PhyFirstName"": ""APIPhysicianFirstName1"",
                    ""PhyLastName"":""APIPhysicianLastName""
                    },
                    {
                    ""PhyFirstName"": ""APIPhysicianFirstName2"",
                    ""PhyLastName"":""APIPhysicianLastName""
                    },
                    {
                    ""PhyFirstName"": ""APIPhysicianFirstName3"",
                    ""PhyLastName"":""APIPhysicianLastName""
                    },
                    {
                    ""PhyFirstName"": ""APIPhysicianFirstName4"",
                    ""PhyLastName"":""APIPhysicianLastName""
                    },
                    {
                    ""PhyFirstName"": ""APIPhysicianFirstName5"",
                    ""PhyLastName"":""APIPhysicianLastName""
                    },
                    {
                    ""PhyFirstName"": ""APIPhysicianFirstName6"",
                    ""PhyLastName"":""APIPhysicianLastName""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Physician records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deletePhysicianFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Physicians/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Physician records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePhysicianFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Physicians/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PhyLastName eq \""APIPhysicianLastName\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Physician based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletPhysicianFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Physicians/{id1}";

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
        /// Select Physicians based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectPhysiciansWithLastNameAPIPhysicianLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Physicians/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PhyLastName eq \""APIPhysicianLastName\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Physicians/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal(@"$filter=PhyLastName eq ""APIPhysicianLastName""", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Physician based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPhysiciansFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Select simple full list of Physicians
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPhysicianSimpleList(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Physicians/GetSimpleList";

            apiHelperObject.APIBody = @"";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);
            Assert.True(apiHelperObject.ResponseData.Count > 5);
        }

        /// <summary>
        /// Select simple full list of Physicians based on page number
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="page"></param>
        private void selectPhysicianSimpleListPage(ref APIUnitTestHelperObject apiHelperObject, int page)
        {
            apiHelperObject.Endpoint = $"/api/v2/Physicians/GetSimpleList/page/{page}";

            apiHelperObject.APIBody = @"";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);
            Assert.True(apiHelperObject.ResponseData.Count > 5);
        }

        /// <summary>
        /// Example of selecting the first Physician record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Physician(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Physicians/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Physician records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updatePhysicianListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Physicians/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""PhyZip"": ""55551"",
                            ""PhyID"": ""{id1}""
                        }},
                        {{
                            ""PhyZip"": ""55552"",
                            ""PhyID"": ""{id2}""
                        }},
                        {{
                            ""PhyZip"": ""55553"",
                            ""PhyID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal(55551, (int)apiHelperObject.ResponseData[0].PhyZip);
            Assert.Equal(55552, (int)apiHelperObject.ResponseData[1].PhyZip);
            Assert.Equal(55553, (int)apiHelperObject.ResponseData[2].PhyZip);
        }

        /// <summary>
        /// Example of updating Physicians based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updatePhysicianZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Physicians";

            apiHelperObject.APIBody = @$"{{
  ""PhysiciansObject"": {{
    ""PhyZip"": ""55555""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PhyLastName eq \""APIPhysicianLastName\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(55555, (int)apiHelperObject.ResponseData[i].PhyZip);
            }
        }

        /// <summary>
        /// Example of updating 1 Physician record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updatePhysicianFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Physicians";

            apiHelperObject.APIBody = @$"{{
                  ""PhysiciansObject"": {{
                    ""PhyZip"": ""55551"",
                    ""PhyID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal(55551, (int)apiHelperObject.ResponseData[0].PhyZip);
        }
    }
}
