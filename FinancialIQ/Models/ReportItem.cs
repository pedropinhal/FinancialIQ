using System;

namespace FinancialIQ.Models
{
    public class ReportItem
    {
        public DateTime Date { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal Savings { get; set; }
    }
}