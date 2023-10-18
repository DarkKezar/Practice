using Stock.Application.DTO;
using FluentValidation;

namespace Stock.Application.Validators;

public class IngridientCreationDTOValidator : AbstractValidator<IngridientCreationDTO>
{
    public IngridientCreationDTOValidator()
    {
        RuleFor(m => m.Name).NotEmpty().WithMessage("Please, enter ingridient name");
    }
}
