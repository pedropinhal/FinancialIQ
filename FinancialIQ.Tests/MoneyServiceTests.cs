using System;
using System.Collections.Generic;
using System.Linq;
using FinancialIQ.Domain.Abstract;
using FinancialIQ.Domain.Data;
using FinancialIQ.Models;
using FinancialIQ.Service;
using Moq;
using NUnit.Framework;

namespace FinancialIQ.Tests
{
    [TestFixture]
    public class MoneyServiceTests
    {
        readonly Mock<IMoneyFlowRepository> _moneyRepo = new Mock<IMoneyFlowRepository>();

        [SetUp]
        public void Setup()
        {
            _moneyRepo.Setup(m => m.GetFlows(1)).Returns(GetSampleLogs());
        }

        private static IQueryable<MoneyFlow> GetSampleLogs()
        {
            return new List<MoneyFlow> 
            {
                new MoneyFlow { Date = DateTime.Now, Category = "Food", Debit = .50m, Id = 1},
                new MoneyFlow { Date = DateTime.Now, Category = "Health", Debit = .75m, Id = 2},
                new MoneyFlow { Date = DateTime.Now, Category = "Food", Debit = 1m, Id = 3},
                new MoneyFlow { Date = DateTime.Now, Category = "Income", Subcategory = "Salary", Credit = 150m, Id = 4},
                new MoneyFlow { Date = DateTime.Now, Category = "Income", Subcategory = "Refunds", Credit = 25m, Id = 5},
            }.AsQueryable();
        }
        
        [Test]
        public void Can_Group_By()
        {
            // Arrange
            var service = new MoneyService(_moneyRepo.Object);
            var list = new List<MoneyFlow> {
                new MoneyFlow { Date = DateTime.Now, Category = "Food", Subcategory = "Groceries", Debit = .50m},
                new MoneyFlow { Date = DateTime.Now, Category = "Food",  Subcategory = "Groceries", Debit = 1m },
                new MoneyFlow { Date = DateTime.Now, Category = "Health",  Subcategory = "Drugs", Debit = .75m },
                
            };

            // Act
            var result = service.Collapse(list);
            
            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Category, Is.EqualTo("Food"));
            Assert.That(result[0].SubCategory, Is.EqualTo("Groceries"));
            Assert.That(result[0].Pounds, Is.EqualTo(-1.5m));

            Assert.That(result[1].Category, Is.EqualTo("Health"));
            Assert.That(result[1].SubCategory, Is.EqualTo("Drugs"));
            Assert.That(result[1].Pounds, Is.EqualTo(-0.75m));


        }

        [Test]
        public void AddItem_WithValidLogEntry_CallsRep()
        {
            // Arrange 
            _moneyRepo.Setup(r => r.SaveItem(It.IsAny<int>(), It.IsAny<MoneyFlow>()));
            var service = new MoneyService(_moneyRepo.Object);
            var item = new LogEntry { Description = "description", Value = 1, Category = "Category", Subcategory = "Subcategory"};
            
            // Act 
            service.AddItem(1, item);
            
            // Assert           
            _moneyRepo.Verify(r => r.SaveItem(It.IsAny<int>(), It.IsAny<MoneyFlow>()), Times.Once());
        }

        [Test]
        public void GetMoneyLogs_WithValidRepo_ShouldReturnItemsAtBeginningAndEndOfMonth()
        {
            // Arrange 
            _moneyRepo.Setup(r => r.SaveItem(It.IsAny<int>(), It.IsAny<MoneyFlow>()));
            var service = new MoneyService(_moneyRepo.Object);
            _moneyRepo.Setup(m => m.GetFlows(It.IsAny<int>())).Returns(new List<MoneyFlow> 
            {
                new MoneyFlow { Date = DateTime.Parse("01/04/2012"), Id = 1 },
                new MoneyFlow { Date = DateTime.Parse("30/04/2012"), Id = 2},
            }.AsQueryable());
            // Act 

            var result = service.GetMoneyLogs(1, DateTime.Parse("07/04/2012"));

            // Assert           
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo(1));
            Assert.That(result[1].Id, Is.EqualTo(2));
        }

        [Test]
        public void GetFlowsByMonth_WithValidData_ReturnsCorrectIncome()
        {
            // Arrange 
            var service = new MoneyService(_moneyRepo.Object);
            _moneyRepo.Setup(m => m.GetFlows(It.IsAny<int>())).Returns(new List<MoneyFlow> 
            {
                new MoneyFlow { Date = DateTime.Parse("01/04/2012"), Credit = 1 },
                new MoneyFlow { Date = DateTime.Parse("01/05/2012"), Credit = 1 }
            }.AsQueryable());
          
            // Act 
            var result = service.GetFlowsByMonth(1);

            // Assert           
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Date.Month, Is.EqualTo(4));
            Assert.That(result[0].Income, Is.EqualTo(1));

            Assert.That(result[1].Date.Month, Is.EqualTo(5));
            Assert.That(result[1].Income, Is.EqualTo(1));
            
        }

        [Test]
        public void GetFlowsByMonth_WithValidData_ReturnsCorrectExpenses()
        {
            // Arrange 
            var service = new MoneyService(_moneyRepo.Object);
            _moneyRepo.Setup(m => m.GetFlows(It.IsAny<int>())).Returns(new List<MoneyFlow> 
            {
                new MoneyFlow { Date = DateTime.Parse("01/05/2012"), Debit = 1},
                new MoneyFlow { Date = DateTime.Parse("01/07/2012"), Debit = 1},
            }.AsQueryable());

            // Act 
            var result = service.GetFlowsByMonth(1);

            // Assert           
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Date.Month, Is.EqualTo(5));
            Assert.That(result[0].Expenses, Is.EqualTo(1));

            Assert.That(result[1].Date.Month, Is.EqualTo(7));
            Assert.That(result[1].Expenses, Is.EqualTo(1));
        }

        [Test]
        public void GetFlowsByMonth_WithValidData_ReturnsCorrectSavings()
        {
            // Arrange 
            var service = new MoneyService(_moneyRepo.Object);
            _moneyRepo.Setup(m => m.GetFlows(It.IsAny<int>())).Returns(new List<MoneyFlow> 
            {
                new MoneyFlow { Date = DateTime.Parse("01/01/2012"), Credit = 1},
                new MoneyFlow { Date = DateTime.Parse("01/02/2012"), Debit = 1},
                new MoneyFlow { Date = DateTime.Parse("01/02/2012"), Debit = 2},
                new MoneyFlow { Date = DateTime.Parse("01/03/2012"), Credit = 3},
            }.AsQueryable());

            // Act 
            var result = service.GetFlowsByMonth(1);

            // Assert           
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].Date.Month, Is.EqualTo(1));
            Assert.That(result[0].Savings, Is.EqualTo(1));

            Assert.That(result[1].Date.Month, Is.EqualTo(2));
            Assert.That(result[1].Savings, Is.EqualTo(-2));

            Assert.That(result[2].Date.Month, Is.EqualTo(3));
            Assert.That(result[2].Savings, Is.EqualTo(1));
        }


        
    }
}
