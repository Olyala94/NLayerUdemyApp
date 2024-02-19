namespace NLayer.Core.DTOs
{
    public class ProductWithCategory : BaseDto
    {
        public string Name { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public CategoryDto Category { get; set; }
        public int CategoryId { get; set; }
    }
}
