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
            string hashFromDB = "4122836217125459792202169167144177327742117442151429824718020512814215314792581261921837813723115918712822919133192153205181196112015838237114197251792098811074139180228";
            /*********************************************************
            the idea here is to manage and update financial records
            
            REQUIREMENTS:
            ===================
            01  need records to be stored in database (encrypted)
            02  password storage
            03  .net core
            04  data layer extracted via interface
            05  dependancy injection for unit testing

            *********************************************************/
            Console.Write("Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            SecureString secPassword = ConsoleManager.GetPassword();
            Console.WriteLine();
            Console.WriteLine();


            if (HashManager.Match(secPassword, hashFromDB))
            {
                var encrypted = CryptoProvider.Encrypt<AesManaged>("This is a test", secPassword);
                Console.WriteLine(encrypted);

                var decrypted = CryptoProvider.Decrypt<AesManaged>(encrypted, secPassword);
                Console.WriteLine(decrypted);
            }
            else
            {
                Console.WriteLine("invalid user/pass.");
            }

         } 

    }
}
