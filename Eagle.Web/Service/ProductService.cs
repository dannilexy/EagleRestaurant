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
        public async Task<T> CreateProductAsync<T>(ProductDTO productDto, string token)
        {
            return  await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Post,
                Data = productDto,
                Url = SD.ProductAPIBase + "api/products",
                Token = token
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id , string token)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Delete,
                Url = SD.ProductAPIBase + "api/products/" + id,
                Token = token,
            });
        }

        public async Task<T> GetAllProductAsync<T>(string token)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Get,
                Url = SD.ProductAPIBase + "api/products",
                Token = token,
            }); 
        }

        public async Task<T> GetProductByIdAsync<T>(int Id, string token)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Get,
                Url = SD.ProductAPIBase + "api/products/" + Id,
                Token = token,
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDTO product, string token)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Put,
                Data = product,
                Url = SD.ProductAPIBase + "api/products",
                Token = token
            });
        }
    }
}
