using Cafe.Application.UseCases.DishCases.Update;
using Cafe.Application.DTO;
using FluentValidation;

namespace Cafe.Application.Validators;

public class UpdateDishCommandValidator : AbstractValidator<UpdateDishCommand>
{
    public UpdateDishCommandValidator()
    {
        RuleFor(d => d.Id).NotEmpty().WithMessage(Messages.NotEmptyField);
        RuleFor(d => d.Price).Must(price => price > 0).When(d => d.Price != null).WithMessage(Messages.Price);
        RuleFor(d => d.Ingridients).Must(list => list.Count > 0).When(d => d.Ingridients != null).WithMessage(Messages.IngridientsCount);
    }
}
