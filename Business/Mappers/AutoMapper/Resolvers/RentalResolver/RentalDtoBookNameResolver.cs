using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers.RentalResolver;

public class RentalDtoBookNameResolver : IValueResolver<Rental, RentalDTO, string>
{
    private readonly IBookService _bookService;

    public RentalDtoBookNameResolver(IBookService bookService)
    {
        _bookService = bookService;
    }

    public string Resolve(Rental source, RentalDTO destination, string destMember, ResolutionContext context)
    {
        Book book = _bookService.GetById(source.BookId).Data;
        return book.BookName;
    }
}