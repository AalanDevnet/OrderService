using FluentValidation;
using OrderService.Command;

namespace OrderService.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.WaybillNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Order.ShipperName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Order.ShipperContact).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Order.ShipperPhone).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Order.ShipperAddr).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Order.OriginCode).NotEmpty().MaximumLength(3);
            RuleFor(x => x.Order.ReceiverName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Order.ReceiverPhone).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Order.ReceiverAddr).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Order.ReceiverZip).NotEmpty().MaximumLength(5);
            RuleFor(x => x.Order.DestinationCode).NotEmpty().MaximumLength(3);
            RuleFor(x => x.Order.ReceiverArea).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Order.Qty).GreaterThan(0);
            RuleFor(x => x.Order.Weight).GreaterThan(0);
            RuleFor(x => x.Order.GoodsDesc).NotEmpty().MaximumLength(40);
            RuleFor(x => x.Order.ServiceType).InclusiveBetween(1, 6);
            RuleFor(x => x.Order.OrderDate).NotEmpty();
            RuleFor(x => x.Order.ItemName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Order.SendStartTime).NotEmpty();
            RuleFor(x => x.Order.SendEndTime).NotEmpty();
            RuleFor(x => x.Order.ExpressType).NotEmpty().MaximumLength(1);
            RuleFor(x => x.Order.GoodsValue).GreaterThan(0);
            RuleFor(x => x.Order.Insurance).GreaterThan(0).When(x => x.Order.Insurance.HasValue); // Validation for Insurance
            RuleFor(x => x.Order.Cod).GreaterThan(0).When(x => x.Order.Cod.HasValue); // Validation for Cod
            RuleFor(x => x.Order.GoodsValue).GreaterThan(0); // Validation for GoodsValue
        }
    }
}
