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
    public class FullCrudAuthorized_Unit_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudAuthorized_Unit_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Authorized_Unit endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudAuthorized_Unit()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Authorized_Unit(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestAuthorized_Units(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Authorized_Unit(ref apiHelperObject);

                Thread.Sleep(3000);

                selectAuthorized_UnitSimpleList(ref apiHelperObject);
                
                Thread.Sleep(3000);

                selectAuthorized_UnitSimpleListPage(ref apiHelperObject, 1);

                Thread.Sleep(3000);

                selectAuthorized_UnitSimpleListWithPatientName(ref apiHelperObject);
                
                Thread.Sleep(3000);

                selectAuthorized_UnitSimpleListWithPatientNamePage(ref apiHelperObject, 1);

                Thread.Sleep(3000);

                selectAuthorized_UnitsWithLastNameAPIAuthorized_UnitLastName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].AuthUnitID;
                int id2 = (int)apiHelperObject.ResponseData[1].AuthUnitID;
                int id3 = (int)apiHelperObject.ResponseData[2].AuthUnitID;

                Thread.Sleep(3000);

                selectAuthorized_UnitsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletAuthorized_UnitFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateAuthorized_UnitListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateAuthorized_UnitZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateAuthorized_UnitFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteAuthorized_UnitFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletAuthorized_UnitFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteAuthorized_UnitFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectAuthorized_UnitsWithLastNameAPIAuthorized_UnitLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Authorized_Unit record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Authorized_Unit(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Units";
            apiHelperObject.APIBody = @"{
                    ""AuthUnitPatFID"": ""1"",
                    ""AuthUnitCertificationNumber"":""APIAuthorized_Unit"",
                    ""AuthUnitModifier1"":""t7""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Authorized_Unit records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestAuthorized_Units(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Units/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""AuthUnitPatFID"": ""1"",
                    ""AuthUnitCertificationNumber"":""APIAuthorized_Unit"",
                    ""AuthUnitModifier1"":""t1""
                    },
                    {
                    ""AuthUnitPatFID"": ""1"",
                    ""AuthUnitCertificationNumber"":""APIAuthorized_Unit"",
                    ""AuthUnitModifier1"":""t2""
                    },
                    {
                    ""AuthUnitPatFID"": ""1"",
                    ""AuthUnitCertificationNumber"":""APIAuthorized_Unit"",
                    ""AuthUnitModifier1"":""t3""
                    },
                    {
                    ""AuthUnitPatFID"": ""1"",
                    ""AuthUnitCertificationNumber"":""APIAuthorized_Unit"",
                    ""AuthUnitModifier1"":""t4""
                    },
                    {
                    ""AuthUnitPatFID"": ""1"",
                    ""AuthUnitCertificationNumber"":""APIAuthorized_Unit"",
                    ""AuthUnitModifier1"":""t5""
                    },
                    {
                    ""AuthUnitPatFID"": ""1"",
                    ""AuthUnitCertificationNumber"":""APIAuthorized_Unit"",
                    ""AuthUnitModifier1"":""t6""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Authorized_Unit records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteAuthorized_UnitFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Units/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Authorized_Unit records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteAuthorized_UnitFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Authorized_Units/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=AuthUnitCertificationNumber eq \""APIAuthorized_Unit\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Authorized_Unit based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletAuthorized_UnitFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Authorized_Units/{id1}";

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
        /// Select simple full list of Authorized_Units
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectAuthorized_UnitSimpleList(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Units/GetSimpleList";

            apiHelperObject.APIBody = @"";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);
            Assert.True(apiHelperObject.ResponseData.Count > 5);
        }

        /// <summary>
        /// Select simple full list of Authorized_Units based on page number
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="page"></param>
        private void selectAuthorized_UnitSimpleListPage(ref APIUnitTestHelperObject apiHelperObject, int page)
        {
            apiHelperObject.Endpoint = $"/api/v2/Authorized_Units/GetSimpleList/page/{page}";

            apiHelperObject.APIBody = @"";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);
            Assert.True(apiHelperObject.ResponseData.Count > 5);
        }

        /// <summary>
        /// Select simple full list of Authorized_Units
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectAuthorized_UnitSimpleListWithPatientName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Units/GetSimpleListWithPatientName";

            apiHelperObject.APIBody = @"";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);
            Assert.True(apiHelperObject.ResponseData.Count > 5);
        }

        /// <summary>
        /// Select simple full list of Authorized_Units based on page number
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="page"></param>
        private void selectAuthorized_UnitSimpleListWithPatientNamePage(ref APIUnitTestHelperObject apiHelperObject, int page)
        {
            apiHelperObject.Endpoint = $"/api/v2/Authorized_Units/GetSimpleListWithPatientName/page/{page}";

            apiHelperObject.APIBody = @"";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);
            Assert.True(apiHelperObject.ResponseData.Count > 5);
        }

        /// <summary>
        /// Select Authorized_Units based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectAuthorized_UnitsWithLastNameAPIAuthorized_UnitLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Units/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=AuthUnitCertificationNumber eq \""APIAuthorized_Unit\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Authorized_Units/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal(@"$filter=AuthUnitCertificationNumber eq ""APIAuthorized_Unit""", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Authorized_Unit based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectAuthorized_UnitsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Authorized_Unit record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Authorized_Unit(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Units/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Authorized_Unit records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateAuthorized_UnitListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Units/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""AuthUnitModifier1"": ""E1"",
                            ""AuthUnitID"": ""{id1}""
                        }},
                        {{
                            ""AuthUnitModifier1"": ""E2"",
                            ""AuthUnitID"": ""{id2}""
                        }},
                        {{
                            ""AuthUnitModifier1"": ""E3"",
                            ""AuthUnitID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal("E1", (string)apiHelperObject.ResponseData[0].AuthUnitModifier1);
            Assert.Equal("E2", (string)apiHelperObject.ResponseData[1].AuthUnitModifier1);
            Assert.Equal("E3", (string)apiHelperObject.ResponseData[2].AuthUnitModifier1);
        }

        /// <summary>
        /// Example of updating Authorized_Units based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateAuthorized_UnitZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Units";

            apiHelperObject.APIBody = @$"{{
  ""Authorized_UnitsObject"": {{
    ""AuthUnitModifier1"": ""EE""
  }},
  ""queryString"": {{
    ""query"": ""$filter=AuthUnitCertificationNumber eq \""APIAuthorized_Unit\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal("EE", (string)apiHelperObject.ResponseData[i].AuthUnitModifier1);
            }
        }

        /// <summary>
        /// Example of updating 1 Authorized_Unit record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateAuthorized_UnitFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Authorized_Units";

            apiHelperObject.APIBody = @$"{{
                  ""Authorized_UnitsObject"": {{
                    ""AuthUnitModifier1"": ""EI"",
                    ""AuthUnitID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal("EI", (string)apiHelperObject.ResponseData[0].AuthUnitModifier1);
        }
    }
}
