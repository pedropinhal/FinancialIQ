using System;
using System.Security.Cryptography;

namespace FinancialIQ.Domain.Entities
{
    public class Security
    {
        public static string CreateHashedPassword(string password, string salt)
        {
            var hmacSHA1 = new HMACSHA1(System.Text.Encoding.UTF8.GetBytes(salt));
            var hashedPassword = hmacSHA1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedPassword);
        }
    }
}