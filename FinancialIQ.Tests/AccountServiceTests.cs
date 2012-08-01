using System;
using System.Collections.Generic;
using System.Linq;
using FinancialIQ.Domain.Abstract;
using FinancialIQ.Domain.Data;
using FinancialIQ.Service;
using Moq;
using NUnit.Framework;

namespace FinancialIQ.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IUserRepository> _users;

        [SetUp]
        public void Setup()
        {
            _users= new Mock<IUserRepository>();
        }

        [Test]
        public void AuthenticateUser_WithValidUsernamePassword_ReturnsUserId()
        {
            // Arrange 
            const string username = "username";
            const string password = "password";
            const int userId = 1;
            var service = new AccountService(_users.Object);
            
            _users.Setup(u => u.AuthenticateUser(username, password)).Returns(userId);
            // Act 
            var result = service.AuthenticateUser(username, password);

            // Assert
            Assert.That(result, Is.EqualTo(userId));

        }

        [Test]
        public void AuthenticateUser_WithInvalidUsernamePassword_ReturnsZero()
        {
            // Arrange 
            var service = new AccountService(_users.Object);

            // Act 
            var result = service.AuthenticateUser("invalid", "invalid");

            // Assert
            Assert.That(result, Is.EqualTo(0));

        }

        [Test]
        public void AuthenticateUser_WithMultipleUsernamePassword_ThrowsException()
        {
            // Arrange
            
            
            _users.Setup(u => u.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Throws
                <InvalidOperationException>();
            var service = new AccountService(_users.Object);

            // Act 
            var ex = Assert.Throws<InvalidOperationException>(() => service.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>()));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Operation is not valid due to the current state of the object."));

        }

        [Test]
        public void CreateUser_WithValidUsernamePassword_ReturnsUserId()
        {
            // Arrange 
            const string username = "username";
            const string password = "password";
            const string email = "email";
            const int userId = 1;
            var service = new AccountService(_users.Object);
            var createdUser = new User{ Id = userId};
            _users.Setup(u => u.CreateUser(It.IsAny<User>())).Returns(createdUser);
            // Act 
            
            var result = service.CreateUser(username, email, password);

            // Assert
            Assert.That(result, Is.EqualTo(userId));

        }
    }
}
