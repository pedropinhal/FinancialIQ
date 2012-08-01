using System;
using System.Collections.Generic;
using FinancialIQ.Controllers;
using FinancialIQ.Models;
using FinancialIQ.Service;
using Moq;
using NUnit.Framework;

namespace FinancialIQ.Tests
{
    [TestFixture]
    public class GraphControllerTests
    {
        readonly Mock<IMoneyService> _moneyService = new Mock<IMoneyService>();
        private GraphController _controller;
        const int UserId = 1;
        [SetUp]
        public void Setup()
        {
         
            _controller = new GraphController(_moneyService.Object);

            
            _controller.ControllerContext = UnitTestHelpers.GetControllerContextWithUserId(UserId);
        }
        [Test]
        public void GraphFlows_WithValidData_ReturnsCorrectViewModel()
        {

            // Arrange
            _moneyService.Setup(m => m.GetFlowsByMonth(UserId)).Returns(new List<ReportItem>
                                                                       {
                                                                           new ReportItem { Date = new DateTime(2012, 1, 1), Income = 10, Expenses = 5, Savings = 5},
                                                                           new ReportItem { Date = new DateTime(2012, 2, 1), Income = 20, Expenses = 15, Savings = 10},
                                                                       }.AsReadOnly);

            // Act
            var result = _controller.GraphFlows();
            var viewModel = (GraphViewModel)result.Model;

            // Assert
            Assert.That(viewModel.GraphData.Count, Is.EqualTo(2));
            

        }
    }
}
