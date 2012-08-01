using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Web.Mvc;
using FinancialIQ.Models;
using FinancialIQ.Service;

namespace FinancialIQ.Controllers
{
    [Authorize]
    public class MoneyController : BaseController
    {
        private readonly IMoneyService _moneyService;

        public MoneyController(IMoneyService moneyService)
        {
            _moneyService = moneyService;
        }

        public ActionResult MoneyLog(int? year, int? month)
        {
            var date = DateTime.Now;
            if (year.HasValue && month.HasValue)
            {
                date = new DateTime(year.Value, month.Value, 1);
            }

            var log = _moneyService.GetMoneyLogs(UserId, date);
            var viewModel = new MoneyLogViewModel
            {
                Log = log,
                CurrentMonth = date.Month,
                CurrentYear = date.Year,
                CurrentMonthName = date.ToString("MMMM"),
                TotalIn = log.Sum(l => l.Credit),
                TotalOut = log.Sum(l => l.Debit)
            };

            return View(viewModel);
        }

        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MonthlyTotals(DateTime? filterMonth)
        {
            var startDate = SqlDateTime.MinValue.Value;
            var endDate = SqlDateTime.MaxValue.Value;
            
            if (filterMonth.HasValue)
            {
                startDate = new DateTime(filterMonth.Value.Year, filterMonth.Value.Month, 1);
                endDate = new DateTime(filterMonth.Value.Year, filterMonth.Value.Month,
                    DateTime.DaysInMonth(filterMonth.Value.Year, filterMonth.Value.Month));
            }

            var viewModel = new MonthlyTotalViewModel { FilterMonth = (filterMonth) ?? DateTime.Now };

            var expenses = _moneyService.GetExpenses(UserId, startDate, endDate);
            var income = _moneyService.GetIncome(UserId, startDate, endDate);

            var categoryTotals =
            expenses.Union(income).GroupBy(g => g.Category)
                .Select(group => new CategoryTotalLine
                {
                    Category = group.Key,
                    Total = group.Sum(l => l.Pounds),
                    Energy = group.Sum(l => l.Energy)
                });

            viewModel.Expenses = expenses.AsQueryable();
            viewModel.Income = income.AsQueryable();
            viewModel.CategoryTotals = categoryTotals.AsQueryable();

            return View("MonthlyTotals", viewModel);
        }

        public ViewResult AddItem()
        {
            var viewModel = _moneyService.GetCategories();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddItem(LogEntry item)
        {

            if (ModelState.IsValid)
            {
                _moneyService.AddItem(UserId, item);
                return RedirectToAction("MoneyLog");
            }
            return View(item);
        }
    }
}