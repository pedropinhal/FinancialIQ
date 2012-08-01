<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FinancialIQ.Models.MonthlyTotalViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MonthlyTotals
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .cat{
        vertical-align:middle !important;
        text-align:center !important;
    }
  </style>
<div class="span8">
  <h1>Monthly Totals</h1>
  <% using(Html.BeginForm("MonthlyTotals","Money", FormMethod.Get)) { %>
    <input type="text" name="filterMonth"/>
    <input type="submit" value="Filter" />
  <% } %>
  
  <% Html.RenderPartial("SummarisedTable", Tuple.Create(Model.Income, Model.CategoryTotals,"Income")); %>
  
  <% Html.RenderPartial("SummarisedTable", Tuple.Create(Model.Expenses, Model.CategoryTotals,"Expenses")); %>

</div>

</asp:Content>
