<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FinancialIQ.Models.LogEntry>" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AddItem
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.EnableUnobtrusiveJavaScript(); %>
<div class="page-header">
  <h2>Add new item <small>add a new bill, transaction, salary or drug purchase</small></h2>
</div>
<div class="row">

     <% using (Html.BeginForm("AddItem", "Money", FormMethod.Post, new { @class = "form-horizontal" })) { %>
     
        <fieldset>
          <div class="control-group">
            <label class="control-label" for="Description">Description</label>
            <div class="controls">
              <%: Html.TextBoxFor(x => x.Description, new { @class = "input-xlarge", placeholder = "bill etc"})%>
              <%: Html.ValidationMessageFor(x => x.Description) %>
            </div>
          </div>
          <div class="control-group">
            <label class="control-label">Amount</label>
              <div class="controls">
                <div class="input-prepend ">
                  <span class="add-on">£</span>
                   <%: Html.TextBoxFor(x => x.Value, new { @class = "span2" })%>
                    <%: Html.ValidationMessageFor(x => x.Value)%>
                </div>
                
                <label class="radio inline">
                  <input type="radio" value="credit" name="Direction"  /> In   
                </label>
                <label class="radio inline">
                  <input type="radio" value="debit" name="Direction" checked /> Out
                </label>  
                
              </div>
          </div>
          <div class="control-group">
             <label class="control-label" for="Category">Category</label>
            <div class="controls">
               <%: Html.TextBoxFor(x => x.Category, new { data_provide = "typeahead", autocomplete = "off", data_items = 4, data_source = new JavaScriptSerializer().Serialize(Model.DistinctCategories) })%>
               <%: Html.ValidationMessageFor(x => x.Category) %>
            </div>
          </div>
          <div class="control-group">
           <label class="control-label" for="select01">Sub-category</label>
            <div class="controls">
              <%: Html.TextBoxFor(x => x.Subcategory, new { data_provide = "typeahead", autocomplete = "off", data_items = 4, data_source = new JavaScriptSerializer().Serialize(Model.DistinctSubCategories) })%>
            </div>
          </div>
          <div class="form-actions">
            <button type="submit" class="btn btn-primary">Add item</button>
          </div>
        </fieldset>
      <% } %>
  </div>

 
  
  



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JSContent" runat="server">

  <script src="<%: Url.Content("~/Content/bootstrap/js/bootstrap-typeahead.js")%>" type="text/javascript"></script>
 <%Html.RenderPartial("validation"); %>
 
</asp:Content>
