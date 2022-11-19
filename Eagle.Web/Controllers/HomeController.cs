using Eagle.Web.Models;
using Eagle.Web.Models.DTO;
using Eagle.Web.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Eagle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices product;
        private readonly ICartService _cart;

        public HomeController(ILogger<HomeController> logger, IProductServices product, ICartService _cart)
        {
            _logger = logger;
            this.product = product;
            this._cart = _cart;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDTO> list = new List<ProductDTO>();
            var response = await product.GetAllProductAsync<ResponseDto>("");
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> Details(int ProductId)
        {
            ProductDTO prod = new ();
            var response = await product.GetProductByIdAsync<ResponseDto>(ProductId,"");
            if (response != null && response.IsSuccess)
            {
                prod = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            }
            return View(prod);
        }

        [HttpPost]
        [ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsPost(ProductDTO productDTO)
        {
            var CartDto = new CartDto
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(x=>x.Type == "sub")?.FirstOrDefault()?.Value
                }
            };
            CartDetailsDto cartDetailsDto = new CartDetailsDto
            {
                Count = productDTO.Count,
                ProductId = productDTO.ProductId,

            };
            var resp = await product.GetProductByIdAsync<ResponseDto>(productDTO.ProductId, "");
            if (resp != null && resp.IsSuccess)
            {
                cartDetailsDto.Product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(resp.Result));
            }
            List<CartDetailsDto> cartDetailsDtos = new List<CartDetailsDto>();
            cartDetailsDtos.Add(cartDetailsDto);
            CartDto.CartDetails = cartDetailsDtos;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var addToCartResp = await _cart.AddToCartAsync<ResponseDto>(CartDto, accessToken);
            if (addToCartResp != null && addToCartResp.IsSuccess)
            {
                cartDetailsDto.Product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(addToCartResp.Result));
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}