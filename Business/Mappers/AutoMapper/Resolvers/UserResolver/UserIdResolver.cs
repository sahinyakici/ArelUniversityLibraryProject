using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers.UserResolver;

public class UserIdResolver : IValueResolver<BookDTO, Book, Guid>
{
    private IUserService _userService;

    public UserIdResolver(IUserService userService)
    {
        _userService = userService;
    }

    public Guid Resolve(BookDTO source, Book destination, Guid destMember, ResolutionContext context)
    {
        User user = _userService.GetAll().Data
            .Where(user => string.Equals(user.UserName, source.OwnerName, StringComparison.OrdinalIgnoreCase))
            .SingleOrDefault();
        return user.UserId;
    }
}