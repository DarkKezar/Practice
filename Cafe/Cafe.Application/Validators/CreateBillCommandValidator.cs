using Cafe.Application.UseCases.BillCases.Create;
using Cafe.Application.DTO;
using FluentValidation;

namespace Cafe.Application.Validators;

public class CreateBillCommandValidator : AbstractValidator<CreateBillCommand>
{
    public CreateBillCommandValidator()
    {
        RuleFor(b => b.Dishes).Must(list => list.Count > 0).WithMessage(Messages.DishesCount);
        
        RuleFor(b => b.Sale).Must(sale => (sale >= 0) && (sale <= 0.5)).WithMessage(Messages.Sale);
    }
}
