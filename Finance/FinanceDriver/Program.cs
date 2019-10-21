using Finance.DataAccess.Models;
using Finance.Secure.Crypto;
using Finance.Secure.Crypto.CryptographicProvider;
using Finance.Secure.Interfaces;
using Finance.Secure.Password;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Linq;
using System.Security;

/*********************************************************
the idea here is to manage and update financial records

REQUIREMENTS:
===================
✔  need records to be stored in database (encrypted)
✔  password storage
✔  .net core
✔  dependancy injection for unit testing

    userName: test
    password: Password1

*********************************************************/

namespace FinanceDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup Dependency Injection
            ServiceProvider diServiceProvider = new ServiceCollection()
                                .AddSingleton<ILoggerFactory, LoggerFactory>()
                                .AddLogging(bldr =>
                                {
                                    bldr.AddConsole();
                                })
                                //add program specific implementations for interfaces
                                .AddSingleton<ICryptographicProvider, AESProvider>()
                                .BuildServiceProvider();

            ILogger logger = diServiceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

            try
            {
                Console.WriteLine("Please Log In");
                Console.WriteLine("-----------------");
                Console.Write("Username: ");
                string userName = Console.ReadLine();
                Console.Write("Password: ");
                SecureString secPassword = ConsoleManager.GetPassword();
                Console.WriteLine();
                Console.WriteLine();
                
                logger.LogInformation("Validation against live database...\r\n");

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
                            logger.LogInformation($"user: {userName} authenticated...\r\n");

                            ICryptographicProvider provider = diServiceProvider.GetService<ICryptographicProvider>();
                            provider.Inititalize(secPassword);

                            var encrypted = provider.Encrypt("this is a test");
                            Console.WriteLine(encrypted);
                            Console.WriteLine();
                            var decrypted = provider.Decrypt(encrypted);
                            Console.WriteLine(decrypted);
                        }
                        else {
                            logger.LogInformation("incorrect password provided...\r\n"); 
                            Console.WriteLine("invalid user/pass."); 
                        }
                    }
                    else {
                        logger.LogInformation("user not found...\r\n");
                        Console.WriteLine("invalid user/pass."); 
                    }

                }//end using

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured...\r\n");
            }
        } 
    }
}
