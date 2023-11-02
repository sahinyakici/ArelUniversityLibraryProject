﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserDal : EfEntityRepositoryBase<User, PostgreContext>, IUserDal
{
    public List<OperationClaim> GetClaims(User user)
    {
        using (var context = new PostgreContext())
        {
            var result = from operationClaim in context.OperationClaims
                join userOperationClaim in context.UserOperationClaims
                    on operationClaim.OperationClaimId equals userOperationClaim.OperationClaimId
                where userOperationClaim.UserId == user.UserId
                select new OperationClaim
                    { OperationClaimId = operationClaim.OperationClaimId, Name = operationClaim.Name };
            return result.ToList();
        }
    }
}