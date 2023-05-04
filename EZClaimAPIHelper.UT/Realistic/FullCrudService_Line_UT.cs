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
    public class FullCrudService_Line_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudService_Line_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Service_Line endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudService_Line()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Service_Line(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestService_Lines(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Service_Line(ref apiHelperObject);

                Thread.Sleep(3000);

                selectService_LinesWithLastNameAPIService_Line(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].SrvID;
                int id2 = (int)apiHelperObject.ResponseData[1].SrvID;
                int id3 = (int)apiHelperObject.ResponseData[2].SrvID;

                Thread.Sleep(3000);

                selectService_LinesFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletService_LineFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateService_LineListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateService_LineZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateService_LineFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteService_LineFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletService_LineFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteService_LineFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectService_LinesWithLastNameAPIService_Line(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Service_Line record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Service_Line(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Service_Lines";
            apiHelperObject.APIBody = @"{
                    ""SrvClaFID"": ""1"",
                    ""SrvFromDate"": ""2023-01-01"",
                    ""SrvToDate"": ""2023-03-07"",
                    ""SrvRespChangeDate"": ""2023-03-07"",
                    ""SrvCustomField1"":""APIService_Line""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Service_Line records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestService_Lines(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Service_Lines/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""SrvClaFID"": ""1"",
                    ""SrvFromDate"": ""2023-01-01"",
                    ""SrvToDate"": ""2023-03-07"",
                    ""SrvRespChangeDate"": ""2023-03-07"",
                    ""SrvCustomField1"":""APIService_Line""
                    },
                    {
                    ""SrvClaFID"": ""1"",
                    ""SrvFromDate"": ""2023-01-01"",
                    ""SrvToDate"": ""2023-03-07"",
                    ""SrvRespChangeDate"": ""2023-03-07"",
                    ""SrvCustomField1"":""APIService_Line""
                    },
                    {
                    ""SrvClaFID"": ""1"",
                    ""SrvFromDate"": ""2023-01-01"",
                    ""SrvToDate"": ""2023-03-07"",
                    ""SrvRespChangeDate"": ""2023-03-07"",
                    ""SrvCustomField1"":""APIService_Line""
                    },
                    {
                    ""SrvClaFID"": ""1"",
                    ""SrvFromDate"": ""2023-01-01"",
                    ""SrvToDate"": ""2023-03-07"",
                    ""SrvRespChangeDate"": ""2023-03-07"",
                    ""SrvCustomField1"":""APIService_Line""
                    },
                    {
                    ""SrvClaFID"": ""1"",
                    ""SrvFromDate"": ""2023-01-01"",
                    ""SrvToDate"": ""2023-03-07"",
                    ""SrvRespChangeDate"": ""2023-03-07"",
                    ""SrvCustomField1"":""APIService_Line""
                    },
                    {
                    ""SrvClaFID"": ""1"",
                    ""SrvFromDate"": ""2023-01-01"",
                    ""SrvToDate"": ""2023-03-07"",
                    ""SrvRespChangeDate"": ""2023-03-07"",
                    ""SrvCustomField1"":""APIService_Line""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Service_Line records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteService_LineFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Service_Lines/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Service_Line records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteService_LineFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Service_Lines/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=SrvCustomField1 eq \""APIService_Line\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Service_Line based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletService_LineFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Service_Lines/{id1}";

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
        /// Select Service_Lines based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectService_LinesWithLastNameAPIService_Line(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Service_Lines/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=SrvCustomField1 eq \""APIService_Line\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Service_Lines/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal(@"$filter=SrvCustomField1 eq ""APIService_Line""", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Service_Line based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectService_LinesFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Service_Line record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Service_Line(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Service_Lines/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Service_Line records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateService_LineListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Service_Lines/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""SrvCharges"": ""51"",
                            ""SrvID"": ""{id1}""
                        }},
                        {{
                            ""SrvCharges"": ""52"",
                            ""SrvID"": ""{id2}""
                        }},
                        {{
                            ""SrvCharges"": ""53"",
                            ""SrvID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal(51, (int)apiHelperObject.ResponseData[0].SrvCharges);
            Assert.Equal(52, (int)apiHelperObject.ResponseData[1].SrvCharges);
            Assert.Equal(53, (int)apiHelperObject.ResponseData[2].SrvCharges);
        }

        /// <summary>
        /// Example of updating Service_Lines based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateService_LineZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Service_Lines";

            apiHelperObject.APIBody = @$"{{
  ""Service_LinesObject"": {{
    ""SrvCharges"": ""54""
  }},
  ""queryString"": {{
    ""query"": ""$filter=SrvCustomField1 eq \""APIService_Line\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(54, (int)apiHelperObject.ResponseData[i].SrvCharges);
            }
        }

        /// <summary>
        /// Example of updating 1 Service_Line record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateService_LineFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Service_Lines";

            apiHelperObject.APIBody = @$"{{
                  ""Service_LinesObject"": {{
                    ""SrvCharges"": ""55"",
                    ""SrvID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal(55, (int)apiHelperObject.ResponseData[0].SrvCharges);
        }
    }
}
