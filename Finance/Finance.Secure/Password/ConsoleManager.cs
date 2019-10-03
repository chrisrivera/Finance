using System;
using System.Security;

namespace Finance.Secure.Password
{
    public static class ConsoleManager
    {
        /// <summary>
        /// Retrieves password from console
        /// </summary>
        /// <returns>read-only SecureString</returns>
        public static SecureString GetPassword()
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
