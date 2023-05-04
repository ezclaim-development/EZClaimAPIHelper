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
    public class FullCrudClaim_Insured_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudClaim_Insured_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Claim_Insured endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudClaim_Insured()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Claim_Insured(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestClaim_Insureds(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Claim_Insured(ref apiHelperObject);

                Thread.Sleep(3000);

                selectClaim_InsuredsWithLastNameAPIClaim_InsuredLastName(ref apiHelperObject, true);

                Guid id1 = (Guid)apiHelperObject.ResponseData[0].ClaInsGUID;
                Guid id2 = (Guid)apiHelperObject.ResponseData[1].ClaInsGUID;
                Guid id3 = (Guid)apiHelperObject.ResponseData[2].ClaInsGUID;

                Thread.Sleep(3000);

                selectClaim_InsuredsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletClaim_InsuredFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateClaim_InsuredListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateClaim_InsuredZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateClaim_InsuredFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteClaim_InsuredFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletClaim_InsuredFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteClaim_InsuredFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectClaim_InsuredsWithLastNameAPIClaim_InsuredLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Claim_Insured record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Claim_Insured(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Insureds";
            apiHelperObject.APIBody = @"{
                    ""ClaInsFirstName"": ""APIClaim_InsuredFirstName1"",
                    ""ClaInsLastName"": ""APIClaim_InsuredLastName"",
                    ""ClaInsClaFID"": ""1"",
                    ""ClaInsSequence"":""1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Claim_Insured records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestClaim_Insureds(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Insureds/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""ClaInsFirstName"": ""APIClaim_InsuredFirstName2"",
                    ""ClaInsLastName"": ""APIClaim_InsuredLastName"",
                    ""ClaInsClaFID"": ""1"",
                    ""ClaInsSequence"":""2""
                    },
                    {
                    ""ClaInsFirstName"": ""APIClaim_InsuredFirstName3"",
                    ""ClaInsLastName"": ""APIClaim_InsuredLastName"",
                    ""ClaInsClaFID"": ""1"",
                    ""ClaInsSequence"":""3""
                    },
                    {
                    ""ClaInsFirstName"": ""APIClaim_InsuredFirstName4"",
                    ""ClaInsLastName"": ""APIClaim_InsuredLastName"",
                    ""ClaInsClaFID"": ""1"",
                    ""ClaInsSequence"":""4""
                    },
                    {
                    ""ClaInsFirstName"": ""APIClaim_InsuredFirstName5"",
                    ""ClaInsLastName"": ""APIClaim_InsuredLastName"",
                    ""ClaInsClaFID"": ""1"",
                    ""ClaInsSequence"":""5""
                    },
                    {
                    ""ClaInsFirstName"": ""APIClaim_InsuredFirstName6"",
                    ""ClaInsLastName"": ""APIClaim_InsuredLastName"",
                    ""ClaInsClaFID"": ""1"",
                    ""ClaInsSequence"":""6""
                    },
                    {
                    ""ClaInsFirstName"": ""APIClaim_InsuredFirstName7"",
                    ""ClaInsLastName"": ""APIClaim_InsuredLastName"",
                    ""ClaInsClaFID"": ""1"",
                    ""ClaInsSequence"":""7""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Claim_Insured records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteClaim_InsuredFromIDs(ref APIUnitTestHelperObject apiHelperObject, Guid id1, Guid id2, Guid id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Insureds/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Claim_Insured records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteClaim_InsuredFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Claim_Insureds/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=ClaInsLastName eq \""APIClaim_InsuredLastName\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Claim_Insured based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletClaim_InsuredFromID(ref APIUnitTestHelperObject apiHelperObject, Guid id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Claim_Insureds/{id1}";

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
        /// Select Claim_Insureds based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectClaim_InsuredsWithLastNameAPIClaim_InsuredLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Insureds/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=ClaInsLastName eq \""APIClaim_InsuredLastName\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Claim_Insureds/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal(@"$filter=ClaInsLastName eq ""APIClaim_InsuredLastName""", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Claim_Insured based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectClaim_InsuredsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Claim_Insured record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Claim_Insured(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Insureds/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Claim_Insured records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateClaim_InsuredListFromIDs(ref APIUnitTestHelperObject apiHelperObject, Guid id1, Guid id2, Guid id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Insureds/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""ClaInsZip"": ""55551"",
                            ""ClaInsGUID"": ""{id1}""
                        }},
                        {{
                            ""ClaInsZip"": ""55552"",
                            ""ClaInsGUID"": ""{id2}""
                        }},
                        {{
                            ""ClaInsZip"": ""55553"",
                            ""ClaInsGUID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal(55551, (int)apiHelperObject.ResponseData[0].ClaInsZip);
            Assert.Equal(55552, (int)apiHelperObject.ResponseData[1].ClaInsZip);
            Assert.Equal(55553, (int)apiHelperObject.ResponseData[2].ClaInsZip);
        }

        /// <summary>
        /// Example of updating Claim_Insureds based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateClaim_InsuredZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Insureds";

            apiHelperObject.APIBody = @$"{{
  ""Claim_InsuredsObject"": {{
    ""ClaInsZip"": ""55555""
  }},
  ""queryString"": {{
    ""query"": ""$filter=ClaInsLastName eq \""APIClaim_InsuredLastName\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(55555, (int)apiHelperObject.ResponseData[i].ClaInsZip);
            }
        }

        /// <summary>
        /// Example of updating 1 Claim_Insured record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateClaim_InsuredFromID(ref APIUnitTestHelperObject apiHelperObject, Guid id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Claim_Insureds";

            apiHelperObject.APIBody = @$"{{
                  ""Claim_InsuredsObject"": {{
                    ""ClaInsZip"": ""55551"",
                    ""ClaInsGUID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal(55551, (int)apiHelperObject.ResponseData[0].ClaInsZip);
        }
    }
}
