using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class BookDtoValidator : AbstractValidator<BookDTO>
{
    public BookDtoValidator()
    {
        RuleFor(bookDtoDto => bookDtoDto.BookName).NotNull();
        RuleFor(bookDto => bookDto.BookName).NotEmpty();
        RuleFor(bookDto => bookDto.BookName).MinimumLength(2);

        RuleFor(bookDto => bookDto.GenreName).NotNull();
        RuleFor(bookDto => bookDto.GenreName).NotEmpty();

        RuleFor(bookDto => bookDto.OwnerName).NotNull();
        RuleFor(bookDto => bookDto.OwnerName).NotEmpty();

        RuleFor(bookDto => bookDto.AuthorName).NotNull();
        RuleFor(bookDto => bookDto.AuthorName).NotEmpty();

        RuleFor(bookDto => bookDto.Location).NotNull();
        RuleFor(bookDto => bookDto.Location).NotEmpty();

        RuleFor(bookDto => bookDto.PageSize).NotNull();
        RuleFor(bookDto => bookDto.PageSize).NotEmpty();
        RuleFor(bookDto => bookDto.PageSize).GreaterThan(10);
    }
}