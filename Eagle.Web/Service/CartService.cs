using Eagle.Web.Models.DTO;
using Eagle.Web.Service.IService;

namespace Eagle.Web.Service
{
    public class CartService : BaseService, ICartService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CartService(IHttpClientFactory _httpClientFactory) : base(_httpClientFactory)
        {
            this._httpClientFactory = _httpClientFactory;

        }
        public async Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Post,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "api/cart/AddCart",
                Token = token
            });
        }

        public async Task<T> ApplyCoupon<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Post,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "api/cart/ApplyCoupon",
                Token = token
            });
        }

        public async Task<T> CheckOut<T>(CartHeaderDto cartHeaderDto, string token = null)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Post,
                Data = cartHeaderDto,
                Url = SD.ShoppingCartAPIBase + "api/cart/CheckOut",
                Token = token
            });
        }

        public async Task<T> GetCartByUserId<T>(string id, string token = null)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Get,
                Url = SD.ShoppingCartAPIBase + "api/Cart/GetCart/" + id,
                Token = token,
            });
        }

        public async Task<T> RemoveCoupon<T>(string UserId, string token = null)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Post,
                Data = UserId,
                Url = SD.ShoppingCartAPIBase + "api/cart/RemoveCoupon",
                Token = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Post,
                Data = cartId,
                Url = SD.ShoppingCartAPIBase + "api/cart/RemoveFromCart",
                Token = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.ApiType.Post,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "api/cart/UpdateCart",
                Token = token
            });
        }
    }
}
