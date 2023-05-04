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
    public class FullCrudPayer_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudPayer_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Payer endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudPayer()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Payer(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestPayers(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Payer(ref apiHelperObject);

                Thread.Sleep(3000);

                create1PayerWithChildren(ref apiHelperObject);

                Thread.Sleep(3000);

                create2PayersWithChildren(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPayerSimpleList(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPayerSimpleListPage(ref apiHelperObject, 1);

                Thread.Sleep(3000);

                selectPayersWithLastNameAPIPayer(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].PayID;
                int id2 = (int)apiHelperObject.ResponseData[1].PayID;
                int id3 = (int)apiHelperObject.ResponseData[2].PayID;

                Thread.Sleep(3000);

                selectPayersFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletPayerFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updatePayerListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updatePayerZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updatePayerFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deletePayerFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletPayerFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deletePayerFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPayersWithLastNameAPIPayer(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Payer record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Payer(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers";
            apiHelperObject.APIBody = @"{
                    ""PayName"": ""APIPayer""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 1 Payer record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1PayerWithChildren(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers";
            apiHelperObject.APIBody = @"{
                    ""PayName"": ""APIPayer"",
                    ""paymentsObjectWithoutID"": [
                        {
                            ""PmtDate"": ""2023-03-07"",
                            ""PmtNameOnCard"":""APIPayment""
                        }
                    ]
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(2, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Payer records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestPayers(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""PayName"": ""APIPayer""
                    },
                    {
                    ""PayName"": ""APIPayer""
                    },
                    {
                    ""PayName"": ""APIPayer""
                    },
                    {
                    ""PayName"": ""APIPayer""
                    },
                    {
                    ""PayName"": ""APIPayer""
                    },
                    {
                    ""PayName"": ""APIPayer""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Payer records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create2PayersWithChildren(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""PayName"": ""APIPayer"",
                    ""paymentsObjectWithoutID"": [
                        {
                            ""PmtDate"": ""2023-03-07"",
                            ""PmtNameOnCard"":""APIPayment1""
                        }
                    ]
                    },
                    {
                    ""PayName"": ""APIPayer"",
                    ""paymentsObjectWithoutID"": [
                        {
                            ""PmtDate"": ""2023-03-07"",
                            ""PmtNameOnCard"":""APIPayment2""
                        }
                    ]
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(4, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Payer records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deletePayerFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Payer records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePayerFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Payers/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PayName eq \""APIPayer\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Payer based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletPayerFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Payers/{id1}";

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
        /// Select Payers based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectPayersWithLastNameAPIPayer(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PayName eq \""APIPayer\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Payers/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal(@"$filter=PayName eq ""APIPayer""", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Payer based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPayersFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Select simple full list of Payers
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPayerSimpleList(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers/GetSimpleList";

            apiHelperObject.APIBody = @"";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);
            Assert.True(apiHelperObject.ResponseData.Count > 5);
        }

        /// <summary>
        /// Select simple full list of Payers based on page number
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="page"></param>
        private void selectPayerSimpleListPage(ref APIUnitTestHelperObject apiHelperObject, int page)
        {
            apiHelperObject.Endpoint = $"/api/v2/Payers/GetSimpleList/page/{page}";

            apiHelperObject.APIBody = @"";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);
            Assert.True(apiHelperObject.ResponseData.Count > 5);
        }

        /// <summary>
        /// Example of selecting the first Payer record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Payer(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Payer records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updatePayerListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""PayZip"": ""55551"",
                            ""PayID"": ""{id1}""
                        }},
                        {{
                            ""PayZip"": ""55552"",
                            ""PayID"": ""{id2}""
                        }},
                        {{
                            ""PayZip"": ""55553"",
                            ""PayID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal(55551, (int)apiHelperObject.ResponseData[0].PayZip);
            Assert.Equal(55552, (int)apiHelperObject.ResponseData[1].PayZip);
            Assert.Equal(55553, (int)apiHelperObject.ResponseData[2].PayZip);
        }

        /// <summary>
        /// Example of updating Payers based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updatePayerZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers";

            apiHelperObject.APIBody = @$"{{
  ""PayersObject"": {{
    ""PayZip"": ""55555""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PayName eq \""APIPayer\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(55555, (int)apiHelperObject.ResponseData[i].PayZip);
            }
        }

        /// <summary>
        /// Example of updating 1 Payer record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updatePayerFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Payers";

            apiHelperObject.APIBody = @$"{{
                  ""PayersObject"": {{
                    ""PayZip"": ""55551"",
                    ""PayID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal(55551, (int)apiHelperObject.ResponseData[0].PayZip);
        }
    }
}
