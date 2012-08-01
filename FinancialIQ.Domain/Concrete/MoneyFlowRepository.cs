using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using FinancialIQ.Domain.Abstract;
using FinancialIQ.Domain.Data;

namespace FinancialIQ.Domain.Concrete
{
    public class MoneyFlowRepository : IMoneyFlowRepository
    {
        private readonly IFinancialIqDataContext _context;

        public MoneyFlowRepository(IFinancialIqDataContext context) 
        {
            _context = context;
        }
       
        public void SaveItem(int userId, MoneyFlow moneyFlow) {
            if (moneyFlow.Id == 0)
            {
                moneyFlow.Date = DateTime.Now;
                GetUser(userId).MoneyFlows.Add(moneyFlow);
                _context.SubmitChanges();
            }
            else if (_context.MoneyFlows.GetOriginalEntityState(moneyFlow) == null)
            {
                _context.MoneyFlows.Attach(moneyFlow);
                _context.MoneyFlows.Context.Refresh(RefreshMode.KeepCurrentValues, moneyFlow);
                _context.MoneyFlows.Context.SubmitChanges();
            }
        }

        public IQueryable<MoneyFlow> GetFlows(int userId) {
            return GetUser(userId).MoneyFlows.AsQueryable();
        }

        public Tuple<List<string>, List<string>> DistinctCategories()
        {

            var distinctCategories = _context.MoneyFlows.Select(x => x.Category).Distinct().ToList();
            var distinctSubCategories =
                _context.MoneyFlows.Where(x => x.Subcategory != null).Select(x => x.Subcategory).Distinct().ToList();
            return Tuple.Create(distinctCategories, distinctSubCategories);
        }

        private User GetUser(int userId) {
            return _context.Users.First(u => u.Id == userId);
        }
    }
}
