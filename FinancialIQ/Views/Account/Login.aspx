<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FinancialIQ.Models.LoginViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LogOn
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<h3>Enter login details</h3>
  <% using (Html.BeginForm("Login", "Account", FormMethod.Post, new { @class = "well" })) { %>
      <div class="control-group">
        <label class="control-label" for="Username">Username</label>
        <div class="controls">
             <%: Html.TextBoxFor(x => x.Username, new { @class = "input-xlarge", placeholder = "please enter username"})%>
             <%: Html.ValidationMessageFor(x => x.Username) %>
        </div>
      </div>
      <div class="control-group">
        <label class="control-label" for="Password">Password</label>
        <div class="controls">
           <%: Html.PasswordFor(x => x.Password, new { @class = "input-xlarge", placeholder = "please password"})%>
           <%: Html.ValidationMessageFor(x => x.Password) %>
        </div>
      </div>
      <%:Html.HiddenFor(m => m.ReturnUrl) %>
      <button type="submit" class="btn btn-primary">Login</button> or <%:Html.ActionLink("register", "Register") %>
  <% } %>




</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
 <%Html.RenderPartial("validation"); %>
</asp:Content>
