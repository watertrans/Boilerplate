using System;
using System.Security.Cryptography;

namespace WaterTrans.Boilerplate.Web
{
    public static class CryptUtil
    {
        public static string HashPassword(string password, byte[] salt, int iterations)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (salt == null) throw new ArgumentNullException(nameof(salt));

            var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            return Convert.ToBase64String(deriveBytes.GetBytes(32));
        }

        public static bool VerifyPassword(string password, byte[] salt, int iterations, string hashedPassword)
        {
            if (hashedPassword == null) throw new ArgumentNullException(nameof(hashedPassword));

            return HashPassword(password, salt, iterations) == hashedPassword;
        }
    }
}
