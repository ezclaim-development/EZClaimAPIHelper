using System;
using System.IO;
using System.Security.Cryptography;

namespace EZClaimAPIHelper
{
    /// <summary>
    /// A helper for encrypting and decrypting AES items
    /// </summary>
    /// <remarks>
    /// Helpful resource: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-5.0#code-try-2
    /// </remarks>
    public static class AESHelper
    {
        public static string DecryptStringFromBytes(byte[] encryptedString, byte[] key, byte[] iv)
        {
            string decryptedString = null;

            CheckIfByteNullOrEmpty(encryptedString, "encryptedString");
            CheckIfByteNullOrEmpty(key, "key");
            CheckIfByteNullOrEmpty(iv, "iv");

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(encryptedString))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            decryptedString = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedString;
        }

        public static byte[] EncryptStringToBytes(string startingString, byte[] key, byte[] iv)
        {

            CheckIfByteNullOrEmpty(key, "key");
            CheckIfByteNullOrEmpty(iv, "iv");

            byte[] encryptedString;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(startingString);
                        }

                        encryptedString = memoryStream.ToArray();
                    }
                }
            }

            return encryptedString;
        }

        private static void CheckIfByteNullOrEmpty(byte[] checkByte, string checkByteName)
        {
            if (checkByte == null || checkByte.Length <= 0)
            {
                throw new ArgumentNullException(checkByteName);
            }
        }
    }
}
