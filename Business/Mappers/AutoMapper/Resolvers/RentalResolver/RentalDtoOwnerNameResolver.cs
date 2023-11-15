using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Mappers.AutoMapper.Resolvers.RentalResolver;

public class RentalDtoOwnerNameResolver : IValueResolver<Rental, RentalDTO, string>
{
    private readonly IBookService _bookService;
    private readonly IUserService _userService;

    public RentalDtoOwnerNameResolver(IUserService userService, IBookService bookService)
    {
        _userService = userService;
        _bookService = bookService;
    }

    public string Resolve(Rental source, RentalDTO destination, string destMember, ResolutionContext context)
    {
        var bookGetResult = _bookService.GetById(source.BookId);
        if (bookGetResult.Success)
        {
            Book book = bookGetResult.Data;
            var ownerGetResult = _userService.GetById(book.OwnerId);
            if (ownerGetResult.Success)
            {
                User owner = ownerGetResult.Data;
                return owner.UserName;
            }

            throw new Exception(ownerGetResult.Message);
        }

        throw new Exception(bookGetResult.Message);
    }
}