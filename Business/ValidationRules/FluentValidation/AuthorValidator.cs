using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(author => author.AuthorName).NotNull();
        RuleFor(author => author.AuthorName).NotEmpty();
        RuleFor(author => author.AuthorName).MinimumLength(3);
    }
}