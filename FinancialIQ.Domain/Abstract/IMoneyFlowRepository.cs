using System;
using System.Collections.Generic;
using System.Linq;
using FinancialIQ.Domain.Data;


namespace FinancialIQ.Domain.Abstract
{
    public interface IMoneyFlowRepository
    {
        void SaveItem(int userId, MoneyFlow moneyFlow);
        IQueryable<MoneyFlow> GetFlows(int userId);
        Tuple<List<string>, List<string>> DistinctCategories();
    }
}
