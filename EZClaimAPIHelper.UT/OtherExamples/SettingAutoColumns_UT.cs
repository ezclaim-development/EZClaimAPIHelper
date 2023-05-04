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
    public class SettingAutoColumns_UT
    {
        private readonly ITestOutputHelper output;

        public SettingAutoColumns_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// Gives examples of attempting to create or update auto columns and shows that they will not update
        /// </summary>
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void SettingAutoColumns()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                string PatCreatedUserGUID = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
                string PatLastUserGUID = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
                string PatCreatedUserName = "AutoColumn";
                string PatLastUserName = "AutoColumn";
                string PatCreatedComputerName = "AutoColumn";
                string PatLastComputerName = "AutoColumn";
                string PatCityStateZipCC = "AutoColumn";

                create1PatientSimple(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPatient(ref apiHelperObject);

                for (int i = 0; i < apiHelperObject.ResponseData.Count; i++)
                {
                    DateTime PatDateTimeCreatedActual = (DateTime)apiHelperObject.ResponseData[i].PatDateTimeCreated;
                    Assert.False(2023 == PatDateTimeCreatedActual.Year && 3 == PatDateTimeCreatedActual.Month && 7 == PatDateTimeCreatedActual.Day);

                    DateTime PatDateTimeModifiedActual = (DateTime)apiHelperObject.ResponseData[i].PatDateTimeModified;
                    Assert.False(2023 == PatDateTimeModifiedActual.Year && 3 == PatDateTimeModifiedActual.Month && 7 == PatDateTimeModifiedActual.Day);

                    Assert.NotEqual(PatCreatedUserGUID, (string)apiHelperObject.ResponseData[i].PatCreatedUserGUID);
                    Assert.NotEqual(PatLastUserGUID, (string)apiHelperObject.ResponseData[i].PatLastUserGUID);
                    Assert.NotEqual(PatCreatedUserName, (string)apiHelperObject.ResponseData[i].PatCreatedUserName);
                    Assert.NotEqual(PatLastUserName, (string)apiHelperObject.ResponseData[i].PatLastUserName);
                    Assert.NotEqual(PatCreatedComputerName, (string)apiHelperObject.ResponseData[i].PatCreatedComputerName);
                    Assert.NotEqual(PatLastComputerName, (string)apiHelperObject.ResponseData[i].PatLastComputerName);

                    DateTime? PatFirstDateTRIGActual = (DateTime?)apiHelperObject.ResponseData[i].PatFirstDateTRIG;
                    if (PatFirstDateTRIGActual.HasValue)
                    {
                        Assert.False(2023 == PatFirstDateTRIGActual.Value.Year && 3 == PatFirstDateTRIGActual.Value.Month && 7 == PatFirstDateTRIGActual.Value.Day);
                    }

                    Assert.NotEqual(PatCityStateZipCC, (string)apiHelperObject.ResponseData[i].PatCityStateZipCC);
                }

                Thread.Sleep(3000);

                updatePatient(ref apiHelperObject);

                Thread.Sleep(3000);

                selectPatient(ref apiHelperObject);

                for (int i = 0; i < apiHelperObject.ResponseData.Count; i++)
                {
                    DateTime PatDateTimeCreatedActual = (DateTime)apiHelperObject.ResponseData[i].PatDateTimeCreated;
                    Assert.False(2023 == PatDateTimeCreatedActual.Year && 3 == PatDateTimeCreatedActual.Month && 7 == PatDateTimeCreatedActual.Day);

                    DateTime PatDateTimeModifiedActual = (DateTime)apiHelperObject.ResponseData[i].PatDateTimeModified;
                    Assert.False(2023 == PatDateTimeModifiedActual.Year && 3 == PatDateTimeModifiedActual.Month && 7 == PatDateTimeModifiedActual.Day);

                    Assert.NotEqual(PatCreatedUserGUID, (string)apiHelperObject.ResponseData[i].PatCreatedUserGUID);
                    Assert.NotEqual(PatLastUserGUID, (string)apiHelperObject.ResponseData[i].PatLastUserGUID);
                    Assert.NotEqual(PatCreatedUserName, (string)apiHelperObject.ResponseData[i].PatCreatedUserName);
                    Assert.NotEqual(PatLastUserName, (string)apiHelperObject.ResponseData[i].PatLastUserName);
                    Assert.NotEqual(PatCreatedComputerName, (string)apiHelperObject.ResponseData[i].PatCreatedComputerName);
                    Assert.NotEqual(PatLastComputerName, (string)apiHelperObject.ResponseData[i].PatLastComputerName);

                    DateTime? PatFirstDateTRIGActual = (DateTime?)apiHelperObject.ResponseData[i].PatFirstDateTRIG;
                    if (PatFirstDateTRIGActual.HasValue)
                    {
                        Assert.False(2023 == PatFirstDateTRIGActual.Value.Year && 3 == PatFirstDateTRIGActual.Value.Month && 7 == PatFirstDateTRIGActual.Value.Day);
                    }

                    Assert.NotEqual(PatCityStateZipCC, (string)apiHelperObject.ResponseData[i].PatCityStateZipCC);
                }

                Thread.Sleep(3000);

                deletePatientFromQuery(ref apiHelperObject);
            }
        }

        /// <summary>
        /// Example creating 1 patient record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1PatientSimple(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients";
            apiHelperObject.APIBody = @"{
                    ""PatFirstName"": ""API_SetAutoColumnTest"",
                    ""PatLastName"":""API_SetAutoColumnTest"",
                    ""PatDateTimeCreated"" : ""2023-03-07"",
                    ""PatDateTimeModified"" : ""2023-03-07"",
                    ""PatCreatedUserGUID"" : ""3fa85f64-5717-4562-b3fc-2c963f66afa6"",
                    ""PatLastUserGUID"" : ""3fa85f64-5717-4562-b3fc-2c963f66afa6"",
                    ""PatCreatedUserName"" : ""AutoColumn"",
                    ""PatLastUserName"" : ""AutoColumn"",
                    ""PatCreatedComputerName"" : ""AutoColumn"",
                    ""PatLastComputerName"" : ""AutoColumn"",
                    ""PatFirstDateTRIG"" : ""2023-03-07"",
                    ""PatCityStateZipCC"" : ""AutoColumn""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example creating 1 patient record
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void create1PatientWithAutoColumns(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients";
            apiHelperObject.APIBody = @"{
                    ""PatFirstName"": ""API_SetAutoColumnTest"",
                    ""PatLastName"":""API_SetAutoColumnTest""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Equal(1, apiHelperObject.ResponseData.Count);
        }

        /// <summary>
        /// Example of deleting patient records based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void deletePatientFromQuery(ref APIUnitTestHelperObject apiHelperObject)
        {

            apiHelperObject.Endpoint = "/api/v2/Patients/query";

            apiHelperObject.APIBody = @"{
                    ""Query"": ""$filter=PatLastName eq \""API_SetAutoColumnTest\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Delete);

            Assert.Equal(200, apiHelperObject.ResponseStatus);

            Assert.Contains("Record(s) Deleted Successfully. Deleted Count : ", (string)apiHelperObject.ResponseData);
        }

        /// <summary>
        /// Example of selecting patient records
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void selectPatient(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients/GetList";

            apiHelperObject.APIBody = @"{
                  ""Query"": ""$filter=PatLastName eq \""API_SetAutoColumnTest\""""
                }";

            apiHelperObject.RunAPICall(HttpMethod.Post);

            Assert.Equal(200, apiHelperObject.ResponseStatus);
        }

        /// <summary>
        /// Example of updating patients based on a query
        /// </summary>
        /// <param name="apiHelperObject"></param>
        private void updatePatient(ref APIUnitTestHelperObject apiHelperObject)
        {
            apiHelperObject.Endpoint = "/api/v2/Patients";

            apiHelperObject.APIBody = @$"{{
  ""patientsObject"": {{
    ""patZip"": ""55555"",
    ""PatDateTimeCreated"" : ""2023-03-07"",
    ""PatDateTimeModified"" : ""2023-03-07"",
    ""PatCreatedUserGUID"" : ""3fa85f64-5717-4562-b3fc-2c963f66afa6"",
    ""PatLastUserGUID"" : ""3fa85f64-5717-4562-b3fc-2c963f66afa6"",
    ""PatCreatedUserName"" : ""AutoColumn"",
    ""PatLastUserName"" : ""AutoColumn"",
    ""PatCreatedComputerName"" : ""AutoColumn"",
    ""PatLastComputerName"" : ""AutoColumn"",
    ""PatFirstDateTRIG"" : ""2023-03-07"",
    ""PatCityStateZipCC"" : ""AutoColumn""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq \""API_SetAutoColumnTest\""""
  }}
}}";

            apiHelperObject.RunAPICall(HttpMethod.Put);

            Assert.Equal(200, apiHelperObject.ResponseStatus);
        }
    }
}
