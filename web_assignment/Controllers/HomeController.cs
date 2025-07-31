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

        [Authorize(Roles = "Waiter")]
        public IActionResult Waiter()
        {
            return View();
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "Chef")]
        public IActionResult Chef()
        {
            return View();
        }
        [Authorize(Roles = "Manager")]
        public IActionResult Manager()
        {
            return View();
        }
        [Authorize(Roles = "Cashier")]
        public IActionResult Cashier()
        {
            return View();
        }
    }
}
