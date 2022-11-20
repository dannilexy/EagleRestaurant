using Eagle.Services.CouponAPI.Models.Dtos;

namespace Eagle.Services.CouponAPI.Repo.IRepo
{
    public interface ICouponRepo
    {
        Task<CouponDto> GetCouponByCode (string code);
    }
}
