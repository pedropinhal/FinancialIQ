using FinancialIQ.Domain.Entities;
using NUnit.Framework;

namespace FinancialIQ.Tests
{
    [TestFixture]
    public class SecurityTests
    {
        [Test]
        public void CreateHashedPassword()
        {
            const string password = "password";
            const string username = "username";
            string hashedPassword = Security.CreateHashedPassword(password, username);
            string hash = "F9e3D3PKX81HixbcZi5hyTnoN5U=";
            Assert.That(hashedPassword, Is.EqualTo(hash));
        }
    }
}
