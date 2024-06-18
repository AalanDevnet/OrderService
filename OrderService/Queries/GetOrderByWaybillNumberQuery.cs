using MediatR;
using OrderService.Models;

namespace OrderService.Queries
{
    public class GetOrderByWaybillNumberQuery : IRequest<Order>
    {
        public string WaybillNumber { get; set; }
    }
}
