using AutoMapper;
using Eagle.Services.ShoppingCartAPI.Data;
using Eagle.Services.ShoppingCartAPI.Models;
using Eagle.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Eagle.Services.ShoppingCartAPI.Repository
{
    public class CartRepo : ICartRepo
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public CartRepo(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> ClearCart(string UserId)
        {
           var cartHeaderFromDb = await _db.CartHeaders.FirstOrDefaultAsync(x=>x.UserId == UserId);
            if (cartHeaderFromDb != null)
            {
                _db.CartDetails.
                    RemoveRange(_db.CartDetails.Where(x => x.CartHeaderId == cartHeaderFromDb.CartHeaderId));
                _db.CartHeaders.Remove(cartHeaderFromDb);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart =  _mapper.Map<Cart>(cartDto);

            var productInDb = await _db.Products.FirstOrDefaultAsync(x=>x.ProductId == cartDto.CartDetails[0].ProductId);
            if (productInDb == null)
            {
                await _db.Products.AddAsync(cart.CartDetails[0].Product);
                await _db.SaveChangesAsync();
            }

            var CartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == cart.CartHeader.UserId);
            if (CartHeaderFromDb == null)
            {
                _db.CartHeaders.Add(cart.CartHeader);
                await _db.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                await _db.CartDetails.AddAsync(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync
                    (u=>u.ProductId == cart.CartDetails[0].ProductId && 
                u.CartHeaderId == CartHeaderFromDb.CartHeaderId);

                if (cartDetailsFromDb == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = CartHeaderFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    await _db.CartDetails.AddAsync(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new Cart();
            cart.CartHeader = await _db.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
            cart.CartDetails = await _db.CartDetails.Include(x=>x.Product).Where(x => x.CartHeaderId == cart.CartHeader.CartHeaderId).ToListAsync();

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCart(int CartDetailId)
        {
            try
            {
                CartDetail cartDetail = await _db.CartDetails.FirstOrDefaultAsync(x => x.CartDetailsId == CartDetailId);
                int totalCountOfCartItems = _db.CartDetails
                    .Where(o => o.CartHeaderId == cartDetail.CartHeaderId).Count();
                _db.CartDetails.Remove(cartDetail);
                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders.FirstOrDefaultAsync(x => x.CartHeaderId == cartDetail.CartHeaderId);
                    _db.CartHeaders.Remove(cartHeaderToRemove);

                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
