using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers;

public class GenreNameResolver : IValueResolver<Book, BookDTO, string>
{
    private IGenreService _genreService;

    public GenreNameResolver(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public string Resolve(Book source, BookDTO destination, string destMember, ResolutionContext context)
    {
        IDataResult<Genre> result = _genreService.GetById(source.GenreId);
        if (result.Success)
        {
            return result.Data.GenreName;
        }

        return result.Message;
    }
}