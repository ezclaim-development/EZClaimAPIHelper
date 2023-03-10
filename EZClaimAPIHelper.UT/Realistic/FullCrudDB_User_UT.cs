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
    public class FullCrudDB_User_UT
    {
        private readonly ITestOutputHelper output;

        public FullCrudDB_User_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Runs a full gambit of tests on the DB_User endpoints. It pauses for 3 seconds between endpoint calls to make sure it doesn't get rate limited.
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void LiveCall_FullCrudDB_User()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                selectTop1DB_User(ref apiHelperObject);

                Thread.Sleep(3000);

                create6TestDB_Users(ref apiHelperObject);

                Thread.Sleep(3000);

                create1DB_User(ref apiHelperObject);

                Thread.Sleep(3000);

                selectDB_UsersWithLastNameAPIDB_UserLastName(ref apiHelperObject, true);

                Guid id1 = (Guid)apiHelperObject.ResponseData[0].DBUserGUID;
                Guid id2 = (Guid)apiHelperObject.ResponseData[1].DBUserGUID;
                Guid id3 = (Guid)apiHelperObject.ResponseData[2].DBUserGUID;

                Thread.Sleep(3000);

                selectDB_UsersFromSetNextPageURL(ref apiHelperObject);

                Thread.Sleep(3000);

                seletDB_UserFromID(ref apiHelperObject, id1, true);

                Thread.Sleep(3000);

                updateDB_UserListFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                updateDB_UserZipBasedOnLastName(ref apiHelperObject);

                Thread.Sleep(3000);

                updateDB_UserFromID(ref apiHelperObject, id1);

                Thread.Sleep(3000);

                deleteDB_UserFromIDs(ref apiHelperObject, id1, id2, id3);

                Thread.Sleep(3000);

                seletDB_UserFromID(ref apiHelperObject, id1, false);

                Thread.Sleep(3000);

                deleteDB_UserFromQuery(ref apiHelperObject);

                Thread.Sleep(3000);

                selectDB_UsersWithLastNameAPIDB_UserLastName(ref apiHelperObject, false);
            }
        }

        /// <summary>
        /// Example creating 1 DB_User record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1DB_User(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/DB_Users";
            apiHelperObject.APIBody = @"{
                    ""DBUserName"": ""APIDB_User7"",
                    ""DBUserWindowsUser"":""APIDB_User""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 6 DB_User records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create6TestDB_Users(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/DB_Users/list";

            apiHelperObject.APIBody = @"[
                    {
                    ""DBUserName"": ""APIDB_User1"",
                    ""DBUserWindowsUser"":""APIDB_User""
                    },
                    {
                    ""DBUserName"": ""APIDB_User2"",
                    ""DBUserWindowsUser"":""APIDB_User""
                    },
                    {
                    ""DBUserName"": ""APIDB_User3"",
                    ""DBUserWindowsUser"":""APIDB_User""
                    },
                    {
                    ""DBUserName"": ""APIDB_User4"",
                    ""DBUserWindowsUser"":""APIDB_User""
                    },
                    {
                    ""DBUserName"": ""APIDB_User5"",
                    ""DBUserWindowsUser"":""APIDB_User""
                    },
                    {
                    ""DBUserName"": ""APIDB_User6"",
                    ""DBUserWindowsUser"":""APIDB_User""
                    }
                ]";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(6, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example deleting 3 DB_User records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void deleteDB_UserFromIDs(ref APIUnitTestHelperObject apiHelperObject, Guid id1, Guid id2, Guid id3)
        {
            apiHelperObject.Endpoint = "/api/v2/DB_Users/ids";

            apiHelperObject.APIBody = @$"[""{id1}"", ""{id2}"", ""{id3}""]";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal($"Record(s) Deleted Successfully. Deleted IdList : {id1},{id2},{id3}", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of deleting DB_User records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deleteDB_UserFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/DB_Users/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=DBUserWindowsUser eq 'APIDB_User'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting a DB_User based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="isSuccessful"></param>
        private void seletDB_UserFromID(ref APIUnitTestHelperObject apiHelperObject, Guid id1, bool isSuccessful)
        {
            apiHelperObject.Endpoint = $"/api/v2/DB_Users/{id1}";

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
        /// Select DB_Users based on query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="isSuccessful"></param>
        private void selectDB_UsersWithLastNameAPIDB_UserLastName(ref APIUnitTestHelperObject apiHelperObject, bool isSuccessful)
        {
            apiHelperObject.Endpoint = "/api/v2/DB_Users/GetList";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=DBUserWindowsUser eq 'APIDB_User'""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            if (isSuccessful)
            {
                Assert.Equal(200, apiHelperObject.ResponseStatus);

                Assert.Equal(5, apiHelperObject.ResponseData.Count);

                string nextPageNote = apiHelperObject.ResponseDynamicResult.NextPageNote;
                Assert.Equal("To access the next page, please use below URL and add the 'Query line' to the body.", nextPageNote);

                string nextPageURL = apiHelperObject.ResponseDynamicResult.NextPageURL;

                Assert.Equal($"{apiHelperObject.BaseAddress}/api/v2/DB_Users/page/2", nextPageURL);

                string query = apiHelperObject.ResponseDynamicResult.Query;

                Assert.Equal("$filter=DBUserWindowsUser eq 'APIDB_User'", query);
            }
            else
            {
                Assert.NotEqual(200, apiHelperObject.ResponseStatus);

                Assert.Equal("Query parameters yielded no results.", apiHelperObject.ResponseErrorResult["description"]);
            }
        }

        /// <summary>
        /// Example of selecting a DB_User based on the returned NextPageURL
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectDB_UsersFromSetNextPageURL(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = apiHelperObject.ResponseDynamicResult.NextPageURL;

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 2);
        }

        /// <summary>
        /// Example of selecting the first DB_User record.
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectTop1DB_User(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/DB_Users/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$top=1""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of updating 3 DB_User records based on id's
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        private void updateDB_UserListFromIDs(ref APIUnitTestHelperObject apiHelperObject, Guid id1, Guid id2, Guid id3)
        {
            apiHelperObject.Endpoint = "/api/v2/DB_Users/list";

            apiHelperObject.APIBody = @$"[
                        {{
                            ""DBUserName"": ""APIDB_User1_1"",
                            ""DBUserGUID"": ""{id1}""
                        }},
                        {{
                            ""DBUserName"": ""APIDB_User1_2"",
                            ""DBUserGUID"": ""{id2}""
                        }},
                        {{
                            ""DBUserName"": ""APIDB_User1_3"",
                            ""DBUserGUID"": ""{id3}""
                        }}
                    ]";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(3, apiHelperObject.ResponseData.Count);

            Assert.Equal("APIDB_User1_1", (string)apiHelperObject.ResponseData[0].DBUserName);
            Assert.Equal("APIDB_User1_2", (string)apiHelperObject.ResponseData[1].DBUserName);
            Assert.Equal("APIDB_User1_3", (string)apiHelperObject.ResponseData[2].DBUserName);
        }

        /// <summary>
        /// Example of updating DB_Users based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updateDB_UserZipBasedOnLastName(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/DB_Users";

            apiHelperObject.APIBody = @$"{{
  ""DB_UsersObject"": {{
    ""DBUserName"": ""APIDB_User1_4""
  }},
  ""queryString"": {{
    ""query"": ""$filter=DBUserWindowsUser eq 'APIDB_User'""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.True(apiHelperObject.ResponseData.Count >= 7);

            for (int i = 0; i < 7; i++)
            {
                Assert.Equal("APIDB_User1_4", (string)apiHelperObject.ResponseData[i].DBUserName);
            }
        }

        /// <summary>
        /// Example of updating 1 DB_User record based on an id
        /// </summary>
        /// <param name="apiHelperObject"></param>
        /// <param name="id1"></param>
        private void updateDB_UserFromID(ref APIUnitTestHelperObject apiHelperObject, Guid id1)
        {
            apiHelperObject.Endpoint = "/api/v2/DB_Users";

            apiHelperObject.APIBody = @$"{{
                  ""DB_UsersObject"": {{
                    ""DBUserName"": ""APIDB_User1_5"",
                    ""DBUserGUID"": ""{id1}""
                  }}
                }}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);

            Assert.Equal("APIDB_User1_5", (string)apiHelperObject.ResponseData[0].DBUserName);
        }
    }
}
