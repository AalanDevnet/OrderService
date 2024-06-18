using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Command;
using OrderService.Models;
using OrderService.Queries;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrderResult>> CreateOrder(CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpDelete("waybill/{waybillNumber}")]
        public async Task<ActionResult<bool>> CancelOrderByWaybillNumber(string waybillNumber)
        {
            var command = new CancelOrderByWaybillNumberCommand { WaybillNumber = waybillNumber };
            var success = await _mediator.Send(command);

            if (success)
            {
                return Ok(true);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("waybill/{waybillNumber}")]
        public async Task<ActionResult<Order>> GetOrderByWaybillNumber(string waybillNumber)
        {
            var query = new GetOrderByWaybillNumberQuery { WaybillNumber = waybillNumber };
            var order = await _mediator.Send(query);

            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return NotFound();
            }
        }


    }
}