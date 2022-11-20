using AutoMapper;
using Eagle.Services.CouponAPI.Data;
using Eagle.Services.CouponAPI.Models.Dtos;
using Eagle.Services.CouponAPI.Repo.IRepo;
using Microsoft.EntityFrameworkCore;

namespace Eagle.Services.CouponAPI.Repo
{
    public class CouponRepo : ICouponRepo
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public CouponRepo(ApplicationDbContext db, IMapper _mapper)
        {
            _db = db;
            this._mapper = _mapper;
        }

        public async Task<CouponDto> GetCouponByCode(string code)
        {
            var coupon = await _db.Coupons.FirstOrDefaultAsync(x => x.Code.Equals(code));
            return _mapper.Map<CouponDto>(coupon);
        }
    }
}
