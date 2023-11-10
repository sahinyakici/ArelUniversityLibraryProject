using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class GenreValidator : AbstractValidator<Genre>
{
    public GenreValidator()
    {
        RuleFor(genre => genre.GenreName).NotNull();
        RuleFor(genre => genre.GenreName).NotEmpty();
        RuleFor(genre => genre.GenreName).MinimumLength(3);
        RuleFor(genre => genre.GenreName).MaximumLength(15);
    }
}