using System.ComponentModel.DataAnnotations;

namespace ExampleMVCApp.DTO
{
    public class ProductCreateDTO : ProductDTO
    {
        [Required]
        public int CategoryId { get; set; }
    }
}
