using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FinancialIQ.Code;
using Moq;
using NUnit.Framework;

namespace FinancialIQ.Tests
{
    public static class UnitTestHelpers
    {
        
        public static void TestRoute(string url, object expectedValues)
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
            var mockHttpContext = MakeMockHttpContext(url);

            // Act
            RouteData routeData = routes.GetRouteData(mockHttpContext.Object);

            var expectedDict = new RouteValueDictionary(expectedValues);

            // Assert
            Assert.That(routeData, Is.Not.Null);
            foreach (var expectedVal in expectedDict)
            {
                Assert.That(routeData.Values[expectedVal.Key].ToString(), Is.EqualTo(expectedVal.Value.ToString()));
            }
        }

        public static string GenerateUrlViaMocks(object values)
        {
            // Arrange
            RouteCollection routeConfig = new RouteCollection();
            MvcApplication.RegisterRoutes(routeConfig);
            var mockHttpContext = MakeMockHttpContext(null);
            RequestContext context = new RequestContext(mockHttpContext.Object, new RouteData());

            // Act
            return UrlHelper.GenerateUrl(null, null, null, new RouteValueDictionary(values), routeConfig, context, true);

        }

        internal static Mock<HttpContextBase> MakeMockHttpContext(string url)
        {
            var mockHttpContext = new Mock<HttpContextBase>();

            // Mock Request
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(mr => mr.AppRelativeCurrentExecutionFilePath).Returns(url);
            mockHttpContext.Setup(m => m.Request).Returns(mockRequest.Object);

            // Mock Response
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(mr => mr.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(mr => mr);
            mockHttpContext.Setup(m => m.Response).Returns(mockResponse.Object);
            return mockHttpContext;
        }

        public static void ShouldBeRedirectionTo(this ActionResult actionResult, object expectedRouteValues)
        {
            var actualValues = ((RedirectToRouteResult)actionResult).RouteValues;
            var expectedValues = new RouteValueDictionary(expectedRouteValues);
            foreach (string key in expectedValues.Keys)
                Assert.That(actualValues[key], Is.EqualTo(expectedValues[key]));
        }

        public static void ShouldBeView(this ActionResult actionResult, string viewName)
        {
            Assert.That(((ViewResult)actionResult).ViewName, Is.EqualTo(viewName));
        }

        public static T WithIncomingValues<T>(this T controller, FormCollection formCollection) where T : Controller
        {
            controller.ValueProvider = formCollection.ToValueProvider();
            controller.ControllerContext = new ControllerContext();
            return controller;
        }

        public static HtmlHelper GetHtmlHelper()
        {
            var mockHttpContext = MakeMockHttpContext(null);
            RouteCollection rt = new RouteCollection();
            MvcApplication.RegisterRoutes(rt);
            RouteData rd = new RouteData();
            rd.Values.Add("controller", "home");
            rd.Values.Add("action", "oldaction");

            ViewDataDictionary vdd = new ViewDataDictionary();

            ViewContext viewContext = new ViewContext()
            {
                HttpContext = mockHttpContext.Object,
                RouteData = rd,
                ViewData = vdd
            };
            Mock<IViewDataContainer> mockVdc = new Mock<IViewDataContainer>();
            mockVdc.Setup(vdc => vdc.ViewData).Returns(vdd);

            HtmlHelper<object> htmlHelper = new HtmlHelper<object>(viewContext, mockVdc.Object, rt);
            return htmlHelper;
        }
        public static ControllerContext GetControllerContextWithUserId(int userId)
        {
            var mockHttpContext = MakeMockHttpContext(null);

            var user = new CustomPrincipal(new CustomIdentity(""), userId);

            mockHttpContext.Setup(c => c.User).Returns(user);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(con => con.HttpContext)
                                 .Returns(mockHttpContext.Object);
            return controllerContextMock.Object;
        }
    }
}