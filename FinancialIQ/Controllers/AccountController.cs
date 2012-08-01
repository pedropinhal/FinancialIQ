using System.Web.Mvc;
using System.Web.Security;
using FinancialIQ.Code;
using FinancialIQ.Models;
using FinancialIQ.Service;

namespace FinancialIQ.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        //
        // GET: /Account/
        public AccountController(IAccountService accountService) {
            _accountService = accountService;
        }
        
        public ActionResult Login(string returnUrl)
        {
            return View( new LoginViewModel{ ReturnUrl = returnUrl});
        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if(!ModelState.IsValid) {
                return View(viewModel);
            }

            int userId = AutheticateUser(viewModel.Username, viewModel.Password);
            if (userId <= 0)
            {
                ModelState.AddModelError("Password", "Invalid login/password");
                return View(viewModel);
            }

            return Redirect(viewModel.ReturnUrl ?? "~/");
        }

        public ViewResult Profile()
        {
            var viewModel = new ProfileViewModel { Username = "Username" };
            viewModel.Username = User.Identity.Name;
            viewModel.IsLoggedIn = User.Identity.IsAuthenticated;
           


            return View(viewModel);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/");
        }

        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var userId = _accountService.CreateUser(viewModel.Username, viewModel.Email, viewModel.Password);
            if (userId <= 0)
            {
                ModelState.AddModelError("Password", "User creation failed");
                return View(viewModel);
            }
            userId = AutheticateUser(viewModel.Username, viewModel.Password);
            if (userId <= 0)
            {
                ModelState.AddModelError("Password", "User login failed");
                return View(viewModel);
            }

            return Redirect("~/");
        }

        private int AutheticateUser(string username, string password)
        {
            int userId = _accountService.AuthenticateUser(username, password);
            if (userId > 0)
            {
                SimpleSessionPersister.Save(username, false, userId);
            }
            return userId;
        }
    }
}
