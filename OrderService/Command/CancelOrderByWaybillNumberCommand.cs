using MediatR;

namespace OrderService.Command
{
    public class CancelOrderByWaybillNumberCommand : IRequest<bool>
    {
        public string WaybillNumber { get; set; }
    }
}
