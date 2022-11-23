using Eagle.MessageBus;
using Eagle.Services.ShoppingCartAPI.Messaging;
using Eagle.Services.ShoppingCartAPI.Models.Dto;
using Eagle.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eagle.Services.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepo _cart;
        private readonly IMessageBus _messageBus;
        protected ResponseDto _response;
        public CartController(ICartRepo cart, IMessageBus _messageBus)
        {
            _cart = cart;
            _response = new ResponseDto();
            this._messageBus = _messageBus;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDto cart = await _cart.GetCartByUserId(userId);
                _response.Result = cart;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.InnerException.Message };
            }
            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartDto)
        {
            try
            {
                CartDto cart = await _cart.CreateUpdateCart(cartDto);
                _response.Result = cart;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.InnerException.Message };
            }
            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                CartDto cart = await _cart.CreateUpdateCart(cartDto);
                _response.Result = cart;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.InnerException.Message };
            }
            return _response;
        }

        [HttpPost("RemoveFromCart")]
        public async Task<object> RemoveFromCart([FromBody]int cartId)
        {
            try
            {
                bool cart = await _cart.RemoveFromCart(cartId);
                _response.Result = cart;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.InnerException.Message };
            }
            return _response;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                bool cart = await _cart.ApplyCoupon(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);
                _response.Result = cart;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.InnerException.Message };
            }
            return _response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                bool cart = await _cart.RemoveCoupon(userId);
                _response.Result = cart;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.InnerException.Message };
            }
            return _response;
        }

        [HttpPost("CheckOut")]
        public async Task<object> CheckOut([FromBody] CheckOutHeaderDto checkOutHeaderDto )
        {
            try
            {
                var cartDto = await _cart.GetCartByUserId(checkOutHeaderDto.UserId);
                if (cartDto == null)
                    return null;
                checkOutHeaderDto.CartDetails = cartDto.CartDetails;
                //Login to add message to process Order
                await _messageBus.PublishMessage(checkOutHeaderDto, "checkOutMessageTopic");

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.InnerException.Message };
            }
            return _response;
        }
    }

}
