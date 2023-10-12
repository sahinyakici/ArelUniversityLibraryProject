using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(book => book.BookName).NotNull();
        RuleFor(book => book.BookName).NotEmpty();
        RuleFor(book => book.BookName).MinimumLength(2);
    }
}