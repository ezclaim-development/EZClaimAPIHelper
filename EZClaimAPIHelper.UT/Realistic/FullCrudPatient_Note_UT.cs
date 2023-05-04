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
    public class FullCrudPatient_Note_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudPatient_Note_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Patient_Note endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudPatient_Note()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Patient_Note(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestPatient_Notes(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Patient_Note(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPatient_NotesWithLastNameAPIPatient_NoteUserName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].PatNoteID;
                int id2 = (int)apiHelperObject.ResponseData[1].PatNoteID;
                int id3 = (int)apiHelperObject.ResponseData[2].PatNoteID;

                Thread.Sleep(3000);

                selectPatient_NotesFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletPatient_NoteFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updatePatient_NoteListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updatePatient_NoteZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updatePatient_NoteFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deletePatient_NoteFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletPatient_NoteFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deletePatient_NoteFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPatient_NotesWithLastNameAPIPatient_NoteUserName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Patient_Note record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Patient_Note(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Notes";
            apiHelperObject.APIBody = @"{
                    ""PatNotePatFID"": ""1"",
                    ""PatNoteEvent"": ""Created"",
                    ""PatNoteUserName"":""APIPatient_NoteUserName""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Patient_Note records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestPatient_Notes(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Notes/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""PatNotePatFID"": ""1"",
                    ""PatNoteEvent"": ""Created"",
                    ""PatNoteUserName"":""APIPatient_NoteUserName""
                    },
                    {
                    ""PatNotePatFID"": ""1"",
                    ""PatNoteEvent"": ""Created"",
                    ""PatNoteUserName"":""APIPatient_NoteUserName""
                    },
                    {
                    ""PatNotePatFID"": ""1"",
                    ""PatNoteEvent"": ""Created"",
                    ""PatNoteUserName"":""APIPatient_NoteUserName""
                    },
                    {
                    ""PatNotePatFID"": ""1"",
                    ""PatNoteEvent"": ""Created"",
                    ""PatNoteUserName"":""APIPatient_NoteUserName""
                    },
                    {
                    ""PatNotePatFID"": ""1"",
                    ""PatNoteEvent"": ""Created"",
                    ""PatNoteUserName"":""APIPatient_NoteUserName""
                    },
                    {
                    ""PatNotePatFID"": ""1"",
                    ""PatNoteEvent"": ""Created"",
                    ""PatNoteUserName"":""APIPatient_NoteUserName""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Patient_Note records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deletePatient_NoteFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Notes/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Patient_Note records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePatient_NoteFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Patient_Notes/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PatNoteUserName eq \""APIPatient_NoteUserName\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Patient_Note based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletPatient_NoteFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Patient_Notes/{id1}";

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
        /// Select Patient_Notes based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectPatient_NotesWithLastNameAPIPatient_NoteUserName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Notes/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PatNoteUserName eq \""APIPatient_NoteUserName\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Patient_Notes/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal(@"$filter=PatNoteUserName eq ""APIPatient_NoteUserName""", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Patient_Note based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPatient_NotesFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Patient_Note record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Patient_Note(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Notes/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Patient_Note records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updatePatient_NoteListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Notes/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""PatNoteEvent"": ""Edited"",
                            ""PatNoteID"": ""{id1}""
                        }},
                        {{
                            ""PatNoteEvent"": ""Edited"",
                            ""PatNoteID"": ""{id2}""
                        }},
                        {{
                            ""PatNoteEvent"": ""Edited"",
                            ""PatNoteID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal("Edited", (string)apiHelperObject.ResponseData[0].PatNoteEvent);
            Assert.Equal("Edited", (string)apiHelperObject.ResponseData[1].PatNoteEvent);
            Assert.Equal("Edited", (string)apiHelperObject.ResponseData[2].PatNoteEvent);
        }

        /// <summary>
        /// Example of updating Patient_Notes based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updatePatient_NoteZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Notes";

            apiHelperObject.APIBody = @$"{{
  ""Patient_NotesObject"": {{
    ""PatNoteEvent"": ""Edited""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatNoteUserName eq \""APIPatient_NoteUserName\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal("Edited", (string)apiHelperObject.ResponseData[i].PatNoteEvent);
            }
        }

        /// <summary>
        /// Example of updating 1 Patient_Note record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updatePatient_NoteFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Patient_Notes";

            apiHelperObject.APIBody = @$"{{
                  ""Patient_NotesObject"": {{
                    ""PatNoteEvent"": ""Edited"",
                    ""PatNoteID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal("Edited", (string)apiHelperObject.ResponseData[0].PatNoteEvent);
        }
    }
}
