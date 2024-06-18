using MediatR;
using OrderService.Command;
using OrderService.Repositories;

namespace OrderService.Handler
{
    public class CancelOrderByWaybillNumberCommandHandler : IRequestHandler<CancelOrderByWaybillNumberCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderByWaybillNumberCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(CancelOrderByWaybillNumberCommand request, CancellationToken cancellationToken)
        {
            var waybillNumber = request.WaybillNumber;
            var success = await _orderRepository.CancelOrderByWaybillNumberAsync(waybillNumber);

            return success;
        }
    }
}
