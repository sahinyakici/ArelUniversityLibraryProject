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
        Book book = _bookService.GetById(source.BookId).Data;
        User owner = _userService.GetById(book.OwnerId).Data;
        return owner.UserName;
    }
}