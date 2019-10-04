
using System.Security;

namespace Finance.Secure.Interfaces
{
    public interface ICryptographicProvider
    {
        void Inititalize(SecureString secPass);

        string Encrypt(string value);

        string Decrypt(string encValue);
    }
}
