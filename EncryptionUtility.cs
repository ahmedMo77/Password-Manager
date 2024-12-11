using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassManger.Csharp
{
    internal class EncryptionUtility
    {
        private static readonly string OriginalChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly string alternateChars ="J5rYltxGQM7PCD8KHvbo0shzN9aFIi1LOXjcRdEkWqg6yVBSp4m2u3nZTeAUfw";
        internal static string Encrypt(string password)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in password)
            {
                var charIndex = OriginalChars.IndexOf(item);
                sb.Append(alternateChars[charIndex]);
            }
            return sb.ToString();
        }
        internal static string Decrypt(string password)
        {
            StringBuilder SB = new StringBuilder();
            foreach (var item in password)
            {
                var charIndex = alternateChars.IndexOf(item);
                SB.Append(OriginalChars[charIndex]);
            }
            return SB.ToString();
        }
    }
}
