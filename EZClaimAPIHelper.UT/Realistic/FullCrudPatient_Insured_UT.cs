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
    public class FullCrudPatient_Insured_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudPatient_Insured_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Patient_Insured endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudPatient_Insured()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Patient_Insured(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestPatient_Insureds(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Patient_Insured(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPatient_InsuredsWithLastNameAPIPatIns(ref apiHelperObject, true);

                Guid id1 = (Guid)apiHelperObject.ResponseData[0].PatInsGUID;
                Guid id2 = (Guid)apiHelperObject.ResponseData[1].PatInsGUID;
                Guid id3 = (Guid)apiHelperObject.ResponseData[2].PatInsGUID;

                Thread.Sleep(3000);

                selectPatient_InsuredsFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletPatient_InsuredFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updatePatient_InsuredListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updatePatient_InsuredZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updatePatient_InsuredFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deletePatient_InsuredFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletPatient_InsuredFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deletePatient_InsuredFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPatient_InsuredsWithLastNameAPIPatIns(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Patient_Insured record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Patient_Insured(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Insureds";
            apiHelperObject.APIBody = @"{
                    ""PatInsPatFID"": ""1"",
                    ""PatInsInsGUID"": ""B589F6E1-E4CF-49A6-855F-B42BF99D5965"",
                    ""PatInsSequence"": ""17"",
                    ""PatInsEligStatus"":""APIPatIns""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Patient_Insured records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestPatient_Insureds(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Insureds/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""PatInsPatFID"": ""1"",
                    ""PatInsInsGUID"": ""967E3CB4-7FB3-4B1D-AEA3-4CBAC597F7ED"",
                    ""PatInsSequence"": ""11"",
                    ""PatInsEligStatus"":""APIPatIns""
                    },
                    {
                    ""PatInsPatFID"": ""1"",
                    ""PatInsInsGUID"": ""60DF84CF-989B-4C65-B640-D0102321DFFD"",
                    ""PatInsSequence"": ""12"",
                    ""PatInsEligStatus"":""APIPatIns""
                    },
                    {
                    ""PatInsPatFID"": ""1"",
                    ""PatInsInsGUID"": ""8668755A-F5E9-480E-97AF-DA5F71DF69C5"",
                    ""PatInsSequence"": ""13"",
                    ""PatInsEligStatus"":""APIPatIns""
                    },
                    {
                    ""PatInsPatFID"": ""1"",
                    ""PatInsInsGUID"": ""B6E9BBEE-8971-4B16-884B-2C4B6BF464DE"",
                    ""PatInsSequence"": ""14"",
                    ""PatInsEligStatus"":""APIPatIns""
                    },
                    {
                    ""PatInsPatFID"": ""1"",
                    ""PatInsInsGUID"": ""783462EC-A815-4D1B-B4B0-2630B7D09B54"",
                    ""PatInsSequence"": ""15"",
                    ""PatInsEligStatus"":""APIPatIns""
                    },
                    {
                    ""PatInsPatFID"": ""1"",
                    ""PatInsInsGUID"": ""C7D0CFAF-BDD9-431A-8D33-2C0A32BC7F64"",
                    ""PatInsSequence"": ""16"",
                    ""PatInsEligStatus"":""APIPatIns""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Patient_Insured records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deletePatient_InsuredFromIDs(ref APIUnitTestHelperObject apiHelperObject, Guid id1, Guid id2, Guid id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Insureds/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Patient_Insured records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePatient_InsuredFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Patient_Insureds/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PatInsEligStatus eq 'APIPatIns'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Patient_Insured based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletPatient_InsuredFromID(ref APIUnitTestHelperObject apiHelperObject, Guid id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Patient_Insureds/{id1}";

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
        /// Select Patient_Insureds based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectPatient_InsuredsWithLastNameAPIPatIns(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Insureds/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PatInsEligStatus eq 'APIPatIns'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Patient_Insureds/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal("$filter=PatInsEligStatus eq 'APIPatIns'", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Patient_Insured based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPatient_InsuredsFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Patient_Insured record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Patient_Insured(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Insureds/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Patient_Insured records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updatePatient_InsuredListFromIDs(ref APIUnitTestHelperObject apiHelperObject, Guid id1, Guid id2, Guid id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Insureds/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""PatInsEligDate"": ""2023-03-01"",
                            ""PatInsGUID"": ""{id1}""
                        }},
                        {{
                            ""PatInsEligDate"": ""2023-03-02"",
                            ""PatInsGUID"": ""{id2}""
                        }},
                        {{
                            ""PatInsEligDate"": ""2023-03-03"",
                            ""PatInsGUID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal(2023, ((DateTime)apiHelperObject.ResponseData[0].PatInsEligDate).Year);
            Assert.Equal(03, ((DateTime)apiHelperObject.ResponseData[0].PatInsEligDate).Month);
            Assert.Equal(2023, ((DateTime)apiHelperObject.ResponseData[1].PatInsEligDate).Year);
            Assert.Equal(03, ((DateTime)apiHelperObject.ResponseData[1].PatInsEligDate).Month);
            Assert.Equal(2023, ((DateTime)apiHelperObject.ResponseData[2].PatInsEligDate).Year);
            Assert.Equal(03, ((DateTime)apiHelperObject.ResponseData[2].PatInsEligDate).Month);
        }

        /// <summary>
        /// Example of updating Patient_Insureds based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updatePatient_InsuredZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Insureds";

            apiHelperObject.APIBody = @$"{{
  ""Patient_InsuredsObject"": {{
    ""PatInsEligDate"": ""2023-03-04""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatInsEligStatus eq 'APIPatIns'""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal(2023, ((DateTime)apiHelperObject.ResponseData[i].PatInsEligDate).Year);
                Assert.Equal(03, ((DateTime)apiHelperObject.ResponseData[i].PatInsEligDate).Month);
                Assert.Equal(04, ((DateTime)apiHelperObject.ResponseData[i].PatInsEligDate).Day);
            }
        }

        /// <summary>
        /// Example of updating 1 Patient_Insured record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updatePatient_InsuredFromID(ref APIUnitTestHelperObject apiHelperObject, Guid id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Insureds";

            apiHelperObject.APIBody = @$"{{
                  ""Patient_InsuredsObject"": {{
                    ""PatInsEligDate"": ""2023-03-05"",
                    ""PatInsGUID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal(2023, ((DateTime)apiHelperObject.ResponseData[0].PatInsEligDate).Year);
            Assert.Equal(03, ((DateTime)apiHelperObject.ResponseData[0].PatInsEligDate).Month);
            Assert.Equal(05, ((DateTime)apiHelperObject.ResponseData[0].PatInsEligDate).Day);
        }
    }
}
