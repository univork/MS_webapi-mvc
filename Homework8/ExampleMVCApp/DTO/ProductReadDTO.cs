namespace ExampleMVCApp.DTO
{
    public class ProductReadDTO : ProductDTO
    {
        public int Id { get; set; }
        public virtual CategoryDTO? Category { get; set; }
    }
}
