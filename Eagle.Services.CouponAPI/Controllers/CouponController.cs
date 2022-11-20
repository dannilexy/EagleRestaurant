using Eagle.Services.CouponAPI.Models.Dtos;
using Eagle.Services.CouponAPI.Repo.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eagle.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private ICouponRepo _coupon;
        protected ResponseDto _response;

        public CouponController(ICouponRepo coupon)
        {
            _coupon = coupon;
            _response = new ResponseDto();

        }
        [HttpGet("GetCouponByCode/{couponCode}")]
        public async Task<ResponseDto> GetCouponByCode(string couponCode)
        {
            try
            {
                var products = await _coupon.GetCouponByCode(couponCode);
                _response.Result = products;
                _response.Message = "Products Fetched successfully";
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
    }
}
