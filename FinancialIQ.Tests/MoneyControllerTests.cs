using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FinancialIQ.Code;
using FinancialIQ.Controllers;
using FinancialIQ.Domain.Data;
using FinancialIQ.Models;
using FinancialIQ.Service;
using Moq;
using NUnit.Framework;

namespace FinancialIQ.Tests
{
    [TestFixture]
    public class MoneyControllerTests
    {
        readonly Mock<IMoneyService> _moneyService = new Mock<IMoneyService>();
        private MoneyController _controller;
          
        const int UserId = 1;
        private readonly DateTime _date = DateTime.MinValue;
        [SetUp]
        public void Setup() {
            _moneyService.Setup(m => m.GetMoneyLogs(UserId, _date)).Returns(GetSampleLogs());
            _controller = new MoneyController(_moneyService.Object);

            _controller.ControllerContext = UnitTestHelpers.GetControllerContextWithUserId(UserId);
        }

        private static IList<MoneyFlow> GetSampleLogs()
        {
            return new List<MoneyFlow> 
            {
                new MoneyFlow { Date = DateTime.Parse("01/04/2012"), Category = "Food", Debit = 0.5m, Id = 1},
                new MoneyFlow { Date = DateTime.Parse("02/04/2012"), Category = "Health", Debit = 1m, Id = 2},
                new MoneyFlow { Date = DateTime.Parse("03/04/2012"), Category = "Food", Debit = 1.5m, Id = 3},
                new MoneyFlow { Date = DateTime.Parse("04/04/2012"), Category = "Income", Subcategory = "Salary", Credit = 7m, Id = 4},
                new MoneyFlow { Date = DateTime.Parse("05/04/2012"), Category = "Income", Subcategory = "Refunds", Credit = 3m, Id = 5},
            };
        }

        private static IQueryable<MoneyFlow> GetLogsWithSimilarCategories()
        {
            return new List<MoneyFlow> 
            {
                new MoneyFlow { Date = DateTime.Now, Category = "Food", Debit = 1.50m, Id = 1},
                new MoneyFlow { Date = DateTime.Now, Category = "food", Debit = 3.75m, Id = 2},
            }.AsQueryable();
        }

        private static IQueryable<MoneyFlow> GetLogsWithDifferentDates()
        {
            return new List<MoneyFlow> 
            {
                new MoneyFlow { Date = DateTime.Now, Category = "Food", Debit = .50m, Id = 1},
                new MoneyFlow { Date = DateTime.Now.AddDays(-31), Category = "Food", Debit = .25m, Id = 2},
            }.AsQueryable();
        }

        private static IQueryable<MoneyFlow> GetLogsWithTwoItems()
        {
            return new List<MoneyFlow> 
            {
                new MoneyFlow { Date = DateTime.Now, Category = "Food", Subcategory = "Groceries", Debit = 55.67m },
                new MoneyFlow { Date = DateTime.Now, Category = "Food", Subcategory = "Groceries", Debit = 23.52m},
            }.AsQueryable();
        }

        [Test]
        public void AddItem_RendersCorrectView()
        {
            // Arrange 
           // Act 
            var result = _controller.AddItem();

            // Assert           
            Assert.That(result.ViewName, Is.EqualTo(""));

        }

        [Test]
        public void AddItem_WithValidItem_CallsServiceMethod()
        {
            // Arrange 
            var item = new LogEntry {Description = "description", Value = 1, Category = "Category", Subcategory = "Subcategory", Direction = "In"};
            _moneyService.Setup(m => m.AddItem(UserId, item));
            // Act 
            var result = _controller.AddItem(item);

            // Assert           
            result.ShouldBeRedirectionTo(new { action = "MoneyLog"});
            _moneyService.Verify(m => m.AddItem(UserId, item),Times.Once());

        }

        [Test]
        public void MoneyLog_WithValidDates_ViewModelContainsTotal()
        {
            // Arrange
            _moneyService.Setup(m => m.GetMoneyLogs(UserId ,It.IsAny<DateTime>())).Returns(GetSampleLogs());

            // Act
            var result = (ViewResult)_controller.MoneyLog(2012, 4);
            var viewModel = (MoneyLogViewModel)result.Model;
          
            // Assert
            Assert.That(viewModel.TotalOut, Is.EqualTo(3));
            Assert.That(viewModel.TotalIn, Is.EqualTo(10));

        }
        
    }
}
