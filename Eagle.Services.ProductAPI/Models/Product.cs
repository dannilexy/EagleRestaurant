using System.ComponentModel.DataAnnotations;

namespace Eagle.Services.ProductAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Range(0,100)]
        public double Price { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
