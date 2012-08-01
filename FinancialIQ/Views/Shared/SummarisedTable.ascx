<%@  Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tuple<IQueryable<FinancialIQ.Models.TotalLine>, IQueryable<FinancialIQ.Models.CategoryTotalLine>, string>>" %>

  <table class="table table-condensed table-bordered">
    <thead>
      <tr>
          <th colspan="2"><%: Model.Item3 %></th>
          <th>pounds</th>
          <th>energy</th>
          <th>Pounds</th>
          <th>Energy</th>
      </tr>
    </thead>
  <% var prevCategory = ""; %>    
  <% foreach (var totalEntry in Model.Item1) {
          var categoryTotal = Model.Item2.First(ct => ct.Category == totalEntry.Category);
          var sci = Model.Item1.Where(t => t.Category == totalEntry.Category).Count().ToString();
  %>

      <tr>
      <% if (totalEntry.Category!=prevCategory) { %>
          <td class="cat"  rowspan="<%: sci%>"><strong><%:totalEntry.Category%></strong></td>
      <% } %>
      
          <td><%: (totalEntry.SubCategory) ?? totalEntry.Category %></td>
          <td><%: totalEntry.Pounds.ToString("C") %></td>
          <td><%: totalEntry.Energy %></td>
          <% if (totalEntry.Category != prevCategory) { %>
              <td class="cat" rowspan="<%: sci%>"><%: categoryTotal.Total.ToString("C")%></td>
              <td class="cat" rowspan="<%: sci%>"><%: categoryTotal.Energy.ToString() %></td>
          <% } %>
            
      </tr>
  <% prevCategory = totalEntry.Category; %>
  <% } %>
    
      <tr>
        <td colspan="4" class="cat">Total</td>
        <td class="cat"><%:Model.Item1.Sum(s => s.Pounds).ToString("C") %></td>
        <td class="cat"><%:Model.Item1.Sum(s => s.Energy) %></td>
      </tr>
  </table>