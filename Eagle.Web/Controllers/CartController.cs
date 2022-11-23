using Eagle.Web.Models.DTO;
using Eagle.Web.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Eagle.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductServices _product;
        private readonly ICartService _cartService;
        private readonly ICouponService _coupon;
        public CartController(IProductServices product, ICartService cartService, ICouponService _coupon)
        {
            _product = product;
            _cartService = cartService;
            this._coupon = _coupon;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartBasedOnLoggedInUser());
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartBasedOnLoggedInUser());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CartDto cart)
        {
            try
            {
                var UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _cartService.CheckOut<ResponseDto>(cart.CartHeader, accessToken);
                if (true)
                {
                    return RedirectToAction(nameof(Confirmation));
                }
            }
            catch (Exception)
            {

                return View(cart);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            var UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.ApplyCoupon<ResponseDto>(cartDto, accessToken);
            if (response.IsSuccess && response.Result != null)
            {
                return RedirectToAction(nameof(CartIndex));
                //return cartDto;
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Remove(int CartDetailsId)
        {
            var UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveFromCartAsync<ResponseDto>(CartDetailsId, accessToken);
            if (response.IsSuccess && response.Result != null)
            {
                return RedirectToAction(nameof(CartIndex));              
                //return cartDto;
            }
            return View();
        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            var UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveCoupon<ResponseDto>(cartDto.CartHeader.UserId, accessToken);
            if (response.IsSuccess && response.Result != null)
            {
                return RedirectToAction(nameof(CartIndex));
                //return cartDto;
            }
            return View();
        }

        private async Task<CartDto> LoadCartBasedOnLoggedInUser()
        {
            var UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.GetCartByUserId<ResponseDto>(UserId, accessToken);
            CartDto cartDto = new CartDto();
            if (response.IsSuccess && response.Result != null)
            {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                //return cartDto;
            }
            if (cartDto.CartHeader != null)
            {
                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    var couponResponse = await _coupon.GetCoupon<ResponseDto>(cartDto.CartHeader.CouponCode, accessToken);
                    if (couponResponse.IsSuccess && couponResponse.Result != null)
                    {
                       var coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(couponResponse.Result));
                        cartDto.CartHeader.DisccountTotal = coupon.DiscountAmount;
                    }
                }
                foreach (var detail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
                }
                cartDto.CartHeader.OrderTotal -= cartDto.CartHeader.DisccountTotal;
            }
            return cartDto;
        }
    }
}
