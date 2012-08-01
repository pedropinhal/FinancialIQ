using NUnit.Framework;
using FinancialIQ.Code;

namespace FinancialIQ.Tests
{
    [TestFixture]
    public class ExtensionTests
    {
        [Test]
        public void ToTitleCase_Works()
        {
            // Arrange
            var input ="fIrSt sEcOND";

            // Act // Assert
            Assert.That(input.ToTitleCase(), Is.EqualTo("First Second"));

        }
    }
}
