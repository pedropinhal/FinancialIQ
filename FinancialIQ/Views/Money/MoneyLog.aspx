<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FinancialIQ.Models.MoneyLogViewModel>" %>
<%@ Import Namespace="FinancialIQ.Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
  .out
  {
      color: Red;
  }
  .in
  {
      color: Green;
  }
</style>
<div class="page-header">
  <h1>Money Flows</h1>
</div>
  <div class="row">
    <div class="span4">
      <%: Html.ActionLink("Add item", "AddItem", "Money", null ,new { @class = "btn btn-primary" })%>
    
    </div>
    

    <div class="span5 offset3">
   
      <div class="btn-group">
        <%:Html.MonthNav(Model.CurrentYear, Model.CurrentMonth)%>   
      </div>
    </div>
  
  
  </div>
  <div class="row">
  <div class="span12">
  <h3>Transactions <%:Model.CurrentMonthName %> <%: Model.CurrentYear %></h3>
  <table class="table table-condensed table-bordered">
   <thead>
    <tr>
        <th>Date</th>
        <th>Item</th>
        <th>Debit</th>
        <th>Credit</th>
        <th>Category</th>
        <th>Sub category</th>
    </tr>
    </thead>
  <% foreach (var item in Model.Log) { %>
    <tr>
        <td><%:item.Date.ToShortDateString() %> </td>
        <td><%:item.Item %></td>
        <td class="out">  <% if (item.Debit != 0m) {%><%:item.Debit.ToString("C")%><%}%></td>
        <td class="in">  <% if (item.Credit != 0m) {%><%:item.Credit.ToString("C")%><%}%></td>
        <td><%:item.Category %></td>
        <td><%:item.Subcategory %></td>
    </tr>  
  <% } %>
  <tr><td colspan="2">Total</td>
    <td class="out"><%: Model.TotalOut.ToString("C") %></td>
    <td class="in"><%: Model.TotalIn.ToString("C") %></td>
    <td colspan="2">&nbsp;</td>
  </tr>
  </table>
  </div>
  </div>
</div>


</asp:Content>

