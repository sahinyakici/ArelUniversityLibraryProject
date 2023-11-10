using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class UserRegisterValidator : AbstractValidator<UserForRegisterDto>
{
    public UserRegisterValidator()
    {
        RuleFor(user => user.UserName).NotEmpty();
        RuleFor(user => user.UserName).NotNull();
        RuleFor(user => user.UserName).MinimumLength(5);
        RuleFor(user => user.UserName).MaximumLength(15);

        RuleFor(user => user.FirstName).NotEmpty();
        RuleFor(user => user.FirstName).NotNull();
        RuleFor(user => user.FirstName).MinimumLength(3);

        RuleFor(user => user.LastName).NotEmpty();
        RuleFor(user => user.LastName).NotNull();
        RuleFor(user => user.LastName).MinimumLength(3);

        RuleFor(user => user.Email).NotNull();
        RuleFor(user => user.Email).NotEmpty();
        RuleFor(user => user.Email).EmailAddress();
        RuleFor(user => user.Email).MinimumLength(5);

        RuleFor(user => user.Password).MinimumLength(8);
        RuleFor(user => user.Password).NotNull();
        RuleFor(user => user.Password).NotEmpty();
        RuleFor(user => user.Password).Matches("[A-Z]").WithMessage("Password must be have capital letter");
        RuleFor(user => user.Password).Matches("[a-z]").WithMessage("Password must be have lower letter");
        RuleFor(user => user.Password).Matches("[0-9]").WithMessage("Password must be have at least one number");
        RuleFor(user => user.Password).Matches(@"[!@#$%^&*()_+\\[\]{};':""<>,.?~\\/-]")
            .WithMessage("Password must be have at least one special character");
    }
}