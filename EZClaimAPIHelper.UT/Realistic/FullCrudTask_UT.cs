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
    public class FullCrudTask_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudTask_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the Task endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudTask()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1Task(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestTasks(ref apiHelperObject);

                Thread.Sleep(3000);

                create1Task(ref apiHelperObject);

                Thread.Sleep(3000);

                selectTasksWithLastNameAPITaskLastName(ref apiHelperObject, true);

                int id1 = (int)apiHelperObject.ResponseData[0].TaskID;
                int id2 = (int)apiHelperObject.ResponseData[1].TaskID;
                int id3 = (int)apiHelperObject.ResponseData[2].TaskID;

                Thread.Sleep(3000);

                selectTasksFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletTaskFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateTaskListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateTaskZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateTaskFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteTaskFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletTaskFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteTaskFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectTasksWithLastNameAPITaskLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 Task record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1Task(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Tasks";
            apiHelperObject.APIBody = @"{
                    ""TaskStatus"": ""Not Started"",
                    ""TaskSubject"":""APITask""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 Task records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestTasks(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Tasks/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""TaskStatus"": ""APITaskFirstName1"",
                    ""TaskSubject"":""APITask""
                    },
                    {
                    ""TaskStatus"": ""APITaskFirstName2"",
                    ""TaskSubject"":""APITask""
                    },
                    {
                    ""TaskStatus"": ""APITaskFirstName3"",
                    ""TaskSubject"":""APITask""
                    },
                    {
                    ""TaskStatus"": ""APITaskFirstName4"",
                    ""TaskSubject"":""APITask""
                    },
                    {
                    ""TaskStatus"": ""APITaskFirstName5"",
                    ""TaskSubject"":""APITask""
                    },
                    {
                    ""TaskStatus"": ""APITaskFirstName6"",
                    ""TaskSubject"":""APITask""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 Task records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteTaskFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Tasks/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting Task records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteTaskFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Tasks/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=TaskSubject eq \""APITask\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a Task based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletTaskFromID(ref APIUnitTestHelperObject apiHelperObject, int id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/Tasks/{id1}";

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
        /// Select Tasks based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectTasksWithLastNameAPITaskLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/Tasks/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=TaskSubject eq \""APITask\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/Tasks/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal(@"$filter=TaskSubject eq ""APITask""", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a Task based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTasksFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first Task record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1Task(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Tasks/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 Task records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateTaskListFromIDs(ref APIUnitTestHelperObject apiHelperObject, int id1, int id2, int id3)
        {
            apiHelperObject.Endpoint = "/api/v2/Tasks/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""TaskStatus"": ""Completed"",
                            ""TaskID"": ""{id1}""
                        }},
                        {{
                            ""TaskStatus"": ""Completed"",
                            ""TaskID"": ""{id2}""
                        }},
                        {{
                            ""TaskStatus"": ""Completed"",
                            ""TaskID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal("Completed", (string)apiHelperObject.ResponseData[0].TaskStatus);
            Assert.Equal("Completed", (string)apiHelperObject.ResponseData[1].TaskStatus);
            Assert.Equal("Completed", (string)apiHelperObject.ResponseData[2].TaskStatus);
        }

        /// <summary>
        /// Example of updating Tasks based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateTaskZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Tasks";

            apiHelperObject.APIBody = @$"{{
  ""TasksObject"": {{
    ""TaskStatus"": ""Completed""
  }},
  ""queryString"": {{
    ""query"": ""$filter=TaskSubject eq \""APITask\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal("Completed", (string)apiHelperObject.ResponseData[i].TaskStatus);
            }
        }

        /// <summary>
        /// Example of updating 1 Task record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateTaskFromID(ref APIUnitTestHelperObject apiHelperObject, int id1)
        {
            apiHelperObject.Endpoint = "/api/v2/Tasks";

            apiHelperObject.APIBody = @$"{{
                  ""TasksObject"": {{
                    ""TaskStatus"": ""Completed"",
                    ""TaskID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal("Completed", (string)apiHelperObject.ResponseData[0].TaskStatus);
        }
    }
}
