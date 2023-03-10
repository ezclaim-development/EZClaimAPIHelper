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
    public class FullCrudPayment_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudPayment_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Payment endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudPayment()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Payment(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestPayments(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Payment(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPaymentsWithLastNameAPIPayment(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].PmtID;
                int id2 = (int)apiHelperObject.ResponseData[1].PmtID;
                int id3 = (int)apiHelperObject.ResponseData[2].PmtID;

                Thread.Sleep(3000);

                selectPaymentsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletPaymentFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updatePaymentListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updatePaymentZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updatePaymentFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deletePaymentFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletPaymentFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deletePaymentFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPaymentsWithLastNameAPIPayment(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Payment record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Payment(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payments";
            apiHelperObject.APIBody = @"{
                    ""PmtPatFID"": ""1"",
                    ""PmtDate"": ""2023-03-07"",
                    ""PmtNameOnCard"":""APIPayment""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Payment records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestPayments(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payments/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""PmtPatFID"": ""1"",
                    ""PmtDate"": ""2023-03-07"",
                    ""PmtNameOnCard"":""APIPayment""
                    },
                    {
                    ""PmtPatFID"": ""1"",
                    ""PmtDate"": ""2023-03-07"",
                    ""PmtNameOnCard"":""APIPayment""
                    },
                    {
                    ""PmtPatFID"": ""1"",
                    ""PmtDate"": ""2023-03-07"",
                    ""PmtNameOnCard"":""APIPayment""
                    },
                    {
                    ""PmtPatFID"": ""1"",
                    ""PmtDate"": ""2023-03-07"",
                    ""PmtNameOnCard"":""APIPayment""
                    },
                    {
                    ""PmtPatFID"": ""1"",
                    ""PmtDate"": ""2023-03-07"",
                    ""PmtNameOnCard"":""APIPayment""
                    },
                    {
                    ""PmtPatFID"": ""1"",
                    ""PmtDate"": ""2023-03-07"",
                    ""PmtNameOnCard"":""APIPayment""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Payment records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deletePaymentFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Payments/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Payment records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePaymentFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Payments/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PmtNameOnCard eq 'APIPayment'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Payment based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletPaymentFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Payments/{id1}";

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
        /// Select Payments based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectPaymentsWithLastNameAPIPayment(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Payments/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PmtNameOnCard eq 'APIPayment'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Payments/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal("$filter=PmtNameOnCard eq 'APIPayment'", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Payment based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPaymentsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Payment record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Payment(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payments/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Payment records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updatePaymentListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Payments/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""PmtCardEntryMethod"": ""1"",
                            ""PmtID"": ""{id1}""
                        }},
                        {{
                            ""PmtCardEntryMethod"": ""2"",
                            ""PmtID"": ""{id2}""
                        }},
                        {{
                            ""PmtCardEntryMethod"": ""3"",
                            ""PmtID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal(1, (int)apiHelperObject.ResponseData[0].PmtCardEntryMethod);
            Assert.Equal(2, (int)apiHelperObject.ResponseData[1].PmtCardEntryMethod);
            Assert.Equal(3, (int)apiHelperObject.ResponseData[2].PmtCardEntryMethod);
        }

        /// <summary>
        /// Example of updating Payments based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updatePaymentZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Payments";

            apiHelperObject.APIBody = @$"{{
  ""PaymentsObject"": {{
    ""PmtCardEntryMethod"": ""4""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PmtNameOnCard eq 'APIPayment'""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(4, (int)apiHelperObject.ResponseData[i].PmtCardEntryMethod);
            }
        }

        /// <summary>
        /// Example of updating 1 Payment record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updatePaymentFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Payments";

            apiHelperObject.APIBody = @$"{{
                  ""PaymentsObject"": {{
                    ""PmtCardEntryMethod"": ""5"",
                    ""PmtID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal(5, (int)apiHelperObject.ResponseData[0].PmtCardEntryMethod);
        }
    }
}
