using ExampleMVCApp.DTO;
using ExampleMVCApp.Models;
using ExampleMVCApp.Util;
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
            List<ProductReadDTO>products = DBUtils.ReadAllProducts();
            return PartialView(@"~/Views/Product/_ProductView.cshtml", products);
        }
        
        public IActionResult ProductDetails(int id)
        {
            var productDTO = DBUtils.GetProductDetails(id);
            if (productDTO == null)
            {
                return NotFound();
            }

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

        public IActionResult UpdateProduct(int id)
        {
            using EstoreContext _context = new();
            var product = _context.Products.Include("Category").FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDTO productDTO = new ProductUpdateDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                StockQuantity = product.StockQuantity
            };
            return View(productDTO);
        }

        [HttpPost]
        public IActionResult UpdateProduct(int id, ProductUpdateDTO productDTO) {
            if (!ModelState.IsValid) { 
                return View(productDTO);
            }

            using EstoreContext context = new EstoreContext();
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) {
                return NotFound();
            }

            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
            product.StockQuantity = productDTO.StockQuantity;
            product.ImageUrl = productDTO.ImageUrl;

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
