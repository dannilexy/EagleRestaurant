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

        public HomeController(ILogger<HomeController> logger, IProductServices product)
        {
            _logger = logger;
            this.product = product;
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