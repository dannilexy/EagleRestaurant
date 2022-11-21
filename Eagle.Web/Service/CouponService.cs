using Eagle.Web.Service.IService;

namespace Eagle.Web.Service
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory _httpClientFactory) : base(_httpClientFactory)
        {
            this._httpClientFactory = _httpClientFactory;

        }

        public async Task<T> GetCoupon<T>(string couponCode, string token = null)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Get,
                Url = SD.CouponAPIBase + "api/Coupon/GetCouponByCode/" + couponCode,
                Token = token,
            });
        }
    }
}
