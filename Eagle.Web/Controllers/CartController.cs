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
        public CartController(IProductServices product, ICartService cartService)
        {
            _product = product;
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartBasedOnLoggedInUser());
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
                foreach (var detail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
                }
            }
            return cartDto;
        }
    }
}
