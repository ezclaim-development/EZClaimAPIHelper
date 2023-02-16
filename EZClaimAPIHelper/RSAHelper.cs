using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Security.Cryptography;

namespace EZClaimAPIHelper
{
    /// <summary>
    /// A helper for encrypting and decrypting RSA items
    /// </summary>
    /// <remarks>
    /// Helpful resource: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-5.0#code-try-2
    /// </remarks>
    public static class RSAHelper
    {
        public static string GetDecryptedAESKeyAsBase64(string rsaPrivateKeyAsBase64, string encryptedAESKeyAsBase64)
        {
            using (RSACryptoServiceProvider rsa = new(2048))
            {
                rsa.ImportCspBlob(Convert.FromBase64String(rsaPrivateKeyAsBase64));

                RSAPKCS1KeyExchangeDeformatter keyDeformatter = new RSAPKCS1KeyExchangeDeformatter(rsa);
                return Convert.ToBase64String(keyDeformatter.DecryptKeyExchange(Convert.FromBase64String(encryptedAESKeyAsBase64)));
            }
        }

        public static byte[] GetDecryptedAESKey(string rsaPrivateKeyAsBase64, string encryptedAESKeyAsBase64)
        {
            using (RSACryptoServiceProvider rsa = new(2048))
            {
                rsa.ImportCspBlob(Convert.FromBase64String(rsaPrivateKeyAsBase64));

                RSAPKCS1KeyExchangeDeformatter keyDeformatter = new RSAPKCS1KeyExchangeDeformatter(rsa);
                return keyDeformatter.DecryptKeyExchange(Convert.FromBase64String(encryptedAESKeyAsBase64));
            }
        }

        public static string GetEncryptedAESKeyAsBase64(string rsaPublicKeyAsBase64, string aesKeyAsBase64)
        {
            using (RSACryptoServiceProvider rsa = new(2048))
            {
                rsa.ImportCspBlob(Convert.FromBase64String(rsaPublicKeyAsBase64));

                RSAPKCS1KeyExchangeFormatter keyFormatter = new RSAPKCS1KeyExchangeFormatter(rsa);
                return Convert.ToBase64String(keyFormatter.CreateKeyExchange(Convert.FromBase64String(aesKeyAsBase64), typeof(Aes)));
            }
        }
        public static string GetEncryptedAESKeyAsBase64(string rsaPublicKeyAsBase64, byte[] aesKey)
        {
            using (RSACryptoServiceProvider rsa = new(2048))
            {
                rsa.ImportCspBlob(Convert.FromBase64String(rsaPublicKeyAsBase64));

                RSAPKCS1KeyExchangeFormatter keyFormatter = new RSAPKCS1KeyExchangeFormatter(rsa);
                return Convert.ToBase64String(keyFormatter.CreateKeyExchange(aesKey));
            }
        }
    }
}
