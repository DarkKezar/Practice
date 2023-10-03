using Stock.Application.DTO;
using FluentValidation;

namespace Stock.Application.Validators;

public class TransactionCTOValidator : AbstractValidator<TransactionCreationTO>
{
    public TransactionCTOValidator()
    {
        RuleFor(m => m.UserId).NotEmpty().WithMessage("Enter userId");
        RuleFor(m => m.IngridientsId).NotNull();
        RuleFor(m => m.Count).NotNull();

        RuleFor(m => m.IngridientsId).Must((rootObject, list, context) => rootObject.Count.Count == list.Count);
    }
}
