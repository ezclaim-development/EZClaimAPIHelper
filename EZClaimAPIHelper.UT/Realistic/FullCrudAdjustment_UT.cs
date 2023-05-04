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
    public class FullCrudAdjustment_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudAdjustment_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Adjustment endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudAdjustment()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Adjustment(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestAdjustments(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Adjustment(ref apiHelperObject);

                Thread.Sleep(3000);

                selectAdjustmentsWithLastNameAPIAdjustmentLastName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].AdjID;
                int id2 = (int)apiHelperObject.ResponseData[1].AdjID;
                int id3 = (int)apiHelperObject.ResponseData[2].AdjID;

                Thread.Sleep(3000);

                selectAdjustmentsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletAdjustmentFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateAdjustmentListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateAdjustmentZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateAdjustmentFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteAdjustmentFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletAdjustmentFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteAdjustmentFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectAdjustmentsWithLastNameAPIAdjustmentLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Adjustment record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Adjustment(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Adjustments";
            apiHelperObject.APIBody = @"{
                    ""AdjGroupCode"": ""CO"",
                    ""AdjSrvGUID"":""8868CE1F-3971-43D4-B803-0089903B41FA"",
                    ""AdjSrvFID"":""79"",
                    ""AdjOtherReference1"":""APIAdjustment"",
                    ""AdjNote"":""APIAdjustmentNote7""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Adjustment records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestAdjustments(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Adjustments/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""AdjGroupCode"": ""CO"",
                    ""AdjSrvGUID"":""8868CE1F-3971-43D4-B803-0089903B41FA"",
                    ""AdjSrvFID"":""79"",
                    ""AdjOtherReference1"":""APIAdjustment"",
                    ""AdjNote"":""APIAdjustmentNote1""
                    },
                    {
                    ""AdjGroupCode"": ""CO"",
                    ""AdjSrvGUID"":""8868CE1F-3971-43D4-B803-0089903B41FA"",
                    ""AdjSrvFID"":""79"",
                    ""AdjOtherReference1"":""APIAdjustment"",
                    ""AdjNote"":""APIAdjustmentNote2""
                    },
                    {
                    ""AdjGroupCode"": ""CO"",
                    ""AdjSrvGUID"":""8868CE1F-3971-43D4-B803-0089903B41FA"",
                    ""AdjSrvFID"":""79"",
                    ""AdjOtherReference1"":""APIAdjustment"",
                    ""AdjNote"":""APIAdjustmentNote3""
                    },
                    {
                    ""AdjGroupCode"": ""CO"",
                    ""AdjSrvGUID"":""8868CE1F-3971-43D4-B803-0089903B41FA"",
                    ""AdjSrvFID"":""79"",
                    ""AdjOtherReference1"":""APIAdjustment"",
                    ""AdjNote"":""APIAdjustmentNote4""
                    },
                    {
                    ""AdjGroupCode"": ""CO"",
                    ""AdjSrvGUID"":""8868CE1F-3971-43D4-B803-0089903B41FA"",
                    ""AdjSrvFID"":""79"",
                    ""AdjOtherReference1"":""APIAdjustment"",
                    ""AdjNote"":""APIAdjustmentNote5""
                    },
                    {
                    ""AdjGroupCode"": ""CO"",
                    ""AdjSrvGUID"":""8868CE1F-3971-43D4-B803-0089903B41FA"",
                    ""AdjSrvFID"":""79"",
                    ""AdjOtherReference1"":""APIAdjustment"",
                    ""AdjNote"":""APIAdjustmentNote6""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Adjustment records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteAdjustmentFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Adjustments/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Adjustment records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteAdjustmentFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Adjustments/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=AdjOtherReference1 eq \""APIAdjustment\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Adjustment based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletAdjustmentFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Adjustments/{id1}";

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
        /// Select Adjustments based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectAdjustmentsWithLastNameAPIAdjustmentLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Adjustments/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=AdjOtherReference1 eq \""APIAdjustment\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Adjustments/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal(@"$filter=AdjOtherReference1 eq ""APIAdjustment""", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Adjustment based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectAdjustmentsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Adjustment record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Adjustment(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Adjustments/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Adjustment records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateAdjustmentListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Adjustments/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""AdjNote"": ""Note1"",
                            ""AdjID"": ""{id1}""
                        }},
                        {{
                            ""AdjNote"": ""Note2"",
                            ""AdjID"": ""{id2}""
                        }},
                        {{
                            ""AdjNote"": ""Note3"",
                            ""AdjID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal("Note1", (string)apiHelperObject.ResponseData[0].AdjNote);
            Assert.Equal("Note2", (string)apiHelperObject.ResponseData[1].AdjNote);
            Assert.Equal("Note3", (string)apiHelperObject.ResponseData[2].AdjNote);
        }

        /// <summary>
        /// Example of updating Adjustments based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateAdjustmentZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Adjustments";

            apiHelperObject.APIBody = @$"{{
  ""AdjustmentsObject"": {{
    ""AdjNote"": ""Note""
  }},
  ""queryString"": {{
    ""query"": ""$filter=AdjOtherReference1 eq \""APIAdjustment\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal("Note", (string)apiHelperObject.ResponseData[i].AdjNote);
            }
        }

        /// <summary>
        /// Example of updating 1 Adjustment record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateAdjustmentFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Adjustments";

            apiHelperObject.APIBody = @$"{{
                  ""AdjustmentsObject"": {{
                    ""AdjNote"": ""Note"",
                    ""AdjID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal("Note", (string)apiHelperObject.ResponseData[0].AdjNote);
        }
    }
}
