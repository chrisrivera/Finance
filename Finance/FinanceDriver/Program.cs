using Finance.Secure.Crypto;
using Finance.Secure.Password;
using System;
using System.Security;
using System.Security.Cryptography;

namespace FinanceDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            /*********************************************************
            the idea here is to manage and update financial records
            
            REQUIREMENTS:
            ===================
            01  need records to be stored in an encrypted database
            02  password storage
            03  hosted in linux (.net core)
            04  data layer extracted via interface
            05  dependancy injection for unit testing


            *********************************************************/
            Console.Write("Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            SecureString secPassword = ConsoleManager.GetPassword();
            Console.WriteLine();
            Console.WriteLine();

            var encrypted = CryptoProvider.Encrypt<AesManaged>("This is a test", secPassword);
            Console.WriteLine(encrypted);

            var decrypted = CryptoProvider.Decrypt<AesManaged>(encrypted, secPassword);
            Console.WriteLine(decrypted);


         } 

    }
}
