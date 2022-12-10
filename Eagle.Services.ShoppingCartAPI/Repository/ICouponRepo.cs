using Eagle.Services.ShoppingCartAPI.Models.Dtos;

namespace Eagle.Services.ShoppingCartAPI.Repository
{
    public interface ICouponRepo
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}
