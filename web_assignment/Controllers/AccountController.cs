using Microsoft.AspNetCore.Mvc;
using web_assignment.Models;

namespace web_assignment.Controllers
{
    public class AccountController : Controller
    {
        private readonly DB db;
        private readonly IWebHostEnvironment en;
        private readonly IConfiguration cf;


        public AccountController(DB db, IWebHostEnvironment en, IConfiguration cf)//The framework will call the constructor and inject the required services
        {
            this.db = db;
            this.en = en;
            this.cf = cf;
        }

        // GET: Account/Login

        public IActionResult Login()
        {
            LoginVM viewModel = new LoginVM();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Login(LoginVM viewModel)
        {
            if (ModelState.IsValid)
            {
                // return RedirectToAction("test");

            }

            return RedirectToAction("test");
        }


        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Register(RegisterVM viewModel)
        {
            if (ModelState.IsValid)
            {
                // Registration logic here
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
