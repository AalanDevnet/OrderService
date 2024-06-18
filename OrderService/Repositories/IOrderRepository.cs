using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<CreateOrderResult> CreateOrderAsync(Order order);
        Task<bool> CancelOrderByWaybillNumberAsync(string waybillNumber);
        Task<Order> GetOrderByWaybillNumberAsync(string waybillNumber);
    }
}
