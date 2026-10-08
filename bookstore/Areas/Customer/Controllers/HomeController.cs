using bookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace bookstore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
