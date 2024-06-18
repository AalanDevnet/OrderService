using MediatR;
using OrderService.Command;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = request.Order;
            var result = await _orderRepository.CreateOrderAsync(order);
            return result;
        }
    }
}
