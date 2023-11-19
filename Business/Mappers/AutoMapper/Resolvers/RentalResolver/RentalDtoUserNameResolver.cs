using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers.RentalResolver;

public class RentalDtoUserNameResolver : IValueResolver<Rental, RentalDTO, string>
{
    private readonly IUserService _userService;

    public RentalDtoUserNameResolver(IUserService userService)
    {
        _userService = userService;
    }

    public string Resolve(Rental source, RentalDTO destination, string destMember, ResolutionContext context)
    {
        User user = _userService.GetById(source.UserId).Data;
        return user.UserName;
    }
}