using Eagle.Web.Models.DTO;

namespace Eagle.Web.Service.IService
{
    public interface ICartService
    {
        Task<T> GetCartByUserId<T>(string id, string token = null);
        Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> ApplyCoupon<T>(CartDto cartDto, string token = null);
        Task<T> RemoveFromCartAsync<T>(int cartId, string token = null);
        Task<T> RemoveCoupon<T>(string UserId, string token = null);
        Task<T> CheckOut<T>(CartHeaderDto cartHeaderDto, string token = null);
    }
}
