using System;
using System.Collections.Generic;
using FinancialIQ.Domain.Data;
using FinancialIQ.Models;

namespace FinancialIQ.Service
{
    public interface IMoneyService
    {
        IList<MoneyFlow> GetMoneyLogs(int userId, DateTime? date);
        IList<TotalLine> GetExpenses(int userId, DateTime startDate, DateTime endDate);
        IList<TotalLine> GetIncome(int userId, DateTime startDate, DateTime endDate);
        IList<TotalLine> Collapse(List<MoneyFlow> list);
        void AddItem(int userId, LogEntry item);
        IList<ReportItem> GetFlowsByMonth(int userId);
        LogEntry GetCategories();
    }
}