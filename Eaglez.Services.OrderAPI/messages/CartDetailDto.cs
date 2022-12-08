using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaglez.Services.OrderAPI.messages
{
    public class CartDetailDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Count { get; set; }

    }
}
 