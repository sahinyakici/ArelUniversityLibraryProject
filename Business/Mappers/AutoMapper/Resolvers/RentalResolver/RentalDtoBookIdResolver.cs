using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers.RentalResolver;

public class RentalDtoBookIdResolver : IValueResolver<RentalDTO, Rental, Guid>
{
    private readonly IBookService _bookService;

    public RentalDtoBookIdResolver(IBookService bookService)
    {
        _bookService = bookService;
    }

    public Guid Resolve(RentalDTO source, Rental destination, Guid destMember, ResolutionContext context)
    {
        var result = _bookService.GetAllByOwnerName(source.OwnerName);
        if (result.Success)
        {
            List<Book> books = result.Data;
            Book book = books.Where(book => book.BookName == source.BookName).SingleOrDefault();
            if (book != null)
            {
                return book.BookId;
            }
        }

        throw new Exception(result.Message);
    }
}