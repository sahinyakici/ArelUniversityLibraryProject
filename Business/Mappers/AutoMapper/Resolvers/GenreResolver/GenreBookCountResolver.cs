using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers;

public class GenreBookCountResolver : IValueResolver<Genre, GenreDTO, int>
{
    private IBookService _bookService;

    public GenreBookCountResolver(IBookService bookService)
    {
        _bookService = bookService;
    }

    public int Resolve(Genre source, GenreDTO destination, int destMember, ResolutionContext context)
    {
        var result = _bookService.GetAllByGenre(source.GenreId);
        if (result.Success)
        {
            int bookCount = result.Data.Count(book => book.RentStatus == false);
            return bookCount;
        }

        throw new InvalidOperationException(result.Message);
    }
}