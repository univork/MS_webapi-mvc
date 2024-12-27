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
                    Category = new CategoryDTO { Name = p.Category.Name },
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                }).ToList();

                return PartialView(@"~/Views/Product/_ProductView.cshtml", products);
            }
        }
        
        public IActionResult ProductDetails(int id)
        {
            using EstoreContext _context = new();
            var product = _context.Products.Include("Category").FirstOrDefault(p => p.Id == id);

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

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductCreateDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                using EstoreContext context = new EstoreContext();
                Product product = new Product
                {
                    Name = productDTO.Name,
                    CategoryId = productDTO.CategoryId,
                    Price = productDTO.Price,
                    ImageUrl = productDTO.ImageUrl,
                    StockQuantity = productDTO.StockQuantity,
                };
                context.Products.Add(product);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(productDTO);
        }
    }
}
