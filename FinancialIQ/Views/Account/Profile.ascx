<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FinancialIQ.Models.ProfileViewModel>" %>
<div class="btn-group pull-right">
  <a class="btn dropdown-toggle" data-toggle="dropdown" data-target="#" href="path/to/page.html">
    <i class="icon-user"></i>
       <%:Model.Username%>
    <span class="caret"></span></a>
    <ul class="dropdown-menu">
    <% if (Model.IsLoggedIn) { %>
      <li><a href="#">Profile</a></li>
      <li class="divider"></li>
      <li><%: Html.ActionLink("Sign out", "SignOut") %></li>
    <% } else { %>
      <li><%: Html.ActionLink("Sign in", "Login") %></li>
    <% } %>
    </ul>
</div>
