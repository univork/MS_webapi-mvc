using ExampleMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExampleMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using(EstoreContext context = new EstoreContext())
            {
                var products = context.Products.Select(p => new
                {
                    p.Name,
                    Category = p.Category.Name,
                    p.Price,
                    p.ImageUrl
                }).Take(2).ToList();

                return View(new {Products = products});
            }
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
