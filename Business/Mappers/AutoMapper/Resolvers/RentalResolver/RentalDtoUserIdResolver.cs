using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers.RentalResolver;

public class RentalDtoUserIdResolver : IValueResolver<RentalDTO, Rental, Guid>
{
    private readonly IUserService _userService;

    public RentalDtoUserIdResolver(IUserService userService)
    {
        _userService = userService;
    }

    public Guid Resolve(RentalDTO source, Rental destination, Guid destMember, ResolutionContext context)
    {
        var result = _userService.GetByUserName(source.UserName);
        if (result.Success)
        {
            User user = result.Data;
            return user.UserId;
        }

        throw new Exception(result.Message);
    }
}