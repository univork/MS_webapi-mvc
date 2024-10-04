using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework2.Models;
using Homework2.DTO;

namespace Homework2.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            using EstoreContext _context = new();
            List<ProductDTO> products = await _context.Products.Select(p => new ProductDTO {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Category = new CategoryDTO { Name = p.Category.Name },
                    ImageUrl = p.ImageUrl,

                }).ToListAsync();
            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            using EstoreContext _context = new();
            var product = await _context.Products.Include("Category").FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDTO productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name, 
                Price = product.Price,
                Category = new CategoryDTO { Name = product.Category.Name },
                ImageUrl= product.ImageUrl,
            };

            return productDTO;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDTO)
        {
            using EstoreContext _context = new();

            Product product = new() {
                Name = productDTO.Name, 
                CategoryId = productDTO.Category.Id, 
                ImageUrl = productDTO.ImageUrl,
                Price = productDTO.Price,
                StockQuantity = productDTO.StockQuantity,
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(ProductDTO productDTO)
        {

            using EstoreContext _context = new();
            var product = await _context.Products.FindAsync(productDTO.Id);
            if (product == null)
            {
                return BadRequest();
            }

            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
            product.StockQuantity = productDTO.StockQuantity;
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(productDTO.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            using EstoreContext _context = new();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("~/api/categories")]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            using EstoreContext _context = new();
            List<CategoryDTO> categories = await _context.Categories.Select(p => new CategoryDTO{
                    Id = p.Id,
                    Name = p.Name,

                }).ToListAsync();
            return categories;
        }

        private bool ProductExists(int id)
        {
            using EstoreContext _context = new();
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
