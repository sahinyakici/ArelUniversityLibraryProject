using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class RentalManager : IRentalService
{
    private readonly IMapper _mapper;
    private IBookService _bookService;
    private IRentalDal _rentalDal;
    private IUserService _userService;

    public RentalManager(IRentalDal rentalDal, IUserService userService, IBookService bookService, IMapper mapper)
    {
        _rentalDal = rentalDal;
        _userService = userService;
        _bookService = bookService;
        _mapper = mapper;
    }

    [CacheAspect]
    [PerformanceAspect(2)]
    public IDataResult<List<RentalDTO>> GetRentalsByUserName(string userName)
    {
        var userGetResult = _userService.GetByUserName(userName);
        if (userGetResult.Success)
        {
            Guid userId = userGetResult.Data.UserId;
            List<Rental> rentalGetResult = _rentalDal.GetAll(rental => rental.UserId == userId);
            List<RentalDTO> rentalDTOs = rentalGetResult.Select(rental => _mapper.Map<RentalDTO>(rental)).ToList();

            return new SuccessDataResult<List<RentalDTO>>(rentalDTOs, Messages.RentalFound);
        }

        return new ErrorDataResult<List<RentalDTO>>(userGetResult.Message);
    }

    [CacheAspect]
    [PerformanceAspect(2)]
    public IDataResult<List<RentalDTO>> GetRentalsByUserId(Guid userId)
    {
        List<Rental> rentalGetResult = _rentalDal.GetAll(rental => rental.UserId == userId);
        List<RentalDTO> rentalDTOs = rentalGetResult.Select(rental => _mapper.Map<RentalDTO>(rental)).ToList();
        return new SuccessDataResult<List<RentalDTO>>(rentalDTOs, Messages.RentalFound);
    }

    [CacheAspect]
    [PerformanceAspect(2)]
    public IDataResult<List<RentalDTO>> GetRentalsByBookId(Guid bookId)
    {
        List<Rental> rentals = _rentalDal.GetAll(rental => rental.BookId == bookId);
        List<RentalDTO> rentalDTOs = rentals.Select(rental => _mapper.Map<RentalDTO>(rental)).ToList();

        return new SuccessDataResult<List<RentalDTO>>(rentalDTOs, Messages.RentalFound);
    }

    [CacheAspect]
    [PerformanceAspect(2)]
    public IDataResult<RentalDTO> GetRentalById(Guid rentalId)
    {
        Rental rental = _rentalDal.Get(rental => rental.RentalId == rentalId);
        RentalDTO rentalDto = _mapper.Map<RentalDTO>(rental);
        return new SuccessDataResult<RentalDTO>(rentalDto, Messages.RentalFound);
    }

    [CacheRemoveAspect("IRentalService.Get")]
    [CacheRemoveAspect("IBookService.Get")]
    [PerformanceAspect(2)]
    [TransactionScopeAspect]
    public IResult Add(Rental rental)
    {
        IResult businessRulesResult = BusinessRules.Run(BookShouldNotBeRented(rental.BookId));
        if (businessRulesResult != null)
        {
            return businessRulesResult;
        }

        var result = _bookService.RentalABook(rental.BookId);
        if (result.Success)
        {
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalIsAdded);
        }

        return new ErrorResult(result.Message);
    }

    [CacheRemoveAspect("IRentalService.Get")]
    [PerformanceAspect(2)]
    [CacheRemoveAspect("IBookService.Get")]
    [TransactionScopeAspect]
    public IResult Update(Rental rental)
    {
        _rentalDal.Update(rental);
        return new SuccessResult(Messages.RentalIsUpdated);
    }

    [CacheRemoveAspect("IRentalService.Get")]
    [PerformanceAspect(2)]
    [CacheRemoveAspect("IBookService.Get")]
    [TransactionScopeAspect]
    public IResult Delete(Rental rental, bool softDelete = true)
    {
        var result = _bookService.CancelRentalABook(rental.BookId);
        if (result.Success)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalIsDeleted);
        }

        return new ErrorResult(result.Message);
    }

    private IResult BookShouldNotBeRented(Guid bookId)
    {
        var result = _bookService.GetById(bookId);
        if (result.Success)
        {
            if (result.Data.RentStatus == false)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.BookAlreadyRented);
        }

        return new ErrorResult(result.Message);
    }
}