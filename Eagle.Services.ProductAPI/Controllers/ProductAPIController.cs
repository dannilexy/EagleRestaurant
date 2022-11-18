using Eagle.Services.ProductAPI.Models.DTO;
using Eagle.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eagle.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _product;
        public ProductAPIController(IProductRepository _product)
        {
            this._product = _product;
            _response = new ResponseDto();
        }
        [HttpGet]
        public async Task<ResponseDto> GetProduct()
        {
            try
            {
                var products = await _product.GetProducts();
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

        [HttpGet]
        [Route("{ProductId}")]
        public async Task<ResponseDto> GetProductById(int ProductId)
        {
            try
            {
                var product = await _product.GetProductById(ProductId);
                _response.Result = product;
                _response.Message = "Product Fetched successfully";
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

        [HttpPost]
        [Authorize]
        public async Task<ResponseDto> CreateProduct([FromBody]ProductDTO productDTO)
        {
            try
            {
                var product = await _product.CreateOrUpdateProduct(productDTO);
                _response.Result = product;
                _response.Message = "Product Created successfully";
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
        [HttpPut]
        [Authorize]
        public async Task<ResponseDto> UpdateProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                var product = await _product.CreateOrUpdateProduct(productDTO);
                _response.Result = product;
                _response.Message = "Product Created successfully";
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

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{ProductId}")]
        public async Task<ResponseDto> DeleteProduct(int ProductId)
        {
            try
            {
                var product = await _product.DeleteProduct(ProductId);
                _response.Result = product;
                _response.Message = "Product Delete successfully";
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
