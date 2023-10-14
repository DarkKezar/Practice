using Cafe.Application.UseCases.DishCases.Create;
using Cafe.Application.DTO;
using FluentValidation;

namespace Cafe.Application.Validators;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(d => d.Name).NotEmpty().WithMessage(Messages.NotEmptyField);
        RuleFor(d => d.Price).Must(price => price > 0).WithMessage(Messages.Price);
        RuleFor(d => d.Description).NotEmpty().WithMessage(Messages.NotEmptyField);
        RuleFor(d => d.Ingridients).Must(list => list.Count > 0).WithMessage(Messages.IngridientsCount);
    }
}
