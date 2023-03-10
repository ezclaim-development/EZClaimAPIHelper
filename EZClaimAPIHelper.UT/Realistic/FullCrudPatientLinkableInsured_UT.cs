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
    public class FullCrudPatientLinkableInsured_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudPatientLinkableInsured_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the PatientLinkableInsureds endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudPatientLinkableInsureds()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1PatientLinkableInsureds(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestPatientLinkableInsureds(ref apiHelperObject);

                Thread.Sleep(3000);

                create1PatientLinkableInsureds(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPatientLinkableInsuredsWithLastNameAPIPatientLinkableInsuredsLastName(ref apiHelperObject, true);

                Guid id1 = (Guid)apiHelperObject.ResponseData[0].InsGUID;

                Thread.Sleep(3000);

                create6TestPatientLinkableInsureds_PatientPartOnly(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                create1PatientLinkableInsureds_PatientPartOnly(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                selectPatientLinkableInsuredsWithLastNameAPIPatientLinkableInsuredsLastName(ref apiHelperObject, true);

                Thread.Sleep(3000);

                selectPatientLinkableInsuredsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                updatePatientLinkableInsuredsZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                deletePatientLinkableInsuredsFromQuery_PatientPartOnly(ref apiHelperObject);

                Thread.Sleep(3000);

                deletePatientLinkableInsuredsFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPatientLinkableInsuredsWithLastNameAPIPatientLinkableInsuredsLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 PatientLinkableInsureds record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1PatientLinkableInsureds(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/PatientLinkableInsureds";
            apiHelperObject.APIBody = @"{
                    ""FirstName"": ""APIPatientLinkableInsuredsFirstName7"",
                    ""LastName"":""APIPatientLinkableInsuredsLastName"",
                    ""PatFID"":""1"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""7""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 PatientLinkableInsureds records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestPatientLinkableInsureds(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/PatientLinkableInsureds/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""FirstName"": ""APIPatientLinkableInsuredsFirstName1"",
                    ""LastName"":""APIPatientLinkableInsuredsLastName"",
                    ""PatFID"":""1"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""1""
                    },
                    {
                    ""FirstName"": ""APIPatientLinkableInsuredsFirstName2"",
                    ""LastName"":""APIPatientLinkableInsuredsLastName"",
                    ""PatFID"":""1"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""2""
                    },
                    {
                    ""FirstName"": ""APIPatientLinkableInsuredsFirstName3"",
                    ""LastName"":""APIPatientLinkableInsuredsLastName"",
                    ""PatFID"":""1"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""3""
                    },
                    {
                    ""FirstName"": ""APIPatientLinkableInsuredsFirstName4"",
                    ""LastName"":""APIPatientLinkableInsuredsLastName"",
                    ""PatFID"":""1"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""4""
                    },
                    {
                    ""FirstName"": ""APIPatientLinkableInsuredsFirstName5"",
                    ""LastName"":""APIPatientLinkableInsuredsLastName"",
                    ""PatFID"":""1"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""5""
                    },
                    {
                    ""FirstName"": ""APIPatientLinkableInsuredsFirstName6"",
                    ""LastName"":""APIPatientLinkableInsuredsLastName"",
                    ""PatFID"":""1"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""6""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 1 PatientLinkableInsureds record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1PatientLinkableInsureds_PatientPartOnly(ref APIUnitTestHelperObject apiHelperObject, Guid id1)
        {
            apiHelperObject.Endpoint = "/api/v2/PatientLinkableInsureds/PatientPartOnly";
            apiHelperObject.APIBody = @$"{{
                    ""PatInsInsGUID"": ""{id1}"",
                    ""PatFID"":""14"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""17""
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 PatientLinkableInsureds records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestPatientLinkableInsureds_PatientPartOnly(ref APIUnitTestHelperObject apiHelperObject, Guid id1)
        {
            apiHelperObject.Endpoint = "/api/v2/PatientLinkableInsureds/PatientPartOnly/list";

            apiHelperObject.APIBody = $@"[
                    {{
                    ""PatInsInsGUID"": ""{id1}"",
                    ""PatFID"":""3"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""11""
                    }},
                    {{
                    ""PatInsInsGUID"": ""{id1}"",
                    ""PatFID"":""4"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""12""
                    }},
                    {{
                    ""PatInsInsGUID"": ""{id1}"",
                    ""PatFID"":""8"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""13""
                    }},
                    {{
                    ""PatInsInsGUID"": ""{id1}"",
                    ""PatFID"":""9"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""14""
                    }},
                    {{
                    ""PatInsInsGUID"": ""{id1}"",
                    ""PatFID"":""10"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""15""
                    }},
                    {{
                    ""PatInsInsGUID"": ""{id1}"",
                    ""PatFID"":""11"",
                    ""RelationToInsured"":""1"",
                    ""Sequence"":""16""
                    }}
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of deleting PatientLinkableInsureds records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePatientLinkableInsuredsFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/PatientLinkableInsureds/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=LastName eq 'APIPatientLinkableInsuredsLastName'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting PatientLinkableInsureds records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePatientLinkableInsuredsFromQuery_PatientPartOnly(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/PatientLinkableInsureds/PatientPartOnly/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=LastName eq 'APIPatientLinkableInsuredsLastName'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Select PatientLinkableInsureds based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectPatientLinkableInsuredsWithLastNameAPIPatientLinkableInsuredsLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/PatientLinkableInsureds/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=LastName eq 'APIPatientLinkableInsuredsLastName'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/PatientLinkableInsureds/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal("$filter=LastName eq 'APIPatientLinkableInsuredsLastName'", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a PatientLinkableInsureds based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPatientLinkableInsuredsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first PatientLinkableInsureds record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1PatientLinkableInsureds(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/PatientLinkableInsureds/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating PatientLinkableInsureds based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updatePatientLinkableInsuredsZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/PatientLinkableInsureds";

            apiHelperObject.APIBody = @$"{{
  ""PatientLinkableInsuredsObject"": {{
    ""Zip"": ""55555""
  }},
  ""queryString"": {{
    ""query"": ""$filter=LastName eq 'APIPatientLinkableInsuredsLastName'""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(55555, (int)apiHelperObject.ResponseData[i].Zip);
            }
        }
    }
}
