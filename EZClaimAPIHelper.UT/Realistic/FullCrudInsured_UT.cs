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
    public class FullCrudInsured_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudInsured_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Insured endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudInsured()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Insured(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestInsureds(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Insured(ref apiHelperObject);

                Thread.Sleep(3000);

                selectInsuredsWithLastNameAPIInsuredLastName(ref apiHelperObject, true);

                Guid id1 = (Guid)apiHelperObject.ResponseData[0].InsGUID;
                Guid id2 = (Guid)apiHelperObject.ResponseData[1].InsGUID;
                Guid id3 = (Guid)apiHelperObject.ResponseData[2].InsGUID;

                Thread.Sleep(3000);

                selectInsuredsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletInsuredFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateInsuredListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateInsuredZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateInsuredFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteInsuredFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletInsuredFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteInsuredFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectInsuredsWithLastNameAPIInsuredLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Insured record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Insured(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Insureds";
            apiHelperObject.APIBody = @"{
                    ""InsFirstName"": ""APIInsuredFirstName7"",
                    ""InsLastName"":""APIInsuredLastName""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Insured records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestInsureds(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Insureds/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""InsFirstName"": ""APIInsuredFirstName1"",
                    ""InsLastName"":""APIInsuredLastName""
                    },
                    {
                    ""InsFirstName"": ""APIInsuredFirstName2"",
                    ""InsLastName"":""APIInsuredLastName""
                    },
                    {
                    ""InsFirstName"": ""APIInsuredFirstName3"",
                    ""InsLastName"":""APIInsuredLastName""
                    },
                    {
                    ""InsFirstName"": ""APIInsuredFirstName4"",
                    ""InsLastName"":""APIInsuredLastName""
                    },
                    {
                    ""InsFirstName"": ""APIInsuredFirstName5"",
                    ""InsLastName"":""APIInsuredLastName""
                    },
                    {
                    ""InsFirstName"": ""APIInsuredFirstName6"",
                    ""InsLastName"":""APIInsuredLastName""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Insured records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteInsuredFromIDs(ref APIUnitTestHelperObject apiHelperObject, Guid id1, Guid id2, Guid id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Insureds/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Insured records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteInsuredFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Insureds/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=InsLastName eq 'APIInsuredLastName'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Insured based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletInsuredFromID(ref APIUnitTestHelperObject apiHelperObject, Guid id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Insureds/{id1}";

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
        /// Select Insureds based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectInsuredsWithLastNameAPIInsuredLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Insureds/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=InsLastName eq 'APIInsuredLastName'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Insureds/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal("$filter=InsLastName eq 'APIInsuredLastName'", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Insured based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectInsuredsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Insured record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Insured(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Insureds/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Insured records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateInsuredListFromIDs(ref APIUnitTestHelperObject apiHelperObject, Guid id1, Guid id2, Guid id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Insureds/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""InsZip"": ""55551"",
                            ""InsGUID"": ""{id1}""
                        }},
                        {{
                            ""InsZip"": ""55552"",
                            ""InsGUID"": ""{id2}""
                        }},
                        {{
                            ""InsZip"": ""55553"",
                            ""InsGUID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal(55551, (int)apiHelperObject.ResponseData[0].InsZip);
            Assert.Equal(55552, (int)apiHelperObject.ResponseData[1].InsZip);
            Assert.Equal(55553, (int)apiHelperObject.ResponseData[2].InsZip);
        }

        /// <summary>
        /// Example of updating Insureds based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateInsuredZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Insureds";

            apiHelperObject.APIBody = @$"{{
  ""InsuredsObject"": {{
    ""InsZip"": ""55555""
  }},
  ""queryString"": {{
    ""query"": ""$filter=InsLastName eq 'APIInsuredLastName'""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(55555, (int)apiHelperObject.ResponseData[i].InsZip);
            }
        }

        /// <summary>
        /// Example of updating 1 Insured record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateInsuredFromID(ref APIUnitTestHelperObject apiHelperObject, Guid id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Insureds";

            apiHelperObject.APIBody = @$"{{
                  ""InsuredsObject"": {{
                    ""InsZip"": ""55551"",
                    ""InsGUID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal(55551, (int)apiHelperObject.ResponseData[0].InsZip);
        }
    }
}
