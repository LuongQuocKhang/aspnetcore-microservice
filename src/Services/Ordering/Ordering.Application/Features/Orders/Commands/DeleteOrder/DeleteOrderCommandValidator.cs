using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator() {
            RuleFor(p => p.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{Id} is required")
                .NotNull();
        }
    }
}
