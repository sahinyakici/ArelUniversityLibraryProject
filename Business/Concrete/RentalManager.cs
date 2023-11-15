using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
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
    [SecuredOperation("admin,editor,user")]
    public IDataResult<List<RentalDTO>> GetRentalsByUserName(string userName, bool withDeleted = false)
    {
        var userGetResult = _userService.GetByUserName(userName);
        if (userGetResult.Success)
        {
            Guid userId = userGetResult.Data.UserId;
            List<Rental> rentalGetResult =
                _rentalDal.GetAll(rental => rental.UserId == userId && (withDeleted || !rental.IsDeleted));
            List<RentalDTO> rentalDTOs = rentalGetResult.Select(rental => _mapper.Map<RentalDTO>(rental)).ToList();

            return new SuccessDataResult<List<RentalDTO>>(rentalDTOs, Messages.RentalFound);
        }

        return new ErrorDataResult<List<RentalDTO>>(userGetResult.Message);
    }

    [CacheAspect]
    [PerformanceAspect(2)]
    [SecuredOperation("admin,editor,user")]
    public IDataResult<List<RentalDTO>> GetRentalsByUserId(Guid userId, bool withDeleted = false)
    {
        List<Rental> rentalGetResult =
            _rentalDal.GetAll(rental => rental.UserId == userId && (withDeleted || !rental.IsDeleted));
        List<RentalDTO> rentalDTOs = rentalGetResult.Select(rental => _mapper.Map<RentalDTO>(rental)).ToList();
        return new SuccessDataResult<List<RentalDTO>>(rentalDTOs, Messages.RentalFound);
    }

    [CacheAspect]
    [PerformanceAspect(2)]
    [SecuredOperation("admin,editor,user")]
    public IDataResult<List<RentalDTO>> GetRentalsByBookId(Guid bookId, bool withDeleted = false)
    {
        List<Rental> rentals =
            _rentalDal.GetAll(rental => rental.BookId == bookId && (withDeleted || !rental.IsDeleted));
        List<RentalDTO> rentalDTOs = rentals.Select(rental => _mapper.Map<RentalDTO>(rental)).ToList();

        return new SuccessDataResult<List<RentalDTO>>(rentalDTOs, Messages.RentalFound);
    }

    [CacheAspect]
    [PerformanceAspect(2)]
    [SecuredOperation("admin,editor,user")]
    public IDataResult<RentalDTO> GetRentalById(Guid rentalId, bool withDeleted = false)
    {
        Rental rental = _rentalDal.Get(rental => rental.RentalId == rentalId && (withDeleted || !rental.IsDeleted));
        RentalDTO rentalDto = _mapper.Map<RentalDTO>(rental);
        return new SuccessDataResult<RentalDTO>(rentalDto, Messages.RentalFound);
    }

    [CacheRemoveAspect("IRentalService.Get")]
    [CacheRemoveAspect("IBookService.Get")]
    [PerformanceAspect(2)]
    [TransactionScopeAspect]
    [SecuredOperation("admin,editor,user")]
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
    [SecuredOperation("admin,editor,user")]
    public IResult Update(RentalDTO rentalDto)
    {
        Rental updateRental = _mapper.Map<Rental>(rentalDto);
        _rentalDal.Update(updateRental);
        return new SuccessResult(Messages.RentalIsUpdated);
    }

    [CacheRemoveAspect("IRentalService.Get")]
    [PerformanceAspect(2)]
    [CacheRemoveAspect("IBookService.Get")]
    [TransactionScopeAspect]
    [SecuredOperation("admin,editor,user")]
    public IResult Delete(Guid id, bool permanently = false)
    {
        Rental rental = _rentalDal.Get(rental => rental.RentalId == id);
        if (rental != null)
        {
            if (!permanently)
            {
                rental.IsDeleted = true;
                rental.DeleteTime = DateTime.UtcNow.ToLocalTime();
                _rentalDal.Update(rental);
                return new SuccessResult(Messages.RentalIsDeleted);
            }

            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalIsDeletedPermanently);
        }

        return new ErrorResult(Messages.RentalNotFound);
    }

    [CacheRemoveAspect("IRentalService.Get")]
    [PerformanceAspect(2)]
    [CacheRemoveAspect("IBookService.Get")]
    [TransactionScopeAspect]
    [SecuredOperation("admin,editor,user")]
    public IResult CancelRental(Guid rentalId)
    {
        Rental rental = _rentalDal.Get(rental => rental.RentalId == rentalId);
        var result = _bookService.CancelRentalABook(rental.BookId);
        if (result.Success)
        {
            var deleteResult = Delete(rental.RentalId);
            return new SuccessResult(deleteResult.Message);
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