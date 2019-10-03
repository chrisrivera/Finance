using System;
using System.Security;

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
            SecureString secPassword = GetPassword();



         }

        private static SecureString GetPassword()
        {
            SecureString secPassword = new SecureString();
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) { break; } //exit loop

                if (key.Key != ConsoleKey.Backspace)
                {
                    secPassword.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
                else if (secPassword.Length > 0)
                {
                    secPassword.RemoveAt(secPassword.Length - 1);
                    Console.Write("\b \b");
                }

            } while (true);
            secPassword.MakeReadOnly();
            return secPassword;
        }


    }
}
