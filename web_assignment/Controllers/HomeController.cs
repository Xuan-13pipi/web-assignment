using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using web_assignment.Models;

namespace web_assignment.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Both()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Customer()
        {
            return View();
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "Staff")]
        public IActionResult Staff()
        {
            return View();
        }
    }
}
