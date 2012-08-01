<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <!-- Main hero unit for a primary marketing message or call to action -->
  <div class="hero-unit">
    <h1>Hello, world!</h1>
    <p>Welcome to financialIQ, the money management app that will get your finances out the shit. Based on principles discussed in Your Money Or Your Life (YMoYL), money becomes easy again.</p>
    <p><a class="btn btn-primary btn-large" href="<%: Url.Action("MoneyLog", "Money") %>">Learn more &raquo;</a></p>
  </div>

  <!-- Example row of columns -->
  <div class="row">
    <div class="span4">
      <h2>Ace Money Management</h2>
      <p>Know where all your money goes by tracking every cent of it.</p>
      <p><a class="btn" href="#">View details &raquo;</a></p>
    </div>
    <div class="span4">
      <h3>Life Energy</h3>
        <p>Find out how much each £ costs you in life energy, time that you won't ever get back.</p>
      <p><a class="btn" href="#">View details &raquo;</a></p>
    </div>
    <div class="span4">
      <h3>Graphs</h3>
        <p>Tott up your accounts at the end of each month and know where you stand.</p>
      <p><a class="btn" href="#">View details &raquo;</a></p>
    </div>
  </div>

  <footer>
    <p>&copy; Company 2012</p>
  </footer>

</asp:Content>
