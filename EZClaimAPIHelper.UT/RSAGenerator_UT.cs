﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EZClaimAPIHelper.UT
{
    public class RSAGenerator_UT
    {
        private readonly ITestOutputHelper output;
        public RSAGenerator_UT(ITestOutputHelper output)
        {
            this.output = output;
        }

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

        //Untested
        [Fact]
        public void GetPublicKeyForPython()
        {
            //Based on https://stackoverflow.com/questions/28288397/import-csp-blob-exported-from-net-into-python-pycrypto
            using (RSACryptoServiceProvider rsa = new(2048))
            {
                rsa.ImportCspBlob(Convert.FromBase64String(APIUnitTestHelperObject.ProductionRSAPublicKey));

                RSAParameters rsaParameters = rsa.ExportParameters(false);
                string modulus = Convert.ToBase64String(rsaParameters.Modulus);
                string exponent = Convert.ToBase64String(rsaParameters.Exponent);

                output.WriteLine($@"
mod_raw = b64decode('{modulus}')
exp_raw = b64decode('{exponent}')
mod = int.from_bytes(mod_raw, 'big')
exp = int.from_bytes(exp_raw, 'big')
seq = asn1.DerSequence()
seq.append(mod)
seq.append(exp)
der = seq.encode()
keyPub = RSA.importKey(der)
");

            }
        }

        [Fact]
        public void GetPublicKeyFile()
        {
            //Based on https://stackoverflow.com/questions/28406888/c-sharp-rsa-public-key-output-not-correct/28407693#28407693
            using (RSACryptoServiceProvider rsa = new(2048))
            {
                rsa.ImportCspBlob(Convert.FromBase64String(APIUnitTestHelperObject.ProductionRSAPublicKey));

                using (StreamWriter writer = File.CreateText($"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}//publicKey.txt"))
                {
                    ExportPublicKey(rsa, writer);
                }
            }
        }

        //The following code was directly taken from https://stackoverflow.com/questions/28406888/c-sharp-rsa-public-key-output-not-correct/28407693#28407693
        #region ExportPublicKeyToFile
        private static void ExportPublicKey(RSACryptoServiceProvider csp, TextWriter outputStream)
        {
            var parameters = csp.ExportParameters(false);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    innerWriter.Write((byte)0x30); // SEQUENCE
                    EncodeLength(innerWriter, 13);
                    innerWriter.Write((byte)0x06); // OBJECT IDENTIFIER
                    var rsaEncryptionOid = new byte[] { 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01 };
                    EncodeLength(innerWriter, rsaEncryptionOid.Length);
                    innerWriter.Write(rsaEncryptionOid);
                    innerWriter.Write((byte)0x05); // NULL
                    EncodeLength(innerWriter, 0);
                    innerWriter.Write((byte)0x03); // BIT STRING
                    using (var bitStringStream = new MemoryStream())
                    {
                        var bitStringWriter = new BinaryWriter(bitStringStream);
                        bitStringWriter.Write((byte)0x00); // # of unused bits
                        bitStringWriter.Write((byte)0x30); // SEQUENCE
                        using (var paramsStream = new MemoryStream())
                        {
                            var paramsWriter = new BinaryWriter(paramsStream);
                            EncodeIntegerBigEndian(paramsWriter, parameters.Modulus); // Modulus
                            EncodeIntegerBigEndian(paramsWriter, parameters.Exponent); // Exponent
                            var paramsLength = (int)paramsStream.Length;
                            EncodeLength(bitStringWriter, paramsLength);
                            bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength);
                        }
                        var bitStringLength = (int)bitStringStream.Length;
                        EncodeLength(innerWriter, bitStringLength);
                        innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength);
                    }
                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                outputStream.WriteLine("-----BEGIN PUBLIC KEY-----");
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                }
                outputStream.WriteLine("-----END PUBLIC KEY-----");
            }
        }

        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }

        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }
        #endregion
    }
}
