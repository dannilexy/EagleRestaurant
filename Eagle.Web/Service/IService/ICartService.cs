using Eagle.Web.Models.DTO;

namespace Eagle.Web.Service.IService
{
    public interface ICartService
    {
        Task<T> GetCartByUserId<T>(string id, string token = null);
        Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> RemoveFromCartAsync<T>(int cartId, string token = null);
    }
}
