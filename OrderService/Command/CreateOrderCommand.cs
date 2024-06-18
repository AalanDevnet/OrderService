using OrderService.Models;
using MediatR;

namespace OrderService.Command
{
    public class CreateOrderCommand : IRequest<CreateOrderResult>
    {
        public Order Order { get; set; }
    }
}
