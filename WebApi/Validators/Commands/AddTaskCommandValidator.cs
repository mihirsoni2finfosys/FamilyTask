using Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Validators.Commands
{
    public class AddTaskCommandValidator : AbstractValidator<AddTaskCommand>
    {
        public AddTaskCommandValidator()
        {
            RuleFor(x => x.Text).NotNull().NotEmpty();
        }
    }
}

