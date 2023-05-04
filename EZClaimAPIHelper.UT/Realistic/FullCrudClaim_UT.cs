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
    public class FullCrudClaim_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudClaim_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Claim endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudClaim()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Claim(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestClaims(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Claim(ref apiHelperObject);

                Thread.Sleep(3000);

                create1ClaimWithChildren(ref apiHelperObject);

                Thread.Sleep(3000);

                create2ClaimsWithChildren(ref apiHelperObject);

                Thread.Sleep(3000);

                selectClaimsWithLastNameAPIClaimLastName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].ClaID;
                int id2 = (int)apiHelperObject.ResponseData[1].ClaID;
                int id3 = (int)apiHelperObject.ResponseData[2].ClaID;

                Thread.Sleep(3000);

                selectClaimsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletClaimFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateClaimListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateClaimZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateClaimFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteClaimFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletClaimFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteClaimFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectClaimsWithLastNameAPIClaimLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Claim record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Claim(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claims";
            apiHelperObject.APIBody = @"{
                    ""ClaPatFID"": ""1"",
                    ""ClaDiagnosis1"":""APIClaimDiagnosis""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 1 Claim record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1ClaimWithChildren(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claims";
            apiHelperObject.APIBody = @"{
                    ""ClaPatFID"": ""1"",
                    ""ClaDiagnosis1"":""APIClaimDiagnosis"",
                        ""claim_InsuredsObjectWithoutID"": [
                            {
                                ""ClaInsFirstName"": ""APIClaim_InsuredFirstName1"",
                                ""ClaInsLastName"": ""APIClaim_InsuredLastName"",
                                ""ClaInsSequence"":""1""
                            }
                        ],
                        ""claim_NotesObjectWithoutID"": [
                            {
                                ""ClaNoteEvent"": ""Created"",
                                ""ClaNoteUserName"":""APIClaim_NotesLastName""
                            }
                        ],
                        ""service_LinesObjectWithoutID"": [
                            {
                                ""SrvFromDate"": ""2023-01-01"",
                                ""SrvToDate"": ""2023-03-07"",
                                ""SrvRespChangeDate"": ""2023-03-07"",
                                ""SrvCustomField1"":""APIService_Line""
                            }
                        ]
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(4, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Claim records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestClaims(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claims/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""ClaPatFID"": ""1"",
                    ""ClaDiagnosis1"":""APIClaimDiagnosis""
                    },
                    {
                    ""ClaPatFID"": ""1"",
                    ""ClaDiagnosis1"":""APIClaimDiagnosis""
                    },
                    {
                    ""ClaPatFID"": ""1"",
                    ""ClaDiagnosis1"":""APIClaimDiagnosis""
                    },
                    {
                    ""ClaPatFID"": ""1"",
                    ""ClaDiagnosis1"":""APIClaimDiagnosis""
                    },
                    {
                    ""ClaPatFID"": ""1"",
                    ""ClaDiagnosis1"":""APIClaimDiagnosis""
                    },
                    {
                    ""ClaPatFID"": ""1"",
                    ""ClaDiagnosis1"":""APIClaimDiagnosis""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Claim records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create2ClaimsWithChildren(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claims/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""ClaPatFID"": ""1"",
                    ""ClaDiagnosis1"":""APIClaimDiagnosis"",
                        ""claim_InsuredsObjectWithoutID"": [
                            {
                                ""ClaInsFirstName"": ""APIClaim_InsuredFirstName1"",
                                ""ClaInsLastName"": ""APIClaim_InsuredLastName"",
                                ""ClaInsSequence"":""1""
                            }
                        ],
                        ""claim_NotesObjectWithoutID"": [
                            {
                                ""ClaNoteEvent"": ""Created"",
                                ""ClaNoteUserName"":""APIClaim_NotesLastName""
                            }
                        ],
                        ""service_LinesObjectWithoutID"": [
                            {
                                ""SrvFromDate"": ""2023-01-01"",
                                ""SrvToDate"": ""2023-03-07"",
                                ""SrvRespChangeDate"": ""2023-03-07"",
                                ""SrvCustomField1"":""APIService_Line""
                            }
                        ],
                    },
                    {
                    ""ClaPatFID"": ""1"",
                    ""ClaDiagnosis1"":""APIClaimDiagnosis"",
                        ""claim_InsuredsObjectWithoutID"": [
                            {
                                ""ClaInsFirstName"": ""APIClaim_InsuredFirstName1"",
                                ""ClaInsLastName"": ""APIClaim_InsuredLastName"",
                                ""ClaInsSequence"":""1""
                            }
                        ],
                        ""claim_NotesObjectWithoutID"": [
                            {
                                ""ClaNoteEvent"": ""Created"",
                                ""ClaNoteUserName"":""APIClaim_NotesLastName1""
                            },
                            {
                                ""ClaNoteEvent"": ""Created"",
                                ""ClaNoteUserName"":""APIClaim_NotesLastName2""
                            }
                        ],
                        ""service_LinesObjectWithoutID"": [
                            {
                                ""SrvFromDate"": ""2023-01-01"",
                                ""SrvToDate"": ""2023-03-07"",
                                ""SrvRespChangeDate"": ""2023-03-07"",
                                ""SrvCustomField1"":""APIService_Line""
                            }
                        ],
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(9, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Claim records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteClaimFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Claims/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Claim records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteClaimFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Claims/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=ClaDiagnosis1 eq \""APIClaimDiagnosis\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Claim based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletClaimFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Claims/{id1}";

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
        /// Select Claims based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectClaimsWithLastNameAPIClaimLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Claims/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=ClaDiagnosis1 eq \""APIClaimDiagnosis\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Claims/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal(@"$filter=ClaDiagnosis1 eq ""APIClaimDiagnosis""", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Claim based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectClaimsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Claim record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Claim(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claims/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Claim records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateClaimListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Claims/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""ClaDiagnosis2"": ""APIClaimDiagnosis2_1"",
                            ""ClaID"": ""{id1}""
                        }},
                        {{
                            ""ClaDiagnosis2"": ""APIClaimDiagnosis2_2"",
                            ""ClaID"": ""{id2}""
                        }},
                        {{
                            ""ClaDiagnosis2"": ""APIClaimDiagnosis2_3"",
                            ""ClaID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal("APIClaimDiagnosis2_1", (string)apiHelperObject.ResponseData[0].ClaDiagnosis2);
            Assert.Equal("APIClaimDiagnosis2_2", (string)apiHelperObject.ResponseData[1].ClaDiagnosis2);
            Assert.Equal("APIClaimDiagnosis2_3", (string)apiHelperObject.ResponseData[2].ClaDiagnosis2);
        }

        /// <summary>
        /// Example of updating Claims based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateClaimZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claims";

            apiHelperObject.APIBody = @$"{{
  ""ClaimsObject"": {{
    ""ClaDiagnosis2"": ""APIClaimDiagnosis2_4""
  }},
  ""queryString"": {{
    ""query"": ""$filter=ClaDiagnosis1 eq \""APIClaimDiagnosis\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal("APIClaimDiagnosis2_4", (string)apiHelperObject.ResponseData[i].ClaDiagnosis2);
            }
        }

        /// <summary>
        /// Example of updating 1 Claim record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateClaimFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Claims";

            apiHelperObject.APIBody = @$"{{
                  ""ClaimsObject"": {{
                    ""ClaDiagnosis2"": ""APIClaimDiagnosis2_5"",
                    ""ClaID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal("APIClaimDiagnosis2_5", (string)apiHelperObject.ResponseData[0].ClaDiagnosis2);
        }
    }
}
