using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FinancialIQ.Code
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString MonthNav(this HtmlHelper helper, int year, int month)
        {
            Func<DateTime, string,string,string, string> gen = (d, b, f, a) =>
            {
                var linkText = string.Format("{0}{1}{2}",b, d.ToString(f),a);
                
                var s = helper.ActionLink(linkText
                    , "MoneyLog", "Money", new { year = d.Year, month = d.Month }, new { @class = "btn" });
                return s.ToString();
            };
            var date = new DateTime(year, month, 1);
            var result = new StringBuilder();
            result.Append(gen.Invoke(date.AddYears(-1) , "< ", "yyyy", ""));
            result.Append(gen.Invoke(date.AddMonths(-1), "◄ ", "MMMM",""));
            result.Append(gen.Invoke(date, "", "MMMM", ""));
            result.Append(gen.Invoke(date.AddMonths(1), "", "MMMM", " ►"));
            result.Append(gen.Invoke(date.AddYears(1), "", "yyyy", " >"));

            return new MvcHtmlString(result.ToString());
        }

        public static MvcHtmlString MenuTab(this HtmlHelper htmlHelper, IEnumerable<MenuTab> tabs)
        {
            var routeData = htmlHelper.ViewContext.RequestContext.RouteData;
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");

            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("nav");
            foreach (var menuTab in tabs)
            {
                TagBuilder liTag = new TagBuilder("li");
                if (menuTab.Controller.Equals(controller) && menuTab.Action.Equals(action))
                    liTag.AddCssClass("active");
                liTag.InnerHtml = htmlHelper.ActionLink(menuTab.Text, menuTab.Action, menuTab.Controller).ToString();
                ulTag.InnerHtml += liTag.ToString();
            }
            return MvcHtmlString.Create(ulTag.ToString());

        }
    }
}