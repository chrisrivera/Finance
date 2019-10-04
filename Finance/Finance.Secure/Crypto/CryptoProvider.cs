using Finance.Secure.Extensions;
using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Finance.Secure.Crypto
{
    public static class CryptoProvider
    {
        private static int ITERATION_CYCLES = 2;
        private static int KEY_SIZE = 256;
        private static string HASH = "SHA1";
        private static string SALT_VAL = "gHUjLKk40DTB9f81";
        private static string VECTOR = "Y4A32EZ8B0NbQszp";

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <typeparam name="T">Valid Crypto Algorithms:
        /// AesManaged, RijndaelManaged, DESCryptoServiceProvider, RC2CryptoServiceProvider, TripleDESCryptoServiceProvider</typeparam>
        /// <param name="value">value to encrypt</param>
        /// <param name="password">password to combine with the encryption</param>
        /// <returns>encrypted string</returns>
        public static string Encrypt<T>(string value, SecureString password) where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = GetBytes<ASCIIEncoding>(VECTOR);
            byte[] saltBytes = GetBytes<ASCIIEncoding>(SALT_VAL);
            byte[] valueBytes = GetBytes<UTF8Encoding>(value);
            
            byte[] encrypted;
            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password.GetBytes(), saltBytes, HASH, ITERATION_CYCLES);
                byte[] keyBytes = _passwordBytes.GetBytes(KEY_SIZE / 8);

                cipher.Mode = CipherMode.CBC;

                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <typeparam name="T">Valid Crypto Algorithms:
        /// AesManaged, RijndaelManaged, DESCryptoServiceProvider, RC2CryptoServiceProvider, TripleDESCryptoServiceProvider</typeparam>
        /// <param name="value">encrypted value</param>
        /// <param name="password">password to combine with the decryption</param>
        /// <returns>decrypted string value</returns>
        public static string Decrypt<T>(string value, SecureString password) where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = GetBytes<ASCIIEncoding>(VECTOR);
            byte[] saltBytes = GetBytes<ASCIIEncoding>(SALT_VAL);
            byte[] valueBytes = Convert.FromBase64String(value);
            byte[] decrypted;
            int decryptedByteCount = 0;

            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password.GetBytes(), saltBytes, HASH, ITERATION_CYCLES);
                byte[] keyBytes = _passwordBytes.GetBytes(KEY_SIZE / 8);

                cipher.Mode = CipherMode.CBC;

                try
                {
                    using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream from = new MemoryStream(valueBytes))
                        {
                            using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                            {
                                decrypted = new byte[valueBytes.Length];
                                decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                            }
                        }
                    }
                }
                catch { return string.Empty; }
                cipher.Clear();
            }
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }

        private static byte[] GetBytes<T>(string input) where T : Encoding
        {
            return Activator.CreateInstance<T>().GetBytes(input);
        }
    }
}
