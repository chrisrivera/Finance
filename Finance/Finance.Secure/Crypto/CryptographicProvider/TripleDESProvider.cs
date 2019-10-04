using Finance.Secure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Security;
using System.Security.Cryptography;

namespace Finance.Secure.Crypto.CryptographicProvider
{
    public class TripleDESProvider : ICryptographicProvider
    {
        private readonly ILogger<TripleDESProvider> _logger;
        private SecureString _password;

        public TripleDESProvider(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TripleDESProvider>();
        }

        public void Inititalize(SecureString secPass)
        {
            _password = secPass;
            _logger.LogInformation("Secure Password set...");
        }

        public string Decrypt(string encValue)
        {
            if (_password == null) { throw new InvalidOperationException("Secure Password not provided"); }
            return CryptoProvider.Decrypt<TripleDESCryptoServiceProvider>(encValue, _password);
        }

        public string Encrypt(string value)
        {
            if (_password == null) { throw new InvalidOperationException("Secure Password not provided"); }
            return CryptoProvider.Encrypt<TripleDESCryptoServiceProvider>(value, _password);
        }
    }
}
