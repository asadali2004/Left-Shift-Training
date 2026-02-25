using Client_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client_Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Using ViewBag (dynamic)
            ViewBag.MyVariable = "Hello Asad";
            ViewBag.Countries = "India, Bangladesh, SriLanka";
            ViewBag.NumberOfLines = 5;
            ViewBag.KuchBhi = "Kuch Bhi";
            ViewBag.ListOfFruits = new List<string>() { "Mango", "Apple", "Papaya", "Banana" };

            // Using ViewData (dictionary)
            ViewData["fruits"] = new List<string>() { "Orange", "Grapes", "Watermelon", "Strawberry" };
            ViewData["Message"] = "This is ViewData";
            ViewData["Count"] = 10;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
