using Eagle.Services.ShoppingCartAPI.Models.Dto;

namespace Eagle.Services.ShoppingCartAPI.Repository
{
    public interface ICartRepo
    {
        Task<CartDto> GetCartByUserId (string userId);
        Task<CartDto> CreateUpdateCart (CartDto cartDto);
        Task<bool> RemoveFromCart (int CartDetailId);
        Task<bool> ClearCart (string UserId);
    }
}
