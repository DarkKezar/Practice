using Cafe.Application.UseCases.EmployeeCases.Create;
using Cafe.Application.DTO;
using FluentValidation;

namespace Cafe.Application.Validators;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(e => e.IdentityId).NotEmpty().WithMessage(Messages.NotEmptyField);
        RuleFor(e => e.Biography).NotEmpty().WithMessage(Messages.NotEmptyField);
        RuleFor(e => e.Salary).Must(salary => salary > 0).WithMessage(Messages.Salary);
    }
}
