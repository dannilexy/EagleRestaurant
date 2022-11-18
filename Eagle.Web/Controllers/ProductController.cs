using Eagle.Web.Models.DTO;
using Eagle.Web.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace Eagle.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _product;
        public ProductController(IProductServices _product)
        {
            this._product = _product;
        }
        public async Task <IActionResult> ProductIndex()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            List<ProductDTO> list = new ();
            var res = await _product.GetAllProductAsync<ResponseDto>(accessToken);
            if (res != null && res.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(res.Result));
            }
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var res = await _product.CreateProductAsync<ResponseDto>(product, accessToken);
            if (res.Result != null && res.IsSuccess)
            {
              return  RedirectToAction(nameof(ProductIndex));
            }
            return View();
        }

        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProduct(int ProductId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var product = new ProductDTO();
            var res = await _product.GetProductByIdAsync<ResponseDto>(ProductId, accessToken);
            if (res != null && res.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(res.Result));
            }
            else
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteProduct(int ProductId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var res = await _product.DeleteProductAsync<ResponseDto>(ProductId, accessToken);
            return RedirectToAction(nameof(ProductIndex)); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var res = await _product.UpdateProductAsync<ResponseDto>(product, accessToken);
            if (res.Result != null && res.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View();
        }
    }
}
