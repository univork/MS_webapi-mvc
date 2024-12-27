using ExampleMVCApp.DTO;
using ExampleMVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleMVCApp.Util
{
    public static class DBUtils
    {

        public static List<ProductReadDTO> ReadAllProducts()
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

                return products;
            }
        }

        public static ProductReadDTO? GetProductDetails(int id)
        {
            using EstoreContext _context = new();
            var product = _context.Products.Include("Category").FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            ProductReadDTO productDTO = new ProductReadDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = new CategoryDTO { Name = product.Category.Name },
                ImageUrl = product.ImageUrl,
            };
            return productDTO;
        }
    }
}
