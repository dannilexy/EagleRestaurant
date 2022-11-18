using System.ComponentModel.DataAnnotations;

namespace Eagle.Web.Models.DTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            Count = 1;
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Range(1,100, ErrorMessage = "Quantity cannot be less that 1 or greater than 100")]
        public int Count { get; set; }
    }
}
