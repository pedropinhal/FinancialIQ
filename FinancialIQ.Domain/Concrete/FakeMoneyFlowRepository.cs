using System;
using System.Collections.Generic;
using System.Linq;
using FinancialIQ.Domain.Abstract;
using FinancialIQ.Domain.Data;


namespace FinancialIQ.Domain.Concrete
{
    public class FakeMoneyFlowRepository : IMoneyFlowRepository
    {
        IQueryable<MoneyFlow> _fakeLog = new List<MoneyFlow> {
            new MoneyFlow{ Date = DateTime.Now, Item = "Groceries shopping at tesco", Debit = 6.78m, Category = "Food"},
            new MoneyFlow{ Date = DateTime.Now, Item = "Boxing subs", Debit = 4.78m, Category = "Health"},
            new MoneyFlow{ Date = DateTime.Now, Item = "Weekly travelcard", Debit = 27.70m, Category = "Transport"}
        }.AsQueryable();
        public IQueryable<MoneyFlow> Log {
            get {
                return _fakeLog;
            }
        }

        public void SaveItem(MoneyFlow moneyFlow) {
            throw new NotImplementedException();
        }

        public void SaveItem(int userId, MoneyFlow moneyFlow) {
            throw new NotImplementedException();
        }

        public IQueryable<MoneyFlow> GetFlows(int userId) {
            throw new NotImplementedException();
        }

        public Tuple<List<string>, List<string>> DistinctCategories()
        {
            throw new NotImplementedException();
        }
    }
}
