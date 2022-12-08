using Eaglez.Services.OrderAPI.Models;

namespace Eaglez.Services.OrderAPI.Repository
{
    public interface IOrderRepo
    {
        Task<bool> AddOrder(OrderHeader orderHeader);
        Task<bool> UpdateOrder(int OrderHeaderId, bool paid);
    }
}
