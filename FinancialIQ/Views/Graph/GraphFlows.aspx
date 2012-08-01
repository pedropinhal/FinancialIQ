<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FinancialIQ.Models.GraphViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Income & Expenses graph
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Income & Expenses graph</h2>
<div id="savings"></div>

<script type="text/javascript">

  var savings_data = [
  <% foreach (var m in Model.GraphData) { %>
    { date: '<%: m.Date.ToString("yyyy-MM") %>', income: <%: m.Income %>, expenses: <%: m.Expenses %>, savings: <%: m.Savings %> , },
  <% } %>
    
  ];


    $(document).ready(function () {
      Morris.Line({
        element: 'savings',
        data: savings_data,
        xkey: 'date',
        ykeys: ['income', 'expenses', 'savings'],
        labels: ['Income', 'Expenses', 'Savings']

      });
    });
  
  
</script>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
<script type="text/javascript" src="<%: Url.Content("~/Scripts/graph/raphael-min.js")%>"></script>
<script type="text/javascript" src="<%: Url.Content("~/Scripts/graph/morris.min.js")%>"></script>
</asp:Content>