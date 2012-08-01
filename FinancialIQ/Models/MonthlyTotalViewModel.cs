using System;
using System.Linq;

namespace FinancialIQ.Models {
    public class MonthlyTotalViewModel 
    {
        public IQueryable<TotalLine> Expenses { get; set; }
        public IQueryable<TotalLine> Income { get; set; }
        public DateTime FilterMonth { get; set; }
        public IQueryable<CategoryTotalLine> CategoryTotals { get; set; }
    }
}