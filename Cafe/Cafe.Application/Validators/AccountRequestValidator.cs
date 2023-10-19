using Cafe.Application.Proto;
using FluentValidation;

namespace Cafe.Application.Validators;

public class AccountRequestValidator : AbstractValidator<AccountRequest>
{
    public AccountRequestValidator()
    {
        RuleFor(r => r.Biography).NotEmpty();
        RuleFor(r => r.Salary).GreaterThanOrEqualTo(0);
        RuleFor(r => r.IdentityIdString).Must(str => {
            try{
                Guid.Parse(str);
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        });
    }
}
