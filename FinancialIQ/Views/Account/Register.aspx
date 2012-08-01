<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FinancialIQ.Models.RegisterViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% Html.EnableUnobtrusiveJavaScript(); %>
<div class="page-header">
  <h2>Register new user <small>please enter your details</small></h2>
   <% using (Html.BeginForm("Register", "Account", FormMethod.Post, new { @class = "well" })) { %>
      <div class="control-group">
        <label class="control-label" for="Username">Username</label>
        <div class="controls">
             <%: Html.TextBoxFor(x => x.Username, new { @class = "input-xlarge", placeholder = "please enter username", autocomplete = "off" })%>
             <%: Html.ValidationMessageFor(x => x.Username) %>
        </div>
      </div>
      <div class="control-group">
        <label class="control-label" for="Email">Email</label>
        <div class="controls">
           <%: Html.TextBoxFor(x => x.Email, new { @class = "input-xlarge", placeholder = "please enter your email address", autocomplete = "off" })%>
           <%: Html.ValidationMessageFor(x => x.Email)%>
        </div>
      </div>
      <div class="control-group">
        <label class="control-label" for="Password">Password</label>
        <div class="controls">
           <%: Html.PasswordFor(x => x.Password, new { @class = "input-xlarge", placeholder = "please enter a strong password", autocomplete = "off" })%>
           <%: Html.ValidationMessageFor(x => x.Password) %>
        </div>
      </div>

      
      <button type="submit" class="btn btn-primary">Create</button>
  <% } %>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">
  <%Html.RenderPartial("validation"); %>
</asp:Content>
