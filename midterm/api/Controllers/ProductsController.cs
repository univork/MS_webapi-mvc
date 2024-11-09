using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ef.Models;
using DTO.Models;
using api.Filters;

namespace api.Controllers
{
    [Route("api/products")]
    [ApiController]
    [ServiceFilter(typeof(ExceptionFilter))]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json", "application/xml")]
        public async Task<ActionResult<List<ProductReadDTO>>> GetProducts()
        {
            using EstoreContext _context = new();
            List<ProductReadDTO> products = await _context.Products.Select(p => new ProductReadDTO {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Category = new CategoryDTO { Name = p.Category.Name },
                    ImageUrl = p.ImageUrl,

                }).ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDTO>> GetProduct(int id)
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
                ImageUrl= product.ImageUrl,
            };

            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductCreateDTO productDTO)
        {
            using EstoreContext _context = new();

            Product product = new() {
                Name = productDTO.Name, 
                CategoryId = productDTO.CategoryId, 
                ImageUrl = productDTO.ImageUrl,
                Price = productDTO.Price,
                StockQuantity = productDTO.StockQuantity,
            };
            _context.Products.Add(product);
            // throw new DbUpdateConcurrencyException();
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, productDTO);
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(ProductUpdateDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest();
            }

            using EstoreContext _context = new();
            var product = await _context.Products.FindAsync(productDTO.Id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
            product.StockQuantity = productDTO.StockQuantity;
            _context.Entry(product).State = EntityState.Modified;
            //throw new DbUpdateConcurrencyException();

            await _context.SaveChangesAsync();

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
    }
}
