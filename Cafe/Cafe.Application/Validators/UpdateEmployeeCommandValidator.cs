using Cafe.Application.UseCases.EmployeeCases.Update;
using Cafe.Application.DTO;
using FluentValidation;

namespace Cafe.Application.Validators;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(d => d.Id).NotEmpty().WithMessage(Messages.NotEmptyField);
        
        RuleFor(d => d.Salary).Must(salary => salary > 0).When(d => d.Salary != null).WithMessage(Messages.Salary);
    }
}
