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
        public void UpdateRecord_RelatedErrors()
        {
            using (Aes aes = Aes.Create())
            {
                //APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ExampleRSAPublicKey, APIUnitTestHelperObject.s01Token, "https://localhost:44320");
                APIUnitTestHelperObject apiHelperObject = new(aes.Key, aes.IV, APIUnitTestHelperObject.ProductionRSAPublicKey, APIUnitTestHelperObject.TestToken, "https://ezclaimapidev.azurewebsites.net");

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
}}";
                apiHelperObject.Token = APIUnitTestHelperObject.s01Token;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "Object reference not set to an instance of an object.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""patZip"": ""55555""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq \""kfjaslkdjflkasdjflkasjdflkasd\""""
  }}
}}";
                apiHelperObject.Token = APIUnitTestHelperObject.s01Token;

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "No data found.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""patZip"": ""55551"",
                    ""patID"": ""0""
                  }}
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "No Objects Could Be Updated - The following errors were encountered: Identification Column Not Found - PatID must be filled in with a valid value. Object with id of 0 has been skipped.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""patID"": ""0""
                  }}
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "No Objects Could Be Updated - The following errors were encountered: Identification Column Not Found - PatID must be filled in with a valid value. Object with id of 0 has been skipped.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""patID"": ""1""
                  }}
                }}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "No Objects Could Be Updated - The following errors were encountered: Update Column Not Found - at least one column should be passed in to be updated. Object with id of 1 has been skipped.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""PatActive"": ""abc"",
                    ""patID"": ""0""
                  }}
                }}";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path 'patientsObject.PatActive', line 3, position 38.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
                  ""patientsObject"": {{
                    ""PatActive"": ""abc"",
                    ""patID"": ""1""
                  }}
                }}";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path 'patientsObject.PatActive', line 3, position 38.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""PatActive"": ""abc"",
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
  }}
}}";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path 'patientsObject.PatActive', line 3, position 22.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""PatActive"": ""abc""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
  }}
}}";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path 'patientsObject.PatActive', line 3, position 22.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""patID"": null
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
  }}
}}";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Error converting value {null} to type 'System.Int32'. Path 'patientsObject.patID', line 3, position 17.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""patID"": ""1""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
  }}
}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "Primary key in body with 'Where condition' cannot be passed to update a record.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""patZip"": ""55555""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName ne \""APIPatientLastName\""""
  }}
}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "This would update 60 rows which is more than the allotted 50 rows permitted. Update the filter query to be more specific.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""junk"": ""55555""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName ne \""APIPatientLastName\""""
  }}
}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "No Object To Update - No object passed in to be updated.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""notCorrectObjectName"": {{
    ""junk"": ""55555""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName ne \""APIPatientLastName\""""
  }}
}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "Object reference not set to an instance of an object.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""junk"": ""55555""
  }},
  ""notCorrectObjectName"": {{
    ""query"": ""$filter=PatLastName ne \""APIPatientLastName\""""
  }}
}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "No Object To Update - No object passed in to be updated.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""junk"": ""55555""
  }},
  ""queryString"": {{
    ""notCorrectObjectName"": ""$filter=PatLastName ne \""APIPatientLastName\""""
  }}
}}";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "No Object To Update - No object passed in to be updated.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""patZip"": ""55551"",
                            ""patID"": null
                        }},
                        {{
                            ""patZip"": ""55552"",
                            ""patID"": null
                        }},
                        {{
                            ""patZip"": ""55553"",
                            ""patID"": null
                        }}
                    ]";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Error converting value {null} to type 'System.Int32'. Path '[0].patID', line 4, position 41.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""patZip"": ""55551"",
                            ""patID"": ""0""
                        }},
                        {{
                            ""patZip"": ""55552"",
                            ""patID"": ""0""
                        }},
                        {{
                            ""patZip"": ""55553"",
                            ""patID"": ""0""
                        }}
                    ]";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "No Objects Could Be Updated - The following errors were encountered: Identification Column Not Found - PatID must be filled in with a valid value. Object with id of 0 has been skipped., Identification Column Not Found - PatID must be filled in with a valid value. Object with id of 0 has been skipped., Identification Column Not Found - PatID must be filled in with a valid value. Object with id of 0 has been skipped.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""patZip"": ""55551"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""patZip"": ""55552"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""patZip"": ""55553"",
                            ""patID"": ""1""
                        }}
                    ]";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "No Objects Could Be Updated - The following errors were encountered: Duplicate Record Detected - a duplicate record was detected for the object with id of 1. All records with this id have been skipped.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""patZip"": ""55551"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""patZip"": ""55552"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""patZip"": ""55553"",
                            ""patID"": ""2""
                        }}
                    ]";

                runOtherErrorCall_ExpectedOutcomeEquals(ref apiHelperObject, HttpMethod.Put, "No data found.");

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""PatActive"": ""abc""
                            ""patID"": ""0""
                        }},
                        {{
                            ""PatActive"": ""abc""
                            ""patID"": ""0""
                        }},
                        {{
                            ""PatActive"": ""abc""
                            ""patID"": ""0""
                        }}
                    ]";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path '[0].PatActive', line 3, position 46.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""0""
                        }},
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""0""
                        }},
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""0""
                        }}
                    ]";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path '[0].PatActive', line 3, position 46.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""1""
                        }}
                    ]";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path '[0].PatActive', line 3, position 46.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""2""
                        }}
                    ]";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path '[0].PatActive', line 3, position 46.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""3""
                        }}
                    ]";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path '[0].PatActive', line 3, position 46.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""PatActive"": ""abc"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""patZip"": ""55552"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""patZip"": ""55553"",
                            ""patID"": ""3""
                        }}
                    ]";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path '[0].PatActive', line 3, position 46.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""PatActive"": ""abc""
                        }},
                        {{
                            ""patZip"": ""55552""
                        }},
                        {{
                            ""patZip"": ""55553""
                        }}
                    ]";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Could not convert string to boolean: abc. Path '[0].PatActive', line 3, position 46.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""PatId"": """",
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq \\""APIPatientLastName\\""""
  }}
}}";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("The provided request was unable to be used by the api for the following reason(s):");
                expectedContainsValuesList.Add("Error converting value {null} to type 'System.Int32'. Path 'patientsObject.PatId', line 3, position 15.)");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients";
                apiHelperObject.APIBody = $@"{{
  ""patientsObject"": {{
    ""patFirstName"": ""ReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyLongName""
  }},
  ""queryString"": {{
    ""query"": ""$filter=PatLastName eq \""APIPatientLastName\""""
  }}
}}";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("No Objects Could Be Updated - No columns would be updated for one or more reasons - The following errors were encountered:");
                expectedContainsValuesList.Add("Column Too Long - PatFirstName cannot be set to a value greater than 50. This column will not be updated.");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);

                Thread.Sleep(3000);

                apiHelperObject.Endpoint = "/api/v2/Patients/list";
                apiHelperObject.APIBody = $@"[
                        {{
                            ""patFirstName"": ""ReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyLongName"",
                            ""patID"": ""1""
                        }},
                        {{
                            ""patFirstName"": ""ReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyLongName"",
                            ""patID"": ""3""
                        }}
                    ]";

                expectedContainsValuesList = new();
                expectedContainsValuesList.Add("No Objects Could Be Updated - No columns would be updated for one or more reasons - The following errors were encountered:");
                expectedContainsValuesList.Add("Column Too Long - PatFirstName cannot be set to a value greater than 50. This column will not be updated.");
                expectedContainsValuesList.Add("Column Too Long - PatFirstName cannot be set to a value greater than 50. This column will not be updated.");

                runOtherErrorCall_ExpectedOutcomeContains(ref apiHelperObject, HttpMethod.Put, expectedContainsValuesList);
            }
        }
    }
}
