using ExampleMVCApp.DTO;
using ExampleMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleMVCApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Browse our products";
            return View();
        }
        public IActionResult ProductList()
        {
            using(EstoreContext context = new EstoreContext())
            {
                var products = context.Products.Select(p => new ProductReadDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = new CategoryDTO { Id = (int)p.CategoryId, Name=p.Category.Name },
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                }).ToList();

                return PartialView(@"~/Views/Product/_ProductView.cshtml", products);
            }

        }

        public async Task<ActionResult<ProductReadDTO>> ProductDetail(int id)
        {
            using EstoreContext _context = new();
            var product = await _context.Products.Include("Category").FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDTO productDTO = new ProductReadDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = new CategoryDTO { Name = product.Category.Name },
                ImageUrl = product.ImageUrl,
            };

            return View(productDTO);
        }
    }
}
