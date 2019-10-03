using Finance.Secure.Extensions;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Finance.Secure.Crypto
{
    public static class HashManager
    {
        public static string GetHashString(SecureString input)
        {
            StringBuilder sb = new StringBuilder();
            byte[] hashBytes = GetHash(input);
            hashBytes.ToList().ForEach(b => sb.Append(b.ToString()));
            return sb.ToString();
        }
        
        public static bool Match(SecureString secureInput, string hashString)
        {
            return GetHashString(secureInput) == hashString;
        }

        private static byte[] GetHash(SecureString input)
        {
            using (SHA512 computer = SHA512.Create())
            {
                return computer.ComputeHash(input.GetBytes());
            }
        }

    }
}
