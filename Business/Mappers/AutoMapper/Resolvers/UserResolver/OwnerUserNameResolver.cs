using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers.UserResolver;

public class OwnerUserNameResolver : IValueResolver<Book, BookDTO, String>
{
    private readonly IUserService _userService;

    public OwnerUserNameResolver(IUserService userService)
    {
        _userService = userService;
    }

    public string Resolve(Book source, BookDTO destination, string destMember, ResolutionContext context)
    {
        Guid userId = source.OwnerId;
        var result = _userService.GetById(userId);
        if (result.Success)
        {
            return result.Data.UserName;
        }

        return result.Message;
    }
}