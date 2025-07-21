using Demo;
using Microsoft.AspNetCore.Mvc;
using web_assignment.Models; 


namespace web_assignment.Controllers
{
    public class AccountController : Controller
    {
        private readonly DB db;
        private readonly Helper hp;
        private readonly IWebHostEnvironment en;
        private readonly IConfiguration cf;


        public AccountController(DB db, IWebHostEnvironment en, IConfiguration cf,Helper hp)//The framework will call the constructor and inject the required services
        {
            this.db = db;
            this.en = en;
            this.cf = cf;
            this.hp = hp;
        }

        // GET: Account/Login

        public IActionResult Login()
        {
            LoginVM viewModel = new LoginVM();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Login(LoginVM viewModel,string? returnURL)
        {

            var user = db.Users.Find(viewModel.Email);//Get user (admin or customer)record based on email(PK)
            
            if (user == null || !hp.VerifyPassword(user.PasswordHash,viewModel.Password))//Check if user exists and password is correct,if unable find user based on email or password not match
            {
                ModelState.AddModelError("", "Invalid email or password.");
            }

            if (ModelState.IsValid)
            {
                TempData["Info"] = "Login successful!";

                hp.SignIn(user!.Email, user.Role, viewModel.RememberMe);//Sign in

                if (string.IsNullOrEmpty(returnURL))
                {

                    if (Url.IsLocalUrl(returnURL))
                    {
                        return Redirect(returnURL);
                    }
                }


                if (user.Role == "Admin")
                {
                    return RedirectToAction("Admin", "Home");
                }
                else if (user.Role == "Customer")
                {
                    return RedirectToAction("Customer", "Home");
                }else if (user.Role == "Staff")
                {
                    return RedirectToAction("Staff", "Home");
                }
                    return RedirectToAction("Index", "Home");//Redirect to home page if no return URL is provided
            }
                return View(viewModel);
        }

        // GET: Account/Logout
        public IActionResult Logout(string? returnURL)
        { 
            TempData["Info"] = "Logout successful!";
            hp.SignOut();//Sign out the user
            return RedirectToAction("Index", "Home");//Redirect to home page after logout
        }

        // GET: Account/AccessDenied  for staff want to access admin page
        public IActionResult AccessDenied(string? returnURL)
        {
            return View();
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
