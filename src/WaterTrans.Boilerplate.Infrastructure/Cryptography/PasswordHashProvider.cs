using System;
using System.Linq;
using System.Security.Cryptography;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.Cryptography;

namespace WaterTrans.Boilerplate.Infrastructure.Cryptography
{
    public class PasswordHashProvider : IPasswordHashProvider
    {
        public byte[] Hash(string password, byte[] salt, int iterations)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (salt == null) throw new ArgumentNullException(nameof(salt));

            var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            return deriveBytes.GetBytes(32);
        }

        public bool Verify(string password, byte[] salt, int iterations, byte[] hashedPassword)
        {
            if (hashedPassword == null) throw new ArgumentNullException(nameof(hashedPassword));

            return Hash(password, salt, iterations).SequenceEqual(hashedPassword);
        }
    }
}
