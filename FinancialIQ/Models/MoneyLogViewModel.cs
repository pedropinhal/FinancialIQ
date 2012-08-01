using System.Collections.Generic;
using FinancialIQ.Domain.Data;

namespace FinancialIQ.Models {
    public class MoneyLogViewModel 
    {
        public IList<MoneyFlow> Log { get; set; }

        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public decimal TotalOut { get; set; }
        public decimal TotalIn { get; set; }

        public string CurrentMonthName { get; set; }
    }
}