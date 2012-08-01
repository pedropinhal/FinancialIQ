using System;
using System.Collections.Generic;
using System.Linq;
using FinancialIQ.Code;
using FinancialIQ.Domain.Abstract;
using FinancialIQ.Domain.Data;
using FinancialIQ.Models;

namespace FinancialIQ.Service
{
    public class MoneyService : IMoneyService
    {
        private readonly IMoneyFlowRepository _moneyFlowRepository;

        public MoneyService(IMoneyFlowRepository moneyFlowRepository)
        {
            _moneyFlowRepository = moneyFlowRepository;
        }

        public IList<MoneyFlow> GetMoneyLogs(int userId, DateTime? date = null)
        {
            var userLogs = _moneyFlowRepository.GetFlows(userId);
            if (!date.HasValue)
            {
                return userLogs.ToList();
            }

            var startDate = new DateTime(date.Value.Year, date.Value.Month, 1);
            var endDate = startDate.AddMonths(1).AddTicks(-1);

            return userLogs
                .Where(w => (w.Date >= startDate && w.Date <= endDate))
                .OrderBy(l => l.Date)
                .ToList();
        }

        public IList<TotalLine> GetExpenses(int userId, DateTime startDate, DateTime endDate)
        {
            var userLogs = _moneyFlowRepository.GetFlows(userId);

            var log = userLogs
                .Where(w => (w.Date >= startDate && w.Date < endDate) && w.Debit != 0)
                .ToList();

            return Collapse(log);
        }

        public IList<TotalLine> GetIncome(int userId, DateTime startDate, DateTime endDate)
        {
            var userLogs = _moneyFlowRepository.GetFlows(userId);

            var log = userLogs
                .Where(w => (w.Date >= startDate && w.Date < endDate) && w.Credit != 0)
                .ToList();

            return Collapse(log);
        }

        public IList<TotalLine> Collapse(List<MoneyFlow> list)
        {
            var log = list.GroupBy(g => new { g.Category, g.Subcategory })
               .Select(group => new TotalLine
               {
                   Category = group.Key.Category,
                   SubCategory = group.Key.Subcategory,
                   Pounds = group.Sum(l => l.Credit) - group.Sum(l => l.Debit),
                   Energy = CalculateEnergyCost(group.Sum(l => l.Credit) - group.Sum(l => l.Debit))

               })
               .OrderBy(l => l.Category).ToList();
            return log;
        }

        public void AddItem(int userId, LogEntry item)
        {
            var entry = new MoneyFlow
            {
                Id = 0,
                Category = item.Category.ToTitleCase(),
                Subcategory = (string.IsNullOrEmpty(item.Subcategory)) ? string.Empty : item.Subcategory.ToTitleCase(),
                Credit = (item.Direction == "credit") ? item.Value : 0m,
                Debit = (item.Direction == "debit") ? item.Value : 0m,
                Date = DateTime.Now,
                Item = item.Description
            };
            _moneyFlowRepository.SaveItem(userId, entry);

        }

        public IList<ReportItem> GetFlowsByMonth(int userId)
        {
            var userLogs = _moneyFlowRepository.GetFlows(userId);

            decimal cumulativeSavings = 0;
            var result = userLogs
                .GroupBy(g => new { g.Date.Year, g.Date.Month })
                .Select(g => new ReportItem
                                 {
                                     Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                                     Income = g.Sum(c => c.Credit),
                                     Expenses = g.Sum(c => c.Debit)
                                 }
                )
                .OrderBy(o => o.Date)
                .ToList();
            foreach (var reportItem in result)
            {
                reportItem.Savings = (reportItem.Income - reportItem.Expenses) + cumulativeSavings;
                cumulativeSavings = reportItem.Savings;
            }
            return result;
        }

        public LogEntry GetCategories()
        {
            var lists = _moneyFlowRepository.DistinctCategories();
            return new LogEntry
            {
                DistinctCategories = lists.Item1,
                DistinctSubCategories = lists.Item2
            };
        }

        private decimal CalculateEnergyCost(decimal pounds)
        {
            const decimal poundLifeEnergyCostInMinutes = 5.97m;
            return Math.Round((poundLifeEnergyCostInMinutes * Math.Abs(pounds)) / 60, 1);
        }
    }
}