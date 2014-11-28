using System;
using System.Text;
using System.Security.Cryptography;

namespace Wizardsgroup.Utilities.Helpers
{
    /// <summary>
    /// Create By: Ryan D. Lintag
    /// Note: Please see the link below for the details of the salt-hashing encryption methods used in this class
    /// http://www.nathandavison.com/posts/view/2/password-salt-and-hashing-in-c
    /// </summary>
    public class PasswordHelper
    {
        internal const string _salt = "30384C74AE49";
        public static string CreateSalt(string userName)
        {
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(userName,
                Encoding.Default.GetBytes(_salt), 10000);
            return Convert.ToBase64String(hasher.GetBytes(25));
        }
        public static string HashPassword(string salt, string password)
        {
            Rfc2898DeriveBytes Hasher = new Rfc2898DeriveBytes(password,
                Encoding.Default.GetBytes(salt), 10000);
            return Convert.ToBase64String(Hasher.GetBytes(25));
        }
    }
}
