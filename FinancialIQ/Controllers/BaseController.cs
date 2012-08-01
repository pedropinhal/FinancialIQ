using System.Web.Mvc;

namespace FinancialIQ.Controllers
{
    public class BaseController : Controller
    {
        public int UserId
        {
            get { return ((Code.CustomPrincipal)(User)).UserId; }
        }
    }
}