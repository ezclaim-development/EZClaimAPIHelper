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
    public partial class OtherErrors_UT
    {
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void TokenRelatedErrors()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = "junk";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied token parameter is invalid.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken2_NotYetRegisteredToken;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "Token is valid but setup is not complete. To have the token be setup please contact ezclaim support or wait for setup to be complete.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_CreatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_DeletePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_UpdatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$join=LEFT Claim ClaPatFID EQ Patient PatID""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_SelectPatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$join=LEFT Claim ClaPatFID EQ Patient PatID""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_CreatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$join=LEFT Claim ClaPatFID EQ Patient PatID""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_DeletePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/GetList";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$join=LEFT Claim ClaPatFID EQ Patient PatID""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_UpdatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_SelectPatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_DeletePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_UpdatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""patZip"": ""55551"",
                    ""patID"": 1
                  }}
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_SelectPatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""patZip"": ""55551"",
                    ""patID"": 1
                  }}
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_CreatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""patZip"": ""55551"",
                    ""patID"": 1
                  }}
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_DeletePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/query";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_SelectPatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/query";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_CreatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/query";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_UpdatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims/GetList";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_CreatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims/GetList";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_DeletePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims/GetList";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_UpdatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims/GetList";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$join=LEFT Patient PatID EQ Claim ClaPatFID""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_SelectPatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");


                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims/GetList";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$join=LEFT Patient PatID EQ Claim ClaPatFID""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_CreatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");


                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims/GetList";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$join=LEFT Patient PatID EQ Claim ClaPatFID""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_DeletePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");


                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims/GetList";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$join=LEFT Patient PatID EQ Claim ClaPatFID""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_UpdatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");


                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_SelectPatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_DeletePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims";
                apiHelperObject.APIBody = $@"{{}}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_UpdatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""patZip"": ""55551"",
                    ""patID"": 1
                  }}
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_SelectPatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""patZip"": ""55551"",
                    ""patID"": 1
                  }}
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_CreatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""patZip"": ""55551"",
                    ""patID"": 1
                  }}
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_DeletePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims/query";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_SelectPatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims/query";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_CreatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "The supplied access credential does not have permission to access this endpoint.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Claims/query";
                apiHelperObject.APIBody = $@"{{
                    ""Query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
                }}";
                apiHelperObject.Token = APIUnitTestHelperObject.TestToken_UpdatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Delete, "The supplied access credential does not have permission to access this endpoint.");

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = @"[
                    {
                    ""PatFirstName"": ""APIPatientFirstName9"",
                    ""PatLastName"":""APIPatientLastName"",
                    ""claimsObjectWithoutIDWithChildrenWithoutID"": [
                    {
                        ""ClaDiagnosis1"" : ""APIClaimDiagnosis"",
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
                    }
                    ],
                    ""patient_NotesObjectWithoutID"": [
                    {
                        ""PatNoteEvent"": ""Created"",
                        ""PatNoteUserName"":""APIPatient_NoteUserName""
                    },
                    {
                        ""PatNoteEvent"": ""Created"",
                        ""PatNoteUserName"":""APIPatient_NoteUserName2""
                    }
                    ],
                    ""paymentsObjectWithoutID"": [
                    {
                        ""PmtDate"": ""2023-03-07"",
                        ""PmtNameOnCard"":""APIPayment""
                    }
                    ]
                    },
                    {
                    ""PatFirstName"": ""APIPatientFirstName10"",
                    ""PatLastName"":""APIPatientLastName"",
                    ""claimsObjectWithoutIDWithChildrenWithoutID"": [
                    {
                        ""ClaDiagnosis1"" : ""APIClaimDiagnosis"",
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
                    ],
                    ""patient_NotesObjectWithoutID"": [
                    {
                        ""PatNoteEvent"": ""Created"",
                        ""PatNoteUserName"":""APIPatient_NoteUserName""
                    }
                    ],
                    ""paymentsObjectWithoutID"": [
                    {
                        ""PmtDate"": ""2023-03-07"",
                        ""PmtNameOnCard"":""APIPayment""
                    }
                    ]
                    }
                ]";

                apiHelperObject.Token = APIUnitTestHelperObject.s01Token_CreatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = @"{
                    ""PatFirstName"": ""APIPatientFirstName8"",
                    ""PatLastName"":""APIPatientLastName"",
                    ""claimsObjectWithoutIDWithChildrenWithoutID"": [
                    {
                        ""ClaDiagnosis1"" : ""APIClaimDiagnosis"",
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
                    ],
                    ""patient_NotesObjectWithoutID"": [
                    {
                        ""PatNoteEvent"": ""Created"",
                        ""PatNoteUserName"":""APIPatient_NoteUserName""
                    }
                    ],
                    ""paymentsObjectWithoutID"": [
                    {
                        ""PmtDate"": ""2023-03-07"",
                        ""PmtNameOnCard"":""APIPayment""
                    }
                    ]
                }";

                apiHelperObject.Token = APIUnitTestHelperObject.s01Token_CreatePatient;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Post, "The supplied access credential does not have permission to access this endpoint.");
            }
        }
    }
}
