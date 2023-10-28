using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers;

public class AuthorIdResolver : IValueResolver<BookDTO, Book, Guid>
{
    private IAuthorService _authorService;

    public AuthorIdResolver(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public Guid Resolve(BookDTO source, Book destination, Guid destMember, ResolutionContext context)
    {
        Author author = _authorService.GetAll().Data
            .Where(a => string.Equals(a.AuthorName, source.AuthorName, StringComparison.OrdinalIgnoreCase))
            .SingleOrDefault();
        return author.AuthorId;
    }
}