﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var addedOrder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {addedOrder.Id} is successfully created.");

            await SendEmmalAsync(addedOrder);

            return addedOrder.Id;
        }

        public async Task SendEmmalAsync(Order order)
        {
            var email = new Email()
            {
                To = order.EmailAddress,
                Subject = $"Order {order.Id} is successfully created.",
                Body = $"Order was created."
            };
            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch(Exception ex) {
                _logger.LogError($"Order {order.Id} failed due to an error with the email service: {ex.Message}");
            }
        }
    }
}
