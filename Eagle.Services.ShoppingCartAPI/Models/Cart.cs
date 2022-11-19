namespace Eagle.Services.ShoppingCartAPI.Models
{
    public class Cart
    {
        public CartHeader CartHeader { get; set; }
        public List<CartDetail> CartDetails { get; set; }
    }
}
