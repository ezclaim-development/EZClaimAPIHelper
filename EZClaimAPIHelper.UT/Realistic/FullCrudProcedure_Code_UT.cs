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
    public class FullCrudProcedure_Code_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudProcedure_Code_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Procedure_Code endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudProcedure_Code()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Procedure_Code(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestProcedure_Codes(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Procedure_Code(ref apiHelperObject);

                Thread.Sleep(3000);

                selectProcedure_CodesWithLastNameAPIProcedure_CodeLastName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].ProcID;
                int id2 = (int)apiHelperObject.ResponseData[1].ProcID;
                int id3 = (int)apiHelperObject.ResponseData[2].ProcID;

                Thread.Sleep(3000);

                selectProcedure_CodesFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletProcedure_CodeFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateProcedure_CodeListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateProcedure_CodeZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateProcedure_CodeFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteProcedure_CodeFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletProcedure_CodeFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteProcedure_CodeFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectProcedure_CodesWithLastNameAPIProcedure_CodeLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Procedure_Code record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Procedure_Code(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Procedure_Codes";
            apiHelperObject.APIBody = @"{
                    ""ProcCode"": ""APIProcedure_Code7"",
                    ""ProcDescription"":""APIProcedure_Code""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Procedure_Code records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestProcedure_Codes(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Procedure_Codes/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""ProcCode"": ""APIProcedure_Code1"",
                    ""ProcDescription"":""APIProcedure_Code""
                    },
                    {
                    ""ProcCode"": ""APIProcedure_Code2"",
                    ""ProcDescription"":""APIProcedure_Code""
                    },
                    {
                    ""ProcCode"": ""APIProcedure_Code3"",
                    ""ProcDescription"":""APIProcedure_Code""
                    },
                    {
                    ""ProcCode"": ""APIProcedure_Code4"",
                    ""ProcDescription"":""APIProcedure_Code""
                    },
                    {
                    ""ProcCode"": ""APIProcedure_Code5"",
                    ""ProcDescription"":""APIProcedure_Code""
                    },
                    {
                    ""ProcCode"": ""APIProcedure_Code6"",
                    ""ProcDescription"":""APIProcedure_Code""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Procedure_Code records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteProcedure_CodeFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Procedure_Codes/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Procedure_Code records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteProcedure_CodeFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Procedure_Codes/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=ProcDescription eq 'APIProcedure_Code'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Procedure_Code based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletProcedure_CodeFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Procedure_Codes/{id1}";

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
        /// Select Procedure_Codes based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectProcedure_CodesWithLastNameAPIProcedure_CodeLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Procedure_Codes/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=ProcDescription eq 'APIProcedure_Code'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Procedure_Codes/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal("$filter=ProcDescription eq 'APIProcedure_Code'", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Procedure_Code based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectProcedure_CodesFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Procedure_Code record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Procedure_Code(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Procedure_Codes/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Procedure_Code records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateProcedure_CodeListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Procedure_Codes/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""ProcCharge"": ""51"",
                            ""ProcID"": ""{id1}""
                        }},
                        {{
                            ""ProcCharge"": ""52"",
                            ""ProcID"": ""{id2}""
                        }},
                        {{
                            ""ProcCharge"": ""53"",
                            ""ProcID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal(51, (int)apiHelperObject.ResponseData[0].ProcCharge);
            Assert.Equal(52, (int)apiHelperObject.ResponseData[1].ProcCharge);
            Assert.Equal(53, (int)apiHelperObject.ResponseData[2].ProcCharge);
        }

        /// <summary>
        /// Example of updating Procedure_Codes based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateProcedure_CodeZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Procedure_Codes";

            apiHelperObject.APIBody = @$"{{
  ""Procedure_CodesObject"": {{
    ""ProcCharge"": ""54""
  }},
  ""queryString"": {{
    ""query"": ""$filter=ProcDescription eq 'APIProcedure_Code'""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(54, (int)apiHelperObject.ResponseData[i].ProcCharge);
            }
        }

        /// <summary>
        /// Example of updating 1 Procedure_Code record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateProcedure_CodeFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Procedure_Codes";

            apiHelperObject.APIBody = @$"{{
                  ""Procedure_CodesObject"": {{
                    ""ProcCharge"": ""55"",
                    ""ProcID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal(55, (int)apiHelperObject.ResponseData[0].ProcCharge);
        }
    }
}
