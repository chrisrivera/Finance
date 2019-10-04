using Finance.Secure.Extensions;
using System.Security;
using System.Security.Cryptography;

namespace Finance.Secure.Crypto
{
    public static class HashManager
    {        
        public static bool Match(SecureString secureInput, byte[] hashVal)
        {
            byte[] secPass = GetHash(secureInput);
            if (secPass.Length != hashVal.Length) { return false; }
            for(int i = 0; i < secPass.Length; i++)
            {
                if (secPass[i] != hashVal[i]) { return false; }
            }
            return true;
        }

        public static byte[] GetHash(SecureString input)
        {
            using (SHA512 computer = SHA512.Create())
            {
                return computer.ComputeHash(input.GetBytes());
            }
        }

    }
}
