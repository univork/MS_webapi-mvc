using System.ComponentModel.DataAnnotations;

namespace ExampleMVCApp.DTO
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }
        [Range(0, int.MaxValue)]
        public int? StockQuantity { get; set; }
        [StringLength(200)]
        public string? ImageUrl { get; set; }
    }
}
