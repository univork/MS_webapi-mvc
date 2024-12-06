using ExampleMVCApp.DTO;
using ExampleMVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleMVCApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductList()
        {
            using(EstoreContext context = new EstoreContext())
            {
                var products = context.Products.Select(p => new ProductDTO
                {
                    Name = p.Name,
                    Category = p.Category.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                }).ToList();

                return PartialView(@"~/Views/Product/_ProductView.cshtml", products);
            }

        }
    }
}
