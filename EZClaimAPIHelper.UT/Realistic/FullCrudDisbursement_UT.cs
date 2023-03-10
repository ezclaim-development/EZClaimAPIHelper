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
    public class FullCrudDisbursement_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudDisbursement_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Disbursement endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudDisbursement()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Disbursement(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestDisbursements(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Disbursement(ref apiHelperObject);

                Thread.Sleep(3000);

                selectDisbursementsWithLastNameAPIDisbursementLastName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].DisbID;
                int id2 = (int)apiHelperObject.ResponseData[1].DisbID;
                int id3 = (int)apiHelperObject.ResponseData[2].DisbID;

                Thread.Sleep(3000);

                selectDisbursementsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletDisbursementFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateDisbursementListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateDisbursementZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateDisbursementFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteDisbursementFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletDisbursementFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteDisbursementFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectDisbursementsWithLastNameAPIDisbursementLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Disbursement record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Disbursement(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Disbursements";
            apiHelperObject.APIBody = @"{
                    ""DisbSrvFID"": ""1"",
                    ""DisbPmtFID"": ""215"",
                    ""DisbNote"": ""APIDisbursement"",
                    ""DisbSrvGUID"":""5E103159-D671-49DE-AB9A-32EC014A1145""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Disbursement records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestDisbursements(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Disbursements/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""DisbSrvFID"": ""1"",
                    ""DisbPmtFID"": ""215"",
                    ""DisbNote"": ""APIDisbursement"",
                    ""DisbSrvGUID"":""5E103159-D671-49DE-AB9A-32EC014A1145""
                    },
                    {
                    ""DisbSrvFID"": ""1"",
                    ""DisbPmtFID"": ""215"",
                    ""DisbNote"": ""APIDisbursement"",
                    ""DisbSrvGUID"":""5E103159-D671-49DE-AB9A-32EC014A1145""
                    },
                    {
                    ""DisbSrvFID"": ""1"",
                    ""DisbPmtFID"": ""215"",
                    ""DisbNote"": ""APIDisbursement"",
                    ""DisbSrvGUID"":""5E103159-D671-49DE-AB9A-32EC014A1145""
                    },
                    {
                    ""DisbSrvFID"": ""1"",
                    ""DisbPmtFID"": ""215"",
                    ""DisbNote"": ""APIDisbursement"",
                    ""DisbSrvGUID"":""5E103159-D671-49DE-AB9A-32EC014A1145""
                    },
                    {
                    ""DisbSrvFID"": ""1"",
                    ""DisbPmtFID"": ""215"",
                    ""DisbNote"": ""APIDisbursement"",
                    ""DisbSrvGUID"":""5E103159-D671-49DE-AB9A-32EC014A1145""
                    },
                    {
                    ""DisbSrvFID"": ""1"",
                    ""DisbPmtFID"": ""215"",
                    ""DisbNote"": ""APIDisbursement"",
                    ""DisbSrvGUID"":""5E103159-D671-49DE-AB9A-32EC014A1145""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Disbursement records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteDisbursementFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Disbursements/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Disbursement records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteDisbursementFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Disbursements/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=DisbNote eq 'APIDisbursement'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Disbursement based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletDisbursementFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Disbursements/{id1}";

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
        /// Select Disbursements based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectDisbursementsWithLastNameAPIDisbursementLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Disbursements/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=DisbNote eq 'APIDisbursement'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Disbursements/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal("$filter=DisbNote eq 'APIDisbursement'", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Disbursement based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectDisbursementsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Disbursement record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Disbursement(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Disbursements/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Disbursement records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateDisbursementListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Disbursements/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""DisbCode"": ""1"",
                            ""DisbID"": ""{id1}""
                        }},
                        {{
                            ""DisbCode"": ""2"",
                            ""DisbID"": ""{id2}""
                        }},
                        {{
                            ""DisbCode"": ""3"",
                            ""DisbID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal(1, (int)apiHelperObject.ResponseData[0].DisbCode);
            Assert.Equal(2, (int)apiHelperObject.ResponseData[1].DisbCode);
            Assert.Equal(3, (int)apiHelperObject.ResponseData[2].DisbCode);
        }

        /// <summary>
        /// Example of updating Disbursements based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateDisbursementZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Disbursements";

            apiHelperObject.APIBody = @$"{{
  ""DisbursementsObject"": {{
    ""DisbCode"": ""4""
  }},
  ""queryString"": {{
    ""query"": ""$filter=DisbNote eq 'APIDisbursement'""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(4, (int)apiHelperObject.ResponseData[i].DisbCode);
            }
        }

        /// <summary>
        /// Example of updating 1 Disbursement record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateDisbursementFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Disbursements";

            apiHelperObject.APIBody = @$"{{
                  ""DisbursementsObject"": {{
                    ""DisbCode"": ""5"",
                    ""DisbID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal(5, (int)apiHelperObject.ResponseData[0].DisbCode);
        }
    }
}
