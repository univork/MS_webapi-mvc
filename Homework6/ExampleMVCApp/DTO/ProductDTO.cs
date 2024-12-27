using System.ComponentModel.DataAnnotations;

namespace ExampleMVCApp.DTO
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 20000)]
        public decimal? Price { get; set; }

        [Required]
        [Range(0, 500)]
        public int? StockQuantity { get; set; }

        [StringLength(200)]
        public string? ImageUrl { get; set; }
    }
}
