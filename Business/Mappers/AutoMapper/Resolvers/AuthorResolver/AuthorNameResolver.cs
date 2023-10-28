using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers;

public class AuthorNameResolver : IValueResolver<Book, BookDTO, string>
{
    private IAuthorService _authorService;

    public AuthorNameResolver(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public string Resolve(Book source, BookDTO destination, string destMember, ResolutionContext context)
    {
        IDataResult<Author> result = _authorService.GetById(source.AuthorId);
        if (result.Success)
        {
            return result.Data.AuthorName;
        }

        return result.Message;
    }
}