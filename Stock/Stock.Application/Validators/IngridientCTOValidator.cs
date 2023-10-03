using Stock.Application.DTO;
using FluentValidation;

namespace Stock.Application.Validators;

public class IngridientCTOValidator : AbstractValidator<IngridientCreationTO>
{
    public IngridientCTOValidator()
    {
        RuleFor(m => m.Name).NotEmpty().WithMessage("Please, enter ingridient name");
    }
}
