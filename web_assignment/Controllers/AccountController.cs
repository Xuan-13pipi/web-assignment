using Microsoft.AspNetCore.Mvc;
using web_assignment.Models;

namespace web_assignment.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login

        public IActionResult Login()
        {
            LoginViewModel viewModel = new LoginViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("test");

            }

            return RedirectToAction("test");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
