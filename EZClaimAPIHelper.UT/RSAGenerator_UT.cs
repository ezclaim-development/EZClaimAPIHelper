using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EZClaimAPIHelper.UT
{
    public class RSAGenerator_UT
    {
        /// <summary>
        /// Example of generating an RSA CSP blob and then using the RSA CSP blob
        /// </summary>
        [Fact]
        public void GenerateAndUseRSA()
        {
            string t1;
            string t2;

            using (RSACryptoServiceProvider rsa = new(2048))
            {
                t1 = Convert.ToBase64String(rsa.ExportCspBlob(true));
                t2 = Convert.ToBase64String(rsa.ExportCspBlob(false));
            }

            using (RSACryptoServiceProvider rsa = new(2048))
            {
                rsa.ImportCspBlob(Convert.FromBase64String(t1));
            }

            using (RSACryptoServiceProvider rsa = new(2048))
            {
                rsa.ImportCspBlob(Convert.FromBase64String(t2));
            }
        }
    }
}
