using Eagle.Web.Models.DTO;

namespace Eagle.Web.Service.IService
{
    public interface ICouponService
    {
        Task<T> GetCoupon<T>(string couponCode, string token = null);
    }
}
