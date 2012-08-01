using System.Web.Mvc;
using FinancialIQ.Models;
using FinancialIQ.Service;

namespace FinancialIQ.Controllers
{
    public class GraphController : BaseController
    {
        private readonly IMoneyService _moneyService;

        public GraphController(IMoneyService moneyService)
        {
            _moneyService = moneyService;
        }
        [Authorize]
        public ViewResult GraphFlows()
        {
            var graphData = _moneyService.GetFlowsByMonth(UserId);
            var viewModel = new GraphViewModel {GraphData = graphData};
            return View(viewModel);
        }
    }
}