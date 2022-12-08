using Eaglez.Services.OrderAPI.Data;
using Eaglez.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Eaglez.Services.OrderAPI.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationDbContext _db;
        public OrderRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
             _db.OrderHeader.Add(orderHeader);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrder(int OrderHeaderId, bool paid)
        {
            var orderHeaderFromDb = await _db.OrderHeader.FirstOrDefaultAsync(x=>x.OrderHeaderId == OrderHeaderId);
            if (orderHeaderFromDb!= null)
            {
                orderHeaderFromDb.PaymentStatus = paid;
                await _db.SaveChangesAsync();
            }
            return true;
        }
    }
}
