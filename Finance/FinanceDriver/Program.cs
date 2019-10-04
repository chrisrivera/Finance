using Finance.DataAccess.Models;
using Finance.Secure.Crypto;
using Finance.Secure.Password;
using System;
using System.Linq;
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
            ✔  need records to be stored in database (encrypted)
            ✔  password storage
            ✔  .net core
            ☐  data layer extracted via interface
            ☐  dependancy injection for unit testing

                userName: test
                password: Password1

            *********************************************************/
            Console.Write("Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            SecureString secPassword = ConsoleManager.GetPassword();
            Console.WriteLine();
            Console.WriteLine();
            

            using (FinanceContext context = new FinanceContext())
            {
                //var hash = HashManager.GetHash(secPassword);
                //context.Add(new User() { UserName = userName, Password = hash });
                //context.SaveChanges();

                var foundUser = context.User.Where(u => u.UserName == userName).FirstOrDefault();
                if (foundUser != null)
                {
                    if (HashManager.Match(secPassword, foundUser.Password))
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
                else
                {
                    Console.WriteLine("invalid user/pass.");
                }
            }


         } 

    }
}
