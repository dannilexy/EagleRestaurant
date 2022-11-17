using Eagle.Web.Models.DTO;
using Eagle.Web.Service.IService;

namespace Eagle.Web.Service
{
    public class ProductService : BaseService, IProductServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory _httpClientFactory) : base(_httpClientFactory)
        {
            this._httpClientFactory = _httpClientFactory;

        }
        public async Task<T> CreateProductAsync<T>(ProductDTO productDto)
        {
            return  await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Post,
                Data = productDto,
                Url = SD.ProductAPIBase + "api/products",
                Token = "",
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Delete,
                Url = SD.ProductAPIBase + "api/products/" + id,
                Token = "",
            });
        }

        public async Task<T> GetAllProductAsync<T>()
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Get,
                Url = SD.ProductAPIBase + "api/products",
                Token = "",
            }); ;
        }

        public async Task<T> GetProductByIdAsync<T>(int Id)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Get,
                Url = SD.ProductAPIBase + "api/products/" + Id,
                Token = "",
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDTO product)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Put,
                Data = product,
                Url = SD.ProductAPIBase + "api/products",
                Token = "",
            });
        }
    }
}
