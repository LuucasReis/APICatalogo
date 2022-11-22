namespace APICatalogo.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
