using MediatR;
using OrderService.Models;
using OrderService.Queries;
using OrderService.Repositories;

namespace OrderService.Handler
{
    public class GetOrderByWaybillNumberQueryHandler : IRequestHandler<GetOrderByWaybillNumberQuery, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByWaybillNumberQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(GetOrderByWaybillNumberQuery request, CancellationToken cancellationToken)
        {
            var waybillNumber = request.WaybillNumber;
            var order = await _orderRepository.GetOrderByWaybillNumberAsync(waybillNumber);

            return order;
        }
    }
}
