using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers;

public class GenreIdResolver : IValueResolver<BookDTO, Book, Guid>
{
    private IGenreService _genreService;

    public GenreIdResolver(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public Guid Resolve(BookDTO source, Book destination, Guid destMember, ResolutionContext context)
    {
        var result = _genreService.GetByName(source.GenreName);
        return result.Data.GenreId;
    }
}