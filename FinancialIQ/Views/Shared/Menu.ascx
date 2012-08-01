<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="FinancialIQ.Code" %>

<%  
    var menuTabs = new List<MenuTab>
                {
                    new MenuTab{ Text = "Home", Action = "Home", Controller = "Money"},
                    new MenuTab{ Text = "Money log", Action = "MoneyLog", Controller = "Money"},
                    new MenuTab{ Text = "Monthly totals", Action = "MonthlyTotals", Controller = "Money"},
                    new MenuTab{ Text = "Graphs", Action = "GraphFlows", Controller = "Graph"}
                    
                }; %>
<%: Html.MenuTab(menuTabs)%>

