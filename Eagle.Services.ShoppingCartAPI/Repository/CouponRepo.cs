using Eagle.Services.ShoppingCartAPI.Models.Dto;
using Eagle.Services.ShoppingCartAPI.Models.Dtos;
using Newtonsoft.Json;

namespace Eagle.Services.ShoppingCartAPI.Repository
{
    public class CouponRepo : ICouponRepo
    {
        private readonly HttpClient _httpClient;
        public CouponRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CouponDto> GetCoupon(string couponName)
        {
            var response = _httpClient.GetAsync($"api/Coupon/GetCouponByCode/{couponName}").Result; 
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
            }
            return new CouponDto();
        }
    }
}
