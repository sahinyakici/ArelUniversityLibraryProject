using AutoMapper;
using Business.Abstract;
using Core.Entities;
using Core.Entities.Concrete;

namespace Business.Mappers.AutoMapper.Resolvers.UserOperationClaimResolver;

public class UserOperationUserIdResolver : IValueResolver<UserOperationClaimDto, UserOperationClaim, Guid>
{
    private IUserService _userService;

    public UserOperationUserIdResolver(IUserService userService)
    {
        _userService = userService;
    }

    public Guid Resolve(UserOperationClaimDto source, UserOperationClaim destination, Guid destMember,
        ResolutionContext context)
    {
        var result = _userService.GetByUserName(source.UserName);
        if (result.Success)
        {
            return result.Data.UserId;
        }

        throw new Exception();
    }
}