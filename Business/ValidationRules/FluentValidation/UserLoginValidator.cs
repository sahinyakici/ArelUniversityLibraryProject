using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class UserLoginValidator : AbstractValidator<UserForLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(dto => dto.UserName).NotNull();
        RuleFor(dto => dto.UserName).NotEmpty();
        RuleFor(dto => dto.UserName).MinimumLength(5);
        RuleFor(dto => dto.UserName).MaximumLength(15);

        RuleFor(dto => dto.Password).MinimumLength(8);
        RuleFor(dto => dto.Password).NotNull();
        RuleFor(dto => dto.Password).NotEmpty();
    }
}