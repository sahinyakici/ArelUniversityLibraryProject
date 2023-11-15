using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IRentalService
{
    IDataResult<List<RentalDTO>> GetRentalsByUserName(string userName, bool withDeleted = false);
    IDataResult<List<RentalDTO>> GetRentalsByUserId(Guid userId, bool withDeleted = false);
    IDataResult<List<RentalDTO>> GetRentalsByBookId(Guid bookId, bool withDeleted = false);
    IDataResult<RentalDTO> GetRentalById(Guid rentalId, bool withDeleted = false);
    IResult Add(Rental rental);
    IResult Update(RentalDTO rentalDto);
    IResult Delete(Guid id, bool permanently = false);
    IResult CancelRental(Guid rentalId);
}