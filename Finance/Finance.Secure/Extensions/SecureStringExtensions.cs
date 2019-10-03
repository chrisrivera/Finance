using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Finance.Secure.Extensions
{
    public static class SecureStringExtensions
    {
        public static byte[] GetBytes(this SecureString ss)
        {
            IntPtr unmanagedBytes = IntPtr.Zero;
            try
            {
                unmanagedBytes = Marshal.SecureStringToGlobalAllocUnicode(ss);

                //TODO: validate safety...
                string unprotectedString = Marshal.PtrToStringUni(unmanagedBytes);
                return new ASCIIEncoding().GetBytes(unprotectedString);
            }
            finally
            {
                // This will completely remove the data from memory
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedBytes);
            }
        }
    }
}
