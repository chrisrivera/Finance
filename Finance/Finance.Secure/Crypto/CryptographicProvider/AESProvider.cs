using Finance.Secure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Security;
using System.Security.Cryptography;

namespace Finance.Secure.Crypto.CryptographicProvider
{
    public class AESProvider : ICryptographicProvider
    {
        private readonly ILogger<AESProvider> _logger;
        private SecureString _password;

        public AESProvider(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AESProvider>();
        }

        public void Inititalize(SecureString secPass)
        {
            _password = secPass;
            _logger.LogInformation("Secure Password set...");
        }

        public string Decrypt(string encValue)
        {
            if (_password == null) { throw new InvalidOperationException("Secure Password not provided"); }
            return CryptoProvider.Decrypt<AesManaged>(encValue, _password);
        }

        public string Encrypt(string value)
        {
            if (_password == null) { throw new InvalidOperationException("Secure Password not provided"); }
            return CryptoProvider.Encrypt<AesManaged>(value, _password);
        }
    }
}
