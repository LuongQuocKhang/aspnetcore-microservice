using AutoMapper;
using EventBus.Messages.Events;
using MediatR;
using MassTransit.Mediator;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using MassTransit;

namespace Ordering.API.EventBusConsummer
{
    public class BasketCheckoutConsummer : IConsumer<BasketCheckoutEvent>
    {
        private readonly MediatR.IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketCheckoutConsummer> _logger;

        public BasketCheckoutConsummer(MediatR.IMediator mediator, IMapper mapper, ILogger<BasketCheckoutConsummer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);

            var result = await _mediator.Send(command);

            _logger.LogInformation($"BasketCheckoutEvent consumed successfully. Created Order Id: {result}");

        }
    }
}
