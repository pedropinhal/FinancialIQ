using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FinancialIQ.Code;
using NUnit.Framework;

namespace FinancialIQ.Tests
{
    [TestFixture]
    public class HelperTests
    {
        [Test]
        public void Can_Generate_Month_Navigation()
        {
            // Arrange
            HtmlHelper html = UnitTestHelpers.GetHtmlHelper();
            var routeData = html.ViewContext.RequestContext.RouteData;

            routeData.Values["controller"] = "Money";
            routeData.Values["action"] = "MoneyLog";

            // Act
            MvcHtmlString result = html.MonthNav(2012, 04);

            // Assert
            Assert.That(result.ToString(), Is.EqualTo(
                @"<a class=""btn"" href=""/Money/MoneyLog/2011/4"">&lt; 2011</a><a class=""btn"" href=""/Money/MoneyLog/2012/3"">◄ March</a><a class=""btn"" href=""/Money/MoneyLog/2012/4"">April</a><a class=""btn"" href=""/Money/MoneyLog/2012/5"">May ►</a><a class=""btn"" href=""/Money/MoneyLog/2013/4"">2013 &gt;</a>"));
        }

        [Test]
        public void Can_Create_Menu_Tab()
        {
            // Arrange
            HtmlHelper htmlHelper = UnitTestHelpers.GetHtmlHelper();
            var routeData = htmlHelper.ViewContext.RequestContext.RouteData;

            routeData.Values["controller"] = "Money";
            routeData.Values["action"] = "MoneyLog";

            var menuTabs = new List<MenuTab>
                {
                    new MenuTab { Text = "Home", Action = "Home", Controller = "Money"},
                    new MenuTab { Text = "Money log", Action = "MoneyLog", Controller = "Money"} 
                };

            // Act
            MvcHtmlString result = htmlHelper.MenuTab(menuTabs);
            // Assert
            Assert.That(result.ToString(), Is.EqualTo(@"<ul class=""nav""><li><a href=""/"">Home</a></li><li class=""active""><a href=""/Money/MoneyLog"">Money log</a></li></ul>"));

        }

    }
}
