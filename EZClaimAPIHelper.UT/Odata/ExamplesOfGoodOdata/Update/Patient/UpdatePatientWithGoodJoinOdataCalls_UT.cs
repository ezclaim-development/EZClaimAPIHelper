using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public partial class UpdatePatientWithGoodOdataCalls_UT
    {
        [Fact(Skip = "This is used for example purposes only. It can be run, but there's no point.")]
        public void UpdatePatientWithGoodJoinOdataCalls()
        {
            //A join by itself will always fail for an update even if it is formatted properly. Update endpoints require a query.
        }
    }
}
