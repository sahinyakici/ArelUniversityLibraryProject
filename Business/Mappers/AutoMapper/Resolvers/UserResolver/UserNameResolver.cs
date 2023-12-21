using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers.UserResolver;

public class UserNameResolver : IValueResolver<Book, BookDTO, string>
{
    private IUserService _userService;

    public UserNameResolver(IUserService userService)
    {
        _userService = userService;
    }

    public string Resolve(Book source, BookDTO destination, string destMember, ResolutionContext context)
    {
        IDataResult<User> result = _userService.GetById(source.OwnerId);
        if (result.Success)
        {
            string ownerFullName = $"{result.Data.FirstName} {result.Data.LastName}";
            return ownerFullName;
        }

        return result.Message;
    }
}